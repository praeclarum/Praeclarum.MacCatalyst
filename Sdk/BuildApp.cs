using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using maccat;
using static maccat.Terminal;

namespace MacCatSdk
{
	public class BuildApp
	{
		public string MonoMacCatDirectory { get; set; } = Path.Combine (".", "mono-maccat");
		public string XamarinMacCatDirectory { get; set; } = Path.Combine (".", "xamarin-maccat");
		readonly string inputAppDir;
		readonly string outputAppDir;
		readonly string outputExecutablePath;
		readonly string projFile;
		readonly string projDir;
		readonly string binDir;
		readonly string cbinDir;
		readonly string objDir;
		readonly string cobjDir;
		readonly string mtouchDir;
		private readonly string configuration;
		private readonly string fromPlatform;
		readonly string executableAsmName;

		readonly string APPNAME;

		readonly string executableName;

		readonly Marzipanify marzipanify;

		public BuildApp (string projFile, string configuration, string platform)
		{
			this.projFile = Path.GetFullPath (projFile);
			this.projDir = Path.GetDirectoryName (this.projFile) ?? throw new Exception ("Bad project file path");
			this.binDir = Path.Combine (projDir, "bin");
			this.objDir = Path.Combine (projDir, "obj");

			this.configuration = configuration;
			this.fromPlatform = platform;

			var xbinDir = Path.Combine (binDir, fromPlatform, configuration);

			var appDirs = new List<(string Path, DateTime Time)> ();
			void FindAppDirs (string dir)
			{
				if (Path.GetExtension (dir).ToLowerInvariant () == ".app") {
					var times =
						(from f in Directory.GetFiles (dir, "*.exe")
						let t = new FileInfo(f).LastWriteTimeUtc
						orderby t descending
						select t).ToList ();
					if (times.Count > 0 && File.Exists (Path.Combine (dir, "Info.plist"))) {
						appDirs.Add ((dir, times[0]));
					}
				}
				else {
					foreach (var c in Directory.GetDirectories (dir)) {
						FindAppDirs (c);
					}
				}
			}
			FindAppDirs (xbinDir);
			inputAppDir = (from x in appDirs orderby x.Time descending select x.Path).FirstOrDefault ();
			if (string.IsNullOrEmpty (inputAppDir))
				throw new Exception ($"Failed to find built app. Please build your app with Configuration={configuration}, Platform={fromPlatform} before running this tool.");

			cbinDir = Path.GetDirectoryName (inputAppDir) ?? "";
			cobjDir = cbinDir.Replace (this.binDir, this.objDir);
			mtouchDir = Path.Combine (cobjDir, "mtouch-cache");

			APPNAME = Path.GetFileNameWithoutExtension (inputAppDir);

			executableName = APPNAME;
			executableAsmName = executableName + ".exe";

			outputAppDir = Path.Combine (binDir, "MacCatalyst", configuration, APPNAME + ".app");
			outputExecutablePath = $"{outputAppDir}/Contents/MacOS/{executableName}";
			Directory.CreateDirectory (outputAppDir);
			Directory.CreateDirectory (Path.GetDirectoryName (outputExecutablePath));

			marzipanify = new Marzipanify (outputAppDir);
		}

		public async Task RunAsync ()
		{
			await FindToolPathsAsync ();
			//await BuildProjectAsync ();
			await KillRunningApp ();
			await MarzipanifyExecutableAsync ();
			//if (!await CompileBinaryAsync ())
			//	return;
			Console.WriteLine ($"Building \"{APPNAME}.app\"...");
			CopyAssemblies ();
			await AddInfoPListAsync ();
			AddPkgInfo ();
			AddResources ();

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ($"Built {outputAppDir}");
		}

		async Task MarzipanifyExecutableAsync ()
		{
			var p = await MarzipanifyAsync (Path.Combine (inputAppDir, executableName));
			File.Copy (p, outputExecutablePath, overwrite: true);
			await ExecAsync ("chmod", $"+x \"{outputExecutablePath}\"");
		}

		async Task KillRunningApp ()
		{
			Console.WriteLine ($"Killing \"{executableName}\"...");
			await ExecAsync ("killall", executableName, showError: false, showOutput: false, throwOnError: false);
		}

		async Task BuildProjectAsync ()
		{
			Console.WriteLine ($"Building \"{projFile}\"...");
			await ExecAsync ("msbuild", $"/p:Configuration={configuration} /p:Platform={fromPlatform} \"{projFile}\"", showOutput: true);
		}

		async Task<bool> CompileBinaryAsync ()
		{
			
			string XAMMACCATDIR = Path.Combine (XamarinMacCatDirectory, "Xamarin.macOSCatalyst.sdk");
			string MONOMACCATDIR = MonoMacCatDirectory;
			string MACSDK = await ExecAsync ("xcrun", "--show-sdk-path", showOutput: false);
			// COMPUTED VARIABLED
			string CFLAGS = "-Wno-unguarded-availability-new -std=c++14 -ObjC";
			string CFLAGS2 = "-lz -liconv -lc++ -x objective-c++ -stdlib=libc++";
			string CFLAGS3 = $"-fno-caret-diagnostics -fno-diagnostics-fixit-info -isysroot {MACSDK}";
			string DEFINES = "-D_THREAD_SAFE";
			// FRAMEWORKS="-framework AppKit -framework Foundation -framework Security -framework Carbon -framework GSS";
			string FRAMEWORKS = $"-iframework {MACSDK}/System/iOSSupport/System/Library/Frameworks -framework Foundation -framework UIKit -framework GSS";
			string XAMMACLIB = $"{XAMMACCATDIR}/lib/libxammaccat.a";
			//string US = "-u _xamarin_IntPtr_objc_msgSend_IntPtr -u _SystemNative_ConvertErrorPlatformToPal -u _SystemNative_ConvertErrorPalToPlatform -u _SystemNative_StrErrorR -u _SystemNative_GetNonCryptographicallySecureRandomBytes -u _SystemNative_Stat2 -u _SystemNative_LStat2 -u _xamarin_timezone_get_local_name -u _xamarin_timezone_get_data -u _xamarin_find_protocol_wrapper_type -u _xamarin_get_block_descriptor";
			string US = String.Join (" ", GetNativeEntryPoints ().Select (x => $"-u _{x}"));
			
			string INCLUDES = $"\"-I{MONOMACCATDIR}/include/mono-2.0\" \"-I{XAMMACCATDIR}/include\"";
			string LINKS = $"\"{MONOMACCATDIR}/lib/libmonosgen-2.0.a\" \"{MONOMACCATDIR}/lib/libmono-native.a\" ";
			//LINKS += string.Join (" ", GetArchivedLibraries ().Select (x => $"\"{x}\""));

			string COMPILES = $"-DAPP_EXECUTABLE_NAME=\\\"{executableAsmName}\\\" catmain.m";

			var clangArgs = $"{CFLAGS} {FRAMEWORKS} {US} {XAMMACLIB} -o {outputExecutablePath} {DEFINES} {INCLUDES} {LINKS} {CFLAGS2} {COMPILES} {CFLAGS3}";
			//System.Console.WriteLine(CLANG);
			//System.Console.WriteLine(clangArgs);
			Console.WriteLine ($"Compiling native \"{executableName}\"...");
			var r = await ClangAsync (clangArgs);
			//System.Console.WriteLine(r);
			return true;
		}

		static readonly HashSet<string> ignoreEntryPoints = new HashSet<string> {
			"mono_profiler_init_log",
		};

		string[] GetNativeEntryPoints ()
		{
			var path = Path.Combine (mtouchDir, "entry-points.txt");
			var lines = File.ReadAllLines (path);
			return (from l in lines
					let s = l.Split ('=')
					where s.Length == 2 && s[0] == "Function"
					let e = s[1]
					where !ignoreEntryPoints.Contains (e)
					select s[1]).ToArray ();
		}

		string[] GetArchivedLibraries ()
		{
			var inArchives = (from a in Directory.GetFiles (mtouchDir, "*.a")
					select a).ToArray ();

			var archives = inArchives.Select (MarzipanifyArchivedLibrary).ToArray ();

			return archives;
		}

		async Task<string> MarzipanifyAsync (string inPath)
		{
			Console.WriteLine ($"Marzipanifying \"{Path.GetFileName (inPath)}\"...");
			return await marzipanify.ModifyMachHeaderAndReturnNSArrayOfLoadedDylibs (inPath, dryRun: false);
		}

		string MarzipanifyArchivedLibrary (string inArchivePath)
		{
			Console.WriteLine ($"Marzipanifying archive \"{Path.GetFileName (inArchivePath)}\"...");

			var outArchivePath = inArchivePath;
			//new Marzipanify ().ModifyMachHeaderAndReturnNSArrayOfLoadedDylibs (inArchivePath);
			return outArchivePath;
		}

		void CopyAssemblies ()
		{
			string ASSEMBLIES_DIR = $"{cobjDir}/mtouch-cache/1-Link";
			string LINKED_ASSEMBLIES_DIR = $"{cobjDir}/mtouch-cache/3-Build";
			var asmsOutDir = Path.Combine (outputAppDir, "Contents", "MonoBundle");
			Directory.CreateDirectory (asmsOutDir);
			void CopyAsm (string a) => File.Copy (a, Path.Combine (asmsOutDir, Path.GetFileName (a)), overwrite: true);
			foreach (var a in Directory.GetFiles (ASSEMBLIES_DIR, "*.dll")) {
				CopyAsm (a);
			}
			foreach (var a in Directory.GetFiles (ASSEMBLIES_DIR, "*.exe")) {
				CopyAsm (a);
			}
			CopyAsm (Path.Combine (XamarinMacCatDirectory, "Xamarin.iOS.dll"));
		}

		

		async Task AddInfoPListAsync ()
		{
			var src = Path.Combine (inputAppDir, "Info.plist");
			var dest = Path.Combine (outputAppDir, "Contents", "Info.plist");
			File.Copy (src, dest, overwrite: true);
			var macOSVersion = "10.15.0";
			await PlistAsync ($"Set :DTPlatformName macosx");
			await PlistAsync ($"Set :DTPlatformVersion {macOSVersion}", $"Add :DTPlatformVersion string {macOSVersion}");
			await PlistAsync ($"Set :DTSDKName macosx10.15");
			await PlistAsync ($"Set :CFBundleSupportedPlatforms:0 MacOSX");
			await PlistAsync ($"Add :LSMinimumSystemVersion string 10.15.0");
			await PlistAsync ($"Delete :MinimumOSVersion");
			await PlistAsync ($"Delete :LSRequiresIPhoneOS");
		}

		async Task PlistAsync (string command, bool showError = true)
		{
			var dest = Path.Combine (outputAppDir, "Contents", "Info.plist");
			await ExecAsync ("/usr/libexec/PlistBuddy", $"-c \"{command}\" \"{dest}\"", showError: showError);
		}

		async Task PlistAsync (string command, string alt)
		{
			try {
				await PlistAsync (command, showError: false);
			}
			catch {
				await PlistAsync (alt);
			}
		}

		void AddPkgInfo ()
		{
			var src = Path.Combine (inputAppDir, "PkgInfo");
			var dest = Path.Combine (outputAppDir, "Contents", "PkgInfo");
			File.Copy (src, dest, overwrite: true);
		}

		/// <summary>
		/// https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
		/// </summary>
		static void CopyResourcesRecur (string sourceDirName, string destDirName)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo (sourceDirName);

			if (!dir.Exists) {
				throw new DirectoryNotFoundException (
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories ();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory (destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles ();
			var srcFiles =
				from f in files
				let e = f.Extension.ToLowerInvariant ()
				where e != ".arm64"
				where e != ".dll"
				where e != ".exe"
				where e != ".mobileprovision"
				select f;
			foreach (FileInfo file in srcFiles) {
				string tempPath = Path.Combine (destDirName, file.Name);
				file.CopyTo (tempPath, overwrite: true);
			}

			// If copying subdirectories, copy them and their contents to new location.
			var srcDirs =
				from f in dirs
				where f.Name != "_CodeSignature"
				select f;
			foreach (DirectoryInfo subdir in srcDirs) {
				string tempPath = Path.Combine (destDirName, subdir.Name);
				CopyResourcesRecur (subdir.FullName, tempPath);
			}
		}

		void AddResources ()
		{
			CopyResourcesRecur (inputAppDir, Path.Combine (outputAppDir, "Contents", "Resources"));
		}
	}
}
