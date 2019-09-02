using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>
	/// Summary description for TransformDlg.
	/// </summary>
	public class TransformDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.TextBox textBoxXslOut;
		private System.Windows.Forms.TextBox textBoxXslFile;
		private System.Windows.Forms.CheckBox checkBoxCurrentFile;
		private System.Windows.Forms.Label labelOverview;
		private System.Windows.Forms.CheckBox checkBoxOpenWith;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;

		ArgMapInterface ami;

		/// <summary>
		/// Transformation (using XSLT) dialog box
		/// </summary>
		/// <param name="a"></param>
		public TransformDlg(ArgMapInterface a)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ami = a;
			textBox1.Text = a.CurrentFilename;
			
			System.IO.Directory.GetCurrentDirectory();
			
			Options.OptionsData od = Options.OptionsData.Instance;
			this.textBoxXslFile.Text = od.getlastXSL();
			this.textBoxXslOut.Text = od.getlastXSLoutput();
			correctOutputFile();
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
			this.components = new System.ComponentModel.Container();
			this.buttonOK = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBoxXslOut = new System.Windows.Forms.TextBox();
			this.buttonSetFile = new System.Windows.Forms.Button();
			this.buttonSetOutput = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.textBoxXslFile = new System.Windows.Forms.TextBox();
			this.buttonSetXSL = new System.Windows.Forms.Button();
			this.checkBoxCurrentFile = new System.Windows.Forms.CheckBox();
			this.labelOverview = new System.Windows.Forms.Label();
			this.checkBoxOpenWith = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(344, 224);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(80, 24);
			this.buttonOK.TabIndex = 11;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(432, 224);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(80, 24);
			this.ButtonCancel.TabIndex = 12;
			this.ButtonCancel.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "From";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 32);
			this.label2.TabIndex = 3;
			this.label2.Text = "With stylesheet";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 168);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "To";
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(72, 88);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(376, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "textBox1";
			this.toolTip.SetToolTip(this.textBox1, "File name to transform");
			// 
			// textBoxXslOut
			// 
			this.textBoxXslOut.Location = new System.Drawing.Point(72, 168);
			this.textBoxXslOut.Name = "textBoxXslOut";
			this.textBoxXslOut.Size = new System.Drawing.Size(376, 20);
			this.textBoxXslOut.TabIndex = 8;
			this.textBoxXslOut.Text = "textBox2";
			this.toolTip.SetToolTip(this.textBoxXslOut, "file name to transform to");
			this.textBoxXslOut.Leave += new System.EventHandler(this.TextBoxXslOutLeave);
			// 
			// buttonSetFile
			// 
			this.buttonSetFile.Enabled = false;
			this.buttonSetFile.Location = new System.Drawing.Point(456, 88);
			this.buttonSetFile.Name = "buttonSetFile";
			this.buttonSetFile.Size = new System.Drawing.Size(56, 24);
			this.buttonSetFile.TabIndex = 5;
			this.buttonSetFile.Text = "Change";
			this.toolTip.SetToolTip(this.buttonSetFile, "Selects the file you wish to transform using a file open dialog box");
			this.buttonSetFile.Click += new System.EventHandler(this.buttonSetFile_Click);
			// 
			// buttonSetOutput
			// 
			this.buttonSetOutput.Location = new System.Drawing.Point(456, 168);
			this.buttonSetOutput.Name = "buttonSetOutput";
			this.buttonSetOutput.Size = new System.Drawing.Size(56, 24);
			this.buttonSetOutput.TabIndex = 9;
			this.buttonSetOutput.Text = "Change";
			this.toolTip.SetToolTip(this.buttonSetOutput, "Selects the file you wish to create using a file open dialog box");
			this.buttonSetOutput.Click += new System.EventHandler(this.buttonSetOutput_Click);
			// 
			// textBoxXslFile
			// 
			this.textBoxXslFile.Location = new System.Drawing.Point(72, 128);
			this.textBoxXslFile.Name = "textBoxXslFile";
			this.textBoxXslFile.Size = new System.Drawing.Size(376, 20);
			this.textBoxXslFile.TabIndex = 6;
			this.textBoxXslFile.Text = "textBox3";
			this.toolTip.SetToolTip(this.textBoxXslFile, "XSL Style sheet to use");
			// 
			// buttonSetXSL
			// 
			this.buttonSetXSL.Location = new System.Drawing.Point(456, 128);
			this.buttonSetXSL.Name = "buttonSetXSL";
			this.buttonSetXSL.Size = new System.Drawing.Size(56, 24);
			this.buttonSetXSL.TabIndex = 7;
			this.buttonSetXSL.Text = "Change";
			this.toolTip.SetToolTip(this.buttonSetXSL, "Selects theXSL  file you wish to use with a file open dialog box");
			this.buttonSetXSL.Click += new System.EventHandler(this.buttonSetXSL_Click);
			// 
			// checkBoxCurrentFile
			// 
			this.checkBoxCurrentFile.Checked = true;
			this.checkBoxCurrentFile.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxCurrentFile.Location = new System.Drawing.Point(72, 64);
			this.checkBoxCurrentFile.Name = "checkBoxCurrentFile";
			this.checkBoxCurrentFile.Size = new System.Drawing.Size(104, 16);
			this.checkBoxCurrentFile.TabIndex = 13;
			this.checkBoxCurrentFile.Text = "Use current file";
			this.toolTip.SetToolTip(this.checkBoxCurrentFile, "Tick if you wish to transform the current argument. Remove tick if you wish to sp" +
			                        "ecify a file.");
			this.checkBoxCurrentFile.CheckedChanged += new System.EventHandler(this.checkBoxCurrentFile_CheckedChanged);
			// 
			// labelOverview
			// 
			this.labelOverview.Location = new System.Drawing.Point(72, 8);
			this.labelOverview.Name = "labelOverview";
			this.labelOverview.Size = new System.Drawing.Size(400, 32);
			this.labelOverview.TabIndex = 10;
			this.labelOverview.Text = "The Transform function takes a structured argument and transforms it into other f" +
				"ormats using an XSLT style sheet.";
			// 
			// checkBoxOpenWith
			// 
			this.checkBoxOpenWith.Checked = true;
			this.checkBoxOpenWith.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxOpenWith.Location = new System.Drawing.Point(72, 200);
			this.checkBoxOpenWith.Name = "checkBoxOpenWith";
			this.checkBoxOpenWith.Size = new System.Drawing.Size(142, 16);
			this.checkBoxOpenWith.TabIndex = 10;
			this.checkBoxOpenWith.Text = "Open with application";
			this.toolTip.SetToolTip(this.checkBoxOpenWith, "Opens the appropriate application. eg Notepad if the file ends with .txt, A web b" +
			                        "rowser if it ends with *.htm");
			// 
			// TransformDlg
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(530, 272);
			this.ControlBox = false;
			this.Controls.Add(this.checkBoxOpenWith);
			this.Controls.Add(this.labelOverview);
			this.Controls.Add(this.checkBoxCurrentFile);
			this.Controls.Add(this.textBoxXslFile);
			this.Controls.Add(this.buttonSetFile);
			this.Controls.Add(this.textBoxXslOut);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonSetOutput);
			this.Controls.Add(this.buttonSetXSL);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "TransformDlg";
			this.Text = "Transform";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button buttonSetXSL;
		private System.Windows.Forms.Button buttonSetFile;
		private System.Windows.Forms.Button buttonSetOutput;
		#endregion

		private void correctOutputFile()
		{
			if(this.textBoxXslOut.Text.Trim().Equals(""))
				this.textBoxXslOut.Text = System.IO.Path.ChangeExtension(textBox1.Text,"txt");
		}
		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			bool success;
			// check for files
			if(! checkBoxCurrentFile.Checked)
			{
				if(! System.IO.File.Exists(textBox1.Text))
				{
					MessageBox.Show(String.Format("From path does not exist: {0}",textBox1.Text));
					return;
				}
			}
			if(! System.IO.File.Exists(textBoxXslFile.Text))
			{
				MessageBox.Show(String.Format("With path (XSL file) does not exist: {0}",textBoxXslFile.Text));
				return;
			}
			// maybe test for existing file and ask before overwriting
			// Execute export
			Transform t = new Transform();
			try
			{
				if(this.checkBoxCurrentFile.Checked)
					success = t.doTransform("",textBoxXslFile.Text,textBoxXslOut.Text,ami.CurrentArg);
				// "" means use current argument in memory
				else
					success = t.doTransform(textBox1.Text,textBoxXslFile.Text,textBoxXslOut.Text,ami.CurrentArg);
				if(success)
				{
					if(checkBoxOpenWith.Checked)
					{
						System.Diagnostics.Process app;
						app = new System.Diagnostics.Process();
						app.StartInfo.FileName = textBoxXslOut.Text;
						app.Start();
						
					}
					else
						MessageBox.Show(String.Format("{0} successfuly created.",textBoxXslOut.Text),"Success");
				}
				Options.OptionsData od = Options.OptionsData.Instance;
				od.setlastXSL(textBoxXslFile.Text);
				od.setlastXSLoutput(textBoxXslOut.Text);
			}
			catch ( Exception ex )
			{
				MessageBox.Show(String.Format("XSL error. {0}",ex.Message));
			}
		}

		private void buttonSetFile_Click(object sender, System.EventArgs e)
		{
			this.openFileDialog1.Filter = "Argumentative files|*.axl|All files|*.*";
			openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(textBox1.Text);
			DialogResult r = openFileDialog1.ShowDialog();
			if(r==DialogResult.OK)
			{
				this.textBox1.Text = this.openFileDialog1.FileName;
			}
		}

		private void buttonSetOutput_Click(object sender, System.EventArgs e)
		{
			saveFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(textBoxXslOut.Text);
			saveFileDialog1.FileName = System.IO.Path.GetFileName(this.textBoxXslOut.Text);
			DialogResult r = this.saveFileDialog1.ShowDialog();
			
			if(r==DialogResult.OK)
			{
				textBoxXslOut.Text = this.saveFileDialog1.FileName;
			}
		}

		private void buttonSetXSL_Click(object sender, System.EventArgs e)
		{
			string dir;
			openFileDialog1.Filter = "XSL file (*.xsl?)|*.xsl;*.xslt";
			dir = textBoxXslFile.Text.Trim();
			if( ! String.IsNullOrEmpty(dir))
			{
				dir = System.IO.Path.GetDirectoryName(textBoxXslFile.Text);
				openFileDialog1.InitialDirectory = dir;
			}
			DialogResult r = openFileDialog1.ShowDialog();
			if(r==DialogResult.OK)
			{
				textBoxXslFile.Text = openFileDialog1.FileName;
			}
		}

		private void checkBoxCurrentFile_CheckedChanged(object sender, System.EventArgs e)
		{
			bool b;
			b = this.checkBoxCurrentFile.Checked;
			this.textBox1.Enabled = ! b;
			this.buttonSetFile.Enabled = ! b;
		}
		
		void TextBoxXslOutLeave(object sender, EventArgs e)
		{
			this.correctOutputFile();
		}
	}
}
