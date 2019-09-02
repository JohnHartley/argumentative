using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Argumentative
{
	/// <summary>
	/// Standard version information windows form
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageAbout;
		private System.Windows.Forms.TabPage tabPageLicence;
		private System.Windows.Forms.TabPage tabPageThanks;
		private System.Windows.Forms.TextBox textBoxAbout;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox textBoxThanks;
		private System.Windows.Forms.TextBox textBoxLicence;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>About Dialog box. Shows version and licensing information.</summary>
		/// <param name="version">Version number. e.g. "0.6.5"</param>
		public About(string version)
		{
			InitializeComponent();

			
			Assembly ThisAssembly = Assembly.GetExecutingAssembly();
			AssemblyName ThisAssemblyName = ThisAssembly.GetName();
			string nl = System.Environment.NewLine;
			// the version number is manually updated
			string aboutText = "Argumentative Version "+version+
				Environment.NewLine+"Build "+
				ThisAssemblyName.Version.Major+"."+
				ThisAssemblyName.Version.Minor+"."+
				ThisAssemblyName.Version.Build+"."+
				ThisAssemblyName.Version.Revision+
				Environment.NewLine+
				".Net "+Environment.Version.ToString()+
				Environment.NewLine+
				"Windows: "+Environment.OSVersion.ToString()+
				Environment.NewLine+
				"Working memory: " +Environment.WorkingSet.ToString()+
				Environment.NewLine+
				"User Application data path: "+Application.UserAppDataPath+
				Environment.NewLine+
				"Locale: "+I18N.getLocale();
				;
			string thanksText = "Argumentative uses the following resources:"+nl+nl+
				"Installation: NSIS 2.24 http://nsis.sourceforge.net"+nl+
				"Spell Checker: Net Spell 2.1.7 http://sourceforge.net/projects/netspell/"+nl+
				"Development environment: #Develop 2.2.1  http://www.icsharpcode.net/OpenSource/SD/"+nl+
				"Unit Testing: NUnit 2.4.2  http://www.nunit.org"+nl+
				"NDoc 1.3.1  http://ndoc.sourceforge.net"
				;

			// string licenceText = "http://www.opensource.org/licenses/gpl-license.php";

			this.textBoxAbout.Text		= aboutText;
			this.textBoxThanks.Text		= thanksText;
			//this.textBoxLicence.Text	= licenceText;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageAbout = new System.Windows.Forms.TabPage();
			this.textBoxAbout = new System.Windows.Forms.TextBox();
			this.tabPageThanks = new System.Windows.Forms.TabPage();
			this.textBoxThanks = new System.Windows.Forms.TextBox();
			this.tabPageLicence = new System.Windows.Forms.TabPage();
			this.textBoxLicence = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPageAbout.SuspendLayout();
			this.tabPageThanks.SuspendLayout();
			this.tabPageLicence.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(40, 32);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(64, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(200, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Argumentative";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPageAbout);
			this.tabControl1.Controls.Add(this.tabPageThanks);
			this.tabControl1.Controls.Add(this.tabPageLicence);
			this.tabControl1.Location = new System.Drawing.Point(8, 56);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(464, 191);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPageAbout
			// 
			this.tabPageAbout.Controls.Add(this.textBoxAbout);
			this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
			this.tabPageAbout.Name = "tabPageAbout";
			this.tabPageAbout.Size = new System.Drawing.Size(456, 165);
			this.tabPageAbout.TabIndex = 0;
			this.tabPageAbout.Text = "About";
			// 
			// textBoxAbout
			// 
			this.textBoxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAbout.Location = new System.Drawing.Point(8, 8);
			this.textBoxAbout.Multiline = true;
			this.textBoxAbout.Name = "textBoxAbout";
			this.textBoxAbout.Size = new System.Drawing.Size(445, 154);
			this.textBoxAbout.TabIndex = 0;
			this.textBoxAbout.Text = "About";
			// 
			// tabPageThanks
			// 
			this.tabPageThanks.Controls.Add(this.textBoxThanks);
			this.tabPageThanks.Location = new System.Drawing.Point(4, 22);
			this.tabPageThanks.Name = "tabPageThanks";
			this.tabPageThanks.Size = new System.Drawing.Size(376, 166);
			this.tabPageThanks.TabIndex = 2;
			this.tabPageThanks.Text = "Thanks";
			// 
			// textBoxThanks
			// 
			this.textBoxThanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxThanks.Location = new System.Drawing.Point(12, 11);
			this.textBoxThanks.Multiline = true;
			this.textBoxThanks.Name = "textBoxThanks";
			this.textBoxThanks.Size = new System.Drawing.Size(338, 144);
			this.textBoxThanks.TabIndex = 1;
			this.textBoxThanks.Text = "Thanks";
			// 
			// tabPageLicence
			// 
			this.tabPageLicence.Controls.Add(this.textBoxLicence);
			this.tabPageLicence.Location = new System.Drawing.Point(4, 22);
			this.tabPageLicence.Name = "tabPageLicence";
			this.tabPageLicence.Size = new System.Drawing.Size(456, 165);
			this.tabPageLicence.TabIndex = 1;
			this.tabPageLicence.Text = "Licence";
			// 
			// textBoxLicence
			// 
			this.textBoxLicence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLicence.Location = new System.Drawing.Point(12, 11);
			this.textBoxLicence.Multiline = true;
			this.textBoxLicence.Name = "textBoxLicence";
			this.textBoxLicence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLicence.Size = new System.Drawing.Size(418, 143);
			this.textBoxLicence.TabIndex = 1;
			this.textBoxLicence.Text = resources.GetString("textBoxLicence.Text");
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(402, 271);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(72, 24);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// About
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 301);
			this.ControlBox = false;
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "About";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "About";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPageAbout.ResumeLayout(false);
			this.tabPageAbout.PerformLayout();
			this.tabPageThanks.ResumeLayout(false);
			this.tabPageThanks.PerformLayout();
			this.tabPageLicence.ResumeLayout(false);
			this.tabPageLicence.PerformLayout();
			this.ResumeLayout(false);
		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
