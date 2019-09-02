using System;
// using PowerPoint;  // TODO: Powerpoint reference
using Microsoft.Office.Interop.PowerPoint;
using System.Drawing;
using Microsoft.Office.Core;

namespace Argumentative
{
	/// <summary>
	/// Summary description for PowerPointExport.
	/// </summary>
	public class PowerPointExport
	{
        private Microsoft.Office.Interop.PowerPoint.ApplicationClass PPapp;
		private int index;

		private void doNode(Node n)
		{
            Microsoft.Office.Interop.PowerPoint.Slide ss;
			string nt;
            Microsoft.Office.Interop.PowerPoint.PpSlideLayout ppl;

			if(n==null) return;


			nt = n.ArgumentNodeTypeString(); // get not type text - Reason, objection etc
			if(index == 1)  // first slide
			{
				DrawTree d;
				Bitmap b;

				// Title page slide
                ppl = Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutTitle;

				PPapp.ActivePresentation.Slides.Add(index,ppl);
				ss = PPapp.ActiveWindow.Presentation.Slides[index];
				ss.Shapes[1].TextFrame.TextRange.Text = n.getRef(": ") + nt;
				ss.Shapes[2].TextFrame.TextRange.Text = n.EditorText;

				// Copy map to clipboard
				d = new DrawTree(0,0,1F); // no offset or zoom
				b = d.drawTree(n);
				System.Windows.Forms.Clipboard.SetDataObject(b);

				// Map page slide
				index++;
                ppl = Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank;
                PPapp.ActivePresentation.Slides.Add(index,ppl);
				ss = PPapp.ActiveWindow.Presentation.Slides[index];
				// ss.Shapes.SelectAll();
				ss.Shapes.Paste();
			}
			else
			{
                ppl = Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutText;

				PPapp.ActivePresentation.Slides.Add(index,ppl);
				ss = PPapp.ActiveWindow.Presentation.Slides[index];
				ss.Shapes[1].TextFrame.TextRange.Text = n.getRef(": ") + nt;
				ss.Shapes[2].TextFrame.TextRange.Text = n.EditorText;
			}
			index++;
			if(n.kids==null) return;
			int k;
			for(k=0;k<n.kids.Count;k++)
				doNode((Node) n.kids[k]);
		}

		/// <summary>
		/// Create the current argument in MS PowerPoint using COM
		/// </summary>
		/// <param name="a"></param>
		public void outputToPowerPoint(Argument a)
		{
			Node n;

			n = a.findHead();	// find main premise node
			a.referenceTree();  // reference tree
			index=1;
            PPapp = new Microsoft.Office.Interop.PowerPoint.ApplicationClass();
			object missing = System.Reflection.Missing.Value;

			Microsoft.Office.Core.MsoTriState mts = MsoTriState.msoTrue;
			
			PPapp.Presentations.Add(mts);
			PPapp.Visible = mts;
			
			doNode(n);
			
			// release the COM object. GC.Collect(); would be overkill
			System.Runtime.InteropServices.Marshal.ReleaseComObject(PPapp);
		}
	}
}
