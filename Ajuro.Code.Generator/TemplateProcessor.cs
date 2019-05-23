using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuro.Net.Template.Processor
{
		public class TemplateMarker
		{
			public object JsonRoot { get; set; }
			Random random = new Random();

			// Sequence of fragment number for each fragment type
			public List<int> Sequence = new List<int>() { 0, 0 };

			public MarkerType LastFragmentType
			{
				get; set;
			}
			public int LastFragmentStart
			{
				get; set;
			}
			public int LastFragmentEnd
			{
				get; set;
			}

			// TemplateManager templateManager = new TemplateManager();

			// CodeFragment ParentFragment { get; set; }

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



			public void OrderFragments(CodeFragment root)
			{
				if (root.Fragments != null)
				{
					for (int i = 0; i < root.Fragments.Count - 2; i++)
					{
						for (int j = i + 1; j < root.Fragments.Count - 1; j++)
						{
							if (root.Fragments[i].SelectionStart > root.Fragments[j].SelectionStart)
							{
								var transport = root.Fragments[i].SelectionStart;
								root.Fragments[i].SelectionStart = root.Fragments[j].SelectionStart;
								root.Fragments[j].SelectionStart = transport;
							}
						}
					}
				}
			}

			public void RestoreParents(CodeFragment codeFragment)
			{
				if (codeFragment.Fragments != null)
				{
					foreach (var fragment in codeFragment.Fragments)
					{
						fragment.Parent = codeFragment;
						RestoreParents(fragment);
					}
				}
			}

			public string Inflate(CodeFragment rootCodeFragment, object v)
			{
				string itemsString = string.Empty;

				string inflated = rootCodeFragment.Content;
				List<List<object>> Fragments = new List<List<object>>();
				if (v is JObject)
				{
					var currentObject = v as JObject;
					rootCodeFragment.FormattedContent = rootCodeFragment.Content;
					for (int i = rootCodeFragment.Fragments.Count - 1; i > -1; i--)
					{
						var childFragment = rootCodeFragment.Fragments[i];
						foreach (var property in currentObject.Children())
						{
							var currentProperty = property as JProperty;
							List<List<string>> stringValues = new List<List<string>>();
							if (childFragment.Name != null && childFragment.Name.Equals(currentProperty.Name))
							{
								var currentValue = currentObject.GetValue(currentProperty.Name);
								string text = Inflate(childFragment, currentValue);
								if (string.IsNullOrEmpty(rootCodeFragment.FormattedContent))
								{
									rootCodeFragment.FormattedContent = rootCodeFragment.Content;
								}

								if (childFragment.SelectionStart - rootCodeFragment.SelectionStart < rootCodeFragment.FormattedContent.Length
									&&
									childFragment.SelectionStart - rootCodeFragment.SelectionStart + childFragment.SelectionLength < rootCodeFragment.FormattedContent.Length
									&&
									childFragment.SelectionStart > rootCodeFragment.SelectionStart
									)
								{
									rootCodeFragment.FormattedContent = rootCodeFragment.FormattedContent.Substring(0, childFragment.SelectionStart - rootCodeFragment.SelectionStart) + text + rootCodeFragment.FormattedContent.Substring(childFragment.SelectionStart - rootCodeFragment.SelectionStart + childFragment.SelectionLength);
								}
								else
								{

								}
							}
						}
						// Fragments.Add(new List<object>() {  });
					}
					return rootCodeFragment.FormattedContent;
				}
				else
				if (v is JArray)
				{
					var currentArray = v as JArray;
					foreach (var item in currentArray)
					{
						if (item is JObject)
						{
							var currentObjectItem = item as JObject;
							Inflate(rootCodeFragment, currentObjectItem);
							itemsString += rootCodeFragment.FormattedContent;
						}
					}
					return itemsString;
				}
				else
				if (v is JProperty)
				{
					foreach (var items in ((JProperty)v).Children())
					{

					}
				}
				if (v is JValue)
				{
					var currentValue = v as JValue;
					return currentValue.ToString();
				}
				return inflated;
			}


			// Create template preview, not accurate!
			public string[] ProcessTemplate(CodeFragment codeFragment)
			{

				string result = string.Empty;
				string formattedResult = string.Empty;
				string inflated = string.Empty;
				if (codeFragment.Type == 1)
				{
					// Is a variable
					result = "@{" + codeFragment.Name + "}@";
					formattedResult = Markers[(int)MarkerType.Replace][0] + codeFragment.Name + Markers[(int)MarkerType.Replace][1];
				}
				else
				{
					result = codeFragment.Content;
					formattedResult = codeFragment.Content;
				}
				if (codeFragment.Fragments != null)
				{
					int Offset = 0;
					for (int i = codeFragment.Fragments.Count - 1; i >= 0; i--)
					// for (int i = 0; i < codeFragment.Fragments.Count; i++)
					{
						var fragment = codeFragment.Fragments[i];
						string[] data = ProcessTemplate(fragment);
						try
						{
							// string processedFragment = data[0];
							result = result.Substring(0, fragment.SelectionStart - codeFragment.SelectionStart + Offset) + data[0] + result.Substring(fragment.SelectionStart + fragment.SelectionLength - codeFragment.SelectionStart + Offset);
							// formattedResult = Escape(result.Substring(0, fragment.SelectionStart - codeFragment.SelectionStart)) + data[1] + Escape(result.Substring(fragment.SelectionStart + fragment.SelectionLength - codeFragment.SelectionStart));
							// Offset += data[0].Length - fragment.SelectionLength;
						}
						catch (Exception yy)
						{

						}
					}
				}
				codeFragment.FormattedContent = result;
				codeFragment.FormattedContent = formattedResult;
				return new string[] { result, formattedResult };
			}

			public object SelectOrCreateJson(CodeFragment currentFragment)
			{
				object Created = null;
				try
				{
					List<CodeFragment> breadcrumbs = new List<CodeFragment>();
					while (currentFragment.Parent != null)
					{
						breadcrumbs.Insert(0, currentFragment);
						currentFragment = currentFragment.Parent;
					}

					object jsonFragment = JsonRoot;
					if (jsonFragment == null)
					{
						jsonFragment = new JObject();
						JsonRoot = jsonFragment;
					}

					foreach (CodeFragment fragment in breadcrumbs)
					{
						switch (fragment.Type)
						{
							case 0:
								if (!((JObject)jsonFragment).ContainsKey(fragment.Name))
								{
									((JObject)jsonFragment).Add(new JProperty(fragment.Name, new JArray()));
								}
								jsonFragment = ((JObject)jsonFragment)[fragment.Name];
								Created = jsonFragment;
								break;
							case 1:
								if (fragment.Parent != null && fragment.Parent.Type == 0)
								{
									if (((JArray)jsonFragment).Count == 0)
									{
										((JArray)jsonFragment).Add(new JObject());
									}
									var i = 0;
									foreach (var item in ((JArray)jsonFragment).Children())
									{
										i++;
										if (!((JObject)item).ContainsKey(fragment.Name))
										{
											((JObject)item).Add(new JProperty(fragment.Name, fragment.Name + "_" + i));
										}
									}
								}
								jsonFragment = ((JArray)jsonFragment)[0];
								jsonFragment = ((JObject)jsonFragment)[fragment.Name];
								Created = jsonFragment;
								break;
						}
					}
				}
				catch (Exception x)
				{

				}
				return Created;
			}

			public void UpdateFromSelection(CodeFragment Root, CodeFragment currentFragment, ref CodeFragment LastFragment, int SelectionStart, int SelectionLength)
			{
				CodeFragment newFragment = new CodeFragment()
				{
					SelectionStart = currentFragment.SelectionStart,
					SelectionLength = currentFragment.SelectionLength,
					Name = currentFragment.Name,
					Type = currentFragment.Type,
					Content = currentFragment.Content,
					Parent = currentFragment
				};
				LastFragment = newFragment;
				newFragment.Color = new int[] { random.Next(55) + 200, random.Next(55) + 200, random.Next(55) + 200 };
				// newFragment.Color = new int[] { random.Next(100) + 10, random.Next(100) + 10, random.Next(100) + 10 };

				if (currentFragment.Fragments == null)
				{
					currentFragment.Fragments = new System.Collections.ObjectModel.ObservableCollection<CodeFragment>();
				}
			currentFragment.Parent = GetInnermostFragment(Root, SelectionStart, SelectionLength);
				newFragment.Parent = currentFragment.Parent;
				if (currentFragment.Parent.Fragments == null)
				{
				currentFragment.Parent.Fragments = new System.Collections.ObjectModel.ObservableCollection<CodeFragment>();
				}
			currentFragment.Parent.Fragments.Add(newFragment);
				currentFragment = newFragment;
			}

			public void UpdateFromSelection2(CodeFragment CurrentFragment)
			{
				// Detect when children are uncovered
				var ChildrenCount = CurrentFragment.Fragments != null ? CurrentFragment.Fragments.Count : 0; // Not sure if number of children is reevaluated in a for loop, just to make sure I can force the for loop to be aware that the number of children might change.
				for (int i = 0; i < ChildrenCount; i++) // I am not using foreach because of a foreach iterration can not be changed inside the foreach.
				{
					var Child = CurrentFragment.Fragments[i]; // Consider each child...
					var Sel = CurrentFragment; // A alias for CurrentFragment just to simplify notations.
											   // ChildrenCount--; // Because there is one less child;

					if (Sel.SelectionStart > Child.SelectionStart || Sel.SelectionStart + Sel.SelectionLength < Child.SelectionStart + Child.SelectionLength) // Detect if the child escaped from under the parent. We might need to also check if the child is partially covered, this check is not needed in case of progressive selection adjustment as the child is uncovered ine character at a time.
					{
						ChildrenCount--;
						Sel.Fragments.Remove(Child); // Make the child a orphan. Not needed in this case but a good practice when a child can not belong to multiple collections (treeView? not sure)
						Child.Parent = Sel.Parent; // Re-asign the orphane to the structure as a sibling to the curent selection.
					var k = 0;
					foreach (var f in Sel.Fragments)
					{
						if (f != Child && f.SelectionStart + f.SelectionLength < Child.SelectionStart)
						{
							k++;
						}
					}
					Sel.Parent.Fragments.Insert(k, Child);
						if (Sel.SelectionStart > Child.SelectionStart) // In case of uncovering the left interval margin, the left margin of CurrentFragment is not allowed to partially overlap it's sibling (former child).
						{
							Sel.SelectionStart = Child.SelectionStart + Child.SelectionLength; // If selection is complete, this will be be sufficient to make sure the new interval does not partially cover it's sibling
																							   // If selection continues to be adjusted. A check against each siblings has to be reevaluated because with each adjustment the SelectionStart will be set to richTextBoxParentCode.SelectionStart;
						}
						if (Sel.SelectionStart + Sel.SelectionLength < Child.SelectionStart + Child.SelectionLength) // In case of uncovering the right interval margin, the right margin of CurrentFragment is not allowed to partially overlap it's sibling (former child).
						{
							Sel.SelectionStart = Child.SelectionStart + Child.SelectionLength; // If selection is complete, this will be be sufficient to make sure the new interval does not partially cover it's sibling
																							   // If selection continues to be adjusted. A check against each siblings has to be reevaluated because with each adjustment the SelectionLength will be set to richTextBoxParentCode.SelectionLength;
						}
						// Above algorithm should be tested for selections that progress to the left and to the right of their starting point or adjustment that jmps over their starting point.

						// Check for duplicates
						if (Sel.SelectionStart > Child.SelectionStart && Sel.SelectionStart + Sel.SelectionLength < Child.SelectionStart + Child.SelectionLength)
						{
							// TODO: Only drop this child if detected after selection completed, so can not be dropped here, should be  before CurrentFragment.Fragments.Add(newFragment);
						}
					}
				}

				// Detect if siblings are fully covered
				var SiblingsCount = CurrentFragment.Parent != null && CurrentFragment.Parent.Fragments != null ? CurrentFragment.Parent.Fragments.Count : 0;
				for (int i = 0; i < SiblingsCount; i++)
				{
					var Sib = CurrentFragment.Parent.Fragments[i]; // Consider each child...
					var Sel = CurrentFragment; // A alias for CurrentFragment just to simplify notations.		
					if (Sel == Sib)
					{
						continue; // Make sure we don't test the current interval against itself
					}

					if (Sel.SelectionStart <= Sib.SelectionStart && Sel.SelectionStart + Sel.SelectionLength >= Sib.SelectionStart + Sib.SelectionLength) // Detect if a sibling gets fully covered
					{
					// While adjusting the selection, there might be cases when for a short time the selection has exact same limits as a existing sibling. In this case the sibling become a child. But it will be dropped if the selection is completed because no duplicates are allowed.

					CurrentFragment.Parent.Fragments.Remove(Sib); // Remove from siblings
						SiblingsCount--;
					i--;
						Sib.Parent = Sel; // Re-asign the orphane to the structure as a sibling to the curent selection.
						if (Sel.Fragments == null)
						{
							Sel.Fragments = new System.Collections.ObjectModel.ObservableCollection<CodeFragment>();
						}
					var k = 0;
					foreach (var f in Sel.Fragments)
					{
						if (f != Sib && f.SelectionStart + f.SelectionLength < Sib.SelectionStart)
						{
							k++;
						}
					}
					Sel.Fragments.Insert(k, Sib); // Re-asign the orphane to the structure as a child to the curent selection.
					}
				}
			}

			public CodeFragment GetInnermostFragment(CodeFragment root, int selectionStart, int SelectionLength)
			{
				foreach (var fragment in root.Fragments)
				{
					if (fragment.Fragments != null)
					{
						if (fragment.SelectionStart <= selectionStart && fragment.SelectionStart + fragment.SelectionLength >= selectionStart + SelectionLength)
						{
							return GetInnermostFragment(fragment, selectionStart, SelectionLength);
						}
					}
				}
				return root;
			}
		}

		public class CodeFragment : System.ComponentModel.INotifyPropertyChanged
		{
			[JsonIgnore]
			public CodeFragment Parent { get; set; }
			[JsonIgnore]
			public object TreeViewItem { get; set; }

			public int Type { get; set; }
			private string name { get; set; }
			public string Name
			{
				get
				{
					return name;
				}

				set
				{
					if (name != value)
					{
						name = value;
						NotifyPropertyChanged();
					}
				}
			}
			private string generator { get; set; }
			public string Generator
			{
				get
				{
					return generator;
				}

				set
				{
					if (generator != value)
					{
						generator = value;
						NotifyPropertyChanged();
					}
				}
			}

			public int SelectionStart { get; set; }
			public int SelectionLength { get; set; }
			private string content { get; set; }
			public string Content
			{
				get
				{
					return content;
				}

				set
				{
					if (content != value)
					{
						content = value;
						NotifyPropertyChanged();
					}
				}
			}
			public string Inflated { get; set; }
			public string FormattedContent { get; set; }
			public int[] Color { get; set; }

			private System.Collections.ObjectModel.ObservableCollection<CodeFragment> fragments { get; set; }
			public System.Collections.ObjectModel.ObservableCollection<CodeFragment> Fragments
			{
				get
				{
					return fragments;
				}
				set
				{
					fragments = value;
					fragments.CollectionChanged += ContentCollectionChanged;
				}
			}

			public CodeFragment()
			{
				fragments = new System.Collections.ObjectModel.ObservableCollection<CodeFragment>();
				fragments.CollectionChanged += ContentCollectionChanged;
			}

			public void ContentCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
			{
				//This will get called when the collection is changed
			}

			public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
			private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
			{
				PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}