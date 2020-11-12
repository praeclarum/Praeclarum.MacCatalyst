using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MacCatSdk
{
	class Program
	{
		static async Task<int> Main (string[] args)
		{
			var projFile = "";
			var wantsHelp = false;
			var config = "Release";
			var platform = "iPhone";
			var argc = args.Length;
			for (var i = 0; i < argc; i++) {
				var a = args[i];
				if (a.EndsWith ("proj", StringComparison.InvariantCultureIgnoreCase)) {
					projFile = a;
				}
				else if (a == "-h" || a == "--help") {
					wantsHelp = true;
				}
				else if (a == "-c" || a == "--configuration" && i + 1 < argc) {
					config = args[i + 1];
					i++;
				}
				else if (a == "-p" || a == "--platform" && i + 1 < argc) {
					platform = args[i + 1];
					i++;
				}
			}
			if (wantsHelp || string.IsNullOrEmpty (projFile)) {
				Console.ResetColor ();
				Console.WriteLine ($"Usage: maccat project-file");
				Console.WriteLine ($"");
				Console.WriteLine ($"Example: maccat /Users/me/Projects/MyApp/MyApp.csproj");
				Console.WriteLine ($"");
				Console.WriteLine ($"Options:");
				Console.WriteLine ($"  -c, --configuration <Release|Debug>");
				Console.WriteLine ($"  -p, --platform <iPhone|iPhoneSimulator>");
				return wantsHelp ? 0 : 1;
			}

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine ("Xamarin.iOS to Mac Catalyst Converter BETA");
			Console.ResetColor ();

			if (!File.Exists (projFile)) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ($"Could not locate {projFile}");
				return 2;
			}

			try {
				await (new BuildApp (projFile, config, platform)).RunAsync ();
				return 0;

			}
			catch (Exception ex) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine (ex.Message);
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine (ex);
				return 10;
			}
		}
	}
}
