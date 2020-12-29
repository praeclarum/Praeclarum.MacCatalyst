using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Praeclarum.MacCatalyst
{
	public class PraeclarumMacCatalystBuildTask : Task
	{
		public ITaskItem[] InputFiles { get; set; } = Array.Empty<ITaskItem> ();

		public override bool Execute ()
		{
			Log.LogMessage ("Running");
			return true;
		}
	}
}
