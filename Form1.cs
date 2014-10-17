using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenGLHelper;
using Win32.WGL;

namespace OpenGL.Core.Example1
{
	public partial class Form1 : Form
	{
		static TimeSpan oneSecond=TimeSpan.FromSeconds(1);
		static double ticksPerSecond=oneSecond.Ticks;
		const double D2R=Math.PI/180;

		public Form1()
		{
			InitializeComponent();

			// here the magic begins
			if(!openGLControl1.Setup(DontCareBool.TRUE, DontCareBool.TRUE, DontCareBool.FALSE, wgl.MajorVersion, wgl.MinorVersion))
				throw new NotSupportedException("Error initializing OpenGL render context. Maybe missing a graphic driver or graphic driver to old to support the necessary features.");
		}

		private void openGLControl1_Paint(object sender, PaintEventArgs e)
		{
			openGLControl1.DrawScene();
		}

		private void openGLControl1_Error(object sender, OpenGLErrorEventArgs e)
		{
			Console.WriteLine(e.Error.ToString());
		}

		bool updateProjectionMatrix=true;
		private void openGLControl1_Resize(object sender, EventArgs e)
		{
			updateProjectionMatrix=true;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			openGLControl1.PreDestoryCleanUp(); // Calls Destroy
		}

		#region Variables

		// OpenGL init states
		bool init=false;
		bool badInit=false;

		// Renderer speed stuff
		DateTime startTime=DateTime.Now;
		DateTime lastTime=DateTime.Now;
		int framesInLastSecond=10;
		int framesInThisSecond=10;

		// Object rotation states
		double heading=0, pitch=0;
		double fRotationAngleCube=0.0f, fRotationAnglePyramid=0.0f;
		double fCubeRotationSpeed=0.0f, fPyramidRotationSpeed=0.0f;
		bool showGeom=true;

		// A TextOverlayRenderer
		TextOverlayRenderer textOverlay;

		// A skybox
		SkyBox skyBox;

		// Names of texture shaders and uniforms
		ShaderProgram programTextured;
		int uniformIndexProjectionMatrixTextured, uniformIndexModelViewMatrixTextured, uniformIndexSamplerTextured;

		// Names for textures, samplers, buffers and array objects
		uint[] uiSamplers, uiTextures, uiVBO, uiVAO;
		#endregion

		#region Constants for texture usage
		const int textureGold=0;
		const int textureSilver=1;
		#endregion

		#region Constants for sampler usage
		const int nlTextureSampler=0;
		const int llTextureSampler=1;
		#endregion

		#region Constants Geometry Buffers and Objects
		const int geomtryVBO=0;
		const int geomtryVAO=0;
		#endregion

		bool Init()
		{
			#region Load shader programs and get uniform locations
			programTextured=new ShaderProgram(File.ReadAllText("Shaders\\Textured.vert"), File.ReadAllText("Shaders\\Textured.frag"));
			uniformIndexProjectionMatrixTextured=programTextured.GetUniformLocation("projectionMatrix");
			uniformIndexModelViewMatrixTextured=programTextured.GetUniformLocation("modelViewMatrix");
			uniformIndexSamplerTextured=programTextured.GetUniformLocation("sampler");

			programTextured.UseProgram();

			// Tell shader which texture unit to use
			gl.Uniform1i(uniformIndexSamplerTextured, 0);
			#endregion

			// reserve some texture names
			uiTextures=gl.GenTextures(50);

			#region Load textures
			glPixelFormat textureFormat;
			int textureWidth, textureHeight;
			byte[] textureBits=LoadTextureFromFile("Textures\\golddiag.jpg", out textureWidth, out textureHeight, out textureFormat);
			gl.BindTexture(glTextureTarget.TEXTURE_2D, uiTextures[textureGold]);
			gl.TexImage2D(glTexture2DProxyTarget.TEXTURE_2D, 0, glInternalFormat.RGB8, textureWidth, textureHeight, 0, textureFormat, glPixelDataType.UNSIGNED_BYTE, textureBits);
			gl.GenerateMipmap(glTextureTarget.TEXTURE_2D);

			textureBits=LoadTextureFromFile("Textures\\snow.jpg", out textureWidth, out textureHeight, out textureFormat);
			gl.BindTexture(glTextureTarget.TEXTURE_2D, uiTextures[textureSilver]);
			gl.TexImage2D(glTexture2DProxyTarget.TEXTURE_2D, 0, glInternalFormat.RGB8, textureWidth, textureHeight, 0, textureFormat, glPixelDataType.UNSIGNED_BYTE, textureBits);
			gl.GenerateMipmap(glTextureTarget.TEXTURE_2D);
			#endregion

			// reserve some sampler names
			uiSamplers=gl.GenSamplers(50);

			#region Init samplers
			gl.SamplerParameteri(uiSamplers[nlTextureSampler], glTextureParameter.TEXTURE_MAG_FILTER, glFilter.LINEAR);
			gl.SamplerParameteri(uiSamplers[nlTextureSampler], glTextureParameter.TEXTURE_MIN_FILTER, glFilter.NEAREST_MIPMAP_LINEAR);

			gl.SamplerParameteri(uiSamplers[llTextureSampler], glTextureParameter.TEXTURE_MAG_FILTER, glFilter.LINEAR);
			gl.SamplerParameteri(uiSamplers[llTextureSampler], glTextureParameter.TEXTURE_MIN_FILTER, glFilter.LINEAR_MIPMAP_LINEAR);
			#endregion

			gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

			// reserve some VBO ans VAO names
			uiVBO=gl.GenBuffers(50);
			uiVAO=gl.GenVertexArrays(50);

			List<float> vboArray=new List<float>();

			#region Build vertex buffer object
			// Add cube to VBO
			for(int i=0; i<36; i++)
			{
				vboArray.Add(vCubeVertices[i]);
				vboArray.Add(vCubeTexCoords[i%6]);
			}

			// Add pyramid to VBO
			for(int i=0; i<12; i++)
			{
				vboArray.Add(vPyramidVertices[i]);
				vboArray.Add(vPyramidTexCoords[i%3]);
			}

			// Add ground to VBO
			for(int i=0; i<6; i++)
			{
				vboArray.Add(vGround[i]);
				vboArray.Add(vCubeTexCoords[i%6]*5);
			}
			#endregion

			#region Build vertex array object
			gl.BindVertexArray(uiVAO[geomtryVAO]);

			gl.BindBuffer(glBufferTarget.ARRAY_BUFFER, uiVBO[geomtryVBO]);
			gl.BufferData(glBufferTarget.ARRAY_BUFFER, vboArray.Count*sizeof(float), vboArray.ToArray(), glBufferUsage.STATIC_DRAW);
			gl.EnableVertexAttribArray(0); // position
			gl.VertexAttribPointer(0, 3, glVertexAttribType.FLOAT, false, Marshal.SizeOf(typeof(Tuple3fs))+Marshal.SizeOf(typeof(Tuple2fs)), 0);
			gl.EnableVertexAttribArray(1); // texture coordinate
			gl.VertexAttribPointer(1, 2, glVertexAttribType.FLOAT, false, Marshal.SizeOf(typeof(Tuple3fs))+Marshal.SizeOf(typeof(Tuple2fs)), Marshal.SizeOf(typeof(Tuple3fs)));
			#endregion

			skyBox=new SkyBox();
			skyBox.LoadTextures("Textures\\SkyBox", LoadTextureFromFile);

			//textOverlay=new TextOverlayRenderer(new OpenGLFont("Segoe UI", 30, FontStyle.Regular, new Tuple<ushort, ushort>(32, 126)));
			textOverlay=new TextOverlayRenderer();

			gl.Enable(glCapability.DEPTH_TEST);
			gl.ClearDepth(1.0);

			updateProjectionMatrix=true;

			return true;
		}

		private void openGLControl1_Render(object sender, EventArgs e)
		{
			#region Initialization
			if(!init)
			{
				init=true;
				if(!Init()) badInit=true;
			}

			if(badInit)
			{
				// TODO Render Info
				return;
			}
			#endregion

			double width=openGLControl1.Width;
			double height=openGLControl1.Height;

			#region Framerate Stuff
			DateTime now=DateTime.Now;
			if((now-lastTime)>=oneSecond)
			{
				lastTime+=oneSecond;
				framesInLastSecond=framesInThisSecond;
				framesInThisSecond=0;
			}

			Text=string.Format("FPS: {0}", framesInLastSecond);
			#endregion

			#region Update Projection Matrix
			if(updateProjectionMatrix)
			{
				textOverlay.SetProjectionMatrix(Matrix4d.OrthoMatrix(0, width, 0, height, -10, 10).ToFloatArrayColumnMajor());

				double near=0.01, far=10000;
				double top=near*Math.Tan(45*D2R/2);
				float[] projectionMatrix=Matrix4d.FrustumMatrixSymmetric(top*width/height, top, near, far).ToFloatArrayColumnMajor();

				skyBox.SetProjectionMatrix(projectionMatrix);

				programTextured.UseProgram();
				gl.UniformMatrix4fv(uniformIndexProjectionMatrixTextured, 1, false, projectionMatrix);

				updateProjectionMatrix=false;
			}
			#endregion

			// We just clear color
			gl.Clear(glBufferMask.COLOR_BUFFER_BIT|glBufferMask.DEPTH_BUFFER_BIT);

			#region Make camera matrix containing only rotation (not translation)
			Matrix4d cameraMatrix=Matrix4d.RotXMatrix(-90*D2R); // look north (Z-Up)
			cameraMatrix*=Matrix4d.RotXMatrix(-pitch);
			cameraMatrix*=Matrix4d.RotZMatrix(-heading);
			#endregion

			skyBox.Draw(cameraMatrix.ToFloatArrayColumnMajor());

			// Finish camera matrix with translation
			cameraMatrix*=Matrix4d.TranslationMatrix(0, 50, 0);

			// Model is in Y-Up system
			Matrix4d modelViewMatrix=cameraMatrix*Matrix4d.RotXMatrix(90*D2R);

			#region Render geometry
			if(showGeom)
			{
				// Activate texture unit and set texture and sampler
				gl.ActiveTexture(glTextureUnit.TEXTURE0);
				gl.BindTexture(glTextureTarget.TEXTURE_2D, uiTextures[textureGold]);
				gl.BindSampler(0, uiSamplers[nlTextureSampler]);

				// Set vertex array object for the next draws
				gl.BindVertexArray(uiVAO[geomtryVAO]);

				programTextured.UseProgram();

				// Rendering of cube
				Matrix4d mCurrent=modelViewMatrix*Matrix4d.TranslationMatrix(-8, 0, 0)*Matrix4d.ScaleMatrix(10, 10, 10)*Matrix4d.RotXMatrix(fRotationAngleCube);
				gl.UniformMatrix4fv(uniformIndexModelViewMatrixTextured, 1, false, mCurrent.ToFloatArrayColumnMajor());
				gl.DrawArrays(glDrawMode.TRIANGLES, 0, 36);

				// Lets set another sampler - watch the behaviour of the texel of the different objects.
				gl.BindSampler(0, uiSamplers[llTextureSampler]);

				// Rendering of pyramid
				mCurrent=modelViewMatrix*Matrix4d.TranslationMatrix(8, 0, 0)*Matrix4d.ScaleMatrix(10, 10, 10)*Matrix4d.RotYMatrix(fRotationAnglePyramid);
				gl.UniformMatrix4fv(uniformIndexModelViewMatrixTextured, 1, false, mCurrent.ToFloatArrayColumnMajor());
				gl.DrawArrays(glDrawMode.TRIANGLES, 36, 12);

				// Render ground
				gl.BindTexture(glTextureTarget.TEXTURE_2D, uiTextures[textureSilver]);

				gl.BindSampler(0, uiSamplers[llTextureSampler]);

				gl.UniformMatrix4fv(uniformIndexModelViewMatrixTextured, 1, false, modelViewMatrix.ToFloatArrayColumnMajor());
				gl.DrawArrays(glDrawMode.TRIANGLES, 48, 6);
			}
			#endregion

			#region Render text overlay
			gl.Enable(glCapability.BLEND);
			gl.BlendFunc(glBlendFuncFactor.SRC_ALPHA, glBlendFuncFactor.ONE_MINUS_SRC_ALPHA);

			#region Text using absolute screen space coordinates
			textOverlay.SetColor(0, 0.6f, 0, 1); // green

			textOverlay.DrawText(Text, (int)1, (int)height-1, OpenGLFont.AnchorPlacement.TopLeft); // Draw the text in top left corner
			#endregion

			#region Text placed and rotated with modelview martix
			textOverlay.SetColor(1, 0, 0, 1); // red
			modelViewMatrix=Matrix4d.TranslationMatrix((int)width/2, (int)height/2, 1)*Matrix4d.RotZMatrix(fRotationAnglePyramid);

			textOverlay.DrawText(Text+" Rotating", modelViewMatrix.ToFloatArrayColumnMajor(), OpenGLFont.AnchorPlacement.Center); // no absolute offsets
			#endregion
			#endregion

			#region Object state machine
			if(wasUp) pitch+=0.01;
			if(wasDown) pitch-=0.01;
			if(wasRight) heading-=0.01;
			if(wasLeft) heading+=0.01;

			wasUp=wasDown=wasRight=wasLeft=false;

			if(wasF1)
			{
				if(fCubeRotationSpeed==0) fCubeRotationSpeed=-0.001;
				else if(fCubeRotationSpeed<0) fCubeRotationSpeed=0.001;
				else if(fCubeRotationSpeed>0) fCubeRotationSpeed=0;
			}

			if(wasF2)
			{
				if(fPyramidRotationSpeed==0) fPyramidRotationSpeed=-0.001;
				else if(fPyramidRotationSpeed<0) fPyramidRotationSpeed=0.001;
				else if(fPyramidRotationSpeed>0) fPyramidRotationSpeed=0;
			}

			// toggle geometry visibility
			if(wasF3) showGeom=!showGeom;

			wasF1=wasF2=wasF3=false;

			fRotationAngleCube+=fCubeRotationSpeed;
			fRotationAnglePyramid+=fPyramidRotationSpeed;
			#endregion

			openGLControl1.Invalidate();
			framesInThisSecond++;
		}

		private void openGLControl1_Destroy(object sender, OpenGLDestroyEventArgs e)
		{
			if(init&&!badInit&&!e.Error)
			{
				// Uninit and delete shader programs
				programTextured.Delete();

				// Unbind current vertex array
				gl.BindVertexArray(0);

				// Delete vertex array names and vertex buffer names
				if(uiVAO!=null) gl.DeleteVertexArrays(uiVAO.Length, uiVAO);
				if(uiVBO!=null) gl.DeleteBuffers(uiVBO.Length, uiVBO);

				// Unbind all textures
				gl.BindTexture(glTextureTarget.TEXTURE_2D, 0);
				gl.BindTexture(glTextureTarget.TEXTURE_RECTANGLE, 0);

				// unbind all samplers
				gl.BindSampler(0, 0);

				gl.ActiveTexture(glTextureUnit.TEXTURE0);

				// Delete texture names and sampler names
				if(uiTextures!=null) gl.DeleteTextures(uiTextures.Length, uiTextures);
				if(uiSamplers!=null) gl.DeleteSamplers(uiSamplers.Length, uiSamplers);

				skyBox.Delete();
				textOverlay.Delete();

				glErrorCode err=gl.GetError();
				if(err!=glErrorCode.NO_ERROR)
					Console.WriteLine(err.ToString());
			}
		}

		#region Keyboard event handling
		bool wasF1=false, wasF2=false, wasF3=false;
		bool wasUp=false, wasDown=false, wasRight=false, wasLeft=false;

		private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Up: wasUp=true; break;
				case Keys.Down: wasDown=true; break;
				case Keys.Right: wasRight=true; break;
				case Keys.Left: wasLeft=true; break;
				case Keys.F1: wasF1=true; break;
				case Keys.F2: wasF2=true; break;
				case Keys.F3: wasF3=true; break;
			}
		}
		#endregion

		public static byte[] LoadTextureFromFile(string filename, out int width, out int height, out glPixelFormat pixelformat)
		{
			Image img=Image.FromFile(filename);
			width=img.Size.Width;
			height=img.Size.Height;

			byte[] ret=null;

			if((img.PixelFormat&PixelFormat.Alpha)==PixelFormat.Alpha)
			{
				pixelformat=glPixelFormat.BGRA;
				ret=new byte[width*height*4];

				Bitmap bmp=new Bitmap(img);
				BitmapData data=bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				Marshal.Copy(data.Scan0, ret, 0, width*height*4);
				bmp.UnlockBits(data);
			}
			else
			{
				pixelformat=glPixelFormat.BGR;
				ret=new byte[width*height*3];

				Bitmap bmp=new Bitmap(img);
				BitmapData data=bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
				if(((int)width*3)==data.Stride)
				{
					Marshal.Copy(data.Scan0, ret, 0, width*height*3);
				}
				else
				{
					for(int i=0; i<height; i++) Marshal.Copy(IntPtr.Add(data.Scan0, i*data.Stride), ret, width*3*i, width*3);
				}

				bmp.UnlockBits(data);
				data=null;
				bmp.Dispose();
				bmp=null;
			}

			img.Dispose();
			img=null;

			// Flip image to opengl line order
			int lw=width*(pixelformat==glPixelFormat.BGR?3:4);

			byte[] retFlipped=new byte[ret.Length];

			for(int i=0; i<height; i++) Buffer.BlockCopy(ret, i*lw, retFlipped, (height-i-1)*lw, lw);

			return retFlipped;
		}
	}
}
