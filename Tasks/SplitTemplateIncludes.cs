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
	/// Task that splits the a template's Include value and returns
	/// each split value as an item with metadata that can be used
	/// later by AssignIncludeTargetPaths to set the Link property.
	/// </summary>
	public class SplitTemplateIncludes : Task
	{
		/// <summary>
		/// Template source with optional Include metadata attribute.
		/// </summary>
		[Required]
		public ITaskItem Template { get; set; }

		/// <summary>
		/// The item spec of the Include attribute, split by ';'.
		/// </summary>
		[Output]
		public ITaskItem[] Includes { get; set; }

		public override bool Execute ()
		{
			var templateFullDir = this.Template.GetMetadata ("RootDir") + this.Template.GetMetadata ("Directory");
			var templateRelativeDir = this.Template.GetMetadata ("RelativeDir");
			var templateInclude = this.Template.GetMetadata ("Include") ?? "";
			this.Includes = templateInclude
								.Split (new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
								.Select (include => new TaskItem (Path.Combine (templateFullDir, include), new Dictionary<string, string>
					{
						// NOTE: these values are used by AssignIncludeTargetPaths to determine the Link for the files.
						// The files are expanded into individual files by the targets, so that the metadata can be
						// set for each independently.
						{ "TemplateDir", templateRelativeDir },
						{ "IncludedDir", include.IndexOf("**") != -1 ?
								// We have a relative dir in the path.
								new DirectoryInfo(Path.Combine(templateFullDir, include.Substring(0, include.IndexOf("**")))).FullName :
								// We don't have relative dirs, so just pick the path.
								new DirectoryInfo(Path.Combine(templateFullDir, include.Substring(0, include.LastIndexOf(@"\") + 1))).FullName
						}
					}))
								.ToArray ();

			return true;
		}
	}
}
