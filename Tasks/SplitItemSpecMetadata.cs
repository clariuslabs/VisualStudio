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
	/// Task that splits the an item's metadata value and returns
	/// each split value as an item.
	/// </summary>
	public class SplitItemSpecMetadata : Task
	{
		/// <summary>
		/// The item that contains the metadata to split.
		/// </summary>
		[Required]
		public ITaskItem Item { get; set; }

		/// <summary>
		/// Name of the metadata value to split.
		/// </summary>
		[Required]
		public System.String MetadataName { get; set; }

		/// <summary>
		/// Optional separator for the split. Defaults to ';'.
		/// </summary>
		public System.String Separator { get; set; }

		/// <summary>
		/// Optionally specify whether the item's directory will
		/// be prepended to the resulting item specs. Defaults to true.
		/// </summary>
		public System.Boolean PrependItemDirectory { get; set; }

		/// <summary>
		/// The resulting items from the split operation.
		/// </summary>
		[Output]
		public ITaskItem[] ItemSpecs { get; set; }

		public override bool Execute ()
		{
			var separator = Separator ?? ";";
			var itemFullDir = Item.GetMetadata ("RootDir") + Item.GetMetadata ("Directory");
			var itemRelativeDir = Item.GetMetadata ("RelativeDir");
			var itemMetadata = Item.GetMetadata (MetadataName) ?? "";
			this.ItemSpecs = itemMetadata
				.Split (new[] { separator }, StringSplitOptions.RemoveEmptyEntries)
				.Select (metadata => new TaskItem (PrependItemDirectory ? Path.Combine (itemFullDir, metadata) : metadata, new Dictionary<string, string> {
					{ "ItemDir", itemRelativeDir },
					{ "MetadataDir", metadata.IndexOf("**") != -1 ?
							// We have a relative dir in the path.
							new DirectoryInfo(Path.Combine(itemFullDir, metadata.Substring(0, metadata.IndexOf("**")))).FullName :
							// We don't have relative dirs, so just pick the path.
							new DirectoryInfo(Path.Combine(itemFullDir, metadata.Substring(0, metadata.LastIndexOf(@"\") + 1))).FullName
					}
				}))
				.ToArray ();

			return true;
		}
	}
}
