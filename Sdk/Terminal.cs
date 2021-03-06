﻿using System;
using System.Text;
using System.Threading.Tasks;

namespace maccat
{
	public static class Terminal
	{
		static string CLANG = "clang";
		static string AR = "ar";

		public static Action<string>? InfoFunc;
		public static Action<string>? WarningFunc;
		public static Action<string>? ErrorFunc;

		public static void Info (string message)
		{
			if (InfoFunc != null) {
				InfoFunc (message);
			}
			else {
				Console.ResetColor ();
				Console.WriteLine (message);
			}
		}

		public static void Warning (string message)
		{
			if (WarningFunc != null) {
				WarningFunc (message);
			}
			else {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine (message);
				Console.ResetColor ();
			}
		}

		public static async Task FindToolPathsAsync ()
		{
			//
			// ENVIRONMENT VARIABLES
			//
			try {
				CLANG = await ExecAsync ("xcrun", "-f clang", showOutput: false, showError: false);
				AR = await ExecAsync ("xcrun", "-f ar", showOutput: false, showError: false);
			}
			catch {
				throw new Exception ($"Couldn't find Xcode tools. Please run: sudo xcode-select --switch /Applications/Xcode.app");
			}
		}

		public static Task<string> ArAsync (string arguments, bool throwOnError = true, bool showOutput = true, bool showError = false, string? cd = null) =>
			ExecAsync (AR, arguments,
				       throwOnError: throwOnError, showOutput: showOutput, showError: showError, cd: cd);

		public static Task<string> ClangAsync (string arguments, bool throwOnError = true, bool showOutput = true, bool showError = true, string? cd = null) =>
			ExecAsync (CLANG, "-target x86_64-apple-ios13.0-macabi " + arguments,
					   throwOnError: throwOnError, showOutput: showOutput, showError: showError, cd: cd);

		public static async Task<string> ExecAsync (string fileName, string arguments, bool throwOnError = true, bool showOutput = true, bool showError = true, string? cd = null, bool waitForExit = true)
		{
			//Console.WriteLine ("{0} {1}", fileName, arguments);
			var si = new System.Diagnostics.ProcessStartInfo (fileName, arguments);
			if (!string.IsNullOrEmpty (cd))
				si.WorkingDirectory = cd;

			if (!waitForExit) {
				System.Diagnostics.Process.Start (si);
				return "";
			}

			si.UseShellExecute = false;
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
						if (ErrorFunc != null) {
							ErrorFunc (line);
						}
						else {
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine (line);
						}
					}
				}
			});
			p.WaitForExit ();
			await readOutTask;
			await readErrorTask;
			if (throwOnError && p.ExitCode != 0)
				throw new Exception ($"{fileName} failed with exit code {p.ExitCode}");
			if (showOutput)
				Console.ResetColor ();
			return sb.ToString ();
		}
	}
}
