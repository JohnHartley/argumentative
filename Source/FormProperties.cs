using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>
	/// Summary description for FormProperties.
	/// </summary>
	public class FormProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelElementTextDisplay;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageElement;
		private System.Windows.Forms.TabPage tabPageGlobal;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxAuthor;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePickerCreated;
		private System.Windows.Forms.HelpProvider helpProviderForProperties;
		private System.Windows.Forms.Label label5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// A form to display and change element and argument properties
		/// </summary>
		/// <param name="tv">Tree view connected to the argument</param>
		/// <param name="ami">Argument interface object</param>
		public FormProperties(TreeView tv,ArgMapInterface ami)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			if(tv.SelectedNode != null)
			{
				currentNode = tv.SelectedNode;
				Node n;
				bool dateEnabled;
				n = (Node) currentNode.Tag;
				this.textBoxComment.Text = n.getComment();  // get current text
				this.labelElementTextDisplay.Text = n.EditorText;
				// set up date field
				dateEnabled = ami.CurrentArg.CreationDateDefined;
				this.dateTimePickerCreated.Checked = dateEnabled;
				if(dateEnabled)
					this.dateTimePickerCreated.Value = ami.CurrentArg.CreationDate;
				// Created & Modified time & date
				this.labelCreatedDateTime.Text = n.Created.ToShortDateString() + "  " +
					n.Created.ToShortTimeString();
				this.labelModifiedDateTime.Text = n.Modified.ToShortDateString() + "  " +
					n.Modified.ToShortTimeString();
			}
			this.tv = tv;
			this.ami = ami;
			this.textBoxAuthor.Text = ami.CurrentArg.Author;
			commentChanged = false;
			FormProperties.isActive = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProperties));
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.labelElementTextDisplay = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageElement = new System.Windows.Forms.TabPage();
			this.labelModifiedDateTime = new System.Windows.Forms.Label();
			this.labelModified = new System.Windows.Forms.Label();
			this.labelCreatedDateTime = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tabPageGlobal = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.dateTimePickerCreated = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxAuthor = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.helpProviderForProperties = new System.Windows.Forms.HelpProvider();
			this.buttonSetToFileDate = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPageElement.SuspendLayout();
			this.tabPageGlobal.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxComment
			// 
			this.textBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxComment.Location = new System.Drawing.Point(8, 104);
			this.textBoxComment.Multiline = true;
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxComment.Size = new System.Drawing.Size(336, 162);
			this.textBoxComment.TabIndex = 0;
			this.textBoxComment.Text = "textBoxComment";
			this.textBoxComment.TextChanged += new System.EventHandler(this.textBoxComment_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Comment";
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.helpProviderForProperties.SetHelpString(this.buttonClose, "Close and save updates");
			this.buttonClose.Location = new System.Drawing.Point(292, 357);
			this.buttonClose.Name = "buttonClose";
			this.helpProviderForProperties.SetShowHelp(this.buttonClose, true);
			this.buttonClose.Size = new System.Drawing.Size(72, 24);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Element text";
			// 
			// labelElementTextDisplay
			// 
			this.labelElementTextDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.labelElementTextDisplay.Location = new System.Drawing.Point(16, 40);
			this.labelElementTextDisplay.Name = "labelElementTextDisplay";
			this.labelElementTextDisplay.Size = new System.Drawing.Size(320, 40);
			this.labelElementTextDisplay.TabIndex = 4;
			this.labelElementTextDisplay.Text = "ElementTextDisplay";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPageElement);
			this.tabControl1.Controls.Add(this.tabPageGlobal);
			this.tabControl1.Location = new System.Drawing.Point(8, 16);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(360, 335);
			this.tabControl1.TabIndex = 5;
			// 
			// tabPageElement
			// 
			this.tabPageElement.Controls.Add(this.labelModifiedDateTime);
			this.tabPageElement.Controls.Add(this.labelModified);
			this.tabPageElement.Controls.Add(this.labelCreatedDateTime);
			this.tabPageElement.Controls.Add(this.label6);
			this.tabPageElement.Controls.Add(this.label1);
			this.tabPageElement.Controls.Add(this.textBoxComment);
			this.tabPageElement.Controls.Add(this.label2);
			this.tabPageElement.Controls.Add(this.labelElementTextDisplay);
			this.tabPageElement.Location = new System.Drawing.Point(4, 22);
			this.tabPageElement.Name = "tabPageElement";
			this.tabPageElement.Size = new System.Drawing.Size(352, 309);
			this.tabPageElement.TabIndex = 0;
			this.tabPageElement.Text = "Element";
			// 
			// labelModifiedDateTime
			// 
			this.labelModifiedDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelModifiedDateTime.Location = new System.Drawing.Point(74, 285);
			this.labelModifiedDateTime.Name = "labelModifiedDateTime";
			this.labelModifiedDateTime.Size = new System.Drawing.Size(134, 16);
			this.labelModifiedDateTime.TabIndex = 8;
			this.labelModifiedDateTime.Text = "ModifiedDateTime";
			// 
			// labelModified
			// 
			this.labelModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelModified.Location = new System.Drawing.Point(8, 285);
			this.labelModified.Name = "labelModified";
			this.labelModified.Size = new System.Drawing.Size(50, 16);
			this.labelModified.TabIndex = 7;
			this.labelModified.Text = "Modified";
			// 
			// labelCreatedDateTime
			// 
			this.labelCreatedDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelCreatedDateTime.Location = new System.Drawing.Point(74, 269);
			this.labelCreatedDateTime.Name = "labelCreatedDateTime";
			this.labelCreatedDateTime.Size = new System.Drawing.Size(134, 16);
			this.labelCreatedDateTime.TabIndex = 6;
			this.labelCreatedDateTime.Text = "CreatedDateTime";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Location = new System.Drawing.Point(8, 269);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(50, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Created";
			// 
			// tabPageGlobal
			// 
			this.tabPageGlobal.Controls.Add(this.buttonSetToFileDate);
			this.tabPageGlobal.Controls.Add(this.label5);
			this.tabPageGlobal.Controls.Add(this.dateTimePickerCreated);
			this.tabPageGlobal.Controls.Add(this.label4);
			this.tabPageGlobal.Controls.Add(this.textBoxAuthor);
			this.tabPageGlobal.Controls.Add(this.label3);
			this.tabPageGlobal.Location = new System.Drawing.Point(4, 22);
			this.tabPageGlobal.Name = "tabPageGlobal";
			this.tabPageGlobal.Size = new System.Drawing.Size(352, 309);
			this.tabPageGlobal.TabIndex = 1;
			this.tabPageGlobal.Text = "Global";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(16, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(280, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Global properties apply to the whole argument map.";
			// 
			// dateTimePickerCreated
			// 
			this.dateTimePickerCreated.Checked = false;
			this.dateTimePickerCreated.Location = new System.Drawing.Point(16, 136);
			this.dateTimePickerCreated.Name = "dateTimePickerCreated";
			this.dateTimePickerCreated.ShowCheckBox = true;
			this.dateTimePickerCreated.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerCreated.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Created";
			// 
			// textBoxAuthor
			// 
			this.textBoxAuthor.Location = new System.Drawing.Point(16, 72);
			this.textBoxAuthor.Name = "textBoxAuthor";
			this.textBoxAuthor.Size = new System.Drawing.Size(160, 20);
			this.textBoxAuthor.TabIndex = 1;
			this.textBoxAuthor.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Author";
			// 
			// helpProviderForProperties
			// 
			this.helpProviderForProperties.HelpNamespace = "argumentative.chm";
			// 
			// buttonSetToFileDate
			// 
			this.buttonSetToFileDate.Location = new System.Drawing.Point(246, 135);
			this.buttonSetToFileDate.Name = "buttonSetToFileDate";
			this.buttonSetToFileDate.Size = new System.Drawing.Size(75, 23);
			this.buttonSetToFileDate.TabIndex = 5;
			this.buttonSetToFileDate.Text = "File Date";
			this.buttonSetToFileDate.UseVisualStyleBackColor = true;
			this.buttonSetToFileDate.Click += new System.EventHandler(this.ButtonSetToFileDateClick);
			// 
			// FormProperties
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(384, 395);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.buttonClose);
			this.helpProviderForProperties.SetHelpKeyword(this, "EditProperties.htm");
			this.helpProviderForProperties.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(400, 200);
			this.MinimizeBox = false;
			this.Name = "FormProperties";
			this.helpProviderForProperties.SetShowHelp(this, true);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Properties";
			this.TopMost = true;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProperties_Closing);
			this.tabControl1.ResumeLayout(false);
			this.tabPageElement.ResumeLayout(false);
			this.tabPageElement.PerformLayout();
			this.tabPageGlobal.ResumeLayout(false);
			this.tabPageGlobal.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonSetToFileDate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelCreatedDateTime;
		private System.Windows.Forms.Label labelModified;
		private System.Windows.Forms.Label labelModifiedDateTime;
		#endregion

		// Argumentative specific
		private TreeView tv;
		private bool commentChanged;
		private TreeNode currentNode;
		private ArgMapInterface ami;

		/// <summary>Set when window is open.</summary>
		public static bool isActive;

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			if(tv.SelectedNode !=null & commentChanged)
			{
				Node n;
				n = (Node) tv.SelectedNode.Tag;
				n.setComment(textBoxComment.Text);
			}
			updateGlobals();
			FormProperties.isActive = false;
			commentChanged = false;
			this.Close();
		}

		private void textBoxComment_TextChanged(object sender, System.EventArgs e)
		{
			// mark comments as having changed
			commentChanged = true;
		}

		private void updateGlobals()
		{
			DateTime dt;
			Argument arg;
			arg = ami.CurrentArg;
			// Check for changes in Global properties
			if( ! this.textBoxAuthor.Text.Equals(arg.Author))
				arg.Author = this.textBoxAuthor.Text;
			
			dt = this.dateTimePickerCreated.Value;
			if(arg.CreationDateDefined == this.dateTimePickerCreated.Enabled)
			{
				if(this.dateTimePickerCreated.Checked == true)
				{
					if(dt.Date.Ticks != arg.CreationDate.Ticks)
						arg.CreationDate = dt;
				}
			}
			else  // checkbox checked on date field
			{
				arg.CreationDateDefined = this.dateTimePickerCreated.Checked;
				if(this.dateTimePickerCreated.Checked)  // set date
					arg.CreationDate = dt;
			}
		}

		/// <summary>
		/// Changes the information in the window when the selected node changes
		/// </summary>
		/// <param name="tn">Selected TreeNode</param>
		public void changeSelection(TreeNode tn)
		{
			Node n;
			if(this.commentChanged)
			{
				n = (Node) currentNode.Tag;
				n.setComment(textBoxComment.Text);
			}
			currentNode = tn;
			n = (Node) tn.Tag;
			this.textBoxComment.Text = n.getComment();
			this.labelElementTextDisplay.Text = n.EditorText;
			this.textBoxAuthor.Text = ami.CurrentArg.Author;
			this.dateTimePickerCreated.Checked = ami.CurrentArg.CreationDateDefined;
			this.dateTimePickerCreated.Value = ami.CurrentArg.CreationDate;
			// Created & Modified time & date
			this.labelCreatedDateTime.Text = n.Created.ToShortDateString() + "  " +
				n.Created.ToShortTimeString();
			this.labelModifiedDateTime.Text = n.Modified.ToShortDateString() + "  " +
				n.Modified.ToShortTimeString();
			commentChanged = false;
		}

		private void FormProperties_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FormProperties.isActive = false;
		}
		
		void ButtonSetToFileDateClick(object sender, EventArgs e)
		{
			System.IO.FileInfo fi;
			if(! ami.CurrentFilename.Equals(Argument.UndefinedFile) )
			{
				fi = new System.IO.FileInfo(ami.CurrentFilename);
				this.dateTimePickerCreated.Value = fi.CreationTime;
			}
		}
	}
}
