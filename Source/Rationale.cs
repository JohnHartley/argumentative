using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>An object for exporting to and from Rationale</summary>
	
	public class Rationale
	{
		StreamWriter sr;
		private const string qm = "\"";  // quote mark
		private bool exp;  // expanded format
		private string reasonNode,objectionNode;
		// For reading
		private StreamReader infile;	// file being read
		private ArrayList lines;		// lines loaded for processing
		private int currentLine;		// which line are we up to
		private enum modeType
		{
			reasonMode,
			analysisMode
		}
		
		private modeType mode;  // Future use

		/*
		public Rationale()
		{
		}
		*/
		
		/// <summary>Exports the current map to Rationale 1.09 file format</summary>
		/// <param name="fileName">File name to write. Ends with .rtnl</param>
		/// <param name="mainPremise">Main Premise Node</param>
		/// <param name="expanded"></param>
		public void exportToRationale(string fileName,Node mainPremise,bool expanded)
		{
			Node n,kn;
			int k;
			n = mainPremise;  // get the premise
			exp = expanded;
			if(exp)
			{
				reasonNode = "\"CompoundReason\"";
				objectionNode = "\"CompoundObjection\"";
			}
			else
			{
				reasonNode = "\"Reason\"";
				objectionNode = "\"Objection\"";
			}
			sr = File.CreateText(fileName);
			// write header
			sr.WriteLine("#Rationale Version 1.09");
			sr.WriteLine("node0 = app.Create(\"Claim\")");
			// Write premise
			sr.WriteLine("app.SetText(node0, "+ qm + n.EditorText + qm +")");
			for(k=0;k<n.countKids();k++)
			{
				kn = (Node) n.kids[k];
				if(kn.nodeType == Node.ArgumentNodeType.reason)
					sr.WriteLine("app.CreateChild(node0, "+reasonNode+")");
				else if(kn.nodeType == Node.ArgumentNodeType.objection)
					sr.WriteLine("app.CreateChild(node0, "+objectionNode+")");
				else if(kn.nodeType == Node.ArgumentNodeType.helper)  // treat helper as a reason - for the moment
					sr.WriteLine("app.CreateChild(node0, "+reasonNode+")");
			}
			// write out nodes
			for(k=0;k<n.countKids();k++)
			{
				kn = (Node) n.kids[k];
				writeNode(kn,"node0["+k+"]");
			}
			// write view declairations
			sr.WriteLine("view0 = app.Gui.CreateView(\"FernView\")");
			sr.WriteLine("if view0 != None:");
			sr.WriteLine("\tview0.SetLocation(node0, -111, -29)");  // sets the relative location of the premise
			// view0.SetSize(node0, 186, 76)
			if (n.countKids()>0)
				sr.WriteLine("\tview0.SetSize(node0, 249, 72)"); // premise size
			for(k=0;k<n.countKids();k++)
			{
				kn = (Node) n.kids[k];
				//sr.WriteLine("\tview0.SetSize(node0["+k+"], 249, 72)");
				// write kids
				writeViewNode(kn,"node0["+k+"]",false);
				//sr.WriteLine("\tview0.SetSize(node0["+k+"][1], 48, 18)");
			}
			sr.WriteLine("");
			sr.WriteLine("\tview0.ShowPrintPagePreview("+exp.ToString()+")");  // True adds "Supports" & "Opposes" to each node
			sr.WriteLine("");
			sr.Close();
		}

		private void writeNode(Node n,string reference)
		{
			Node kn;
			int k;

			if(exp) sr.WriteLine("app.CreateChild("+reference+", \"Claim\")");  // note - multiple claims required for co-premises
			if(exp) sr.WriteLine("app.CreateChild("+reference+", \"Inference\")");
			if(exp) sr.WriteLine("app.SetText("+reference+"[0], \""+ n.EditorText +"\")");
			else  sr.WriteLine("app.SetText("+reference+", \""+ n.EditorText +"\")");
			// co-premises are written here
			// declare kids
			for(k=0;k<n.countKids();k++)
			{
				kn = (Node) n.kids[k];
				if(kn.nodeType == Node.ArgumentNodeType.reason)
					sr.WriteLine("app.CreateChild("+reference+", "+reasonNode+")");
				else if(kn.nodeType == Node.ArgumentNodeType.objection)
					sr.WriteLine("app.CreateChild("+reference+", "+objectionNode+")");
				else if(kn.nodeType == Node.ArgumentNodeType.helper)  // treat helper as a reason - for the moment
					sr.WriteLine("app.CreateChild("+reference+", "+reasonNode+")");
			}
			for(k=0;k<n.countKids();k++)
			{
				kn = (Node) n.kids[k];
				writeNode(kn,reference+"["+k+"]"); // write kids
			}
			if(exp) // extra text for expanded mode
			{
				if(n.nodeType == Node.ArgumentNodeType.reason)
					sr.WriteLine("app.SetText("+reference+"[1], \"supports\")");// supports
				else if(n.nodeType == Node.ArgumentNodeType.objection)
					sr.WriteLine("app.SetText("+reference+"[1], \"opposes\")");// opposes
			}
		}

		private void writeViewNode(Node n,string reference,bool subnode)
		{
			Node kn;
			int k;

			// view0.SetSize(node0[0], 249, 72)
			//if(subnode)
			sr.WriteLine("\tview0.SetSize("+reference+", 249, 72)");
			
			for(k=0;k<n.countKids();k++)
			{
				kn = (Node) n.kids[k];
				writeViewNode(kn,reference+"["+k+"]",true); // write kids
			}
			if(exp & subnode)
				sr.WriteLine("\tview0.SetSize("+reference+"[0][1], 48, 18)");  // supports or opposes node size
		}

		// **************************  Read Rationale version 1.09 file format *************************
		
		Node premise = null;

		/// <summary>
		/// Import a Rationale file
		/// </summary>
		/// <param name="fileName">File name including path to import.</param>
		/// <returns></returns>
		public Argument importFromRationale(string fileName)
		{
			Argument arg;
			string line,version,s;
			int i;

			premise = null;
			try
			{
				infile = new StreamReader(fileName);
				
				// expecting #Rationale Version 1.09
				line = infile.ReadLine();
				if(line == null) return null;
				String [] split;
				split = line.Split();  // splits the text using the default delimiters
				// if(split.Length != 3) { infile.Close(); return null; }  // not what was expected - run away
				if(split[2].Equals("1.09"))
					version = split[2];
				else if(split[2].Equals("1.08"))
				{
					MessageBox.Show("Sorry, version 1.08 of Rationale cannot be read");
					return null;
				}
				else if(split[2].Equals("130.54") || split[2].Equals("120.53")
				        || split[2].Equals("140.56")|| split[2].Equals("140.58")
				       )
				{
					version = split[2];
					return importFromRationale130(fileName,version);
				}
				else
				{
					DialogResult d;
					d = MessageBox.Show(String.Format("I do not know version {0}. Try to load anyway?",split[2]),"Unknown version", MessageBoxButtons.YesNo);  // TODO Make question
					if(d == DialogResult.Yes)
						return importFromRationale130(fileName,split[2]);
					else
						return null;
				}

				line = infile.ReadLine();
				if(line == null) return null;
				// node0 = app.Create("Claim") expected
				if(line.ToLower().IndexOf("claim") < 0)
					return null;
				line = infile.ReadLine();
				if(line == null) return null;
				// app.SetText(node0, "I should go to the beach today.")
				i = line.IndexOf("app.SetText(node0,");		// should return 0
				if(i != 0)
				{
					MessageBox.Show(String.Format("unknown entry, app.SetText expected.  Found {0}",line));
					return null;
				}
				s = line.Substring(line.IndexOf("\"")+1);	// split string at the quote mark
				s = s.Substring(0,s.IndexOf("\""));			// remove the last quote and bracket
				premise = new Node(s);						// add main premise
				// the next lines declare the top level reasons or objections
				// eg
				// app.CreateChild(node0, "CompoundReason") or app.CreateChild(node0, "CompoundObjection")
				// or app.CreateChild(node0, "Reason") or app.CreateChild(node0, "Objection")

				//  read remaining lines
				lines = new ArrayList();
				line = infile.ReadLine();
				while(line != null)
				{
					lines.Add(line);
					line = infile.ReadLine();
				}
				currentLine = 0;
				// unless the argument has a main premise only, the next lines will define top level
				line = (string) lines[currentLine];
				if(line.IndexOf("CompoundReason")>0 ||line.IndexOf("CompoundObjection")>0 )
				{
					mode = modeType.analysisMode;
				}
				else if(line.IndexOf("Reason")>0 ||line.IndexOf("Objection")>0 )
					mode = modeType.reasonMode;
				else
				{
					MessageBox.Show(String.Format("Expecting to find reasons and or objections.  Found: {0}",line));
					return null;
				}
				readTopLevelElement(premise);
			}
			catch (Exception e) { MessageBox.Show(String.Format("Error reading Rationale file. {0}\n{1}",e.Message,e.StackTrace)); }

			infile.Close();
			if(premise == null)
				return null;
			arg = new Argument();
			arg.setArg(premise);
			return arg;
		}

		// start of command lines
		private string createElement = "app.CreateChild(node0";
		private string setText = "app.SetText(node0";

		/// <summary>
		/// Extracts the array style reference from a line
		/// </summary>
		/// <param name="line">string read from file</param>
		/// <returns></returns>
		private string getRef(string line)
		{
			int i,j;
			string reference;
			
			if(line.IndexOf(createElement) >= 0)
				i = createElement.Length;
			else if(line.IndexOf(setText) >= 0)
				i = setText.Length;
			else return "";

			j = line.IndexOf(",");
			reference = line.Substring(i,j-i);
			return reference;
		}

		private Node findNode(string reference)
		{
			Node n;
			int k;
			for(k=0;k<nodes.Count;k++)  // go through each node in the list
			{
				n = (Node) nodes[k];  // next node in the list
				if(n.nodeType == Node.ArgumentNodeType.premise && n.EditorText.Equals(""))
					return n;  // zz should be an error
				if(n.getRef().Equals(reference) && n.EditorText.Equals(""))
					return n;
			}
			MessageBox.Show(String.Format("Could not find node with reference '{0}'",reference)); // should have found it by now
			return null;
		}

		private ArrayList nodes;
		private bool readTopLevelElement(Node n)
		{
			string line,reference,subref;
			Node newNode,rn;

			nodes = new ArrayList();
			line = (string) lines[currentLine++];
			if(line == null)
				return false;

			while(line.IndexOf(createElement) >= 0 || line.IndexOf(setText) >= 0 ) // 
			{
				reference = getRef(line);
				if(line.IndexOf(createElement) >= 0)
				{
					newNode = new Node();
					newNode.setRef(reference);
					// get node type
					if(line.IndexOf("Reason")>= 0)
						newNode.nodeType = Node.ArgumentNodeType.reason;
					else if(line.IndexOf("Reason")>= 0)
						newNode.nodeType = Node.ArgumentNodeType.objection;
					else
						newNode.nodeType = Node.ArgumentNodeType.unknown;
					if(newNode.nodeType != Node.ArgumentNodeType.unknown)
					{
						nodes.Add(newNode);
						n.addKid(newNode);  // zz not correct - reconnect through reference - but at least they will show up...
					}
				}
				else if(line.IndexOf(setText) >= 0 && line.IndexOf("supports")< 0 && line.IndexOf("opposes") < 0)
				{
					// get text and find associated node
					// reference will depend on mode.  (e.g modeType.analysisMode)
					// e.g. "app.SetText(node0[0][0], \"I would enjoy myself at the beach.\")"
					subref = reference.Substring(0,reference.Length-"[0][0]".Length); // chop off last two elements
					rn = findNode(subref);
					if(rn != null)
					{
						string s= line.Substring(line.IndexOf("\""));
						rn.EditorText = s.Replace("\\n",System.Environment.NewLine);
					}
				}
				line = (string) lines[currentLine++];
				if(line == null)
					return false;
			}
			return true;
		}

		// ******** v130 import - should be its own object
		

		
		Node head=null;  // head of the new argument tree
		
		/// <summary>Add a node to the tree</summary>
		/// <param name="newRef">reference of the new node</param>
		/// <param name="toRef">the reference of the nofe to add the new node to</param>
		/// <param name="n">the node to add</param>
		private void addNode(string newRef, string toRef, Node n)
		{
			Node existingNode;
			
			System.Diagnostics.Debug.Assert(head != null);
			System.Diagnostics.Debug.Assert(newRef != null,"newRef cannot be null");
			System.Diagnostics.Debug.Assert(toRef != null,"toRef cannot be null");
			existingNode = Node.findNodeByReference(toRef,head);
			System.Diagnostics.Debug.Assert(existingNode != null,"Cannot find existing node "+toRef);
			existingNode.addKid(n,true); // There may be empty nodes
			
			n.setRef(newRef);
		}
		/// <summary>Removes duplicates created by the import of compound elements</summary>
		/// <param name="n"></param>
		private void removeDuplicates(Node n)
		{
			int k,r;
			Node kn;
			
			if(n.countKids() == 0)
			{
				if(n.kids != null)
					n.kids = null;
				return;  // no need to check
			}
			r = -1; // which kid to remove
			for(k=0;k<n.countKids();k++)
			{
				kn = (Node) n.kids[k];
				if(n.EditorText.Equals(kn.EditorText))
					r=k;
				else
					removeDuplicates(kn);
			}
			if(r != -1)
				n.kids.RemoveAt(r);
		}
		
		private void updateCompoundElements(Node n)
		{
			// TODO Remove compound elements
			
			int k,j;
			Node kn,tempNode=null;
			
			if(n.countKids() == 0) return;  // no need to check
			for(k=n.countKids()-1;k>=0;k--)
			{
				kn = (Node) n.kids[k];
				if(kn.EditorText.Equals("CompoundReason"))
				{
					tempNode = kn;
					n.kids.Remove(kn);
					n.kids.InsertRange(k,tempNode.kids);
					for(j=0;j<tempNode.countKids();j++)
						updateCompoundElements((Node) tempNode.kids[j]);
				}
				else if(kn.EditorText.Equals("CompoundObjection") | kn.EditorText.Equals("Inference"))
				{
					Node kkn;
					for(j=0;j<kn.countKids();j++){  // change all the kids to objections
						kkn = (Node) kn.kids[j];
						System.Diagnostics.Debug.Assert(kkn.nodeType == Node.ArgumentNodeType.reason,"Reason Node expected."+kkn.nodeType.ToString());
						kkn.nodeType = Node.ArgumentNodeType.objection;
						updateCompoundElements(kkn);  // and check them as well
					}
					tempNode = kn;
					n.kids.Remove(kn);
					n.kids.InsertRange(k,tempNode.kids);
				}
				else
					updateCompoundElements(kn);
			}
		}
		
		private Argument importFromRationale130(string fileName,string version)
		{
			string line;
			string mapref;  // currently created element reference
			float versionNumber = 0F;
			
			line = infile.ReadLine();
			if(line == null) return null;
			
			float.TryParse(version,out versionNumber);
			if(versionNumber >= 130.54F)  // skip prelude on versions higher than 130.54
			{
				while ((line != null) && (!line.Equals("#-- End of Prelude --")))
					line = infile.ReadLine();
				if(line == null) return null;
			}
			
			int claimsRead=0,s,e;
			Argument arg = new Argument();
			string variable,argument,note;
			bool isNote=false;
			Node n=null;
			
			// line = infile.ReadLine(); // read next line
			while (line != null)
			{
				if(line.StartsWith("map"))  // standard variable
				{
					int x = line.IndexOf(" = ");
					variable = line.Substring(0,x).Trim();
					if(line.Contains("Create(\"Claim\")"))  // top level argument
					{
						if(claimsRead == 0) // first tree
						{
							head = new Node();
							n = head;
							n.nodeType = Node.ArgumentNodeType.premise;
							n.setComment("Import of a Rationale(TM) file (version "+version+") from "+fileName);
						}
						else  // subsequent trees added as reasons
						{
							n = new Node();
							n.nodeType = Node.ArgumentNodeType.reason;
							n.setComment("Additional tree #"+claimsRead);
							n.setRef(variable);
							head.addKid(n,true);
						}
						n.setRef(variable);
						claimsRead++;
						
					}
					else if(line.Contains("Create(\"Note\")"))
					{
						isNote = true;  // next text node is a note
					}
					else if(line.Contains("CreateChild"))
					{
						string toRef,what;
						
						// eg
						// map0_20 = CreateChild(map0_10, "CompoundReason")
						
						// get map node that is being created
						s = line.IndexOf("CreateChild(");
						e = line.IndexOf(",",s);  // locate comma
						e = e-s-12;  // length of map reference
						toRef = line.Substring(s+12,e);
						
						// get mapref
						e = line.IndexOf("=");  // locate =
						mapref = line.Substring(0,e).Trim();
						
						// find what to add
						s = line.IndexOf("\"");  // find first quote
						e = line.IndexOf("\"",s+1);  // locate last quote
						what = line.Substring(s+1,e-s-1);  // do not include quote marks
						
						if(what.Equals("CompoundReason")){
							// A compound reason translates to a reason plus helpers
							n = new Node("CompoundReason",Node.ArgumentNodeType.reason,variable);
							// TODO Add compound reason node
							addNode(mapref,toRef,n);
						}
						else if(what.Equals("CompoundObjection")){
							// A compound objection translates to a objection plus helpers
							n = new Node("CompoundObjection",Node.ArgumentNodeType.objection,variable);
							addNode(mapref,toRef,n);
						}
						else if(what.Equals("Reason")){
							n = new Node();
							n.nodeType = Node.ArgumentNodeType.reason;
							addNode(mapref,toRef,n);
						}
						else if(what.Equals("Objection")){
							n = new Node();
							n.nodeType = Node.ArgumentNodeType.objection;
							addNode(mapref,toRef,n);
						}
						else if(what.Equals("Claim")){
							n = new Node("Claim: "+what);
							n.nodeType = Node.ArgumentNodeType.reason;
							addNode(mapref,toRef,n);
						}
						else if(what.Equals("Inference")){
							// do not add
							n = new Node(what);
							n.nodeType = Node.ArgumentNodeType.reason;
							addNode(mapref,toRef,n);
						}
						else  // some other element - add as reason
						{
							n = new Node(what);
							n.nodeType = Node.ArgumentNodeType.reason;
							addNode(mapref,toRef,n);
							// TODO Add as a undefined node - if there was one
						}
					}
				}
				else if(line.Contains("SetText(")){
					s = line.IndexOf("SetText(");
					e = line.LastIndexOf("\"");  // last quote mark
					argument = line.Substring(s+9,e-s-9);
					string w = "\\n";
					int i = argument.IndexOf(w);
					if(i >= 0){
						// argument.Replace(w,r); // does not replace a \n
						string s1 = argument.Substring(0,i);
						string s2 = argument.Substring(i+w.Length);
						argument = s1 + System.Environment.NewLine + s2;
					}
					
					while(argument.IndexOf("\\\"") != -1)		// replace \" with "
						argument = argument.Replace("\\\"","\"");
					
					if(isNote)			// 
					{ note = argument; isNote=false;}
					
					else if(n.EditorText.Equals("CompoundReason") || n.EditorText.Equals("CompoundObjection"))
						n.EditorText = argument;
					else if(string.IsNullOrEmpty(n.EditorText))
						n.EditorText = argument;
					else {
						string ss;
						Node nn;
						ss = n.getRef();
						nn = new Node(argument);
						n.EditorText = argument;
						n.addKid(nn);  // more than one text line for
						nn.setRef(ss);
					}
				}
				line = infile.ReadLine(); // read next line
			}
			
			infile.Close();
			removeDuplicates(head);
			updateCompoundElements(head);
			Argument newArg2 = new Argument();
			newArg2.setArg(head);
			return newArg2;
		}
	}
}
