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
		public readonly string MonoMacCatDirectory;
		public readonly string XamarinMacCatDirectory;
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
		private readonly bool run;
		readonly string executableAsmName;

		readonly string APPNAME;

		readonly string executableName;

		readonly Marzipanify marzipanify;

		readonly bool isDebug;

		readonly string sdkPath;

		public string CodesignEntitlements { get; set; } = "";
		public string CodesignProvision { get; set; } = "";
		public string CodesignKey { get; set; } = "";

		const string GeneratedOutputDirectoryName = "Praeclarum.MacCatalyst";

		public BuildApp (string projFile, string configuration, string platform, bool run, string sdkPath, string assemblyNameHint = "", string outputPathHint = "")
		{
			this.sdkPath = sdkPath;
			MonoMacCatDirectory = Path.Combine (sdkPath, "mono-maccat");
			XamarinMacCatDirectory = Path.Combine (sdkPath, "xamarin-maccat");
			this.projFile = Path.GetFullPath (projFile);
			this.projDir = Path.GetDirectoryName (this.projFile) ?? throw new Exception ("Bad project file path");
			this.binDir = Path.Combine (projDir, "bin");
			this.objDir = Path.Combine (projDir, "obj");

			this.configuration = configuration;
			this.fromPlatform = platform;
			this.run = run;

			var xbinDir = Path.Combine (binDir, fromPlatform, configuration);
			if (!string.IsNullOrEmpty (outputPathHint)) {
				var op = outputPathHint.Replace ('\\', Path.DirectorySeparatorChar);
				xbinDir = Path.Combine (projDir, op);
			}
			if (!Directory.Exists (xbinDir)) {
				throw new Exception ($"Failed to find built app. Please build your app with Configuration={configuration}, Platform={fromPlatform} before running this tool.");
			}

			var appDirs = new List<(string Path, DateTime Time)> ();
			void FindAppDirs (string dir)
			{
				if (Path.GetFileName (dir) == GeneratedOutputDirectoryName) {
					// Ignore our own output
					return;
				}
				if (Path.GetExtension (dir).ToLowerInvariant () == ".app") {
					var times =
						(from f in Directory.GetFiles (dir, "*.exe")
						 let t = new FileInfo (f).LastWriteTimeUtc
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

			outputAppDir = Path.Combine (cbinDir, GeneratedOutputDirectoryName, APPNAME + ".app");
			outputExecutablePath = $"{outputAppDir}/Contents/MacOS/{executableName}";

			marzipanify = new Marzipanify (outputAppDir);

			isDebug = configuration != "Release";
		}

		public async Task RunAsync ()
		{
			Info ($"Using SDK \"{sdkPath}\".");
			Info ($"Converting \"{inputAppDir}\".");
			await FindToolPathsAsync ();
			//await BuildProjectAsync ();
			await KillRunningApp ();

			DeleteExistingApp ();
			Directory.CreateDirectory (outputAppDir);
			Directory.CreateDirectory (Path.GetDirectoryName (outputExecutablePath));

			//await MarzipanifyExecutableAsync ();
			if (!await CompileBinaryAsync ())
				return;
			Info ($"Building \"{APPNAME}.app\".");
			CopyAssemblies ();
			await AddInfoPListAsync ();
			AddPkgInfo ();
			AddResources ();

			if (!string.IsNullOrEmpty (CodesignEntitlements)) {
				//await SignAppAsync ();
			}

			Info ($"Built {outputAppDir}");

			if (run) {
				Info ($"Running \"{executableName}\".");
				await ExecAsync ("open", $"\"{outputAppDir}\"", waitForExit: false);
			}
		}

		void DeleteExistingApp ()
		{
			if (Directory.Exists (outputAppDir)) {
				var dirs = new[] {
					Path.Combine(outputAppDir, "Contents"),
				};
				foreach (var d in dirs) {
					if (Directory.Exists (d)) {
						Directory.Delete (d, recursive: true);
					}
				}
			}
		}

		async Task MarzipanifyExecutableAsync ()
		{
			var p = await MarzipanifyAsync (Path.Combine (inputAppDir, executableName));
			File.Copy (p, outputExecutablePath, overwrite: true);
			await ExecAsync ("chmod", $"+x \"{outputExecutablePath}\"");
		}

		async Task KillRunningApp ()
		{
			Info ($"Killing \"{executableName}\".");
			await ExecAsync ("killall", $"\"{executableName}\"", showError: false, showOutput: false, throwOnError: false);
		}

		async Task BuildProjectAsync ()
		{
			Info ($"Building \"{projFile}\".");
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
			string FRAMEWORKS = $"-iframework {MACSDK}/System/iOSSupport/System/Library/Frameworks -framework Foundation -framework Security -framework UIKit -framework GSS";
			string XAMMACLIB = $"{XAMMACCATDIR}/lib/libxammaccat.a";
			if (isDebug) {
				XAMMACLIB = $"{XAMMACCATDIR}/lib/libxammaccat-debug.a";
			}
			string US = String.Join (" ", GetNativeEntryPoints ().Select (x => $"-u _{x}"));

			string INCLUDES = $"\"-I{MONOMACCATDIR}/include/mono-2.0\" \"-I{XAMMACCATDIR}/include\"";
			string LINKS = $"\"{XAMMACCATDIR}/lib/libmonosgen-2.0.a\" \"{XAMMACCATDIR}/lib/libmono-native.a\" ";
			LINKS += string.Join (" ", (await GetArchivedLibrariesAsync ()).Where (x => !string.IsNullOrEmpty (x) && File.Exists (x)).Select (x => $"\"{x}\""));

			string COMPILES = $"\"-DAPP_EXECUTABLE_NAME=\\\"{executableAsmName}\\\"\" catmain.m";
			var emainPath = Path.Combine (mtouchDir, "x86_64", "main.m");
			if (File.Exists (emainPath)) {
				//Console.WriteLine ("USE MAIN " + emainPath);
				COMPILES = $"\"-Dxamarin_gc_pump=int __xamarin_gc_pump\" \"{emainPath}\"";
			}
			else {
				Warning ("Startup performance can be increased by using a simulator build.");
			}
			var shimsPath = Path.Combine (Path.GetTempPath (), "maccat-shims.m");
			File.WriteAllText (shimsPath, @"
#include ""xamarin/xamarin.h""
extern ""C"" { void xamarin_create_classes_Xamarin_iOS() {} }
extern ""C"" { void mono_profiler_init_log () {} }
typedef void (*xamarin_profiler_symbol_def)();
extern xamarin_profiler_symbol_def xamarin_profiler_symbol;
//xamarin_profiler_symbol_def xamarin_profiler_symbol = NULL;

");
			COMPILES += $" \"{shimsPath}\"";

			var clangArgs = $"{CFLAGS} {FRAMEWORKS} {US} {XAMMACLIB} -o \"{outputExecutablePath}\" {DEFINES} {INCLUDES} {LINKS} {CFLAGS2} {COMPILES} {CFLAGS3}";
			//System.Console.WriteLine(CLANG);
			//System.Console.WriteLine (clangArgs);
			Info ($"Compiling native \"{executableName}\" (debug={isDebug}).");
			var r = await ClangAsync (clangArgs);
			//System.Console.WriteLine(r);
			return true;
		}

		static readonly HashSet<string> ignoreEntryPoints = new HashSet<string> {
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

		Task<string[]> GetArchivedLibrariesAsync ()
		{
			var inArchives = (from a in Directory.GetFiles (mtouchDir, "*.a")
							  select a).ToArray ();
			return Task.WhenAll (inArchives.Select (MarzipanifyArchivedLibrary));
		}

		async Task<string> MarzipanifyAsync (string inPath)
		{
			Info ($"Marzipanifying \"{Path.GetFileName (inPath)}\".");
			return await marzipanify.ModifyMachHeaderAsync (inPath, dryRun: false);
		}

		async Task<string> MarzipanifyArchivedLibrary (string inPath)
		{
			Info ($"Marzipanifying \"{Path.GetFileName (inPath)}\".");
			return await marzipanify.ModifyMachHeaderAsync (inPath, dryRun: false);
		}

		void CopyAssemblies ()
		{
			string ASSEMBLIES_DIR = $"{cobjDir}/mtouch-cache/1-Link";
			string LINKED_ASSEMBLIES_DIR = $"{cobjDir}/mtouch-cache/3-Build";
			var asmsOutDir = Path.Combine (outputAppDir, "Contents", "MonoBundle");
			Directory.CreateDirectory (asmsOutDir);
			void CopyAsm (string a) => File.Copy (a, Path.Combine (asmsOutDir, Path.GetFileName (a)), overwrite: true);
			foreach (var a in Directory.GetFiles (LINKED_ASSEMBLIES_DIR, "*.dll")) {
				CopyAsm (a);
			}
			foreach (var a in Directory.GetFiles (LINKED_ASSEMBLIES_DIR, "*.exe")) {
				CopyAsm (a);
			}
			if (isDebug) {
				foreach (var a in Directory.GetFiles (LINKED_ASSEMBLIES_DIR, "*.pdb")) {
					CopyAsm (a);
				}
			}
			CopyAsm (Path.Combine (ASSEMBLIES_DIR, "mscorlib.dll"));
			CopyAsm (Path.Combine (ASSEMBLIES_DIR, "System.dll"));
			CopyAsm (Path.Combine (ASSEMBLIES_DIR, "System.Core.dll"));
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
			if (!File.Exists (src)) {
				//Console.WriteLine ("SYNTH PKGINFO");
				src = Path.Combine (Path.GetTempPath (), "MacCatPkgInfo");
				if (File.Exists (src))
					File.Delete (src);
				File.WriteAllBytes (src, new byte[] { 0x41, 0x50, 0x50, 0x4C, 0x3F, 0x3F, 0x3F, 0x3F });
			}
			var dest = Path.Combine (outputAppDir, "Contents", "PkgInfo");
			File.Copy (src, dest, overwrite: true);
		}

		/// <summary>
		/// https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
		/// </summary>
		void CopyResourcesRecur (string sourceDirName, string destDirName, int depth)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo (sourceDirName);

			if (!dir.Exists) {
				throw new DirectoryNotFoundException (
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] subdirs = dir.GetDirectories ();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory (destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles ();
			var srcFiles =
				from f in files
				let n = f.Name
				let e = f.Extension.ToLowerInvariant ()
				where e != ".arm64"
				where e != ".dll" || n.Contains (".resources.", StringComparison.InvariantCulture)
				where e != ".pdb"
				where e != ".mdb"
				where e != ".exe"
				where e != ".mobileprovision"
				where n != executableName
				where !(depth == 0 && n == "PkgInfo")
				where !(depth == 0 && n == "Info.plist")
				select f;
			foreach (var file in srcFiles) {
				//Console.WriteLine ($"DEPTH={depth} FILE={file}");
				string destPath = Path.Combine (destDirName, file.Name);
				if (depth == 0 && file.Extension == ".config") {
					destPath = Path.Combine (outputAppDir, "Contents", "MonoBundle", file.Name);
				}
				file.CopyTo (destPath, overwrite: true);
			}

			// If copying subdirectories, copy them and their contents to new location.
			foreach (var subdir in subdirs) {
				var destPath = Path.Combine (destDirName, subdir.Name);
				if (depth == 0 && Directory.GetFiles (subdir.FullName, "*.resources.dll").Length > 0) {
					destPath = Path.Combine (outputAppDir, "Contents", "MonoBundle", subdir.Name);
				}
				else if (depth == 0 && subdir.Name == "Frameworks") {
					destPath = Path.Combine (outputAppDir, "Contents", subdir.Name);
				}
				else if (depth == 0 && subdir.Name == "_CodeSignature") {
					destPath = Path.Combine (outputAppDir, "Contents", subdir.Name);
				}
				CopyResourcesRecur (subdir.FullName, destPath, depth + 1);
			}
		}

		void AddResources ()
		{
			CopyResourcesRecur (inputAppDir, Path.Combine (outputAppDir, "Contents", "Resources"), 0);
		}

		async Task SignAppAsync ()
		{
			var codesign = "/usr/bin/codesign";
			var entitlementsPath = Path.Combine (cobjDir, "Entitlements.xcent");
			if (File.Exists (entitlementsPath)) {
				Info ($"Signing \"{APPNAME}.app\" with entitlements \"{entitlementsPath}\".");
				var args = $"-v --force --timestamp=none --sign - --entitlements \"{entitlementsPath}\"  \"{outputAppDir}\"";
				await ExecAsync (codesign, args);
			}
			else {
				Warning ($"No signing entitlements found in \"{inputAppDir}\".");
			}
		}
	}
}
