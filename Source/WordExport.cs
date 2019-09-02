using System;
using System.Drawing;
using Word;
using Microsoft.Office.Core;

// This code is based on the example on the site:
//    http://www.c-sharpcorner.com/Code/2002/Mar/WordFromDotNet.asp

namespace Argumentative
{
	/// <summary>
	/// Summary description for WordExport.
	/// </summary>
	public class WordExport
	{

		Argument arg;
		Word.ApplicationClass WordApp;

		/// <summary>
		/// Exports the argument to Microsoft Word using COM
		/// </summary>
		/// <param name="a"></param>
		public void outputToWord(Argument a)
		{
			Node n;
			DrawTree d;
			Bitmap b;

			arg = a;
			Document aDoc = null;

			WordApp = new  Word.ApplicationClass();
			
			object missing = System.Reflection.Missing.Value;
			object fileName = "normal.dot";   // template file name
			object newTemplate = false;

			object docType = 0;
			object isVisible = true;

			//  Create a new Document, by calling the Add function in the Documents collection
			try
			{
				aDoc = WordApp.Documents.Add(ref fileName, ref newTemplate, ref docType,
				                                           ref isVisible);
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show("Cannot create new document in MS Word.\n"
				                                     +e.Message);
			}
			
			
			WordApp.Visible = true;
			aDoc.Activate();

			WordApp.Selection.Font.Bold = (int)Word.WdConstants.wdToggle;
			WordApp.Selection.TypeText("Argument");
			WordApp.Selection.Font.Bold = (int)Word.WdConstants.wdToggle;
			WordApp.Selection.TypeParagraph();

			n = arg.findHead();
			d = new DrawTree(0,0,1F); // no offset or zoom
			b = d.drawTree(n);
			System.Windows.Forms.Clipboard.SetDataObject(b);
			WordApp.Selection.Paste();
			WordApp.Selection.TypeParagraph();

			writeWordDoc();

			// release the COM object. GC.Collect(); would be overkill
			System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp);
		}
		private void writeNode(Node n,int depth)
		{
			int f;
			Node nn;
			string tag,s,tabs;

			// crude indenting with tabs
			tabs="";
			for(f=0;f<depth;f++)
				tabs=tabs+"  ";  // was \t for tabs

			s=n.EditorText;
			tag=n.ArgumentNodeTypeString();
			WordApp.Selection.TypeText(tabs + tag+ ": "+s);
			WordApp.Selection.TypeParagraph();  // newline

			if(n.kids!=null)
				for(f=0;f<n.kids.Count;f++)
			{
				nn = (Node) n.kids[f];
				if(nn!=null)
					if(nn.EditorText!=null)
					if(! nn.EditorText.Trim().Equals(""))
					this.writeNode(nn,depth+1);
			}
			// file.WriteLine("</"+tag+">");
		}
		public void writeWordDoc()
		{
			Node n;
			// file.WriteLine("<argument>");
			n = arg.findHead();
			this.writeNode(n,0);
			// file.WriteLine("</argument>");
		}
	}
}
