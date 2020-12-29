using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using maccat;
using MacCatSdk;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using static maccat.Terminal;

namespace Praeclarum.MacCatalyst
{
	public class PraeclarumMacCatalystBuildTask : Task
	{
		public string SdkPath { get; set; } = "";
		public string ProjectFile { get; set; } = "";
		public string ProjectAssemblyName { get; set; } = "";
		public string ProjectOutputPath { get; set; } = "";
		public string Configuration { get; set; } = "";
		public string Platform { get; set; } = "";
		public bool Run { get; set; } = false;
		public ITaskItem[] InputFiles { get; set; } = Array.Empty<ITaskItem> ();

		public override bool Execute ()
		{
			InfoFunc = m => Log.LogMessage (m);
			WarningFunc = m => Log.LogWarning (m);
			ErrorFunc = m => Log.LogError (m);
			try {
				var sdkPath = ExpandSdk ();
				var builder = new BuildApp (ProjectFile, Configuration, Platform, Run, sdkPath, ProjectAssemblyName, ProjectOutputPath);
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

		string ExpandSdk ()
		{
			var asm = GetType ().Assembly;
			var names = asm.GetManifestResourceNames ();
			var name = names.FirstOrDefault (x => x.Contains("maccat-sdk-") && x.EndsWith (".zip"));
			if (!string.IsNullOrEmpty (name)) {
				var simpleName = Path.GetFileNameWithoutExtension (name[name.IndexOf ("maccat-sdk-")..]);

				var sdkPath = Path.Combine (Path.GetTempPath (), simpleName);
				var sdkTestFilePath = Path.Combine (sdkPath, "xamarin-maccat", "Xamarin.iOS.dll");
				var sdkExists = File.Exists (sdkTestFilePath);
				if (sdkExists)
					return sdkPath;

				Info ($"Installing SDK in \"{sdkPath}\".");

				using var zips = asm.GetManifestResourceStream (name);

				using var zip = new ZipArchive (zips, ZipArchiveMode.Read);
				zip.ExtractToDirectory (sdkPath, true);

				sdkExists = File.Exists (sdkTestFilePath);
				if (sdkExists)
					return sdkPath;

				throw new InvalidOperationException ("SDK failed to extract");
			}
			throw new InvalidOperationException ($"Failed to find SDK in {asm.Location}");
		}
	}
}
