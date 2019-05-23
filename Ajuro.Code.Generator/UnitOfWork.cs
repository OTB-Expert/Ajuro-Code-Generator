namespace Ajuro.Notes.Markup
{
	public class UnitOfWork
	{
		public string Name { get; set; }

		public bool OriginalCodeHasChanged { get; set; }
		public bool DataJsonHasChanged { get; set; }
		public bool TemplateStructureHasChanged { get; set; }
		public bool LastOutputHasChanged { get; set; }
		public string originalCode { get; set; }
		public string OriginalCode {
			get { return originalCode; }
			set
			{
				if(originalCode != value)
				{
					originalCode = value;
					OriginalCodeHasChanged = true;
				}
			}
		}

		public string dataJson { get; set; }
		public string DataJson
		{
			get { return dataJson; }
			set
			{
				if (dataJson != value)
				{
					dataJson = value;
					OriginalCodeHasChanged = true;
				}
			}
		}

		public string templateStructure { get; set; }
		public string TemplateStructure
		{
			get { return templateStructure; }
			set
			{
				if (templateStructure != value)
				{
					templateStructure = value;
					OriginalCodeHasChanged = true;
				}
			}
		}

		public string lastOutput { get; set; }
		public string LastOutput
		{
			get { return lastOutput; }
			set
			{
				if (lastOutput != value)
				{
					lastOutput = value;
					OriginalCodeHasChanged = true;
				}
			}
		}
	}
}