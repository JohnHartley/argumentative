using System;
using System.Collections;
using System.Collections.Generic;


namespace Argumentative
{
	
	/// <summary>
	/// The basic argument element
	/// </summary>
	public class Node : Element 
	{
		/// <summary>Type of element for a node.</summary>
		public enum ArgumentNodeType 
		{
			/// <summary>Unknown argument type</summary>
			unknown,
			/// <summary></summary>
			premise,
			/// <summary>Main Premise. One per argument</summary>
			reason,
			/// <summary>Objection</summary>
			objection,
			/// <summary>Helper</summary>
			helper,
			/// <summary>Co-premise</summary>
			copremise
		}

		/// <summary>The x axis graphical location of the upper left of the node</summary>
		public float	x;
		/// <summary>The y axis graphical location of the upper left of the node</summary>
		public float	y;
		/// <summary>Width</summary>
		public float	width=0F;
		/// <summary>height</summary>
		public float	height=0F;	// width and hieght
		/// <summary>Width of this node and its children</summary>
		public float	fullWidth = 0F;			// Width of this node and all kids
		/// <summary>Height of this node and its children</summary>
		public float    fullHeight = 0F;		// height of this node and all kids
		
		private string	editorText = "";		// the text for each node (name comes from re3 file format)
		private string	CommentText = "";		// comment text if any
		private string	reference;				// node reference e.g 1.2.1
		private string	Rnotation;				// R1,O1,H1 etc
		private DateTime created;
		private DateTime modified;
		private bool isVisible = true;			// 
		/// <summary>Parent node</summary>
		public Node fromNode=null;
		/// <summary>Argument element type. Reason, objection, helper</summary>
		public ArgumentNodeType nodeType=ArgumentNodeType.unknown;
		/// <summary>Child nodes</summary>
		public List<Node> kids = null;
		/// <summary>
		/// An argument node
		/// </summary>
		public Node() : base ()
		{
			// base() is the same as Super() in java
			this.type  = Tokeniser.TokenType.T_Node;
			this.created = DateTime.Now;
		}
		/// <summary> Creates a new reason node with specified text</summary>
		/// <param name="nodeText">node text</param>
		public Node(string nodeText) : base ()
		{
			// base() is the same as Super() in java
			this.type  = Tokeniser.TokenType.T_Node;
			this.editorText = nodeText;
			this.nodeType = ArgumentNodeType.reason;
			this.created = DateTime.Now;
		}
		/// <summary>Node with the basics</summary>
		/// <param name="nodeText">Text</param>
		/// <param name="arg">Type of element</param>
		/// <param name="reference">Reference</param>
		public Node(string nodeText,ArgumentNodeType arg,string reference) : base ()
		{
			// base() is the same as Super() in java
			this.type  = Tokeniser.TokenType.T_Node;
			this.editorText = nodeText;
			this.nodeType = arg;
			this.reference = reference;
			this.created = DateTime.Now;
		}

		/// <summary>
		/// Swaps the text of two nodes
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static void swapNodeText(Node a,Node b)
		{
			Node temp;
			temp				= new Node();
			temp.editorText		= a.editorText;
			temp.CommentText	= a.CommentText;

			a.editorText		= b.editorText;
			a.CommentText		= b.CommentText;

			b.editorText		= temp.editorText;
			b.CommentText		= temp.CommentText;
			//TODO Check time stamps
		}

		/// <summary>
		/// Is the specified node equal to the this node?
		/// </summary>
		/// <remarks>Used for testing.</remarks>
		/// <param name="n"></param>
		/// <returns></returns>
		public bool Equals(Node n)
		{
			if( ! n.EditorText.Equals(this.editorText))
				return false;
			if(n.countKids() != this.countKids())
				return false;
			int f;
			for(f=0; f<n.countKids();f++)
			{
				Node a,b;
				a = (Node) this.kids[f];
				b = (Node) n.kids[f];
				if(!a.Equals(b))
					return false;
			}
			return true;
		}
		
		/// <summary>
		/// Add a child node to the current node. Empty nodes (no editor text) are not added.
		/// </summary>
		/// <param name="n">Node to add</param>
		public void addKid(Node n) { addKid(n,false); }

		/// <summary>
		/// Add a child node to the current node.
		/// </summary>
		/// <param name="n">Node to add</param>
		/// <param name="allowEmpty">Empty nodes (no editor text) may be added if true</param>
		public void addKid(Node n,bool allowEmpty) 
		{
			if(n==null)
				return;
			if(kids==null)
				kids = new List<Node>();
			if(kids.Contains(n))
				return;  // element already in the list
			if(! allowEmpty & String.IsNullOrEmpty(n.editorText))  // do not add empty nodes
				return;
			n.fromNode = this;  // add parent node
			kids.Add(n);
		}

		/// <summary>
		/// Returns node type as a string
		/// </summary>
		/// <returns></returns>
		public string ArgumentNodeTypeString()
		{
			if(nodeType==ArgumentNodeType.helper)
				return "Helper";
			if(nodeType==ArgumentNodeType.objection)
				return "Objection";
			if(nodeType==ArgumentNodeType.reason)
				return "Reason";
			if(nodeType==ArgumentNodeType.premise)
				return "Premise";
			if(nodeType==ArgumentNodeType.unknown)
				return "unknown";
			return "dunno";  // undefined
		}
		
		/// <summary>How many child elements in the Node</summary>
		/// <returns>Number of child elements</returns>
		public int countKids()
		{
			if(kids==null)
				return 0;
			return kids.Count;
		}
		// ***************** Static Node Functions *******************
		private static int R,O,H;
		private static string calcRnotationForNode(Node n)
		{
			switch (n.nodeType)
			{
				case ArgumentNodeType.premise:	return "P1";
				case ArgumentNodeType.reason:	R++; return "R"+R;
				case ArgumentNodeType.objection:	O++; return "O"+O;
				case ArgumentNodeType.helper:	H++; return "H"+H;
			}
			return "unknown";
		}
		/// <summary>
		/// Calculates the R notation referencing
		/// </summary>
		/// <param name="n">Starting Node</param>
		public static void calcRnotation(Node n)
		{
			int k;
			Node kn;
			if(n.nodeType == ArgumentNodeType.premise)
			{ R=0;O=0;H=0;}

			if(n.nodeType == ArgumentNodeType.premise)
				n.Rnotation = calcRnotationForNode(n);
			
			// calculate the labeling for the kids before transversing deeper
			for(k=0;k < n.countKids();k++)
			{
				kn = (Node)n.kids[k];
				kn.Rnotation = calcRnotationForNode(kn);
			}
			for(k=0;k < n.countKids();k++)
				calcRnotation((Node)n.kids[k]);
		}
		/// <summary>
		/// Finds a Node by its reference string
		/// </summary>
		/// <param name="reference">Reference to search for</param>
		/// <param name="start">Node to start from</param>
		/// <returns>Node found or null if not found</returns>
		public static Node findNodeByReference(string reference,Node start)
		{
			Node n;
			if(start == null) return null;
			System.Diagnostics.Debug.Assert(start.reference != null,"No reference for "+start.editorText);
			if(start.reference.Equals(reference))
				return start;
			// check the children
			int f;
			for(f=0;f<start.countKids();f++)
			{
				n = findNodeByReference(reference,(Node) start.kids[f]);
				if(n != null)
					return n;
			}
			return null;  // nothing found
		}
		
		
		/// <summary>
		/// Is the Node a Co-premise
		/// </summary>
		/// <returns></returns>
		/// <remarks>All Helpers are Co-premises for the moment</remarks>
		public bool isCoPremise()
		{
			if(nodeType == Node.ArgumentNodeType.helper)
				return true;
			if(nodeType == Node.ArgumentNodeType.copremise)  // Copremise Not used yet
				return true;
			return false;
		}
		
		/// <summary>
		/// Determines if a node has co-premises
		/// </summary>
		/// <param name="n"></param>
		/// <returns>True if node has co-premises</returns>
		public bool hasCoPremises(Node n)
		{
			int k;
			
			if(countKids()==0) return false;
			for(k=0;k<n.countKids();k++)
			{
				Node kn;
				kn = (Node) n.kids[k];
				if(kn.isCoPremise())
					return true;
			}
			return false;
		}
		
		internal bool parseElement(Tokeniser T)
		{
			assertToken(T,Tokeniser.TokenType.T_Node,"Node expected");
			labelExpected(T,"name");
			T.getNextToken();
			name = T.getToken_string();
			labelExpected(T,"x");
			T.getNextToken();
			assertToken(T,Tokeniser.TokenType.T_Number,"Number expected");
			x = T.getLongValue();
			labelExpected(T,"y");
			T.getNextToken();
			assertToken(T,Tokeniser.TokenType.T_Number,"Number expected");
			y = T.getLongValue();
			labelExpected(T,"width");
			T.getNextToken();
			assertToken(T,Tokeniser.TokenType.T_Number,"Number expected");
			width = T.getLongValue();
			labelExpected(T,"height");
			T.getNextToken();
			assertToken(T,Tokeniser.TokenType.T_Number,"Number expected");
			height = T.getLongValue();
			// optional fields
			T.getNextToken();
			if(T.getToken_string().Equals("numberOfInPorts")) 
			{
				T.getNextToken();
				assertToken(T,Tokeniser.TokenType.T_Number,"Number expected");
				T.getLongValue();  // read outPorts
				T.getNextToken();
			}
		
			if(T.getToken_string().Equals("numberOfOutPorts")) 
			{
				T.getNextToken();
				assertToken(T,Tokeniser.TokenType.T_Number,"Number expected");
				T.getLongValue();  // read outPorts
				T.getNextToken();
			}

			if(T.getToken_string().Equals("Editor Node")) 
			{
				// longNode=true;
				T.getNextToken();  // Ignore version
				T.getNextToken();  // Get node text
				editorText = T.getToken_string();
				T.getNextToken();  // Some object data e.g Reason.MapperViewFactory3#CNodeViewReason2 9
				T.getNextToken();  // some hex number e.g. 0x0
				T.getNextToken();  // some hex number e.g. 0xcaffca
				T.getNextToken();  // some integer number e.g. 1
				T.getNextToken();  // some hex number and space delimited information
				T.getNextToken();  // Version 1.1 Info
				if(T.getToken_string().Equals("Version 1.1 Info")) 
				{
					T.getNextToken();  // Get comment if there
					CommentText = T.getToken_string();
					T.getNextToken();
				}
			}
			return true;
		}
		// setters & getters
		/// <summary>Get Reference</summary>
		/// <returns>Reference</returns>
		public string getRef(){ if(reference==null) return ""; return reference; }
		/// <summary>
		/// Get reference and add string
		/// </summary>
		/// <param name="add">String to append</param>
		/// <returns></returns>
		public string getRef(string add){ if(reference==null) return ""; return reference + add; }
		/// <summary>
		/// Sets reference
		/// </summary>
		/// <param name="newRef"></param>
		public void	  setRef(string newRef) { reference = newRef; }
		/// <summary>
		/// Get R notation.
		/// </summary>
		/// <returns>Notation string for Node</returns>
		public string getRnotation() { return Rnotation; }
		/// <summary>Sets comment text</summary>
		/// <param name="newText">comment text</param>
		public void   setComment(string newText) 
		{
			if(!CommentText.Equals(newText))
			   this.modified = DateTime.Now;
			CommentText = newText;
		}
		/// <summary>Retrieves comment text</summary>
		/// <returns>Comment text</returns>
		public string getComment() { return CommentText; }
		/// <summary>Gets and sets the Node's text content.</summary>
		public string EditorText {
			get { return editorText; }
			set 
			{
				if(!editorText.Equals(value))
					this.modified = DateTime.Now;
				editorText = value; 
			}
		}
		/// <summary>Gets the Date and time the node was created.</summary>
		public DateTime Created { get { return created; } }
		/// <summary>Gets the Date and time the node was modified.</summary>
		public DateTime Modified { get { return modified; } }
		/// <summary>Is this is an expanded node?</summary>
		public bool IsVisible { 
			get { return isVisible; } 
			set { this.isVisible = value; }
		}
	}
}
