using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacCatSdk
{
	public class BuildApp
	{
		public string MonoMacCatDirectory { get; set; } = Path.Combine (".", "mono-maccat");
		public string XamarinMacCatDirectory { get; set; } = Path.Combine (".", "xamarin-maccat");
		readonly string inputAppDir;
		readonly string outputAppDir;
		readonly string projFile;
		readonly string projDir;
		readonly string binDir;
		readonly string cbinDir;
		readonly string objDir;
		readonly string cobjDir;
		private readonly string configuration;
		private readonly string fromPlatform;
		readonly string executableAsmName;

		readonly string APPNAME;

		public BuildApp (string projFile, string configuration, string platform)
		{
			this.projFile = Path.GetFullPath (projFile);
			this.projDir = Path.GetDirectoryName (this.projFile) ?? throw new Exception ("Bad project file path");
			this.binDir = Path.Combine (projDir, "bin");
			this.objDir = Path.Combine (projDir, "obj");

			this.configuration = configuration;
			this.fromPlatform = platform;

			cbinDir = Path.Combine (binDir, fromPlatform, configuration);
			cobjDir = Path.Combine (objDir, fromPlatform, configuration);
			if (Directory.Exists (Path.Combine (cbinDir, "device-builds"))) {
				var dbins =
					from d in Directory.GetDirectories (Path.Combine (cbinDir, "device-builds"))
					let ads = Directory.GetDirectories (d, "*.app")
					where ads.Length > 0
					let ad = ads[0]
					let exes = Directory.GetFiles (ad, "*.exe")
					where exes.Length > 0
					let modTime = exes.Max (x => new FileInfo (x).LastWriteTimeUtc)
					orderby modTime descending
					select d;
				var dbinDir = dbins.FirstOrDefault ();
				if (!string.IsNullOrEmpty (dbinDir)) {
					cbinDir = dbinDir;
					cobjDir = Path.Combine (cobjDir, "device-builds", Path.GetFileName (dbinDir));
				}
			}
			inputAppDir = Directory.GetDirectories (cbinDir, "*.app").FirstOrDefault () ?? "";
			if (string.IsNullOrEmpty (inputAppDir))
				throw new Exception ($"Failed to find built app. Please build your app with Configuration={configuration}, Platform={fromPlatform} before running this tool.");

			APPNAME = Path.GetFileNameWithoutExtension (inputAppDir);

			executableAsmName = APPNAME + ".exe";

			outputAppDir = Path.Combine (binDir, "MacCatalyst", configuration, APPNAME + ".app");
			Directory.CreateDirectory (outputAppDir);
		}

		public async Task RunAsync ()
		{
			//await BuildProjectAsync ();
			await CompileBinaryAsync ();
			Console.WriteLine ($"Building app...");
			CopyAssemblies ();
			await AddInfoPListAsync ();
			AddPkgInfo ();
			AddResources ();

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ($"Built {outputAppDir}");
		}

		async Task BuildProjectAsync ()
		{
			Console.WriteLine ($"Building \"{projFile}\"...");
			await ExecAsync ("msbuild", $"/p:Configuration={configuration} /p:Platform={fromPlatform} \"{projFile}\"", showOutput: true);
		}

		async Task CompileBinaryAsync ()
		{
			Console.WriteLine ($"Compiling binary...");

			//
			// ENVIRONMENT VARIABLES
			string CLANG = await ExecAsync ("xcrun", "-f clang", showOutput: false);
			string XAMMACCATDIR = Path.Combine (XamarinMacCatDirectory, "Xamarin.macOSCatalyst.sdk");
			string MONOMACCATDIR = MonoMacCatDirectory;
			string MACSDK = await ExecAsync ("xcrun", "--show-sdk-path", showOutput: false);
			// COMPUTED VARIABLED
			string CFLAGS = "-target x86_64-apple-ios13.0-macabi -Wno-unguarded-availability-new -std=c++14 -ObjC";
			string CFLAGS2 = "-lz -liconv -lc++ -x objective-c++ -stdlib=libc++";
			string CFLAGS3 = $"-fno-caret-diagnostics -fno-diagnostics-fixit-info -isysroot {MACSDK}";
			string DEFINES = "-D_THREAD_SAFE";
			// FRAMEWORKS="-framework AppKit -framework Foundation -framework Security -framework Carbon -framework GSS";
			string FRAMEWORKS = $"-iframework {MACSDK}/System/iOSSupport/System/Library/Frameworks -framework Foundation -framework UIKit -framework GSS";
			string XAMMACLIB = $"{XAMMACCATDIR}/lib/libxammaccat.a";
			//string US = "-u _xamarin_IntPtr_objc_msgSend_IntPtr -u _SystemNative_ConvertErrorPlatformToPal -u _SystemNative_ConvertErrorPalToPlatform -u _SystemNative_StrErrorR -u _SystemNative_GetNonCryptographicallySecureRandomBytes -u _SystemNative_Stat2 -u _SystemNative_LStat2 -u _xamarin_timezone_get_local_name -u _xamarin_timezone_get_data -u _xamarin_find_protocol_wrapper_type -u _xamarin_get_block_descriptor";
			string US = String.Join (" ", GetNativeEntryPoints ().Select (x => $"-u _{x}"));
			string OUT = $"{outputAppDir}/Contents/MacOS/{APPNAME}";
			Directory.CreateDirectory (Path.GetDirectoryName (OUT));
			string INCLUDES = $"-I{MONOMACCATDIR}/include/mono-2.0 -I{XAMMACCATDIR}/include";
			string LINKS = $"{MONOMACCATDIR}/lib/libmonosgen-2.0.a {MONOMACCATDIR}/lib/libmono-native.a";

			string COMPILES = $"-DAPP_EXECUTABLE_NAME=\\\"{executableAsmName}\\\" catmain.m";

			var clangArgs = $"{CFLAGS} {FRAMEWORKS} {US} {XAMMACLIB} -o {OUT} {DEFINES} {INCLUDES} {LINKS} {CFLAGS2} {COMPILES} {CFLAGS3}";
			//System.Console.WriteLine(CLANG);
			//System.Console.WriteLine(clangArgs);
			var r = await ExecAsync (CLANG, clangArgs);
			//System.Console.WriteLine(r);
		}

		static readonly HashSet<string> ignoreEntryPoints = new HashSet<string> {
			"mono_profiler_init_log",
		};

		string[] GetNativeEntryPoints ()
		{
			var path = Path.Combine (cobjDir, "mtouch-cache", "entry-points.txt");
			var lines = File.ReadAllLines (path);
			return (from l in lines
					let s = l.Split ('=')
					where s.Length == 2 && s[0] == "Function"
					let e = s[1]
					where !ignoreEntryPoints.Contains (e)
					select s[1]).ToArray ();
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

		async Task<string> ExecAsync (string fileName, string arguments, bool showOutput = true, bool showError = true)
		{
			var si = new System.Diagnostics.ProcessStartInfo (fileName, arguments);
			si.RedirectStandardOutput = true;
			si.RedirectStandardError = true;
			var p = System.Diagnostics.Process.Start (si);
			string? line;
			var sb = new StringBuilder ();
			var sbe = new StringBuilder ();
			var readOutTask = Task.Run (async () => {
				while ((line = await p.StandardOutput.ReadLineAsync ()) != null) {
					sb.Append (line);
					if (showOutput) {
						Console.ForegroundColor = line.Contains ("error", StringComparison.InvariantCultureIgnoreCase) ? ConsoleColor.Red : ConsoleColor.DarkGray;
						Console.WriteLine (line);
					}
				}
			});
			var readErrorTask = Task.Run (async () => {
				while ((line = await p.StandardError.ReadLineAsync ()) != null) {
					sbe.Append (line);
					if (showError) {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine (line);
					}
				}
			});
			p.WaitForExit ();
			await readOutTask;
			await readErrorTask;
			if (p.ExitCode != 0)
				throw new Exception ("Failed to execute " + fileName);
			if (showOutput)
				Console.ResetColor ();
			return sb.ToString ();
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
