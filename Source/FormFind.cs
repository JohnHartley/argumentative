using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>
	/// Dialogue box for the Edit / Find
	/// </summary>
	public class FormFind : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelFindWhat;
		private System.Windows.Forms.TextBox textBoxFind;
		private System.Windows.Forms.Button buttonFindNext;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.HelpProvider helpProviderFind;
		private System.Windows.Forms.CheckBox checkBoxIgnoreCase;
		private System.Windows.Forms.TextBox textBoxReplace;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.CheckBox checkBoxSearchComments;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Search dialog box
		/// </summary>
		/// <param name="tv">TreeView to search</param>
		/// <param name="findSpec">The find specification for repeated searches</param>
		public FormFind(TreeView tv,FindSpec findSpec)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
			this.tv					= tv;
			this.findSpec			= findSpec;
			justFound				= false;  // has found just been successful - used for replace
			this.textBoxFind.Text	= this.findSpec.SearchFor;
			this.checkBoxIgnoreCase.Checked = this.findSpec.IgnoreCase;
			textBoxFind.SelectAll();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFind));
			this.labelFindWhat = new System.Windows.Forms.Label();
			this.textBoxFind = new System.Windows.Forms.TextBox();
			this.buttonFindNext = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.helpProviderFind = new System.Windows.Forms.HelpProvider();
			this.checkBoxIgnoreCase = new System.Windows.Forms.CheckBox();
			this.textBoxReplace = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonReplace = new System.Windows.Forms.Button();
			this.checkBoxSearchComments = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// labelFindWhat
			// 
			this.labelFindWhat.Location = new System.Drawing.Point(16, 24);
			this.labelFindWhat.Name = "labelFindWhat";
			this.labelFindWhat.Size = new System.Drawing.Size(72, 16);
			this.labelFindWhat.TabIndex = 0;
			this.labelFindWhat.Text = "Find what:";
			// 
			// textBoxFind
			// 
			this.helpProviderFind.SetHelpKeyword(this.textBoxFind, "Edit.htm#Find");
			this.helpProviderFind.SetHelpNavigator(this.textBoxFind, System.Windows.Forms.HelpNavigator.Topic);
			this.helpProviderFind.SetHelpString(this.textBoxFind, "Type text to search for here.");
			this.textBoxFind.Location = new System.Drawing.Point(88, 24);
			this.textBoxFind.Name = "textBoxFind";
			this.helpProviderFind.SetShowHelp(this.textBoxFind, true);
			this.textBoxFind.Size = new System.Drawing.Size(216, 20);
			this.textBoxFind.TabIndex = 1;
			this.textBoxFind.Text = "Search for";
			// 
			// buttonFindNext
			// 
			this.buttonFindNext.Location = new System.Drawing.Point(328, 16);
			this.buttonFindNext.Name = "buttonFindNext";
			this.buttonFindNext.Size = new System.Drawing.Size(112, 24);
			this.buttonFindNext.TabIndex = 4;
			this.buttonFindNext.Text = "Find Next";
			this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(328, 96);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(112, 24);
			this.buttonClose.TabIndex = 6;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// helpProviderFind
			// 
			this.helpProviderFind.HelpNamespace = "argumentative.chm";
			// 
			// checkBoxIgnoreCase
			// 
			this.checkBoxIgnoreCase.Checked = true;
			this.checkBoxIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxIgnoreCase.Location = new System.Drawing.Point(88, 104);
			this.checkBoxIgnoreCase.Name = "checkBoxIgnoreCase";
			this.checkBoxIgnoreCase.Size = new System.Drawing.Size(112, 16);
			this.checkBoxIgnoreCase.TabIndex = 3;
			this.checkBoxIgnoreCase.Text = "Ignore Case";
			// 
			// textBoxReplace
			// 
			this.helpProviderFind.SetHelpKeyword(this.textBoxReplace, "Find.htm#Replace");
			this.helpProviderFind.SetHelpNavigator(this.textBoxReplace, System.Windows.Forms.HelpNavigator.Topic);
			this.helpProviderFind.SetHelpString(this.textBoxReplace, "Text to replace the search string with");
			this.textBoxReplace.Location = new System.Drawing.Point(88, 64);
			this.textBoxReplace.Name = "textBoxReplace";
			this.helpProviderFind.SetShowHelp(this.textBoxReplace, true);
			this.textBoxReplace.Size = new System.Drawing.Size(216, 20);
			this.textBoxReplace.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Replace with";
			// 
			// buttonReplace
			// 
			this.buttonReplace.Location = new System.Drawing.Point(328, 56);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.buttonReplace.Size = new System.Drawing.Size(112, 24);
			this.buttonReplace.TabIndex = 5;
			this.buttonReplace.Text = "Find && &Replace";
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// checkBoxSearchComments
			// 
			this.checkBoxSearchComments.Location = new System.Drawing.Point(88, 128);
			this.checkBoxSearchComments.Name = "checkBoxSearchComments";
			this.checkBoxSearchComments.Size = new System.Drawing.Size(120, 16);
			this.checkBoxSearchComments.TabIndex = 7;
			this.checkBoxSearchComments.Text = "Search Comments";
			// 
			// FormFind
			// 
			this.AcceptButton = this.buttonFindNext;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(448, 150);
			this.Controls.Add(this.checkBoxSearchComments);
			this.Controls.Add(this.textBoxFind);
			this.Controls.Add(this.textBoxReplace);
			this.Controls.Add(this.checkBoxIgnoreCase);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonFindNext);
			this.Controls.Add(this.labelFindWhat);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonReplace);
			this.HelpButton = true;
			this.helpProviderFind.SetHelpKeyword(this, "Find.htm");
			this.helpProviderFind.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(200, 60);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFind";
			this.helpProviderFind.SetShowHelp(this, true);
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

		// Data for this form / window
		private TreeView tv;
		private FindSpec findSpec;
		private bool justFound;

		private void buttonFindNext_Click(object sender, System.EventArgs e)
		{
			// search tree
			bool r;
			string s;
			
			s = this.textBoxFind.Text;
			r = findSpec.find(s,checkBoxIgnoreCase.Checked,checkBoxSearchComments.Checked);
			if(! r)
				MessageBox.Show(String.Format("{0} not found",s));
			else 
				justFound = true;
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			// clicking ignore case and close will remember the setting
			findSpec.IgnoreCase		= checkBoxIgnoreCase.Checked;
			findSpec.SearchComments	= checkBoxSearchComments.Checked;
			this.Close();
		}

		private void buttonReplace_Click(object sender, System.EventArgs e)
		{
			bool found;
			string s,rs,oldText,newText;
			
			s = this.textBoxFind.Text;
			rs = this.textBoxReplace.Text;
			if(s.Length == 0 & rs.Length == 0)  // anything to do
				return;
			if(! justFound)  // you need to find before you replace
				found = findSpec.find(s,checkBoxIgnoreCase.Checked,checkBoxSearchComments.Checked);
			else
				found = true;
			if(! found)
				MessageBox.Show(String.Format("{0} not found.",s));
			else  // replace
			{
				justFound = false;
				Node n = (Node) tv.SelectedNode.Tag;
				oldText = n.EditorText;
				newText = n.EditorText.Replace(s,rs);
				tv.SelectedNode.Text	= newText;
				n.EditorText			= newText;
			}

		}
	}
	/// <summary>
	/// Stores search specification between calls to find and find next.
	/// </summary>
	public class FindSpec
	{
		private string searchFor;
		
		/// <summary>
		/// Retrieves the search string.
		/// </summary>
		public string SearchFor {
			get { return searchFor; }
		}
		private bool ignoreCase;
		
		/// <summary>
		/// Gets or sets the ignore case flag
		/// </summary>
		public bool IgnoreCase {
			get { return ignoreCase; }
			set { ignoreCase = value; }
		}
		private bool searchComments;
		
		/// <summary>
		/// Search within comments flag
		/// </summary>
		public bool SearchComments {
			get { return searchComments; }
			set { searchComments = value; }
		}
		private TreeView tv;

		private TreeNode lastFoundNode;	// in which node was the text last found
		private int lastFoundPos;	// in what position

		/// <summary>
		/// Create new Find (Search) Specification
		/// </summary>
		/// <param name="search">String to search for</param>
		/// <param name="tv">Tree view</param>
		/// <param name="ignoreCase">Do not match case</param>
		public FindSpec(string search,TreeView tv,bool ignoreCase)
		{
			this.searchFor = search;
			this.ignoreCase = ignoreCase;
			this.tv = tv;
			// reset last found location
			lastFoundNode	= null;
			lastFoundPos	= -1;
		}
		/// <summary>
		/// Copy FindSpec object
		/// </summary>
		/// <returns>Copy of FindSpec object</returns>
		public FindSpec CopySpec()
		{
			FindSpec fs = new FindSpec(searchFor,tv,ignoreCase);
			return fs;
		}
		private void foundInComments()
		{
			MessageBox.Show("Match found in comments. Use F4 to view comments.");
		}
		
		/// <summary>
		/// Find string in current argument
		/// </summary>
		/// <param name="searchFor">String to search for</param>
		/// <param name="ignoreCase">Do not match case</param>
		/// <param name="searchComments">Search the comments properties.</param>
		/// <returns></returns>
		public bool find(string searchFor,bool ignoreCase,bool searchComments)
		{
			this.searchFor		= searchFor;
			this.ignoreCase		= ignoreCase;
			this.searchComments	= searchComments;

			tv.ExpandAll();  // find next visible needs to have all nodes seen
			tv.Focus();

			return find();
		}
		
		/// <summary>
		/// Find using current Search Specification
		/// </summary>
		/// <returns>True if match</returns>
		public bool find()
		{
			TreeNode tn;
			int f;
			string tns,sf;
			Node n;
			
			tn = tv.SelectedNode;
			if(tn==null) tn=tv.Nodes[0];  // select premise
			
			if(ignoreCase)
				sf = searchFor.ToLower();
			else
				sf = searchFor;

			while(tn != null)
			{
				if(ignoreCase)
					tns = tn.Text.ToLower();
				else
					tns = tn.Text;

				f = tns.IndexOf(sf);		// TODO Note -  this method only finds one instance per node
				if(f >= 0)
				{
					if(! (lastFoundNode == tn && f == lastFoundPos))
					{
						tv.SelectedNode	= tn;	// set current tree node
						lastFoundNode	= tn;	// set the current node as that last found
						lastFoundPos	= f;	// 
						return true;
					}
				}

				if(searchComments)
				{
					n = (Node) tn.Tag;
					if(ignoreCase)
						tns = n.getComment().ToLower();
					else
						tns = n.getComment();
					f = tns.IndexOf(sf);
					if(f >= 0)
					{
						tv.SelectedNode = tn;
						foundInComments();
						return true;
					}
				}
				tn = tn.NextVisibleNode;	// next visible node in the tree - the tree as viewed as a list
			}
			return false;
		}
	}
}
