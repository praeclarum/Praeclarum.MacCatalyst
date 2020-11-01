using System;
using System.IO;
using System.Threading.Tasks;

namespace MacCatSdk
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var projDir = args.Length > 0 ? args[0] : "";
            if (string.IsNullOrEmpty (projDir) || !Directory.Exists (projDir)) {
                Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ($"Usage: maccat project-directory");
                return 1;
            }


            await (new BuildApp (projDir)).RunAsync ();
            return 0;
        }
    }
}
