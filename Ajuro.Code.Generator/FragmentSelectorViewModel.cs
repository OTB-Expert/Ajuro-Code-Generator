using Ajuro.Net.Template.Processor;
using Ajuro.Notes.Markup;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ajuro.Notes.Views
{
	public class TemplateConfigViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class AppConfigViewModel : INotifyPropertyChanged
	{
		private string outputDirectory { get; set; }
		public string OutputDirectory
		{
			get
			{
				if (baseDirectory == null)
				{
					return tempCodeBaseDirectory + "\\Inflated";
				}
				return outputDirectory;
			}
			set
			{
				outputDirectory = value;
				NotifyPropertyChanged();
			}
		}

		private string baseDirectory { get; set; }
		public string BaseDirectory
		{
			get
			{
				if(baseDirectory == null)
				{
					return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OTB\\Ajuro.Notes.Markup";
				}
				return baseDirectory;
			}
			set
			{
				baseDirectory = value;
				NotifyPropertyChanged();
			}
		}

		// The original code used to edit / build the temp.txt template
		private string tempCodeBaseDirectory { get; set; }
		public string TempCodeBaseDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(tempCodeBaseDirectory))
				{
					return tempCodeBaseDirectory + "\\Originals";
				}
				else
				{
					return baseDirectory;
				}
			}
			set
			{
				tempCodeBaseDirectory = value;
				NotifyPropertyChanged();
			}
		}

		// The template under construction used for preview. If you are happy with it and want to use it, copy it to Template
		private string tempTextBaseDirectory { get; set; }
		public string TempTextBaseDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(tempTextBaseDirectory))
				{
					return tempTextBaseDirectory + "\\Text";
				}
				else
				{
					return BaseDirectory;
				}
			}
			set
			{
				tempTextBaseDirectory = value;
				NotifyPropertyChanged();
			}
		}

		private string tempDataBaseDirectory { get; set; }
		public string TempDataBaseDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(tempDataBaseDirectory))
				{
					return tempDataBaseDirectory + "\\JsonData";
				}
				else
				{
					return BaseDirectory;
				}
			}
			set
			{
				tempDataBaseDirectory = value;
				NotifyPropertyChanged();
			}
		}

		private string tempJsonBaseDirectory { get; set; }
		public string TempJsonBaseDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(tempJsonBaseDirectory))
				{
					return tempJsonBaseDirectory + "\\Templates";
				}
				else
				{
					return BaseDirectory;
				}
			}
			set
			{
				tempJsonBaseDirectory = value;
				NotifyPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class FragmentSelectorViewModel : INotifyPropertyChanged
	{
		private AppConfigViewModel appConfig { get; set; }
		public AppConfigViewModel AppConfig
		{
			get { return appConfig; }
			set
			{
				appConfig = value;
				NotifyPropertyChanged();
			}
		}

		private string selection { get; set; }
		public string Selection
		{
			get { return selection; }
			set
			{
				selection = value;
				NotifyPropertyChanged();
			}
		}
		private string originalCode { get; set; }
		public string OriginalCode
		{
			get { return originalCode; }
			set
			{
				originalCode = value;
				NotifyPropertyChanged();
			}
		}
		private object jsonSample { get; set; }
		public object JsonSample
		{
			get { return jsonSample; }
			set
			{
				jsonSample = value;
				NotifyPropertyChanged();
			}
		}
		private string jsonSampleString { get; set; }
		public string JsonSampleString
		{
			get { return jsonSampleString; }
			set
			{
				jsonSampleString = value;
				NotifyPropertyChanged();
			}
		}
		private string structureJson { get; set; }
		public string StructureJson
		{
			get { return structureJson; }
			set
			{
				structureJson = value;
				NotifyPropertyChanged();
			}
		}

		private CodeFragment currentFragment;
		public CodeFragment CurrentFragment
		{
			get
			{
				return currentFragment;
			}
			set
			{
				currentFragment = value;
				NotifyPropertyChanged();
			}
		}
		private CodeFragment rootCodeFragment;
		public CodeFragment RootCodeFragment
		{
			get
			{
				return rootCodeFragment;
			}
			set
			{
				rootCodeFragment = value;
				NotifyPropertyChanged();
			}
		}

		private System.Collections.ObjectModel.ObservableCollection<TemplaterInstructionViewModel> templaterInstructionList { get; set; }
		public System.Collections.ObjectModel.ObservableCollection<TemplaterInstructionViewModel> TemplaterInstructionList
		{
			get
			{
				return templaterInstructionList;
			}
			set
			{
				templaterInstructionList = value;
				templaterInstructionList.CollectionChanged += ContentCollectionChanged;
			}
		}

		public FragmentSelectorViewModel()
		{
			TemplaterInstructionList = new System.Collections.ObjectModel.ObservableCollection<TemplaterInstructionViewModel>();
			TemplaterInstructionList.CollectionChanged += ContentCollectionChanged;
			if(appConfig == null)
			{
				appConfig = new AppConfigViewModel();
			}
		}

		public void ContentCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			//This will get called when the collection is changed
		}

		public TemplaterInstructionViewModel TemplaterInstruction { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}