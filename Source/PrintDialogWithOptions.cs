/*
 * Created by SharpDevelop.
 * User: John
 * Date: 26/08/2007
 * Time: 3:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>Print dialog.</summary>
	public partial class PrintDialogWithOptions : Form
	{
		/// <summary>True prints the Graphical view. False prints text</summary>
		public bool printGraphics = true;	// false prints text
		/// <summary>Print all pages</summary>
		public bool allPages = true;
		/// <summary>The first page to be printed</summary>
		public int fromPage;
			/// <summary>The last page to be printed</summary>
		public int toPage;
		/// <summary>Number of copies to print</summary>
		public int copies;				// how many to print
		/// <summary>Draw page numbers at the bottom of the page</summary>
		public bool showPageNumbers;
		private bool singlePage;
		/// <summary>Retrieve singlr page boolean</summary>
		public bool SinglePage { get { return singlePage; } }
		
		/// <summary>
		/// Print Dialog box
		/// </summary>
		public PrintDialogWithOptions()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
		}
		
		void calcFields()
		{
			printGraphics = this.radioButtonGraphics.Checked;
			allPages = this.radioButtonAll.Checked;
			fromPage = 1;
			toPage = 9999;
			if(! allPages)		// get page start and stop
			{
				Int32.TryParse(this.textBoxFrom.Text.Trim(),out fromPage);
				Int32.TryParse(this.textBoxTo.Text.Trim(),out toPage);
			}
			// copies = Int32.Parse(this
			copies = Decimal.ToInt32(numericUpDownCopies.Value);
			showPageNumbers = this.checkBoxShowPageNumber.Checked;
			singlePage = this.checkBoxSinglePage.Checked;
		}
		
		void ButtonOkClick(object sender, EventArgs e)
		{
			calcFields();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		
		void ButtonPreviewClick(object sender, EventArgs e)
		{
			calcFields();
			this.DialogResult = DialogResult.Abort;  // abort printing - in this case preview
			this.Close();
		}
	}
}
