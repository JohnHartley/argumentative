using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Drawing.Imaging;  // For ImageFormat enumeration

namespace Argumentative
{
	/// <summary>
	/// The main argument object.
	/// </summary>
	public class Argument
	{
		// Use a 'static readonly' field (C# syntax) to make sure a reference to the field itself is kept and avoid recompiling all assemblies.
		/// <summary>The string used for an undefined file</summary>
		public static readonly string UndefinedFile = "untitled"; // TODO Needs to be in resources
		
		private ArrayList elementList = new ArrayList();	// all elements
		
		private int nodesLoaded = 0; // number of noded loaded from a RE3 file
		private int edgeCount=0;
		
		private Node head;  // top of the argument tree
		private string fileName = UndefinedFile;
		private string author = "";
		private string title = "";
		private DateTime creationDate = DateTime.Today;	// not defined - is there an empty date?
		private bool creationDateDefined = false;
		private DateTime modifiedDate = DateTime.Today;
		private int modifiedCount;  // TODO Remove from ArgMapInterface
		
		/// <summary>
		/// Argument constructor
		/// </summary>
		public Argument()
		{
			Options.OptionsData.init(null);
			modifiedCount = 0;
		}
		
		private bool readRE3(string fileName)
		{
			try
			{
				Tokeniser T = new Tokeniser(fileName);
				T.getNextToken();
				while (T.getTokenType() != Tokeniser.TokenType.T_Eof)
				{
					if (T.getTokenType() == Tokeniser.TokenType.T_Node)
					{
						nodesLoaded++;
						Node N = new Node();
						elementList.Add(N);
						N.parseElement(T);
					}
					// if (T.getTokenType() == Tokeniser.TokenType.T_Edges)
					// 	;
					if (T.getTokenType() == Tokeniser.TokenType.T_Edge)
					{
						edgeCount++;
						Edge E = new Edge();
						elementList.Add(E);
						E.parseElement(T);
					}
					if(T.getTokenType() != Tokeniser.TokenType.T_Edge & T.getTokenType() != Tokeniser.TokenType.T_Node)
						T.getNextToken();
				}
				this.fileName = fileName;
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(
					String.Format("Error during re3 file read. {0}",e.Message));
				return false;
			}
			if(nodesLoaded == 0)
			{
				System.Windows.Forms.MessageBox.Show(
					String.Format("No nodes found in {0}. Sure it is a Reason!able file?",fileName));
				return false;
			}
			return true;
		}

		/// <summary>
		/// Reads a Reasonable! file
		/// </summary>
		/// <param name="fileName">File to read. Usually ends in re3</param>
		/// <returns>True if file read successfuly</returns>
		public bool openRE3(string fileName)
		{
			if(readRE3(fileName)==false)  // there was an error in the file
				return false;
			analyseEdges();
			correctNodes();
			string s;
			s = String.Format("Reasonable! file imported from {0} on {1}.",fileName,DateTime.Now.ToString());
			findHead().setComment(s);
			this.fileName = fileName;
			this.modifiedCount++;
			return true;
		}

		// In the format YYYY-MM-DD
		private string DateTimetoString(DateTime dt)
		{
			return dt.Year.ToString() + "-" +
				dt.Month.ToString() + "-" +
				dt.Day.ToString();
		}
		private string correctForXML(string s)
		{
			s=s.Replace("&","&amp;");
			s=s.Replace("<","&lt;");
			s=s.Replace(">","&gt;");
			return s;
		}
		private void writeNode(TextWriter file,Node n,bool addTags)
		{
			int f;
			Node nn;
			string tag,s;

			tag=n.nodeType.ToString();
			s=correctForXML(n.EditorText);
			file.WriteLine("<"+tag+">\r\n<title>"+s+"</title>");
			if(! n.getComment().Equals(""))
				file.WriteLine("<comment>"+correctForXML(n.getComment())+"</comment>");
			if(addTags)
			{
				Options.ElementOptions eo;
				eo = Options.OptionsData.whichOption(n);
				file.WriteLine("<ref>"+n.getRef()+"</ref>");
				file.WriteLine("<x>"+n.x+"</x>");
				file.WriteLine("<y>"+n.y+"</y>");
				file.WriteLine("<width>"+n.width+"</width>");
				file.WriteLine("<height>"+n.height+"</height>");
				file.WriteLine("<fontSize>"+eo.font.Size.ToString()+"</fontSize>");
			}
			if(n.kids!=null)
				for(f=0;f<n.kids.Count;f++)
			{
				nn = (Node) n.kids[f];
				if(nn!=null)
					if(nn.EditorText!=null)
					if(! nn.EditorText.Trim().Equals(""))
					this.writeNode(file,nn,addTags);
			}
			file.WriteLine("</"+tag+">");
		}

		/// <summary>
		/// Writes the XML for an argument to a file.
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="n">Argument Node</param>
		/// <param name="makePremise">True sets the XML representation of the top node to type of Premise.</param>
		public void writeArgumentXML(string fileName,Node n,bool makePremise)
		{
			StreamWriter file;
			try
			{
				if (File.Exists(fileName))
					File.Delete(fileName);
				
				file = new StreamWriter(fileName, true);
				writeArgumentXML(file,n,makePremise,false);
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(
					String.Format("Error while writing file: {0}",e.Message));
			}
			this.fileName = fileName;
		}
		
		/// <summary>
		/// Returns the XML representation of an argument.
		/// </summary>
		/// <param name="n">Argument Node</param>
		/// <param name="addTags">Add tags used in XSL Transforms</param>
		/// <returns>The String XML representation of the Node n and its children.</returns>
		public System.Text.StringBuilder writeArgumentXMLstring(Node n,bool addTags)
		{
			StringWriter file = new StringWriter();
			
			writeArgumentXML(file,n,false,addTags);
			return file.GetStringBuilder();
		}
		
		/// <summary>
		/// Writes the argument to a TextWriter file.
		/// </summary>
		/// <param name="file">TextWriter file</param>
		/// <param name="n">Top Argument Node</param>
		/// <param name="addTags">Add tags used in XSL Transforms</param>
		/// <param name="makePremise"></param>
		private void writeArgumentXML(System.IO.TextWriter file,Node n,bool makePremise,bool addTags)
		{
			bool saveSubArgument = false;
			Node.ArgumentNodeType temp = Node.ArgumentNodeType.unknown;

			Options.OptionsData d = Options.OptionsData.Instance;

			if(n == null)
				n = findHead();
			if(addTags)
				referenceTree();  // Reference tree for <ref> tag

			if(makePremise & (n.nodeType != Node.ArgumentNodeType.premise))
			{
				saveSubArgument = true;
				temp = n.nodeType;
				n.nodeType = Node.ArgumentNodeType.premise;
			}

			file.WriteLine("<argument>");
			if(!(author.Equals("")& d.Author.Equals(""))) // Add author if defined
			{
				file.Write("<author>");
				if(author.Equals(""))  // does the argument not have an author
					file.Write(d.Author);
				else
					file.Write(author);
				file.WriteLine("</author>");
			}
			if(creationDateDefined)
			{
				file.Write("<created>");
				file.Write(DateTimetoString(creationDate));
				file.WriteLine("</created>");
			}
			file.Write("<modified>");
			file.Write(DateTimetoString(this.modifiedDate));
			file.WriteLine("</modified>");
			
			if(addTags)  // add tags used for XSLT
			{
				// Title
				file.Write("<title>");
				file.Write(Title);
				file.WriteLine("</title>");
				// Todays date - difficult to format with XSLT
				file.Write("<day>");
				file.Write(DateTime.Today.DayOfWeek.ToString());
				file.Write("</day>");
				file.Write("<dayNum>");
				file.Write(DateTime.Today.Day.ToString());
				file.Write("</dayNum>");
				file.Write("<month>");
				file.Write(DateTime.Today.Month.ToString());
				file.Write("</month>");
				file.Write("<year>");
				file.Write(DateTime.Today.Year.ToString());
				file.Write("</year>");
				
				file.Write("<shortDate>");
				file.Write(DateTime.Today.ToShortDateString());
				file.Write("</shortDate>");
				file.Write("<shortTime>");
				file.Write(DateTime.Now.ToShortTimeString());
				file.Write("</shortTime>");
				
				file.Write("<longDate>");
				file.Write(DateTime.Today.ToLongDateString());
				file.Write("</longDate>");
				file.Write("<longTime>");
				file.Write(DateTime.Now.ToLongTimeString());
				file.Write("</longTime>");
				
			}
			writeNode(file,n,addTags);
			file.WriteLine("</argument>");
			file.Close();
			if(saveSubArgument)
				n.nodeType = temp;
		}

		private string getTitle(XmlNode xn) { return getAnyTag("title",xn); }
		private string getComment(XmlNode xn)
		{
			string s;
			s = getAnyTag("comment",xn);
			s = s.Replace("\n","\r\n");
			return s;
		}


		/// <summary>
		/// Gets the value of a sub element in the list of DOM nodes
		/// </summary>
		/// <param name="name">The XML element name to search for (any case).</param>
		/// <param name="xn">DOM (XML)node</param>
		/// <returns>Text in the node</returns>
		private string getAnyTag(string name,XmlNode xn)
		{
			int f;
			XmlNode x1;
			string s;

			name=name.ToLower();

			if(xn==null) return "";
			if(xn.Name.ToLower().Equals(name))
				return xn.InnerText;
			for(f=0;f<xn.ChildNodes.Count;f++)
			{
				x1 = (XmlNode) xn.ChildNodes[f];
				s=getAnyTag(name,x1);
				if(! s.Equals("") )
					return s;
			}
			return "";
		}

		private Node getDomNode(XmlNode xn)
		{
			string name;
			int f;
			XmlNode x1;
			Node n,n2;

			if(xn==null)
				return null;
			name = xn.Name.ToLower();
			n = new Node();
			if(name.Equals("premise"))
			{
				n.nodeType = Node.ArgumentNodeType.premise;
			}
			else if(name.Equals("reason"))
			{
				n.nodeType = Node.ArgumentNodeType.reason;
			}
			else if(name.Equals("objection"))
			{
				n.nodeType = Node.ArgumentNodeType.objection;
			}
			else if(name.Equals("helper"))
			{
				n.nodeType = Node.ArgumentNodeType.helper;
			}
			else // title, comment, created or modified node - not handled here
			{
				return null;
			}
			
			n.EditorText = getTitle(xn);
			n.setComment(getComment(xn));
			// load nodes
			for(f=0;f<xn.ChildNodes.Count;f++)
			{
				x1 = (XmlNode) xn.ChildNodes[f];
				n2 = getDomNode(x1);
				if(n2!=null)
					n.addKid(n2);
			}
			return n;
		}


		/// <summary>Load argument from a file</summary>
		public Argument loadXmlArg(string fileName)
		{
			// Load argument from a file
			Argument arg;
			StreamReader sr = new StreamReader(fileName);
			arg = loadXmlArg(sr);
			sr.Close();
			FileInfo finfo = new FileInfo(fileName);
			arg.CreationDate = finfo.CreationTime;
			return arg;
		}


		/// <summary>Load argument from a string</summary>
		public Argument loadXmlArgString(string s)
		{
			Argument arg;
			StringReader sr = new StringReader(s);
			arg = loadXmlArg(sr);
			sr.Close();
			return arg;
		}


		/// <summary>Loads the AXL formatted argument. The source may be a file or StringReader</summary>
		public Argument loadXmlArg(TextReader tr)
		{
			XmlNode xn;
			Node n=null;

			XmlDocument doc = new XmlDocument();
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			settings.IgnoreComments = true;
			XmlReader reader = XmlReader.Create(tr, settings);


			try
			{
				doc.Load(reader);  // Load the AXL into the DOM (Document Object Model)
				
			}
			catch (Exception e)
			{
				// report error
				MessageBox.Show (String.Format("Error in XML format: {0}",e.Message),"Argumentative",
				                 MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
				return null;
			}
			// load document into argument
			Argument a = new Argument();

			xn = (XmlNode) doc.DocumentElement;

			if(xn.Name.ToLower().Equals("argument"))
				xn = xn.ChildNodes[0];
			// extract Argument level information
			do
			{
				if(xn.Name.Equals("author"))
					author = getAnyTag("author",xn);
				if(xn.Name.Equals("created"))
					getDate("created",xn);
				if(xn.Name.Equals("modified"))
					getDate("modified",xn);
				if(xn.Name.Equals("premise"))
					n = getDomNode(xn);
				if(xn.Name.Equals("reason"))
					n = getDomNode(xn);
				if(xn.Name.Equals("objection"))
					n = getDomNode(xn);
				if(xn.Name.Equals("helper"))
					n = getDomNode(xn);
				xn = xn.NextSibling;
			}while(xn != null);
			a.setArg(n);
			a.Author = author;
			a.CreationDate = this.creationDate;
			a.CreationDateDefined = this.creationDateDefined;
			
			return a;
		}

		/// <summary>Filter for saveFileDialog.Filter field with the supported file types.</summary>
		public static readonly string saveFilter = 
			"Argumentative XML|*.axl|Bitmap|*.bmp|PNG|*.png|JPEG|*.jpg|GIF|*.gif|Rationale 1.1|*.rtnl";
		
		/// <summary>Enumeration of file types that an argument may be saved / exported in.
		/// This correlates with the saveFilter string above
		/// </summary>
		public enum fileIndex {
			/// <summary>Native Argumentative XML format</summary>
			axl = 1,
			/// <summary>Windows bitmap</summary>
			bmp = 2,
			/// <summary>Portable Network Graphics</summary>
			png = 3,
			/// <summary>JPEG graphic</summary>
			jpg = 4,
			/// <summary>GIF graphic</summary>
			gif = 5,
			/// <summary>Rationale argument map format</summary>
			rtn = 6
		}
		/// <summary>Save argument in format depanding on file extension or id number</summary>
		/// <param name="fileName">File name with extension</param>
		/// <param name="FilterIndex"></param>
		public void saveAs(string fileName,int FilterIndex)
		{
			Node n;
			string ending;
			
			n = this.findHead();
				ending = System.IO.Path.GetExtension(fileName).ToLower();
				if(ending.Equals(".bmp")|| FilterIndex == (int) fileIndex.bmp)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(System.IO.Path.GetFileNameWithoutExtension(fileName)+".bmp",n,ImageFormat.Bmp);
				}
				else if(ending.ToLower().Equals(".png")|| FilterIndex == (int) fileIndex.png)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(System.IO.Path.GetFileNameWithoutExtension(fileName)+".png",n,ImageFormat.Png);
				}
				else if(ending.ToLower().Equals(".jpg")|| FilterIndex == (int) fileIndex.jpg)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(fileName,n,ImageFormat.Jpeg);
				}
				else if(ending.ToLower().Equals(".gif")|| FilterIndex == (int) fileIndex.gif)
				{
					DrawTree d=new DrawTree(0,0,1);
					d.drawTree(fileName,n,ImageFormat.Gif);
				}
				else if(ending.ToLower().Equals(".rtnl")|| FilterIndex == (int) fileIndex.rtn) // Rationale export
				{
					Rationale rx = new Rationale();
					rx.exportToRationale(fileName,findHead(),false); // expanded = false.  Option should set to true zz
				}
				else if(ending.Equals(".axl")|| FilterIndex == (int) fileIndex.axl)  // already has the correct extension
				{
					saveArg(fileName,null,false);
					// addFileToRecent(fileName);
				}
				else if(ending.ToLower().Equals(""))
				{
					if(fileName != fileName + ".axl")
					{
						if(System.IO.File.Exists(fileName + ".axl"))
						{
							DialogResult result;
							string message = String.Format("{0} exists. Do you wish to overwrite?",fileName);
							
							result = System.Windows.Forms.MessageBox.Show
								(message,"Overwrite?",System.Windows.Forms.MessageBoxButtons.YesNo);
							if(result != DialogResult.Yes)
								return;
						}
					}
					fileName = fileName + ".axl";
					saveArg(fileName,null,false);
				}
				else
				{
					fileName = fileName + ".axl";
					saveArg(fileName,null,false);
					// addFileToRecent(fileName);
				}
		}
		
		/// <summary>
		/// Save argument or argument fragment as a file
		/// </summary>
		/// <param name="fileName">File Name</param>
		/// <param name="n">Argument Node</param>
		/// <param name="makePremise">Save n as a main premise</param>
		public void saveArg(string fileName,Node n,bool makePremise)
		{
			// this.verifyTree(false);  // TODO verifyTree testing
			// check to see if the .axl extension is there
			string ext = Path.GetExtension(fileName).ToLower();
			if( ! ext.Equals(".axl"))
				fileName = fileName+".axl";
			writeArgumentXML(fileName,n,makePremise);  // null means write the current argument, not a sub argument
			this.fileName = fileName;
			
			// modifiedCount = 0;  // current file no longer deemed updated TODO modifiedCount should be in Argument
			// this.verifyTree(false);
		}
		
		private enum elementTimeStampType { created,modified,unknown }  // Just used in getDate
		
		private void getDate(string elementName,XmlNode xn)
		{
			DateTime dateTime;
			string dateTimeString,year,month,day;
			int y,m,d;
			
			elementTimeStampType elementType = elementTimeStampType.unknown;
			
			if(elementName.Equals("created"))
				elementType = elementTimeStampType.created;
			else if(elementName.Equals("modified"))
				elementType = elementTimeStampType.modified;
			System.Diagnostics.Debug.Assert(elementType != elementTimeStampType.unknown,"Unknown element type:"+elementName);
			dateTimeString = getAnyTag(elementName,xn);  // get the date time string
			dateTimeString = dateTimeString.Trim();
			// format is reverse date e.g. 2007-12-5 th the 5th December 2007
			if(dateTimeString.Equals(""))
			{
				MessageBox.Show(String.Format("Empty <{0}> element",elementName));
				return;
			}
			if(dateTimeString.IndexOf("-") < 0)
			{
				MessageBox.Show(String.Format("<{0}> element['{1}'] is in the incorrect format",elementName,dateTimeString));
				return;
			}
			year = dateTimeString.Substring(0,dateTimeString.IndexOf("-"));	// year is before the first dash
			dateTimeString = dateTimeString.Substring(dateTimeString.IndexOf("-")+1);	// trim out the year
			month = dateTimeString.Substring(0,dateTimeString.IndexOf("-"));
			day = dateTimeString.Substring(dateTimeString.IndexOf("-")+1);	// remove month

			Int32.TryParse(year,out y);
			Int32.TryParse(month,out m);
			Int32.TryParse(day,out d);
			dateTime = new DateTime(y,m,d);
			
			if(elementType == elementTimeStampType.created)
			{
				this.creationDate = dateTime;
				this.creationDateDefined = true;
			}
			else
			{
				this.modifiedDate = dateTime;
			}
		}

		private Node findNode(String findName)
		{
			int f;
			Element e;
			
			for(f=0;f<elementList.Count;f++)
			{
				e = (Element) elementList[f];
				if(e.type==Tokeniser.TokenType.T_Node)
					if(e.name.Equals(findName))
					return (Node) e;
			}
			return null;
		}
		
		private void analyseEdges()
		{
			// fix up the RE3 file
			int f;
			Element e;
			Edge edge;
			Node n1,n2;
			
			// Go through all nodes
			for(f=0;f<elementList.Count;f++)
			{
				e = (Element) elementList[f];
				if(e.type==Tokeniser.TokenType.T_Edge)
				{
					edge = (Edge) e;
					n1=findNode(edge.fromNodeName);
					System.Diagnostics.Debug.Assert(n1!=null,"Node "+edge.fromNodeName+" not found.");
					n2=findNode(edge.toNodeName);
					System.Diagnostics.Debug.Assert(n2!=null,"Node "+edge.toNodeName+" not found.");
					// confirm ? System.Diagnostics.Debug.Assert(n2.fromNode==null,"From node should be null.");
					if(n2.fromNode==null)
					{
						n2.fromNode=n1;
						n1.addKid(n2);
					}
				}
				else if(e.type==Tokeniser.TokenType.T_Node)
				{ // Check node
					n1=(Node) e;
					// System.Diagnostics.Debug.Assert(!(n1.EditorText==null & n1.longNode==true),"Node is in the wrong mode");
				}
			}
			// Set the top of the list
			head = findHead();
			
			// correctNodes();  refactored out
		}

		private void mergeKids(Node n1,Node n2)
		{
			if(n1==null & n2==null) return; // error (assert)
			if(n1.kids==null)
			{
				n1.kids = n2.kids;
				return;
			}
			if(n2.kids==null)
			{
				n1.kids.RemoveRange(0,1); // remove first element
				return;
			}
			int f;
			for(f=0;f<n2.kids.Count;f++)
			{
				n1.addKid((Node) n2.kids[f]);
			}
			n1.kids.RemoveRange(0,1); // remove first element
		}

		private void correctNode(Node n)
		{
			int f;
			Node n1;

			if(n==null)
				return;
			if(n.nodeType==Node.ArgumentNodeType.unknown)
				n.nodeType = Node.ArgumentNodeType.helper; // Helper node if not known otherwise
			if(n.EditorText.ToLower().Equals("helper"))
				return;  // error but should never happen
			if ((n.EditorText.ToLower().Equals("reason"))
			    | (n.EditorText.ToLower().Equals("objection")))
			{
				if(n.EditorText.ToLower().Equals("reason"))
					n.nodeType = Node.ArgumentNodeType.reason;
				else
					n.nodeType = Node.ArgumentNodeType.objection;
				// first kid is primary reason/objection
				n1 = (Node) n.kids[0];
				n.EditorText = n1.EditorText;  // overwrite reason/objection with actual text
				mergeKids(n,n1);
			}
			if(n.kids != null)
				for(f=0;f<n.kids.Count;f++)
				correctNode( (Node) n.kids[f] );
		}
		
		private void correctNodes()
		{
			Node n;

			n = findHead();
			n.nodeType = Node.ArgumentNodeType.premise;
			correctNode(n);
		}

		/// <summary>
		/// Returns the top node of the argument
		/// </summary>
		/// <returns>Head Node</returns>
		public Node findHead() // Note: in future versions there may be multiple heads
		{
			int f;
			Node n1;
			Element e;

			if(head!=null) return head;

			for(f=0;f<elementList.Count;f++)
			{
				e = (Element) elementList[f];
				if(e.type==Tokeniser.TokenType.T_Node)
				{
					n1 = (Node) e;
					if(n1.fromNode==null & n1.EditorText != null)
						return n1;
				}
			}
			return null;
		}
		
		private String incrementRef(String r)
		{
			int i;
			char c;
			String s,prefix;
			
			i = r.LastIndexOf(".");
			if (i == -1)
			{
				prefix = "";
				s = r;
			}
			else
			{
				prefix = r.Substring(0, i+1);
				s = r.Substring(i + 1);
			}
			
			if(Char.IsLetter(s[0]))
			{
				// a,b,c
				if(s.Length > 1)
					return "A";  // restart sequence
				c = s[0];
				c++; // Next character
				s = ""+c; // convert to string
			}
			else if(Char.IsDigit(r[0]))
			{
				try
				{
					i = Int32.Parse(s);
				}
				catch
				{i=0;}
				i++;
				s = ""+i;
			}
			r = prefix+s;
			return r;
		}
		
		private void referenceNode(Node n,String r)
		{
			Node k;
			int i;
			if(n.EditorText==null) return;

			n.setRef(r);
			
			if(n.kids==null)
				return;

			r=r+".1";
			for(i=0;i<n.kids.Count;i++)
			{
				k = (Node) n.kids[i];
				referenceNode(k,r);
				r=incrementRef(r);
			}
		}
		
		/// <summary>
		/// references the nodes in the argument 1, 1.1,1.2.1 etc
		/// </summary>
		public void referenceTree()
			// adds numbering
		{
			Node head,k;
			int f;
			String r;
			
			head = findHead();
			head.setRef("");
			r="1";

			for(f=0;f < head.kids.Count;f++)
			{
				k = (Node) head.kids[f];
				if(k.EditorText != null)
				{
					referenceNode(k,r);
					r=incrementRef(r);
				}
			}
		}
		
		/// <summary>
		/// Finds a Node by its reference e.g. 1.2.1
		/// </summary>
		/// <param name="reference">Dotted reference string</param>
		/// <returns></returns>
		public Node findByReference(string reference)
		{
			return findByReference(findHead(),reference);
		}
		/// <summary>
		/// Finds a Node by its reference e.g. 1.2.1
		/// </summary>
		/// <param name="startNode">Node to start search</param>
		/// <param name="reference">Dotted reference string</param>
		/// <returns>null if not found</returns>
		public Node findByReference(Node startNode,string reference)
		{
			if(startNode.getRef().Equals(reference))
				return startNode;
			int f;
			Node nn;
			for(f=0;f<startNode.countKids();f++)
			{
				nn = (Node) startNode.kids[f];
				nn = findByReference(nn,reference);
				if(nn != null)
					return nn;
			}
			return null;
		}
		

		// manipulation routines

		/// <summary>
		/// Adds a Node (element) to the argument
		/// </summary>
		/// <param name="argType">Node type</param>
		/// <param name="text">Element text</param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public Node addNode(Node.ArgumentNodeType argType,string text,Node parent)
		{
			Node n = new Node();
			n.EditorText=text;
			n.nodeType=argType;
			n.fromNode = parent;
			if(parent==null)
			{
				System.Diagnostics.Debug.Assert(argType==Node.ArgumentNodeType.premise |
				                                argType==Node.ArgumentNodeType.unknown,
				                                "Incorrect node type given as "+argType.ToString());
				head = n;
				return n;
			}
			parent.addKid(n);
			return n;
		}

		/// <summary>
		/// Creates a sample argument map with a premise, a reason with helper and a objection with helper.
		/// </summary>
		/// <returns>Head Node of new argument</returns>
		public Node setupSample()
		{
			Node p,nr,no,nh;
			Options.OptionsData d = Options.OptionsData.Instance;

			// TODO Add to resources for I18N
			// addPremise
			p=addNode(Node.ArgumentNodeType.premise,"The Main Premise to the argument",null);
			// addReadon
			nr=addNode(Node.ArgumentNodeType.reason,"A reason",p);
			nh=addNode(Node.ArgumentNodeType.helper,"A helper for the reason",nr);
			// add Objection
			no=addNode(Node.ArgumentNodeType.objection,"An objection",p);
			nh=addNode(Node.ArgumentNodeType.helper,"A helper for the objection",no);

			head = p;
			author = d.Author;  // load author from options

			this.fileName = UndefinedFile;
			modifiedCount=0;
			return p;
		}

		/// <summary>
		/// Get the depth of the argument from the root node
		/// </summary>
		/// <returns></returns>
		public int getDepth() { return getDepth(null); }
		
		/// <summary>
		/// Get the depth of the argument from the specified node.
		/// </summary>
		/// <param name="n">null retrieves the depth of the entire argument</param>
		/// <returns>Number of levels including the main premise</returns>
		public int getDepth(Node n)
		{
			if(n == null)
				n = findHead();
			if(n.countKids() == 0)
				return 1;
			int f,maxCount,count;
			maxCount=0;
			for(f=0;f<n.countKids();f++)
			{
				count = getDepth((Node) n.kids[f]);
				if(count > maxCount) maxCount = count;
			}
			return maxCount + 1;
		}
		
		
		private int wordCount(string s)
		{
			string delimStr = " ,.:";
			char [] delimiter = delimStr.ToCharArray();
			string [] split = null;
			split = s.Split(delimiter);
			return split.Length;
		}

		/// <summary>
		/// Counts the number of words in a node and its kids.
		/// </summary>
		/// <param name="n">Starting Node</param>
		/// <returns>Number of words</returns>
		public int wordCount(Node n)
		{
			int count,f;
			
			if(n == null)
				n = findHead();
			
			count = wordCount(n.EditorText);
			if(n.kids==null) return count;
			if(n.kids.Count==0) return count;
			for(f=0;f<n.kids.Count;f++)
				count += wordCount((Node) n.kids[f]);
			return count;
		}
		
		/// <summary>
		/// Counts the number of nodes of all types.
		/// </summary>
		/// <param name="n">Starting node</param>
		/// <returns>Number of children + 1</returns>
		public int nodeCount(Node n)
		{
			int count,f;
			
			if(n == null)
				n = findHead();
			
			count = 1;
			if(n.kids==null) return count;
			if(n.kids.Count==0) return count;
			for(f=0;f<n.kids.Count;f++)
				count += nodeCount((Node) n.kids[f]);
			return count;
		}
		
		/// <summary>
		/// Is the given argument the same as this argument?
		/// </summary>
		/// <remarks>Used for testing</remarks>
		/// <param name="arg">Argument to compare</param>
		/// <returns>True if all nodes are equal.</returns>
		public bool Equals(Argument arg)
		{
			Node n,nn;
			n = arg.findHead();
			nn = this.findHead();
			return n.Equals(nn);
		}
		// Getters and setters

		/// <summary>Replace primary argument</summary>
		/// <param name="n">New argument premise node</param>
		public void setArg(Node n) { head = n; }
		/// <summary>Sets the current file name</summary>
		public string FileName { set { this.fileName = value;} }
		/// <summary>Get or set the author name.</summary>
		public string Author { get { return this.author;} set {author=value;}}
		/// <summary>Get the creation date</summary>
		public DateTime CreationDate { get {return this.creationDate;} set {creationDate=value;}}
		/// <summary>Is the creation date defined</summary>
		public bool CreationDateDefined { get {return this.creationDateDefined;} set { creationDateDefined=value;}}
		/// <summary></summary>
		public string Title {
			get {
				if(String.IsNullOrEmpty(title))
					return Path.GetFileNameWithoutExtension(fileName);
				else
					return title; }
			set { title = value; }
		}
				
		/// <summary>
		/// Return the number of times the argument has been modified
		/// </summary>
		public int ModifiedCount {
			get { return modifiedCount; }
		}
	}
}
