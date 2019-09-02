using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Windows.Forms;
// using System.Windows.Forms;

namespace Argumentative
{
	// Use the PrinterSettings.PrinterName property to specify which printer should print the document.

	/// <summary>
	/// Printing of graphical and textual arguments
	/// </summary>
	public class PrintTree
	{
		
		private PrintPageEventHandler printevent = null;
		private Font printFont;
		private ArrayList flatTree;
		private int index;
		private PrintDocument pd;
		private ArgMapInterface a;
		private int pagesHigh,pagesWide;
		private int cpx;	// Current page in the x direction starting from 0
		private int cpy;
		private int pageNumber;
		private int rangeStart;
		private int rangeFinish;
		private bool firstPage = true;
		private bool showPageNumber;
		private bool scaleToPage=false;
		
		/// <summary>
		/// Prints a text view of the current map.
		/// </summary>
		/// <param name="dummy"></param>
		/// <param name="a"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeFinish"></param>
		/// <param name="showPageNumber"></param>
		public PrintTree(bool dummy,ArgMapInterface a,int rangeStart,int rangeFinish,bool showPageNumber)
		{
			index = 0;
			a.CurrentArg.referenceTree();
			pd = a.Pd;   // set print document/setup
			pageNumber = 1;
			flatTree = new ArrayList();
			flattenTree(a.getTreeView().Nodes[0]);
			printFont = new Font("Arial", 10);
			this.printevent = new PrintPageEventHandler(pd_PrintPage);
			pd.PrintPage += printevent;
			this.rangeStart = rangeStart;
			this.rangeFinish = rangeFinish;
			this.firstPage = true;
			this.showPageNumber = showPageNumber;
		}
		
		/// <summary>
		/// Prints the graphical view of the current map
		/// </summary>
		/// <param name="a"></param>
		/// <param name="rangeStart">First page</param>
		/// <param name="rangeFinish">Last page</param>
		/// <param name="showPageNumber">Displays the word Page and the page number.</param>
		/// <param name="scaleToPage">Scale the map to the current page size.</param>
		public PrintTree(ArgMapInterface a,int rangeStart,int rangeFinish,bool showPageNumber,bool scaleToPage)
		{
			this.a = a;
			pd = a.Pd;
			pagesHigh=0;
			pagesWide=0;
			cpx=cpy=0;
			pageNumber=1;
			printevent = new PrintPageEventHandler(pd_PrintPageGraphic);
			pd.PrintPage += printevent;
			
			this.rangeStart = rangeStart;
			this.rangeFinish = rangeFinish;
			this.firstPage = true;
			this.showPageNumber = showPageNumber;
			this.scaleToPage = scaleToPage;
		}
		
		/// <summary>
		/// Release the print event. ~PrintTree executes only when the application is exiting
		/// </summary>
		public void release()
		{
			pd.PrintPage -= printevent;
		}
		

		// The PrintPage event is raised for each page to be printed.
		private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
		{
			Node n;
			TreeNode ctn;   // current tree node
			int linesPerPage = 0;
			float yPos =  0;
			int count = 0;
			float leftMargin = ev.MarginBounds.Left;
			float topMargin = ev.MarginBounds.Top;
			String line=null;
			
			// Calculate the number of lines per page.
			linesPerPage = (int) (ev.MarginBounds.Height  /
			                      printFont.GetHeight(ev.Graphics)) ;
			
			if(this.pageNumber < this.rangeStart)
				index = linesPerPage * (this.rangeStart-this.pageNumber);

			// Iterate over the file, printing each line.
			while (count < linesPerPage && (index < flatTree.Count))
			{
				ctn = (TreeNode) flatTree[index];
				n = (Node) ctn.Tag;
				line = n.getRef(": ")+ctn.Text;
				yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
				ev.Graphics.DrawString (line, printFont, Brushes.Black,
				                        leftMargin, yPos, new StringFormat());
				count++;
				index++;
			}

			// If more lines exist, print another page.
			if(pageNumber > this.rangeFinish)	// this was the last page to be printed
				ev.HasMorePages = false;
			else if (index < flatTree.Count)
				ev.HasMorePages = true;
			else
				ev.HasMorePages = false;
		}

		/// <summary>
		/// Start the printing process
		/// </summary>
		public void print()
		{
			pd.Print();
		}
		
		private void flattenTree(TreeNode tn)
		{
			while(tn != null)
			{
				flatTree.Add(tn);
				tn = tn.NextVisibleNode;
			}
		}
		
		private SizeF calcFooter(string s,float maxWidth,Graphics g,Font f)
		{
			SizeF layoutSize,stringSize;
			layoutSize = new SizeF(maxWidth, 500.0F);  // footer must fit in this size
			
			// Measure string.
			stringSize = new SizeF();
			stringSize = g.MeasureString(s, f, layoutSize);
			return stringSize;
		}

		// The PrintPage event is raised for each page to be printed.
		private void pd_PrintPageGraphic(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
		{
			float offsetX,offsetY;
			float marginWidth = ev.MarginBounds.Width;
			float marginHeight = ev.MarginBounds.Height;
			float left = ev.MarginBounds.Left;
			float top = ev.MarginBounds.Top;
			SizeF footerSize;
			string footer;
			DrawTree d;
			
			if(scaleToPage)  // Print the whole argument on one page
			{
				Bitmap b;
				float hz,wz,fz; // Horizontal, vertical & final zoom
				// Calculate maximum width & height
				d = new DrawTree(0,0,1F);					// no offset or zoom
				b = d.drawTree(a.CurrentArg.findHead());	// create image
				
				hz = (marginHeight)/(d.MaxHeight+ 2 * DrawTree.margin);
				wz = (marginWidth)/(d.MaxWidth + 2 *DrawTree.margin);
				fz = Math.Min(hz,wz);	// Smallest zoom
				fz = Math.Min(fz,1F);	// No enlargement
				d = new DrawTree(left/fz,top/fz,fz);
				ev.Graphics.Clip = new Region(new RectangleF(left,top,marginWidth,marginHeight));
				d.drawTree(ev.Graphics,a.CurrentArg.findHead());
				ev.HasMorePages = false;
				return;
			}
			
			if(firstPage)
			{
				cpx = this.rangeStart -1;	// TODO Check - what if the page is in the second row?
				this.pageNumber = this.rangeStart;
			}
			offsetX = -cpx*marginWidth;
			offsetY = - cpy*marginHeight;
			d = new DrawTree(left+offsetX,top+offsetY,1);

			ev.Graphics.Clip = new Region(new RectangleF(left,top,marginWidth,marginHeight));
			d.drawTree(ev.Graphics,a.CurrentArg.findHead());
			
			if(firstPage)  // how many pages high if not calculated
			{
				float ph = d.MaxHeight/marginHeight;
				pagesHigh = (int) ph +1;  // round up
				float pw = d.MaxWidth/marginWidth;
				pagesWide = (int) pw +1;  // round up
			}

			// add page number in the lower right
			if(this.showPageNumber)
			{
				footer = "Page "+pageNumber++;
				Font f = new Font("Arial",10);
				SolidBrush brush = new SolidBrush(Color.Black);
				footerSize = calcFooter(footer,marginWidth,ev.Graphics,f);
				ev.Graphics.DrawString(footer,f,brush,
				                       left+marginWidth-footerSize.Width,
				                       top+marginHeight-footerSize.Height);
			}

			cpy++;  // next page down
			if(cpy >= pagesHigh)  // have we come to the bottom?
			{
				cpx++;  // next column of pages
				cpy=0;  // start from the top page
			}
			
			firstPage = false;

			// If more pages exist, print another page.
			if(pageNumber > this.rangeFinish)	// this was the last page to be printed
				ev.HasMorePages = false;
			else if (cpx < pagesWide) // any more pages across to go?
				ev.HasMorePages = true;
			else
				ev.HasMorePages = false;
		}


		/// <summary>Print the graphical view</summary>
		public void GraphicsPrinting()
		{
			PrintPageEventHandler p = new PrintPageEventHandler(pd_PrintPageGraphic);
			pd.PrintPage += p;
			// Print the document.
			pd.Print();
			pd.PrintPage -= p;
		}
	}
}
