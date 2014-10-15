using System;
using System.Windows.Forms;
using OpenGL.Core;
using Win32.WGL;

namespace OpenGL.Core.Example1
{
	class Program
	{
		static void Main(string[] args)
		{
			if(!wgl.LoadWGLExtensions())
			{
				MessageBox.Show("Error loading wgl-extensions. Maybe missing a graphic driver or graphic driver to old to support the necessary features.",
					"Error loading OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}

			Form1 dlg=new Form1();

			wgl.MakeCurrent(dlg.openGLControl1.DC, dlg.openGLControl1.RC);
			gl.LoadOpenGL();
			wgl.MakeCurrent(IntPtr.Zero, IntPtr.Zero);

			dlg.ShowDialog();
		}
	}
}
