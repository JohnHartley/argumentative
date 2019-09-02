using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>
	/// Reads in Araucaria files and schemas
	/// </summary>
	public class Araucaria
	{
		/// <summary>
		/// New Araucaria object
		/// </summary>
		public Araucaria()
		{
			a = new Argument();  // empty argument
			comment = "";
		}
		
		private Argument a;
		
		string fileName;
		string comment;
		
		/// <summary>
		/// Loads the Araucaria XML format file
		/// </summary>
		/// <param name="fileName">File name to Araucaria file to load</param>
		public void load(string fileName)
		{
			// Load argument from a file
			
			StreamReader sr = new StreamReader(fileName);
			StringBuilder sb;
			this.fileName = fileName;
			sb = preload(fileName);
			StringReader str = new StringReader(sb.ToString());
			loadXML(str);
			sr.Close();
		}
		
		private StringBuilder preload(string fileName)
		{
			int c;
			StringBuilder sb;
			StreamReader sr = new StreamReader(fileName);
			string lineToKill = "<!DOCTYPE ARG SYSTEM \"argument.dtd\">";
			sb = new StringBuilder();
			c = sr.Read();
			while(c >= 0 | c==65535)
			{
				sb.Append((char)c);
				c =  sr.Read();
			}
			sb.Replace(lineToKill,"");
			return sb;
		}
		private void loadXML(TextReader tr)
		{
			XmlNode xn;

			XmlDocument doc = new XmlDocument();
			XmlTextReader xtr = new XmlTextReader(tr);
			xtr.WhitespaceHandling = WhitespaceHandling.None;
			// No longer using XmlValidatingReader
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			XmlReader reader = XmlReader.Create(xtr,settings);
			
			try 
			{
				doc.Load(reader);  // Load the AXL into the DOM (Document Object Model)
	
			} 
			catch (Exception e)
			{
				// report error
				MessageBox.Show (String.Format("Error in AXL format: {0}",e.Message),String.Format("Argumentative"),
					MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
				return;
			}
			
			xn = (XmlNode) doc.DocumentElement;
			Node n = readArg(xn.ChildNodes);
			n.setComment("Araucaria argument read from "+fileName+System.Environment.NewLine+
				comment);
			a.setArg(n);
		}
		private Node readArg(XmlNodeList xnl)
		{
			Node n = null;
			XmlNode xn;
			xn = (XmlNode) xnl[0];
			while(xn != null)
			{
				if(xn.Name.Equals("SCHEMESET"))
				{
					readSchemeSet(xn.ChildNodes);
				}
				else if(xn.Name.Equals("TEXT"))
				{
					comment = comment + "Background:" + xn.InnerText + System.Environment.NewLine;
				}
				else if(xn.Name.Equals("AU"))
				{
					n = readArgumentUnit(xn.ChildNodes,Node.ArgumentNodeType.premise);
				}
				xn = xn.NextSibling;
			}
			return n;
		}
		
		private Node readArgumentUnit(XmlNodeList xnl,Node.ArgumentNodeType nt)
		{
			//	An argument unit, composed of a proposition (a conclusion)
			//	followed, optionally, by a single refutation, followed,
			//	optionally, by premises arranged in either convergent or
			//	linked structures.
			//	<!ELEMENT AU (PROP, REFUTATION?, (CA | LA)*)>

			XmlNode xn;
			Node n = new Node();
			n.nodeType = nt;
			xn = (XmlNode) xnl[0];
			while(xn != null)
			{
				if(xn.Name.Equals("PROP"))
				{
					n.EditorText = this.getAnyTag("PROPTEXT",xn.ChildNodes[0]);
				}
				else if(xn.Name.Equals("REFUTATION"))
				{
					readRefutation(xn.ChildNodes,n);
				}
				else if(xn.Name.Equals("CA"))
				{
					readConvergentArgument(xn.ChildNodes,n);
				}
				else if(xn.Name.Equals("LA"))
				{
					readLinkedArgument(xn.ChildNodes,n);
				}
				xn = xn.NextSibling;
			}
			return n;
		}
		
		private void readRefutation(XmlNodeList xnl,Node n)
		{
			System.Diagnostics.Debug.Assert(xnl.Count == 1,"REFUTATION has one AU only");
			n.addKid(this.readArgumentUnit(xnl[0].ChildNodes,Node.ArgumentNodeType.objection));
		}
		
		private void readConvergentArgument(XmlNodeList xnl,Node n)
		{
			Node newNode;
			int f;
			XmlNode cn;  // current node
			for(f=0;f < xnl.Count;f++)
			{
				cn = xnl[f];
				newNode = new Node();
				newNode = this.readArgumentUnit(cn.ChildNodes,Node.ArgumentNodeType.reason);
				n.addKid(newNode);
			}
		}
		
		private void readLinkedArgument(XmlNodeList xnl,Node n)
		{
			Node newNode;
			int f;
			XmlNode cn;  // current node
			for(f=0;f < xnl.Count;f++)
			{
				cn = xnl[f];
				newNode = new Node();
				newNode = this.readArgumentUnit(cn.ChildNodes,Node.ArgumentNodeType.reason);
				n.addKid(newNode);
			}
		}
		
		private SchemeSet readSchemeSet(XmlNodeList xnl)
		{
			
			XmlNode xn;
			SchemeSet schemeset;
			schemeset = new SchemeSet();
			xn = (XmlNode) xnl[0];
			while(xn != null)
			{
				if(xn.Name.Equals("SCHEME"))
				{
					schemeset.Schemes.Add(readScheme(xn.ChildNodes));
				}
				
				xn = xn.NextSibling;
			}
			
			return schemeset;
		}

		private Scheme readScheme(XmlNodeList xnl)
		{
			// <!ELEMENT SCHEME (NAME, FORM, CQ*)>
			XmlNode xn;
			Scheme scm = new Scheme();
			xn = (XmlNode) xnl[0];
			while(xn != null)
			{
				if(xn.Name.Equals("NAME"))
				{
					scm.Name = xn.InnerText;
				}
				else if(xn.Name.Equals("FORM"))
				{
					scm.Form = xn.InnerText;
				}
				else if(xn.Name.Equals("CQ"))
				{
					string s;
					s = xn.InnerText;
					scm.addCriticalQuestion(s);
				}
				xn = xn.NextSibling;
			}
			
			return scm;
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
	
		/// <summary>Sets the Arg map</summary>
		public Argument A {
			get { return a; }
			set { a = value; }
		}
	}
	

	/// <summary>
	/// Represents a Araucaria schemeset.  This is a series of standard arguments
	/// </summary>
	public class SchemeSet
	{
		private ArrayList schemes;
		/// <summary>Get list of schemes</summary>
		public ArrayList Schemes {
			get { return schemes; }
		}

		/// <summary>
		/// New Scheme set
		/// </summary>
		public SchemeSet()
		{
			schemes = new ArrayList();
		}
	}

	/// <summary>
	/// Represents an individual scheme in an Araucaria Scheme set.
	/// </summary>
	public class Scheme
	{
		// <!ELEMENT SCHEME (NAME, FORM, CQ*)>
		private string name;
		private string form;
		private ArrayList criticalQuestions;  // list of strings
		
		/// <summary>
		/// Creates a new scheme including critical question list.
		/// </summary>
		public Scheme()
		{
			this.criticalQuestions = new ArrayList();
		}
		
		/// <summary>Gets or sets a scheme name</summary>
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		/// <summary>Gets or sets a scheme form</summary>
		public string Form {
			get { return form; }
			set { form = value; }
		}
		
		/// <summary>
		/// Adds a critical question to the list.
		/// </summary>
		/// <param name="question">Question</param>
		public void addCriticalQuestion(string question)
		{
			this.criticalQuestions.Add(question);
		}
	}
}
