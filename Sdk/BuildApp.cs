using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  MacCatSdk
{
    public class BuildApp
    {
        public string MonoMacCatDirectory { get; set; } = Path.Combine (".", "mono-maccat");
        public string XamarinMacCatDirectory { get; set; } = Path.Combine (".", "xamarin-maccat");
        readonly string inputAppDir;
        readonly string outputAppDir;
        readonly string projDir;
		readonly string binDir;
		readonly string objDir;

        readonly string APPNAME;

        public BuildApp (string projDir)
		{
			this.projDir = projDir;
            this.binDir = Path.Combine (projDir, "bin");
            this.objDir = Path.Combine (projDir, "obj");

            inputAppDir = Directory.GetDirectories (Path.Combine (binDir, "iPhone", "Release"), "*.app").FirstOrDefault () ?? "";

            APPNAME = Path.GetFileNameWithoutExtension (inputAppDir);

            outputAppDir = Path.Combine (binDir, "MacCat", "Release", APPNAME + ".app");
            Directory.CreateDirectory (outputAppDir);
        }

        public async Task RunAsync ()
        {
            await CompileBinaryAsync ();
            CopyAssemblies ();
            Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ($"Built {outputAppDir}");
        }

        async Task CompileBinaryAsync ()
        {
            //
            // ENVIRONMENT VARIABLES
            string CLANG=await ExecAsync("xcrun", "-f clang");
            string XAMMACCATDIR=Path.Combine(XamarinMacCatDirectory, "Xamarin.macOSCatalyst.sdk");
            string MONOMACCATDIR=MonoMacCatDirectory;
            string MACSDK=await ExecAsync("xcrun", "--show-sdk-path");
            // COMPUTED VARIABLED
            string CFLAGS="-target x86_64-apple-ios13.0-macabi -Wno-unguarded-availability-new -std=c++14 -ObjC";
            string CFLAGS2="-lz -liconv -lc++ -x objective-c++ -stdlib=libc++";
            string CFLAGS3=$"-fno-caret-diagnostics -fno-diagnostics-fixit-info -isysroot {MACSDK}";
            string DEFINES="-D_THREAD_SAFE";
            // FRAMEWORKS="-framework AppKit -framework Foundation -framework Security -framework Carbon -framework GSS";
            string FRAMEWORKS=$"-iframework {MACSDK}/System/iOSSupport/System/Library/Frameworks -framework Foundation -framework UIKit";
            string XAMMACLIB=$"{XAMMACCATDIR}/lib/libxammaccat.a";
            string US="-u _SystemNative_ConvertErrorPlatformToPal -u _SystemNative_ConvertErrorPalToPlatform -u _SystemNative_StrErrorR -u _SystemNative_GetNonCryptographicallySecureRandomBytes -u _SystemNative_Stat2 -u _SystemNative_LStat2 -u _xamarin_timezone_get_local_name -u _xamarin_timezone_get_data -u _xamarin_find_protocol_wrapper_type -u _xamarin_get_block_descriptor";
            string OUT=$"{outputAppDir}/Contents/MacOS/{APPNAME}";
            Directory.CreateDirectory(Path.GetDirectoryName (OUT));
            string INCLUDES=$"-I{MONOMACCATDIR}/include/mono-2.0 -I{XAMMACCATDIR}/include";
            string LINKS=$"{MONOMACCATDIR}/lib/libmonosgen-2.0.a {MONOMACCATDIR}/lib/libmono-native.a";
            string COMPILES="catmain.m";

            var clangArgs = $"{CFLAGS} {FRAMEWORKS} {US} {XAMMACLIB} -o {OUT} {DEFINES} {INCLUDES} {LINKS} {CFLAGS2} {COMPILES} {CFLAGS3}";
            //System.Console.WriteLine(CLANG);
            //System.Console.WriteLine(clangArgs);
            var r = await ExecAsync (CLANG, clangArgs);
            //System.Console.WriteLine(r);
        }

        void CopyAssemblies ()
        {
            string ASSEMBLIES_DIR=$"{objDir}/iPhone/Release/mtouch-cache/1-Link";
            string LINKED_ASSEMBLIES_DIR=$"{objDir}/iPhone/Release/mtouch-cache/3-Build";
            var asmsOutDir = Path.Combine(outputAppDir, "Contents", "MonoBundle");
            Directory.CreateDirectory(asmsOutDir);
            void CopyAsm(string a) => File.Copy(a, Path.Combine(asmsOutDir, Path.GetFileName(a)), overwrite: true);
            foreach (var a in Directory.GetFiles(ASSEMBLIES_DIR, "*.dll")) {
                CopyAsm (a);
            }
            foreach (var a in Directory.GetFiles(ASSEMBLIES_DIR, "*.exe")) {
                CopyAsm (a);
            }
            CopyAsm(Path.Combine(XamarinMacCatDirectory, "Xamarin.iOS.dll"));
        }

        async Task<string> ExecAsync(string fileName, string arguments)
        {
            var si = new System.Diagnostics.ProcessStartInfo (fileName, arguments);
            si.RedirectStandardOutput = true;
            var p = System.Diagnostics.Process.Start(si);
            string? line;
            var sb = new StringBuilder ();
            while ((line = await p.StandardOutput.ReadLineAsync()) != null) {
                sb.Append (line);
            }
            p.WaitForExit();
            return sb.ToString ();
        }
    }
}
