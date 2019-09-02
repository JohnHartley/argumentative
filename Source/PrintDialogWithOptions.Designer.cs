/*
 * Created by SharpDevelop.
 * User: John
 * Date: 26/08/2007
 * Time: 3:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Argumentative
{
	partial class PrintDialogWithOptions
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintDialogWithOptions));
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.labelTo = new System.Windows.Forms.Label();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.labelFrom = new System.Windows.Forms.Label();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButtonAll = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButtonText = new System.Windows.Forms.RadioButton();
			this.radioButtonGraphics = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.numericUpDownCopies = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonPreview = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.checkBoxShowPageNumber = new System.Windows.Forms.CheckBox();
			this.checkBoxSinglePage = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCopies)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(269, 212);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(363, 212);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxTo);
			this.groupBox1.Controls.Add(this.labelTo);
			this.groupBox1.Controls.Add(this.textBoxFrom);
			this.groupBox1.Controls.Add(this.labelFrom);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButtonAll);
			this.groupBox1.Location = new System.Drawing.Point(12, 98);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(206, 93);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Print Range";
			// 
			// textBoxTo
			// 
			this.textBoxTo.Location = new System.Drawing.Point(165, 52);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(35, 20);
			this.textBoxTo.TabIndex = 5;
			this.textBoxTo.Text = "1";
			// 
			// labelTo
			// 
			this.labelTo.Location = new System.Drawing.Point(147, 55);
			this.labelTo.Name = "labelTo";
			this.labelTo.Size = new System.Drawing.Size(23, 18);
			this.labelTo.TabIndex = 4;
			this.labelTo.Text = "to";
			// 
			// textBoxFrom
			// 
			this.textBoxFrom.Location = new System.Drawing.Point(106, 52);
			this.textBoxFrom.Name = "textBoxFrom";
			this.textBoxFrom.Size = new System.Drawing.Size(35, 20);
			this.textBoxFrom.TabIndex = 3;
			this.textBoxFrom.Text = "1";
			// 
			// labelFrom
			// 
			this.labelFrom.Location = new System.Drawing.Point(75, 55);
			this.labelFrom.Name = "labelFrom";
			this.labelFrom.Size = new System.Drawing.Size(32, 18);
			this.labelFrom.TabIndex = 2;
			this.labelFrom.Text = "from";
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(15, 49);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(58, 24);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "Pages";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButtonAll
			// 
			this.radioButtonAll.Checked = true;
			this.radioButtonAll.Location = new System.Drawing.Point(15, 19);
			this.radioButtonAll.Name = "radioButtonAll";
			this.radioButtonAll.Size = new System.Drawing.Size(58, 24);
			this.radioButtonAll.TabIndex = 0;
			this.radioButtonAll.TabStop = true;
			this.radioButtonAll.Text = "All";
			this.radioButtonAll.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButtonText);
			this.groupBox2.Controls.Add(this.radioButtonGraphics);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(206, 80);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "View";
			// 
			// radioButtonText
			// 
			this.radioButtonText.Location = new System.Drawing.Point(15, 40);
			this.radioButtonText.Name = "radioButtonText";
			this.radioButtonText.Size = new System.Drawing.Size(104, 24);
			this.radioButtonText.TabIndex = 1;
			this.radioButtonText.Text = "Text";
			this.radioButtonText.UseVisualStyleBackColor = true;
			// 
			// radioButtonGraphics
			// 
			this.radioButtonGraphics.Checked = true;
			this.radioButtonGraphics.Location = new System.Drawing.Point(15, 19);
			this.radioButtonGraphics.Name = "radioButtonGraphics";
			this.radioButtonGraphics.Size = new System.Drawing.Size(104, 24);
			this.radioButtonGraphics.TabIndex = 0;
			this.radioButtonGraphics.TabStop = true;
			this.radioButtonGraphics.Text = "Graphics";
			this.radioButtonGraphics.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.numericUpDownCopies);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Location = new System.Drawing.Point(238, 98);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 93);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Copies";
			// 
			// numericUpDownCopies
			// 
			this.numericUpDownCopies.Location = new System.Drawing.Point(125, 23);
			this.numericUpDownCopies.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.numericUpDownCopies.Name = "numericUpDownCopies";
			this.numericUpDownCopies.Size = new System.Drawing.Size(46, 20);
			this.numericUpDownCopies.TabIndex = 1;
			this.numericUpDownCopies.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Number of copies";
			// 
			// buttonPreview
			// 
			this.buttonPreview.Location = new System.Drawing.Point(151, 212);
			this.buttonPreview.Name = "buttonPreview";
			this.buttonPreview.Size = new System.Drawing.Size(75, 23);
			this.buttonPreview.TabIndex = 5;
			this.buttonPreview.Text = "Preview";
			this.buttonPreview.UseVisualStyleBackColor = true;
			this.buttonPreview.Click += new System.EventHandler(this.ButtonPreviewClick);
			// 
			// checkBoxShowPageNumber
			// 
			this.checkBoxShowPageNumber.Checked = true;
			this.checkBoxShowPageNumber.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxShowPageNumber.Location = new System.Drawing.Point(244, 33);
			this.checkBoxShowPageNumber.Name = "checkBoxShowPageNumber";
			this.checkBoxShowPageNumber.Size = new System.Drawing.Size(127, 23);
			this.checkBoxShowPageNumber.TabIndex = 6;
			this.checkBoxShowPageNumber.Text = "Show Page Number";
			this.checkBoxShowPageNumber.UseVisualStyleBackColor = true;
			// 
			// checkBoxSinglePage
			// 
			this.checkBoxSinglePage.Location = new System.Drawing.Point(244, 51);
			this.checkBoxSinglePage.Name = "checkBoxSinglePage";
			this.checkBoxSinglePage.Size = new System.Drawing.Size(137, 24);
			this.checkBoxSinglePage.TabIndex = 7;
			this.checkBoxSinglePage.Text = "Fit to single page";
			this.checkBoxSinglePage.UseVisualStyleBackColor = true;
			// 
			// PrintDialogWithOptions
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(454, 245);
			this.ControlBox = false;
			this.Controls.Add(this.checkBoxSinglePage);
			this.Controls.Add(this.checkBoxShowPageNumber);
			this.Controls.Add(this.buttonPreview);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "PrintDialogWithOptions";
			this.Text = "Print Argument Map";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCopies)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox checkBoxSinglePage;
		private System.Windows.Forms.CheckBox checkBoxShowPageNumber;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button buttonPreview;
		private System.Windows.Forms.NumericUpDown numericUpDownCopies;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radioButtonGraphics;
		private System.Windows.Forms.RadioButton radioButtonText;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonAll;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.Label labelFrom;
		private System.Windows.Forms.TextBox textBoxFrom;
		private System.Windows.Forms.Label labelTo;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
	}
}
