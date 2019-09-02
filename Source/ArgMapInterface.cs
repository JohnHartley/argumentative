using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Diagnostics;


namespace Argumentative
{
	/// <summary>
	/// Interface object to handle interactions between the GUI and the Argument objects.
	/// </summary>
	public class ArgMapInterface
	{

		private Argument currentArg;
		/// <summary>Current file name.</summary>
		private string currentfilename;
		private int modifiedCount=0;  // does the file need saving? TODO Should be in Argument
		private TreeView theTV;
		private RichTextBox editArea;
		private PrintDocument pd = new PrintDocument();

		/// <summary>
		/// Argument Map Interface object with a sample map.
		/// </summary>
		/// <param name="addSample"></param>
		/// <param name="theTV"></param>
		/// <param name="editArea"></param>
		public ArgMapInterface(bool addSample,TreeView theTV,RichTextBox editArea)
		{
			setup(addSample,theTV,editArea);
		}
		private void setup(bool addSample,TreeView theTV,RichTextBox editArea)
		{
			this.currentArg = new Argument();
			if(addSample)
				currentArg.setupSample();
			this.theTV = theTV;
			this.editArea = editArea;
			
		}


		private void loadRE3Map(string fileName)
		{
			Argument a = new Argument();
			currentArg=null;
			if(a.openRE3(fileName))
			{
				currentArg = a;
			}
			else
			{
				a.setupSample();
				currentArg = a;
			}
		}
		

		/// <summary>
		/// Saves the currently selected node as an argument. The selected node is saves as a Main Premise.
		/// </summary>
		public void exportCurrentNode()
		{// Exports the selected tree node and its children as a AXL
			
			DialogResult r;
			string fileName;
			TreeNode tn;

			tn = theTV.SelectedNode;
			if(tn==null) return;

			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.Filter = "Argumentative XML|*.axl";
			saveFileDialog1.Title = "Save an element as an Argument File";
			saveFileDialog1.FileName = Argument.UndefinedFile;
			r=saveFileDialog1.ShowDialog();
			if(saveFileDialog1.FileName == "")
				return;
			if(r == DialogResult.OK)
			{
				fileName = saveFileDialog1.FileName;
				currentArg.saveArg(fileName,(Node) tn.Tag,true);
			}
		}
		
		private void loadArg(string filename)
		{
			// load AXL argument map
			Argument a = new Argument();
			a = a.loadXmlArg(filename);
			if(a != null)
			{
				currentArg = a;
				
				currentfilename = filename;
				a.FileName = filename;
			}
		}
		
		/// <summary>
		/// Loads an argument in any of the known formats.
		/// </summary>
		/// <param name="filename"></param>
		public void loadAXLorRE3(string filename)
		{
			if(filename.ToLower().EndsWith(".axl"))
				loadArg(filename);
			else if(filename.ToLower().EndsWith(".re3"))
				this.loadRE3Map(filename);
			else if(filename.ToLower().EndsWith(".rtnl"))
			{
				Rationale rat = new Rationale();
				Argument arat = rat.importFromRationale(filename);
				currentArg = arat;
			}
			else
				return;
			loadTree();
			verifyTree(false);
		}

		/// <summary>
		/// Ask if an argument needs to be saved, if it has been modified.
		/// </summary>
		/// <returns></returns>
		public DialogResult askToSave()
		{
			if(isModified())
			{
				DialogResult r;
				r = MessageBox.Show(String.Format("Argument map has been modified.  Do you wish to save?"),"Save?",
				                    MessageBoxButtons.YesNoCancel);
				return r;
			}
			return DialogResult.No;  // don't save, file not modified
		}
		/// <summary>
		/// Open an argument file with a standard open file dialog box.
		/// </summary>
		/// <returns></returns>
		public ArgMapInterface openArgument()
		{
			System.Windows.Forms.OpenFileDialog openFileDialog1;
			ArgMapInterface ai;  // if this object needs to be re-created
			DialogResult r;
			string fn;
			
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			openFileDialog1.Filter = "Argumentative files (*.axl)|*.axl|Reasonable files (*.re3)|*.re3"+
				"|Araucaria (*.aml;*.scm)|*.aml;*.scm|All Argument files (.axl;.re3;.rtnl)|*.axl;*.re3;*.rtnl" ;
			openFileDialog1.FilterIndex = 4;

			try
			{
				r = openFileDialog1.ShowDialog();
			}
			catch
			{
				r = DialogResult.Cancel;
			}
			if(r==DialogResult.OK)
			{
				fn = openFileDialog1.FileName;
				
				if(fn.ToLower().EndsWith(".axl") || fn.ToLower().EndsWith(".re3"))
				{
					ai = new ArgMapInterface(false,theTV,editArea);
					ai.loadAXLorRE3(fn);
					currentfilename = fn;
					return ai;
				}
				else if(fn.ToLower().EndsWith(".aml"))  // Araucaria file
				{
					Araucaria ara = new Araucaria();
					ara.load(fn);
					CurrentArg.setArg(ara.A.findHead());
					loadTree();
					currentfilename = fn;
					return this;
				}
				else if(fn.ToLower().EndsWith(".scm"))  // Read scheme set
				{
					Araucaria ara = new Araucaria();
					MessageBox.Show("Araucaria Scheme Sets are read but are not yet supported.");
					ara.load(fn);
					// TODO What to do with the scheme set
				}
				else if(fn.ToLower().EndsWith(".rtnl"))  // Read scheme set
				{
					Rationale rat = new Rationale();
					Argument arat = rat.importFromRationale(fn);
					if(arat == null)
						return null;
					CurrentArg.setArg(arat.findHead());
					loadTree();
					currentfilename = fn;
					return this;
				}
				else
					MessageBox.Show(String.Format("Do not know how to open file type {0}",System.IO.Path.GetExtension(fn)));
			}
			return null;
		}

		/// <summary>
		/// Adds a file name to the recently open files menu item.
		/// </summary>
		/// <param name="fileName">File name to add.</param>
		/// <param name="recentMenuItem">The menu item of the list</param>
		/// <param name="maxItems">Maximum number of items allowed to be in the list.</param>
		/// <returns></returns>
		public ToolStripMenuItem addFileToRecentList(string fileName,ToolStripMenuItem recentMenuItem,int maxItems)
		{
			// Adds the file just open to the recently open file list
			ToolStripMenuItem m=null;
			int c,f,p=0;

			// search list to see if the file name is already there
			c = recentMenuItem.DropDownItems.Count;
			for(f=0;f<c;f++)
			{
				if(recentMenuItem.DropDownItems[f].Text.Equals(fileName))
				{
					m = (ToolStripMenuItem) recentMenuItem.DropDownItems[f];
					p = f;
				}
			}
			if(m == null)  // duplicate not found
			{
				ToolStripMenuItem mi = new ToolStripMenuItem(fileName);
				recentMenuItem.DropDownItems.Insert(0,mi);  // add at the top
				if(c==maxItems)  // delete last item
					recentMenuItem.DropDownItems.RemoveAt(c);
				return mi;  // return to have event added to menu item
			}
			else  // duplicate found
			{
				// move to top of the list
				ToolStripMenuItem mi = new ToolStripMenuItem(m.Text);
				recentMenuItem.DropDownItems.Insert(0,mi);
				recentMenuItem.DropDownItems.RemoveAt(p+1);
				return mi;
			}
			// return null; // unreachable code
		}

		/// <summary>
		/// Export the current argument map to Microsoft Word
		/// </summary>
		public void exportWord()
		{
			string filename,xslfile;
			bool success;
			
			Transform t = new Transform();
			xslfile = Application.StartupPath+"\\RTF with picture.xslt";
			filename = Application.StartupPath+"\\word.rtf";
			try
			{
				success = t.doTransform("",xslfile,filename,CurrentArg);
				if(success)
				{
					System.Diagnostics.Process app;
					app = new System.Diagnostics.Process();
					app.StartInfo.FileName = filename;
					app.Start();
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(String.Format("Transform failed. {0}",e.Message));
			}
		}

		/// <summary>
		/// Export to Microsoft Power Point using COM/Interop
		/// </summary>
		public void exportPowerPoint()
		{
			PowerPointExport p = new PowerPointExport();
			p.outputToPowerPoint(this.currentArg);
		}


		/// <summary>
		/// Used to set the options in Quick Views
		/// </summary>
		/// <param name="options">The options object</param>
		/// <param name="join">Join</param>
		/// <param name="arrow">Arrow</param>
		/// <param name="notation">Box or R notation</param>
		/// <param name="orientation">Orientation</param>
		/// <param name="shading">Shading and drop shadow</param>
		/// <param name="distributed">Distributed line drawing</param>
		/// <param name="blackAndWhite">Set colours to black and white</param>
		public void setStandardOptions(Options.OptionsData options,DrawTree.joinType join,DrawTree.arrowType arrow,
		                               DrawTree.notationType notation,DrawTree.treeOrientationType orientation,
		                               bool shading,bool distributed,
		                               bool blackAndWhite)
		{
			options.Justification	= DrawTree.justificationType.jCentre;
			options.Join			= join;
			options.Arrow			= arrow;
			options.BoxShading		= shading;
			options.DropShadow		= shading;  // Part of shading as standard
			options.ShowLegend		= true;  // Default
			options.Notation		= notation;
			options.TreeOrientation	= orientation;
			options.Distributed		= distributed;
			
			if(! blackAndWhite)
			{
				options.premise		= new Options.ElementOptions("Arial",16,Color.Black,Color.LightGray,3,400,DashStyle.Solid);
				options.reason		= new Options.ElementOptions("Arial",10,Color.Black,Color.FromArgb(142,243,141),2,200,DashStyle.Solid);
				options.objection	= new Options.ElementOptions("Arial",10,Color.Black,Color.FromArgb(238,136,134),2,200,DashStyle.Solid);
				options.helper		= new Options.ElementOptions("Arial",10,Color.Black,Color.FromArgb(133,132,225),1,200,DashStyle.Solid);
			}
			else
			{
				options.premise		= new Options.ElementOptions("Arial",16,Color.Black,Color.Black,3,400,DashStyle.Solid);
				options.reason		= new Options.ElementOptions("Arial",10,Color.Black,Color.Black,2,200,DashStyle.Solid);
				options.objection	= new Options.ElementOptions("Arial",10,Color.Black,Color.Black,2,200,DashStyle.Dash);
				options.helper		= new Options.ElementOptions("Arial",10,Color.Black,Color.Black,1,200,DashStyle.Dot);
			}
		}

		//  ******** editing routines **********

		private Node getSelectedNode(System.Windows.Forms.TreeView tv)
		{
			TreeNode tn = tv.SelectedNode;
			if(tn==null) return null;
			return (Node) tn.Tag;
		}

		/// <summary>
		/// Can the current node be changed to the specified node type?
		/// </summary>
		/// <param name="newNodeType"></param>
		/// <returns></returns>
		private bool canChangeNode(Node.ArgumentNodeType newNodeType)
		{
			bool b;
			b = canChangeNodeReason(newNodeType) == null;
			return b;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="newNodeType"></param>
		/// <returns>null if node can be changed</returns>
		public string canChangeNodeReason(Node.ArgumentNodeType newNodeType)
		{
			int f;
			Node kn;
			string returnReason;
			
			returnReason  = I18N.getString("ChangeNode-Null","Node not changed");
			Node n = getSelectedNode(this.theTV);
			if(n == null)
				return returnReason;
			
			if(n.nodeType==newNodeType)
				return returnReason;	// Not changed
			
			if(n.nodeType==Node.ArgumentNodeType.premise)  // Premise not changed so easily
			{
				returnReason  = I18N.getString("ChangeNode-MainPremise","Cannot change Main Premise");
				return returnReason;
			}
			if(newNodeType!=Node.ArgumentNodeType.helper) // only keep checking if changing to a helper
			{
				returnReason = "";
				return null;
			}
			kn = (Node) this.theTV.SelectedNode.Parent.Tag;  // get parent node
			if(kn.nodeType == Node.ArgumentNodeType.helper)  // is it a helper?
				return I18N.getString("ChangeNode-HelperHelper","Cannot have a Helper for a Helper");
			if(n.countKids()==0)  // only keep checking
				return null;
			// if any kids are helpers
			
			for(f=0;f<n.countKids();f++)
			{
				kn = (Node) n.kids[f];
				if(kn.nodeType == Node.ArgumentNodeType.helper)
					return I18N.getString("ChangeNode-HelperHelper","Cannot have a Helper for a Helper");;
			}
			return null;
		}
		/// <summary>
		/// Change current node type
		/// </summary>
		/// <param name="newNodeType"></param>
		/// <returns></returns>
		public bool changeNodeto(Node.ArgumentNodeType newNodeType)
		{
			if(!this.canChangeNode(newNodeType)) return false;
			Node n = getSelectedNode(this.theTV);
			n.nodeType = newNodeType;
			setImageIndex(theTV.SelectedNode,n);
			modifiedCount++;
			return true;
		}
		/// <summary>
		/// Adds a node at the same level or as a child of the premise
		/// </summary>
		/// <param name="tn"></param>
		public void addPeerNodeToTree(TreeNode tn)
		{
			Node n;
			Node.ArgumentNodeType nodeType;
			string title;
			n = (Node) tn.Tag;
			nodeType = n.nodeType;
			if(tn.Parent != null) // we are not at the top (the main premise)
			{
				tn = tn.Parent;
				title = String.Format("New {0}",n.ArgumentNodeTypeString());
			}
			else
			{
				nodeType = Node.ArgumentNodeType.reason;  // cannot add a premise to an argument
				title = "New Reason";
			}
			this.addNodeToTree(tn,nodeType,title);
		}

		/// <summary>
		/// Add a node to the node selected in the tree
		/// </summary>
		/// <param name="nodeType">Type of node (reason, objection or helper</param>
		/// <param name="text">Text for the node</param>
		public void addNodeToTree(Node.ArgumentNodeType nodeType,string text)
		{
			TreeNode tn;
			tn = theTV.SelectedNode;
			addNodeToTree(tn,nodeType,text);
		}

		private void addNodeToTree(TreeNode where,Node.ArgumentNodeType nt,string text)
		{
			TreeNode newtn;
			Node n,newNode;

			System.Diagnostics.Debug.Assert(nt != Node.ArgumentNodeType.premise,"Cannot add a premise");

			if(where==null) return;
			n = (Node) where.Tag;		// get node
			if(n.nodeType==Node.ArgumentNodeType.helper & nt==Node.ArgumentNodeType.helper)
			{
				MessageBox.Show("Sorry, You cannot have a helper for a helper");  // Duplicated
				return;
			}
			newtn = new TreeNode(text);
			where.Nodes.Add(newtn);
			newNode=new Node();
			newNode.EditorText=text;
			newNode.nodeType = nt;
			n.addKid(newNode,true);  // allow the adding of an empty node
			newtn.Tag = newNode;
			setImageIndex(newtn,newNode);
			// newtn.EnsureVisible();
			theTV.SelectedNode = newtn;
		}
		
		private TreeNode checkTreeNode(bool repair,TreeNode tn)
		{
			// checks the current node and its kids
			// returns the node in error or null if all OK
			TreeNode rtn;
			Node n,kn;
			int i;

			n = (Node) tn.Tag;
			Debug.Assert(n.nodeType != Node.ArgumentNodeType.unknown,"Node not defined");
			if( ! tn.Text.Equals(n.EditorText))  // does the displayed text match the argument node text
				return tn;
			Debug.Assert(tn.Nodes.Count == n.countKids(),"Count mismatch between TreeNode and Argument Node:("+tn.Text+")");
			for(i=0;i<tn.Nodes.Count;i++)
			{
				kn = (Node) n.kids[i];
				Debug.Assert(kn == tn.Nodes[i].Tag,"TreeNode does not match Node.kids. "+tn.Nodes[i].Text);
				rtn = checkTreeNode(repair,tn.Nodes[i]);
				if(rtn != null)
					return rtn;
			}
			if(n.kids==null)
				Debug.Assert(tn.Nodes.Count == 0,"TreeNode has children where the Argument Node does not:("+tn.Text+")");
			else if(tn.Nodes.Count != n.kids.Count)
				return tn;
			return null;
		}

		/// <summary>
		/// Verifies the argument tree against the TreeView using assert().
		/// </summary>
		/// <param name="repair">Attempt to repair the tree</param>
		/// <returns>null</returns>
		public TreeNode verifyTree(bool repair)
		{
			int i;
			TreeNode tn;
			Node n;

			// top level assertions / tests
			System.Diagnostics.Debug.Assert(theTV.Nodes.Count > 0,"There needs to be at least one node");
			n = (Node) theTV.Nodes[0].Tag;  // Get Premise node
			System.Diagnostics.Debug.Assert(n.nodeType == Node.ArgumentNodeType.premise,"First node needs to be a Premise");
			for(i=0;i<theTV.Nodes.Count;i++)
			{
				tn = this.checkTreeNode(repair,theTV.Nodes[i]);
				if(tn != null)
					return tn;
			}
			return null;
		}

		// Editing functionality
		
		/// <summary>
		/// Removes any trailing returns from the RichTextBox
		/// </summary>
		/// <param name="richTextBox1">true if a return found.</param>
		/// <returns></returns>
		public bool editBoxChange(RichTextBox richTextBox1)
		{
			//TODO Trailing returns should be cleaned up one editing is complete
//			if(richTextBox1.Text.EndsWith(t))
//			{
//				richTextBox1.Text = richTextBox1.Text.Substring(0,richTextBox1.Text.Length-1);
//				richTextBox1.SelectionStart = richTextBox1.TextLength;
//				return true;
//			}
			updateEditorText(richTextBox1);
			return false;
		}
		/// <summary>
		/// Delete the currently selected node and its children  in the TreeView.
		/// Deletes the corresponding Nodes from the Argument.
		/// </summary>
		/// <returns></returns>
		public TreeNode deleteNodeFromTree()
		{
			// delete the current node - and all nodes below
			// record information for undo
			
			TreeNode tn,p;
			Node n,pn;

			System.Diagnostics.Debug.Assert(this.verifyTree(false)==null,"Delete verify failed (1)");
			tn = this.theTV.SelectedNode;  // Get selected tree node
			if(tn==null) return null;
			if(tn.Parent==null)  // cannot delete premise
				return null;
			n = (Node) tn.Tag;		// get node
			p = tn.Parent;
			pn = (Node) p.Tag;
			if(p==null)
			{
				MessageBox.Show("Cannot delete Main Premise");
				return null;
			}
			p.Nodes.Remove(tn);
			pn.kids.Remove(n);
			if(pn.kids.Count == 0)
				pn.kids=null;
			System.Diagnostics.Debug.Assert(this.verifyTree(false)==null,"Delete verify failed (2)");
			modifiedCount++;
			return tn;
		}


		/// <summary>
		/// update the text editor box with the node text
		/// </summary>
		/// <param name="tb">RichTextBox to update.</param>
		/// <returns></returns>
		public bool updateEditorText(RichTextBox tb)
		{
			if(theTV==null | tb==null) return false;
			Node n = getSelectedNode(theTV);
			if(n==null) return false;
			if(tb.Modified & ! n.EditorText.Equals(tb.Text))
			{
				n.EditorText = tb.Text;
				theTV.SelectedNode.Text = tb.Text;
				modifiedCount++;  // lots of updates
				return true;
			}
			return false;
		}

		// ******* Move commands **********

		/// <summary>Direction to move node in the tree view</summary>
		public enum direction {
			/// <summary>Move node up in the current list of siblings</summary>
			up,
			/// <summary>Move node down in the current list of siblings</summary>
			down,
			/// <summary>Move node left (promote)</summary>
			left,
			/// <summary>Move node right (demote)</summary>
			right};
		
		/// <summary>
		/// Move the current TreeView node up or down.
		/// </summary>
		/// <param name="d">Direction, up or down</param>
		/// <returns></returns>
		public bool moveNodeUpDown(direction d)
		{
			TreeNode tn,p;
			Node n,move;
			int i;

			System.Diagnostics.Debug.Assert(d==direction.up | d==direction.down,"Up and down only");
			tn = theTV.SelectedNode;  // Get selected tree node
			System.Diagnostics.Debug.Assert(checkTreeNode(false,tn)==null,"Tree verify failed for move "+d);
			if(tn==null) return false;
			if(d==direction.up & tn.PrevNode == null)
				return false;
			if(d==direction.down & tn.NextNode == null)  // cannot move further down
				return false;
			p = tn.Parent;
			if(p == null) // it is a premise
				return false;
			i = p.Nodes.IndexOf(tn);
			try
			{
				p.Nodes.RemoveAt(i);
				if(d==direction.up)
					p.Nodes.Insert(i-1,tn);
				else if(d==direction.down)
					p.Nodes.Insert(i+1,tn);
				else
					return false;
				// move in node structure
				n = (Node) p.Tag;
				move = (Node) n.kids[i];
				n.kids.RemoveAt(i);
				if(d==direction.up)
					n.kids.Insert(i-1,move);
				else if(d==direction.down)
					n.kids.Insert(i+1,move);
				// move selection
				theTV.SelectedNode=tn;
				
				modifiedCount++;  // mark as changed
			}
			catch (Exception e)
			{
				MessageBox.Show(String.Format("Oops error while moving {0} i={1} count = {2}  {3}",
				                              d.ToString(),i,p.Nodes.Count.ToString(),e.Message));
			}
			return true;
		}
		
		/// <summary>
		/// Moves the currently selected node to the left
		/// </summary>
		/// <returns>True if able to move Node to the left</returns>
		public bool moveNodeLeft()
		{
			TreeNode tn,p,pp;
			Node n,pn,ppn;
			int i;

			System.Diagnostics.Debug.Assert(checkTreeNode(false,theTV.Nodes[0])==null,"Move left verify failed (1)");
			tn = theTV.SelectedNode;  // Get selected tree node
			if(tn==null) return false;
			p = tn.Parent;  // get parent tree node
			if(p==null)
				return false;		// Cannot promote a premise.
			pp = p.Parent;
			if(pp==null)			// Cannot promote to premise (use swap)
				return false;
			i=pp.Nodes.IndexOf(p);   // and the location of the parent
			// j=p.Nodes.IndexOf(tn);
			p.Nodes.Remove(tn);
			pp.Nodes.Insert(i+1,tn);  // should land in a sensible spot.
			
			pn = (Node) p.Tag;
			n=(Node) tn.Tag;
			pn.kids.Remove(n);
			ppn= (Node) pp.Tag;
			ppn.kids.Insert(i+1,n);
			
			// move selection
			theTV.SelectedNode=tn;
			
			System.Diagnostics.Debug.Assert(this.checkTreeNode(false,theTV.Nodes[0])==null,"Move left verify failed (2)");
			modifiedCount++;
			return true;
		}
		/// <summary>
		/// Moves currently selected node to be a child of the node above it in the TreeView
		/// </summary>
		/// <returns>True if able to move Node to the right</returns>
		public bool moveNodeRight()
		{
			TreeNode tn,p;
			Node n,pn;
			int i;

			System.Diagnostics.Debug.Assert(this.checkTreeNode(false,theTV.Nodes[0])==null,"Move right verify failed (1)");
			tn = theTV.SelectedNode;  // Get selected tree node
			if(tn==null) return false;
			p = tn.Parent;
			if(p==null)
				return false;  // Swap is used to demote premise.
			i = p.Nodes.IndexOf(tn);
			if(i==0)
			{
				// This is as far as it goes
				return false;
			}
			n = (Node) tn.Tag;
			pn = (Node) p.Tag;
			p.Nodes.Remove(tn);
			p.Nodes[i-1].Nodes.Add(tn);

			pn.kids.Remove(n);
			pn = (Node) pn.kids[i-1];
			pn.addKid(n);

			theTV.SelectedNode=tn;
			System.Diagnostics.Debug.Assert(this.checkTreeNode(false,theTV.Nodes[0])==null,"Move right verify failed (2)");
			modifiedCount++;
			return true;
		}


		/// <summary>
		/// Swaps the text in the currently selected node for its parent's text. Node type is not changed.
		/// </summary>
		/// <param name="tn"></param>
		/// <returns></returns>
		public bool swapText(TreeNode tn)
		{
			Node n1,n2;
			TreeNode parent,child;

			child = this.theTV.SelectedNode;
			parent = child.Parent;
			if(parent==null) return false;  // main premise cannot be swaped
			n1=(Node) child.Tag;
			n2=(Node) parent.Tag;
			Node.swapNodeText(n1,n2);  // swaps the data in the nodes - text & comments
			child.Text = n1.EditorText;
			parent.Text = n1.EditorText;
			this.loadTree(); // rebuild the tree view
			return true;
		}

		// ************ TreeView Display ****************

		/// <summary>
		/// Sets the image index of the
		/// </summary>
		/// <param name="tn"></param>
		/// <param name="n"></param>
		public static void setImageIndex(TreeNode tn,Node n)  // can be called from elsewhere
		{
			int index = 0;

			if(n==null) return;
			if(n.nodeType==Node.ArgumentNodeType.premise)	index = 1;
			if(n.nodeType==Node.ArgumentNodeType.reason)		index = 2;
			if(n.nodeType==Node.ArgumentNodeType.objection)	index = 3;
			if(n.nodeType==Node.ArgumentNodeType.helper)		index = 4;
			
			tn.ImageIndex = index;
			tn.SelectedImageIndex = index;
		}

		private TreeNode addTreeNode(Node n) // add the tree structure of the argument to the tree view
		{
			TreeNode t,tt;
			Node e,nn;
			int i;

			if(n==null)
				return null;
			t = new TreeNode(n.EditorText);  // new node with the node text
			t.Tag = n;		// add argument node
			setImageIndex(t,n);
			if(n.kids == null)
				return t;
			for(i=0;i<n.kids.Count;i++)
			{
				e = (Node) n.kids[i];
				if(e.nodeType != Node.ArgumentNodeType.unknown)
				{
					nn = e;
					if( nn.EditorText!=null)
					{
						tt = addTreeNode(nn);
						t.Nodes.Add(tt);
					}
				}
			}
			return t;
		}

		/// <summary>
		/// Loads an argument into a TreeView
		/// </summary>
		public void loadTree()
		{
			// loads argument into tree view
			TreeNode tn;
			Node n;

			if(this.modifiedCount > 0)
			{
				// Save current map first
			}

			n = currentArg.findHead();
			theTV.Nodes.Clear();
			tn = addTreeNode(n);
			theTV.Nodes.Add(tn);
			theTV.ExpandAll();
			// update edit area
			editArea.Text = n.EditorText;
			// Select top level
			theTV.SelectedNode = theTV.Nodes[0];
		}

		/// <summary>
		/// Imposes the TreeView structure on the Argument structure.
		/// </summary>
		/// <remarks>The TreeNode needs to have the correct Node against it.</remarks>
		/// <param name="tn"></param>
		public void relinkArgument(TreeNode tn)
		{
			int f;
			Node n,nn;
			
			nn = (Node) tn.Tag;
			// nn.IsExpanded = tn.IsExpanded;
			if(tn.Nodes.Count > 0)
				nn.kids = new List<Node>();
			else
			{
				nn.kids = null;
				// nn.IsVisible = true;  // leaf nodes are always expanded
			}
			for(f=0;f<tn.Nodes.Count;f++){
				n = (Node) tn.Nodes[f].Tag;
				if(tn.IsExpanded)  // if the current node is expanded then all its kids are expanded (visable)
					n.IsVisible = true;
				else
					n.IsVisible = false;
				nn.kids.Add(n);
				relinkArgument(tn.Nodes[f]);
			}
		}

		private TreeNode getNodeAt(TreeNode tn,Point p)
		{
			TreeNode rtn;
			Node n;
			n = (Node) tn.Tag;
			if(p.X >= n.x & p.X <= n.x+n.width)
				if(p.Y >= n.y & p.Y <= n.y+n.height)
				return tn;
			int f;
			for(f=0;f<tn.Nodes.Count;f++)
			{
				rtn = getNodeAt(tn.Nodes[f],p);
				if(rtn != null)
					return rtn;
			}
			return null;  // nothing found
		}

		/// <summary>
		/// returns Node at the position p
		/// </summary>
		/// <param name="p">Point on the graphical view to search</param>
		/// <returns>The Node at point p. null if nothing found</returns>
		public TreeNode getNodeAt(Point p)
		{
			TreeNode tn;

			tn = theTV.Nodes[0];  // main premise
			return getNodeAt(tn,p);
		}

		// Getters and setters
		
		/// <summary>Current file name.</summary>
		public string CurrentFilename 
		{
			get {
				if(string.IsNullOrEmpty(currentfilename))
					return Argument.UndefinedFile;
				return currentfilename; }
		}
		/// <summary>Is the current argument modified</summary>
		public bool isModified()
		{
			if(modifiedCount > 0)
				return true;
			if(this.currentArg.ModifiedCount > 0)
				return true;
			return false;
		}
		/// <summary>Call when argument is modified</summary>
		public void Modified() { modifiedCount++; }
		/// <summary>Return the Argument belonging to this interface object</summary>
		public Argument CurrentArg { get { return currentArg; } }
		/// <summary>Returns the TreeView for the argument</summary>
		public TreeView getTreeView(){ return theTV; }
		/// <summary>Gets or sets the Print Document</summary>
		public PrintDocument Pd {
			get { return pd; }
			set { pd = value; }
		}
	}
	
}
