using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Drawing.Printing;
using System.Drawing.Imaging;  // For ImageFormat enumeration
// using arg_options;

namespace Argumentative
{
	/// <summary>
	/// Was the main form of the application.<b>Replaced by MainForm</b>
	/// </summary>
	/// <seealso cref="MainForm"/>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.RadioButton radioButtonHelper;
		private System.Windows.Forms.RadioButton radioButtonObjection;
		private System.Windows.Forms.RadioButton radioButtonReason;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.MenuItem menuFileNew;
		private System.Windows.Forms.MenuItem menuFileOpen;
		private System.Windows.Forms.MenuItem menuFileSave;
		private System.Windows.Forms.MenuItem menuFileSaveAs;
		private System.Windows.Forms.MenuItem menuFileExit;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuEditAddReason;
		private System.Windows.Forms.MenuItem menuEditAddObjection;
		private System.Windows.Forms.MenuItem menuEditDelete;
		private System.Windows.Forms.MenuItem menuEditAddHelper;
		private System.Windows.Forms.MenuItem menuFileExport;
		private System.Windows.Forms.MenuItem menuFileExportTransform;
		private System.Windows.Forms.MenuItem menuEditUndo;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuFileExportWord;
		private System.Windows.Forms.MenuItem menuFileExportPPoint;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuEditMove;
		private System.Windows.Forms.MenuItem menuEditMoveUp;
		private System.Windows.Forms.MenuItem menuEditMoveDown;
		private System.Windows.Forms.MenuItem menuEditMoveIn;
		private System.Windows.Forms.MenuItem menuEditMoveOut;
		private System.Windows.Forms.MenuItem menuItemExportCurrentNode;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.MenuItem menuItemPrint;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuHelpAbout;
		private System.Windows.Forms.MenuItem menuViewShowGraphical;
		private System.Windows.Forms.MenuItem menuEditCut;
		private System.Windows.Forms.MenuItem menuEditCopy;
		private System.Windows.Forms.MenuItem menuEditPaste;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuFilePageSetup;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
		private System.Windows.Forms.MenuItem menuItemPrintGraphicView;
		private System.Windows.Forms.MenuItem menuItemProperties;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuEditFind;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.MenuItem menuHelpContents;
		private System.Windows.Forms.MenuItem menuEditEdit;
		private System.Windows.Forms.MenuItem menuItemCheckUpdates;
		private System.Windows.Forms.MenuItem menuItemEditProps;
		private System.Windows.Forms.MenuItem menuViewFindSelection;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuView100;
		private System.Windows.Forms.MenuItem menuViewFull;
		private System.Windows.Forms.MenuItem menuViewZoomIn;
		private System.Windows.Forms.MenuItem menuViewZoomOut;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuViewQuickViews;
		private System.Windows.Forms.MenuItem menuViewQuickBoxes;
		private System.Windows.Forms.MenuItem menuViewQuickColourful;
		private System.Windows.Forms.MenuItem menuViewQuickRnotation;
		private System.Windows.Forms.MenuItem menuViewOrientation;
		private System.Windows.Forms.MenuItem menuViewOrientationTopDown;
		private System.Windows.Forms.MenuItem menuViewOrientationBottomUp;
		private System.Windows.Forms.MenuItem menuFileRecentFiles;
		private System.Windows.Forms.Panel panelGraphicalView;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemEdit;
		private System.Windows.Forms.MenuItem menuItemView;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuView50;
		private System.Windows.Forms.MenuItem menuView200;
		private System.Windows.Forms.MenuItem menuViewShowEdit;
		private System.Windows.Forms.MenuItem menuViewOrientationLeftRight;
		private System.Windows.Forms.MenuItem menuViewOrientationRightLeft;
		private System.Windows.Forms.MenuItem menuEditFindNext;
		private System.Windows.Forms.MenuItem menuViewExpandCollapse;
		private System.Windows.Forms.MenuItem menuViewExpColExpandAll;
		private System.Windows.Forms.MenuItem menuViewExpColCollapseAll;
		private System.Windows.Forms.MenuItem menuViewOrientationCycle;
		private System.Windows.Forms.MenuItem menuEditOptions;
		private System.Windows.Forms.MenuItem menuEditSwap;
		private NetSpell.SpellChecker.Spelling spelling;
		private NetSpell.SpellChecker.Dictionary.WordDictionary wordDictionary;
		private System.Windows.Forms.MenuItem menuItemTools;
		private System.Windows.Forms.MenuItem menuToolsSpelling;
		private System.Windows.Forms.MenuItem menuToolsFullStops;
		private System.Windows.Forms.MenuItem menuViewQuickPreferred;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuToolsSaveOptions;
		private System.Windows.Forms.MenuItem menuViewQuickCycle;
		private System.Windows.Forms.MenuItem menuItemHomePage;
		private System.Windows.Forms.MenuItem menuToolsNewInterface;
		private System.ComponentModel.IContainer components;

		public Form1(string commandline)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
			this.treeView1.KeyPress +=new KeyPressEventHandler(treeView1_KeyPress);
			this.richTextBox1.KeyPress +=new KeyPressEventHandler(richTextBox1_KeyPress);
			// Options.OptionsData.init(this.menuFileRecentFiles);		// start options sub-system
			// Add events to recent files menu
			int f;
			for(f=0;f<menuFileRecentFiles.MenuItems.Count;f++)
				menuFileRecentFiles.MenuItems[f].Click += new System.EventHandler(recentFile_Click);

			a = new ArgMapInterface(true,treeView1,richTextBox1);
			a.loadTree();
			// init command system
			cmd = new Command();
			deleteCommand dc = new deleteCommand(a);
			cmd.loadCommand(dc);
			// cmd.loadCommand(new treeKeyPressCommand());
			if(!commandline.Equals(""))
			{
				a = new ArgMapInterface(false,treeView1,richTextBox1);
				a.loadAXLorRE3(commandline);
				doRedraw(true);
				this.addFileToRecent(a.currentfilename);
			}
			setAppTitle(a.currentfilename);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Reason");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Objection");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Main Premise", new System.Windows.Forms.TreeNode[] {
									treeNode1,
									treeNode2});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelGraphicalView = new System.Windows.Forms.Panel();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.panel2 = new System.Windows.Forms.Panel();
			this.radioButtonHelper = new System.Windows.Forms.RadioButton();
			this.radioButtonObjection = new System.Windows.Forms.RadioButton();
			this.radioButtonReason = new System.Windows.Forms.RadioButton();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuFileNew = new System.Windows.Forms.MenuItem();
			this.menuFileOpen = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuFileSave = new System.Windows.Forms.MenuItem();
			this.menuFileSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItemPrint = new System.Windows.Forms.MenuItem();
			this.menuItemPrintGraphicView = new System.Windows.Forms.MenuItem();
			this.menuFilePageSetup = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuFileExport = new System.Windows.Forms.MenuItem();
			this.menuFileExportWord = new System.Windows.Forms.MenuItem();
			this.menuFileExportPPoint = new System.Windows.Forms.MenuItem();
			this.menuFileExportTransform = new System.Windows.Forms.MenuItem();
			this.menuItemExportCurrentNode = new System.Windows.Forms.MenuItem();
			this.menuItemProperties = new System.Windows.Forms.MenuItem();
			this.menuFileRecentFiles = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuFileExit = new System.Windows.Forms.MenuItem();
			this.menuItemEdit = new System.Windows.Forms.MenuItem();
			this.menuEditUndo = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuEditCut = new System.Windows.Forms.MenuItem();
			this.menuEditCopy = new System.Windows.Forms.MenuItem();
			this.menuEditPaste = new System.Windows.Forms.MenuItem();
			this.menuEditDelete = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuEditEdit = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuEditAddReason = new System.Windows.Forms.MenuItem();
			this.menuEditAddObjection = new System.Windows.Forms.MenuItem();
			this.menuEditAddHelper = new System.Windows.Forms.MenuItem();
			this.menuEditMove = new System.Windows.Forms.MenuItem();
			this.menuEditMoveUp = new System.Windows.Forms.MenuItem();
			this.menuEditMoveDown = new System.Windows.Forms.MenuItem();
			this.menuEditMoveIn = new System.Windows.Forms.MenuItem();
			this.menuEditMoveOut = new System.Windows.Forms.MenuItem();
			this.menuEditSwap = new System.Windows.Forms.MenuItem();
			this.menuItemEditProps = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuEditFind = new System.Windows.Forms.MenuItem();
			this.menuEditFindNext = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemView = new System.Windows.Forms.MenuItem();
			this.menuViewShowGraphical = new System.Windows.Forms.MenuItem();
			this.menuViewShowEdit = new System.Windows.Forms.MenuItem();
			this.menuViewFindSelection = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuView50 = new System.Windows.Forms.MenuItem();
			this.menuView100 = new System.Windows.Forms.MenuItem();
			this.menuView200 = new System.Windows.Forms.MenuItem();
			this.menuViewFull = new System.Windows.Forms.MenuItem();
			this.menuViewZoomIn = new System.Windows.Forms.MenuItem();
			this.menuViewZoomOut = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuViewQuickViews = new System.Windows.Forms.MenuItem();
			this.menuViewQuickBoxes = new System.Windows.Forms.MenuItem();
			this.menuViewQuickColourful = new System.Windows.Forms.MenuItem();
			this.menuViewQuickRnotation = new System.Windows.Forms.MenuItem();
			this.menuViewQuickPreferred = new System.Windows.Forms.MenuItem();
			this.menuViewQuickCycle = new System.Windows.Forms.MenuItem();
			this.menuViewOrientation = new System.Windows.Forms.MenuItem();
			this.menuViewOrientationTopDown = new System.Windows.Forms.MenuItem();
			this.menuViewOrientationBottomUp = new System.Windows.Forms.MenuItem();
			this.menuViewOrientationLeftRight = new System.Windows.Forms.MenuItem();
			this.menuViewOrientationRightLeft = new System.Windows.Forms.MenuItem();
			this.menuViewOrientationCycle = new System.Windows.Forms.MenuItem();
			this.menuViewExpandCollapse = new System.Windows.Forms.MenuItem();
			this.menuViewExpColExpandAll = new System.Windows.Forms.MenuItem();
			this.menuViewExpColCollapseAll = new System.Windows.Forms.MenuItem();
			this.menuItemTools = new System.Windows.Forms.MenuItem();
			this.menuToolsSpelling = new System.Windows.Forms.MenuItem();
			this.menuToolsFullStops = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuToolsSaveOptions = new System.Windows.Forms.MenuItem();
			this.menuEditOptions = new System.Windows.Forms.MenuItem();
			this.menuToolsNewInterface = new System.Windows.Forms.MenuItem();
			this.menuItemHelp = new System.Windows.Forms.MenuItem();
			this.menuHelpContents = new System.Windows.Forms.MenuItem();
			this.menuItemCheckUpdates = new System.Windows.Forms.MenuItem();
			this.menuItemHomePage = new System.Windows.Forms.MenuItem();
			this.menuHelpAbout = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.spelling = new NetSpell.SpellChecker.Spelling(this.components);
			this.wordDictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary(this.components);
			this.panel1.SuspendLayout();
			this.panelGraphicalView.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.AllowDrop = true;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.helpProvider1.SetHelpKeyword(this.treeView1, "TreeView.htm");
			this.helpProvider1.SetHelpNavigator(this.treeView1, System.Windows.Forms.HelpNavigator.Topic);
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "";
			treeNode1.Text = "Reason";
			treeNode2.Name = "";
			treeNode2.Text = "Objection";
			treeNode3.Name = "";
			treeNode3.Text = "Main Premise";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
									treeNode3});
			this.helpProvider1.SetShowHelp(this.treeView1, true);
			this.treeView1.Size = new System.Drawing.Size(352, 842);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			this.treeView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyUp);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(352, 0);
			this.splitter1.MinExtra = 0;
			this.splitter1.MinSize = 0;
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 842);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panelGraphicalView);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.richTextBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(355, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(261, 842);
			this.panel1.TabIndex = 2;
			// 
			// panelGraphicalView
			// 
			this.panelGraphicalView.AutoScroll = true;
			this.panelGraphicalView.BackColor = System.Drawing.SystemColors.Window;
			this.panelGraphicalView.Controls.Add(this.hScrollBar1);
			this.panelGraphicalView.Controls.Add(this.vScrollBar1);
			this.panelGraphicalView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.helpProvider1.SetHelpKeyword(this.panelGraphicalView, "GraphicalView.htm");
			this.helpProvider1.SetHelpNavigator(this.panelGraphicalView, System.Windows.Forms.HelpNavigator.Topic);
			this.panelGraphicalView.Location = new System.Drawing.Point(0, 104);
			this.panelGraphicalView.Name = "panelGraphicalView";
			this.helpProvider1.SetShowHelp(this.panelGraphicalView, true);
			this.panelGraphicalView.Size = new System.Drawing.Size(261, 738);
			this.panelGraphicalView.TabIndex = 2;
			this.panelGraphicalView.Click += new System.EventHandler(this.panelGraphicalView_Click);
			this.panelGraphicalView.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGraphicalView_Paint);
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.hScrollBar1.LargeChange = 100;
			this.hScrollBar1.Location = new System.Drawing.Point(0, 721);
			this.hScrollBar1.Maximum = 1000;
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(244, 17);
			this.hScrollBar1.SmallChange = 10;
			this.hScrollBar1.TabIndex = 1;
			this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.LargeChange = 100;
			this.vScrollBar1.Location = new System.Drawing.Point(244, 0);
			this.vScrollBar1.Maximum = 1000;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 738);
			this.vScrollBar1.SmallChange = 10;
			this.vScrollBar1.TabIndex = 0;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.radioButtonHelper);
			this.panel2.Controls.Add(this.radioButtonObjection);
			this.panel2.Controls.Add(this.radioButtonReason);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 56);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(261, 48);
			this.panel2.TabIndex = 1;
			// 
			// radioButtonHelper
			// 
			this.helpProvider1.SetHelpKeyword(this.radioButtonHelper, "Helper.htm");
			this.helpProvider1.SetHelpNavigator(this.radioButtonHelper, System.Windows.Forms.HelpNavigator.Topic);
			this.radioButtonHelper.Location = new System.Drawing.Point(112, 16);
			this.radioButtonHelper.Name = "radioButtonHelper";
			this.helpProvider1.SetShowHelp(this.radioButtonHelper, true);
			this.radioButtonHelper.Size = new System.Drawing.Size(56, 16);
			this.radioButtonHelper.TabIndex = 2;
			this.radioButtonHelper.Text = "Helper";
			this.radioButtonHelper.CheckedChanged += new System.EventHandler(this.radioButtonHelper_CheckedChanged);
			// 
			// radioButtonObjection
			// 
			this.helpProvider1.SetHelpKeyword(this.radioButtonObjection, "Objection.htm");
			this.helpProvider1.SetHelpNavigator(this.radioButtonObjection, System.Windows.Forms.HelpNavigator.Topic);
			this.radioButtonObjection.Location = new System.Drawing.Point(16, 24);
			this.radioButtonObjection.Name = "radioButtonObjection";
			this.helpProvider1.SetShowHelp(this.radioButtonObjection, true);
			this.radioButtonObjection.Size = new System.Drawing.Size(80, 16);
			this.radioButtonObjection.TabIndex = 1;
			this.radioButtonObjection.Text = "Objection";
			this.radioButtonObjection.CheckedChanged += new System.EventHandler(this.radioButtonObjection_CheckedChanged);
			// 
			// radioButtonReason
			// 
			this.helpProvider1.SetHelpKeyword(this.radioButtonReason, "Reason.htm");
			this.helpProvider1.SetHelpNavigator(this.radioButtonReason, System.Windows.Forms.HelpNavigator.Topic);
			this.radioButtonReason.Location = new System.Drawing.Point(16, 8);
			this.radioButtonReason.Name = "radioButtonReason";
			this.helpProvider1.SetShowHelp(this.radioButtonReason, true);
			this.radioButtonReason.Size = new System.Drawing.Size(88, 16);
			this.radioButtonReason.TabIndex = 0;
			this.radioButtonReason.Text = "Reason";
			this.radioButtonReason.CheckedChanged += new System.EventHandler(this.radioButtonReason_CheckedChanged);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.helpProvider1.SetHelpKeyword(this.richTextBox1, "EditElement.htm");
			this.helpProvider1.SetHelpNavigator(this.richTextBox1, System.Windows.Forms.HelpNavigator.Topic);
			this.richTextBox1.Location = new System.Drawing.Point(0, 0);
			this.richTextBox1.MaxLength = 3000;
			this.richTextBox1.Name = "richTextBox1";
			this.helpProvider1.SetShowHelp(this.richTextBox1, true);
			this.richTextBox1.Size = new System.Drawing.Size(261, 56);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			this.richTextBox1.Leave += new System.EventHandler(this.richTextBox1_Leave);
			this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuItemFile,
									this.menuItemEdit,
									this.menuItemView,
									this.menuItemTools,
									this.menuItemHelp});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuFileNew,
									this.menuFileOpen,
									this.menuItem14,
									this.menuFileSave,
									this.menuFileSaveAs,
									this.menuItem8,
									this.menuItemPrint,
									this.menuItemPrintGraphicView,
									this.menuFilePageSetup,
									this.menuItem5,
									this.menuFileExport,
									this.menuItemProperties,
									this.menuFileRecentFiles,
									this.menuItem9,
									this.menuFileExit});
			this.menuItemFile.Text = "&File";
			// 
			// menuFileNew
			// 
			this.menuFileNew.Index = 0;
			this.menuFileNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.menuFileNew.Text = "New";
			this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
			// 
			// menuFileOpen
			// 
			this.menuFileOpen.Index = 1;
			this.menuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.menuFileOpen.Text = "&Open";
			this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 2;
			this.menuItem14.Text = "-";
			// 
			// menuFileSave
			// 
			this.menuFileSave.Index = 3;
			this.menuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuFileSave.Text = "Save";
			this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
			// 
			// menuFileSaveAs
			// 
			this.menuFileSaveAs.Index = 4;
			this.menuFileSaveAs.Text = "Save &As...";
			this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "-";
			// 
			// menuItemPrint
			// 
			this.menuItemPrint.Index = 6;
			this.menuItemPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
			this.menuItemPrint.Text = "Print";
			this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
			// 
			// menuItemPrintGraphicView
			// 
			this.menuItemPrintGraphicView.Index = 7;
			this.menuItemPrintGraphicView.Text = "Print Graphic View";
			this.menuItemPrintGraphicView.Click += new System.EventHandler(this.menuItemPrintGraphicView_Click);
			// 
			// menuFilePageSetup
			// 
			this.menuFilePageSetup.Index = 8;
			this.menuFilePageSetup.Text = "Page Setup...";
			this.menuFilePageSetup.Click += new System.EventHandler(this.menuFilePageSetup_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 9;
			this.menuItem5.Text = "-";
			// 
			// menuFileExport
			// 
			this.menuFileExport.Index = 10;
			this.menuFileExport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuFileExportWord,
									this.menuFileExportPPoint,
									this.menuFileExportTransform,
									this.menuItemExportCurrentNode});
			this.menuFileExport.Text = "Export";
			// 
			// menuFileExportWord
			// 
			this.menuFileExportWord.Index = 0;
			this.menuFileExportWord.Text = "MS Word...";
			this.menuFileExportWord.Click += new System.EventHandler(this.menuFileExportWord_Click);
			// 
			// menuFileExportPPoint
			// 
			this.menuFileExportPPoint.Index = 1;
			this.menuFileExportPPoint.Text = "MS PowerPoint...";
			this.menuFileExportPPoint.Click += new System.EventHandler(this.menuFileExportPPoint_Click);
			// 
			// menuFileExportTransform
			// 
			this.menuFileExportTransform.Index = 2;
			this.menuFileExportTransform.Shortcut = System.Windows.Forms.Shortcut.F9;
			this.menuFileExportTransform.Text = "Transform...";
			this.menuFileExportTransform.Click += new System.EventHandler(this.menuFileExportTransform_Click);
			// 
			// menuItemExportCurrentNode
			// 
			this.menuItemExportCurrentNode.Index = 3;
			this.menuItemExportCurrentNode.Text = "Current element...";
			this.menuItemExportCurrentNode.Click += new System.EventHandler(this.menuItemExportCurrentNode_Click);
			// 
			// menuItemProperties
			// 
			this.menuItemProperties.Index = 11;
			this.menuItemProperties.Text = "Properties...";
			this.menuItemProperties.Click += new System.EventHandler(this.menuItemProperties_Click);
			// 
			// menuFileRecentFiles
			// 
			this.menuFileRecentFiles.Index = 12;
			this.menuFileRecentFiles.Text = "Recent Files";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 13;
			this.menuItem9.Text = "-";
			// 
			// menuFileExit
			// 
			this.menuFileExit.Index = 14;
			this.menuFileExit.Text = "E&xit";
			this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
			// 
			// menuItemEdit
			// 
			this.menuItemEdit.Index = 1;
			this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuEditUndo,
									this.menuItem12,
									this.menuEditCut,
									this.menuEditCopy,
									this.menuEditPaste,
									this.menuEditDelete,
									this.menuItem17,
									this.menuEditEdit,
									this.menuItem13,
									this.menuEditMove,
									this.menuItemEditProps,
									this.menuItem10,
									this.menuEditFind,
									this.menuEditFindNext,
									this.menuItem3});
			this.menuItemEdit.Text = "&Edit";
			// 
			// menuEditUndo
			// 
			this.menuEditUndo.Index = 0;
			this.menuEditUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
			this.menuEditUndo.Text = "Undo";
			this.menuEditUndo.Click += new System.EventHandler(this.menuEditUndo_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 1;
			this.menuItem12.Text = "-";
			// 
			// menuEditCut
			// 
			this.menuEditCut.Index = 2;
			this.menuEditCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuEditCut.Text = "Cut";
			this.menuEditCut.Click += new System.EventHandler(this.menuEditCut_Click);
			// 
			// menuEditCopy
			// 
			this.menuEditCopy.Index = 3;
			this.menuEditCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuEditCopy.Text = "Copy";
			this.menuEditCopy.Click += new System.EventHandler(this.menuEditCopy_Click);
			// 
			// menuEditPaste
			// 
			this.menuEditPaste.Index = 4;
			this.menuEditPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuEditPaste.Text = "Paste";
			this.menuEditPaste.Click += new System.EventHandler(this.menuEditPaste_Click);
			// 
			// menuEditDelete
			// 
			this.menuEditDelete.Index = 5;
			this.menuEditDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
			this.menuEditDelete.Text = "Delete";
			this.menuEditDelete.Click += new System.EventHandler(this.menuEditDelete_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 6;
			this.menuItem17.Text = "-";
			// 
			// menuEditEdit
			// 
			this.menuEditEdit.Index = 7;
			this.menuEditEdit.Shortcut = System.Windows.Forms.Shortcut.F2;
			this.menuEditEdit.Text = "Edit";
			this.menuEditEdit.Click += new System.EventHandler(this.menuEditEdit_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 8;
			this.menuItem13.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuEditAddReason,
									this.menuEditAddObjection,
									this.menuEditAddHelper});
			this.menuItem13.Text = "Add";
			// 
			// menuEditAddReason
			// 
			this.menuEditAddReason.Index = 0;
			this.menuEditAddReason.Shortcut = System.Windows.Forms.Shortcut.Ins;
			this.menuEditAddReason.Text = "Reason";
			this.menuEditAddReason.Click += new System.EventHandler(this.menuEditAddReason_Click);
			// 
			// menuEditAddObjection
			// 
			this.menuEditAddObjection.Index = 1;
			this.menuEditAddObjection.Shortcut = System.Windows.Forms.Shortcut.ShiftIns;
			this.menuEditAddObjection.Text = "Objection";
			this.menuEditAddObjection.Click += new System.EventHandler(this.menuEditAddObjection_Click);
			// 
			// menuEditAddHelper
			// 
			this.menuEditAddHelper.Index = 2;
			this.menuEditAddHelper.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
			this.menuEditAddHelper.Text = "Helper";
			this.menuEditAddHelper.Click += new System.EventHandler(this.menuEditAddHelper_Click);
			// 
			// menuEditMove
			// 
			this.menuEditMove.Index = 9;
			this.menuEditMove.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuEditMoveUp,
									this.menuEditMoveDown,
									this.menuEditMoveIn,
									this.menuEditMoveOut,
									this.menuEditSwap});
			this.menuEditMove.Text = "Move";
			// 
			// menuEditMoveUp
			// 
			this.menuEditMoveUp.Index = 0;
			this.menuEditMoveUp.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
			this.menuEditMoveUp.Text = "Up";
			this.menuEditMoveUp.Click += new System.EventHandler(this.menuEditMoveUp_Click);
			// 
			// menuEditMoveDown
			// 
			this.menuEditMoveDown.Index = 1;
			this.menuEditMoveDown.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
			this.menuEditMoveDown.Text = "Down";
			this.menuEditMoveDown.Click += new System.EventHandler(this.menuEditMoveDown_Click);
			// 
			// menuEditMoveIn
			// 
			this.menuEditMoveIn.Index = 2;
			this.menuEditMoveIn.Shortcut = System.Windows.Forms.Shortcut.CtrlJ;
			this.menuEditMoveIn.Text = "In";
			this.menuEditMoveIn.Click += new System.EventHandler(this.menuEditMoveIn_Click);
			// 
			// menuEditMoveOut
			// 
			this.menuEditMoveOut.Index = 3;
			this.menuEditMoveOut.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
			this.menuEditMoveOut.Text = "Out";
			this.menuEditMoveOut.Click += new System.EventHandler(this.menuEditMoveOut_Click);
			// 
			// menuEditSwap
			// 
			this.menuEditSwap.Index = 4;
			this.menuEditSwap.Text = "Swap";
			this.menuEditSwap.Click += new System.EventHandler(this.menuEditSwap_Click);
			// 
			// menuItemEditProps
			// 
			this.menuItemEditProps.Index = 10;
			this.menuItemEditProps.Shortcut = System.Windows.Forms.Shortcut.F4;
			this.menuItemEditProps.Text = "Properties";
			this.menuItemEditProps.Click += new System.EventHandler(this.menuItemEditProps_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 11;
			this.menuItem10.Text = "-";
			// 
			// menuEditFind
			// 
			this.menuEditFind.Index = 12;
			this.menuEditFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
			this.menuEditFind.Text = "Find...";
			this.menuEditFind.Click += new System.EventHandler(this.menuEditFind_Click);
			// 
			// menuEditFindNext
			// 
			this.menuEditFindNext.Index = 13;
			this.menuEditFindNext.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
			this.menuEditFindNext.Text = "Find next";
			this.menuEditFindNext.Click += new System.EventHandler(this.menuFindNext_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 14;
			this.menuItem3.Text = "-";
			// 
			// menuItemView
			// 
			this.menuItemView.Index = 2;
			this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuViewShowGraphical,
									this.menuViewShowEdit,
									this.menuViewFindSelection,
									this.menuItem11,
									this.menuView50,
									this.menuView100,
									this.menuView200,
									this.menuViewFull,
									this.menuViewZoomIn,
									this.menuViewZoomOut,
									this.menuItem15,
									this.menuViewQuickViews,
									this.menuViewOrientation,
									this.menuViewExpandCollapse});
			this.menuItemView.Text = "View";
			// 
			// menuViewShowGraphical
			// 
			this.menuViewShowGraphical.Checked = true;
			this.menuViewShowGraphical.Index = 0;
			this.menuViewShowGraphical.Text = "Show Graphical View";
			this.menuViewShowGraphical.Click += new System.EventHandler(this.menuViewShowGraphical_Click);
			// 
			// menuViewShowEdit
			// 
			this.menuViewShowEdit.Checked = true;
			this.menuViewShowEdit.Index = 1;
			this.menuViewShowEdit.Text = "Show Edit Area";
			this.menuViewShowEdit.Click += new System.EventHandler(this.menuViewShowEdit_Click);
			// 
			// menuViewFindSelection
			// 
			this.menuViewFindSelection.Index = 2;
			this.menuViewFindSelection.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.menuViewFindSelection.Text = "Find selection";
			this.menuViewFindSelection.Click += new System.EventHandler(this.menuViewFindSelection_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 3;
			this.menuItem11.Text = "-";
			// 
			// menuView50
			// 
			this.menuView50.Index = 4;
			this.menuView50.Shortcut = System.Windows.Forms.Shortcut.Ctrl5;
			this.menuView50.Text = "50%";
			this.menuView50.Click += new System.EventHandler(this.menuItemView50_Click);
			// 
			// menuView100
			// 
			this.menuView100.Index = 5;
			this.menuView100.Shortcut = System.Windows.Forms.Shortcut.Ctrl1;
			this.menuView100.Text = "100%";
			this.menuView100.Click += new System.EventHandler(this.menuView100_Click);
			// 
			// menuView200
			// 
			this.menuView200.Index = 6;
			this.menuView200.Shortcut = System.Windows.Forms.Shortcut.Ctrl2;
			this.menuView200.Text = "200%";
			this.menuView200.Click += new System.EventHandler(this.menuItemView200_Click);
			// 
			// menuViewFull
			// 
			this.menuViewFull.Index = 7;
			this.menuViewFull.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.menuViewFull.Text = "Full";
			this.menuViewFull.Click += new System.EventHandler(this.menuViewFull_Click);
			// 
			// menuViewZoomIn
			// 
			this.menuViewZoomIn.Index = 8;
			this.menuViewZoomIn.Shortcut = System.Windows.Forms.Shortcut.F11;
			this.menuViewZoomIn.Text = "Zoom in";
			this.menuViewZoomIn.Click += new System.EventHandler(this.menuViewZoomIn_Click);
			// 
			// menuViewZoomOut
			// 
			this.menuViewZoomOut.Index = 9;
			this.menuViewZoomOut.Shortcut = System.Windows.Forms.Shortcut.F12;
			this.menuViewZoomOut.Text = "Zoom out";
			this.menuViewZoomOut.Click += new System.EventHandler(this.menuViewZoomOut_Click);
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 10;
			this.menuItem15.Text = "-";
			// 
			// menuViewQuickViews
			// 
			this.menuViewQuickViews.Index = 11;
			this.menuViewQuickViews.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuViewQuickBoxes,
									this.menuViewQuickColourful,
									this.menuViewQuickRnotation,
									this.menuViewQuickPreferred,
									this.menuViewQuickCycle});
			this.menuViewQuickViews.Text = "Quick Views";
			// 
			// menuViewQuickBoxes
			// 
			this.menuViewQuickBoxes.Index = 0;
			this.menuViewQuickBoxes.Text = "Plain Boxes";
			this.menuViewQuickBoxes.Click += new System.EventHandler(this.menuViewQuickBoxes_Click);
			// 
			// menuViewQuickColourful
			// 
			this.menuViewQuickColourful.Index = 1;
			this.menuViewQuickColourful.Text = "Colourful";
			this.menuViewQuickColourful.Click += new System.EventHandler(this.menuViewQuickColourful_Click);
			// 
			// menuViewQuickRnotation
			// 
			this.menuViewQuickRnotation.Index = 2;
			this.menuViewQuickRnotation.Text = "R Notation";
			this.menuViewQuickRnotation.Click += new System.EventHandler(this.menuViewQuickRnotation_Click);
			// 
			// menuViewQuickPreferred
			// 
			this.menuViewQuickPreferred.Index = 3;
			this.menuViewQuickPreferred.Text = "Preferred";
			this.menuViewQuickPreferred.Click += new System.EventHandler(this.menuViewQuickPreferred_Click);
			// 
			// menuViewQuickCycle
			// 
			this.menuViewQuickCycle.Index = 4;
			this.menuViewQuickCycle.Shortcut = System.Windows.Forms.Shortcut.F8;
			this.menuViewQuickCycle.Text = "Cycle";
			this.menuViewQuickCycle.Click += new System.EventHandler(this.menuViewQuickCycle_Click);
			// 
			// menuViewOrientation
			// 
			this.menuViewOrientation.Index = 12;
			this.menuViewOrientation.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuViewOrientationTopDown,
									this.menuViewOrientationBottomUp,
									this.menuViewOrientationLeftRight,
									this.menuViewOrientationRightLeft,
									this.menuViewOrientationCycle});
			this.menuViewOrientation.Text = "Orientation";
			// 
			// menuViewOrientationTopDown
			// 
			this.menuViewOrientationTopDown.Index = 0;
			this.menuViewOrientationTopDown.Text = "Top Down";
			this.menuViewOrientationTopDown.Click += new System.EventHandler(this.menuViewOrientationTopDown_Click);
			// 
			// menuViewOrientationBottomUp
			// 
			this.menuViewOrientationBottomUp.Index = 1;
			this.menuViewOrientationBottomUp.Text = "Bottom Up";
			this.menuViewOrientationBottomUp.Click += new System.EventHandler(this.menuViewOrientationBottomUp_Click);
			// 
			// menuViewOrientationLeftRight
			// 
			this.menuViewOrientationLeftRight.Index = 2;
			this.menuViewOrientationLeftRight.Text = "Left to Right";
			this.menuViewOrientationLeftRight.Click += new System.EventHandler(this.menuViewOrientationLeftRightClick);
			// 
			// menuViewOrientationRightLeft
			// 
			this.menuViewOrientationRightLeft.Index = 3;
			this.menuViewOrientationRightLeft.Text = "Right to Left";
			this.menuViewOrientationRightLeft.Click += new System.EventHandler(this.menuViewOrientationRightLeft_Click);
			// 
			// menuViewOrientationCycle
			// 
			this.menuViewOrientationCycle.Index = 4;
			this.menuViewOrientationCycle.Shortcut = System.Windows.Forms.Shortcut.F6;
			this.menuViewOrientationCycle.Text = "Cycle";
			this.menuViewOrientationCycle.Click += new System.EventHandler(this.menuViewOrientationCycle_Click);
			// 
			// menuViewExpandCollapse
			// 
			this.menuViewExpandCollapse.Index = 13;
			this.menuViewExpandCollapse.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuViewExpColExpandAll,
									this.menuViewExpColCollapseAll});
			this.menuViewExpandCollapse.Text = "Expand / Collapse";
			// 
			// menuViewExpColExpandAll
			// 
			this.menuViewExpColExpandAll.Index = 0;
			this.menuViewExpColExpandAll.Text = "Expand All";
			this.menuViewExpColExpandAll.Click += new System.EventHandler(this.menuViewExpColExpandAll_Click);
			// 
			// menuViewExpColCollapseAll
			// 
			this.menuViewExpColCollapseAll.Index = 1;
			this.menuViewExpColCollapseAll.Text = "Collapse All";
			this.menuViewExpColCollapseAll.Click += new System.EventHandler(this.menuViewExpColCollapseAll_Click);
			// 
			// menuItemTools
			// 
			this.menuItemTools.Index = 3;
			this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuToolsSpelling,
									this.menuToolsFullStops,
									this.menuItem1,
									this.menuToolsSaveOptions,
									this.menuEditOptions,
									this.menuToolsNewInterface});
			this.menuItemTools.Text = "Tools";
			// 
			// menuToolsSpelling
			// 
			this.menuToolsSpelling.Index = 0;
			this.menuToolsSpelling.Shortcut = System.Windows.Forms.Shortcut.F7;
			this.menuToolsSpelling.Text = "Spelling...";
			this.menuToolsSpelling.Click += new System.EventHandler(this.menuToolsSpelling_Click);
			// 
			// menuToolsFullStops
			// 
			this.menuToolsFullStops.Index = 1;
			this.menuToolsFullStops.Text = "Full stops...";
			this.menuToolsFullStops.Click += new System.EventHandler(this.menuEditFullStops_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// menuToolsSaveOptions
			// 
			this.menuToolsSaveOptions.Index = 3;
			this.menuToolsSaveOptions.Text = "Save Options...";
			this.menuToolsSaveOptions.Click += new System.EventHandler(this.menuToolsSaveOptions_Click);
			// 
			// menuEditOptions
			// 
			this.menuEditOptions.Index = 4;
			this.menuEditOptions.Text = "Options...";
			this.menuEditOptions.Click += new System.EventHandler(this.menuEditOptions_Click);
			// 
			// menuToolsNewInterface
			// 
			this.menuToolsNewInterface.Index = 5;
			this.menuToolsNewInterface.Text = "New Interface";
			this.menuToolsNewInterface.Click += new System.EventHandler(this.menuToolsNewInterface_Click);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Index = 4;
			this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuHelpContents,
									this.menuItemCheckUpdates,
									this.menuItemHomePage,
									this.menuHelpAbout});
			this.menuItemHelp.Text = "&Help";
			// 
			// menuHelpContents
			// 
			this.menuHelpContents.Index = 0;
			this.menuHelpContents.Text = "Contents...";
			this.menuHelpContents.Click += new System.EventHandler(this.menuHelpContents_Click);
			// 
			// menuItemCheckUpdates
			// 
			this.menuItemCheckUpdates.Index = 1;
			this.menuItemCheckUpdates.Text = "Check for Updates...";
			this.menuItemCheckUpdates.Click += new System.EventHandler(this.menuItemCheckUpdates_Click);
			// 
			// menuItemHomePage
			// 
			this.menuItemHomePage.Index = 2;
			this.menuItemHomePage.Text = "Home page...";
			this.menuItemHomePage.Click += new System.EventHandler(this.menuItemHomePage_Click);
			// 
			// menuHelpAbout
			// 
			this.menuHelpAbout.Index = 3;
			this.menuHelpAbout.Text = "About...";
			this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
			// 
			// helpProvider1
			// 
			this.helpProvider1.HelpNamespace = "argumentative.chm";
			// 
			// spelling
			// 
			this.spelling.Dictionary = this.wordDictionary;
			this.spelling.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.spelling_EndOfText);
			this.spelling.DeletedWord += new NetSpell.SpellChecker.Spelling.DeletedWordEventHandler(this.spelling_DeletedWord);
			this.spelling.ReplacedWord += new NetSpell.SpellChecker.Spelling.ReplacedWordEventHandler(this.spelling_ReplacedWord);
			// 
			// wordDictionary
			// 
			this.wordDictionary.DictionaryFile = "en-AU.dic";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 842);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Argumentative";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.panel1.ResumeLayout(false);
			this.panelGraphicalView.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
//		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
//		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		#endregion


		// ************** Global objects *****************

		private const string current_version = "0.4.1 alpha 8";
		private const int maxRecentFiles = 9;

		public ArgMapInterface a;
		public Command cmd;
		public FormProperties properties;  // modal dialog box
		private string[] recent = new string[maxRecentFiles];  // storage of recent files
		public float zoom = 1F;
		private int maxWidth, maxHeight;
		private bool recalc;  // some change has been made to the data structuure
		
		private void setAppTitle(string fileName)
		{
			fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
			this.Text = "Argumentative - " + fileName;
		}

		private void doRedraw(bool recalc)
		{
			this.recalc = recalc;
			this.panelGraphicalView.Invalidate();
		}
		private void menuFileExit_Click(object sender, System.EventArgs e)
		{
			// this.Form1_Closing(sender,e);
			// Options.OptionsData.saveOptions();
			this.Close();
		}

		private void menuFileOpen_Click(object sender, System.EventArgs e)
		{
			ArgMapInterface ai = a.openArgument();
			if(ai != null)
			{
				a = ai;
				zoom = 1F;
				addFileToRecent(a.currentfilename);  // Recent files list
				setAppTitle(a.currentfilename);
			}
			doRedraw(true);  // redraw and recalc
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode tn;
			Node n;

			tn = treeView1.SelectedNode;
			if(tn != null)
			{
				n = (Node) tn.Tag;
				// node not set zz
				richTextBox1.Text = tn.Text;
				// correct font if it has changed
				Font f = richTextBox1.SelectionFont;
				if( (!f.Name.Equals("Microsoft Sans Serif")) | (f.Size != 8.25F) )
				{
					richTextBox1.SelectAll();
					Font ff = new Font("Microsoft Sans Serif",8.25F);
					richTextBox1.SelectionFont = ff;
				}
				if(n != null) 
				{
					if(n.nodeType==Node.ArgumentNodeType.reason)
					{
						radioButtonReason.Checked		= true;
						radioButtonObjection.Checked	= false;
						radioButtonHelper.Checked		= false;
					} else
					if(n.nodeType==Node.ArgumentNodeType.objection)
					{
						radioButtonReason.Checked		= false;
						radioButtonObjection.Checked	= true;
						radioButtonHelper.Checked		= false;
					} 
					else if(n.nodeType==Node.ArgumentNodeType.helper)
					{
						radioButtonReason.Checked		= false;
						radioButtonObjection.Checked	= false;
						radioButtonHelper.Checked		= true;
					}
					else if(n.nodeType==Node.ArgumentNodeType.premise)
					{
						radioButtonReason.Checked		= false;
						radioButtonObjection.Checked	= false;
						radioButtonHelper.Checked		= false;
					}
					// Change properties box if open
					if(properties != null)
					{
						properties.changeSelection(tn);
					}
				}
			}
		}

		private void menuFileClose_Click(object sender, System.EventArgs e)
		{
		}

		private void menuFileSaveAs_Click(object sender, System.EventArgs e)
		{
			DialogResult r;
			string fileName;
			Node n;

			n = a.CurrentArg.findHead();
			saveFileDialog1.Filter = "Argumentative XML|*.axl|Bitmap|*.bmp|PNG|*.png|JPEG|*.jpg|GIF|*.gif|Rationale 1.1|*.rtnl";
			saveFileDialog1.Title = "Save an Argument File";
			if(a != null && !a.currentfilename.Equals(""))
			{
				fileName = a.currentfilename;
				if(fileName.Equals(ArgMapInterface.UndefinedFile))
					fileName = a.CurrentArg.findHead().EditorText;  // premise text
				fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);  // +".axl";  missing extension is AXL
				saveFileDialog1.FileName = fileName;
				saveFileDialog1.FilterIndex = 1;  // Make sure file type is known
			}
			try 
			{
				r = saveFileDialog1.ShowDialog();
			} 
			catch (Exception ex)
			{
				MessageBox.Show("Error in file save: "+ex.Message);
				return;
			}
			if(saveFileDialog1.FileName == "")
				return;
			if(r==DialogResult.OK)
			{
				fileName = saveFileDialog1.FileName;
				string ending;
				ending = System.IO.Path.GetExtension(fileName).ToLower();
				if(ending.Equals(".bmp")|| saveFileDialog1.FilterIndex == 2)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(System.IO.Path.GetFileNameWithoutExtension(fileName)+".bmp",n,ImageFormat.Bmp);
				}
				else if(ending.ToLower().Equals(".png")|| saveFileDialog1.FilterIndex == 3)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(System.IO.Path.GetFileNameWithoutExtension(fileName)+".png",n,ImageFormat.Png);
				}
				else if(ending.ToLower().Equals(".jpg")|| saveFileDialog1.FilterIndex == 4)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(fileName,n,ImageFormat.Jpeg);
				}
				else if(ending.ToLower().Equals(".gif")|| saveFileDialog1.FilterIndex == 5)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(fileName,n,ImageFormat.Gif);
				}
				else if(ending.ToLower().Equals(".rtnl")|| saveFileDialog1.FilterIndex == 6) // Rationale export
				{
					Rationale rx = new Rationale();
					rx.exportToRationale(fileName,a.CurrentArg.findHead(),false); // expanded = false.  Option should set to true zz
				}
				else if(ending.Equals(".axl"))  // already has the correct extension
				{
					a.saveArg(fileName,null,false);
					addFileToRecent(fileName);
				}
				else if(ending.ToLower().Equals(""))
				{
					if(fileName != fileName + ".axl")
					{
						if(System.IO.File.Exists(fileName + ".axl"))
						{
							DialogResult result;
							string message = fileName+" exists. Do you wish to overwrite?";
							result = MessageBox.Show(this,message,"Overwrite?",MessageBoxButtons.YesNo);
							if(result != DialogResult.Yes)
								return;
						}
					}
					fileName = fileName + ".axl";
					a.saveArg(fileName,null,false);
				}
				else
				{
					fileName = fileName + ".axl";
					a.saveArg(fileName,null,false);
					addFileToRecent(fileName);
				}
				
				setAppTitle(fileName);  // Set the window title
			}
		}

		private void richTextBox1_TextChanged(object sender, System.EventArgs e)
		{
			if(a==null) return;
			if(a.editBoxChange(richTextBox1))
				doRedraw(true);
			return;
		}

		/// <summary>
		/// Processes key presses sent to the Tree View.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;
			if(c==13)  // if enter is pressed
			{
				a.addPeerNodeToTree(treeView1.SelectedNode);
				richTextBox1.SelectAll();
				e.Handled = true;
				return;
			}
			// a key is pressed - edit this node and replace existing text
			cmd.executeCommand(new treeKeyPressCommand(e.KeyChar,richTextBox1));

			e.Handled = true;
		}

		private void menuFileExportTransform_Click(object sender, System.EventArgs e)
		{
			Form dlg1 = new TransformDlg(a);
			dlg1.ShowDialog();
		}

		private void menuHelpAbout_Click(object sender, System.EventArgs e)
		{
			// About - shows version information

			About about = new About(current_version);
			about.ShowDialog();
		}

		private void menuFileSave_Click(object sender, System.EventArgs e)
		{
			if(a.currentfilename.Equals(ArgMapInterface.UndefinedFile))
				menuFileSaveAs_Click(sender,e);  // same as Save As for the moment
			else 
			{
				a.saveArg(a.currentfilename,null,false);
				setAppTitle(a.currentfilename);  // Set the window title
			}
		}

		private void menuEditOptions_Click(object sender, System.EventArgs e)
		{
			// Options Dialog
			Options d = new Options(treeView1);
			d.ShowDialog();
			doRedraw(true);
		}

		private void menuFileExportWord_Click(object sender, System.EventArgs e)
		{
			// Export to Word
			a.exportWord();
		}

		private void menuFileExportPPoint_Click(object sender, System.EventArgs e)
		{
			a.exportPowerPoint();
		}

		private void radioButtonReason_CheckedChanged(object sender, System.EventArgs e)
		{
			// Reason radio button clicked
			a.changeNodeto(Node.ArgumentNodeType.reason);
			doRedraw(true);
		}

		private void radioButtonObjection_CheckedChanged(object sender, System.EventArgs e)
		{
			a.changeNodeto(Node.ArgumentNodeType.objection);
			doRedraw(true);
		}

		private void radioButtonHelper_CheckedChanged(object sender, System.EventArgs e)
		{
			a.changeNodeto(Node.ArgumentNodeType.helper);
			doRedraw(true);
		}

		private void menuFileNew_Click(object sender, System.EventArgs e)
		{
			if(a.isModified())
				menuFileSave_Click(sender,e);  // Save if modified
			a = new ArgMapInterface(true,treeView1,richTextBox1);  // true means add sample map nodes
			a.loadTree();
			setAppTitle(a.currentfilename);
			this.vScrollBar1.Value = 0;  // reset scroll bars
			this.hScrollBar1.Value = 0;
			doRedraw(true);
		}

		private void menuEditAddReason_Click(object sender, System.EventArgs e)
		{
			// add reason below the current node
			a.addNodeToTree(Node.ArgumentNodeType.reason,"New Reason");
			richTextBox1.SelectAll();
			doRedraw(true);
		}

		private void menuEditAddObjection_Click(object sender, System.EventArgs e)
		{
			a.addNodeToTree(Node.ArgumentNodeType.objection,"New Objection");
			richTextBox1.SelectAll();
			doRedraw(true);
		}

		private void menuEditAddHelper_Click(object sender, System.EventArgs e)
		{
			a.addNodeToTree(Node.ArgumentNodeType.helper,"New Helper");
			richTextBox1.SelectAll();
			doRedraw(true);
		}

		private void menuEditDelete_Click(object sender, System.EventArgs e)
		{
			if(! this.richTextBox1.Focused) 
			{
				cmd.executeCommand("delete");
				doRedraw(true);
			}
			else  // sent to rich text box  //  implement undo
			{
				if(this.richTextBox1.SelectionLength>0)
					richTextBox1.SelectedText="";
				else
				{
					richTextBox1.SelectionLength=1;
					richTextBox1.SelectedText="";
				}
			}
		}

		private void menuItemProperties_Click(object sender, System.EventArgs e)
		{
			int depth = a.getDepth(this.treeView1.Nodes[0]);
			int wordcount = a.wordCount((Node)treeView1.Nodes[0].Tag);
			MessageBox.Show(
				"There are "+treeView1.GetNodeCount(true)+" elements. \n"+
				"Depth is "+depth+" levels down.\n"
				+"Word count is "+wordcount,
				"Argument: "+a.currentfilename
				);
		}

		private void menuEditMoveUp_Click(object sender, System.EventArgs e)
		{
			// Move up in the same node list
			a.moveNodeUpDown(ArgMapInterface.direction.up);
			doRedraw(true);
		}

		private void menuEditMoveDown_Click(object sender, System.EventArgs e)
		{
			// Move down in the same node list
			a.moveNodeUpDown(ArgMapInterface.direction.down);
			doRedraw(true);
		}

		private void menuEditMoveIn_Click(object sender, System.EventArgs e)
		{
			// Move to the parent node list
			a.moveNodeLeft();
			doRedraw(true);
		}

		private void menuEditMoveOut_Click(object sender, System.EventArgs e)
		{
			// Move to the note list of the previous node
			a.moveNodeRight();
			doRedraw(true);
		}

		private void treeView1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Control)
			{
				if(e.KeyCode==Keys.Down)
					menuEditMoveDown_Click(sender,e);
				else if(e.KeyCode==Keys.Up)
					menuEditMoveUp_Click(sender,e);
				else if(e.KeyCode==Keys.Left)
					menuEditMoveIn_Click(sender,e);
				else if(e.KeyCode==Keys.Right)
					menuEditMoveOut_Click(sender,e);
				
			}
		}
		
		/// <summary>
		/// Swaps the current node with its parent
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuEditSwap_Click(object sender, System.EventArgs e)
		{
			a.swapText(this.treeView1.SelectedNode);
			doRedraw(true);
		}

		private void menuItemExportCurrentNode_Click(object sender, System.EventArgs e)
		{
			a.exportCurrentNode();
		}
		
		private void panelGraphicalView_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			DrawTree d = new DrawTree(-this.hScrollBar1.Value,-this.vScrollBar1.Value,zoom);
			d.drawTree(e,a);  // should pass less later
			maxWidth = (int) d.MaxWidth;
			maxHeight = (int) d.MaxHeight;

			hScrollBar1.Maximum = maxWidth;
			vScrollBar1.Maximum = maxHeight;
		}

		private void menuItemPrint_Click(object sender, System.EventArgs e)
		{
			DialogResult d;
			PrintTree p;

			pageSetupDialog1.Document = a.Pd;
			d = pageSetupDialog1.ShowDialog();
			if(d == DialogResult.OK)
			{
				a.Pd = pageSetupDialog1.Document;		// save settings
				p = new PrintTree(true,a,1,9999);	// constructor does the printing
			}
		}

		private void menuFilePageSetup_Click(object sender, System.EventArgs e)
		{
			DialogResult d;

			pageSetupDialog1.Document = a.Pd;
			d = pageSetupDialog1.ShowDialog();
			if(d == DialogResult.OK)
				a.Pd = pageSetupDialog1.Document;
		}

		private void menuItemPrintGraphicView_Click(object sender, System.EventArgs e)
		{
			PrintTree p;
			p = new PrintTree(a,1,9999);
		}

		private void menuViewShowGraphical_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();
			if(menuViewShowGraphical.Checked)
			{
				menuViewShowGraphical.Checked = false;
				d.ShowGraphicalView = false;
				panelGraphicalView.Visible = false;
				doRedraw(false);
			}
			else
			{
				menuViewShowGraphical.Checked = true;
				d.ShowGraphicalView = true;
				panelGraphicalView.Visible = true;
				doRedraw(true);
			}
		}

		private void menuViewShowEdit_Click(object sender, System.EventArgs e)
		{
			if(menuViewShowEdit.Checked)  // remove tick
			{
				menuViewShowEdit.Checked = false;
				this.panel2.Visible = false;
				this.richTextBox1.Visible = false;
				if(! menuViewShowGraphical.Checked) // both now hidden
				{
					// treeView1.Anchor = treeView1.Anchor | AnchorStyles.Right;
					// panel1.Dock = DockStyle.Fill;
				}
			}
			else
			{
				menuViewShowEdit.Checked = true;
				panel2.Visible = true;
				richTextBox1.Visible = true;
			}
		}

		private void vScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			doRedraw(false);
		}

		private void hScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			doRedraw(false); 
		}

		
		private void menuEditPaste_Click(object sender, System.EventArgs e)
		{
			string s;
			TreeNode tn;

			if(this.richTextBox1.Focused)
			{
				richTextBox1.Paste();
				a.updateEditorText(richTextBox1);
			}
			else
			{
				tn = treeView1.SelectedNode;
				// check what is on the clipboard
				IDataObject data = Clipboard.GetDataObject();
				
				if (data.GetDataPresent("AXL"))
				{
					Argument arg = new Argument();
					s = data.GetData("AXL").ToString();
					arg = arg.loadXmlArgString(s);
					Node n,nn;
					n = (Node) treeView1.SelectedNode.Tag;
					nn = arg.findHead();
					if(nn.nodeType == Node.ArgumentNodeType.premise)  // cannot add a second premise
						nn.nodeType = Node.ArgumentNodeType.reason;
					n.addKid(nn);
					a.loadTree();  // recreates tree view dependent on the argument 
				}
				else
					if (data.GetDataPresent(DataFormats.Text))
				{
					s = data.GetData(DataFormats.Text).ToString();
					CutAndPaste cp = new CutAndPaste (this.treeView1);
					cp.pasteFromText(s);
				}
				a.verifyTree(false);
				doRedraw(true);
				a.Modified();
				treeView1.SelectedNode = tn;
				cmd.executeCommand((string) null);
			}
		}

		private void copyToClipboard()
		{
			CutAndPaste cp = new CutAndPaste(this.treeView1);
			cp.copyToClipboard(a.CurrentArg);
		}

		private void menuEditCopy_Click(object sender, System.EventArgs e)
		{
			if(this.richTextBox1.Focused)
				richTextBox1.Copy();
			else
			{
			copyToClipboard();
			}
		}

		private void menuEditCut_Click(object sender, System.EventArgs e)
		{
			if(this.richTextBox1.Focused)
				richTextBox1.Cut();
			else
			{
				copyToClipboard();
				menuEditDelete_Click(sender,e);
				doRedraw(true);
			}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(a.isModified())
			{
				DialogResult r;
				r = MessageBox.Show("Argument map has been modified.  Do you wish to save?","Save?",
					MessageBoxButtons.YesNoCancel);
				if(r == DialogResult.Yes)
					menuFileSave_Click(sender,e);
				else if(r == DialogResult.Cancel)
				{
					e.Cancel = true;
					return;
				}
			}
			//Options.OptionsData.saveOptions(this.menuFileRecentFiles,""); // save to the default file
		}

		private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;

			if(c==13)
			{
				e.Handled = true;  // drop this char
				a.addPeerNodeToTree(treeView1.SelectedNode);
				richTextBox1.SelectAll();
			}
			a.Modified();  // flag the argument as being modified
		}

		private void menuEditUndo_Click(object sender, System.EventArgs e)
		{
			cmd.undo();
			doRedraw(true);  // redraw so any change can be seen
		}

		private FindSpec findSpec = null;
		private void menuEditFind_Click(object sender, System.EventArgs e)
		{
			FormFind f;
			if(findSpec == null)
				findSpec = new FindSpec("",treeView1,true);
			f = new FormFind(treeView1,findSpec);
			f.Show();
		}

		private void menuFindNext_Click(object sender, System.EventArgs e)
		{
			bool r;

			r = findSpec.find();
			if(!r) MessageBox.Show("Cannot find \""+findSpec.searchFor+"\".","Find Next");
		}

		private void richTextBox1_Leave(object sender, System.EventArgs e)
		{
			string t;
			a.Modified();  // flag the argument as being modified
			t = "\n";
			if(richTextBox1.Text.EndsWith(t))
				richTextBox1.Text.Substring(0,richTextBox1.Text.Length-2);
			doRedraw(true);  // redraw graphical view
		}

		private void menuHelpContents_Click(object sender, System.EventArgs e)
		{
			// Open help file contents
			string helpfile = Application.StartupPath + "\\argumentative.chm";
			if(! System.IO.File.Exists(helpfile))
			{
				MessageBox.Show("Help file not found: "+helpfile);
				return;
			}
			// Help.ShowHelpIndex(this,helpfile);
			Help.ShowHelp(this,helpfile);
		}

		private void panelGraphicalView_Click(object sender, System.EventArgs e)
		{
			// mouse click in the Graphic View
			Point p;
			TreeNode tn;
			
			p = Control.MousePosition;		// where was clicked
			p = panelGraphicalView.PointToClient(p);	// relative to this panel
			p.X += this.hScrollBar1.Value;	// compensate for the scroll position
			p.Y += this.vScrollBar1.Value;
			p.X = (int) (p.X / zoom);					// compensate for zoom
			p.Y = (int) (p.Y / zoom);
			tn = a.getNodeAt(p);			// search for node
			if(tn != null)
			{
				treeView1.SelectedNode = tn;  // select if found
				this.treeView1.Focus();			// focus is on the tree view for consistency
			}
		}

		private void menuEditEdit_Click(object sender, System.EventArgs e)
		{
			// Moves the focus to the editing box and highlights the text ready to be replaced
			if(richTextBox1.Focus())
					richTextBox1.SelectAll();
		}

		private void menuItemCheckUpdates_Click(object sender, System.EventArgs e)
		{
			// launches a browser and calls a page that uses PHP to check the current number
			string url;
			url="http://argumentative.sourceforge.net/arg_resource/version.php?version=";
			url = url + current_version;
			System.Diagnostics.Process.Start(url);
		}

		private void menuItemEditProps_Click(object sender, System.EventArgs e)
		{
			// zz check to see if already open
			if(FormProperties.isActive)
				return;
			properties = new FormProperties(treeView1,a);
			properties.Show();
		}

		private void menuViewFindSelection_Click(object sender, System.EventArgs e)
		{
			// get the current selection
			Node n;
			TreeNode tn;
			tn = this.treeView1.SelectedNode;
			if(tn==null) return;  // nothing selected
			n = (Node) tn.Tag;
			hScrollBar1.Value = (int) n.x;
			vScrollBar1.Value = (int)n.y;
			doRedraw(false);
		}

		private void menuItemView200_Click(object sender, System.EventArgs e)
		{
			zoom = 2F;
			doRedraw(true);
		}

		private void menuView100_Click(object sender, System.EventArgs e)
		{
			zoom = 1F;
			doRedraw(true);
		}

		private void menuItemView50_Click(object sender, System.EventArgs e)
		{
			zoom = 0.5F;
			doRedraw(true);
		}

		private void menuViewFull_Click(object sender, System.EventArgs e)
		{
			// fill the panel with the image
			float width,height,mw,mh,margin;
			float zw,zh;
			width = (float) panelGraphicalView.Width - this.vScrollBar1.Width;		// get viewable area
			height = (float) panelGraphicalView.Height - this.hScrollBar1.Height;
			mw = (float) maxWidth;				// get drawing area dimensions from last redraw
			mh = (float) maxHeight;
			margin = 2F * DrawTree.margin;
			zw = (width )  / (mw + margin);
			zh = (height) / (mh + margin);
			zoom = Math.Min(zw,zh);
			// zoom = width / (float) maxWidth;
			this.vScrollBar1.Value=0;
			this.hScrollBar1.Value=0;
			doRedraw(true);
		}

		private const float zoomIncrement = 0.25F;

		private void menuViewZoomIn_Click(object sender, System.EventArgs e)
		{
			// Add .25 (25% to zoom)
			int z = (int) (zoom / zoomIncrement);
			zoom = zoomIncrement * (float) (z + 1);
			doRedraw(true);
		}

		private void menuViewZoomOut_Click(object sender, System.EventArgs e)
		{
			// Subtract .25 (25% to zoom)
			
			int z = (int) (zoom / zoomIncrement);
			zoom = zoomIncrement * (float) (z - 1);
			if(zoom < zoomIncrement)
				zoom = zoomIncrement;
			doRedraw(true);
		}

		// ********************************* QUICK VIEWS *****************************


		private void menuViewQuickColourful_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();

			a.setStandardOptions(d,DrawTree.joinType.dogleg,DrawTree.arrowType.none,
				DrawTree.notationType.boxes,DrawTree.treeOrientationType.top_down,true);
			doRedraw(true);
		}

		private void menuViewQuickBoxes_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();

			a.setStandardOptions(d,DrawTree.joinType.dogleg,DrawTree.arrowType.none,
				DrawTree.notationType.boxes,DrawTree.treeOrientationType.top_down,false);
			doRedraw(true);
		}

		private void menuViewQuickRnotation_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();

			a.setStandardOptions(d,DrawTree.joinType.direct,DrawTree.arrowType.end,
				DrawTree.notationType.Rnotation,DrawTree.treeOrientationType.bottom_up,false);
			doRedraw(true);
		}
		

		private void menuViewQuickPreferred_Click(object sender, System.EventArgs e)
		{
			// load predefined list
			string fileName;
			fileName = Application.StartupPath + "\\Preferred.xml";
			prefs p = new prefs();
			if(! Options.OptionsData.loadGraphical(fileName,p))
				MessageBox.Show("Cannot find "+fileName+". Use Tools/Save Options first","Error");
			doRedraw(true);
		}

		private int nextQuickView = 2;  // Next quick view is Colourful. Boxes is the default
		private void menuViewQuickCycle_Click(object sender, System.EventArgs e)
		{
			if(nextQuickView == 1)
				menuViewQuickBoxes_Click(sender,e);
			else if(nextQuickView==2)
				menuViewQuickColourful_Click(sender,e);
			else if(nextQuickView==3)
				menuViewQuickRnotation_Click(sender,e);
			else if(nextQuickView==4)
			{
				menuViewQuickPreferred_Click(sender,e);
				nextQuickView = 0; // back to the beginning, less one.
			}
			nextQuickView++;
		}

		// **************************** ORIENTATION **********************************

		private void menuViewOrientationTopDown_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();
			d.TreeOrientation = DrawTree.treeOrientationType.top_down;
			doRedraw(true);
		}

		private void menuViewOrientationBottomUp_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();
			d.TreeOrientation = DrawTree.treeOrientationType.bottom_up;
			doRedraw(true);
		}

		private void menuViewOrientationLeftRightClick(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();
			d.TreeOrientation = DrawTree.treeOrientationType.left_to_right;
			doRedraw(true);
		}

		private void menuViewOrientationRightLeft_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();
			d.TreeOrientation = DrawTree.treeOrientationType.right_to_left;
			doRedraw(true);
		}

		private void menuViewOrientationCycle_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();
			if(d.TreeOrientation == DrawTree.treeOrientationType.top_down)
				d.TreeOrientation = DrawTree.treeOrientationType.bottom_up;
			else if(d.TreeOrientation == DrawTree.treeOrientationType.bottom_up)
				d.TreeOrientation = DrawTree.treeOrientationType.left_to_right;
			else if(d.TreeOrientation == DrawTree.treeOrientationType.left_to_right)
				d.TreeOrientation = DrawTree.treeOrientationType.right_to_left;
			else if(d.TreeOrientation == DrawTree.treeOrientationType.right_to_left)
				d.TreeOrientation = DrawTree.treeOrientationType.top_down;

			doRedraw(true);
		}

		// ********************** Recent menu function ************************

		private void recentFile_Click(object sender, System.EventArgs e)
		{
			MenuItem m = (MenuItem) sender;
			// open file name
			a = new ArgMapInterface(false,treeView1,richTextBox1);
			a.loadAXLorRE3(m.Text);
			setAppTitle(m.Text);
			addFileToRecent(m.Text);
			doRedraw(true);
		}
		private void addFileToRecent(string fileName)
		{
//			Options.OptionsData d = Options.OptionsData.getInstance();
//			MenuItem m;
//
//			// m = a.addFileToRecent(fileName,menuFileRecentFiles,d.RecentFiles);
//			// will not work with ToolStripMenuItem
//			if(m != null)
//				m.Click += new System.EventHandler(this.recentFile_Click);

		}

		private void menuViewExpColExpandAll_Click(object sender, System.EventArgs e)
		{
			this.treeView1.ExpandAll();
		}

		private void menuViewExpColCollapseAll_Click(object sender, System.EventArgs e)
		{
			this.treeView1.CollapseAll();
		}

		private void menuEditFullStops_Click(object sender, System.EventArgs e)
		{
			FormFullStops ffs = new FormFullStops(a);

			if( ffs.ShowDialog() == DialogResult.OK)
				this.doRedraw(true);
		}

		TreeNode spellTreeNode;  // Node presently being checked
		ArrayList flatNode;
		ArrayList starts;

		private string calcLines()
		{
			int start;
			TreeNode tn;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			flatNode = new ArrayList();
			starts = new ArrayList();

			start=0;
			tn = treeView1.Nodes[0];
			while(tn != null)
			{
				sb.Append(tn.Text); sb.Append("\n");
				starts.Add(start);
				start += tn.Text.Length + "\n".Length;
				flatNode.Add(tn);
				tn = tn.NextVisibleNode;
			}
			return sb.ToString();
		}
		private void menuToolsSpelling_Click(object sender, System.EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.getInstance();

			this.wordDictionary.DictionaryFolder = Application.StartupPath;
			this.wordDictionary.DictionaryFile = d.Dictionary;

			treeView1.ExpandAll();  // Required for NextVisibleNode to work
			// Create string of whole argument
			

			spellTreeNode = this.treeView1.SelectedNode;  // get Premise
			// this.spelling.Text = spellTreeNode.Text;
			this.spelling.Text = calcLines();
			this.spelling.AlertComplete = false;  // do not show the Spelling Complete dialog box
			this.spelling.SpellCheck();
		}

		private void spelling_EndOfText(object sender, System.EventArgs e)
		{
			doRedraw(true);
			a.updateEditorText(richTextBox1);
			MessageBox.Show("Spell Check Complete","Spelling");
			return;  // redraw all
		}

		private void spelling_ReplacedWord(object sender, NetSpell.SpellChecker.ReplaceWordEventArgs e)
		{
			int start,length,i;
			Node n;
			TreeNode tn;
			string s;

			
			start = e.TextIndex;
			i=0;
			while(i < starts.Count && start >= (int) starts[i])  // find the first line start greater than the word position
				i++;
			if(i>0)
				start -= (int) starts[i-1];

			length = e.Word.Length;
			tn = (TreeNode) flatNode[i-1];
			s = tn.Text;
			s = s.Remove(start,length);
			s = s.Insert(start,e.ReplacementWord);

			tn.Text = s;
			n = (Node) tn.Tag;
			n.EditorText = s;

			calcLines();
		}

		private void spelling_DeletedWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
		{
			int start,length,i;
			Node n;
			TreeNode tn;
			string s;

			start = e.TextIndex;
			i=0;
			while(i < starts.Count && start >= (int) starts[i])  // find the first line start greater than the word position
				i++;
			if(i>0)
				start -= (int) starts[i-1];

			length = e.Word.Length;
			tn = (TreeNode) flatNode[i-1];
			s = tn.Text;
			s = s.Remove(start,length);

			tn.Text = s;
			n = (Node) tn.Tag;
			n.EditorText = s;

			calcLines();
		}

		private void menuToolsSaveOptions_Click(object sender, System.EventArgs e)
		{
			// Options.OptionsData.saveOptions(this.menuFileRecentFiles);
			string directory;

			saveFileDialog1.Filter = "Argumentative prefs XML|*.xml|All Files|*.*";
			saveFileDialog1.Title = "Save Prefs file";
			saveFileDialog1.FilterIndex = 1; // first entry i.e. *.XML
			directory = saveFileDialog1.InitialDirectory;
			this.saveFileDialog1.FileName="Preferred.xml";
			saveFileDialog1.InitialDirectory = Application.StartupPath;  // where the prefs are stored
			if(saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Options.OptionsData.saveOptions(null,saveFileDialog1.FileName);
			}
			// restore initial directory
			saveFileDialog1.InitialDirectory = directory;
		}

		private void menuItemHomePage_Click(object sender, System.EventArgs e)
		{
			// launches a browser and opens the home page
			System.Diagnostics.Process.Start("http://argumentative.sourceforge.net/");
		}

		private void menuToolsNewInterface_Click(object sender, System.EventArgs e)
		{
			MainForm mp;
			mp = new MainForm("");
			mp.Show();
		}


	}
}
