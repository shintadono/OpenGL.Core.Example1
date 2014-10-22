using OpenGL.Helper;

namespace OpenGL.Core.Example1
{
	public partial class Form1
	{
		#region Geometry
		static Tuple3fs[] vCubeVertices=new Tuple3fs[]
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

		static Tuple2fs[] vCubeTexCoords=new Tuple2fs[] { new Tuple2fs(0.0f, 1.0f), new Tuple2fs(1.0f, 1.0f), new Tuple2fs(1.0f, 0.0f), new Tuple2fs(1.0f, 0.0f), new Tuple2fs(0.0f, 0.0f), new Tuple2fs(0.0f, 1.0f) };

		static Tuple3fs[] vCubeOutLineVertices=new Tuple3fs[]
		{
			new Tuple3fs(0.5f, 0.5f, 0.5f), new Tuple3fs(-0.5f, 0.5f, 0.5f),
			new Tuple3fs(-0.5f, 0.5f, 0.5f), new Tuple3fs(-0.5f, -0.5f, 0.5f),
			new Tuple3fs(-0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, 0.5f),
			new Tuple3fs(0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, 0.5f, 0.5f),

			new Tuple3fs(0.5f, 0.5f, -0.5f), new Tuple3fs(-0.5f, 0.5f, -0.5f),
			new Tuple3fs(-0.5f, 0.5f, -0.5f), new Tuple3fs(-0.5f, -0.5f, -0.5f),
			new Tuple3fs(-0.5f, -0.5f, -0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f),
			new Tuple3fs(0.5f, -0.5f, -0.5f), new Tuple3fs(0.5f, 0.5f, -0.5f),

			new Tuple3fs(0.5f, 0.5f, 0.5f), new Tuple3fs(0.5f, 0.5f, -0.5f),
			new Tuple3fs(-0.5f, 0.5f, 0.5f), new Tuple3fs(-0.5f, 0.5f, -0.5f),
			new Tuple3fs(-0.5f, -0.5f, 0.5f), new Tuple3fs(-0.5f, -0.5f, -0.5f),
			new Tuple3fs(0.5f, -0.5f, 0.5f), new Tuple3fs(0.5f, -0.5f, -0.5f),
		};

		static Tuple3fs[] vPyramidVertices=new Tuple3fs[]
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

		static Tuple2fs[] vPyramidTexCoords=new Tuple2fs[] { new Tuple2fs(0.5f, 1.0f), new Tuple2fs(0.0f, 0.0f), new Tuple2fs(1.0f, 0.0f) };

		static Tuple3fs[] vGround=new Tuple3fs[]
		{
			new Tuple3fs(-50, -10, -50), new Tuple3fs(50, -10, -50), new Tuple3fs(50, -10, 50), new Tuple3fs(50, -10, 50), new Tuple3fs(-50, -10, 50), new Tuple3fs(-50, -10, -50)
		};
		#endregion
	}
}
