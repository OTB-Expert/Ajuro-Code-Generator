using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuro.Notes.Markup
{

	public class TemplaterInstructionViewModel
	{
		// Idented CSV to build the json
		public string Name { get; set; }

		// Idented CSV to build the json
		public string CSV { get; set; }

		// TemplateFilePath
		public string Template { get; set; }

		// VariablesFilePath
		public string Model { get; set; }

		// Save the template here
		public string Ready { get; set; }

		// Save the template here
		public string TempCode { get; set; }

		public string TempData { get; set; }

		// Save the template here
		public string TempText { get; set; }

		// Save the template here
		public string TempJson { get; set; }

		// Save the template here
		public string Project { get; set; }
	}
}
