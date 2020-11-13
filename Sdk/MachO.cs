using System;
using System.Net;
using System.Runtime.InteropServices;
using uint32_t = System.UInt32;

namespace maccat.MachO
{
	// /Library/Developer/CommandLineTools/SDKs/MacOSX10.15.sdk/usr/include/mach-o/loader.h
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

	public enum cpu_type_t : uint
	{
		/*
		 * Capability bits used in the definition of cpu_type.
		 */
		ARCH_MASK = 0xff000000,      /* mask for architecture bits */
		ARCH_ABI64 = 0x01000000,      /* 64 bit ABI */
		ARCH_ABI64_32 = 0x02000000,      /* ABI for 64-bit hardware with 32-bit types; LP32 */

		/*
		 *	Machine types known by all.
		 */

		ANY = 0xffffffff,

		VAX = (1),
		/* skip				((cpu_type_t) 2)	*/
		/* skip				((cpu_type_t) 3)	*/
		/* skip				((cpu_type_t) 4)	*/
		/* skip				((cpu_type_t) 5)	*/
		MC680x0 = (6),
		X86 = (7),
		I386 = X86,            /* compatibility */
		X86_64 = (X86 | ARCH_ABI64),

		/* skip CPU_TYPE_MIPS		((cpu_type_t) 8)	*/
		/* skip                         ((cpu_type_t) 9)	*/
		MC98000 = (10),
		HPPA = (11),
		ARM = (12),
		ARM64 = (ARM | ARCH_ABI64),
		ARM64_32 = (ARM | ARCH_ABI64_32),
		MC88000 = (13),
		SPARC = (14),
		I860 = (15),
		/* skip	CPU_TYPE_ALPHA		((cpu_type_t) 16)	*/
		/* skip				((cpu_type_t) 17)	*/
		POWERPC = (18),
		POWERPC64 = (POWERPC | ARCH_ABI64),
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
		public MH magic;     /* mach magic number identifier */
		public cpu_type_t cputype; /* cpu specifier */
		public cpu_subtype_t cpusubtype;   /* machine specifier */
		public uint filetype;  /* type of file */
		public uint ncmds;     /* number of load commands */
		public uint sizeofcmds;    /* the size of all the load commands */
		public uint flags;     /* flags */
		public uint reserved;  /* reserved */
	};

	/* Constant for the magic field of the mach_header_64 (64-bit architectures) */
	public enum MH : uint
	{
		MAGIC_64 = 0xfeedfacf, /* the 64-bit mach magic number */
		CIGAM_64 = 0xcffaedfe, /* NXSwapInt(MH_MAGIC_64) */
	}

	/*
	 * The load commands directly follow the mach_header.  The total size of all
	 * of the commands is given by the sizeofcmds field in the mach_header.  All
	 * load commands must have as their first two fields cmd and cmdsize.  The cmd
	 * field is filled in with a constant for that command type.  Each command type
	 * has a structure specifically for it.  The cmdsize field is the size in bytes
	 * of the particular load command structure plus anything that follows it that
	 * is a part of the load command (i.e. section structures, strings, etc.).  To
	 * advance to the next load command the cmdsize can be added to the offset or
	 * pointer of the current load command.  The cmdsize for 32-bit architectures
	 * MUST be a multiple of 4 bytes and for 64-bit architectures MUST be a multiple
	 * of 8 bytes (these are forever the maximum alignment of any load commands).
	 * sizeof(long) (this is forever the maximum alignment of any load commands).
	 * The padded bytes must be zero.  All tables in the object file must also
	 * follow these rules so the file can be memory mapped.  Otherwise the pointers
	 * to these tables will not work well or at all on some machines.  With all
	 * padding zeroed like objects will compare byte for byte.
	 */
	[StructLayout (LayoutKind.Sequential)]
	public struct load_command
	{
		public LC cmd;      /* type of load command */
		public uint cmdsize;      /* total size of command in bytes */
	};

	/* Constants for the cmd field of all load commands, the type */
	public enum LC : uint
	{
		SEGMENT = 0x1,  /* segment of this file to be mapped */
		SYMTAB = 0x2,   /* link-edit stab symbol table info */
		SYMSEG = 0x3,   /* link-edit gdb symbol table info (obsolete) */
		THREAD = 0x4,   /* thread */
		UNIXTHREAD = 0x5,   /* unix thread (includes a stack) */
		LOADFVMLIB = 0x6,   /* load a specified fixed VM shared library */
		IDFVMLIB = 0x7, /* fixed VM shared library identification */
		IDENT = 0x8,    /* object identification info (obsolete) */
		FVMFILE = 0x9,  /* fixed VM file inclusion (internal use) */
		PREPAGE = 0xa,     /* prepage command (internal use) */
		DYSYMTAB = 0xb, /* dynamic link-edit symbol table info */
		LOAD_DYLIB = 0xc,   /* load a dynamicly linked shared library */
		ID_DYLIB = 0xd, /* dynamicly linked shared lib identification */
		LOAD_DYLINKER = 0xe,    /* load a dynamic linker */
		ID_DYLINKER = 0xf,  /* dynamic linker identification */
		PREBOUND_DYLIB = 0x10,  /* modules prebound for a dynamicly linked shared library */

		ROUTINES = 0x11,    /* image routines */
		SUB_FRAMEWORK = 0x12,   /* sub framework */
		SUB_UMBRELLA = 0x13,    /* sub umbrella */
		SUB_CLIENT = 0x14,  /* sub client */
		SUB_LIBRARY = 0x15, /* sub library */
		TWOLEVEL_HINTS = 0x16,  /* two-level namespace lookup hints */
		PREBIND_CKSUM = 0x17,   /* prebind checksum */

		/*
		 * After MacOS X 10.1 when a new load command is added that is required to be
		 * understood by the dynamic linker for the image to execute properly the
		 * LC_REQ_DYLD bit will be or'ed into the load command constant.  If the dynamic
		 * linker sees such a load command it it does not understand will issue a
		 * "unknown load command required for execution" error and refuse to use the
		 * image.  Other load commands without this bit that are not understood will
		 * simply be ignored.
		 */
		REQ_DYLD = 0x80000000,

		/*
		 * load a dynamically linked shared library that is allowed to be missing
		 * (all symbols are weak imported).
		 */
		LOAD_WEAK_DYLIB = (0x18 | REQ_DYLD),

		SEGMENT_64 = 0x19,  /* 64-bit segment of this file to be mapped */
		ROUTINES_64 = 0x1a, /* 64-bit image routines */
		UUID = 0x1b,    /* the uuid */
		RPATH = (0x1c | REQ_DYLD),    /* runpath additions */
		CODE_SIGNATURE = 0x1d,  /* local of code signature */
		SEGMENT_SPLIT_INFO = 0x1e, /* local of info to split segments */
		REEXPORT_DYLIB = (0x1f | REQ_DYLD), /* load and re-export dylib */
		LAZY_LOAD_DYLIB = 0x20, /* delay load of dylib until first use */
		ENCRYPTION_INFO = 0x21, /* encrypted segment information */
		DYLD_INFO = 0x22,   /* compressed dyld information */
		DYLD_INFO_ONLY = (0x22 | REQ_DYLD),  /* compressed dyld information only */
		LOAD_UPWARD_DYLIB = (0x23 | REQ_DYLD), /* load upward dylib */
		VERSION_MIN_MACOSX = 0x24,   /* build for MacOSX min OS version */
		VERSION_MIN_IPHONEOS = 0x25, /* build for iPhoneOS min OS version */
		FUNCTION_STARTS = 0x26, /* compressed table of function start addresses */
		DYLD_ENVIRONMENT = 0x27, /* string for dyld to treat like environment variable */
		MAIN = (0x28 | REQ_DYLD), /* replacement for LC_UNIXTHREAD */
		DATA_IN_CODE = 0x29, /* table of non-instructions in __text */
		SOURCE_VERSION = 0x2A, /* source version used to build binary */
		DYLIB_CODE_SIGN_DRS = 0x2B, /* Code signing DRs copied from linked dylibs */
		ENCRYPTION_INFO_64 = 0x2C, /* 64-bit encrypted segment information */
		LINKER_OPTION = 0x2D, /* linker options in MH_OBJECT files */
		LINKER_OPTIMIZATION_HINT = 0x2E, /* optimization hints in MH_OBJECT files */
		VERSION_MIN_TVOS = 0x2F, /* build for AppleTV min OS version */
		VERSION_MIN_WATCHOS = 0x30, /* build for Watch min OS version */
		NOTE = 0x31, /* arbitrary data included within a Mach-O file */
		BUILD_VERSION = 0x32, /* build for platform min OS version */
		DYLD_EXPORTS_TRIE = (0x33 | REQ_DYLD), /* used with linkedit_data_command, payload is trie */
		DYLD_CHAINED_FIXUPS = (0x34 | REQ_DYLD), /* used with linkedit_data_command */

	}

	/*
 * Dynamicly linked shared libraries are identified by two things.  The
 * pathname (the name of the library as found for execution), and the
 * compatibility version number.  The pathname must match and the compatibility
 * number in the user of the library must be greater than or equal to the
 * library being used.  The time stamp is used to record the time a library was
 * built and copied into user so it can be use to determined if the library used
 * at runtime is exactly the same as used to built the program.
 */
	[StructLayout (LayoutKind.Sequential)]
	public struct dylib
	{
		public uint32_t name_offset;			/* library's path name */
		public uint32_t timestamp;         /* library's build time stamp */
		public uint32_t current_version;       /* library's current version number */
		public uint32_t compatibility_version; /* library's compatibility vers number*/
	};

	/*
 * A dynamically linked shared library (filetype == MH_DYLIB in the mach header)
 * contains a dylib_command (cmd == LC_ID_DYLIB) to identify the library.
 * An object that uses a dynamically linked shared library also contains a
 * dylib_command (cmd == LC_LOAD_DYLIB, LC_LOAD_WEAK_DYLIB, or
 * LC_REEXPORT_DYLIB) for each library it uses.
 */
	[StructLayout (LayoutKind.Sequential)]
	public struct dylib_command
	{
		public LC cmd;       /* LC_ID_DYLIB, LC_LOAD_{,WEAK_}DYLIB,
					   LC_REEXPORT_DYLIB */
		public uint32_t cmdsize;   /* includes pathname string */
		public dylib dylib;		/* the library identification */
};

	/*
 * The version_min_command contains the min OS version on which this 
 * binary was built to run.
 */
	[StructLayout (LayoutKind.Sequential)]
	public struct version_min_command
	{
		public LC cmd;       /* LC_VERSION_MIN_MACOSX or
				   LC_VERSION_MIN_IPHONEOS or
				   LC_VERSION_MIN_WATCHOS or
				   LC_VERSION_MIN_TVOS */
		public uint32_t cmdsize;   /* sizeof(struct min_version_command) */
		public uint32_t version;   /* X.Y.Z is encoded in nibbles xxxx.yy.zz */
		public uint32_t sdk;       /* X.Y.Z is encoded in nibbles xxxx.yy.zz */
	};

	/*
 * The build_version_command contains the min OS version on which this 
 * binary was built to run for its platform.  The list of known platforms and
 * tool values following it.
 */
	[StructLayout (LayoutKind.Sequential)]
	public struct build_version_command
	{
		public LC cmd;       /* LC_BUILD_VERSION */
		public uint32_t cmdsize;   /* sizeof(struct build_version_command) plus */
		/* ntools * sizeof(struct build_tool_version) */
		public PLATFORM platform;  /* platform */
		public uint32_t minos;     /* X.Y.Z is encoded in nibbles xxxx.yy.zz */
		public uint32_t sdk;       /* X.Y.Z is encoded in nibbles xxxx.yy.zz */
		public uint32_t ntools;        /* number of tool entries following this */
	};



	/* Known values for the platform field above. */
	public enum PLATFORM
	{
		MACOS = 1,
		IOS = 2,
		TVOS = 3,
		WATCHOS = 4,
		BRIDGEOS = 5,
		MACCATALYST = 6,
		IOSSIMULATOR = 7,
		TVOSSIMULATOR = 8,
		WATCHOSSIMULATOR = 9,
		DRIVERKIT = 10,
	}
}
