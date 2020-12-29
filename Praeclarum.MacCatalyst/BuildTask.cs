using System;
using maccat;
using MacCatSdk;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Praeclarum.MacCatalyst
{
	public class PraeclarumMacCatalystBuildTask : Task
	{
		public string SdkPath { get; set; } = "";
		public string ProjectFile { get; set; } = "";
		public string ProjectOutputPath { get; set; } = "";
		public string Configuration { get; set; } = "";
		public string Platform { get; set; } = "";
		public bool Run { get; set; } = false;
		public ITaskItem[] InputFiles { get; set; } = Array.Empty<ITaskItem> ();

		public override bool Execute ()
		{
			Terminal.InfoFunc = m => Log.LogMessage (m);
			Terminal.WarningFunc = m => Log.LogWarning (m);
			Terminal.ErrorFunc = m => Log.LogError (m);
			var builder = new BuildApp (ProjectFile, Configuration, Platform, Run, SdkPath);
			try {
				builder.RunAsync ().Wait ();
				return true;
			}
			catch (Exception ex) {
				while (ex.InnerException != null)
					ex = ex.InnerException;
				Log.LogErrorFromException (ex);
				return false;
			}
		}
	}
}
