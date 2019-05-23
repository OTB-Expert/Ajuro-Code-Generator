using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuro.Notes.Markup
{
	public class TemplateManager
	{
		public enum MarkerType { Repeat, Replace };

		public static List<List<string>> Markers = new List<List<string>>()
		{
			// Repeat
			new List<string>()
			{
				"<span class='fragment_marker'>!====================",
				"====================</span><span class='repeat_fragment'>",
				"</span><span class='fragment_marker'>====================!</span>"
			},
			// Replace
			new List<string>()
			{
				"<span class='fragment_marker'>@{</span><span class='replace_fragment'>",
				"</span><span class='fragment_marker'>}@</span>"
			}
		};

		private string Escape(string content)
		{
			return content.Replace("<", "&lt;").Replace("\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(" ", "&nbsp;");
		}
		
		public string ToHtmlString(Ajuro.Net.Template.Processor.CodeFragment codeFragment)
		{
			StringBuilder stringBuilder = new StringBuilder();
			codeFragment.FormattedContent = codeFragment.Content;// .Replace("<", "&lt;").Replace("\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(" ", "&nbsp;");

			if (codeFragment.Fragments != null)
			{
				for (int i = codeFragment.Fragments.Count-1; i> -1; i--)
				{
					if (codeFragment.Fragments[i].Type.Equals((int)MarkerType.Repeat))
					{
						var templateFragment = ToHtmlString(codeFragment.Fragments[i]);
						//if(codeFragment.FormattedContent)
						{

						}
						//codeFragment.FormattedContent = Escape(codeFragment.FormattedContent.Substring(0, codeFragment.Fragments[i].SelectionStart)) + Markers[(int)MarkerType.Repeat][0] + codeFragment.Fragments[i].Name + Markers[(int)MarkerType.Repeat][1] + Escape(templateFragment) + Markers[(int)MarkerType.Repeat][2] + Escape(codeFragment.FormattedContent.Substring(codeFragment.Fragments[i].SelectionStart + codeFragment.Fragments[i].SelectionLength));
					}
					else if (codeFragment.Fragments[i].Type.Equals((int)MarkerType.Replace))
					{
						// codeFragment.FormattedContent = codeFragment.FormattedContent.Substring(0, codeFragment.Fragments[i].SelectionStart) + Markers[(int)MarkerType.Replace][0] + codeFragment.Fragments[i].Name + Markers[(int)MarkerType.Replace][1] + codeFragment.FormattedContent.Substring(codeFragment.Fragments[i].SelectionStart + codeFragment.Fragments[i].SelectionLength);
					}
				}
			}
			return codeFragment.FormattedContent;
		}
	}
}
