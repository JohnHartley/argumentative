/*
 * Created by SharpDevelop.
 * User: John
 * Date: 27/05/2007
 * Time: 12:58 AM
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;  // For ImageFormat enumeration
using System.Collections;
using System.IO;

namespace Argumentative
{
	/// <summary>
	/// The primary application form
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>Primary form for the application</summary>
		/// <param name="commandline">File to open</param>
		public MainForm(string commandline)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			Options.OptionsData.init(this.recentFilesToolStripMenuItem);		// start options sub-system
			// Add events to recent files menu
			int f;
			for(f=0;f<recentFilesToolStripMenuItem.DropDownItems.Count;f++)
				recentFilesToolStripMenuItem.DropDownItems[f].Click += new System.EventHandler(recentFile_Click);
			
			ami = new ArgMapInterface(true,treeView,richTextBox1);
			ami.loadTree();
			Options.OptionsData d = Options.OptionsData.Instance;
			// set tree font and colour
			this.treeView.Font = d.TreeFont;
			this.treeView.ForeColor = d.TreeColour;
			cmd = new Command();
			deleteCommand dc = new deleteCommand(ami);
			cmd.loadCommand(dc);
			if(!commandline.Equals(""))
			{
				ami = new ArgMapInterface(false,treeView,richTextBox1);
				ami.loadAXLorRE3(commandline);
				doRedraw(true);
				this.addFileToRecent(ami.CurrentFilename);
			}
			setAppTitle(ami.CurrentFilename);
			this.WindowState = FormWindowState.Maximized;
			I18N.init("");
			
		}
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				string f="";
				if(args.Length==1)  // a file name has been specified
				{
					f=args[0];  // this will be a DOS 8.3 style file name
					FileInfo fi = new FileInfo(f);
					if(!fi.Exists)
						f = "";
					else
						f = fi.FullName;  // Full file name with spaces etc.
				}
				
				Application.Run(new MainForm(f));
			}
			catch (Exception e)
			{
				MessageBox.Show(String.Format("Sorry, some unhandled error - please report this error to "+
				                              "Bugs found on http://sourceforge.net/projects/argumentative/\n{0}",e));
			}
		}
		
		// Global variables and objects
		private const string current_version = "0.5.55";
		/// <summary>Main interface object</summary>
		private ArgMapInterface ami;
		private FormProperties properties;  // modal dialog box
		private Command cmd;  // TODO Use the mechanism in .Net 2 for undo
		private bool recalc;  // some change has been made to the data structuure
		private float zoom = 1F;
		private int maxWidth, maxHeight;  // TODO should be redundant
		private bool textUpdated = false;
		
		private void setAppTitle(string fileName)
		{
			fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
			this.Text = "Argumentative - " + fileName;
		}
		
		/// <summary>Sets the status line at the bottom of the main screen</summary>
		/// <param name="text">Text string to display</param>
		private void setStatus(string text)
		{
			toolStripStatusLabel1.Text = text;
		}

		private void doRedraw(bool recalc)
		{
			this.recalc = recalc;
			this.graphicalViewPanel.Invalidate();
		}
		
		void GraphicalViewPanelPaint(object sender, PaintEventArgs e)
		{
			DrawTree d = new DrawTree(-this.hScrollBar1.Value,-this.vScrollBar1.Value,zoom);
			d.drawTree(e,ami);
			maxWidth = (int) d.MaxWidth;
			maxHeight = (int) d.MaxHeight;

			hScrollBar1.Maximum = maxWidth +  ((int)DrawTree.margin * 2);
			vScrollBar1.Maximum = maxHeight + ((int)DrawTree.margin * 2);
			
			hScrollBar1.LargeChange = (int) (graphicalViewPanel.Size.Width / zoom);
			vScrollBar1.LargeChange = (int) (graphicalViewPanel.Size.Height / zoom);
			
			// Show / hide scroll bars
			hScrollBar1.Visible = graphicalViewPanel.Size.Width < maxWidth * zoom;
			vScrollBar1.Visible = graphicalViewPanel.Size.Height < maxHeight * zoom;
			// For debug
//			setStatus("Height: "+graphicalViewPanel.Size.Height.ToString()+" < "+(maxHeight * zoom).ToString()+
//			               "  Width: "+graphicalViewPanel.Size.Width.ToString()+" < "+(maxWidth * zoom).ToString());
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void OpenToolStripMenuItemClick(object sender, EventArgs e)
		{
			DialogResult d;
			d = ami.askToSave();
			if(d == DialogResult.Yes)
				SaveToolStripMenuItemClick(sender,e);
			else if(d == DialogResult.Cancel)
				return;
			
			ArgMapInterface ai = null;
			try
			{
				ai = ami.openArgument();
			} catch (Exception ex)
			{
				MessageBox.Show(String.Format("Error occured while opening the selected file. The file may be corrupt.\n",ex.Message));
				ami = new ArgMapInterface(true,this.treeView,this.richTextBox1);
				ami.loadTree();
			}
			if(ai != null)
			{
				ami = ai;
				zoom = 1F;
				addFileToRecent(ami.CurrentFilename);
				setAppTitle(ami.CurrentFilename);
				setStatus(String.Format("{0} opened.",ami.CurrentFilename));
			}
			doRedraw(true);  // redraw and recalc
		}
		
		void ToolStripButtonOpenClick(object sender, EventArgs e)
		{
			OpenToolStripMenuItemClick(sender,e);
		}
		
		void RichTextBox1TextChanged(object sender, EventArgs e)
		{
			if(ami==null) return;
			if(ami.editBoxChange((RichTextBox) this.richTextBox1))
			{
				setStatus(String.Format("Text set to: {0}",richTextBox1.Text));
				textUpdated = true;
				doRedraw(true);
			}
		}
		
		void UpToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Move up in the same node list
			ami.moveNodeUpDown(ArgMapInterface.direction.up);
			doRedraw(true);
		}
		
		void ToolStripButtonUpClick(object sender, EventArgs e)
		{
			ami.moveNodeUpDown(ArgMapInterface.direction.up);
			doRedraw(true);
		}
		
		void DownToolStripMenuItemClick(object sender, EventArgs e)
		{
			ami.moveNodeUpDown(ArgMapInterface.direction.down);
			doRedraw(true);
		}
		
		void ToolStripButtonDownClick(object sender, EventArgs e)
		{
			ami.moveNodeUpDown(ArgMapInterface.direction.down);
			doRedraw(true);
		}
		
		private void skipWordInTextBox(bool forward)
		{
			int start;
			string s;
			start = richTextBox1.SelectionStart;
			s = richTextBox1.Text;
			if(forward)
			{
				if(richTextBox1.SelectionLength != 0)
					start += richTextBox1.SelectionLength;
				if(start >= s.Length)
					return;
				while(start < s.Length && s[start] != ' ')  // skip characters
					++start;
				while(start < s.Length && s[start] == ' ')
					++start;
			}
			else // backwards
			{
				if(start==0)
					return;
				while(start > 0 && s[start-1] == ' ')  // skip space
					start--;
				while(start > 0 && s[start-1] != ' ')  // skip characters
					start--;
			}
			richTextBox1.SelectionLength = 0;
			richTextBox1.SelectionStart = start;
		}
		
		void InToolStripMenuItem1Click(object sender, EventArgs e)
		{
			if(richTextBox1.Focused)	// ignore if editing
			{
				skipWordInTextBox(false);  // move back one word
				return;
			}
			
			// Move to the parent node list
			ami.moveNodeLeft();
			doRedraw(true);
		}
		
		void ToolStripButtonLeftClick(object sender, EventArgs e)
		{
			// Move to the parent node list
			ami.moveNodeLeft();
			doRedraw(true);
		}
		
		void OutToolStripMenuItem1Click(object sender, EventArgs e)
		{
			if(this.richTextBox1.Focused)	// ignore if editing
			{
				skipWordInTextBox(true);  // move forward one word
				return;
			}
			// Move to the note list of the previous node
			ami.moveNodeRight();
			doRedraw(true);
		}
		
		void ToolStripButtonRightClick(object sender, EventArgs e)
		{
			// Move to the note list of the previous node
			ami.moveNodeRight();
			doRedraw(true);
		}
		
		private void copyToClipboard()
		{
			CutAndPaste cp = new CutAndPaste(treeView);
			cp.copyToClipboard(ami.CurrentArg);
		}
		
		void CutToolStripMenuItemClick(object sender, EventArgs e)
		{
			
			if(this.richTextBox1.Focused)
				richTextBox1.Cut();
			else
			{
				copyToClipboard();
				DeleteToolStripMenuItemClick(sender,e);
			}
		}
		
		void ToolStripButtonCutClick(object sender, EventArgs e)
		{
			CutToolStripMenuItemClick(sender,e);
		}
		
		void SetToolbarButtons()
		{
			Node n;
			if(this.treeView.SelectedNode == null) return;
			n = (Node) treeView.SelectedNode.Tag;
			// update toolbar
			toolStripButtonReason.Checked		= n.nodeType==Node.ArgumentNodeType.reason;
			toolStripButtonObjection.Checked	= n.nodeType==Node.ArgumentNodeType.objection;
			toolStripButtonHelper.Checked		= n.nodeType==Node.ArgumentNodeType.helper;

		}
		
		private void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode tn;
			Node n;
			
			tn = treeView.SelectedNode;
			if(tn==null) return;		// no selected node
			n = (Node) tn.Tag;
			richTextBox1.Text = tn.Text;
			Font f = richTextBox1.SelectionFont;
			if( (!f.Name.Equals("Microsoft Sans Serif")) | (f.Size != 8.25F) )
			{
				richTextBox1.SelectAll();
				Font ff = new Font("Microsoft Sans Serif",8.25F);
				richTextBox1.SelectionFont = ff;
			}
			SetToolbarButtons();
			// Set status
			string typeText = n.ArgumentNodeTypeString();
			// TODO referenceTree here is slow.  Update references on change
			ami.CurrentArg.referenceTree();
			setStatus(String.Format("{0} {1}, {2}",n.getRef(),typeText,n.EditorText));
			
			// Change properties box if open
			if(properties != null)
			{
				properties.changeSelection(tn);
			}
			
		}
		
		private void TreeViewBeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			// Selection is about to change
			
			Node n;
			
			if(treeView.SelectedNode == null) return;
			n = (Node) treeView.SelectedNode.Tag;
			if(textUpdated)
			{
				textUpdated = false;
				doRedraw(true);
			}
		}
		
		void CopyToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(this.richTextBox1.Focused)
				richTextBox1.Copy();
			else
				copyToClipboard();
		}
		
		void ToolStripButtonCopyClick(object sender, EventArgs e)
		{
			CopyToolStripMenuItemClick(sender,e);
		}
		
		void ToolStripButtonPasteClick(object sender, EventArgs e)
		{
			PasteToolStripMenuItemClick(sender,e);
		}
		
		void PasteToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(this.richTextBox1.Focused)
			{
				richTextBox1.Paste();
				ami.updateEditorText(richTextBox1);
			}
			else
			{
				CutAndPaste cp = new CutAndPaste(treeView);
				try
				{
					if(cp.paste())
						ami.loadTree();
				} catch (Exception ex)
				{
					MessageBox.Show(String.Format("Sorry, there was an error while attempting paste. Try copying the text in the edit area. ",
					                              ex.StackTrace));
				}
				ami.verifyTree(false);
				doRedraw(true);
				ami.Modified();
				cmd.executeCommand((string) null);
			}
		}
		
		void DeleteToolStripMenuItemClick(object sender, EventArgs e)
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
		
		void ToolStripButtonDeleteClick(object sender, EventArgs e)
		{
			DeleteToolStripMenuItemClick(sender,e);
		}
		
		void changeTo(Node.ArgumentNodeType nt)
		{
			// Set current tree item to Reason
			if(ami.changeNodeto(nt))
				doRedraw(true);
			else
			{
				string reason;
				reason = ami.canChangeNodeReason(nt);
				MessageBox.Show(reason);
				this.setStatus(reason);
			}
			SetToolbarButtons();
		}
		
		void ToolStripButtonReasonClick(object sender, EventArgs e)
		{
			// Set current tree item to Reason
			changeTo(Node.ArgumentNodeType.reason);
		}
		
		void ToolStripButtonObjectionClick(object sender, EventArgs e)
		{
			changeTo(Node.ArgumentNodeType.objection);
		}
		
		void ToolStripButtonHelperClick(object sender, EventArgs e)
		{
			changeTo(Node.ArgumentNodeType.helper);
		}
		
		void PropertiesToolStripMenuItem1Click(object sender, EventArgs e)
		{
			// Check to see if already open
			if(FormProperties.isActive)
				return;
			properties = new FormProperties(treeView,ami);
			properties.Show();
		}
		
		void ToolStripButtonPropertiesClick(object sender, EventArgs e)
		{
			PropertiesToolStripMenuItem1Click(sender,e);
		}
		
		// ********************** Drag & Drop ******************
		
		void TreeViewItemDrag(object sender, ItemDragEventArgs e)
		{
			// Move the dragged node when the left mouse button is used.
			if (e.Button == MouseButtons.Left)
			{
				DoDragDrop(e.Item, DragDropEffects.Move);
			}

			// Copy the dragged node when the right mouse button is used.
			else if (e.Button == MouseButtons.Right)
			{
				DoDragDrop(e.Item, DragDropEffects.Copy);
			}
		}
		
		void TreeViewDragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.AllowedEffect;
		}
		
		void TreeViewDragOver(object sender, DragEventArgs e)
		{
			// Retrieve the client coordinates of the mouse position.
			Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));

			// Select the node at the mouse position.
			treeView.SelectedNode = treeView.GetNodeAt(targetPoint);
		}
		
		void TreeViewDragDrop(object sender, DragEventArgs e)
		{
			// Retrieve the client coordinates of the drop location.
			Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));

			// Retrieve the node at the drop location.
			TreeNode targetNode = treeView.GetNodeAt(targetPoint);

			// Retrieve the node that was dragged.
			TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			System.Diagnostics.Debug.Assert(draggedNode != null);

			// Confirm that the node at the drop location is not
			// the dragged node or a descendant of the dragged node.
			if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
			{
				// If it is a move operation, remove the node from its current
				// location and add it to the node at the drop location.
				if (e.Effect == DragDropEffects.Move)
				{
					draggedNode.Remove();
					targetNode.Nodes.Add(draggedNode);
				}

				// If it is a copy operation, clone the dragged node
				// and add it to the node at the drop location.
				else if (e.Effect == DragDropEffects.Copy)
				{
					targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
				}

				// Expand the node at the location
				// to show the dropped node.
				targetNode.Expand();
				ami.relinkArgument(this.treeView.Nodes[0]);
				this.doRedraw(true);
				this.ami.Modified();
			}
		}
		
		// Determine whether one node is a parent
		// or ancestor of a second node.
		private bool ContainsNode(TreeNode node1, TreeNode node2)
		{
			// Check the parent node of the second node.
			if (node2.Parent == null) return false;
			if (node2.Parent.Equals(node1)) return true;

			// If the parent node is not null or equal to the first node,
			// call the ContainsNode method recursively using the parent of
			// the second node.
			return ContainsNode(node1, node2.Parent);
		}

		// ****************** File save routines **************
		
		private void recentFile_Click(object sender, System.EventArgs e)
		{
			ToolStripMenuItem m = (ToolStripMenuItem) sender;
			// open file name
			ami = new ArgMapInterface(false,treeView,richTextBox1);
			ami.loadAXLorRE3(m.Text);
			setAppTitle(m.Text);
			addFileToRecent(m.Text);
			doRedraw(true);
		}
		private void addFileToRecent(string fileName)
		{
			Options.OptionsData d = Options.OptionsData.Instance;
			ToolStripMenuItem m;

			m = ami.addFileToRecentList(fileName,recentFilesToolStripMenuItem,d.RecentFiles);
			System.Diagnostics.Debug.Assert(m != null,"Could not add recent menu item.");
			m.Click += new System.EventHandler(this.recentFile_Click);
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Save
			if(ami.CurrentFilename.Equals(Argument.UndefinedFile))
				SaveAsToolStripMenuItemClick(sender,e);  // same as Save As for the moment
			else
			{
				ami.CurrentArg.saveArg(ami.CurrentFilename,null,false);
				setAppTitle(ami.CurrentFilename);  // Set the window title
			}
		}
		
		void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			// TODO Move to argmapinterface
			DialogResult r;
			string fileName;
			Node n;
			SaveFileDialog saveFileDialog1;
			saveFileDialog1 = new SaveFileDialog();

			n = ami.CurrentArg.findHead();
			// saveFileDialog1.Filter = "Argumentative XML|*.axl|Bitmap|*.bmp|PNG|*.png|JPEG|*.jpg|GIF|*.gif|Rationale 1.1|*.rtnl";
			saveFileDialog1.Filter = Argument.saveFilter;
			saveFileDialog1.Title = "Save an Argument File";
			if(ami != null && !ami.CurrentFilename.Equals(""))
			{
				fileName = ami.CurrentFilename;
				if(fileName.Equals(Argument.UndefinedFile))
					fileName = ami.CurrentArg.findHead().EditorText;  // premise text
				fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);  // +".axl";  missing extension is AXL
				saveFileDialog1.FileName = fileName;
				saveFileDialog1.FilterIndex = (int) Argument.fileIndex.axl;  // Make sure file type is known
			}
			r = saveFileDialog1.ShowDialog();
			
			if(saveFileDialog1.FileName == "")
				return;
			if(r==DialogResult.OK)
			{
				fileName = saveFileDialog1.FileName;
				
				ami.CurrentArg.saveAs(fileName,saveFileDialog1.FilterIndex);
				
				addFileToRecent(fileName);
				setAppTitle(fileName);  // Set the window title
				setStatus(String.Format("{0} saved.",fileName));
			}
		}
		
		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(ami.isModified()){
				DialogResult r;
				r = MessageBox.Show(this,"Do you wish to save this map?","Save?",MessageBoxButtons.YesNo);
				if(r == DialogResult.Yes)
					SaveToolStripMenuItemClick(sender,e);  // Save if modified
			}
			ami = new ArgMapInterface(true,treeView,richTextBox1);  // true means add sample map nodes
			ami.loadTree();
			setAppTitle(ami.CurrentFilename);
			this.vScrollBar1.Value = 0;  // reset scroll bars
			this.hScrollBar1.Value = 0;
			zoom = 1;
			doRedraw(true);
			this.setStatus("New Argument");
		}
		
		void PrintToolStripMenuItemClick(object sender, EventArgs e)
		{
			DialogResult d;
			PrintTree p;
			
			PrintDialogWithOptions myPrintDialog;
			
			myPrintDialog = new PrintDialogWithOptions();
			d = myPrintDialog.ShowDialog();

			if(d == DialogResult.OK)
			{
				// TODO Implement page range
				int count;
				for(count=1;count<=myPrintDialog.copies;count++)
				{
					if(myPrintDialog.printGraphics)
					{
						p = new PrintTree(ami,myPrintDialog.fromPage,myPrintDialog.toPage,myPrintDialog.showPageNumbers,myPrintDialog.SinglePage);
						p.print();
					}
					else
					{
						p = new PrintTree(true,ami,myPrintDialog.fromPage,myPrintDialog.toPage,myPrintDialog.showPageNumbers);	// constructor does the printing
						p.print();
					}
					p.release();
				}
			}
			else if(d == DialogResult.Abort)  // signals print preview
			{
				printPreview(myPrintDialog.fromPage,myPrintDialog.toPage,myPrintDialog.showPageNumbers,myPrintDialog.SinglePage);
			}
			
		}
		
		void printPreview(int rangeStart,int rangeEnd,bool showPageNumbers,bool singlePage)
		{
			PrintTree p;
			PrintPreviewDialog ppd;
			
			p = new PrintTree(ami,rangeStart,rangeEnd,showPageNumbers,singlePage);
			ppd = new PrintPreviewDialog();
			ppd.Document = ami.Pd;
			ppd.ShowDialog();
			p.release();
		}
		
		void PrintPreviewToolStripMenuItemClick(object sender, EventArgs e)
		{
			printPreview(1,9999,true,false);
		}
		
		void ToolStripButtonPrintPreviewClick(object sender, EventArgs e)
		{
			PrintPreviewToolStripMenuItemClick(sender,e);
		}
		
		void PageSetupToolStripMenuItemClick(object sender, EventArgs e)
		{
			DialogResult d;

			pageSetupDialog1.Document = ami.Pd;
			d = pageSetupDialog1.ShowDialog();
			if(d == DialogResult.OK)
				ami.Pd = pageSetupDialog1.Document;
		}
		
		void ReasonToolStripMenuItemClick(object sender, EventArgs e)
		{
			// add reason below the current node
			ami.Modified();
			ami.addNodeToTree(Node.ArgumentNodeType.reason,"New Reason");
			richTextBox1.SelectAll();
			doRedraw(true);
		}
		
		void ObjectionToolStripMenuItemClick(object sender, EventArgs e)
		{
			ami.Modified();
			ami.addNodeToTree(Node.ArgumentNodeType.objection,"New Objection");
			richTextBox1.SelectAll();
			doRedraw(true);
		}
		
		void HelperToolStripMenuItemClick(object sender, EventArgs e)
		{
			ami.Modified();
			ami.addNodeToTree(Node.ArgumentNodeType.helper,"New Helper");
			richTextBox1.SelectAll();
			doRedraw(true);
		}
		
		void EditToolStripMenuItem1Click(object sender, EventArgs e)
		{
			if(richTextBox1.Focused)
				doRedraw(true);
			// Moves the focus to the editing box and highlights the text ready to be replaced
			else if(richTextBox1.Focus())
				richTextBox1.SelectAll();
		}
		
		// View / Show menu actions
		
		void GraphicalViewToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.graphicalViewPanel.Visible = ! graphicalViewToolStripMenuItem.Checked;
			graphicalViewToolStripMenuItem.Checked = graphicalViewPanel.Visible;
		}
		
		void EditAreaToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.richTextBox1.Visible = ! this.editAreaToolStripMenuItem.Checked;
			this.editAreaToolStripMenuItem.Checked = this.richTextBox1.Visible;
		}
		
		void ToolBarToolStripMenuItemClick(object sender, EventArgs e)
		{
			toolStrip1.Visible = ! this.toolBarToolStripMenuItem.Checked;
			this.toolBarToolStripMenuItem.Checked = toolStrip1.Visible;
		}
		
		private FindSpec findSpec = null;
		void FindToolStripMenuItemClick(object sender, EventArgs e)
		{
			FormFind f;
			if(findSpec == null)
				findSpec = new FindSpec("",treeView,true);
			f = new FormFind(treeView,findSpec);
			f.Show();
		}
		
		void FindNextToolStripMenuItemClick(object sender, EventArgs e)
		{
			bool r;

			if(findSpec == null) return;
			r = findSpec.find();
			if(!r) MessageBox.Show(String.Format("Cannot find \"{0}\".",findSpec.SearchFor),"Find Next");
		}
		
		// ***********   Zoom menu items ***************
		
		private const float zoomIncrement = 0.25F;
		
		void FullToolStripMenuItemClick(object sender, EventArgs e)
		{
			// fill the panel with the image
			float width,height,mw,mh,margin;
			float zw,zh;
			width = (float) this.graphicalViewPanel.Width - this.vScrollBar1.Width;		// get viewable area
			height = (float) graphicalViewPanel.Height - this.hScrollBar1.Height;
			mw = (float) maxWidth;				// get drawing area dimensions from last redraw
			mh = (float) maxHeight;
			margin = 2F * DrawTree.margin;
			zw = (width )  / (mw + margin);
			zh = (height) / (mh + margin);
			zoom = Math.Min(zw,zh);
			// zoom = width / (float) maxWidth;
			this.vScrollBar1.Value=0;
			this.hScrollBar1.Value=0;
			doRedraw(true);	// TODO Try recalc false
			setStatus(String.Format("Zoom full page. Zoom : {0}",zoom.ToString()));
		}
		
		void InToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Add .25 (25% to zoom)
			int z = (int) (zoom / zoomIncrement);
			zoom = zoomIncrement * (float) (z + 1);
			doRedraw(true);
			setStatus(String.Format("Zoom : {0}",zoom.ToString()));
		}
		
		void OutToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Subtract .25 (25% to zoom)
			
			int z = (int) (zoom / zoomIncrement);
			zoom = zoomIncrement * (float) (z - 1);
			if(zoom < zoomIncrement)
				zoom = zoomIncrement;
			doRedraw(true);
			setStatus(String.Format("Zoom : {0}",zoom.ToString()));
		}
		
		void Zoom50toolStripMenuItemClick(object sender, EventArgs e)
		{
			zoom = 0.5F;
			doRedraw(true);
			setStatus(String.Format("Zoom : {0}",zoom.ToString()));
		}
		
		void Zoom100toolStripMenuItemClick(object sender, EventArgs e)
		{
			zoom = 1F;
			doRedraw(true);
			setStatus(String.Format("Zoom : {0}",zoom.ToString()));
		}
		
		void Zoom200toolStripMenuItemClick(object sender, EventArgs e)
		{
			zoom = 2F;
			doRedraw(true);
			setStatus(String.Format("Zoom : {0}",zoom.ToString()));
		}
		// ********************************* QUICK VIEWS *****************************
		void PlainBoxesToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;

			ami.setStandardOptions(d,DrawTree.joinType.dogleg,DrawTree.arrowType.none,
			                       DrawTree.notationType.boxes,DrawTree.treeOrientationType.top_down,
			                       false,false,false);
			doRedraw(true);
			setStatus("Quick view set to Plain Boxes");
		}
		
		void ColourfulToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;

			ami.setStandardOptions(d,DrawTree.joinType.dogleg,DrawTree.arrowType.none,
			                       DrawTree.notationType.boxes,DrawTree.treeOrientationType.top_down,
			                       true,false,false);
			doRedraw(true);
			setStatus("Quick view set to Colourful");
		}
		
		void RNotationToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;

			ami.setStandardOptions(d,DrawTree.joinType.direct,DrawTree.arrowType.end,
			                       DrawTree.notationType.Rnotation,DrawTree.treeOrientationType.bottom_up,
			                       false,false,false);
			doRedraw(true);
			setStatus("Quick view set to R Notation");
		}
		
		
		void BlackAndWhiteToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;

			ami.setStandardOptions(d,DrawTree.joinType.dogleg,DrawTree.arrowType.none,
			                       DrawTree.notationType.boxes,DrawTree.treeOrientationType.top_down,
			                       false,false,true);
			doRedraw(true);
			setStatus("Quick view set to Black and White");
		}
		
		void PreferredToolStripMenuItemClick(object sender, EventArgs e)
		{
			// load predefined list
			string fileName;
			fileName = Application.StartupPath + "\\Preferred.xml";
			Prefs p = new Prefs();
			if(! Options.OptionsData.loadGraphical(fileName,p))
				setStatus(String.Format("Cannot find {0}. Use Tools/Save Options first",fileName));
			else
				setStatus(String.Format("Quick view set to Preferred ({0}).",fileName));  // TODO Use String.Format for setStatus
			doRedraw(true);
		}
		
		private int nextQuickView = 2;  // Next quick view is Colourful. Boxes is the default
		void CycleToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(nextQuickView == 1)
				PlainBoxesToolStripMenuItemClick(sender,e);
			else if(nextQuickView==2)
				ColourfulToolStripMenuItemClick(sender,e);
			else if(nextQuickView==3)
				RNotationToolStripMenuItemClick(sender,e);
			else if(nextQuickView==4)
				BlackAndWhiteToolStripMenuItemClick(sender,e);
			else if(nextQuickView==5)
			{
				PreferredToolStripMenuItemClick(sender,e);
				nextQuickView = 0; // back to the beginning, less one.
			}
			nextQuickView++;
		}
		
		void PropertiesToolStripMenuItemClick(object sender, EventArgs e)
		{
			// display properties of the current argument.
			
			int depth		= ami.CurrentArg.getDepth(null);
			int wordcount	= ami.CurrentArg.wordCount(null);
			int elements	= ami.CurrentArg.nodeCount(null);
			MessageBox.Show(
				"There are "+elements+" elements. \n"+
				"Depth is "+depth+" levels down.\n"+
				"Word count is "+wordcount+".\n"+
				"Zoom is "+zoom,
				"Argument: "+ami.CurrentFilename
			);
			setStatus(elements+" elements, "+
			          depth+" depth, "+wordcount+" words in argument "+
			          "Zoom is "+zoom+" "+
			          ami.CurrentFilename);
		}
		
		// **************************** ORIENTATION **********************************
		void TopDownToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;
			d.TreeOrientation = DrawTree.treeOrientationType.top_down;
			doRedraw(true);
			setStatus("View set to Top Down");
		}
		
		void BottomUpToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;
			d.TreeOrientation = DrawTree.treeOrientationType.bottom_up;
			doRedraw(true);
			setStatus("View set to Bottom Up");
		}
		
		void LeftToRightToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;
			d.TreeOrientation = DrawTree.treeOrientationType.left_to_right;
			doRedraw(true);
			toolStripStatusLabel1.Text = "View set to Left to Right";
		}
		
		void RightToLeftToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;
			d.TreeOrientation = DrawTree.treeOrientationType.right_to_left;
			doRedraw(true);
			setStatus("View set to Right to Left");
		}
		
		void CycleToolStripMenuItem1Click(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;
			if(d.TreeOrientation == DrawTree.treeOrientationType.top_down)
				BottomUpToolStripMenuItemClick(sender,e);
			else if(d.TreeOrientation == DrawTree.treeOrientationType.bottom_up)
				LeftToRightToolStripMenuItemClick(sender,e);
			else if(d.TreeOrientation == DrawTree.treeOrientationType.left_to_right)
				RightToLeftToolStripMenuItemClick(sender,e);
			else if(d.TreeOrientation == DrawTree.treeOrientationType.right_to_left)
				this.TopDownToolStripMenuItemClick(sender,e);
		}
		
		void ExpandCollapseToolStripMenuItemClick(object sender, EventArgs e)
		{
			// TODO code missing?
		}
		
		void ExpandAllToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.treeView.ExpandAll();
			setStatus("All elements in the Tree View Expanded");
		}
		
		void CollapseAllToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.treeView.CollapseAll();
			setStatus("All elements in the Tree View Collapsed");
		}
		
		// ****************** Spelling ************************
		
		TreeNode spellTreeNode;  // Node presently being checked
		ArrayList flatNode;
		ArrayList starts;
		int spellingErrorsFound;
		
		void SpellingToolStripMenuItemClick(object sender, EventArgs e)
		{
			Options.OptionsData d = Options.OptionsData.Instance;

			NetSpell.SpellChecker.Dictionary.WordDictionary dictionary;
			dictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary();
			dictionary.DictionaryFolder = Application.StartupPath;
			dictionary.DictionaryFile = d.Dictionary;
			spelling.Dictionary = dictionary;
			
			spellingErrorsFound = 0;

			treeView.ExpandAll();  // Required for NextVisibleNode to work
			// Create string of whole argument
			
			spellTreeNode = this.treeView.SelectedNode;  // get Premise
			// this.spelling.Text = spellTreeNode.Text;
			this.spelling.Text = calcLines();
			this.spelling.AlertComplete = false;  // do not show the Spelling Complete dialog box
			try {
				this.spelling.SpellCheck();
			} catch (Exception ex)
			{
				MessageBox.Show(String.Format("Error during spell check. {0}",ex.Message));
			}
		}
		
		void spelling_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
		{
			spellingErrorsFound++;
		}
		
		private void spelling_EndOfText(object sender, System.EventArgs e)
		{
			doRedraw(true);
			ami.updateEditorText(richTextBox1);
			if(spellingErrorsFound == 0)
				setStatus("Spell check complete. No errors found.");
			else
				setStatus("Spell check complete.");
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

			ami.Modified();
			
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

			ami.Modified();
			
			calcLines();
		}

		private string calcLines()
		{
			int start;
			TreeNode tn;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			flatNode = new ArrayList();
			starts = new ArrayList();

			start=0;
			tn = treeView.Nodes[0];
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
		void FullStopsToolStripMenuItemClick(object sender, EventArgs e)
		{
			FormFullStops ffs = new FormFullStops(ami);

			if( ffs.ShowDialog() == DialogResult.OK)
				this.doRedraw(true);
		}
		
		void OptionsToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Options Dialog
			Options d = new Options(treeView);
			d.ShowDialog();
			doRedraw(true);
		}
		
		void SaveOptionsToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Options.OptionsData.saveOptions(this.menuFileRecentFiles);
			string directory;
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.Filter = "Argumentative prefs XML|*.xml|All Files|*.*";
			saveFileDialog1.Title = "Save Prefs file";
			saveFileDialog1.FilterIndex = 1; // first entry i.e. *.XML
			directory = saveFileDialog1.InitialDirectory;
			saveFileDialog1.FileName="Preferred.xml";
			saveFileDialog1.InitialDirectory = Application.StartupPath;  // where the prefs are stored
			if(saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Options.OptionsData.saveOptions(null,saveFileDialog1.FileName);
			}
			// restore initial directory
			saveFileDialog1.InitialDirectory = directory;
		}
		
		void VScrollBar1Scroll(object sender, ScrollEventArgs e)
		{
			// doRedraw(true);
			this.graphicalViewPanel.Invalidate();
		}
		
		void HScrollBar1Scroll(object sender, ScrollEventArgs e)
		{
			// doRedraw(true);
			this.graphicalViewPanel.Invalidate();
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult d;
			d = ami.askToSave();  // if modified, ask to save file
			if(d == DialogResult.Yes)
				SaveToolStripMenuItemClick(sender,e);
			else if(d == DialogResult.Cancel)
				e.Cancel = true;
			else  // No was clicked or file was not modified
				Options.OptionsData.saveOptions(this.recentFilesToolStripMenuItem,""); // save to the default file
		}
		
		void TransformToolStripMenuItemClick(object sender, EventArgs e)
		{
			Form dlg1 = new TransformDlg(ami);
			dlg1.ShowDialog();
		}
		
		void UndoToolStripMenuItemClick(object sender, EventArgs e)
		{
			cmd.undo();
			doRedraw(true);  // redraw so any change can be seen
		}
		
		void TreeViewKeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;
			int i = (int) c;
			if(i < 32) return; // Some strange control character has been sent to the treeview
			if(c==13)  // if enter is pressed
			{
				ami.addPeerNodeToTree(treeView.SelectedNode);
				richTextBox1.SelectAll();
				e.Handled = true;
				doRedraw(true);
				return;
			}
			// a key is pressed - edit this node and replace existing text
			cmd.executeCommand(new treeKeyPressCommand(e.KeyChar,richTextBox1));

			e.Handled = true;
			doRedraw(true);	// update graphical view
		}
		
		/// <summary>
		/// Translates a mouse click in the graphical view
		/// </summary>
		/// <returns></returns>
		Point getPointInGraphicalView()
		{
			Point p;
			
			p = Control.MousePosition;						// where was clicked
			p = this.graphicalViewPanel.PointToClient(p);	// relative to this panel
			p.X = (int) (p.X / zoom);						// compensate for zoom
			p.Y = (int) (p.Y / zoom);
			p.X = (int) (p.X + hScrollBar1.Value);	// compensate for the scroll position
			p.Y = (int) (p.Y + vScrollBar1.Value);
			
			return p;
		}
		
		void elementNotFoundAt(Point p)
		{
			setStatus(String.Format("No element found at {0},{1}",p.X,p.Y));
		}
		
		void GraphicalViewPanelClick(object sender, EventArgs e)
		{
			// mouse click in the Graphic View
			Point p;
			TreeNode tn;
			
			p = getPointInGraphicalView();
			
			tn = ami.getNodeAt(p);							// search for node
			if(tn != null)
			{
				treeView.SelectedNode = tn;  // select if found
				this.treeView.Focus();			// focus is on the tree view for consistency
				Node n = (Node) tn.Tag;
				string typeText = n.ArgumentNodeTypeString();
				ami.CurrentArg.referenceTree();  // TODO quite inefficient
				setStatus(String.Format("{0} {1}, {2}",n.getRef(),typeText,n.EditorText));
			}
			else
			{
				elementNotFoundAt(p);
			}
		}
		
		void SelectionToolStripMenuItemClick(object sender, EventArgs e)
		{
			// show the current selection in the graphical view
			Node n;
			TreeNode tn;
			tn = this.treeView.SelectedNode;
			if(tn==null) return;  // nothing selected
			n = (Node) tn.Tag;
			hScrollBar1.Value = Math.Max((int) n.x,0);
			vScrollBar1.Value = Math.Max((int)n.y,0);
			doRedraw(false);
		}
		
		// ************************ Help menu *********************
		
		void ContentsToolStripMenuItemClick(object sender, EventArgs e)
		{
			// Open help file contents
			string helpfile = Application.StartupPath + "\\argumentative.chm";
			if(! System.IO.File.Exists(helpfile))
			{
				MessageBox.Show(String.Format("Help file not found: {0}",helpfile));
				return;
			}
			Help.ShowHelp(this,helpfile);
		}
		
		void CheckForUpdatesToolStripMenuItemClick(object sender, EventArgs e)
		{
			// launches a browser and calls a page that uses PHP to check the current number
			string url;
			url="http://argumentative.sourceforge.net/arg_resource/version.php?version=";
			url = url + current_version;
			System.Diagnostics.Process.Start(url);
		}
		
		void HomePageToolStripMenuItemClick(object sender, EventArgs e)
		{
			// launches a browser and opens the home page
			System.Diagnostics.Process.Start("http://argumentative.sourceforge.net/");
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			About about = new About(current_version);
			about.ShowDialog();
		}
		
		void MSWordToolStripMenuItemClick(object sender, EventArgs e)
		{
			ami.exportWord();
		}
		
		void MSPowerpointToolStripMenuItemClick(object sender, EventArgs e)
		{
			ami.exportPowerPoint();
		}
		
		void ToolStripButtonPrintClick(object sender, EventArgs e)
		{
			this.PrintToolStripMenuItemClick(sender,e);
		}
		
		void RichTextBox1KeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;

			if(c==13)
			{
				e.Handled = true;  // drop this char
				// TODO Remove enter from text
				ami.addPeerNodeToTree(treeView.SelectedNode);
				richTextBox1.SelectAll();
			}
			ami.Modified();  // flag the argument as being modified
			textUpdated = true;
		}
		
		void GraphicalViewPanelResize(object sender, EventArgs e)
		{
			this.graphicalViewPanel.Invalidate();
		}
		
		// GraphicalView drag and drop
		
		private Point dragScroll;  // undefined
		private bool isDragScrolling = false;
		
		void GraphicalViewPanelMouseDown(object sender, MouseEventArgs e)
		{
			Point p;
			TreeNode tn;
			
			p = getPointInGraphicalView();
			
			tn = ami.getNodeAt(p);							// search for node
			if(tn != null)
			{
				isDragScrolling = false;
				// Move the dragged node when the left mouse button is used.
				if (e.Button == MouseButtons.Left)
				{
					DoDragDrop(tn, DragDropEffects.Move);
				}

				// Copy the dragged node when the right mouse button is used.
				else if (e.Button == MouseButtons.Right)
				{
					DoDragDrop(tn, DragDropEffects.Copy);
				}
			}
			else if(this.vScrollBar1.Visible || this.hScrollBar1.Visible)// Pan
			{
				dragScroll = Control.MousePosition; // e.Location;
				isDragScrolling = true;
				DoDragDrop(new TreeNode(), DragDropEffects.Move);
			}
			
		}
		
		void GraphicalViewPanelDragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.AllowedEffect;
		}
		
		void GraphicalViewPanelDragDrop(object sender, DragEventArgs e)
		{
			Point p;
			TreeNode targetNode;
			
			if(this.isDragScrolling)
			{
				int dx,dy;
				Point cp;
				
				cp = Control.MousePosition;
				dx = this.dragScroll.X - cp.X;
				dy = cp.Y - this.dragScroll.Y;
				
				// show the current selection in the graphical view
				
				if(hScrollBar1.Visible && hScrollBar1.Value < hScrollBar1.Maximum)
					hScrollBar1.Value = Math.Max(0,Math.Min(hScrollBar1.Value + dx,hScrollBar1.Maximum));
				if(vScrollBar1.Visible)
					vScrollBar1.Value = Math.Max(0,vScrollBar1.Value + dy);
				doRedraw(false);
				this.setStatus("dx = "+dx+" dy = "+dy);
				isDragScrolling = false;
				return;
			}
			
			p = getPointInGraphicalView();
			
			targetNode = ami.getNodeAt(p);							// search for node
			if(targetNode != null)
			{
				
				// Retrieve the node that was dragged.
				TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
				

				if(draggedNode.Equals(targetNode))  // Drag on the same node - show info
				{
					treeView.SelectedNode = targetNode;  // select if found
					this.treeView.Focus();			// focus is on the tree view for consistency
					Node n = (Node) targetNode.Tag;
					string typeText = n.ArgumentNodeTypeString();
					ami.CurrentArg.referenceTree();  // TODO quite inefficient
					setStatus(String.Format("{0} {1}, {2}",n.getRef(),typeText,n.EditorText));
					return;
				}
				
				// Confirm that the node at the drop location is not
				// the dragged node or a descendant of the dragged node.
				if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
				{
					if(((Node) targetNode.Tag).nodeType == Node.ArgumentNodeType.helper)
					{
						// Canot have a helper of a helper
						MessageBox.Show("Sorry, You cannot have a helper for a helper");  // TODO Duplicated
						return;
					}
					// If it is a move operation, remove the node from its current
					// location and add it to the node at the drop location.
					if (e.Effect == DragDropEffects.Move)
					{
						draggedNode.Remove();
						targetNode.Nodes.Add(draggedNode);
					}

					// If it is a copy operation, clone the dragged node
					// and add it to the node at the drop location.
					else if (e.Effect == DragDropEffects.Copy)
					{
						targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
					}

					// Expand the node at the location
					// to show the dropped node.
					targetNode.Expand();
					ami.relinkArgument(this.treeView.Nodes[0]);
					this.doRedraw(true);
					this.ami.Modified();
					setStatus("Dropped");
				}
			}
			else
			{
				// display node properties
			}
		}
		
		void TreeViewAfterCollapse(object sender, TreeViewEventArgs e)
		{
			ami.relinkArgument(this.treeView.Nodes[0]);
			doRedraw(true);
		}
		
		void TreeViewAfterExpand(object sender, TreeViewEventArgs e)
		{
			ami.relinkArgument(this.treeView.Nodes[0]);
			doRedraw(true);
		}
	}
}
