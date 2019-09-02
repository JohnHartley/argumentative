using System;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Drawing;

namespace Argumentative
{
	/// <summary>
	/// Cut and Paste functionality between the Argumentative node structure 
	/// and text and image formats.
	/// </summary>
	public class CutAndPaste
	{
		private TreeView tv;

		/// <summary>
		/// Constructor 
		/// </summary>
		/// <param name="theTree">TreeView object to work on.</param>
		public CutAndPaste(TreeView theTree)
		{
			tv = theTree;
		}

		// ************ Paste from Clipboard ************

		private TreeNode addNode(TreeNode tn,string text)
		{
			Node n,nn;
			TreeNode ntn;

			ntn = new TreeNode(text);
			n = new Node(text);
			ntn.Tag = n;		// add node to payload
			ArgMapInterface.setImageIndex(ntn,n);
			tn.Nodes.Add(ntn);	// add tree node to tree
			nn = (Node) tn.Tag;
			nn.addKid(n);
			return ntn;
		}

		private ArrayList splitString(string s)  // splits on carrage return, line feed
		{
			string delim = "";
			ArrayList a = new ArrayList();
			int x,y;
			x=0;
			if(s.IndexOf("\r\n") >=0 )
				delim = "\r\n";
			else if(s.IndexOf("\n") >=0 )
				delim = "\n";
			else if(s.IndexOf("\r") >=0 )
				delim = "\r";
			else
			{
				if(! s.Equals("")) a.Add(s);
				return a;
			}
			x=0;
			y = s.IndexOf("\r",x);
			while(y>=0)
			{
				string ss = s.Substring(x,y-x);
				if(! ss.Equals("")) a.Add(ss);
				x = y + delim.Length;
				y = s.IndexOf("\r",x);
			}
			if(x<s.Length)
				if(! s.Substring(x).Equals("")) a.Add(s.Substring(x));
			return a;
		}

		private string determineIndent(ArrayList lines)  // are the indents tabs or spaces
		{
			string indent;
			int i, f;
			for(f=0;f<lines.Count;f++)
			{
				string s = (string) lines[f];  // retrieve
				if(s.StartsWith(" "))
				{
					i=0;
					indent="";
					while(s[i++]==' ' & i<s.Length)
						indent = indent + " ";
					return indent;
				}
				else if(s.StartsWith("\t"))
					return "\t";
			}
			return "";
		}

		private int getIndentLevel(string s,string indent) // How many indent levels
		{
			if(indent.Equals(""))
				return 0;
			if(s.IndexOf(indent) == -1)
				return 0;
			// trim any trailing indents
			s=s.TrimEnd(null);
			int x = 0;
			int y, r;
			r = 0;
			y=s.IndexOf(indent,x);
			while( (y != -1) & (x < s.Length) )
			{
				r++;
				x = y+indent.Length;
				y = s.IndexOf(indent,x);
			}
			return r;
		}


		private ArrayList theLines;
		private int currentLine;
		private string theIndent;

		private void addALine(TreeNode tn,int level)
		{
			string text;
			TreeNode ntn;

			if(currentLine == theLines.Count) return;
			text = ((string) theLines[currentLine]).Trim();  // current line to paste without leading indenting
			ntn = addNode(tn,text);
			currentLine++;
			if(currentLine == theLines.Count) return;
			int i;
			i = getIndentLevel((string) theLines[currentLine],theIndent);
			while(i>=level & (currentLine < theLines.Count))
			{
				if(i > level)
				{
					addALine(ntn,level+1);
				}
				else if(i==level) // add node at same level
				{
					ntn=addNode(tn,((string) theLines[currentLine]).Trim());
					currentLine++;
				}
				else return;

				
				if(currentLine < theLines.Count)
					i = getIndentLevel((string) theLines[currentLine],theIndent);
			}
			// connect nodes
		}

		/// <summary>
		/// Each line from a text paste action is added under the selected TreeNode
		/// </summary>
		/// <param name="clipboardtext"></param>
		public void pasteFromText(string clipboardtext)
		{
			// is it indented and what is the indent text
			int i;
			currentLine = 0;
			theLines = splitString(clipboardtext.Trim());
			theIndent = this.determineIndent(theLines);
			if(theLines.Count==1) // one line to be pasted
			{
				string s = (string) theLines[0];
				addNode(this.tv.SelectedNode,s);
			}
			else if(theLines.Count==0) // no lines ?
				return;
			else 
			{
				i = getIndentLevel((string) theLines[0],theIndent);
				if(i != 0)
				{
					MessageBox.Show("Cannot paste this text. The list is indented and the first line is indented and cannot be.");
					return;
				}
				addALine(tv.SelectedNode,0);  // add lines recursively
			}
		}

		private string copyNode(TreeNode tn,int level)
		{
			string s,indent="";
			int f;

			for(f=0;f<level;f++)
				indent = indent + theIndent;
			s = indent + tn.Text + System.Environment.NewLine;
			foreach(TreeNode n in tn.Nodes)
				s = s + copyNode(n,level+1);
			return s;
		}

		/// <summary>Copies the current TreeNode and its children to the clipboard.</summary>
		/// <returns>Indented text</returns>
		public string copySelectedTreeNodeAsText()
		{
			string s;
			theIndent = "\t";
			s = copyNode(this.tv.SelectedNode,0);
			return s;
		}
		
		/// <summary>
		/// Copies the current node and its children to the clipboard in text and graphic formats
		/// </summary>
		/// <param name="arg"></param>
		public void copyToClipboard (Argument arg)
		{
			DataObject myDataObject;
			
			Node n;
			DrawTree d;
			Bitmap b;

			n = (Node) tv.SelectedNode.Tag;

			System.Text.StringBuilder sb;

			// builds a string which is the AXL (XML) for the current node
			sb = arg.writeArgumentXMLstring(n,false);
			// Creates a new data format.
			DataFormats.Format myFormat = DataFormats.GetFormat("AXL");
			myDataObject = new DataObject(myFormat.Name, sb.ToString());
			// Add the text format
			myDataObject.SetData(copySelectedTreeNodeAsText());
			// add graphic
			d = new DrawTree(0,0,1F); // no offset or zoom
			b = d.drawTree(n);			// create image
			myDataObject.SetData(b);

			Clipboard.SetDataObject(myDataObject,true);
		}
		/// <summary>
		/// Paste AXL or text under the current node
		/// </summary>
		/// <returns></returns>
		public bool paste()
		{
			string s;
			TreeNode tn;
			
			tn = tv.SelectedNode;
			// check what is on the clipboard
			IDataObject data = Clipboard.GetDataObject();
			
			if (data.GetDataPresent("AXL"))
			{
				Argument arg = new Argument();
				s = data.GetData("AXL").ToString();
				arg = arg.loadXmlArgString(s);
				Node n,nn;
				n = (Node) tv.SelectedNode.Tag;
				nn = arg.findHead();
				if(nn.nodeType == Node.ArgumentNodeType.premise)  // cannot add a second premise
					nn.nodeType = Node.ArgumentNodeType.reason;
				n.addKid(nn);
				// a.loadTree();  // recreates tree view dependent on the argument 
				return true;  // needs loadTree
			}
			else if (data.GetDataPresent(DataFormats.Text))
			{
				s = data.GetData(DataFormats.Text).ToString();
				pasteFromText(s);
			}
			
			tv.SelectedNode = tn;
			return false;
		}
	}
}
