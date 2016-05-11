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
		public Microsoft.Build.Framework.ITaskItem Template { get; set; }

		/// <summary>
		/// The item spec of the Include attribute, split by ';'.
		/// </summary>
		[Output]
		public Microsoft.Build.Framework.ITaskItem[] Includes { get; set; }

		/// <summary>
		/// The item spec of the Exclude attribute, split by ';'.
		/// </summary>
		[Output]
		public Microsoft.Build.Framework.ITaskItem[] Excludes { get; set; }

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
						{ "SourceDir", include.IndexOf("**") != -1 ?
								// We have a relative dir in the path.
								new DirectoryInfo(Path.Combine(templateFullDir, include.Substring(0, include.IndexOf("**")))).FullName :
								// We don't have relative dirs, so just pick the path.
								new DirectoryInfo(Path.Combine(templateFullDir, include.Substring(0, include.LastIndexOf(@"\") + 1))).FullName
						}
					}))
								.ToArray ();

			var templateExclude = this.Template.GetMetadata ("Exclude") ?? "";
			this.Excludes = templateExclude
								.Split (new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
								.Select (exclude => new TaskItem (Path.Combine (templateFullDir, exclude), new Dictionary<string, string>
					{
						// NOTE: these values are used by AssignIncludeTargetPaths to determine the Link for the files.
						// The files are expanded into individual files by the targets, so that the metadata can be
						// set for each independently.
						{ "TemplateDir", templateRelativeDir },
						{ "SourceDir", exclude.IndexOf("**") != -1 ?
								// We have a relative dir in the path.
								new DirectoryInfo(Path.Combine(templateFullDir, exclude.Substring(0, exclude.IndexOf("**")))).FullName :
								// We don't have relative dirs, so just pick the path.
								new DirectoryInfo(Path.Combine(templateFullDir, exclude.Substring(0, exclude.LastIndexOf(@"\") + 1))).FullName
						}
					}))
								.ToArray ();

			return true;
		}
	}
}
