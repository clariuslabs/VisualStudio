using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Clarius.VisualStudio.Tasks
{
	public class AssignIncludeTargetPaths : Task
	{
		[Required]
		public ITaskItem[] Files { get; set; }

		public ITaskItem[] AssignedFiles { get; set; }

		public override bool Execute ()
		{
			this.AssignedFiles = this.Files.Select (i => new TaskItem (i)).ToArray ();
			foreach (var item in this.AssignedFiles) {
				item.SetMetadata ("Link",
					Path.Combine (item.GetMetadata ("TemplateDir"),
						new FileInfo (item.GetMetadata ("FullPath"))
							.FullName.Replace (item.GetMetadata ("IncludedDir"), "")
					));

				// For compatibility with the built-in AssignTargetPath task that runs for content files.
				item.SetMetadata ("TargetPath", item.GetMetadata ("Link"));
			}

			return true;
		}
	}
}
