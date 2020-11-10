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
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine ("Xamarin.iOS to Mac Catalyst Converter BETA");
			Console.ResetColor ();

			var projFile = args.FirstOrDefault (x => x.EndsWith ("proj", StringComparison.InvariantCultureIgnoreCase));
			if (string.IsNullOrEmpty (projFile)) {
				Console.ResetColor ();
				Console.WriteLine ($"Usage: maccat project-file");
				Console.WriteLine ($"");
				Console.WriteLine ($"Example: maccat /Users/me/Projects/MyApp/MyApp.csproj");
				return 1;
			}
			if (!File.Exists (projFile)) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ($"Could not locate {projFile}");
				return 2;
			}

			try {
				await (new BuildApp (projFile)).RunAsync ();
				return 0;

			}
			catch (Exception ex) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine (ex.Message);
				return 10;
			}
		}
	}
}
