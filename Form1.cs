using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenGL.Core;
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
		double fRotationAngleCube=0.0f, fRotationAnglePyramid=0.0f;
		double fCubeRotationSpeed=0.0f, fPyramidRotationSpeed=0.0f;

		// Names of texture shaders and uniforms
		uint shaderVertexTextured, shaderFragmentTextured, programTextured;
		int uniformIndexProjectionMatrixTextured, uniformIndexModelViewMatrixTextured, uniformIndexSamplerTextured;

		// Names of text overlay shaders and uniforms
		uint shaderVertexTextOverlay, shaderFragmentTextOverlay, programTextOverlay;
		int uniformIndexProjectionMatrixTextOverlay, uniformIndexModelViewMatrixTextOverlay, uniformIndexSamplerTextOverlay, uniformIndexColorTextOverlay;

		// Matrices and transfer arrays
		Matrix4d projectionMatrix=new Matrix4d(), modelViewMatrix=new Matrix4d();
		float[] projectionMatrixArray=new float[16], modelViewMatrixArray=new float[16];

		// Names for textures, samplers, buffers and array objects
		uint[] uiSamplers, uiTextures, uiVBO, uiVAO;

		// a font
		OpenGLFont oglFont;
		#endregion

		#region Constants for texture usage
		const int textureFont=0;
		const int textureGold=1;
		const int textureSilver=2;
		#endregion

		#region Constants for sampler usage
		const int rectangleTextureSampler=0;
		const int nlTextureSampler=1;
		const int llTextureSampler=2;
		#endregion

		#region Constants Geometry Buffers and Objects
		const int geomtryVBO=0;
		const int geomtryVAO=0;
		const int textOverlayVBO=1;
		const int textOverlayVAO=1;
		#endregion

		#region Geometry
		Tuple3fs[] vCubeVertices=new Tuple3fs[]
		{
			// Front face
			new Tuple3fs(-0.5f, 0.5f, 0.5f), new Tuple3fs(0.5f, 0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, 0.5f), new Tuple3fs(-0.5f, -0.5f, 0.5f), new Tuple3fs(-0.5f, 0.5f, 0.5f),
			// Back face
			new Tuple3fs(0.5f, 0.5f, -0.5f), new Tuple3fs(-0.5f, 0.5f, -0.5f), new Tuple3fs(-0.5f, -0.5f, -0.5f), new Tuple3fs(-0.5f, -0.5f, -0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f), new Tuple3fs(0.5f, 0.5f, -0.5f),
			// Left face
			new Tuple3fs(-0.5f, 0.5f, -0.5f), new Tuple3fs(-0.5f, 0.5f, 0.5f), new Tuple3fs(-0.5f, -0.5f, 0.5f), new Tuple3fs(-0.5f, -0.5f, 0.5f), new Tuple3fs(-0.5f, -0.5f, -0.5f), new Tuple3fs(-0.5f, 0.5f, -0.5f),
			// Right face
			new Tuple3fs(0.5f, 0.5f, 0.5f), new Tuple3fs(0.5f, 0.5f, -0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f), new Tuple3fs(0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, 0.5f, 0.5f),
			// Top face
			new Tuple3fs(-0.5f, 0.5f, -0.5f), new Tuple3fs(0.5f, 0.5f, -0.5f), new Tuple3fs(0.5f, 0.5f, 0.5f), new Tuple3fs(0.5f, 0.5f, 0.5f), new Tuple3fs(-0.5f, 0.5f, 0.5f), new Tuple3fs(-0.5f, 0.5f, -0.5f),
			// Bottom face
			new Tuple3fs(-0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f), new Tuple3fs(-0.5f, -0.5f, -0.5f),new Tuple3fs(-0.5f, -0.5f, 0.5f),
		};

		Tuple2fs[] vCubeTexCoords=new Tuple2fs[] { new Tuple2fs(0.0f, 1.0f), new Tuple2fs(1.0f, 1.0f), new Tuple2fs(1.0f, 0.0f), new Tuple2fs(1.0f, 0.0f), new Tuple2fs(0.0f, 0.0f), new Tuple2fs(0.0f, 1.0f) };

		Tuple3fs[] vPyramidVertices=new Tuple3fs[]
		{
			// Front face
			new Tuple3fs(0.0f, 0.5f, 0.0f), new Tuple3fs(-0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, 0.5f),
			// Back face
			new Tuple3fs(0.0f, 0.5f, 0.0f), new Tuple3fs(0.5f, -0.5f, -0.5f), new Tuple3fs(-0.5f, -0.5f, -0.5f),
			// Left face
			new Tuple3fs(0.0f, 0.5f, 0.0f), new Tuple3fs(-0.5f, -0.5f, -0.5f), new Tuple3fs(-0.5f, -0.5f, 0.5f),
			// Right face
			new Tuple3fs(0.0f, 0.5f, 0.0f), new Tuple3fs(0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f)
		};

		Tuple2fs[] vPyramidTexCoords=new Tuple2fs[] { new Tuple2fs(0.5f, 1.0f), new Tuple2fs(0.0f, 0.0f), new Tuple2fs(1.0f, 0.0f) };

		Tuple3fs[] vGround=new Tuple3fs[]
		{
			new Tuple3fs(-50, -10, -50), new Tuple3fs(50, -10, -50), new Tuple3fs(50, -10, 50), new Tuple3fs(50, -10, 50), new Tuple3fs(-50, -10, 50), new Tuple3fs(-50, -10, -50)
		};
		#endregion

		bool Init()
		{
			#region Load Textured shader
			string vert=File.ReadAllText("Shaders\\Textured.vert");
			string frag=File.ReadAllText("Shaders\\Textured.frag");

			// Load shaders and create shader program
			int iCompilationStatus;

			shaderVertexTextured=gl.CreateShader(glShaderType.VERTEX_SHADER);
			gl.ShaderSource(shaderVertexTextured, vert);
			gl.CompileShader(shaderVertexTextured);
			gl.GetShaderi(shaderVertexTextured, glShaderParameter.COMPILE_STATUS, out iCompilationStatus);
			if(iCompilationStatus==0)
				return false;

			shaderFragmentTextured=gl.CreateShader(glShaderType.FRAGMENT_SHADER);
			gl.ShaderSource(shaderFragmentTextured, frag);
			gl.CompileShader(shaderFragmentTextured);
			gl.GetShaderi(shaderFragmentTextured, glShaderParameter.COMPILE_STATUS, out iCompilationStatus);
			if(iCompilationStatus==0)
				return false;

			programTextured=gl.CreateProgram();
			gl.AttachShader(programTextured, shaderVertexTextured);
			gl.AttachShader(programTextured, shaderFragmentTextured);

			gl.LinkProgram(programTextured);

			int iLinkStatus;
			gl.GetProgrami(programTextured, glProgramParameter.LINK_STATUS, out iLinkStatus);
			if(iLinkStatus==0)
				return false;
			#endregion

			#region Load Text Overlay shader
			string vertTO=File.ReadAllText("Shaders\\TextOverlay.vert");
			string fragTO=File.ReadAllText("Shaders\\TextOverlay.frag");

			// Load shaders and create shader program
			shaderVertexTextOverlay=gl.CreateShader(glShaderType.VERTEX_SHADER);
			gl.ShaderSource(shaderVertexTextOverlay, vertTO);
			gl.CompileShader(shaderVertexTextOverlay);
			gl.GetShaderi(shaderVertexTextOverlay, glShaderParameter.COMPILE_STATUS, out iCompilationStatus);
			if(iCompilationStatus==0)
				return false;

			shaderFragmentTextOverlay=gl.CreateShader(glShaderType.FRAGMENT_SHADER);
			gl.ShaderSource(shaderFragmentTextOverlay, fragTO);
			gl.CompileShader(shaderFragmentTextOverlay);
			gl.GetShaderi(shaderFragmentTextOverlay, glShaderParameter.COMPILE_STATUS, out iCompilationStatus);
			if(iCompilationStatus==0)
				return false;

			programTextOverlay=gl.CreateProgram();
			gl.AttachShader(programTextOverlay, shaderVertexTextOverlay);
			gl.AttachShader(programTextOverlay, shaderFragmentTextOverlay);

			gl.LinkProgram(programTextOverlay);

			gl.GetProgrami(programTextOverlay, glProgramParameter.LINK_STATUS, out iLinkStatus);
			if(iLinkStatus==0)
				return false;
			#endregion

			#region Get locations of shader uniforms
			uniformIndexProjectionMatrixTextured=gl.GetUniformLocation(programTextured, "projectionMatrix");
			uniformIndexModelViewMatrixTextured=gl.GetUniformLocation(programTextured, "modelViewMatrix");
			uniformIndexSamplerTextured=gl.GetUniformLocation(programTextured, "sampler");

			uniformIndexProjectionMatrixTextOverlay=gl.GetUniformLocation(programTextOverlay, "projectionMatrix");
			uniformIndexModelViewMatrixTextOverlay=gl.GetUniformLocation(programTextOverlay, "modelViewMatrix");
			uniformIndexSamplerTextOverlay=gl.GetUniformLocation(programTextOverlay, "sampler");
			uniformIndexColorTextOverlay=gl.GetUniformLocation(programTextOverlay, "textColor");
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

			#region Load font texture
			oglFont=new OpenGLFont("Segoe UI", 30, FontStyle.Regular, new Tuple<ushort, ushort>(32, 126));

			gl.BindTexture(glTextureTarget.TEXTURE_RECTANGLE, uiTextures[textureFont]);
			gl.PixelStorei(glPixelStoreParameter.UNPACK_ALIGNMENT, 1);
			gl.TexImage2D(glTexture2DProxyTarget.TEXTURE_RECTANGLE, 0, glInternalFormat.RED, oglFont.Width, oglFont.Height, 0, glPixelFormat.RED, glPixelDataType.UNSIGNED_BYTE, oglFont.bits);

			oglFont.bits=null; // free memory
			#endregion

			// reserve some sampler names
			uiSamplers=gl.GenSamplers(50);

			#region Init samplers
			gl.SamplerParameteri(uiSamplers[nlTextureSampler], glTextureParameter.TEXTURE_MAG_FILTER, glFilter.LINEAR);
			gl.SamplerParameteri(uiSamplers[nlTextureSampler], glTextureParameter.TEXTURE_MIN_FILTER, glFilter.NEAREST_MIPMAP_LINEAR);

			gl.SamplerParameteri(uiSamplers[llTextureSampler], glTextureParameter.TEXTURE_MAG_FILTER, glFilter.LINEAR);
			gl.SamplerParameteri(uiSamplers[llTextureSampler], glTextureParameter.TEXTURE_MIN_FILTER, glFilter.LINEAR_MIPMAP_LINEAR);

			gl.SamplerParameteri(uiSamplers[rectangleTextureSampler], glTextureParameter.TEXTURE_WRAP_S, glTextureWrapMode.CLAMP_TO_EDGE); // we need this line for rectangle textures to work
			gl.SamplerParameteri(uiSamplers[rectangleTextureSampler], glTextureParameter.TEXTURE_WRAP_T, glTextureWrapMode.CLAMP_TO_EDGE); // we need this line for rectangle textures to work
			gl.SamplerParameteri(uiSamplers[rectangleTextureSampler], glTextureParameter.TEXTURE_MAG_FILTER, glFilter.NEAREST);
			gl.SamplerParameteri(uiSamplers[rectangleTextureSampler], glTextureParameter.TEXTURE_MIN_FILTER, glFilter.NEAREST);
			#endregion

			var err=gl.GetError();

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

			gl.Enable(glCapability.DEPTH_TEST);
			gl.ClearDepth(1.0);

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

			// We just clear color
			gl.Clear(glBufferMask.COLOR_BUFFER_BIT|glBufferMask.DEPTH_BUFFER_BIT);

			gl.UseProgram(programTextured);

			#region Projection Matrix
			double near=0.01;
			double far=10000;
			double top=near*Math.Tan(45*D2R/2);
			projectionMatrix.SetFrustumMatrixSymmetric(top*width/height, top, near, far);

			projectionMatrix.GetElements(projectionMatrixArray);
			gl.UniformMatrix4fv(uniformIndexProjectionMatrixTextured, 1, true, projectionMatrixArray);
			#endregion

			#region ViewMatrix (Not Loaded, just build)
			Tuple3fs origin=new Tuple3fs(0, 12, 27);
			Tuple3fs target=new Tuple3fs(0, 0, 0);
			Tuple3fs up=new Tuple3fs(0, 1, 0);

			Tuple3fs dir=~(origin-target);
			Tuple3fs side=~(up^dir);
			up=~(dir^side);

			modelViewMatrix.SetRows(side, up, dir);
			modelViewMatrix*=Matrix4d.TranslationMatrix(target-origin);
			#endregion

			#region Render geometry
			// Activate texture unit and set texture and sampler
			gl.ActiveTexture(glTextureUnit.TEXTURE0);
			gl.BindTexture(glTextureTarget.TEXTURE_2D, uiTextures[textureGold]);
			gl.BindSampler(0, uiSamplers[nlTextureSampler]);

			// Tell shader which texture unit to use
			gl.Uniform1i(uniformIndexSamplerTextured, 0);

			// Set vertex array object for the next draws
			gl.BindVertexArray(uiVAO[geomtryVAO]);

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
			#endregion

			#region Render text overlay
			gl.UseProgram(programTextOverlay);

			gl.Enable(glCapability.BLEND);
			gl.BlendFunc(glBlendFuncFactor.SRC_ALPHA, glBlendFuncFactor.ONE_MINUS_SRC_ALPHA);
			gl.BindTexture(glTextureTarget.TEXTURE_RECTANGLE, uiTextures[textureFont]);

			gl.Uniform1i(uniformIndexSamplerTextOverlay, 0);
			gl.BindSampler(0, uiSamplers[rectangleTextureSampler]);

			#region Ortho matrix for text overlay in screen space coordinate
			projectionMatrix.SetOrthoMatrix(0, width, 0, height, -10, 10);

			projectionMatrix.GetElements(projectionMatrixArray);
			gl.UniformMatrix4fv(uniformIndexProjectionMatrixTextOverlay, 1, true, projectionMatrixArray);
			#endregion

			#region Text using absolute screen space coordinates
			gl.Uniform4f(uniformIndexColorTextOverlay, 0, 0.7f, 0, 1); // green

			modelViewMatrix.SetIdentity(); // no transformation
			gl.UniformMatrix4fv(uniformIndexModelViewMatrixTextOverlay, 1, false, modelViewMatrix.ToFloatArrayColumnMajor());

			DrawText(Text, (int)30, (int)height/2, OpenGLFont.AnchorPlacement.CenterLeft); // Draw the text 30 pixel from the left and half the screen down from the top
			#endregion

			#region Text placed and rotated with modelview martix
			gl.Uniform4f(uniformIndexColorTextOverlay, 1, 0, 0, 1); // red

			// move to screen center and rotate
			modelViewMatrix=Matrix4d.TranslationMatrix(width/2, height/2, 1)*Matrix4d.RotZMatrix(fRotationAnglePyramid);
			gl.UniformMatrix4fv(uniformIndexModelViewMatrixTextOverlay, 1, false, modelViewMatrix.ToFloatArrayColumnMajor());

			DrawText(Text+" Rotating", OpenGLFont.AnchorPlacement.Center); // no absolute offsets
			#endregion

			#endregion

			#region Object state machine
			if(wasUp) fCubeRotationSpeed-=0.001;
			if(wasDown) fCubeRotationSpeed+=0.001;
			if(wasRight) fPyramidRotationSpeed+=0.001;
			if(wasLeft) fPyramidRotationSpeed-=0.001;

			wasUp=wasDown=wasRight=wasLeft=false;

			fRotationAngleCube+=fCubeRotationSpeed;
			fRotationAnglePyramid+=fPyramidRotationSpeed;

			// do more events here
			if(wasF3)
			{
				// e.g. toggle something
			}
	
			wasF1=wasF2=wasF3=false;
			#endregion

			openGLControl1.Invalidate();
			framesInThisSecond++;
		}

		private void openGLControl1_Destroy(object sender, OpenGLDestroyEventArgs e)
		{
			if(init&&!badInit&&!e.Error)
			{
				// Uninit shaders
				gl.UseProgram(0);
				gl.DetachShader(programTextured, shaderVertexTextured);
				gl.DetachShader(programTextured, shaderFragmentTextured);
				gl.DeleteShader(shaderVertexTextured);
				gl.DeleteShader(shaderFragmentTextured);
				gl.DeleteProgram(programTextured);

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

		public void DrawText(string msg, OpenGLFont.AnchorPlacement anchor=OpenGLFont.AnchorPlacement.BottomLeft)
		{
			List<int> vboArray=oglFont.BuildDrawBuffer(msg, anchor);

			gl.BindVertexArray(uiVAO[2]);

			gl.BindBuffer(glBufferTarget.ARRAY_BUFFER, uiVBO[2]);
			gl.BufferData(glBufferTarget.ARRAY_BUFFER, vboArray.Count*sizeof(int), vboArray.ToArray(), glBufferUsage.DYNAMIC_DRAW);
			gl.EnableVertexAttribArray(0);
			gl.VertexAttribPointer(0, 2, glVertexAttribType.INT, false, sizeof(int)*4, 0);
			gl.EnableVertexAttribArray(1);
			gl.VertexAttribPointer(1, 2, glVertexAttribType.INT, false, sizeof(int)*4, sizeof(int)*2);

			gl.DrawArrays(glDrawMode.TRIANGLES, 0, vboArray.Count/4); // each element(Point) has x, y, u and v
		}

		public void DrawText(string msg, int posX, int posY, OpenGLFont.AnchorPlacement anchor=OpenGLFont.AnchorPlacement.BottomLeft)
		{
			List<int> vboArray=oglFont.BuildDrawBuffer(msg, posX, posY, out posX, anchor);

			gl.BindVertexArray(uiVAO[2]);

			gl.BindBuffer(glBufferTarget.ARRAY_BUFFER, uiVBO[2]);
			gl.BufferData(glBufferTarget.ARRAY_BUFFER, vboArray.Count*sizeof(int), vboArray.ToArray(), glBufferUsage.DYNAMIC_DRAW);
			gl.EnableVertexAttribArray(0);
			gl.VertexAttribPointer(0, 2, glVertexAttribType.INT, false, sizeof(int)*4, 0);
			gl.EnableVertexAttribArray(1);
			gl.VertexAttribPointer(1, 2, glVertexAttribType.INT, false, sizeof(int)*4, sizeof(int)*2);

			gl.DrawArrays(glDrawMode.TRIANGLES, 0, vboArray.Count/4); // each element(Point) has x, y, u and v
		}

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

			return ret;
		}
	}
}
