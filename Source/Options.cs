using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argumentative
	// namespace arg_options
{
	/// <summary>
	/// Summary description for Options.
	/// </summary>
	public class Options : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.Button buttonSetFont;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label labelTreeViewSample;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabPage tabGraphical;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.GroupBox groupBoxPremise;
		private System.Windows.Forms.Label labelPremiseSample;
		private System.Windows.Forms.Button buttonSetPremiseFont;
		private System.Windows.Forms.Button buttonSetPremiseBoxColour;

		// Objects to be modified
		private System.Windows.Forms.NumericUpDown premiseLineWidth;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton JustifyCentre;
		private System.Windows.Forms.RadioButton JustifyLeft;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonStraight;
		private System.Windows.Forms.RadioButton radioButtonDogleg;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBoxReason;
		private System.Windows.Forms.NumericUpDown reasonLineWidth;
		private System.Windows.Forms.Button buttonSetReasonBoxColour;
		private System.Windows.Forms.Label labelReasonSample;
		private System.Windows.Forms.Button buttonSetReasonFont;
		private System.Windows.Forms.GroupBox groupBoxObjection;
		private System.Windows.Forms.NumericUpDown objectionLineWidth;
		private System.Windows.Forms.Label labelObjectionSample;
		private System.Windows.Forms.GroupBox groupBoxHelper;
		private System.Windows.Forms.NumericUpDown helperLineWidth;
		private System.Windows.Forms.Label labelHelperSample;
		private System.Windows.Forms.Button buttonSetObjectionBoxColour;
		private System.Windows.Forms.Button buttonSetObjectionFont;
		private System.Windows.Forms.Button buttonSetHelperBoxColour;
		private System.Windows.Forms.Button buttonSetHelperFont;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton radioButtonTopDown;
		private System.Windows.Forms.RadioButton radioButtonBottomUp;
		private System.Windows.Forms.RadioButton radioButtonLeftRight;
		private System.Windows.Forms.RadioButton radioButtonRightLeft;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton radioButtonTextBoxes;
		private System.Windows.Forms.RadioButton radioButtonRnotation;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.CheckBox checkBoxArrowStart;
		private System.Windows.Forms.CheckBox checkBoxArrowEnd;
		private System.Windows.Forms.CheckBox checkBoxDropShadow;
		private System.Windows.Forms.CheckBox checkBoxShading;
		private System.Windows.Forms.RadioButton radioButtonCurve;
		private System.Windows.Forms.TabPage tabGraphical2;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDownRecentFiles;
		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxAuthor;
		private System.Windows.Forms.Label Date;
		private System.Windows.Forms.HelpProvider helpProviderOptions;
		private System.Windows.Forms.CheckBox checkBoxToday;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ListBox listBoxReasonLineStyle;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ListBox listBoxObjectionLineStyle;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ListBox listBoxHelperLineStyle;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.GroupBox groupBox11;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ComboBox comboBoxDictionary;
		private System.Windows.Forms.RadioButton JustifyRight;

		/// <summary>
		/// Initialise options subsystem
		/// </summary>
		/// <param name="tv">Associated tree view</param>
		public Options(TreeView tv)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			theTree = tv;
			
			opts = OptionsData.Instance;
			// set controls
			// set general tab
			labelTreeViewSample.BackColor=tv.BackColor;
			labelTreeViewSample.ForeColor=tv.ForeColor;
			labelTreeViewSample.Font = tv.Font;
			currentFont = labelTreeViewSample.Font;
			currentColour = labelTreeViewSample.ForeColor;
			
			this.numericUpDownRecentFiles.Value = opts.RecentFiles;
			this.textBoxAuthor.Text = opts.Author;
			if(opts.Author.Equals(""))textBoxAuthor.Text = System.Environment.UserName;
			this.comboBoxDictionary.Items.AddRange(new object[] {
			                                       	"Australian",
			                                       	"Spanish",
			                                       	"US"  });
			if(opts.Dictionary.Equals("en-AU.dic"))
				comboBoxDictionary.SelectedIndex=0;
			else if(opts.Dictionary.Equals("es-ES.dic"))
				comboBoxDictionary.SelectedIndex=1;
			else if(opts.Dictionary.Equals("en-US.dic"))
				comboBoxDictionary.SelectedIndex=2;

			// set tree font
			
			
			// set Premise label sample
			premiseLineWidth.Value = opts.premise.lineWidth;
			setSampleLabel(labelPremiseSample,premiseLineWidth,premiseMaskedTextBox,opts.premise);
			setSampleLabel(labelReasonSample,reasonLineWidth,reasonMaskedTextBox,opts.reason);
			setSampleLabel(labelObjectionSample,objectionLineWidth,objectionMaskedTextBox,opts.objection);
			setSampleLabel(labelHelperSample,helperLineWidth,helperMaskedTextBox,opts.helper);

			// Set line style list boxes
			this.listBoxReasonLineStyle.SelectedIndex = getListBoxIndex(opts.reason.lineStyle);
			this.listBoxObjectionLineStyle.SelectedIndex = getListBoxIndex(opts.objection.lineStyle);
			this.listBoxHelperLineStyle.SelectedIndex = getListBoxIndex(opts.helper.lineStyle);

			if(opts.Justification == DrawTree.justificationType.jCentre)
				JustifyCentre.Checked = true;
			if(opts.Justification == DrawTree.justificationType.jLeft)
				JustifyLeft.Checked = true;
			if(opts.Justification == DrawTree.justificationType.jRight)
				JustifyRight.Checked = true;

			if(opts.Join == DrawTree.joinType.direct)
				radioButtonStraight.Checked = true;
			else if(opts.Join == DrawTree.joinType.dogleg)
				radioButtonDogleg.Checked = true;
			else
				this.radioButtonCurve.Checked = true;
			

			premise = opts.premise.uglyCopy(opts.premise);
			reason = opts.reason.uglyCopy(opts.reason);
			objection = opts.objection.uglyCopy(opts.objection);
			helper = opts.helper.uglyCopy(opts.helper);

			// set orientation radio button
			if(opts.TreeOrientation == DrawTree.treeOrientationType.top_down)
				this.radioButtonTopDown.Checked=true;
			else if(opts.TreeOrientation == DrawTree.treeOrientationType.bottom_up)
				this.radioButtonBottomUp.Checked=true;
			else if(opts.TreeOrientation == DrawTree.treeOrientationType.left_to_right)
				this.radioButtonLeftRight.Checked=true;
			else if(opts.TreeOrientation == DrawTree.treeOrientationType.right_to_left)
				this.radioButtonRightLeft.Checked=true;

			if(opts.Notation == DrawTree.notationType.boxes)
				this.radioButtonTextBoxes.Checked = true;
			else
				this.radioButtonRnotation.Checked = true;

			this.checkBoxShading.Checked = opts.BoxShading;
			this.checkBoxDropShadow.Checked = opts.DropShadow;
			this.checkBoxDropShadow.Enabled = this.checkBoxShading.Checked;
			this.checkBoxShowLegend.Checked = opts.ShowLegend;
			this.checkBoxDistributed.Checked = opts.Distributed;
			this.checkBoxHelpersAsCoPremise.Checked = opts.HelpersAsCoPremises;
			
			if(opts.Marker == DrawTree.markerType.none)
				comboBoxMarker.SelectedIndex = 0;
			else if(opts.Marker == DrawTree.markerType.lines)
				comboBoxMarker.SelectedIndex = 1;
			else if(opts.Marker == DrawTree.markerType.letters)
				comboBoxMarker.SelectedIndex = 2;

			if(opts.Arrow == DrawTree.arrowType.start | opts.Arrow == DrawTree.arrowType.both)
				this.checkBoxArrowStart.Checked = true;
			if(opts.Arrow == DrawTree.arrowType.end | opts.Arrow == DrawTree.arrowType.both)
				this.checkBoxArrowEnd.Checked = true;
		}
		// data that may change
		TreeView theTree;
		OptionsData opts;
		
		ElementOptions premise,reason,objection,helper;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.groupBox11 = new System.Windows.Forms.GroupBox();
			this.comboBoxDictionary = new System.Windows.Forms.ComboBox();
			this.label16 = new System.Windows.Forms.Label();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.checkBoxToday = new System.Windows.Forms.CheckBox();
			this.textBoxAuthor = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.Date = new System.Windows.Forms.Label();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.numericUpDownRecentFiles = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelTreeViewSample = new System.Windows.Forms.Label();
			this.buttonSetFont = new System.Windows.Forms.Button();
			this.tabGraphical = new System.Windows.Forms.TabPage();
			this.groupBoxHelper = new System.Windows.Forms.GroupBox();
			this.label17 = new System.Windows.Forms.Label();
			this.helperMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.helperLineWidth = new System.Windows.Forms.NumericUpDown();
			this.buttonSetHelperBoxColour = new System.Windows.Forms.Button();
			this.labelHelperSample = new System.Windows.Forms.Label();
			this.buttonSetHelperFont = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.listBoxHelperLineStyle = new System.Windows.Forms.ListBox();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBoxObjection = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.objectionMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.objectionLineWidth = new System.Windows.Forms.NumericUpDown();
			this.buttonSetObjectionBoxColour = new System.Windows.Forms.Button();
			this.labelObjectionSample = new System.Windows.Forms.Label();
			this.buttonSetObjectionFont = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.listBoxObjectionLineStyle = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBoxReason = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listBoxReasonLineStyle = new System.Windows.Forms.ListBox();
			this.reasonMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.reasonLineWidth = new System.Windows.Forms.NumericUpDown();
			this.buttonSetReasonBoxColour = new System.Windows.Forms.Button();
			this.labelReasonSample = new System.Windows.Forms.Label();
			this.buttonSetReasonFont = new System.Windows.Forms.Button();
			this.groupBoxPremise = new System.Windows.Forms.GroupBox();
			this.premiseMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.premiseLineWidth = new System.Windows.Forms.NumericUpDown();
			this.buttonSetPremiseBoxColour = new System.Windows.Forms.Button();
			this.labelPremiseSample = new System.Windows.Forms.Label();
			this.buttonSetPremiseFont = new System.Windows.Forms.Button();
			this.tabGraphical2 = new System.Windows.Forms.TabPage();
			this.checkBoxExpansion = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.comboBoxMarker = new System.Windows.Forms.ComboBox();
			this.checkBoxHelpersAsCoPremise = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.JustifyLeft = new System.Windows.Forms.RadioButton();
			this.JustifyCentre = new System.Windows.Forms.RadioButton();
			this.JustifyRight = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkBoxDistributed = new System.Windows.Forms.CheckBox();
			this.radioButtonStraight = new System.Windows.Forms.RadioButton();
			this.radioButtonDogleg = new System.Windows.Forms.RadioButton();
			this.radioButtonCurve = new System.Windows.Forms.RadioButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.radioButtonTopDown = new System.Windows.Forms.RadioButton();
			this.radioButtonBottomUp = new System.Windows.Forms.RadioButton();
			this.radioButtonLeftRight = new System.Windows.Forms.RadioButton();
			this.radioButtonRightLeft = new System.Windows.Forms.RadioButton();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.checkBoxShowLegend = new System.Windows.Forms.CheckBox();
			this.radioButtonTextBoxes = new System.Windows.Forms.RadioButton();
			this.radioButtonRnotation = new System.Windows.Forms.RadioButton();
			this.checkBoxDropShadow = new System.Windows.Forms.CheckBox();
			this.checkBoxShading = new System.Windows.Forms.CheckBox();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.checkBoxArrowStart = new System.Windows.Forms.CheckBox();
			this.checkBoxArrowEnd = new System.Windows.Forms.CheckBox();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.helpProviderOptions = new System.Windows.Forms.HelpProvider();
			this.toolTipOptions = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentFiles)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tabGraphical.SuspendLayout();
			this.groupBoxHelper.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.helperLineWidth)).BeginInit();
			this.groupBoxObjection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.objectionLineWidth)).BeginInit();
			this.groupBoxReason.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.reasonLineWidth)).BeginInit();
			this.groupBoxPremise.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.premiseLineWidth)).BeginInit();
			this.tabGraphical2.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(432, 456);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(520, 456);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageGeneral);
			this.tabControl1.Controls.Add(this.tabGraphical);
			this.tabControl1.Controls.Add(this.tabGraphical2);
			this.helpProviderOptions.SetHelpKeyword(this.tabControl1, "EditOptions.htm#GraphicalView2");
			this.helpProviderOptions.SetHelpNavigator(this.tabControl1, System.Windows.Forms.HelpNavigator.Topic);
			this.tabControl1.Location = new System.Drawing.Point(16, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.helpProviderOptions.SetShowHelp(this.tabControl1, true);
			this.tabControl1.Size = new System.Drawing.Size(592, 440);
			this.tabControl1.TabIndex = 5;
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.groupBox11);
			this.tabPageGeneral.Controls.Add(this.groupBox10);
			this.tabPageGeneral.Controls.Add(this.groupBox9);
			this.tabPageGeneral.Controls.Add(this.groupBox1);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Size = new System.Drawing.Size(584, 414);
			this.tabPageGeneral.TabIndex = 0;
			this.tabPageGeneral.Text = "General";
			// 
			// groupBox11
			// 
			this.groupBox11.Controls.Add(this.comboBoxDictionary);
			this.groupBox11.Controls.Add(this.label16);
			this.groupBox11.Location = new System.Drawing.Point(24, 320);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.Size = new System.Drawing.Size(280, 72);
			this.groupBox11.TabIndex = 6;
			this.groupBox11.TabStop = false;
			this.groupBox11.Text = "Spell Check";
			// 
			// comboBoxDictionary
			// 
			this.comboBoxDictionary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDictionary.Location = new System.Drawing.Point(128, 32);
			this.comboBoxDictionary.Name = "comboBoxDictionary";
			this.comboBoxDictionary.Size = new System.Drawing.Size(112, 21);
			this.comboBoxDictionary.TabIndex = 1;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(16, 32);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(64, 16);
			this.label16.TabIndex = 0;
			this.label16.Text = "Dictionary";
			// 
			// groupBox10
			// 
			this.groupBox10.Controls.Add(this.checkBoxToday);
			this.groupBox10.Controls.Add(this.textBoxAuthor);
			this.groupBox10.Controls.Add(this.label8);
			this.groupBox10.Controls.Add(this.Date);
			this.groupBox10.Location = new System.Drawing.Point(16, 200);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(288, 96);
			this.groupBox10.TabIndex = 5;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "New File Properties";
			// 
			// checkBoxToday
			// 
			this.checkBoxToday.Location = new System.Drawing.Point(144, 64);
			this.checkBoxToday.Name = "checkBoxToday";
			this.checkBoxToday.Size = new System.Drawing.Size(112, 16);
			this.checkBoxToday.TabIndex = 2;
			this.checkBoxToday.Text = "Use Today\'s date";
			// 
			// textBoxAuthor
			// 
			this.textBoxAuthor.Location = new System.Drawing.Point(144, 32);
			this.textBoxAuthor.Name = "textBoxAuthor";
			this.textBoxAuthor.Size = new System.Drawing.Size(112, 20);
			this.textBoxAuthor.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 32);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "Author";
			this.toolTipOptions.SetToolTip(this.label8, "Author");
			// 
			// Date
			// 
			this.Date.Location = new System.Drawing.Point(8, 64);
			this.Date.Name = "Date";
			this.Date.Size = new System.Drawing.Size(96, 16);
			this.Date.TabIndex = 0;
			this.Date.Text = "Date";
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.numericUpDownRecentFiles);
			this.groupBox9.Controls.Add(this.label6);
			this.groupBox9.Location = new System.Drawing.Point(16, 120);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(288, 64);
			this.groupBox9.TabIndex = 4;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Recent File List";
			// 
			// numericUpDownRecentFiles
			// 
			this.helpProviderOptions.SetHelpNavigator(this.numericUpDownRecentFiles, System.Windows.Forms.HelpNavigator.Topic);
			this.numericUpDownRecentFiles.Location = new System.Drawing.Point(160, 24);
			this.numericUpDownRecentFiles.Maximum = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.numericUpDownRecentFiles.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.numericUpDownRecentFiles.Name = "numericUpDownRecentFiles";
			this.helpProviderOptions.SetShowHelp(this.numericUpDownRecentFiles, true);
			this.numericUpDownRecentFiles.Size = new System.Drawing.Size(80, 20);
			this.numericUpDownRecentFiles.TabIndex = 1;
			this.numericUpDownRecentFiles.Value = new decimal(new int[] {
									3,
									0,
									0,
									0});
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(144, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "Maximum Number of Files";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelTreeViewSample);
			this.groupBox1.Controls.Add(this.buttonSetFont);
			this.groupBox1.Location = new System.Drawing.Point(16, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 80);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tree View Font";
			// 
			// labelTreeViewSample
			// 
			this.labelTreeViewSample.Location = new System.Drawing.Point(16, 32);
			this.labelTreeViewSample.Name = "labelTreeViewSample";
			this.labelTreeViewSample.Size = new System.Drawing.Size(168, 24);
			this.labelTreeViewSample.TabIndex = 2;
			this.labelTreeViewSample.Text = "Tree View Sample";
			// 
			// buttonSetFont
			// 
			this.helpProviderOptions.SetHelpKeyword(this.buttonSetFont, "EditOptions.htm");
			this.helpProviderOptions.SetHelpNavigator(this.buttonSetFont, System.Windows.Forms.HelpNavigator.Topic);
			this.buttonSetFont.Location = new System.Drawing.Point(200, 32);
			this.buttonSetFont.Name = "buttonSetFont";
			this.helpProviderOptions.SetShowHelp(this.buttonSetFont, true);
			this.buttonSetFont.Size = new System.Drawing.Size(75, 23);
			this.buttonSetFont.TabIndex = 1;
			this.buttonSetFont.Text = "Set Font";
			this.toolTipOptions.SetToolTip(this.buttonSetFont, "Change standard font");
			this.buttonSetFont.Click += new System.EventHandler(this.buttonSetFont_Click);
			// 
			// tabGraphical
			// 
			this.tabGraphical.Controls.Add(this.groupBoxHelper);
			this.tabGraphical.Controls.Add(this.groupBoxObjection);
			this.tabGraphical.Controls.Add(this.groupBoxReason);
			this.tabGraphical.Controls.Add(this.groupBoxPremise);
			this.helpProviderOptions.SetHelpNavigator(this.tabGraphical, System.Windows.Forms.HelpNavigator.Topic);
			this.helpProviderOptions.SetHelpString(this.tabGraphical, "EditOptions.htm#GraphicalView");
			this.tabGraphical.Location = new System.Drawing.Point(4, 22);
			this.tabGraphical.Name = "tabGraphical";
			this.helpProviderOptions.SetShowHelp(this.tabGraphical, true);
			this.tabGraphical.Size = new System.Drawing.Size(584, 414);
			this.tabGraphical.TabIndex = 3;
			this.tabGraphical.Text = "Graphical View";
			// 
			// groupBoxHelper
			// 
			this.groupBoxHelper.Controls.Add(this.label17);
			this.groupBoxHelper.Controls.Add(this.helperMaskedTextBox);
			this.groupBoxHelper.Controls.Add(this.label9);
			this.groupBoxHelper.Controls.Add(this.helperLineWidth);
			this.groupBoxHelper.Controls.Add(this.buttonSetHelperBoxColour);
			this.groupBoxHelper.Controls.Add(this.labelHelperSample);
			this.groupBoxHelper.Controls.Add(this.buttonSetHelperFont);
			this.groupBoxHelper.Controls.Add(this.label14);
			this.groupBoxHelper.Controls.Add(this.listBoxHelperLineStyle);
			this.groupBoxHelper.Controls.Add(this.label15);
			this.groupBoxHelper.Location = new System.Drawing.Point(24, 296);
			this.groupBoxHelper.Name = "groupBoxHelper";
			this.groupBoxHelper.Size = new System.Drawing.Size(540, 88);
			this.groupBoxHelper.TabIndex = 6;
			this.groupBoxHelper.TabStop = false;
			this.groupBoxHelper.Text = "Helper";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(476, 29);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(58, 19);
			this.label17.TabIndex = 13;
			this.label17.Text = "Box Width";
			// 
			// helperMaskedTextBox
			// 
			this.helperMaskedTextBox.Location = new System.Drawing.Point(476, 50);
			this.helperMaskedTextBox.Mask = "900";
			this.helperMaskedTextBox.Name = "helperMaskedTextBox";
			this.helperMaskedTextBox.Size = new System.Drawing.Size(27, 20);
			this.helperMaskedTextBox.TabIndex = 12;
			this.helperMaskedTextBox.Text = "200";
			this.helperMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 56);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 16);
			this.label9.TabIndex = 7;
			this.label9.Text = "Line Width";
			// 
			// helperLineWidth
			// 
			this.helperLineWidth.Location = new System.Drawing.Point(80, 56);
			this.helperLineWidth.Maximum = new decimal(new int[] {
									9,
									0,
									0,
									0});
			this.helperLineWidth.Name = "helperLineWidth";
			this.helperLineWidth.Size = new System.Drawing.Size(40, 20);
			this.helperLineWidth.TabIndex = 6;
			// 
			// buttonSetHelperBoxColour
			// 
			this.buttonSetHelperBoxColour.Location = new System.Drawing.Point(128, 56);
			this.buttonSetHelperBoxColour.Name = "buttonSetHelperBoxColour";
			this.buttonSetHelperBoxColour.Size = new System.Drawing.Size(72, 24);
			this.buttonSetHelperBoxColour.TabIndex = 5;
			this.buttonSetHelperBoxColour.Text = "Box Colour";
			this.buttonSetHelperBoxColour.Click += new System.EventHandler(this.buttonSetHelperBoxColour_Click);
			// 
			// labelHelperSample
			// 
			this.labelHelperSample.Location = new System.Drawing.Point(8, 24);
			this.labelHelperSample.Name = "labelHelperSample";
			this.labelHelperSample.Size = new System.Drawing.Size(264, 24);
			this.labelHelperSample.TabIndex = 4;
			this.labelHelperSample.Text = "Helper Sample";
			// 
			// buttonSetHelperFont
			// 
			this.buttonSetHelperFont.Location = new System.Drawing.Point(208, 56);
			this.buttonSetHelperFont.Name = "buttonSetHelperFont";
			this.buttonSetHelperFont.Size = new System.Drawing.Size(72, 23);
			this.buttonSetHelperFont.TabIndex = 3;
			this.buttonSetHelperFont.Text = "Set Font";
			this.buttonSetHelperFont.Click += new System.EventHandler(this.buttonSetHelperFont_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(304, 40);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(32, 16);
			this.label14.TabIndex = 8;
			this.label14.Text = "Style";
			// 
			// listBoxHelperLineStyle
			// 
			this.listBoxHelperLineStyle.Items.AddRange(new object[] {
									"Solid",
									"Dash",
									"Dash Dot",
									"Dash Dot Dot",
									"Dot"});
			this.listBoxHelperLineStyle.Location = new System.Drawing.Point(368, 48);
			this.listBoxHelperLineStyle.Name = "listBoxHelperLineStyle";
			this.listBoxHelperLineStyle.Size = new System.Drawing.Size(88, 17);
			this.listBoxHelperLineStyle.TabIndex = 10;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(304, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(72, 16);
			this.label15.TabIndex = 9;
			this.label15.Text = "Joining Line";
			// 
			// groupBoxObjection
			// 
			this.groupBoxObjection.Controls.Add(this.label4);
			this.groupBoxObjection.Controls.Add(this.objectionMaskedTextBox);
			this.groupBoxObjection.Controls.Add(this.label7);
			this.groupBoxObjection.Controls.Add(this.objectionLineWidth);
			this.groupBoxObjection.Controls.Add(this.buttonSetObjectionBoxColour);
			this.groupBoxObjection.Controls.Add(this.labelObjectionSample);
			this.groupBoxObjection.Controls.Add(this.buttonSetObjectionFont);
			this.groupBoxObjection.Controls.Add(this.label12);
			this.groupBoxObjection.Controls.Add(this.listBoxObjectionLineStyle);
			this.groupBoxObjection.Controls.Add(this.label13);
			this.groupBoxObjection.Location = new System.Drawing.Point(24, 200);
			this.groupBoxObjection.Name = "groupBoxObjection";
			this.groupBoxObjection.Size = new System.Drawing.Size(540, 88);
			this.groupBoxObjection.TabIndex = 5;
			this.groupBoxObjection.TabStop = false;
			this.groupBoxObjection.Text = "Objection";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(476, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 19);
			this.label4.TabIndex = 13;
			this.label4.Text = "Box Width";
			// 
			// objectionMaskedTextBox
			// 
			this.objectionMaskedTextBox.Location = new System.Drawing.Point(476, 53);
			this.objectionMaskedTextBox.Mask = "900";
			this.objectionMaskedTextBox.Name = "objectionMaskedTextBox";
			this.objectionMaskedTextBox.Size = new System.Drawing.Size(27, 20);
			this.objectionMaskedTextBox.TabIndex = 12;
			this.objectionMaskedTextBox.Text = "200";
			this.objectionMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 16);
			this.label7.TabIndex = 7;
			this.label7.Text = "Line Width";
			// 
			// objectionLineWidth
			// 
			this.objectionLineWidth.Location = new System.Drawing.Point(80, 56);
			this.objectionLineWidth.Maximum = new decimal(new int[] {
									9,
									0,
									0,
									0});
			this.objectionLineWidth.Name = "objectionLineWidth";
			this.objectionLineWidth.Size = new System.Drawing.Size(40, 20);
			this.objectionLineWidth.TabIndex = 6;
			// 
			// buttonSetObjectionBoxColour
			// 
			this.buttonSetObjectionBoxColour.Location = new System.Drawing.Point(128, 56);
			this.buttonSetObjectionBoxColour.Name = "buttonSetObjectionBoxColour";
			this.buttonSetObjectionBoxColour.Size = new System.Drawing.Size(72, 24);
			this.buttonSetObjectionBoxColour.TabIndex = 5;
			this.buttonSetObjectionBoxColour.Text = "Box Colour";
			this.buttonSetObjectionBoxColour.Click += new System.EventHandler(this.buttonSetObjectionBoxColour_Click);
			// 
			// labelObjectionSample
			// 
			this.labelObjectionSample.Location = new System.Drawing.Point(8, 24);
			this.labelObjectionSample.Name = "labelObjectionSample";
			this.labelObjectionSample.Size = new System.Drawing.Size(272, 24);
			this.labelObjectionSample.TabIndex = 4;
			this.labelObjectionSample.Text = "Objection Sample";
			// 
			// buttonSetObjectionFont
			// 
			this.buttonSetObjectionFont.Location = new System.Drawing.Point(216, 56);
			this.buttonSetObjectionFont.Name = "buttonSetObjectionFont";
			this.buttonSetObjectionFont.Size = new System.Drawing.Size(72, 23);
			this.buttonSetObjectionFont.TabIndex = 3;
			this.buttonSetObjectionFont.Text = "Set Font";
			this.buttonSetObjectionFont.Click += new System.EventHandler(this.buttonSetObjectionFont_Click);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(304, 56);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(32, 16);
			this.label12.TabIndex = 8;
			this.label12.Text = "Style";
			// 
			// listBoxObjectionLineStyle
			// 
			this.listBoxObjectionLineStyle.Items.AddRange(new object[] {
									"Solid",
									"Dash",
									"Dash Dot",
									"Dash Dot Dot",
									"Dot"});
			this.listBoxObjectionLineStyle.Location = new System.Drawing.Point(368, 56);
			this.listBoxObjectionLineStyle.Name = "listBoxObjectionLineStyle";
			this.listBoxObjectionLineStyle.Size = new System.Drawing.Size(88, 17);
			this.listBoxObjectionLineStyle.TabIndex = 10;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(304, 32);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(72, 16);
			this.label13.TabIndex = 9;
			this.label13.Text = "Joining Line";
			// 
			// groupBoxReason
			// 
			this.groupBoxReason.Controls.Add(this.label2);
			this.groupBoxReason.Controls.Add(this.listBoxReasonLineStyle);
			this.groupBoxReason.Controls.Add(this.reasonMaskedTextBox);
			this.groupBoxReason.Controls.Add(this.label11);
			this.groupBoxReason.Controls.Add(this.label10);
			this.groupBoxReason.Controls.Add(this.label5);
			this.groupBoxReason.Controls.Add(this.reasonLineWidth);
			this.groupBoxReason.Controls.Add(this.buttonSetReasonBoxColour);
			this.groupBoxReason.Controls.Add(this.labelReasonSample);
			this.groupBoxReason.Controls.Add(this.buttonSetReasonFont);
			this.groupBoxReason.Location = new System.Drawing.Point(24, 104);
			this.groupBoxReason.Name = "groupBoxReason";
			this.groupBoxReason.Size = new System.Drawing.Size(540, 88);
			this.groupBoxReason.TabIndex = 4;
			this.groupBoxReason.TabStop = false;
			this.groupBoxReason.Text = "Reason";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(476, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 19);
			this.label2.TabIndex = 11;
			this.label2.Text = "Box Width";
			// 
			// listBoxReasonLineStyle
			// 
			this.listBoxReasonLineStyle.Items.AddRange(new object[] {
									"Solid",
									"Dash",
									"Dash Dot",
									"Dash Dot Dot",
									"Dot"});
			this.listBoxReasonLineStyle.Location = new System.Drawing.Point(368, 41);
			this.listBoxReasonLineStyle.Name = "listBoxReasonLineStyle";
			this.listBoxReasonLineStyle.Size = new System.Drawing.Size(88, 17);
			this.listBoxReasonLineStyle.TabIndex = 10;
			// 
			// reasonMaskedTextBox
			// 
			this.reasonMaskedTextBox.Location = new System.Drawing.Point(476, 38);
			this.reasonMaskedTextBox.Mask = "900";
			this.reasonMaskedTextBox.Name = "reasonMaskedTextBox";
			this.reasonMaskedTextBox.Size = new System.Drawing.Size(27, 20);
			this.reasonMaskedTextBox.TabIndex = 10;
			this.reasonMaskedTextBox.Text = "200";
			this.reasonMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(304, 17);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 16);
			this.label11.TabIndex = 9;
			this.label11.Text = "Joining Line";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(304, 41);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(32, 16);
			this.label10.TabIndex = 8;
			this.label10.Text = "Style";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Line Width";
			// 
			// reasonLineWidth
			// 
			this.reasonLineWidth.Location = new System.Drawing.Point(80, 56);
			this.reasonLineWidth.Maximum = new decimal(new int[] {
									9,
									0,
									0,
									0});
			this.reasonLineWidth.Name = "reasonLineWidth";
			this.reasonLineWidth.Size = new System.Drawing.Size(40, 20);
			this.reasonLineWidth.TabIndex = 6;
			// 
			// buttonSetReasonBoxColour
			// 
			this.buttonSetReasonBoxColour.Location = new System.Drawing.Point(128, 56);
			this.buttonSetReasonBoxColour.Name = "buttonSetReasonBoxColour";
			this.buttonSetReasonBoxColour.Size = new System.Drawing.Size(72, 24);
			this.buttonSetReasonBoxColour.TabIndex = 5;
			this.buttonSetReasonBoxColour.Text = "Box Colour";
			this.buttonSetReasonBoxColour.Click += new System.EventHandler(this.buttonSetReasonBoxColour_Click);
			// 
			// labelReasonSample
			// 
			this.labelReasonSample.Location = new System.Drawing.Point(8, 24);
			this.labelReasonSample.Name = "labelReasonSample";
			this.labelReasonSample.Size = new System.Drawing.Size(272, 24);
			this.labelReasonSample.TabIndex = 4;
			this.labelReasonSample.Text = "Reason Sample";
			// 
			// buttonSetReasonFont
			// 
			this.buttonSetReasonFont.Location = new System.Drawing.Point(216, 56);
			this.buttonSetReasonFont.Name = "buttonSetReasonFont";
			this.buttonSetReasonFont.Size = new System.Drawing.Size(72, 23);
			this.buttonSetReasonFont.TabIndex = 3;
			this.buttonSetReasonFont.Text = "Set Font";
			this.buttonSetReasonFont.Click += new System.EventHandler(this.buttonSetReasonFont_Click);
			// 
			// groupBoxPremise
			// 
			this.groupBoxPremise.Controls.Add(this.premiseMaskedTextBox);
			this.groupBoxPremise.Controls.Add(this.label3);
			this.groupBoxPremise.Controls.Add(this.label1);
			this.groupBoxPremise.Controls.Add(this.premiseLineWidth);
			this.groupBoxPremise.Controls.Add(this.buttonSetPremiseBoxColour);
			this.groupBoxPremise.Controls.Add(this.labelPremiseSample);
			this.groupBoxPremise.Controls.Add(this.buttonSetPremiseFont);
			this.groupBoxPremise.Location = new System.Drawing.Point(24, 8);
			this.groupBoxPremise.Name = "groupBoxPremise";
			this.groupBoxPremise.Size = new System.Drawing.Size(454, 88);
			this.groupBoxPremise.TabIndex = 0;
			this.groupBoxPremise.TabStop = false;
			this.groupBoxPremise.Text = "Premise";
			// 
			// premiseMaskedTextBox
			// 
			this.premiseMaskedTextBox.BeepOnError = true;
			this.premiseMaskedTextBox.Location = new System.Drawing.Point(374, 53);
			this.premiseMaskedTextBox.Mask = "900";
			this.premiseMaskedTextBox.Name = "premiseMaskedTextBox";
			this.premiseMaskedTextBox.Size = new System.Drawing.Size(27, 20);
			this.premiseMaskedTextBox.TabIndex = 9;
			this.premiseMaskedTextBox.Text = "400";
			this.premiseMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 7;
			this.label3.Text = "Line Width";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(304, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Box Width";
			// 
			// premiseLineWidth
			// 
			this.helpProviderOptions.SetHelpKeyword(this.premiseLineWidth, "Graphical View");
			this.premiseLineWidth.Location = new System.Drawing.Point(80, 56);
			this.premiseLineWidth.Maximum = new decimal(new int[] {
									9,
									0,
									0,
									0});
			this.premiseLineWidth.Name = "premiseLineWidth";
			this.helpProviderOptions.SetShowHelp(this.premiseLineWidth, true);
			this.premiseLineWidth.Size = new System.Drawing.Size(40, 20);
			this.premiseLineWidth.TabIndex = 6;
			// 
			// buttonSetPremiseBoxColour
			// 
			this.buttonSetPremiseBoxColour.Location = new System.Drawing.Point(128, 56);
			this.buttonSetPremiseBoxColour.Name = "buttonSetPremiseBoxColour";
			this.buttonSetPremiseBoxColour.Size = new System.Drawing.Size(72, 24);
			this.buttonSetPremiseBoxColour.TabIndex = 5;
			this.buttonSetPremiseBoxColour.Text = "Box Colour";
			this.buttonSetPremiseBoxColour.Click += new System.EventHandler(this.buttonSetPremiseBoxColour_Click);
			// 
			// labelPremiseSample
			// 
			this.labelPremiseSample.Location = new System.Drawing.Point(8, 24);
			this.labelPremiseSample.Name = "labelPremiseSample";
			this.labelPremiseSample.Size = new System.Drawing.Size(272, 24);
			this.labelPremiseSample.TabIndex = 4;
			this.labelPremiseSample.Text = "Premise Sample";
			// 
			// buttonSetPremiseFont
			// 
			this.buttonSetPremiseFont.Location = new System.Drawing.Point(216, 56);
			this.buttonSetPremiseFont.Name = "buttonSetPremiseFont";
			this.buttonSetPremiseFont.Size = new System.Drawing.Size(72, 23);
			this.buttonSetPremiseFont.TabIndex = 3;
			this.buttonSetPremiseFont.Text = "Set Font";
			this.buttonSetPremiseFont.Click += new System.EventHandler(this.buttonSetPremiseFont_Click);
			// 
			// tabGraphical2
			// 
			this.tabGraphical2.Controls.Add(this.checkBoxExpansion);
			this.tabGraphical2.Controls.Add(this.groupBox6);
			this.tabGraphical2.Controls.Add(this.checkBoxHelpersAsCoPremise);
			this.tabGraphical2.Controls.Add(this.groupBox2);
			this.tabGraphical2.Controls.Add(this.groupBox3);
			this.tabGraphical2.Controls.Add(this.groupBox4);
			this.tabGraphical2.Controls.Add(this.groupBox5);
			this.tabGraphical2.Controls.Add(this.groupBox8);
			this.helpProviderOptions.SetHelpKeyword(this.tabGraphical2, "EditOptions.htm#GraphicalView2");
			this.helpProviderOptions.SetHelpNavigator(this.tabGraphical2, System.Windows.Forms.HelpNavigator.Topic);
			this.tabGraphical2.Location = new System.Drawing.Point(4, 22);
			this.tabGraphical2.Name = "tabGraphical2";
			this.helpProviderOptions.SetShowHelp(this.tabGraphical2, true);
			this.tabGraphical2.Size = new System.Drawing.Size(584, 414);
			this.tabGraphical2.TabIndex = 4;
			this.tabGraphical2.Text = "Graphical Cont...";
			// 
			// checkBoxExpansion
			// 
			this.helpProviderOptions.SetHelpString(this.checkBoxExpansion, "");
			this.checkBoxExpansion.Location = new System.Drawing.Point(254, 40);
			this.checkBoxExpansion.Name = "checkBoxExpansion";
			this.helpProviderOptions.SetShowHelp(this.checkBoxExpansion, true);
			this.checkBoxExpansion.Size = new System.Drawing.Size(174, 24);
			this.checkBoxExpansion.TabIndex = 14;
			this.checkBoxExpansion.Text = "Match Tree View Expansion";
			this.toolTipOptions.SetToolTip(this.checkBoxExpansion, "Experimental");
			this.checkBoxExpansion.UseVisualStyleBackColor = true;
			this.checkBoxExpansion.Visible = false;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.comboBoxMarker);
			this.groupBox6.Location = new System.Drawing.Point(244, 96);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(184, 48);
			this.groupBox6.TabIndex = 13;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Marker";
			// 
			// comboBoxMarker
			// 
			this.comboBoxMarker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMarker.FormattingEnabled = true;
			this.comboBoxMarker.Items.AddRange(new object[] {
									"None",
									"Lines",
									"Text"});
			this.comboBoxMarker.Location = new System.Drawing.Point(10, 16);
			this.comboBoxMarker.Name = "comboBoxMarker";
			this.comboBoxMarker.Size = new System.Drawing.Size(86, 21);
			this.comboBoxMarker.TabIndex = 11;
			// 
			// checkBoxHelpersAsCoPremise
			// 
			this.checkBoxHelpersAsCoPremise.Location = new System.Drawing.Point(254, 21);
			this.checkBoxHelpersAsCoPremise.Name = "checkBoxHelpersAsCoPremise";
			this.checkBoxHelpersAsCoPremise.Size = new System.Drawing.Size(193, 24);
			this.checkBoxHelpersAsCoPremise.TabIndex = 10;
			this.checkBoxHelpersAsCoPremise.Text = "Show Helpers as Co-Premises";
			this.toolTipOptions.SetToolTip(this.checkBoxHelpersAsCoPremise, "Expermental");
			this.checkBoxHelpersAsCoPremise.UseVisualStyleBackColor = true;
			this.checkBoxHelpersAsCoPremise.Visible = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.JustifyLeft);
			this.groupBox2.Controls.Add(this.JustifyCentre);
			this.groupBox2.Controls.Add(this.JustifyRight);
			this.groupBox2.Location = new System.Drawing.Point(24, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(184, 80);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Layout";
			this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
			// 
			// JustifyLeft
			// 
			this.JustifyLeft.Checked = true;
			this.JustifyLeft.Location = new System.Drawing.Point(16, 16);
			this.JustifyLeft.Name = "JustifyLeft";
			this.JustifyLeft.Size = new System.Drawing.Size(96, 16);
			this.JustifyLeft.TabIndex = 1;
			this.JustifyLeft.TabStop = true;
			this.JustifyLeft.Text = "Left Justify";
			// 
			// JustifyCentre
			// 
			this.JustifyCentre.Location = new System.Drawing.Point(16, 32);
			this.JustifyCentre.Name = "JustifyCentre";
			this.JustifyCentre.Size = new System.Drawing.Size(96, 16);
			this.JustifyCentre.TabIndex = 1;
			this.JustifyCentre.Text = "Centre";
			// 
			// JustifyRight
			// 
			this.JustifyRight.Location = new System.Drawing.Point(16, 48);
			this.JustifyRight.Name = "JustifyRight";
			this.JustifyRight.Size = new System.Drawing.Size(104, 17);
			this.JustifyRight.TabIndex = 1;
			this.JustifyRight.Text = "Right Justify";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkBoxDistributed);
			this.groupBox3.Controls.Add(this.radioButtonStraight);
			this.groupBox3.Controls.Add(this.radioButtonDogleg);
			this.groupBox3.Controls.Add(this.radioButtonCurve);
			this.groupBox3.Location = new System.Drawing.Point(24, 96);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(184, 72);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Lines";
			// 
			// checkBoxDistributed
			// 
			this.checkBoxDistributed.Location = new System.Drawing.Point(97, 13);
			this.checkBoxDistributed.Name = "checkBoxDistributed";
			this.checkBoxDistributed.Size = new System.Drawing.Size(81, 24);
			this.checkBoxDistributed.TabIndex = 2;
			this.checkBoxDistributed.Text = "Distributed";
			this.checkBoxDistributed.UseVisualStyleBackColor = true;
			// 
			// radioButtonStraight
			// 
			this.radioButtonStraight.Location = new System.Drawing.Point(16, 16);
			this.radioButtonStraight.Name = "radioButtonStraight";
			this.radioButtonStraight.Size = new System.Drawing.Size(104, 16);
			this.radioButtonStraight.TabIndex = 0;
			this.radioButtonStraight.Text = "Straight";
			// 
			// radioButtonDogleg
			// 
			this.radioButtonDogleg.Location = new System.Drawing.Point(16, 32);
			this.radioButtonDogleg.Name = "radioButtonDogleg";
			this.radioButtonDogleg.Size = new System.Drawing.Size(88, 16);
			this.radioButtonDogleg.TabIndex = 1;
			this.radioButtonDogleg.Text = "Dog leg";
			// 
			// radioButtonCurve
			// 
			this.radioButtonCurve.Location = new System.Drawing.Point(16, 48);
			this.radioButtonCurve.Name = "radioButtonCurve";
			this.radioButtonCurve.Size = new System.Drawing.Size(88, 16);
			this.radioButtonCurve.TabIndex = 1;
			this.radioButtonCurve.Text = "Curve";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.radioButtonTopDown);
			this.groupBox4.Controls.Add(this.radioButtonBottomUp);
			this.groupBox4.Controls.Add(this.radioButtonLeftRight);
			this.groupBox4.Controls.Add(this.radioButtonRightLeft);
			this.groupBox4.Location = new System.Drawing.Point(24, 224);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(184, 64);
			this.groupBox4.TabIndex = 7;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Orientation";
			// 
			// radioButtonTopDown
			// 
			this.radioButtonTopDown.Location = new System.Drawing.Point(16, 24);
			this.radioButtonTopDown.Name = "radioButtonTopDown";
			this.radioButtonTopDown.Size = new System.Drawing.Size(80, 16);
			this.radioButtonTopDown.TabIndex = 0;
			this.radioButtonTopDown.Text = "Top Down";
			// 
			// radioButtonBottomUp
			// 
			this.radioButtonBottomUp.Location = new System.Drawing.Point(16, 40);
			this.radioButtonBottomUp.Name = "radioButtonBottomUp";
			this.radioButtonBottomUp.Size = new System.Drawing.Size(80, 16);
			this.radioButtonBottomUp.TabIndex = 0;
			this.radioButtonBottomUp.Text = "Bottom Up";
			// 
			// radioButtonLeftRight
			// 
			this.radioButtonLeftRight.Location = new System.Drawing.Point(104, 24);
			this.radioButtonLeftRight.Name = "radioButtonLeftRight";
			this.radioButtonLeftRight.Size = new System.Drawing.Size(72, 16);
			this.radioButtonLeftRight.TabIndex = 0;
			this.radioButtonLeftRight.Text = "Left Right";
			// 
			// radioButtonRightLeft
			// 
			this.radioButtonRightLeft.Location = new System.Drawing.Point(104, 40);
			this.radioButtonRightLeft.Name = "radioButtonRightLeft";
			this.radioButtonRightLeft.Size = new System.Drawing.Size(72, 16);
			this.radioButtonRightLeft.TabIndex = 0;
			this.radioButtonRightLeft.Text = "Right Left";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.checkBoxShowLegend);
			this.groupBox5.Controls.Add(this.radioButtonTextBoxes);
			this.groupBox5.Controls.Add(this.radioButtonRnotation);
			this.groupBox5.Controls.Add(this.checkBoxDropShadow);
			this.groupBox5.Controls.Add(this.checkBoxShading);
			this.groupBox5.Location = new System.Drawing.Point(24, 304);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(194, 88);
			this.groupBox5.TabIndex = 8;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Notation";
			// 
			// checkBoxShowLegend
			// 
			this.checkBoxShowLegend.Checked = true;
			this.checkBoxShowLegend.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxShowLegend.Location = new System.Drawing.Point(96, 40);
			this.checkBoxShowLegend.Name = "checkBoxShowLegend";
			this.checkBoxShowLegend.Size = new System.Drawing.Size(92, 22);
			this.checkBoxShowLegend.TabIndex = 12;
			this.checkBoxShowLegend.Text = "Show Legend";
			this.toolTipOptions.SetToolTip(this.checkBoxShowLegend, "Only valid when R Notation selected");
			this.checkBoxShowLegend.UseVisualStyleBackColor = true;
			// 
			// radioButtonTextBoxes
			// 
			this.radioButtonTextBoxes.Location = new System.Drawing.Point(16, 16);
			this.radioButtonTextBoxes.Name = "radioButtonTextBoxes";
			this.radioButtonTextBoxes.Size = new System.Drawing.Size(80, 16);
			this.radioButtonTextBoxes.TabIndex = 0;
			this.radioButtonTextBoxes.Text = "Text Boxes";
			// 
			// radioButtonRnotation
			// 
			this.radioButtonRnotation.Location = new System.Drawing.Point(96, 16);
			this.radioButtonRnotation.Name = "radioButtonRnotation";
			this.radioButtonRnotation.Size = new System.Drawing.Size(82, 18);
			this.radioButtonRnotation.TabIndex = 0;
			this.radioButtonRnotation.Text = "R Notation";
			this.toolTipOptions.SetToolTip(this.radioButtonRnotation, "Shows a reference (R1, R2 etc) instead of the element text.\r\nThe Legend displays " +
						"the element text.");
			// 
			// checkBoxDropShadow
			// 
			this.checkBoxDropShadow.Location = new System.Drawing.Point(16, 63);
			this.checkBoxDropShadow.Name = "checkBoxDropShadow";
			this.checkBoxDropShadow.Size = new System.Drawing.Size(94, 16);
			this.checkBoxDropShadow.TabIndex = 10;
			this.checkBoxDropShadow.Text = "Drop Shadow";
			// 
			// checkBoxShading
			// 
			this.checkBoxShading.Location = new System.Drawing.Point(16, 36);
			this.checkBoxShading.Name = "checkBoxShading";
			this.checkBoxShading.Size = new System.Drawing.Size(96, 24);
			this.checkBoxShading.TabIndex = 11;
			this.checkBoxShading.Text = "Shading";
			this.checkBoxShading.CheckedChanged += new System.EventHandler(this.CheckBoxShadingCheckedChanged);
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.checkBoxArrowStart);
			this.groupBox8.Controls.Add(this.checkBoxArrowEnd);
			this.groupBox8.Location = new System.Drawing.Point(24, 176);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(184, 40);
			this.groupBox8.TabIndex = 9;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Arrows";
			// 
			// checkBoxArrowStart
			// 
			this.checkBoxArrowStart.Location = new System.Drawing.Point(16, 16);
			this.checkBoxArrowStart.Name = "checkBoxArrowStart";
			this.checkBoxArrowStart.Size = new System.Drawing.Size(48, 16);
			this.checkBoxArrowStart.TabIndex = 0;
			this.checkBoxArrowStart.Text = "Start";
			// 
			// checkBoxArrowEnd
			// 
			this.checkBoxArrowEnd.Location = new System.Drawing.Point(72, 16);
			this.checkBoxArrowEnd.Name = "checkBoxArrowEnd";
			this.checkBoxArrowEnd.Size = new System.Drawing.Size(48, 16);
			this.checkBoxArrowEnd.TabIndex = 0;
			this.checkBoxArrowEnd.Text = "End";
			// 
			// helpProviderOptions
			// 
			this.helpProviderOptions.HelpNamespace = "argumentative.chm";
			// 
			// Options
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(640, 486);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.helpProviderOptions.SetHelpKeyword(this, "EditOptions.htm");
			this.helpProviderOptions.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.helpProviderOptions.SetShowHelp(this, true);
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Options";
			this.tabControl1.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.groupBox11.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			this.groupBox10.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentFiles)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.tabGraphical.ResumeLayout(false);
			this.groupBoxHelper.ResumeLayout(false);
			this.groupBoxHelper.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.helperLineWidth)).EndInit();
			this.groupBoxObjection.ResumeLayout(false);
			this.groupBoxObjection.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.objectionLineWidth)).EndInit();
			this.groupBoxReason.ResumeLayout(false);
			this.groupBoxReason.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.reasonLineWidth)).EndInit();
			this.groupBoxPremise.ResumeLayout(false);
			this.groupBoxPremise.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.premiseLineWidth)).EndInit();
			this.tabGraphical2.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox checkBoxExpansion;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.ComboBox comboBoxMarker;
		private System.Windows.Forms.CheckBox checkBoxHelpersAsCoPremise;
		private System.Windows.Forms.MaskedTextBox reasonMaskedTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.MaskedTextBox objectionMaskedTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.MaskedTextBox helperMaskedTextBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MaskedTextBox premiseMaskedTextBox;
		private System.Windows.Forms.CheckBox checkBoxDistributed;
		private System.Windows.Forms.ToolTip toolTipOptions;
		private System.Windows.Forms.CheckBox checkBoxShowLegend;
		#endregion

		Font currentFont;
		Color currentColour;
		private void buttonSetFont_Click(object sender, System.EventArgs e)
		{
			fontDialog1.ShowColor = true;

			fontDialog1.Font = theTree.Font;
			fontDialog1.Color = theTree.ForeColor;

			if(fontDialog1.ShowDialog() != DialogResult.Cancel )
			{
				currentFont = fontDialog1.Font;
				currentColour = fontDialog1.Color;
				
				this.labelTreeViewSample.Font = currentFont;
				this.labelTreeViewSample.ForeColor = currentColour;
			}
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			// Apply options
			// zz check if saved
			theTree.Font = currentFont;
			theTree.ForeColor = currentColour;
			// store options
			OptionsData od = OptionsData.Instance;
			// set general options
			od.TreeFont = currentFont;
			od.TreeColour = currentColour;
			od.RecentFiles = (int) this.numericUpDownRecentFiles.Value;
			od.Author = textBoxAuthor.Text;
			if(comboBoxDictionary.SelectedIndex==0)
				od.Dictionary="en-AU.dic";
			else if(comboBoxDictionary.SelectedIndex==1)
				od.Dictionary="es-ES.dic";
			else if(comboBoxDictionary.SelectedIndex==2)
				od.Dictionary="en-US.dic";
			// set justification for graphical view
			if(this.JustifyCentre.Checked)
				od.Justification = DrawTree.justificationType.jCentre;
			if(this.JustifyLeft.Checked)
				od.Justification = DrawTree.justificationType.jLeft;
			if(this.JustifyRight.Checked)
				od.Justification = DrawTree.justificationType.jRight;
			// Join type
			if(this.radioButtonStraight.Checked)
				od.Join = DrawTree.joinType.direct;
			else if(this.radioButtonDogleg.Checked)
				od.Join = DrawTree.joinType.dogleg;
			else
				od.Join = DrawTree.joinType.curve;
			// Graphical View Orientation

			premise.lineWidth	= (int) premiseLineWidth.Value;
			reason.lineWidth	= (int) reasonLineWidth.Value;
			objection.lineWidth	= (int) objectionLineWidth.Value;
			helper.lineWidth	= (int) helperLineWidth.Value;
			
			premise.boxWidth	= Int32.Parse(this.premiseMaskedTextBox.Text);
			reason.boxWidth		= Int32.Parse(this.reasonMaskedTextBox.Text);
			objection.boxWidth	= Int32.Parse(this.objectionMaskedTextBox.Text);
			helper.boxWidth		= Int32.Parse(this.helperMaskedTextBox.Text);

			reason.lineStyle	= getDashStyle(listBoxReasonLineStyle.SelectedIndex);
			objection.lineStyle	= getDashStyle(listBoxObjectionLineStyle.SelectedIndex);
			helper.lineStyle	= getDashStyle(listBoxHelperLineStyle.SelectedIndex);

			// set the options for the elements
			od.premise		= premise;
			od.reason		= reason;
			od.objection	= objection;
			od.helper		= helper;

			// od.Notation
			if(this.radioButtonTextBoxes.Checked)
				od.Notation = DrawTree.notationType.boxes;
			else
				od.Notation = DrawTree.notationType.Rnotation;

			od.DropShadow = this.checkBoxDropShadow.Checked;
			od.BoxShading = this.checkBoxShading.Checked;
			od.ShowLegend = this.checkBoxShowLegend.Checked;
			od.Distributed = this.checkBoxDistributed.Checked;
			od.HelpersAsCoPremises = this.checkBoxHelpersAsCoPremise.Checked;

			if(this.radioButtonTopDown.Checked)
				od.TreeOrientation = DrawTree.treeOrientationType.top_down;
			else if(this.radioButtonBottomUp.Checked)
				od.TreeOrientation = DrawTree.treeOrientationType.bottom_up;
			else if(this.radioButtonLeftRight.Checked)
				od.TreeOrientation = DrawTree.treeOrientationType.left_to_right;
			else if(this.radioButtonRightLeft.Checked)
				od.TreeOrientation = DrawTree.treeOrientationType.right_to_left;

			// Set arrow options
			if(this.checkBoxArrowStart.Checked & this.checkBoxArrowEnd.Checked)
				od.Arrow = DrawTree.arrowType.both;
			else if(this.checkBoxArrowStart.Checked)
				od.Arrow = DrawTree.arrowType.start;
			else if(this.checkBoxArrowEnd.Checked)
				od.Arrow = DrawTree.arrowType.end;
			else
				od.Arrow = DrawTree.arrowType.none;
			
			if(comboBoxMarker.SelectedIndex == 0)
				od.Marker = DrawTree.markerType.none;
			else if(comboBoxMarker.SelectedIndex == 1)
				od.Marker = DrawTree.markerType.lines;
			else if(comboBoxMarker.SelectedIndex == 2)
				od.Marker = DrawTree.markerType.letters;
				
		}

		private DashStyle getDashStyle(int index)
		{
			switch(index)
			{
				case 0:
					return DashStyle.Solid;
				case 1:
					return DashStyle.Dash;
				case 2:
					return DashStyle.DashDot;
				case 3:
					return DashStyle.DashDotDot;
				case 4:
					return DashStyle.Dot;
				default:
					return DashStyle.Dash;
			}
		}
		private int getListBoxIndex(DashStyle ds)
		{
			switch(ds)
			{
				case DashStyle.Solid:
					return 0;
				case DashStyle.Dash:
					return 1;
				case DashStyle.DashDot:
					return 2;
				case DashStyle.DashDotDot:
					return 3;
				case DashStyle.Dot:
					return 4;
				default:
					return 0;
			}
		}
		private void setElementFont(ElementOptions eo,Label L)
		{
			fontDialog1.ShowColor = true;

			fontDialog1.Font = eo.font;
			fontDialog1.Color = eo.fontColour;

			if(fontDialog1.ShowDialog() != DialogResult.Cancel )
			{
				eo.font = fontDialog1.Font;
				eo.fontColour = fontDialog1.Color;
				
				L.Font		= eo.font;
				L.ForeColor	= eo.fontColour;
			}
		}

		private void buttonSetPremiseFont_Click(object sender, System.EventArgs e)
		{
			setElementFont(premise,this.labelPremiseSample);
		}

		private void buttonSetPremiseBoxColour_Click(object sender, System.EventArgs e)
		{
			setBoxColour(premise);
		}

		private void setBoxColour(ElementOptions eo)
		{
			DialogResult r;
			colorDialog1.Color = eo.BoxColour;
			r = colorDialog1.ShowDialog();
			if(r == DialogResult.OK)
				eo.BoxColour = colorDialog1.Color;
		}

		// Options label
		private void setSampleLabel(Label label,NumericUpDown nud,MaskedTextBox mtb,ElementOptions e)
		{
			label.Font = e.font;
			//System.Diagnostics.Debug.Assert(opts.premise.fontColour.ToArgb() == Color.Red.ToArgb()
			//	,"Color: "+opts.premise.fontColour.ToString());
			label.ForeColor = e.fontColour;
			label.BackColor = Color.White;
			nud.Value = e.lineWidth;
			mtb.Text = e.boxWidth.ToString();
		}

		private void buttonSetReasonFont_Click(object sender, System.EventArgs e)
		{
			this.setElementFont(reason,this.labelReasonSample);
		}

		private void buttonSetObjectionFont_Click(object sender, System.EventArgs e)
		{
			this.setElementFont(objection,this.labelObjectionSample);
		}

		private void buttonSetHelperFont_Click(object sender, System.EventArgs e)
		{
			this.setElementFont(helper,this.labelHelperSample);
		}

		private void buttonSetReasonBoxColour_Click(object sender, System.EventArgs e)
		{
			setBoxColour(reason);
		}

		private void buttonSetObjectionBoxColour_Click(object sender, System.EventArgs e)
		{
			setBoxColour(objection);
		}

		private void buttonSetHelperBoxColour_Click(object sender, System.EventArgs e)
		{
			setBoxColour(helper);
		}

		private void groupBox2_Enter(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>Element display properties</summary>
		public class ElementOptions
		{
			/// <summary>Element font</summary>
			public Font font;
			/// <summary>Element colour</summary>
			public Color fontColour;
			/// <summary>Colour of the enclosing box.</summary>
			private Color boxColour;
			/// <summary>Line width</summary>
			public int lineWidth;
			/// <summary>Line style (dashed dotted etc)</summary>
			public DashStyle lineStyle;
			/// <summary>Line width of the enclosing box</summary>
			public int boxWidth;
			/// <summary>Font Brush - sets colour</summary>
			public SolidBrush fontBrush; // sets font colour

			
			/// <summary>Constructor setting defaults:
			/// Font: Arial 8 point
			/// Colour: black
			/// Lines: Solid, Black, 2point
			/// Box Width: 200 pixels
			/// </summary>
			public ElementOptions()
			{
				font = new Font("Arial",8);
				fontColour	= Color.Black;
				boxColour	= Color.Black;
				lineWidth	= 2;
				fontBrush	= new SolidBrush(Color.Black);
				// boxPen = new Pen(Color.Black,1);
				lineStyle	= DashStyle.Solid;
				boxWidth	= 200;
			}
			/// <summary>Gets Box pen object</summary>
			/// <returns></returns>
			public Pen BoxPen(){ return new Pen(boxColour,lineWidth); }
			
			/// <summary>
			/// Constructor using all properties
			/// </summary>
			/// <param name="theFont">Font</param>
			/// <param name="fontSize">Text size</param>
			/// <param name="fontColour">Font Colour</param>
			/// <param name="boxColour">Enclosing box colour</param>
			/// <param name="lineWidth">Enclosing box line width</param>
			/// <param name="boxWidth">Standard box width</param>
			/// <param name="lineStyle">Line style of the enclosing box</param>
			public ElementOptions(string theFont,int fontSize,Color fontColour,Color boxColour,int lineWidth,int boxWidth,DashStyle lineStyle)
			{
				font = new Font(theFont,fontSize);
				this.fontColour = fontColour;
				this.boxColour = boxColour;
				this.lineWidth = lineWidth ;
				fontBrush = new SolidBrush(Color.Black);
				// boxPen = new Pen(boxColour,lineWidth);
				this.lineStyle = lineStyle;
				this.boxWidth	= boxWidth;
			}

			/// <summary>
			/// Copies the an ElementOptions object
			/// </summary>
			/// <param name="e">ElementOptions object to copy</param>
			/// <returns></returns>
			public ElementOptions uglyCopy(ElementOptions e)
			{
				// surly clone is neater
				ElementOptions n = new ElementOptions(e.font.Name,(int) e.font.Size,e.fontColour,
				                                      e.boxColour,e.lineWidth,e.boxWidth,e.lineStyle);
				return n;
			}

			/// <summary>Gets element graphical containing box colour</summary>
			public Color BoxColour
			{
				get {return boxColour; }
				set {
					boxColour = value;
					// boxPen.Color = boxColour;
				}
			}
		}

		
		// The OptionsData object is a Singleton. This can be accessed anywhere.
		/// <summary>
		/// Stores options for features in Argumentative
		/// </summary>
		public class OptionsData
		{
			// options variables
			/// <summary>maximum number of files in the recent files list</summary>
			private int recentFiles;
			/// <summary>Stores the names of the files. Only used for loading</summary>
			private ArrayList recentFileNames;
			private string author;
			private string xslFilepath;
			private string XSLoutputFilepath;
			private string lastXSL;			// full filename of last stylesheet used
			private string lastXSLoutput;	// last output used
			private string dictionary;
			// ***************** Graphical Options *****************
			/// <summary>Display options for the main premise</summary>
			public ElementOptions premise;
			/// <summary>Display options for all objections</summary>
			public ElementOptions objection;
			/// <summary>Display options for all reasons</summary>
			public ElementOptions reason;
			/// <summary>Display options for all helpers</summary>
			public ElementOptions helper;
			private bool showGraphicalView;
			private DrawTree.justificationType justification;
			private DrawTree.joinType join;
			private DrawTree.treeOrientationType treeOrientation;
			private DrawTree.notationType notation;
			private DrawTree.arrowType arrow;
			private bool dropShadow;   // Display drop shadow
			private bool boxShading;
			private bool showLegend;
			private bool distributed;
			private bool helpersAsCoPremises;
			private DrawTree.markerType marker;
			private Font treeFont;
			private Color treeColour;

			private Prefs pref;  // preference handler

			private static OptionsData instance = null;
			/// <summary>Returns the options (singleton) object</summary>
			public static OptionsData Instance { get { return instance; } }

			/// <summary>
			/// Retrieve the options belonging to a Node
			/// </summary>
			/// <param name="n"></param>
			/// <returns></returns>
			public static ElementOptions whichOption(Node n)
			{
				return whichOption(n.nodeType);
			}
			private static ElementOptions whichOption(Node.ArgumentNodeType n)
			{
				switch(n)
				{
						case Node.ArgumentNodeType.premise:   return instance.premise;
						case Node.ArgumentNodeType.reason:    return instance.reason;
						case Node.ArgumentNodeType.objection: return instance.objection;
						case Node.ArgumentNodeType.helper:    return instance.helper;
				}
				System.Diagnostics.Debug.Assert(false,"Unknown node type");
				return null;
			}

			/// <summary>
			/// Initialise the static variable for OptionsData
			/// </summary>
			/// <param name="recent">Menu to populate with recent files</param>
			public static void init(System.Windows.Forms.ToolStripMenuItem recent)
			{
				if(instance!=null)
					return;

				instance = new OptionsData();
				// Set default options
				instance.recentFiles = 3;
				instance.recentFileNames = new ArrayList();
				instance.author = System.Environment.UserName;  // use user name as default
				instance.xslFilepath = Application.StartupPath+"\\xsl\\";
				instance.XSLoutputFilepath = Application.StartupPath+"\\";
				instance.lastXSL = "";
				instance.lastXSLoutput = "";
				instance.dictionary = "en-AU.dic";  // default is the Australian Dictionary  TODO Dictionary should be set using I18N
				instance.premise = new ElementOptions("Arial",16,Color.Black,Color.LightGray,3,400,DashStyle.Solid);
				instance.reason = new ElementOptions("Arial",10,Color.Black,Color.FromArgb(142,243,141),2,200,DashStyle.Solid);
				instance.objection = new ElementOptions("Arial",10,Color.Black,Color.FromArgb(238,136,134),2,200,DashStyle.Solid);
				instance.helper = new ElementOptions("Arial",10,Color.Black,Color.FromArgb(133,132,225),1,200,DashStyle.Solid);
				instance.showGraphicalView = true;
				instance.justification = DrawTree.justificationType.jCentre;
				instance.join = DrawTree.joinType.dogleg;
				instance.treeOrientation = DrawTree.treeOrientationType.top_down;
				instance.notation = DrawTree.notationType.boxes;
				instance.arrow = DrawTree.arrowType.none;
				instance.dropShadow = true;
				instance.boxShading = true;
				instance.showLegend = true;
				instance.distributed = false;
				instance.helpersAsCoPremises = false;
				instance.marker = DrawTree.markerType.none;;
				instance.treeFont = new Font("Arial",8.5F);
				instance.treeColour = Color.Black;
				
				instance.pref = new Prefs();
				// instance.pref.test();
				loadOptions(recent);
				// open options file if there one
				// System.Configuration.ConfigurationSettings
			}
			// Save options
			/// <summary>
			/// Save the current options
			/// </summary>
			/// <param name="recent">The menu strip to extract the recent file list from</param>
			/// <param name="fileName">Options file name</param>
			public static void saveOptions(System.Windows.Forms.ToolStripMenuItem recent,string fileName)
			{
				instance.pref.setPref("RecentFileMax","general",instance.recentFiles.ToString());
				instance.pref.setPref("DefaultAuthor","general",instance.Author);
				instance.pref.setPref("XSLfilepath","general",instance.xslFilepath);
				instance.pref.setPref("XSLoutputFilepath","general",instance.XSLoutputFilepath);
				instance.pref.setPref("lastXSL","general",instance.lastXSL);
				instance.pref.setPref("lastXSLoutput","general",instance.lastXSLoutput);
				instance.pref.setPref("Dictionary","general",instance.dictionary);
				instance.pref.setFont("TreeViewFont","general",instance.treeFont);
				instance.pref.setColour("TreeViewColour","general",instance.treeColour);

				// save the options for the graphical view layout
				saveElementOptions(instance.pref,instance.premise,"Premise");
				saveElementOptions(instance.pref,instance.reason,"Reason");
				saveElementOptions(instance.pref,instance.objection,"Objection");
				saveElementOptions(instance.pref,instance.helper,"Helper");

				instance.pref.setPref("GraphicalViewJoin","graphical",instance.join.ToString());
				instance.pref.setPref("GraphicalViewOrientation","graphical",instance.treeOrientation.ToString());
				instance.pref.setPref("GraphicalViewNotation","graphical",instance.notation.ToString());
				instance.pref.setPref("GraphicalViewArrow","graphical",instance.arrow.ToString());

				instance.pref.setPref("GraphicalViewDropShadow","graphical",instance.dropShadow.ToString());
				instance.pref.setPref("GraphicalViewShading","graphical",instance.boxShading.ToString());
				instance.pref.setPref("GraphicalViewLegend","graphical",instance.showLegend.ToString());
				instance.pref.setPref("GraphicalViewDistributed","graphical",instance.distributed.ToString());
				instance.pref.setPref("GraphicalViewHelperAsCoPremise","graphical",instance.helpersAsCoPremises.ToString());
				instance.pref.setPref("GraphicalViewMarkers","graphical",instance.marker.ToString());

				
				// save recent files from menu
				if(recent != null) //  only if menu defined
				{
					int f;
					for(f=0;f<recent.DropDownItems.Count;f++)
					{
						instance.pref.setPref("RecentFile"+f.ToString(),"general",recent.DropDownItems[f].Text);
					}
				}

				if(fileName.Equals(""))
					instance.pref.save(Application.StartupPath+"\\prefs.xml","");
				else
					instance.pref.save(fileName,"graphical");
			}

			private static void saveElementOptions(Prefs p,ElementOptions eo,string prefix)
			{
				p.setFont(prefix+"Font","graphical",eo.font);
				p.setColour(prefix+"FontColour","graphical",eo.fontColour);
				p.setPref(prefix+"LineWidth","graphical",eo.lineWidth.ToString());
				p.setColour(prefix+"BoxColour","graphical",eo.BoxColour);
				p.setPref(prefix+"JoinLineStyle","graphical",eo.lineStyle.ToString());
				p.setPref(prefix+"BoxWidth","graphical",eo.boxWidth.ToString());
			}
			/// <summary>
			/// Load options relevant to the graphical view
			/// </summary>
			/// <param name="fileName"></param>
			/// <returns></returns>
			public static bool loadGraphicalOnly(string fileName)
			{
				Prefs p = new Prefs();
				return loadGraphical(fileName,p);  // prefs loaded but discarded. instance properties set
			}
			/// <summary>Loads the graphical options from a prefs file</summary>
			/// <param name="fileName"></param>
			/// <param name="p"></param>
			/// <returns></returns>
			public static bool loadGraphical(string fileName, Prefs p)
			{
				if(fileName.Equals(""))
					fileName = Application.StartupPath+"\\prefs.xml";
				//p = instance.pref;
				if(p == null)
					p = new Prefs();
				
				if(! p.load(fileName))
					return false;
				
				loadElementOptions(p,instance.premise,"Premise");
				loadElementOptions(p,instance.reason,"Reason");
				loadElementOptions(p,instance.objection,"Objection");
				loadElementOptions(p,instance.helper,"Helper");

				// load Enum
				instance.join = (DrawTree.joinType) p.getEnum("GraphicalViewJoin",typeof(DrawTree.joinType),instance.join);
				instance.treeOrientation =
					(DrawTree.treeOrientationType) p.getEnum("GraphicalViewOrientation",typeof(DrawTree.treeOrientationType),instance.treeOrientation);
				instance.notation = (DrawTree.notationType) p.getEnum("GraphicalViewNotation",typeof(DrawTree.notationType),instance.notation);
				instance.arrow = (DrawTree.arrowType) p.getEnum("GraphicalViewArrow",typeof(DrawTree.arrowType),instance.arrow);
				instance.dropShadow = p.getBool("GraphicalViewDropShadow");
				instance.boxShading = p.getBool("GraphicalViewShading");
				instance.showLegend = p.getBool("GraphicalViewLegend");
				instance.distributed = p.getBool("GraphicalViewDistributed");
				instance.helpersAsCoPremises = p.getBool("GraphicalViewHelperAsCoPremise");
				instance.marker = (DrawTree.markerType) p.getEnum("GraphicalViewMarkers",typeof(DrawTree.markerType),instance.marker);
				instance.treeFont	= p.getFont("TreeViewFont",new Font("Arial",8.5F));
				instance.treeColour = p.getColour("TreeViewColour",Color.Black);
				return true;
			}
			/// <summary>
			/// Load options from the options XML file format and loads the recent menus.
			/// </summary>
			/// <param name="recent">The menu object to load recent files into</param>
			public static void loadOptions(System.Windows.Forms.ToolStripMenuItem recent)
			{
				Prefs p;
				bool success;

				if(recent == null) return;
				p = instance.pref;
				success = loadGraphical("",p);
				if(! success)
					return;
				instance.recentFiles		= (int)p.getInt("RecentFileMax",3,1);
				instance.Author				= p.getPref("DefaultAuthor");
				instance.xslFilepath		= p.getPref("XSLfilepath");
				instance.XSLoutputFilepath	= p.getPref("XSLoutputFilepath");
				instance.lastXSL			= p.getPref("lastXSL");
				instance.lastXSLoutput		= p.getPref("lastXSLoutput");
				instance.dictionary			= p.getPref("Dictionary");

				// Load recent files.  This is the only data structure not in the prefs list.
				int r,f;
				string s;
				r=instance.recentFiles;
				for(f=0;f<r;f++)
				{
					s = "RecentFile" + f.ToString();
					if(instance.pref.prefExists(s))
						recent.DropDownItems.Add(instance.pref.getPref(s));
				}
			}

			private static void loadElementOptions(Prefs p,ElementOptions eo,string prefix)
			{
				eo.font			= p.getFont(prefix+"Font",eo.font);
				eo.fontColour	= p.getColour(prefix+"FontColour",eo.fontColour);
				eo.lineWidth	= (int) p.getInt(prefix+"LineWidth",eo.lineWidth,1);
				eo.BoxColour	= p.getColour(prefix+"BoxColour",eo.BoxColour);
				eo.lineStyle	= (DashStyle) p.getEnum(prefix+"JoinLineStyle",typeof(DashStyle),DashStyle.Solid);
				eo.boxWidth		= (int)p.getInt(prefix+"BoxWidth",eo.boxWidth,200);
			}

			// Getters & setters
			/// <summary>Gets or sets the number of recent files listed</summary>
			public int RecentFiles {
				get {return this.recentFiles;} set {recentFiles=value;} }
			/// <summary>Gets or sets the current author</summary>
			public string Author { get { return this.author;} set {author=value;}}
			
			/// <summary>Get property. True if the Graphical view is visiable</summary>
			public bool ShowGraphicalView {	get { return this.showGraphicalView; } }

			/// <summary>Gets or sets the dictionary file in use. E.g. en-Au.dic</summary>
			public string Dictionary
			{
				get { return dictionary; }
				set { dictionary = value; }
			}

			/// <summary>Gets or sets drawing justification</summary>
			public DrawTree.justificationType Justification
			{
				get { return justification; }
				set { justification = value; }
			}

			/// <summary>Gets or sets drawing join type.</summary>
			public DrawTree.joinType Join
			{
				get { return join; }
				set { join = value; }
			}

			/// <summary>Gets or sets drawing orientation.</summary>
			public DrawTree.treeOrientationType TreeOrientation
			{
				get { return this.treeOrientation; }
				set { treeOrientation = value; }
			}

			/// <summary>Gets or sets drawing notation type</summary>
			public DrawTree.notationType Notation
			{
				get { return this.notation; }
				set { notation = value; }
			}

			/// <summary>Gets or sets drawing arrow directions</summary>
			public DrawTree.arrowType Arrow
			{
				get { return this.arrow; }
				set { arrow = value; }
			}
			
			/// <summary>Gets or sets drawing box drop shadow.</summary>
			public bool DropShadow
			{
				get { return this.dropShadow; }
				set { dropShadow = value; }
			}
			
			/// <summary>Gets or sets drawing graduated shading.</summary>
			public bool BoxShading
			{
				get { return this.boxShading; }
				set { boxShading = value; }
			}
			
			/// <summary>Gets or sets drawing legend.  Intended for R notation option</summary>
			public bool ShowLegend
			{
				get { return this.showLegend; }
				set { showLegend = value; }
			}
			
			/// <summary>Gets or sets drawing join distribution type.</summary>
			public bool Distributed
			{
				get { return this.distributed; }
				set { distributed = value; }
			}
			/// <summary>Show Helpers at the same level as the owning element.</summary>
			public bool HelpersAsCoPremises
			{
				get { return this.helpersAsCoPremises; }
				set { helpersAsCoPremises = value; }
			}
			
			/// <summary>Gets or sets drawing marker type.</summary>
			public DrawTree.markerType Marker
			{
				get { return this.marker; }
				set { this.marker = value; }
			}
			
			/// <summary>Gets or sets tree view font.</summary>
			public Font TreeFont
			{
				get { return this.treeFont; }
				set { treeFont = value; }
			}
			
			/// <summary>Gets or sets tree view text colour</summary>
			public Color TreeColour
			{
				get { return this.treeColour; }
				set { treeColour = value; }
			}

			/// <summary>Gets path of XSL transformation files.</summary>
			public string XSLfilepath  // TODO not used.  Is this a problem?
			{
				get { return this.xslFilepath; }
				set { this.xslFilepath = value; }
			}

			/// <summary>
			/// Returns the last XSL file used in a transformation.
			/// </summary>
			/// <returns></returns>
			public string getlastXSL() { return this.lastXSL; }
			/// <summary>
			/// Sets the last XSL file used for a transformation
			/// </summary>
			/// <param name="lastXSL"></param>
			public void setlastXSL(string lastXSL) 	{ this.lastXSL = lastXSL; }

			/// <summary>
			/// Gets the last output file used for a transformation.
			/// </summary>
			/// <returns></returns>
			public string getlastXSLoutput() { return this.lastXSLoutput; }
			
			/// <summary>
			/// Sets the last output file used for a transformation.
			/// </summary>
			/// <param name="lastXSLoutput"></param>
			public void setlastXSLoutput(string lastXSLoutput)
			{ this.lastXSLoutput = lastXSLoutput; }
		}
		
		void CheckBoxShadingCheckedChanged(object sender, EventArgs e)
		{
			if(this.checkBoxShading.Checked)
				this.checkBoxDropShadow.Enabled = true;
			else
				this.checkBoxDropShadow.Enabled = false;
		}
	}
}
