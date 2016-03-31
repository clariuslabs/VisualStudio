using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Clarius.VisualStudio.Tasks
{
	/// <summary>
	/// Assigns the target path to the given items, by setting the
	/// Link metadata on the items based on the original item directory.
	/// </summary>
	public class AssignIncludeTargetPaths : Task
	{
		/// <summary>
		/// Files to assign the target paths to.
		/// </summary>
		[Required]
		public Microsoft.Build.Framework.ITaskItem[] Files { get; set; }

		/// <summary>
		/// Files with the assigned target path.
		/// </summary>
		[Output]
		public Microsoft.Build.Framework.ITaskItem[] AssignedFiles { get; set; }

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
