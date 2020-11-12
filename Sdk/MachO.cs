using System;
using System.Net;
using System.Runtime.InteropServices;
using uint32_t = System.UInt32;

namespace maccat.MachO
{
	public static class NX
	{
		public static uint SwapLong (uint v)
		{
			unchecked {
				return (uint)IPAddress.HostToNetworkOrder ((int)v);
			}
		}
	}

	public static class FAT
	{
		public const uint MAGIC = 0xcafebabe;
		public static readonly uint CIGAM = NX.SwapLong (0xcafebabe);
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct fat_header
	{
		public uint magic;        /* FAT_MAGIC */
		public uint nfat_arch;    /* number of structs that follow */
	};

	[StructLayout (LayoutKind.Sequential)]
	public struct fat_arch
	{
		public cpu_type_t cputype; /* cpu specifier (int) */
		public cpu_subtype_t cpusubtype;   /* machine specifier (int) */
		public uint offset;       /* file offset to this object file */
		public uint size;     /* size of this object file */
		public uint align;        /* alignment as a power of 2 */
	};

	public enum cpu_type_t : int
	{
	}

	public enum cpu_subtype_t : int
	{
	}

	/*
	 * The mach header appears at the very beginning of the object file; it
	 * is the same for both 32-bit and 64-bit architectures.
	 */
	[StructLayout (LayoutKind.Sequential)]
	public struct mach_header
	{
		public uint magic;     /* mach magic number identifier */
		public cpu_type_t cputype; /* cpu specifier */
		public cpu_subtype_t cpusubtype;   /* machine specifier */
		public uint filetype;  /* type of file */
		public uint ncmds;     /* number of load commands */
		public uint sizeofcmds;    /* the size of all the load commands */
		public uint flags;     /* flags */
	};

	/*
	 * The 64-bit mach header appears at the very beginning of object files for
	 * 64-bit architectures.
	 */
	[StructLayout (LayoutKind.Sequential)]
	public struct mach_header_64
	{
		public uint magic;     /* mach magic number identifier */
		public cpu_type_t cputype; /* cpu specifier */
		public cpu_subtype_t cpusubtype;   /* machine specifier */
		public uint filetype;  /* type of file */
		public uint ncmds;     /* number of load commands */
		public uint sizeofcmds;    /* the size of all the load commands */
		public uint flags;     /* flags */
		public uint reserved;  /* reserved */
	};
}
