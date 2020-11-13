using System;
using System.Collections.Generic;
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

		unsafe string[] modifyMachHeaderAndReturnNSArrayOfLoadedDylibs (byte* macho, int sz, bool INJECT_MARZIPAN_GLUE = true, bool DRY_RUN = false)
		{
			var dylibs = new List<string> ();

			var header_fat = (fat_header*)macho;
			var imageHeaderPtr = (byte*)macho;

			long header64offset = 0;

			if (header_fat->magic == FAT.CIGAM || header_fat->magic == FAT.MAGIC) {
				int narchs = OSSwapBigToHostInt32 (header_fat->nfat_arch);
				imageHeaderPtr += sizeof (fat_header*);

				for (int i = 0; i < narchs; i++) {
					fat_arch uarch = *(fat_arch*)imageHeaderPtr;

					if ((cpu_type_t)OSSwapBigToHostInt32 ((int)uarch.cputype) == cpu_type_t.X86_64) {
						//printf("mach_header_64 offset = %u\n", OSSwapBigToHostInt32(uarch.offset));
						header64offset = OSSwapBigToHostInt32 (uarch.offset) - 32 + sizeof (mach_header_64);
						break;
					}
					else if ((cpu_type_t)OSSwapBigToHostInt32 ((int)uarch.cputype) == cpu_type_t.ARM64) {
						//printf("mach_header_64 offset = %u\n", OSSwapBigToHostInt32(uarch.offset));
						header64offset = OSSwapBigToHostInt32 (uarch.offset) - 32 + sizeof (mach_header_64);
						break;
					}
					else
						imageHeaderPtr += sizeof (fat_arch);
				}

				if (header64offset == 0) {
					throw new Exception ("ERROR: No X86_64 or ARM64 slice found.\n");
				}
			}

			imageHeaderPtr = (byte*)(macho + header64offset);

			var header64 = (mach_header_64*)imageHeaderPtr;

			if (header64->magic != MH.MAGIC_64)
				return Array.Empty<string> ();

			imageHeaderPtr += sizeof (mach_header_64);
			var command = (load_command*)(imageHeaderPtr);

			for (int i = 0; i < header64->ncmds && header64->ncmds > 0; ++i) {
				if (command->cmd == LC.LOAD_DYLIB) {
					var ucmd = *(dylib_command*)imageHeaderPtr;
					var offset = ucmd.dylib.name_offset;
					var size = ucmd.cmdsize;

					//printf("LC_LOAD_DYLIB %s\n", name);
					if (Marshal.PtrToStringUTF8 (new IntPtr (imageHeaderPtr + offset), (int)size) is string name) {
						dylibs.Add (name);
					}
				}
				else if (command->cmd == LC.VERSION_MIN_IPHONEOS) {
					if (INJECT_MARZIPAN_GLUE) {
						Console.WriteLine ("WARNING: This binary was built with an earlier iOS SDK.");
					}
					else {
						Console.WriteLine ("ERROR: This binary was built with an earlier iOS SDK. As of macOS 10.14 beta 3, it needs to be rebuilt with a minimum deployment target of iOS 12.");
						Console.WriteLine ("\nNOTE: iOSMac binaries require the LC_BUILD_VERSION load command to be present. This is added automatically by the linker when the minimum deployment target is iOS 12.0 or macOS 10.14, and cannot be added to existing binaries for older OSes. Use the INJECT_MARZIPAN_GLUE=1 environment variable to use code injection to attempt to work around this.\n\n");
					}

					var ucmd = *(version_min_command*)imageHeaderPtr;
					ucmd.cmd = LC.VERSION_MIN_MACOSX;
					ucmd.sdk = 10 << 16 | 14 << 8 | 0;
					ucmd.version = 10 << 16 | 14 << 8 | 0;

					if (!DRY_RUN) {
						//memcpy (imageHeaderPtr, &ucmd, ucmd.cmdsize);
					}
				}
				else if (command->cmd == LC.BUILD_VERSION) {
					var ucmd = *(build_version_command*)imageHeaderPtr;
					ucmd.platform = PLATFORM.MACCATALYST;
					ucmd.minos = 12 << 16 | 0 << 8 | 0;
					ucmd.sdk = 10 << 16 | 14 << 8 | 0;

					if (!DRY_RUN) {
						//memcpy (imageHeaderPtr, &ucmd, ucmd.cmdsize);
					}
				}

				imageHeaderPtr += command->cmdsize;
				command = (load_command*)imageHeaderPtr;
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
