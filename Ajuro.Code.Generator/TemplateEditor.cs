using Ajuro.Net.Template.Processor;
using Ajuro.Notes.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ajuro.Notes.Markup
{
	public partial class TemplateEditor : Form
	{
		Random random = new Random();
		private List<UnitOfWork> UnitsOfWork = new List<UnitOfWork>();
		string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OTB\\Ajuro.Notes.Markup\\";
		public UnitOfWork CurrentUnitOfWork { get; set; }

		// Save selection starting point
		private int SelectionStart { get; set; }

		public ObservableCollection<TemplaterInstructionViewModel> FiltredTemplaterInstructionList { get; set; }

		// Save selection length
		private int SelectionLength { get; set; }
		bool SuspendedSelectionTrigger = false;
		TreeNode CurrentNode { get; set; }
		public bool IsSelecting { get;  set; } // Keep selecting and do not create new nodes while in same selection session.

		Ajuro.Net.Template.Processor.TemplateMarker Marker = new TemplateMarker();
		Ajuro.Notes.Views.FragmentSelectorViewModel vm = new Views.FragmentSelectorViewModel();
		CodeFragment LastFragment = null;


		public TemplateEditor()
		{
			InitializeComponent();
			CustomInitialize();
		}

		private void CustomInitialize()
		{
			try
			{
				if (!Directory.Exists(AppDataFolder))
				{
					Directory.CreateDirectory(AppDataFolder);
				}
				vm.TemplaterInstructionList = new ObservableCollection<TemplaterInstructionViewModel>();
				ListTemplates();
				RealTimeUpdatesCheckBox.Checked = true;
				webBrowserPreview.ScriptErrorsSuppressed = true;
			}
			catch (Exception)
			{

			}

			ClearStructure();


			if (File.Exists(AppDataFolder + "Resources\\AppConfig.json"))
			{
				try
				{
					vm.AppConfig = JsonConvert.DeserializeObject<AppConfigViewModel>(File.ReadAllText(AppDataFolder + "Resources\\AppConfig.json"));
					richTextBoxAppConfig.Text = JsonConvert.SerializeObject(vm.AppConfig, Formatting.Indented).Replace("\\\\", "\\");
				}
				catch (Exception e)
				{

				}
			}

			try
			{
				Marker.RestoreParents(vm.RootCodeFragment);

				richTextBoxParentCode.Text = File.ReadAllText("Resources\\InputExample.txt");
				richTextBoxParentCode.Text = richTextBoxParentCode.Text.Replace("\r\n", "\n");
				RichTextBoxCodeStructurePreview.Text = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
				vm.CurrentFragment = vm.RootCodeFragment;
				vm.CurrentFragment.Content = richTextBoxParentCode.Text;
				PreviewHTML();
				UpdateTree(vm.RootCodeFragment.Fragments, FragmentsTreeView.Nodes);
				FragmentsTreeView.ExpandAll();
				ColorizeText(vm.RootCodeFragment);
				richTextBoxParentCode.Select(0, 0);
				this.FormClosing += MarkingForm_FormClosing;
				FilterTemplates(string.Empty);
				tabControlContent.SelectedIndex = 1;
			}
			catch (Exception)
			{

			}
		}

		#region Events


		private void listBoxTemplates_KeyUp(object sender, KeyEventArgs e)
		{
			if (listBoxTemplates.SelectedItem == null)
			{
				return;
			}

			if (e.KeyCode == Keys.Delete)
			{
				TemplateKeyUp();
			}
		}

		private void buttonSaveTemplate_Click(object sender, EventArgs e)
		{
			SaveTemplateText();
		}

		private void buttonSaveOriginalText_Click(object sender, EventArgs e)
		{
			SaveOriginalText();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveCurentUnitOfWork();
		}

		private void MarkingForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveCurrentUnitOfWork();
		}

		private void richTextBox1_SelectionChanged(object sender, EventArgs e)
		{
			UpdateSelection();
		}

		private void richTextBoxParentCode_TextChanged(object sender, EventArgs e)
		{
			vm.RootCodeFragment.Content = richTextBoxParentCode.Text;
		}

		private void CheckForChanges()
		{
			bool hasChanges = CurrentUnitOfWork.OriginalCodeHasChanged || CurrentUnitOfWork.TemplateStructureHasChanged || CurrentUnitOfWork.DataJsonHasChanged || CurrentUnitOfWork.LastOutputHasChanged;
			buttonSaveTemplateFiles.Visible = hasChanges;
		}

		private void richTextBoxInflated_TextChanged(object sender, EventArgs e)
		{
			if (CurrentUnitOfWork != null)
			{
				CurrentUnitOfWork.LastOutputHasChanged = true;
				buttonSaveResult.BackColor = Color.LightGreen;
			}
		}

		private void richTextBoxParentCode_KeyUp(object sender, KeyEventArgs e)
		{
			if (CurrentUnitOfWork != null)
			{
				CurrentUnitOfWork.OriginalCodeHasChanged = true;
				buttonSaveOriginalText.BackColor = Color.LightGreen;
			}
		}

		private void richTextBoxTemplateConfig_KeyUp(object sender, KeyEventArgs e)
		{
			TemplateConfigUpdated();
		}

		private void buttonSaveData_Click(object sender, EventArgs e)
		{
			SaveDataClicked();
		}

		private void buttonSaveResult_Click(object sender, EventArgs e)
		{
			if (CurrentUnitOfWork == null || vm.TemplaterInstruction == null)
			{
				return;
			}
			SaveResultsClicked();
		}

		private void textBoxTemplatesFilter_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Process.Start(AppDataFolder + "Resources\\TemplateConfigs");
		}

		private void buttonSaveTemplateFiles_Click(object sender, EventArgs e)
		{
			SaveTemplate();
		}

		private void listBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
		{
			TemplateSelected();
		}


		private void richTextBoxParentCode_Click(object sender, EventArgs e)
		{
			vm.CurrentFragment = Marker.GetInnermostFragment(vm.RootCodeFragment, richTextBoxParentCode.SelectionStart, 0);
			AdoptFragment();
		}

		private void textBoxType_KeyUp(object sender, KeyEventArgs e)
		{
			vm.CurrentFragment.Generator = textBoxType.Text;
		}

		private void richTextBoxAppConfig_KeyUp(object sender, KeyEventArgs e)
		{
			AppConfigUpdated();
		}

		private void JsonEditorRichTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			JsonDataUpdated();
		}

		private void JsonEditorRichTextBox_TextChanged(object sender, EventArgs e)
		{
			JsonTemplateUpdated();
		}

		private void textBoxTemplatesFilter_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				TemplateFilterUpdated();
			}
			else
			{
				string templateName = textBoxTemplatesFilter.Text.Trim();
				FilterTemplates(templateName.ToLower());
			}
		}

		private void RichTextBoxChildCode_KeyUp(object sender, KeyEventArgs e)
		{
			if (RealTimeUpdatesCheckBox.Checked)
			{
				vm.CurrentFragment.Content = richTextBoxChildCode.Text;
				RichTextBoxCodeStructurePreview.Text = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
				PreviewHTML();
			}
		}


		private void ButtonReset_Click(object sender, EventArgs e)
		{
			ClearStructure();
		}

		private void FragmentsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			FragmentSelected();
		}

		private void richTextBoxParentCode_MouseUp(object sender, MouseEventArgs e)
		{
			IsSelecting = false;
			try
			{
				if (LastFragment != null && richTextBoxParentCode != null)
				{
					richTextBoxParentCode.SelectionBackColor = Color.FromArgb(LastFragment.Color[0], LastFragment.Color[1], LastFragment.Color[2]);
				}
			}
			catch (Exception)
			{

			}
		}

		private void FragmentNameTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (CurrentNode != null)
			{
				CurrentNode.Text = FragmentNameTextBox.Text.Trim();
				vm.CurrentFragment.Name = FragmentNameTextBox.Text.Trim();
			}
		}

		private void FragmentsTreeView_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (FragmentsTreeView.SelectedNode != null)
				{
					var codeFragment = (CodeFragment)FragmentsTreeView.SelectedNode.Tag;
					DeleteFragment(codeFragment);
				}
			}
		}

		#endregion Events

		private void ListTemplates()
		{
			FiltredTemplaterInstructionList = new ObservableCollection<TemplaterInstructionViewModel>();
			vm.TemplaterInstructionList.Clear();
			if (!Directory.Exists(AppDataFolder + "Resources\\TemplateConfigs"))
			{
				Directory.CreateDirectory(AppDataFolder + "Resources\\TemplateConfigs");
			}
			foreach (var file in Directory.GetFiles(AppDataFolder + "Resources\\TemplateConfigs", "*.rd"))
			{
				string templateName = file.Substring(file.LastIndexOf('\\') + 1);
				templateName = templateName.Substring(0, templateName.LastIndexOf('.'));
				vm.TemplaterInstructionList.Add(new TemplaterInstructionViewModel()
				{
					Name = templateName
				});
			}
		}

		private void ColorizeText(CodeFragment currentFragment)
		{
			if (currentFragment.Color != null)
			{
				SuspendedSelectionTrigger = true;
				richTextBoxParentCode.SelectionStart = currentFragment.SelectionStart;
				richTextBoxParentCode.SelectionLength = currentFragment.SelectionLength;
				richTextBoxParentCode.SelectionBackColor = Color.FromArgb(currentFragment.Color[0], currentFragment.Color[1], currentFragment.Color[2]);
				SuspendedSelectionTrigger = false;
			}
			if (currentFragment.Fragments != null)
			{
				foreach (var fragment in currentFragment.Fragments)
				{
					ColorizeText(fragment);
				}
			}
		}

		public void SaveCurrentUnitOfWork()
		{
			File.WriteAllText(AppDataFolder + "Resources\\BackupStructure.txt", JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented));
			Marker.JsonRoot = JsonConvert.DeserializeObject(richTextBoxJsonData.Text);
			File.WriteAllText(AppDataFolder + "Resources\\JsonObject.json", JsonConvert.SerializeObject(Marker.JsonRoot, Formatting.Indented));
			if (!File.Exists(AppDataFolder + "Resources\\AppConfig.json"))
			{
				var file = File.Create(AppDataFolder + "Resources\\AppConfig.json");
				file.Close();
			}
			File.WriteAllText(AppDataFolder + "Resources\\AppConfig.json", JsonConvert.SerializeObject(vm.AppConfig, Formatting.Indented));
			SaveTemplate();
		}

		private string Escape(string content)
		{
			return content.Replace("<", "&lt;").Replace("\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(" ", "&nbsp;");
		}

		private void UpdateSelection()
		{
			if (SuspendedSelectionTrigger)
			{
				return;
			}
			// User selected a fragment
			SelectionStart = richTextBoxParentCode.SelectionStart;
			SelectionFromTextBox.Text = SelectionStart + String.Empty;
			SelectionLength = richTextBoxParentCode.SelectionLength;
			SelectionLengthTextBox.Text = SelectionLength + String.Empty;
			string text = richTextBoxParentCode.SelectedText;

			if (SelectionLength > 0) // otherwise is just carrt position changed
			{
				if (!IsSelecting && vm.CurrentFragment.SelectionStart != SelectionStart && vm.CurrentFragment.SelectionStart + vm.CurrentFragment.SelectionLength != SelectionStart + SelectionLength)
				{
					IsSelecting = true;
					CodeFragment parent = Marker.GetInnermostFragment(vm.RootCodeFragment, SelectionStart, SelectionLength);
					vm.CurrentFragment = new CodeFragment() { Name = "Text", Parent = parent };
					var k = 0;
					foreach(var f in parent.Fragments)
					{
						if (f != vm.CurrentFragment && f.SelectionStart + f.SelectionLength < SelectionStart)
						{
							k++;
						}
					}
					parent.Fragments.Insert(k, vm.CurrentFragment);
					myTreeNode = new TreeNode(vm.CurrentFragment.Name);
					myTreeNode.Tag = vm.CurrentFragment;
					if (parent.TreeViewItem != null)
					{
						var pn = (TreeNode)parent.TreeViewItem;
						var i = 0;
						foreach (var f in parent.Fragments)
						{
							if (f != vm.CurrentFragment && f.SelectionStart + f.SelectionLength < SelectionStart)
							{
								i++;
							}
						}
						pn.Nodes.Insert(i, myTreeNode);
					}
					else
					{
						var i = 0;
						foreach(var f in vm.RootCodeFragment.Fragments)
						{
							if(f!=vm.CurrentFragment && f.SelectionStart + f.SelectionLength < SelectionStart)
							{
								i++;
							}
						}
						FragmentsTreeView.Nodes.Insert(i, myTreeNode);
					}
					vm.CurrentFragment.TreeViewItem = myTreeNode;
				}
			
				vm.CurrentFragment.SelectionStart = SelectionStart;
				vm.CurrentFragment.SelectionLength = SelectionLength;

				Marker.UpdateFromSelection2(vm.CurrentFragment);
				FragmentsTreeView.ExpandAll();

				vm.CurrentFragment.Color = new int[] { random.Next(55) + 200, random.Next(55) + 200, random.Next(55) + 200 };
				
				if (text.Trim().IndexOfAny(new char[] { '\r', '\n', ' ', '\t' }) > -1)
				{
					vm.CurrentFragment.Type = 0;
					UpdateRadioButtond(vm.CurrentFragment.Type);
				}
				else
				{
					vm.CurrentFragment.Name = text.Trim();
					vm.CurrentFragment.Type = 1;
					UpdateRadioButtond(vm.CurrentFragment.Type);
				}
				
				vm.CurrentFragment.Content = text;
				FragmentsTreeView.Nodes.Clear();
				UpdateTree(vm.RootCodeFragment.Fragments, FragmentsTreeView.Nodes);

				myTreeNode = (TreeNode)vm.CurrentFragment.TreeViewItem;
				if (myTreeNode != null)
				{
					myTreeNode.Text = vm.CurrentFragment.Name + (vm.CurrentFragment.Type == 1 ? "" : " section") + " - " + SelectionStart + "-" + SelectionLength;
				}

				FragmentsTreeView.ExpandAll();
			}
		}

		private void UpdateRadioButtond(int type)
		{
			radioButtonRepeat.Checked = type == 0;
			radioButtonReplace.Checked = type == 1;
		}

		TreeNode myTreeNode;

		private void UpdateSelection0()
		{
			if (SuspendedSelectionTrigger)
			{
				return;
			}
			// User selected a fragment
			richTextBoxChildCode.Text = richTextBoxParentCode.SelectedText;
			SelectionStart = richTextBoxParentCode.SelectionStart;
			SelectionFromTextBox.Text = SelectionStart + String.Empty;
			SelectionLength = richTextBoxParentCode.SelectionLength;
			SelectionLengthTextBox.Text = SelectionLength + String.Empty;

			richTextBox1.Text = richTextBoxParentCode.Text.Substring(0, SelectionLength) + "\r\n----------------------------------------\r\n" + richTextBoxParentCode.Text.Substring(SelectionLength);
			richTextBox1.Text = richTextBox1.Text.Replace("\r\n", "\n");



			if (SelectionLength > 0 && Marker.LastFragmentStart != SelectionStart && Marker.LastFragmentEnd != SelectionStart + SelectionLength)
			{
				if (richTextBoxParentCode.SelectedText.Trim().Length > 0)
				{
					Marker.UpdateFromSelection(vm.RootCodeFragment, vm.CurrentFragment, ref LastFragment, SelectionStart, SelectionLength);
					if (LastFragment != null)
					{
						richTextBoxParentCode.SelectionBackColor = Color.FromArgb(LastFragment.Color[0], LastFragment.Color[1], LastFragment.Color[2]);
					}
					RichTextBoxCodeStructurePreview.Text = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
				}

				Marker.Sequence[(int)Marker.LastFragmentType]++;
			}


			if (vm.CurrentFragment != vm.RootCodeFragment && richTextBoxParentCode.SelectionLength > 0) // Makes no sense to compute no selection and in case of shrinking, a minimum duplicate selection would be 1 in lenght, sa for length 0 there is nothing to consider.
			{
				Marker.UpdateFromSelection2(vm.CurrentFragment);
			}

			if (SelectionLength > 0)
			{
				Marker.LastFragmentStart = SelectionStart;
				Marker.LastFragmentEnd = SelectionStart + SelectionLength;
			}

			// It there are spaces in selection, most probably you want a repeat fragment.
			if (richTextBoxParentCode.SelectedText.Trim().IndexOf(' ') < 0 && richTextBoxParentCode.SelectedText.Trim().IndexOf('\t') < 0 && richTextBoxParentCode.SelectedText.Trim().IndexOf('\n') < 0)
			{
				if (SelectionLength > 0)
				{
					Marker.LastFragmentType = Ajuro.Net.Template.Processor.TemplateMarker.MarkerType.Replace;
				}
				radioButtonReplace.Checked = true;
				FragmentNameTextBox.Text = "Property_" + Marker.Sequence[(int)TemplateManager.MarkerType.Replace];
			}
			else
			{
				if (SelectionLength > 0)
				{
					Marker.LastFragmentType = Ajuro.Net.Template.Processor.TemplateMarker.MarkerType.Repeat;
				}
				radioButtonRepeat.Checked = true;
				FragmentNameTextBox.Text = "ItemsArray_" + Marker.Sequence[(int)TemplateManager.MarkerType.Repeat];
			}
			if (RealTimeUpdatesCheckBox.Checked && richTextBoxParentCode.SelectedText.Trim().Length > 0)
			{
				string fragmentName = FragmentNameTextBox.Text.Trim();
				int fragmentType = radioButtonRepeat.Checked ? 0 : 1; // This is not nice. I should not use UI here.

				vm.CurrentFragment.Name = fragmentName;
				vm.CurrentFragment.Type = fragmentType;
				vm.CurrentFragment.SelectionStart = SelectionStart;
				vm.CurrentFragment.SelectionLength = SelectionLength;
				vm.CurrentFragment.Content = richTextBoxChildCode.Text;
				RichTextBoxCodeStructurePreview.Text = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
				PreviewHTML();
			}
			CurrentNode.Text = vm.CurrentFragment.Name + " " + SelectionStart + "-" + SelectionLength;
			FragmentsTreeView.Nodes.Clear();
			if (vm.RootCodeFragment.Fragments != null)
			{
				CheckNesting(vm.RootCodeFragment);
				UpdateTree(vm.RootCodeFragment.Fragments, FragmentsTreeView.Nodes);
				FragmentsTreeView.ExpandAll();
			}
		}

		private void CheckNesting(CodeFragment rootCodeFragment)
		{
			if (rootCodeFragment.Parent != null)
			{
				var k = rootCodeFragment.Parent.Fragments.Count;
				for (int i = 0; i < k; i++)
				{
					var fragment = rootCodeFragment.Parent.Fragments[i];
					if (fragment != rootCodeFragment)
					{
						if (fragment.SelectionStart < rootCodeFragment.SelectionStart && fragment.SelectionStart + fragment.SelectionLength > rootCodeFragment.SelectionStart + rootCodeFragment.SelectionLength)
						{
							rootCodeFragment.Parent.Fragments.Remove(rootCodeFragment);
							fragment.Fragments.Add(rootCodeFragment);
							rootCodeFragment.Parent = fragment;
							k++;
						}
					}
				}
			}

			if (rootCodeFragment.Fragments != null)
				foreach (var fragment in rootCodeFragment.Fragments)
				{
					//CheckNesting(fragment);
				}
		}

		private void traverse_tree_inside(CodeFragment current, ref CodeFragment newFragment)
		{
			return;
			bool found_a_fit = false;
			if (current.Fragments != null)
			{
				//loop through childrem
				for (var i = 0; i < current.Fragments.Count; i++)
				{
					//if the selection fits, check if the child has more children where it could fit as a child
					if (current.Fragments[i].SelectionStart <= SelectionStart && current.Fragments[i].SelectionStart + current.Fragments[i].SelectionLength >= SelectionStart + SelectionLength && !(current.Fragments[i].SelectionStart == SelectionStart && current.Fragments[i].SelectionStart + current.Fragments[i].SelectionLength == SelectionStart))
					{
						found_a_fit = true;
						CodeFragment newCurrent = current.Fragments[i];
						traverse_tree_inside(newCurrent, ref newFragment);
					}

				}
				if (found_a_fit == false)
				//if it didn't find a child that could take the new selection as a child
				{
					current.Fragments.Add(newFragment);
					newFragment.Parent = current;
				}
			}
			else //if the child doesn't have children
			{
				current.Fragments = new ObservableCollection<CodeFragment>();
				current.Fragments.Add(newFragment);
				newFragment.Parent = current;
			}
		}

		private void UpdateTree(ObservableCollection<CodeFragment> fragments, TreeNodeCollection nodes)
		{
			foreach (var fragment in fragments)
			{
				var treeNodeItem = new TreeNode(fragment.Name + " " + fragment.SelectionStart + " - " + fragment.SelectionLength + "");
				treeNodeItem.Tag = fragment;
				fragment.TreeViewItem = treeNodeItem;
				nodes.Add(treeNodeItem);
				if (fragment.Fragments != null)
				{
					UpdateTree(fragment.Fragments, treeNodeItem.Nodes);
				}
			}

			//me code, should change it:
			//TreeNode node = new TreeNode(CurrentFragment.Name);
			//node.Tag = CurrentFragment;
			//node.Name = CurrentFragment.Name;

			//Boolean added = false;

			//TreeNodeCollection nodes = treeView1.Nodes;
			//foreach (TreeNode n in nodes)
			//{
			//    var selectionStart = ((CodeFragment)node.Tag).SelectionStart;
			//    var selectionEnd = ((CodeFragment)node.Tag).SelectionStart + ((CodeFragment)node.Tag).SelectionLength;
			//    if ( (selectionStart < ((CodeFragment)n.Tag).SelectionStart) && (selectionEnd < (((CodeFragment)n.Tag).SelectionStart + ((CodeFragment)n.Tag).SelectionLength)) )
			//    {
			//        n.Nodes.Add(CurrentFragment.Content);
			//        added = true;
			//    }

			//}

			//if (added == false)
			//{
			//    treeView1.Nodes.Add(node);
			//    node.Nodes.Add(((CodeFragment)node.Tag).Content);
			//}


		}

		private void SaveCurentUnitOfWork()
		{
			string fragmentName = FragmentNameTextBox.Text.Trim();
			int fragmentType = radioButtonRepeat.Checked ? 0 : 1;
			if (string.IsNullOrEmpty(fragmentName))
			{
				MessageBox.Show("Please define both name and type!");
			}
			CodeFragment newFragment = new CodeFragment()
			{
				Name = fragmentName,
				Type = fragmentType,
				SelectionStart = SelectionStart,
				SelectionLength = SelectionLength,
				Content = richTextBoxChildCode.Text
			};
			if (vm.CurrentFragment.Fragments == null)
			{
				vm.CurrentFragment.Fragments = new ObservableCollection<CodeFragment>();
			}
			vm.CurrentFragment.Fragments.Add(newFragment);

			File.WriteAllText(AppDataFolder + "Resources\\BackupStructure.txt", JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented));

			richTextBoxParentCode.Text = richTextBoxChildCode.Text;
			richTextBoxChildCode.Text = string.Empty;
			RichTextBoxCodeStructurePreview.Text = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
			PreviewHTML();
			newFragment.Parent = vm.CurrentFragment;
			vm.CurrentFragment = newFragment;
		}

		private void PreviewHTML()
		{
			webBrowserPreview.Refresh();
			/// webBrowserPreview.DocumentText = "<html><style>.fragment_marker { color: green; display: none; } .repeat_fragment { border: 1 solid magenta; margin: 2px; } .replace_fragment { border: 1 solid blue; margin: 2px; }</style>" + Marker.ToHtmlString(vm.RootCodeFragment) + "</html>";
		}

		private void AdoptFragment()
		{
			richTextBoxChildCode.Text = vm.CurrentFragment.Content;
			if (vm.CurrentFragment.TreeViewItem != null)
			{
				TreeNode node = (TreeNode)vm.CurrentFragment.TreeViewItem;
				FragmentsTreeView.SelectedNode = (node);
			}

			string inflated = Marker.Inflate(vm.CurrentFragment, Marker.SelectOrCreateJson(vm.CurrentFragment));
			richTextBoxInflated.Text = inflated;
		}

		private void ClearStructure()
		{
			////// richTextBoxParentCode.Text = File.ReadAllText(AppDataFolder + "Resources\\InputExample.txt");
			richTextBoxParentCode.Text = richTextBoxParentCode.Text.Replace("\r\n", "\n");
			vm.RootCodeFragment = new CodeFragment() { Name = "Root", SelectionLength = richTextBoxParentCode.Text.Length };
			vm.CurrentFragment = vm.RootCodeFragment;
			vm.CurrentFragment.Content = richTextBoxParentCode.Text;
			RichTextBoxCodeStructurePreview.Text = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
			PreviewHTML();
			FragmentsTreeView.Nodes.Clear();
			UpdateTree(vm.CurrentFragment.Fragments, FragmentsTreeView.Nodes);
		}

		private void FragmentSelected()
		{
			CurrentNode = FragmentsTreeView.SelectedNode;
			if (FragmentsTreeView.SelectedNode != null)
			{
				vm.CurrentFragment = (CodeFragment)FragmentsTreeView.SelectedNode.Tag;
				if (LastFragment != null && LastFragment != vm.CurrentFragment)
				{
					ColorizeText(LastFragment);
				}

				richTextBoxChildCode.Text = vm.CurrentFragment.Content;
				SuspendedSelectionTrigger = true;
				richTextBoxParentCode.SelectionStart = vm.CurrentFragment.SelectionStart;
				richTextBoxParentCode.SelectionLength = vm.CurrentFragment.SelectionLength;
				richTextBoxParentCode.Select(vm.CurrentFragment.SelectionStart, vm.CurrentFragment.SelectionLength);
				richTextBoxParentCode.SelectionBackColor = Color.FromArgb(vm.CurrentFragment.Color[0] - 50, vm.CurrentFragment.Color[1] - 50, vm.CurrentFragment.Color[2] - 50);

				richTextBoxParentCode.SelectionStart = 0;
				richTextBoxParentCode.SelectionLength = 0;
				SuspendedSelectionTrigger = false;
				LastFragment = vm.CurrentFragment;
				FragmentNameTextBox.Text = vm.CurrentFragment.Name;
				SelectionFromTextBox.Text = vm.CurrentFragment.SelectionStart + "";
				SelectionLengthTextBox.Text = vm.CurrentFragment.SelectionLength + "";

				Marker.SelectOrCreateJson(vm.CurrentFragment);
				richTextBoxJsonData.Text = JsonConvert.SerializeObject(Marker.JsonRoot, Formatting.Indented);
			}

			string inflated = Marker.Inflate(vm.CurrentFragment, Marker.SelectOrCreateJson(vm.CurrentFragment));
			richTextBoxInflated.Text = inflated;
		}


		private void DeleteFragment(CodeFragment codeFragment)
		{
			if (codeFragment.Fragments != null)
			{
				foreach (var fragment in codeFragment.Fragments)
				{
					fragment.Parent = codeFragment.Parent;
					codeFragment.Parent.Fragments.Add(fragment);
				}
			}
			codeFragment.Parent.Fragments.Remove(codeFragment);

			FragmentsTreeView.Nodes.Clear();
			UpdateTree(vm.RootCodeFragment.Fragments, FragmentsTreeView.Nodes);
			FragmentsTreeView.ExpandAll();
			ColorizeText(vm.RootCodeFragment);
		}

		private void JsonTemplateUpdated()
		{
			string[] data = Marker.ProcessTemplate(vm.RootCodeFragment);
			// TemplateProcessor TemplateProcessor = new TemplateProcessor();
			// var result0 = TemplateProcessor.UpdateTemplate(richTextBoxJsonData.Text, data[0]);
			// NewItem(templaterInstruction.Ready, result);
			// TemplateInterpreter.InterpretProcessedTemplate(templaterInstruction.Project, MainModel.Instance.SelectedProfile.Properties, result0);
			// richTextBoxInflated.Text = "<pre>" + result0 + "</pre>";


			webBrowserPreview.Refresh();
			webBrowserPreview.DocumentText = "<html><style>.fragment_marker { color: green; display: none; } .repeat_fragment { border: 1 solid magenta; margin: 2px; } .replace_fragment { border: 1 solid blue; margin: 2px; }</style>" + data[1] + "</html>";
			richTextBox1.Text = data[0];

			if (CurrentUnitOfWork != null)
			{
				CurrentUnitOfWork.DataJson = richTextBoxJsonData.Text;
				buttonSaveData.BackColor = Color.LightGreen;
			}
		}

		private void JsonDataUpdated()
		{
			richTextBoxJsonData.BackColor = Color.FromArgb(255, 220, 220);
			try
			{
				Marker.JsonRoot = JsonConvert.DeserializeObject(richTextBoxJsonData.Text);
				File.WriteAllText(AppDataFolder + "Resources\\JsonObject.json", JsonConvert.SerializeObject(Marker.JsonRoot, Formatting.Indented));
				Marker.JsonRoot.ToString();
				richTextBoxJsonData.BackColor = Color.LightYellow;
			}
			catch (Exception ex)
			{

			}
			string inflated = Marker.Inflate(vm.RootCodeFragment, Marker.JsonRoot);
			richTextBoxInflated.Text = inflated;
			if (!File.Exists(AppDataFolder + "Resources\\Inflatedtemplate.txt"))
			{
				var file = File.Create(AppDataFolder + "Resources\\Inflatedtemplate.txt");
				file.Close();
			}
			File.WriteAllText(AppDataFolder + "Resources\\Inflatedtemplate.txt", JsonConvert.SerializeObject(Marker.JsonRoot, Formatting.Indented));
		}

		private void TemplateFilterUpdated()
		{
				string templateName = textBoxTemplatesFilter.Text.Trim();
				if (!File.Exists(templateName))
				{
					vm.TemplaterInstruction = new TemplaterInstructionViewModel()
					{
						TempCode = templateName + ".code.txt",
						TempData = templateName + ".data.json",
						TempJson = templateName + ".temp.json",
						TempText = templateName + ".temp.text",
						Name = templateName
						// = templateName + ".data.json",
					};
					vm.TemplaterInstructionList.Add(vm.TemplaterInstruction);
					FiltredTemplaterInstructionList.Add(vm.TemplaterInstruction);
					// listBoxTemplates.Items.Add(vm.TemplaterInstruction);
					var file = File.Create(AppDataFolder + "Resources\\TemplateConfigs\\" + templateName + ".rd");
					FilterTemplates(textBoxTemplatesFilter.Text.Trim().ToLower()); ;
					file.Close();
					File.WriteAllText(AppDataFolder + "Resources\\TemplateConfigs\\" + templateName + ".rd", JsonConvert.SerializeObject(vm.TemplaterInstruction, Formatting.Indented).Replace("\\\\", "\\"));
				}
		}

		private void FilterTemplates(string filterValue)
		{
			FiltredTemplaterInstructionList.Clear();
			foreach (var item in vm.TemplaterInstructionList)
			{
				if (item.Name.ToLower().Contains(filterValue))
				{
					FiltredTemplaterInstructionList.Add(item);
				}
			}
			listBoxTemplates.DataSource = null;
			listBoxTemplates.DataSource = FiltredTemplaterInstructionList;
			listBoxTemplates.DisplayMember = "Name";
			listBoxTemplates.Refresh();
			listBoxTemplates.Update();
		}

		private void AppConfigUpdated()
		{
			richTextBoxAppConfig.BackColor = Color.FromArgb(255, 220, 220);
			try
			{

				vm.AppConfig = JsonConvert.DeserializeObject<AppConfigViewModel>(richTextBoxAppConfig.Text.Replace("\\", "\\\\"));
				if (!File.Exists(AppDataFolder + "Resources\\AppConfig.json"))
				{
					var file = File.Create(AppDataFolder + "Resources\\AppConfig.json");
					file.Close();
				}
				File.WriteAllText(AppDataFolder + "Resources\\AppConfig.json", JsonConvert.SerializeObject(vm.AppConfig, Formatting.Indented));
				richTextBoxAppConfig.BackColor = Color.LightYellow;
			}
			catch (Exception)
			{

			}
		}

		private void TemplateSelected()
		{
			if (listBoxTemplates.SelectedItem == null)
			{
				return;
			}

			TemplaterInstructionViewModel templateConfig = (TemplaterInstructionViewModel)listBoxTemplates.SelectedItem;
			if (File.Exists(AppDataFolder + "Resources\\TemplateConfigs\\" + templateConfig.Name + ".rd"))
			{
				templateConfig = JsonConvert.DeserializeObject<TemplaterInstructionViewModel>(File.ReadAllText(AppDataFolder + "Resources\\TemplateConfigs\\" + templateConfig.Name + ".rd"));
				vm.TemplaterInstruction = templateConfig;
				var unitOfWork = UnitsOfWork.Find(p => p.Name == templateConfig.Name);
				if (unitOfWork == null)
				{
					unitOfWork = new UnitOfWork() { Name = templateConfig.Name };
				}
				else
				{
				}

				CurrentUnitOfWork = unitOfWork;
				if (File.Exists(vm.AppConfig.TempCodeBaseDirectory + '\\' + templateConfig.TempCode))
				{
					CurrentUnitOfWork.OriginalCode = File.ReadAllText(vm.AppConfig.TempCodeBaseDirectory + '\\' + templateConfig.TempCode);
				}

				if (File.Exists(vm.AppConfig.TempJsonBaseDirectory + '\\' + templateConfig.TempJson))
				{
					CurrentUnitOfWork.TemplateStructure = File.ReadAllText(vm.AppConfig.TempJsonBaseDirectory + '\\' + templateConfig.TempJson);
				}

				if (File.Exists(vm.AppConfig.TempDataBaseDirectory + '\\' + templateConfig.TempData))
				{
					CurrentUnitOfWork.DataJson = File.ReadAllText(vm.AppConfig.TempDataBaseDirectory + '\\' + templateConfig.TempData);
				}

				if (File.Exists(vm.AppConfig.TempTextBaseDirectory + '\\' + templateConfig.TempText))
				{
					CurrentUnitOfWork.LastOutput = File.ReadAllText(vm.AppConfig.TempTextBaseDirectory + '\\' + templateConfig.TempText);
				}

				UnitsOfWork.Add(CurrentUnitOfWork);

				AdoptUnitOfWork();

				richTextBoxTemplateConfig.Text = JsonConvert.SerializeObject(templateConfig, Formatting.Indented).Replace("\\\\", "\\");
			}
		}

		private void AdoptUnitOfWork()
		{
			richTextBoxParentCode.Text = CurrentUnitOfWork.OriginalCode;
			richTextBoxInflated.Text = CurrentUnitOfWork.LastOutput;

			if (CurrentUnitOfWork.TemplateStructure != null)
			{
				ReloadTemplate(CurrentUnitOfWork.TemplateStructure);
			}
			else
			{
				ReloadTemplate(string.Empty);
			}
			FragmentsTreeView.Nodes.Clear();
			UpdateTree(vm.RootCodeFragment.Fragments, FragmentsTreeView.Nodes);
			FragmentsTreeView.ExpandAll();

			richTextBoxJsonData.Text = CurrentUnitOfWork.DataJson;
		}

		private void ReloadTemplate(string text)
		{
			vm.RootCodeFragment = new CodeFragment() { Name = "Root" };
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			vm.RootCodeFragment = JsonConvert.DeserializeObject<CodeFragment>(text);
			Marker.OrderFragments(vm.RootCodeFragment);
			Marker.RestoreParents(vm.RootCodeFragment);

			vm.CurrentFragment = vm.RootCodeFragment;
			vm.CurrentFragment.Content = richTextBoxParentCode.Text;
			RichTextBoxCodeStructurePreview.Text = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
			PreviewHTML();
			ColorizeText(vm.RootCodeFragment);
			richTextBoxParentCode.Select(0, 0);
		}

		private void SaveTemplate()
		{
			if (CurrentUnitOfWork == null)
			{
				return;
			}

			FileStream fileStream = null;
			if (!File.Exists(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode))
			{
				if (!Directory.Exists(vm.AppConfig.TempCodeBaseDirectory))
				{
					Directory.CreateDirectory(vm.AppConfig.TempCodeBaseDirectory);
				}
				fileStream = File.Create(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode);
				fileStream.Close();
			}
			CurrentUnitOfWork.OriginalCode = richTextBoxParentCode.Text;
			File.WriteAllText(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode, CurrentUnitOfWork.OriginalCode);
			CurrentUnitOfWork.OriginalCodeHasChanged = false;

			if (!File.Exists(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson))
			{
				if (!Directory.Exists(vm.AppConfig.TempJsonBaseDirectory))
				{
					Directory.CreateDirectory(vm.AppConfig.TempJsonBaseDirectory);
				}
				fileStream = File.Create(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson);
				fileStream.Close();
			}
			CurrentUnitOfWork.TemplateStructure = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
			File.WriteAllText(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson, CurrentUnitOfWork.TemplateStructure);
			CurrentUnitOfWork.TemplateStructureHasChanged = false;

			if (!File.Exists(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData))
			{
				if (!Directory.Exists(vm.AppConfig.TempDataBaseDirectory))
				{
					Directory.CreateDirectory(vm.AppConfig.TempDataBaseDirectory);
				}
				fileStream = File.Create(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData);
				fileStream.Close();
			}

			CurrentUnitOfWork.DataJson = richTextBoxJsonData.Text;
			File.WriteAllText(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData, CurrentUnitOfWork.DataJson);
			CurrentUnitOfWork.DataJsonHasChanged = false;

			if (!File.Exists(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText))
			{
				if (!Directory.Exists(vm.AppConfig.TempTextBaseDirectory))
				{
					Directory.CreateDirectory(vm.AppConfig.TempTextBaseDirectory);
				}
				fileStream = File.Create(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText);
				fileStream.Close();
			}

			CurrentUnitOfWork.LastOutput = richTextBoxInflated.Text;
			File.WriteAllText(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText, CurrentUnitOfWork.LastOutput);
			CurrentUnitOfWork.LastOutputHasChanged = false;
			CheckForChanges();
		}

		private void SaveResultsClicked()
		{
			FileStream fileStream = null;

			if (!Directory.Exists(vm.AppConfig.TempTextBaseDirectory))
			{
				Directory.CreateDirectory(vm.AppConfig.TempTextBaseDirectory);
			}

			if (!File.Exists(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText))
			{
				fileStream = File.Create(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText);
				fileStream.Close();
			}

			CurrentUnitOfWork.LastOutput = richTextBoxInflated.Text;
			File.WriteAllText(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText, CurrentUnitOfWork.LastOutput);

			CurrentUnitOfWork.LastOutputHasChanged = false;
			buttonSaveResult.BackColor = Color.LemonChiffon;
		}

		private void SaveDataClicked()
		{
			if (CurrentUnitOfWork == null || vm.TemplaterInstruction == null)
			{
				return;
			}
			FileStream fileStream = null;

			if (!Directory.Exists(vm.AppConfig.TempDataBaseDirectory))
			{
				Directory.CreateDirectory(vm.AppConfig.TempDataBaseDirectory);
			}

			if (!File.Exists(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData))
			{
				fileStream = File.Create(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData);
				fileStream.Close();
			}

			CurrentUnitOfWork.DataJson = richTextBoxJsonData.Text;
			File.WriteAllText(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData, CurrentUnitOfWork.DataJson);

			CurrentUnitOfWork.LastOutputHasChanged = false;
			buttonSaveData.BackColor = Color.LemonChiffon;

		}

		private void TemplateConfigUpdated()
		{
			richTextBoxTemplateConfig.BackColor = Color.FromArgb(255, 220, 220);
			string templateName = vm.TemplaterInstruction.Name;
			try
			{
				vm.TemplaterInstruction = JsonConvert.DeserializeObject<TemplaterInstructionViewModel>(richTextBoxTemplateConfig.Text);
				File.WriteAllText(AppDataFolder + "Resources\\TemplateConfigs\\" + templateName + ".rd", JsonConvert.SerializeObject(vm.TemplaterInstruction, Formatting.Indented).Replace("\\\\", "\\"));
				richTextBoxTemplateConfig.BackColor = Color.LightYellow;
			}
			catch (Exception)
			{
			}
		}

		private void SaveOriginalText()
		{
			if (CurrentUnitOfWork == null || vm.TemplaterInstruction == null)
			{
				return;
			}
			FileStream fileStream = null;

			if (!Directory.Exists(vm.AppConfig.TempCodeBaseDirectory))
			{
				Directory.CreateDirectory(vm.AppConfig.TempCodeBaseDirectory);
			}

			if (!File.Exists(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode))
			{
				fileStream = File.Create(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode);
				fileStream.Close();
			}

			CurrentUnitOfWork.OriginalCode = richTextBoxParentCode.Text;
			File.WriteAllText(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode, CurrentUnitOfWork.OriginalCode);

			CurrentUnitOfWork.OriginalCodeHasChanged = false;
			buttonSaveOriginalText.BackColor = Color.LemonChiffon;
		}

		private void SaveTemplateText()
		{
			if (CurrentUnitOfWork == null || vm.TemplaterInstruction == null)
			{
				return;
			}
			FileStream fileStream = null;

			if (!Directory.Exists(vm.AppConfig.TempJsonBaseDirectory))
			{
				Directory.CreateDirectory(vm.AppConfig.TempJsonBaseDirectory);
			}

			if (!File.Exists(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson))
			{
				fileStream = File.Create(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson);
				fileStream.Close();
			}

			CurrentUnitOfWork.TemplateStructure = JsonConvert.SerializeObject(vm.RootCodeFragment, Formatting.Indented);
			File.WriteAllText(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson, CurrentUnitOfWork.TemplateStructure);

			CurrentUnitOfWork.OriginalCodeHasChanged = false;
			buttonSaveTemplate.BackColor = Color.LemonChiffon;
		}

		private void TemplateKeyUp()
		{
			TemplaterInstructionViewModel templateConfig = (TemplaterInstructionViewModel)listBoxTemplates.SelectedItem;
			var response = MessageBox.Show("Are you sure you want to delete " + templateConfig.Name + " ?", "Delete Template", MessageBoxButtons.YesNo);
			if (response == DialogResult.Yes)
			{
				if (File.Exists(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode))
				{
					File.Delete(vm.AppConfig.TempCodeBaseDirectory + '\\' + vm.TemplaterInstruction.TempCode);
				}

				if (File.Exists(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson))
				{
					File.Delete(vm.AppConfig.TempJsonBaseDirectory + '\\' + vm.TemplaterInstruction.TempJson);
				}

				if (File.Exists(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData))
				{
					File.Delete(vm.AppConfig.TempDataBaseDirectory + '\\' + vm.TemplaterInstruction.TempData);
				}

				if (File.Exists(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText))
				{
					File.Delete(vm.AppConfig.TempTextBaseDirectory + '\\' + vm.TemplaterInstruction.TempText);
				}

				if (File.Exists(AppDataFolder + "Resources\\TemplateConfigs" + templateConfig.Name + ".rd"))
				{
					File.Delete(AppDataFolder + "Resources\\TemplateConfigs" + templateConfig.Name + ".rd");
				}
			}
			FiltredTemplaterInstructionList.Remove(templateConfig);
			vm.TemplaterInstructionList.Remove(templateConfig);
			FilterTemplates(textBoxTemplatesFilter.Text.Trim().ToLower());
		}

		private void richTextBoxParentCode_DoubleClick(object sender, EventArgs e)
		{
			IsSelecting = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Process.Start("Resources\\Help.html");
		}
	}
}