using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using maccat.MachO;

namespace maccat
{
	public class Marzipanify
	{
		public Marzipanify ()
		{
		}

		public unsafe string ModifyMachHeaderAndReturnNSArrayOfLoadedDylibs (string inPath, bool injectMarzipanGlue = false, bool dryRun = false)
		{
			var bytes = File.ReadAllBytes (inPath);
			string[] dsyms;
			fixed (byte* p = bytes) {
				dsyms = ModifyMachHeaderAndReturnNSArrayOfLoadedDylibs (p, bytes.Length, injectMarzipanGlue: injectMarzipanGlue, dryRun: dryRun);
			}
			if (!dryRun) {
				var outPath = Path.GetTempFileName ();
				File.WriteAllBytes (outPath, bytes);
				return outPath;
			}
			return inPath;
		}

		unsafe string[] ModifyMachHeaderAndReturnNSArrayOfLoadedDylibs (byte* fileBytes, int sz, bool injectMarzipanGlue, bool dryRun)
		{
			var header_fat = (fat_header*)fileBytes;
			var ptr = (byte*)fileBytes;

			long header64offset = 0;

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
						header64offset = OSSwapBigToHostInt32 (uarch.offset) - 32 + sizeof (mach_header_64);
						break;
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

				if (header64offset == 0) {
					throw new Exception ("ERROR: No X86_64 slice found.\n");
				}
			}

			//
			// Marz
			//
			return MarzMachO (ptr + header64offset, injectMarzipanGlue: injectMarzipanGlue, dryRun: dryRun);
		}

		unsafe string[] MarzMachO (byte *macho, bool injectMarzipanGlue, bool dryRun)
		{
			var ptr = macho;
			var dylibs = new List<string> ();

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
					if (Marshal.PtrToStringUTF8 (new IntPtr (ptr + offset)) is string name) {
						dylibs.Add (name);
					}
				}
				else if (command->cmd == LC.VERSION_MIN_IPHONEOS) {
					if (injectMarzipanGlue) {
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine ("WARNING: This binary was built with an earlier iOS SDK.");
						Console.ResetColor ();
					}
					else {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine ("ERROR: This binary needs to be rebuilt with a minimum deployment target of iOS 12.");
						Console.ResetColor ();
					}

					var ucmd = (version_min_command*)ptr;
					if (!dryRun) {
						ucmd->cmd = LC.VERSION_MIN_MACOSX;
						ucmd->sdk = 10 << 16 | 14 << 8 | 0;
						ucmd->version = 10 << 16 | 14 << 8 | 0;
					}
				}
				else if (command->cmd == LC.BUILD_VERSION) {
					var ucmd = (build_version_command*)ptr;
					if (!dryRun) {
						ucmd->platform = PLATFORM.MACCATALYST;
						ucmd->minos = 12 << 16 | 0 << 8 | 0;
						ucmd->sdk = 10 << 16 | 14 << 8 | 0;
					}
				}

				ptr += command->cmdsize;
				command = (load_command*)ptr;
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
}
