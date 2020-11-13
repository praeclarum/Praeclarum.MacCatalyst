using System;
using System.Text;
using System.Threading.Tasks;

namespace maccat
{
	public static class Terminal
	{
		static string CLANG = "clang";

		public static async Task FindToolPathsAsync ()
		{
			//
			// ENVIRONMENT VARIABLES
			//
			try {
				CLANG = await ExecAsync ("xcrun", "-f clang", showOutput: false, showError: false);
			}
			catch {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine ($"Couldn't run Xcode tools. Please run:");
				Console.ResetColor ();
				Console.WriteLine ("sudo xcode-select --switch /Applications/Xcode.app");
			}
		}
		public static Task<string> ClangAsync (string arguments, bool throwOnError = true, bool showOutput = true, bool showError = true)
		{
			return ExecAsync (CLANG, "-target x86_64-apple-ios13.0-macabi " + arguments,
				throwOnError: throwOnError, showOutput: showOutput, showError: showError);
		}

		public static async Task<string> ExecAsync (string fileName, string arguments, bool throwOnError = true, bool showOutput = true, bool showError = true)
		{
			//Console.WriteLine ("{0} {1}", fileName, arguments);
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
			if (throwOnError && p.ExitCode != 0)
				throw new Exception ($"{fileName} failed with code: {p.ExitCode}");
			if (showOutput)
				Console.ResetColor ();
			return sb.ToString ();
		}
	}
}
