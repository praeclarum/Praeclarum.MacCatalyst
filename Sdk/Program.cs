using System;
using System.IO;
using System.Threading.Tasks;

namespace MacCatSdk
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var projFile = args.Length > 0 ? args[0] : "";
            if (string.IsNullOrEmpty (projFile) || !File.Exists (projFile)) {
                Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ($"Usage: maccat project-directory");
                return 1;
            }

			try {
                await (new BuildApp (projFile)).RunAsync ();
                return 0;

			}
			catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine (ex.Message);
                return 2;
			}
        }
    }
}
