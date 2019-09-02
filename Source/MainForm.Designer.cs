/*
 * Created by SharpDevelop.
 * User: John
 * Date: 27/05/2007
 * Time: 12:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Argumentative
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("A Helper for the reason");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("A Reason", new System.Windows.Forms.TreeNode[] {
									treeNode1});
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("A Helper for the objection");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("An Objection", new System.Windows.Forms.TreeNode[] {
									treeNode3});
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Main Premise", new System.Windows.Forms.TreeNode[] {
									treeNode2,
									treeNode4});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mSWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mSPowerpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reasonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.objectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.downToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemIn = new System.Windows.Forms.ToolStripMenuItem();
			this.outToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.graphicalViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
			this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.inToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.outToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoom50toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoom100toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoom200toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quickViewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plainBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.colourfulToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rNotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.blackAndWhiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.preferredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cycleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.orientationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.topDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bottomUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.leftToRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rightToLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cycleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.expandCollapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.spellingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fullStopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.homePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.graphicalViewPanel = new System.Windows.Forms.Panel();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPrintPreview = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCut = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPaste = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonLeft = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonUp = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDown = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRight = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonProperties = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonReason = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonObjection = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonHelper = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
			this.spelling = new NetSpell.SpellChecker.Spelling(this.components);
			this.wordDictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.menuStrip1.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.graphicalViewPanel.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.editToolStripMenuItem,
									this.viewToolStripMenuItem,
									this.toolsToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.ShowItemToolTips = true;
			this.menuStrip1.Size = new System.Drawing.Size(701, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.newToolStripMenuItem,
									this.openToolStripMenuItem,
									this.toolStripMenuItem1,
									this.saveToolStripMenuItem,
									this.saveAsToolStripMenuItem,
									this.toolStripMenuItem2,
									this.printToolStripMenuItem,
									this.printPreviewToolStripMenuItem,
									this.pageSetupToolStripMenuItem,
									this.toolStripMenuItem3,
									this.exportToolStripMenuItem,
									this.propertiesToolStripMenuItem,
									this.recentFilesToolStripMenuItem,
									this.toolStripMenuItem4,
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.openToolStripMenuItem.Text = "&Open...";
			this.openToolStripMenuItem.ToolTipText = "Open file";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.printToolStripMenuItem.Text = "&Print...";
			this.printToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripMenuItemClick);
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Preview";
			this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.PrintPreviewToolStripMenuItemClick);
			// 
			// pageSetupToolStripMenuItem
			// 
			this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
			this.pageSetupToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.pageSetupToolStripMenuItem.Text = "Page Setup";
			this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.PageSetupToolStripMenuItemClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(160, 6);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.mSWordToolStripMenuItem,
									this.mSPowerpointToolStripMenuItem,
									this.transformToolStripMenuItem});
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.exportToolStripMenuItem.Text = "&Export";
			// 
			// mSWordToolStripMenuItem
			// 
			this.mSWordToolStripMenuItem.Name = "mSWordToolStripMenuItem";
			this.mSWordToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.mSWordToolStripMenuItem.Text = "MS &Word...";
			this.mSWordToolStripMenuItem.ToolTipText = "Export to RTF and launch word";
			this.mSWordToolStripMenuItem.Click += new System.EventHandler(this.MSWordToolStripMenuItemClick);
			// 
			// mSPowerpointToolStripMenuItem
			// 
			this.mSPowerpointToolStripMenuItem.Name = "mSPowerpointToolStripMenuItem";
			this.mSPowerpointToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.mSPowerpointToolStripMenuItem.Text = "MS &PowerPoint...";
			this.mSPowerpointToolStripMenuItem.Click += new System.EventHandler(this.MSPowerpointToolStripMenuItemClick);
			// 
			// transformToolStripMenuItem
			// 
			this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
			this.transformToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.transformToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.transformToolStripMenuItem.Text = "Transform";
			this.transformToolStripMenuItem.Click += new System.EventHandler(this.TransformToolStripMenuItemClick);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItemClick);
			// 
			// recentFilesToolStripMenuItem
			// 
			this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
			this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.recentFilesToolStripMenuItem.Text = "Recent Files";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(160, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.undoToolStripMenuItem,
									this.toolStripMenuItem5,
									this.cutToolStripMenuItem,
									this.copyToolStripMenuItem,
									this.pasteToolStripMenuItem,
									this.deleteToolStripMenuItem,
									this.toolStripMenuItem6,
									this.editToolStripMenuItem1,
									this.addToolStripMenuItem,
									this.moveToolStripMenuItem,
									this.propertiesToolStripMenuItem1,
									this.toolStripMenuItem7,
									this.findToolStripMenuItem,
									this.findNextToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItemClick);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(167, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItemClick);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItemClick);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(167, 6);
			// 
			// editToolStripMenuItem1
			// 
			this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
			this.editToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.editToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
			this.editToolStripMenuItem1.Text = "Edit";
			this.editToolStripMenuItem1.Click += new System.EventHandler(this.EditToolStripMenuItem1Click);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.reasonToolStripMenuItem,
									this.objectionToolStripMenuItem,
									this.helperToolStripMenuItem});
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.addToolStripMenuItem.Text = "Add";
			// 
			// reasonToolStripMenuItem
			// 
			this.reasonToolStripMenuItem.Name = "reasonToolStripMenuItem";
			this.reasonToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
			this.reasonToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.reasonToolStripMenuItem.Text = "Reason";
			this.reasonToolStripMenuItem.Click += new System.EventHandler(this.ReasonToolStripMenuItemClick);
			// 
			// objectionToolStripMenuItem
			// 
			this.objectionToolStripMenuItem.Name = "objectionToolStripMenuItem";
			this.objectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)));
			this.objectionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.objectionToolStripMenuItem.Text = "Objection";
			this.objectionToolStripMenuItem.Click += new System.EventHandler(this.ObjectionToolStripMenuItemClick);
			// 
			// helperToolStripMenuItem
			// 
			this.helperToolStripMenuItem.Name = "helperToolStripMenuItem";
			this.helperToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.helperToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.helperToolStripMenuItem.Text = "Helper";
			this.helperToolStripMenuItem.Click += new System.EventHandler(this.HelperToolStripMenuItemClick);
			// 
			// moveToolStripMenuItem
			// 
			this.moveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.upToolStripMenuItem,
									this.downToolStripMenuItem,
									this.ToolStripMenuItemIn,
									this.outToolStripMenuItem1});
			this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
			this.moveToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.moveToolStripMenuItem.Text = "Move";
			// 
			// upToolStripMenuItem
			// 
			this.upToolStripMenuItem.Name = "upToolStripMenuItem";
			this.upToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
			this.upToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.upToolStripMenuItem.Text = "Up";
			this.upToolStripMenuItem.Click += new System.EventHandler(this.UpToolStripMenuItemClick);
			// 
			// downToolStripMenuItem
			// 
			this.downToolStripMenuItem.Name = "downToolStripMenuItem";
			this.downToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
			this.downToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.downToolStripMenuItem.Text = "Down";
			this.downToolStripMenuItem.Click += new System.EventHandler(this.DownToolStripMenuItemClick);
			// 
			// ToolStripMenuItemIn
			// 
			this.ToolStripMenuItemIn.Name = "ToolStripMenuItemIn";
			this.ToolStripMenuItemIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
			this.ToolStripMenuItemIn.Size = new System.Drawing.Size(171, 22);
			this.ToolStripMenuItemIn.Text = "In";
			this.ToolStripMenuItemIn.Click += new System.EventHandler(this.InToolStripMenuItem1Click);
			// 
			// outToolStripMenuItem1
			// 
			this.outToolStripMenuItem1.Name = "outToolStripMenuItem1";
			this.outToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
			this.outToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
			this.outToolStripMenuItem1.Text = "Out";
			this.outToolStripMenuItem1.Click += new System.EventHandler(this.OutToolStripMenuItem1Click);
			// 
			// propertiesToolStripMenuItem1
			// 
			this.propertiesToolStripMenuItem1.Name = "propertiesToolStripMenuItem1";
			this.propertiesToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.propertiesToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
			this.propertiesToolStripMenuItem1.Text = "Properties";
			this.propertiesToolStripMenuItem1.Click += new System.EventHandler(this.PropertiesToolStripMenuItem1Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(167, 6);
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.findToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.findToolStripMenuItem.Text = "Find...";
			this.findToolStripMenuItem.Click += new System.EventHandler(this.FindToolStripMenuItemClick);
			// 
			// findNextToolStripMenuItem
			// 
			this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
			this.findNextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.findNextToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.findNextToolStripMenuItem.Text = "Find Next";
			this.findNextToolStripMenuItem.Click += new System.EventHandler(this.FindNextToolStripMenuItemClick);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.showToolStripMenuItem,
									this.zoomToolStripMenuItem,
									this.quickViewsToolStripMenuItem,
									this.orientationToolStripMenuItem,
									this.expandCollapseToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// showToolStripMenuItem
			// 
			this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.graphicalViewToolStripMenuItem,
									this.editAreaToolStripMenuItem,
									this.toolBarToolStripMenuItem,
									this.toolStripMenuItem12,
									this.selectionToolStripMenuItem});
			this.showToolStripMenuItem.Name = "showToolStripMenuItem";
			this.showToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.showToolStripMenuItem.Text = "Show";
			// 
			// graphicalViewToolStripMenuItem
			// 
			this.graphicalViewToolStripMenuItem.Checked = true;
			this.graphicalViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.graphicalViewToolStripMenuItem.Name = "graphicalViewToolStripMenuItem";
			this.graphicalViewToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.graphicalViewToolStripMenuItem.Text = "Graphical View";
			this.graphicalViewToolStripMenuItem.Click += new System.EventHandler(this.GraphicalViewToolStripMenuItemClick);
			// 
			// editAreaToolStripMenuItem
			// 
			this.editAreaToolStripMenuItem.Checked = true;
			this.editAreaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.editAreaToolStripMenuItem.Name = "editAreaToolStripMenuItem";
			this.editAreaToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.editAreaToolStripMenuItem.Text = "Edit Area";
			this.editAreaToolStripMenuItem.Click += new System.EventHandler(this.EditAreaToolStripMenuItemClick);
			// 
			// toolBarToolStripMenuItem
			// 
			this.toolBarToolStripMenuItem.Checked = true;
			this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
			this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.toolBarToolStripMenuItem.Text = "Tool Bar";
			this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItemClick);
			// 
			// toolStripMenuItem12
			// 
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			this.toolStripMenuItem12.Size = new System.Drawing.Size(151, 6);
			// 
			// selectionToolStripMenuItem
			// 
			this.selectionToolStripMenuItem.AutoToolTip = true;
			this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
			this.selectionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.selectionToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.selectionToolStripMenuItem.Text = "Selection";
			this.selectionToolStripMenuItem.ToolTipText = "Shows the element selected in the tree view in the upper right of the graphical v" +
			"iew.";
			this.selectionToolStripMenuItem.Click += new System.EventHandler(this.SelectionToolStripMenuItemClick);
			// 
			// zoomToolStripMenuItem
			// 
			this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fullToolStripMenuItem,
									this.inToolStripMenuItem,
									this.outToolStripMenuItem,
									this.zoom50toolStripMenuItem,
									this.zoom100toolStripMenuItem,
									this.zoom200toolStripMenuItem});
			this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
			this.zoomToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.zoomToolStripMenuItem.Text = "Zoom";
			// 
			// fullToolStripMenuItem
			// 
			this.fullToolStripMenuItem.Name = "fullToolStripMenuItem";
			this.fullToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.fullToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.fullToolStripMenuItem.Text = "Full";
			this.fullToolStripMenuItem.Click += new System.EventHandler(this.FullToolStripMenuItemClick);
			// 
			// inToolStripMenuItem
			// 
			this.inToolStripMenuItem.Name = "inToolStripMenuItem";
			this.inToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.inToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.inToolStripMenuItem.Text = "In";
			this.inToolStripMenuItem.Click += new System.EventHandler(this.InToolStripMenuItemClick);
			// 
			// outToolStripMenuItem
			// 
			this.outToolStripMenuItem.Name = "outToolStripMenuItem";
			this.outToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.outToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.outToolStripMenuItem.Text = "Out";
			this.outToolStripMenuItem.Click += new System.EventHandler(this.OutToolStripMenuItemClick);
			// 
			// zoom50toolStripMenuItem
			// 
			this.zoom50toolStripMenuItem.Name = "zoom50toolStripMenuItem";
			this.zoom50toolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
			this.zoom50toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.zoom50toolStripMenuItem.Text = "50%";
			this.zoom50toolStripMenuItem.Click += new System.EventHandler(this.Zoom50toolStripMenuItemClick);
			// 
			// zoom100toolStripMenuItem
			// 
			this.zoom100toolStripMenuItem.Name = "zoom100toolStripMenuItem";
			this.zoom100toolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.zoom100toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.zoom100toolStripMenuItem.Text = "100%";
			this.zoom100toolStripMenuItem.Click += new System.EventHandler(this.Zoom100toolStripMenuItemClick);
			// 
			// zoom200toolStripMenuItem
			// 
			this.zoom200toolStripMenuItem.Name = "zoom200toolStripMenuItem";
			this.zoom200toolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.zoom200toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.zoom200toolStripMenuItem.Text = "200%";
			this.zoom200toolStripMenuItem.Click += new System.EventHandler(this.Zoom200toolStripMenuItemClick);
			// 
			// quickViewsToolStripMenuItem
			// 
			this.quickViewsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.plainBoxesToolStripMenuItem,
									this.colourfulToolStripMenuItem,
									this.rNotationToolStripMenuItem,
									this.blackAndWhiteToolStripMenuItem,
									this.preferredToolStripMenuItem,
									this.cycleToolStripMenuItem});
			this.quickViewsToolStripMenuItem.Name = "quickViewsToolStripMenuItem";
			this.quickViewsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.quickViewsToolStripMenuItem.Text = "Quick Views";
			// 
			// plainBoxesToolStripMenuItem
			// 
			this.plainBoxesToolStripMenuItem.Name = "plainBoxesToolStripMenuItem";
			this.plainBoxesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.plainBoxesToolStripMenuItem.Text = "Plain Boxes";
			this.plainBoxesToolStripMenuItem.Click += new System.EventHandler(this.PlainBoxesToolStripMenuItemClick);
			// 
			// colourfulToolStripMenuItem
			// 
			this.colourfulToolStripMenuItem.Name = "colourfulToolStripMenuItem";
			this.colourfulToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.colourfulToolStripMenuItem.Text = "Colourful";
			this.colourfulToolStripMenuItem.Click += new System.EventHandler(this.ColourfulToolStripMenuItemClick);
			// 
			// rNotationToolStripMenuItem
			// 
			this.rNotationToolStripMenuItem.Name = "rNotationToolStripMenuItem";
			this.rNotationToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.rNotationToolStripMenuItem.Text = "R Notation";
			this.rNotationToolStripMenuItem.Click += new System.EventHandler(this.RNotationToolStripMenuItemClick);
			// 
			// blackAndWhiteToolStripMenuItem
			// 
			this.blackAndWhiteToolStripMenuItem.Name = "blackAndWhiteToolStripMenuItem";
			this.blackAndWhiteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.blackAndWhiteToolStripMenuItem.Text = "Black and White";
			this.blackAndWhiteToolStripMenuItem.Click += new System.EventHandler(this.BlackAndWhiteToolStripMenuItemClick);
			// 
			// preferredToolStripMenuItem
			// 
			this.preferredToolStripMenuItem.Name = "preferredToolStripMenuItem";
			this.preferredToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.preferredToolStripMenuItem.Text = "Preferred";
			this.preferredToolStripMenuItem.Click += new System.EventHandler(this.PreferredToolStripMenuItemClick);
			// 
			// cycleToolStripMenuItem
			// 
			this.cycleToolStripMenuItem.Name = "cycleToolStripMenuItem";
			this.cycleToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.cycleToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.cycleToolStripMenuItem.Text = "Cycle";
			this.cycleToolStripMenuItem.Click += new System.EventHandler(this.CycleToolStripMenuItemClick);
			// 
			// orientationToolStripMenuItem
			// 
			this.orientationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.topDownToolStripMenuItem,
									this.bottomUpToolStripMenuItem,
									this.leftToRightToolStripMenuItem,
									this.rightToLeftToolStripMenuItem,
									this.cycleToolStripMenuItem1});
			this.orientationToolStripMenuItem.Name = "orientationToolStripMenuItem";
			this.orientationToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.orientationToolStripMenuItem.Text = "Orientation";
			// 
			// topDownToolStripMenuItem
			// 
			this.topDownToolStripMenuItem.Name = "topDownToolStripMenuItem";
			this.topDownToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.topDownToolStripMenuItem.Text = "Top Down";
			this.topDownToolStripMenuItem.Click += new System.EventHandler(this.TopDownToolStripMenuItemClick);
			// 
			// bottomUpToolStripMenuItem
			// 
			this.bottomUpToolStripMenuItem.Name = "bottomUpToolStripMenuItem";
			this.bottomUpToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.bottomUpToolStripMenuItem.Text = "Bottom Up";
			this.bottomUpToolStripMenuItem.Click += new System.EventHandler(this.BottomUpToolStripMenuItemClick);
			// 
			// leftToRightToolStripMenuItem
			// 
			this.leftToRightToolStripMenuItem.Name = "leftToRightToolStripMenuItem";
			this.leftToRightToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.leftToRightToolStripMenuItem.Text = "Left to Right";
			this.leftToRightToolStripMenuItem.Click += new System.EventHandler(this.LeftToRightToolStripMenuItemClick);
			// 
			// rightToLeftToolStripMenuItem
			// 
			this.rightToLeftToolStripMenuItem.Name = "rightToLeftToolStripMenuItem";
			this.rightToLeftToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.rightToLeftToolStripMenuItem.Text = "Right to Left";
			this.rightToLeftToolStripMenuItem.Click += new System.EventHandler(this.RightToLeftToolStripMenuItemClick);
			// 
			// cycleToolStripMenuItem1
			// 
			this.cycleToolStripMenuItem1.Name = "cycleToolStripMenuItem1";
			this.cycleToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.cycleToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
			this.cycleToolStripMenuItem1.Text = "Cycle";
			this.cycleToolStripMenuItem1.Click += new System.EventHandler(this.CycleToolStripMenuItem1Click);
			// 
			// expandCollapseToolStripMenuItem
			// 
			this.expandCollapseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.expandAllToolStripMenuItem,
									this.collapseAllToolStripMenuItem});
			this.expandCollapseToolStripMenuItem.Name = "expandCollapseToolStripMenuItem";
			this.expandCollapseToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.expandCollapseToolStripMenuItem.Text = "Expand / Collapse";
			// 
			// expandAllToolStripMenuItem
			// 
			this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
			this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.expandAllToolStripMenuItem.Text = "Expand All";
			this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.ExpandAllToolStripMenuItemClick);
			// 
			// collapseAllToolStripMenuItem
			// 
			this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
			this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.collapseAllToolStripMenuItem.Text = "Collapse All";
			this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.CollapseAllToolStripMenuItemClick);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.spellingToolStripMenuItem,
									this.fullStopsToolStripMenuItem,
									this.toolStripMenuItem8,
									this.optionsToolStripMenuItem,
									this.saveOptionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// spellingToolStripMenuItem
			// 
			this.spellingToolStripMenuItem.Name = "spellingToolStripMenuItem";
			this.spellingToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
			this.spellingToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.spellingToolStripMenuItem.Text = "Spelling...";
			this.spellingToolStripMenuItem.Click += new System.EventHandler(this.SpellingToolStripMenuItemClick);
			// 
			// fullStopsToolStripMenuItem
			// 
			this.fullStopsToolStripMenuItem.Name = "fullStopsToolStripMenuItem";
			this.fullStopsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.fullStopsToolStripMenuItem.Text = "Full Stops...";
			this.fullStopsToolStripMenuItem.Click += new System.EventHandler(this.FullStopsToolStripMenuItemClick);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(201, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
									| System.Windows.Forms.Keys.O)));
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.optionsToolStripMenuItem.Text = "Options...";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
			// 
			// saveOptionsToolStripMenuItem
			// 
			this.saveOptionsToolStripMenuItem.Name = "saveOptionsToolStripMenuItem";
			this.saveOptionsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.saveOptionsToolStripMenuItem.Text = "Save Options...";
			this.saveOptionsToolStripMenuItem.Click += new System.EventHandler(this.SaveOptionsToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.contentsToolStripMenuItem,
									this.checkForUpdatesToolStripMenuItem,
									this.homePageToolStripMenuItem,
									this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// contentsToolStripMenuItem
			// 
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.contentsToolStripMenuItem.Text = "Contents...";
			this.contentsToolStripMenuItem.Click += new System.EventHandler(this.ContentsToolStripMenuItemClick);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates...";
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItemClick);
			// 
			// homePageToolStripMenuItem
			// 
			this.homePageToolStripMenuItem.Name = "homePageToolStripMenuItem";
			this.homePageToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.homePageToolStripMenuItem.Text = "Home page...";
			this.homePageToolStripMenuItem.Click += new System.EventHandler(this.HomePageToolStripMenuItemClick);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.aboutToolStripMenuItem.Text = "About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// treeView
			// 
			this.treeView.AllowDrop = true;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.HideSelection = false;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.imageList;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			treeNode1.ImageIndex = 4;
			treeNode1.Name = "Node0";
			treeNode1.Text = "A Helper for the reason";
			treeNode2.ImageIndex = 2;
			treeNode2.Name = "Node1";
			treeNode2.Text = "A Reason";
			treeNode3.ImageIndex = 4;
			treeNode3.Name = "Node1";
			treeNode3.Text = "A Helper for the objection";
			treeNode4.ImageIndex = 3;
			treeNode4.Name = "Node2";
			treeNode4.Text = "An Objection";
			treeNode5.ImageIndex = 1;
			treeNode5.Name = "Node0";
			treeNode5.Text = "Main Premise";
			this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
									treeNode5});
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(317, 410);
			this.treeView.TabIndex = 3;
			this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterCollapse);
			this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeViewDragDrop);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterSelect);
			this.treeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeViewDragEnter);
			this.treeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TreeViewKeyPress);
			this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeViewBeforeSelect);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterExpand);
			this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeViewItemDrag);
			this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeViewDragOver);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "default.bmp");
			this.imageList.Images.SetKeyName(1, "premise.bmp");
			this.imageList.Images.SetKeyName(2, "reason.bmp");
			this.imageList.Images.SetKeyName(3, "objection.bmp");
			this.imageList.Images.SetKeyName(4, "helper.bmp");
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1});
			this.statusStrip.Location = new System.Drawing.Point(0, 459);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(701, 22);
			this.statusStrip.TabIndex = 4;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 49);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(701, 410);
			this.splitContainer1.SplitterDistance = 317;
			this.splitContainer1.TabIndex = 5;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.richTextBox1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.graphicalViewPanel);
			this.splitContainer2.Size = new System.Drawing.Size(380, 410);
			this.splitContainer2.SplitterDistance = 53;
			this.splitContainer2.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(380, 53);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			this.toolTip1.SetToolTip(this.richTextBox1, "Edit area");
			this.richTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RichTextBox1KeyPress);
			this.richTextBox1.TextChanged += new System.EventHandler(this.RichTextBox1TextChanged);
			// 
			// graphicalViewPanel
			// 
			this.graphicalViewPanel.AllowDrop = true;
			this.graphicalViewPanel.BackColor = System.Drawing.Color.White;
			this.graphicalViewPanel.Controls.Add(this.hScrollBar1);
			this.graphicalViewPanel.Controls.Add(this.vScrollBar1);
			this.graphicalViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphicalViewPanel.Location = new System.Drawing.Point(0, 0);
			this.graphicalViewPanel.Name = "graphicalViewPanel";
			this.graphicalViewPanel.Size = new System.Drawing.Size(380, 353);
			this.graphicalViewPanel.TabIndex = 0;
			this.graphicalViewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphicalViewPanelPaint);
			this.graphicalViewPanel.Click += new System.EventHandler(this.GraphicalViewPanelClick);
			this.graphicalViewPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.GraphicalViewPanelDragDrop);
			this.graphicalViewPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphicalViewPanelMouseDown);
			this.graphicalViewPanel.Resize += new System.EventHandler(this.GraphicalViewPanelResize);
			this.graphicalViewPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.GraphicalViewPanelDragEnter);
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.hScrollBar1.LargeChange = 20;
			this.hScrollBar1.Location = new System.Drawing.Point(0, 336);
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(363, 17);
			this.hScrollBar1.SmallChange = 10;
			this.hScrollBar1.TabIndex = 1;
			this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HScrollBar1Scroll);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.LargeChange = 40;
			this.vScrollBar1.Location = new System.Drawing.Point(363, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 353);
			this.vScrollBar1.SmallChange = 10;
			this.vScrollBar1.TabIndex = 0;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScrollBar1Scroll);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripButtonOpen,
									this.toolStripButtonSave,
									this.toolStripButtonPrint,
									this.toolStripButtonPrintPreview,
									this.toolStripButtonDelete,
									this.toolStripButtonCut,
									this.toolStripButtonCopy,
									this.toolStripButtonPaste,
									this.toolStripButtonLeft,
									this.toolStripButtonUp,
									this.toolStripButtonDown,
									this.toolStripButtonRight,
									this.toolStripButtonProperties,
									this.toolStripButtonReason,
									this.toolStripButtonObjection,
									this.toolStripButtonHelper});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(701, 25);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonOpen
			// 
			this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
			this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpen.Name = "toolStripButtonOpen";
			this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonOpen.Text = "Open";
			this.toolStripButtonOpen.Click += new System.EventHandler(this.ToolStripButtonOpenClick);
			// 
			// toolStripButtonSave
			// 
			this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
			this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSave.Name = "toolStripButtonSave";
			this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSave.Text = "Save";
			// 
			// toolStripButtonPrint
			// 
			this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrint.Image")));
			this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPrint.Name = "toolStripButtonPrint";
			this.toolStripButtonPrint.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonPrint.Text = "Print";
			this.toolStripButtonPrint.Click += new System.EventHandler(this.ToolStripButtonPrintClick);
			// 
			// toolStripButtonPrintPreview
			// 
			this.toolStripButtonPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrintPreview.Image")));
			this.toolStripButtonPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPrintPreview.Name = "toolStripButtonPrintPreview";
			this.toolStripButtonPrintPreview.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonPrintPreview.Text = "Print Preview";
			this.toolStripButtonPrintPreview.Click += new System.EventHandler(this.ToolStripButtonPrintPreviewClick);
			// 
			// toolStripButtonDelete
			// 
			this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
			this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDelete.Name = "toolStripButtonDelete";
			this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonDelete.Text = "Delete";
			this.toolStripButtonDelete.Click += new System.EventHandler(this.ToolStripButtonDeleteClick);
			// 
			// toolStripButtonCut
			// 
			this.toolStripButtonCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonCut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCut.Image")));
			this.toolStripButtonCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCut.Name = "toolStripButtonCut";
			this.toolStripButtonCut.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonCut.Text = " Cut";
			this.toolStripButtonCut.Click += new System.EventHandler(this.ToolStripButtonCutClick);
			// 
			// toolStripButtonCopy
			// 
			this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopy.Image")));
			this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCopy.Name = "toolStripButtonCopy";
			this.toolStripButtonCopy.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonCopy.Text = "Copy";
			this.toolStripButtonCopy.Click += new System.EventHandler(this.ToolStripButtonCopyClick);
			// 
			// toolStripButtonPaste
			// 
			this.toolStripButtonPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPaste.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPaste.Image")));
			this.toolStripButtonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPaste.Name = "toolStripButtonPaste";
			this.toolStripButtonPaste.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonPaste.Text = "Paste";
			this.toolStripButtonPaste.Click += new System.EventHandler(this.ToolStripButtonPasteClick);
			// 
			// toolStripButtonLeft
			// 
			this.toolStripButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonLeft.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLeft.Image")));
			this.toolStripButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonLeft.Name = "toolStripButtonLeft";
			this.toolStripButtonLeft.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonLeft.Text = "Move Left";
			this.toolStripButtonLeft.Click += new System.EventHandler(this.ToolStripButtonLeftClick);
			// 
			// toolStripButtonUp
			// 
			this.toolStripButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonUp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUp.Image")));
			this.toolStripButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonUp.Name = "toolStripButtonUp";
			this.toolStripButtonUp.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonUp.Text = "Move Up";
			this.toolStripButtonUp.Click += new System.EventHandler(this.ToolStripButtonUpClick);
			// 
			// toolStripButtonDown
			// 
			this.toolStripButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDown.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDown.Image")));
			this.toolStripButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDown.Name = "toolStripButtonDown";
			this.toolStripButtonDown.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonDown.Text = "Move Down";
			this.toolStripButtonDown.Click += new System.EventHandler(this.ToolStripButtonDownClick);
			// 
			// toolStripButtonRight
			// 
			this.toolStripButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRight.Image")));
			this.toolStripButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRight.Name = "toolStripButtonRight";
			this.toolStripButtonRight.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRight.Text = "Move Right";
			this.toolStripButtonRight.Click += new System.EventHandler(this.ToolStripButtonRightClick);
			// 
			// toolStripButtonProperties
			// 
			this.toolStripButtonProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonProperties.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProperties.Image")));
			this.toolStripButtonProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonProperties.Name = "toolStripButtonProperties";
			this.toolStripButtonProperties.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonProperties.Text = "Edit Properties";
			this.toolStripButtonProperties.Click += new System.EventHandler(this.ToolStripButtonPropertiesClick);
			// 
			// toolStripButtonReason
			// 
			this.toolStripButtonReason.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonReason.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReason.Image")));
			this.toolStripButtonReason.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonReason.Name = "toolStripButtonReason";
			this.toolStripButtonReason.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonReason.Text = "Reason";
			this.toolStripButtonReason.Click += new System.EventHandler(this.ToolStripButtonReasonClick);
			// 
			// toolStripButtonObjection
			// 
			this.toolStripButtonObjection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonObjection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonObjection.Image")));
			this.toolStripButtonObjection.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonObjection.Name = "toolStripButtonObjection";
			this.toolStripButtonObjection.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonObjection.Text = "Objection";
			this.toolStripButtonObjection.Click += new System.EventHandler(this.ToolStripButtonObjectionClick);
			// 
			// toolStripButtonHelper
			// 
			this.toolStripButtonHelper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonHelper.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHelper.Image")));
			this.toolStripButtonHelper.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonHelper.Name = "toolStripButtonHelper";
			this.toolStripButtonHelper.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonHelper.Text = "Helper";
			this.toolStripButtonHelper.Click += new System.EventHandler(this.ToolStripButtonHelperClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// spelling
			// 
			this.spelling.Dictionary = this.wordDictionary;
			this.spelling.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.spelling_EndOfText);
			this.spelling.DeletedWord += new NetSpell.SpellChecker.Spelling.DeletedWordEventHandler(this.spelling_DeletedWord);
			this.spelling.ReplacedWord += new NetSpell.SpellChecker.Spelling.ReplacedWordEventHandler(this.spelling_ReplacedWord);
			this.spelling.MisspelledWord += new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(this.spelling_MisspelledWord);
			// 
			// wordDictionary
			// 
			this.wordDictionary.DictionaryFile = "en-AU.dic";
			// 
			// toolTip1
			// 
			this.toolTip1.IsBalloon = true;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(701, 481);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.graphicalViewPanel.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem blackAndWhiteToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonPrintPreview;
		private System.Windows.Forms.ToolStripButton toolStripButtonPrint;
		private System.Windows.Forms.ToolTip toolTip1;
		private NetSpell.SpellChecker.Dictionary.WordDictionary wordDictionary;
		private NetSpell.SpellChecker.Spelling spelling;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cycleToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem rightToLeftToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem leftToRightToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bottomUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem topDownToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripMenuItem plainBoxesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cycleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem preferredToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rNotationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem colourfulToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoom100toolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoom200toolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoom50toolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
		private System.Windows.Forms.ToolStripMenuItem helperToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem objectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reasonToolStripMenuItem;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
		private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mSPowerpointToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mSWordToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonProperties;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIn;
		private System.Windows.Forms.ToolStripButton toolStripButtonUp;
		private System.Windows.Forms.ToolStripButton toolStripButtonDown;
		private System.Windows.Forms.ToolStripMenuItem outToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem homePageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveOptionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem fullStopsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem spellingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem expandCollapseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem orientationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quickViewsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem outToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem inToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fullToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editAreaToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem graphicalViewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolStripButton toolStripButtonHelper;
		private System.Windows.Forms.ToolStripButton toolStripButtonObjection;
		private System.Windows.Forms.ToolStripButton toolStripButtonReason;
		private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
		private System.Windows.Forms.ToolStripButton toolStripButtonCut;
		private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
		private System.Windows.Forms.ToolStripButton toolStripButtonPaste;
		private System.Windows.Forms.ToolStripButton toolStripButtonLeft;
		private System.Windows.Forms.ToolStripButton toolStripButtonRight;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Panel graphicalViewPanel;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ToolStripButton toolStripButtonSave;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
		private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
	}
}
