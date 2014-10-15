namespace OpenGL.Core.Example1
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components=null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing&&(components!=null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.openGLControl1 = new Win32.WGL.OpenGLControl();
			this.SuspendLayout();
			// 
			// openGLControl1
			// 
			this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.openGLControl1.Location = new System.Drawing.Point(0, 0);
			this.openGLControl1.Name = "openGLControl1";
			this.openGLControl1.Size = new System.Drawing.Size(947, 500);
			this.openGLControl1.TabIndex = 0;
			this.openGLControl1.Error += new Win32.WGL.OpenGLControl.ErrorEventHandler(this.openGLControl1_Error);
			this.openGLControl1.Render += new Win32.WGL.OpenGLControl.RenderEventHandler(this.openGLControl1_Render);
			this.openGLControl1.Destroy += new Win32.WGL.OpenGLControl.DestroyEventHandler(this.openGLControl1_Destroy);
			this.openGLControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_Paint);
			this.openGLControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyDown);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(947, 500);
			this.Controls.Add(this.openGLControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		public Win32.WGL.OpenGLControl openGLControl1;

	}
}