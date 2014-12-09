using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Clarius.VisualStudio.Tasks
{
	public class SplitTemplateIncludes : Task
	{
		[Required]
		public ITaskItem Template { get; set; }

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
