using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argumentative
{
	/// <summary>
	/// Adds or removes full stops from argument nodes
	/// </summary>
	public class FormFullStops : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.RadioButton radioButtonAdd;
		private System.Windows.Forms.RadioButton radioButtonRemove;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// A constructor for the FormFullStops dialog box.
		/// </summary>
		/// <param name="ami"></param>
		public FormFullStops(ArgMapInterface ami)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
			this.head = ami.CurrentArg.findHead();;
			this.tv = ami.getTreeView();
			this.ami = ami;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormFullStops));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.radioButtonAdd = new System.Windows.Forms.RadioButton();
			this.radioButtonRemove = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(184, 120);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(64, 24);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(112, 120);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(56, 24);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// radioButtonAdd
			// 
			this.radioButtonAdd.Checked = true;
			this.radioButtonAdd.Location = new System.Drawing.Point(32, 48);
			this.radioButtonAdd.Name = "radioButtonAdd";
			this.radioButtonAdd.Size = new System.Drawing.Size(176, 16);
			this.radioButtonAdd.TabIndex = 2;
			this.radioButtonAdd.TabStop = true;
			this.radioButtonAdd.Text = "Add full stops";
			// 
			// radioButtonRemove
			// 
			this.radioButtonRemove.Location = new System.Drawing.Point(32, 72);
			this.radioButtonRemove.Name = "radioButtonRemove";
			this.radioButtonRemove.Size = new System.Drawing.Size(136, 16);
			this.radioButtonRemove.TabIndex = 3;
			this.radioButtonRemove.Text = "Remove full stops";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 32);
			this.label1.TabIndex = 4;
			this.label1.Text = "This option adds or removes full stops (.) from each element";
			// 
			// FormFullStops
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(256, 158);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.radioButtonRemove);
			this.Controls.Add(this.radioButtonAdd);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFullStops";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Add/Remove full stops";
			this.ResumeLayout(false);

		}
		#endregion


		ArgMapInterface ami;
		Node head;
		TreeView tv;
		bool add;
		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			add = this.radioButtonAdd.Checked;
			updateNode(head);
			ami.loadTree();				// rebuilds the TreeView
			DialogResult = DialogResult.OK;
		}
		private void updateNode(Node n)
		{
			int k;
			if(n.EditorText.EndsWith("."))
			{
				if(!add) // remove .
					n.EditorText = n.EditorText.Substring(0,n.EditorText.Length-1);
			}
			else if(n.EditorText.EndsWith("?"))
			{
				// leave question marks alone
			}
			else if(add)
				n.EditorText = n.EditorText + ".";
			for(k=0;k<n.countKids();k++)
				updateNode((Node) n.kids[k]);
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
