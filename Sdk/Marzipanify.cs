using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using maccat.MachO;
using static maccat.Terminal;

namespace maccat
{
	/// <summary>
	/// Based on https://github.com/steventroughtonsmith/marzipanify/blob/master/marzipanify/main.m
	/// </summary>
	public class Marzipanify
	{
		readonly Dictionary<string, string> NewDylibs = new Dictionary<string, string> ();
		private readonly string outputAppDir;

		public Marzipanify (string outputAppDir)
		{
			this.outputAppDir = outputAppDir;
		}

		public async Task<string> ModifyMachHeaderAsync (string inPath, bool injectMarzipanGlue = false, bool dryRun = false)
		{
			try {
				return await MarzFile (Path.GetFileName (inPath), inPath, injectMarzipanGlue: injectMarzipanGlue, dryRun: dryRun);
			}
			catch (OldXcodeException) {
				return "";
			}
		}

		async Task<string> MarzFile (string name, string inPath, bool injectMarzipanGlue, bool dryRun)
		{
			var fileBytes = File.ReadAllBytes (inPath);

			var (index, length) = GetCpuHeader (fileBytes);

			if (GetMachMagic (fileBytes) == MH.MAGIC_64) {
				var dsyms = MarzMachO (name, fileBytes, index, injectMarzipanGlue: injectMarzipanGlue, dryRun: dryRun);

				if (!dryRun) {
					var outPath = Path.GetTempFileName ();
					File.WriteAllBytes (outPath, fileBytes);
					await RenameDylibsAsync (outPath, dsyms);
					return outPath;
				}
				return inPath;
			}

			return await MarzArchive (name, fileBytes, index, length, dryRun: dryRun);
		}

		unsafe MH GetMachMagic (byte[] fileBytes)
		{
			fixed (byte* ptr = fileBytes) {
				var machHeader = (mach_header_64*)ptr;
				return machHeader->magic;
			}
		}

		async Task RenameDylibsAsync (string outPath, string[] dsyms)
		{
			if (dsyms.Length == 0)
				return;
			var args = "";
			foreach (var d in dsyms) {
				args += " " + await NewLinkerPathForLoadedDylib (d);
			}
			args += $" \"{outPath}\"";
			await ExecAsync ("install_name_tool", args);
		}

		async Task<string> NewLinkerPathForLoadedDylib (string loadedDylib)
		{
			if (loadedDylib.StartsWith ("/System/iOSSupport"))
				return "";

			var possibleiOSMacDylibPath = Path.Combine ("/System/iOSSupport", loadedDylib[1..]);

			if (File.Exists (possibleiOSMacDylibPath)) {
				return $"-change \"{loadedDylib}\" \"{possibleiOSMacDylibPath}\"";
			}

			if (!File.Exists (loadedDylib)) {
				Warning ($"The library {Path.GetFileName (loadedDylib)} is not available.");
				possibleiOSMacDylibPath = await MakeDummyLibraryAsync (loadedDylib);
				return $"-change \"{loadedDylib}\" \"@executable_path/../Frameworks/{Path.GetFileName (possibleiOSMacDylibPath)}\"";
			}

			return "";
		}

		async Task<string> MakeDummyLibraryAsync (string loadedDylib)
		{
			await Task.Yield ();

			var name = Path.GetFileName (loadedDylib);

			if (!NewDylibs.TryGetValue (name, out var outPath)) {
				var outSrcPath = outPath + ".c";
				File.WriteAllText (outSrcPath, $@"int dummy_{name}() {{ return 42; }}");
				var fwsPath = Path.Combine (outputAppDir, "Contents", "Frameworks");
				Directory.CreateDirectory (fwsPath);
				outPath = Path.Combine (fwsPath, name + ".dylib");
				await ClangAsync ($"-dynamiclib \"{outSrcPath}\" -o \"{outPath}\"");
				NewDylibs[name] = outPath;
			}

			return outPath;
		}

		unsafe (int Offset, int Size) GetCpuHeader (byte[] fileBytes)
		{
			fixed (byte* fptr = fileBytes) {
				var ptr = fptr;
				var header_fat = (fat_header*)ptr;

				//
				// Read FAT if it's there
				//
				if (header_fat->magic == FAT.CIGAM || header_fat->magic == FAT.MAGIC) {
					int narchs = OSSwapBigToHostInt32 (header_fat->nfat_arch);
					ptr += sizeof (fat_header);

					for (int i = 0; i < narchs; i++) {
						var uarch = *(fat_arch*)ptr;

						var cpuType = (cpu_type_t)OSSwapBigToHostInt32 ((int)uarch.cputype);

						if (cpuType == MachO.cpu_type_t.X86_64) {
							//printf("mach_header_64 offset = %u\n", OSSwapBigToHostInt32(uarch.offset));
							return (OSSwapBigToHostInt32 (uarch.offset) - 32 + sizeof (mach_header_64), OSSwapBigToHostInt32 (uarch.size));
						}
						//else if (cpuType == MachO.cpu_type_t.ARM64) {
						//	//printf("mach_header_64 offset = %u\n", OSSwapBigToHostInt32(uarch.offset));
						//	header64offset = OSSwapBigToHostInt32 (uarch.offset) - 32 + sizeof (mach_header_64);
						//	break;
						//}
						else {
							ptr += sizeof (fat_arch);
						}
					}

					throw new Exception ("ERROR: No X86_64 slice found.\n");
				}
			}

			// Not FAT
			return (0, fileBytes.Length);
		}

		async Task<string> MarzArchive (string arName, byte[] arBuffer, int arIndex, int arLength, bool dryRun)
		{
			var arPath = Path.Combine (Path.GetTempPath (), arName);
			var arXPath = Path.Combine (Path.GetTempPath (), arName + ".out");
			await File.WriteAllBytesAsync (arPath, arBuffer[arIndex..(arIndex + arLength)]);
			Directory.CreateDirectory (arXPath);
			//Console.WriteLine ("Extracting to " + arXPath);
			await ArAsync ($"-o -x \"{arPath}\"", cd: arXPath);

			var newOPaths = new List<string> ();
			foreach (var oPath in Directory.GetFiles (arXPath, "*.o")) {
				var newOPath = await MarzFile (arName, oPath, injectMarzipanGlue: false, dryRun: dryRun);
				//Console.WriteLine (newOPath);
				newOPaths.Add (newOPath);
			}

			var arNewPath = Path.Combine (Path.GetTempPath (), Path.GetFileNameWithoutExtension (arName) + ".catalyst.a");

			await ArAsync ($"-r \"{arNewPath}\" {string.Join (" ", newOPaths.Select (x => $"\"{x}\""))}");

			return arNewPath;
		}

		readonly HashSet<string> ios11Errors = new HashSet<string> ();

		unsafe string[] MarzMachO (string machoName, byte[] machoBuffer, int machoIndex, bool injectMarzipanGlue, bool dryRun)
		{
			var dylibs = new List<string> ();

			fixed (byte* macho = machoBuffer) {
				var ptr = macho + machoIndex;

				var header64 = (mach_header_64*)ptr;

				if (header64->magic != MH.MAGIC_64)
					return Array.Empty<string> ();

				ptr += sizeof (mach_header_64);
				var command = (load_command*)(ptr);

				for (int i = 0; i < header64->ncmds && header64->ncmds > 0; ++i) {
					if (command->cmd == LC.LOAD_DYLIB) {
						var ucmd = *(dylib_command*)ptr;
						var offset = ucmd.dylib.name_offset;
						var size = ucmd.cmdsize;

						//printf("LC_LOAD_DYLIB %s\n", name);
						if (Marshal.PtrToStringUTF8 (new IntPtr (ptr + offset)) is string name && !string.IsNullOrEmpty (name)) {
							dylibs.Add (name);
						}
					}
					else if (command->cmd == LC.VERSION_MIN_IPHONEOS) {
						if (injectMarzipanGlue) {
							Warning ($"{machoName} was built with an earlier iOS SDK.");
						}
						else {
							if (!ios11Errors.Contains (machoName)) {
								ios11Errors.Add (machoName);
								Warning ($"{machoName} needs to be rebuilt with a minimum deployment target of iOS 12. Any code that uses it will crash.");
							}
							throw new OldXcodeException ();
						}

						if (!dryRun) {
							if (sizeof (build_version_command) <= sizeof (version_min_command)) {
								var ucmd = (build_version_command*)ptr;
								ucmd->cmd = LC.BUILD_VERSION;
								ucmd->platform = PLATFORM.MACCATALYST;
								ucmd->minos = 12 << 16 | 0 << 8 | 0;
								ucmd->sdk = 10 << 16 | 14 << 8 | 0;
							}
							else {
								var vcmd = (version_min_command*)ptr;
								vcmd->cmd = LC.VERSION_MIN_MACOSX;
								vcmd->sdk = 10 << 16 | 14 << 8 | 0;
								vcmd->version = 10 << 16 | 14 << 8 | 0;
							}
						}
					}
					else if (command->cmd == LC.BUILD_VERSION) {
						if (!dryRun) {
							var ucmd = (build_version_command*)ptr;
							ucmd->platform = PLATFORM.MACCATALYST;
							ucmd->minos = 12 << 16 | 0 << 8 | 0;
							ucmd->sdk = 10 << 16 | 14 << 8 | 0;
						}
					}

					ptr += command->cmdsize;
					command = (load_command*)ptr;
				}
			}

			return dylibs.ToArray ();
		}

		int OSSwapBigToHostInt32 (int v)
		{
			return System.Net.IPAddress.HostToNetworkOrder (v);
		}

		int OSSwapBigToHostInt32 (uint v)
		{
			unchecked {
				return System.Net.IPAddress.HostToNetworkOrder ((int)v);
			}
		}
	}

	public class OldXcodeException : Exception
	{
	}
}
