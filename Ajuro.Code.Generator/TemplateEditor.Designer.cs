namespace Ajuro.Notes.Markup
{
	partial class TemplateEditor
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.richTextBoxParentCode = new System.Windows.Forms.RichTextBox();
			this.richTextBoxChildCode = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.RichTextBoxCodeStructurePreview = new System.Windows.Forms.RichTextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.FragmentNameTextBox = new System.Windows.Forms.TextBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
			this.radioButtonRepeat = new System.Windows.Forms.RadioButton();
			this.radioButtonReplace = new System.Windows.Forms.RadioButton();
			this.groupBoxFragmentType = new System.Windows.Forms.GroupBox();
			this.buttonReset = new System.Windows.Forms.Button();
			this.SelectionFromTextBox = new System.Windows.Forms.TextBox();
			this.SelectionLengthTextBox = new System.Windows.Forms.TextBox();
			this.RealTimeUpdatesCheckBox = new System.Windows.Forms.CheckBox();
			this.FragmentsTreeView = new System.Windows.Forms.TreeView();
			this.richTextBoxJsonData = new System.Windows.Forms.RichTextBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.richTextBoxInflated = new System.Windows.Forms.RichTextBox();
			this.textBoxType = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControlContent = new System.Windows.Forms.TabControl();
			this.tabPageFiles = new System.Windows.Forms.TabPage();
			this.richTextBoxTemplateConfig = new System.Windows.Forms.RichTextBox();
			this.textBoxTemplatesFilter = new System.Windows.Forms.TextBox();
			this.listBoxTemplates = new System.Windows.Forms.ListBox();
			this.tabPageTemplate = new System.Windows.Forms.TabPage();
			this.tabPageAppConfig = new System.Windows.Forms.TabPage();
			this.richTextBoxAppConfig = new System.Windows.Forms.RichTextBox();
			this.buttonSaveTemplateFiles = new System.Windows.Forms.Button();
			this.buttonSaveOriginalText = new System.Windows.Forms.Button();
			this.buttonSaveTemplate = new System.Windows.Forms.Button();
			this.buttonSaveData = new System.Windows.Forms.Button();
			this.buttonSaveResult = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBoxFragmentType.SuspendLayout();
			this.tabControlContent.SuspendLayout();
			this.tabPageFiles.SuspendLayout();
			this.tabPageTemplate.SuspendLayout();
			this.tabPageAppConfig.SuspendLayout();
			this.SuspendLayout();
			// 
			// richTextBoxParentCode
			// 
			this.richTextBoxParentCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxParentCode.Location = new System.Drawing.Point(3, 6);
			this.richTextBoxParentCode.Name = "richTextBoxParentCode";
			this.richTextBoxParentCode.Size = new System.Drawing.Size(626, 693);
			this.richTextBoxParentCode.TabIndex = 0;
			this.richTextBoxParentCode.Text = "";
			this.richTextBoxParentCode.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
			this.richTextBoxParentCode.Click += new System.EventHandler(this.richTextBoxParentCode_Click);
			this.richTextBoxParentCode.TextChanged += new System.EventHandler(this.richTextBoxParentCode_TextChanged);
			this.richTextBoxParentCode.DoubleClick += new System.EventHandler(this.richTextBoxParentCode_DoubleClick);
			this.richTextBoxParentCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBoxParentCode_KeyUp);
			this.richTextBoxParentCode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBoxParentCode_MouseUp);
			// 
			// richTextBoxChildCode
			// 
			this.richTextBoxChildCode.Location = new System.Drawing.Point(674, 206);
			this.richTextBoxChildCode.Name = "richTextBoxChildCode";
			this.richTextBoxChildCode.Size = new System.Drawing.Size(626, 294);
			this.richTextBoxChildCode.TabIndex = 2;
			this.richTextBoxChildCode.Text = "";
			this.richTextBoxChildCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RichTextBoxChildCode_KeyUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(664, 119);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 25);
			this.label3.TabIndex = 4;
			this.label3.Text = "Name:";
			// 
			// RichTextBoxCodeStructurePreview
			// 
			this.RichTextBoxCodeStructurePreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.RichTextBoxCodeStructurePreview.Location = new System.Drawing.Point(1323, 86);
			this.RichTextBoxCodeStructurePreview.Name = "RichTextBoxCodeStructurePreview";
			this.RichTextBoxCodeStructurePreview.Size = new System.Drawing.Size(145, 752);
			this.RichTextBoxCodeStructurePreview.TabIndex = 6;
			this.RichTextBoxCodeStructurePreview.Text = "";
			this.RichTextBoxCodeStructurePreview.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(1318, 33);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(162, 25);
			this.label5.TabIndex = 7;
			this.label5.Text = "Selections Tree";
			// 
			// FragmentNameTextBox
			// 
			this.FragmentNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FragmentNameTextBox.Location = new System.Drawing.Point(746, 106);
			this.FragmentNameTextBox.Name = "FragmentNameTextBox";
			this.FragmentNameTextBox.Size = new System.Drawing.Size(286, 44);
			this.FragmentNameTextBox.TabIndex = 8;
			this.FragmentNameTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FragmentNameTextBox_KeyUp);
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonSave.Location = new System.Drawing.Point(1092, 777);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(208, 56);
			this.buttonSave.TabIndex = 10;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// webBrowserPreview
			// 
			this.webBrowserPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowserPreview.Location = new System.Drawing.Point(1748, 616);
			this.webBrowserPreview.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowserPreview.Name = "webBrowserPreview";
			this.webBrowserPreview.Size = new System.Drawing.Size(1621, 220);
			this.webBrowserPreview.TabIndex = 12;
			// 
			// radioButtonRepeat
			// 
			this.radioButtonRepeat.AutoSize = true;
			this.radioButtonRepeat.ForeColor = System.Drawing.Color.Maroon;
			this.radioButtonRepeat.Location = new System.Drawing.Point(6, 30);
			this.radioButtonRepeat.Name = "radioButtonRepeat";
			this.radioButtonRepeat.Size = new System.Drawing.Size(112, 29);
			this.radioButtonRepeat.TabIndex = 13;
			this.radioButtonRepeat.TabStop = true;
			this.radioButtonRepeat.Text = "Repeat";
			this.radioButtonRepeat.UseVisualStyleBackColor = true;
			// 
			// radioButtonReplace
			// 
			this.radioButtonReplace.AutoSize = true;
			this.radioButtonReplace.ForeColor = System.Drawing.Color.Navy;
			this.radioButtonReplace.Location = new System.Drawing.Point(124, 30);
			this.radioButtonReplace.Name = "radioButtonReplace";
			this.radioButtonReplace.Size = new System.Drawing.Size(122, 29);
			this.radioButtonReplace.TabIndex = 14;
			this.radioButtonReplace.TabStop = true;
			this.radioButtonReplace.Text = "Replace";
			this.radioButtonReplace.UseVisualStyleBackColor = true;
			// 
			// groupBoxFragmentType
			// 
			this.groupBoxFragmentType.Controls.Add(this.radioButtonRepeat);
			this.groupBoxFragmentType.Controls.Add(this.radioButtonReplace);
			this.groupBoxFragmentType.Location = new System.Drawing.Point(1036, 86);
			this.groupBoxFragmentType.Name = "groupBoxFragmentType";
			this.groupBoxFragmentType.Size = new System.Drawing.Size(264, 77);
			this.groupBoxFragmentType.TabIndex = 15;
			this.groupBoxFragmentType.TabStop = false;
			this.groupBoxFragmentType.Text = "Type:";
			// 
			// buttonReset
			// 
			this.buttonReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.buttonReset.ForeColor = System.Drawing.Color.Maroon;
			this.buttonReset.Location = new System.Drawing.Point(1743, 23);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(83, 56);
			this.buttonReset.TabIndex = 16;
			this.buttonReset.Text = "Clear";
			this.buttonReset.UseVisualStyleBackColor = false;
			this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
			// 
			// SelectionFromTextBox
			// 
			this.SelectionFromTextBox.Location = new System.Drawing.Point(1042, 36);
			this.SelectionFromTextBox.Name = "SelectionFromTextBox";
			this.SelectionFromTextBox.Size = new System.Drawing.Size(100, 31);
			this.SelectionFromTextBox.TabIndex = 17;
			// 
			// SelectionLengthTextBox
			// 
			this.SelectionLengthTextBox.Location = new System.Drawing.Point(1149, 36);
			this.SelectionLengthTextBox.Name = "SelectionLengthTextBox";
			this.SelectionLengthTextBox.Size = new System.Drawing.Size(100, 31);
			this.SelectionLengthTextBox.TabIndex = 18;
			// 
			// RealTimeUpdatesCheckBox
			// 
			this.RealTimeUpdatesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RealTimeUpdatesCheckBox.AutoSize = true;
			this.RealTimeUpdatesCheckBox.Location = new System.Drawing.Point(674, 793);
			this.RealTimeUpdatesCheckBox.Name = "RealTimeUpdatesCheckBox";
			this.RealTimeUpdatesCheckBox.Size = new System.Drawing.Size(215, 29);
			this.RealTimeUpdatesCheckBox.TabIndex = 19;
			this.RealTimeUpdatesCheckBox.Text = "RealTimeUpdates";
			this.RealTimeUpdatesCheckBox.UseVisualStyleBackColor = true;
			// 
			// FragmentsTreeView
			// 
			this.FragmentsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.FragmentsTreeView.Location = new System.Drawing.Point(1323, 89);
			this.FragmentsTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.FragmentsTreeView.Name = "FragmentsTreeView";
			this.FragmentsTreeView.Size = new System.Drawing.Size(418, 749);
			this.FragmentsTreeView.TabIndex = 20;
			this.FragmentsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FragmentsTreeView_AfterSelect);
			this.FragmentsTreeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FragmentsTreeView_KeyUp);
			// 
			// richTextBoxJsonData
			// 
			this.richTextBoxJsonData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.richTextBoxJsonData.Location = new System.Drawing.Point(675, 506);
			this.richTextBoxJsonData.Name = "richTextBoxJsonData";
			this.richTextBoxJsonData.Size = new System.Drawing.Size(625, 265);
			this.richTextBoxJsonData.TabIndex = 21;
			this.richTextBoxJsonData.Text = "";
			this.richTextBoxJsonData.TextChanged += new System.EventHandler(this.JsonEditorRichTextBox_TextChanged);
			this.richTextBoxJsonData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.JsonEditorRichTextBox_KeyUp);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(1974, 23);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(122, 74);
			this.richTextBox1.TabIndex = 22;
			this.richTextBox1.Text = "";
			this.richTextBox1.Visible = false;
			// 
			// richTextBoxInflated
			// 
			this.richTextBoxInflated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.richTextBoxInflated.Location = new System.Drawing.Point(1748, 89);
			this.richTextBoxInflated.Name = "richTextBoxInflated";
			this.richTextBoxInflated.Size = new System.Drawing.Size(1510, 521);
			this.richTextBoxInflated.TabIndex = 23;
			this.richTextBoxInflated.Text = "";
			this.richTextBoxInflated.TextChanged += new System.EventHandler(this.richTextBoxInflated_TextChanged);
			// 
			// textBoxType
			// 
			this.textBoxType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxType.Location = new System.Drawing.Point(746, 156);
			this.textBoxType.Name = "textBoxType";
			this.textBoxType.Size = new System.Drawing.Size(286, 44);
			this.textBoxType.TabIndex = 24;
			this.textBoxType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxType_KeyUp);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(669, 168);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 25);
			this.label2.TabIndex = 25;
			this.label2.Text = "Type:";
			// 
			// tabControlContent
			// 
			this.tabControlContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tabControlContent.Controls.Add(this.tabPageFiles);
			this.tabControlContent.Controls.Add(this.tabPageTemplate);
			this.tabControlContent.Controls.Add(this.tabPageAppConfig);
			this.tabControlContent.Location = new System.Drawing.Point(12, 86);
			this.tabControlContent.Name = "tabControlContent";
			this.tabControlContent.SelectedIndex = 0;
			this.tabControlContent.Size = new System.Drawing.Size(651, 752);
			this.tabControlContent.TabIndex = 26;
			// 
			// tabPageFiles
			// 
			this.tabPageFiles.Controls.Add(this.richTextBoxTemplateConfig);
			this.tabPageFiles.Controls.Add(this.textBoxTemplatesFilter);
			this.tabPageFiles.Controls.Add(this.listBoxTemplates);
			this.tabPageFiles.Location = new System.Drawing.Point(8, 39);
			this.tabPageFiles.Name = "tabPageFiles";
			this.tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFiles.Size = new System.Drawing.Size(635, 705);
			this.tabPageFiles.TabIndex = 0;
			this.tabPageFiles.Text = "Files";
			this.tabPageFiles.UseVisualStyleBackColor = true;
			// 
			// richTextBoxTemplateConfig
			// 
			this.richTextBoxTemplateConfig.Location = new System.Drawing.Point(6, 6);
			this.richTextBoxTemplateConfig.Name = "richTextBoxTemplateConfig";
			this.richTextBoxTemplateConfig.Size = new System.Drawing.Size(620, 377);
			this.richTextBoxTemplateConfig.TabIndex = 2;
			this.richTextBoxTemplateConfig.Text = "";
			this.richTextBoxTemplateConfig.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBoxTemplateConfig_KeyUp);
			// 
			// textBoxTemplatesFilter
			// 
			this.textBoxTemplatesFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxTemplatesFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxTemplatesFilter.Location = new System.Drawing.Point(3, 389);
			this.textBoxTemplatesFilter.Name = "textBoxTemplatesFilter";
			this.textBoxTemplatesFilter.Size = new System.Drawing.Size(623, 50);
			this.textBoxTemplatesFilter.TabIndex = 1;
			this.textBoxTemplatesFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxTemplatesFilter_KeyUp);
			this.textBoxTemplatesFilter.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxTemplatesFilter_MouseDoubleClick);
			// 
			// listBoxTemplates
			// 
			this.listBoxTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listBoxTemplates.FormattingEnabled = true;
			this.listBoxTemplates.ItemHeight = 25;
			this.listBoxTemplates.Location = new System.Drawing.Point(6, 445);
			this.listBoxTemplates.Name = "listBoxTemplates";
			this.listBoxTemplates.Size = new System.Drawing.Size(623, 254);
			this.listBoxTemplates.TabIndex = 0;
			this.listBoxTemplates.SelectedIndexChanged += new System.EventHandler(this.listBoxTemplates_SelectedIndexChanged);
			this.listBoxTemplates.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBoxTemplates_KeyUp);
			// 
			// tabPageTemplate
			// 
			this.tabPageTemplate.Controls.Add(this.richTextBoxParentCode);
			this.tabPageTemplate.Location = new System.Drawing.Point(8, 39);
			this.tabPageTemplate.Name = "tabPageTemplate";
			this.tabPageTemplate.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTemplate.Size = new System.Drawing.Size(635, 705);
			this.tabPageTemplate.TabIndex = 1;
			this.tabPageTemplate.Text = "Template";
			this.tabPageTemplate.UseVisualStyleBackColor = true;
			// 
			// tabPageAppConfig
			// 
			this.tabPageAppConfig.Controls.Add(this.richTextBoxAppConfig);
			this.tabPageAppConfig.Location = new System.Drawing.Point(8, 39);
			this.tabPageAppConfig.Name = "tabPageAppConfig";
			this.tabPageAppConfig.Size = new System.Drawing.Size(635, 705);
			this.tabPageAppConfig.TabIndex = 3;
			this.tabPageAppConfig.Text = "AppConfig";
			this.tabPageAppConfig.UseVisualStyleBackColor = true;
			// 
			// richTextBoxAppConfig
			// 
			this.richTextBoxAppConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxAppConfig.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxAppConfig.Name = "richTextBoxAppConfig";
			this.richTextBoxAppConfig.Size = new System.Drawing.Size(632, 697);
			this.richTextBoxAppConfig.TabIndex = 0;
			this.richTextBoxAppConfig.Text = "";
			this.richTextBoxAppConfig.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBoxAppConfig_KeyUp);
			// 
			// buttonSaveTemplateFiles
			// 
			this.buttonSaveTemplateFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.buttonSaveTemplateFiles.ForeColor = System.Drawing.Color.Maroon;
			this.buttonSaveTemplateFiles.Location = new System.Drawing.Point(655, 33);
			this.buttonSaveTemplateFiles.Name = "buttonSaveTemplateFiles";
			this.buttonSaveTemplateFiles.Size = new System.Drawing.Size(83, 56);
			this.buttonSaveTemplateFiles.TabIndex = 27;
			this.buttonSaveTemplateFiles.Text = "Save";
			this.buttonSaveTemplateFiles.UseVisualStyleBackColor = false;
			this.buttonSaveTemplateFiles.Click += new System.EventHandler(this.buttonSaveTemplateFiles_Click);
			// 
			// buttonSaveOriginalText
			// 
			this.buttonSaveOriginalText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.buttonSaveOriginalText.ForeColor = System.Drawing.Color.Maroon;
			this.buttonSaveOriginalText.Location = new System.Drawing.Point(744, 32);
			this.buttonSaveOriginalText.Name = "buttonSaveOriginalText";
			this.buttonSaveOriginalText.Size = new System.Drawing.Size(52, 56);
			this.buttonSaveOriginalText.TabIndex = 28;
			this.buttonSaveOriginalText.Text = "O";
			this.buttonSaveOriginalText.UseVisualStyleBackColor = false;
			this.buttonSaveOriginalText.Click += new System.EventHandler(this.buttonSaveOriginalText_Click);
			// 
			// buttonSaveTemplate
			// 
			this.buttonSaveTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.buttonSaveTemplate.ForeColor = System.Drawing.Color.Maroon;
			this.buttonSaveTemplate.Location = new System.Drawing.Point(802, 31);
			this.buttonSaveTemplate.Name = "buttonSaveTemplate";
			this.buttonSaveTemplate.Size = new System.Drawing.Size(52, 56);
			this.buttonSaveTemplate.TabIndex = 29;
			this.buttonSaveTemplate.Text = "T";
			this.buttonSaveTemplate.UseVisualStyleBackColor = false;
			this.buttonSaveTemplate.Click += new System.EventHandler(this.buttonSaveTemplate_Click);
			// 
			// buttonSaveData
			// 
			this.buttonSaveData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.buttonSaveData.ForeColor = System.Drawing.Color.Maroon;
			this.buttonSaveData.Location = new System.Drawing.Point(860, 31);
			this.buttonSaveData.Name = "buttonSaveData";
			this.buttonSaveData.Size = new System.Drawing.Size(52, 56);
			this.buttonSaveData.TabIndex = 30;
			this.buttonSaveData.Text = "D";
			this.buttonSaveData.UseVisualStyleBackColor = false;
			this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
			// 
			// buttonSaveResult
			// 
			this.buttonSaveResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.buttonSaveResult.ForeColor = System.Drawing.Color.Maroon;
			this.buttonSaveResult.Location = new System.Drawing.Point(918, 31);
			this.buttonSaveResult.Name = "buttonSaveResult";
			this.buttonSaveResult.Size = new System.Drawing.Size(52, 56);
			this.buttonSaveResult.TabIndex = 31;
			this.buttonSaveResult.Text = "R";
			this.buttonSaveResult.UseVisualStyleBackColor = false;
			this.buttonSaveResult.Click += new System.EventHandler(this.buttonSaveResult_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3290, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(79, 46);
			this.button1.TabIndex = 32;
			this.button1.Text = "Help";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// TemplateEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(3381, 848);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.buttonSaveResult);
			this.Controls.Add(this.buttonSaveData);
			this.Controls.Add(this.buttonSaveTemplate);
			this.Controls.Add(this.buttonSaveOriginalText);
			this.Controls.Add(this.buttonSaveTemplateFiles);
			this.Controls.Add(this.tabControlContent);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxType);
			this.Controls.Add(this.richTextBoxInflated);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.richTextBoxJsonData);
			this.Controls.Add(this.FragmentsTreeView);
			this.Controls.Add(this.RealTimeUpdatesCheckBox);
			this.Controls.Add(this.SelectionLengthTextBox);
			this.Controls.Add(this.SelectionFromTextBox);
			this.Controls.Add(this.buttonReset);
			this.Controls.Add(this.groupBoxFragmentType);
			this.Controls.Add(this.webBrowserPreview);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.FragmentNameTextBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.RichTextBoxCodeStructurePreview);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.richTextBoxChildCode);
			this.Name = "TemplateEditor";
			this.Text = "Ajuro.Code.Generator - OTB Expert";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.groupBoxFragmentType.ResumeLayout(false);
			this.groupBoxFragmentType.PerformLayout();
			this.tabControlContent.ResumeLayout(false);
			this.tabPageFiles.ResumeLayout(false);
			this.tabPageFiles.PerformLayout();
			this.tabPageTemplate.ResumeLayout(false);
			this.tabPageAppConfig.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBoxParentCode;
		private System.Windows.Forms.RichTextBox richTextBoxChildCode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox RichTextBoxCodeStructurePreview;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox FragmentNameTextBox;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.WebBrowser webBrowserPreview;
		private System.Windows.Forms.RadioButton radioButtonRepeat;
		private System.Windows.Forms.RadioButton radioButtonReplace;
		private System.Windows.Forms.GroupBox groupBoxFragmentType;
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.TextBox SelectionFromTextBox;
		private System.Windows.Forms.TextBox SelectionLengthTextBox;
		private System.Windows.Forms.CheckBox RealTimeUpdatesCheckBox;
        private System.Windows.Forms.TreeView FragmentsTreeView;
		private System.Windows.Forms.RichTextBox richTextBoxJsonData;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.RichTextBox richTextBoxInflated;
		private System.Windows.Forms.TextBox textBoxType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabControl tabControlContent;
		private System.Windows.Forms.TabPage tabPageFiles;
		private System.Windows.Forms.TextBox textBoxTemplatesFilter;
		private System.Windows.Forms.ListBox listBoxTemplates;
		private System.Windows.Forms.TabPage tabPageTemplate;
		private System.Windows.Forms.TabPage tabPageAppConfig;
		private System.Windows.Forms.RichTextBox richTextBoxAppConfig;
		private System.Windows.Forms.RichTextBox richTextBoxTemplateConfig;
		private System.Windows.Forms.Button buttonSaveTemplateFiles;
		private System.Windows.Forms.Button buttonSaveOriginalText;
		private System.Windows.Forms.Button buttonSaveTemplate;
		private System.Windows.Forms.Button buttonSaveData;
		private System.Windows.Forms.Button buttonSaveResult;
		private System.Windows.Forms.Button button1;
	}
}

