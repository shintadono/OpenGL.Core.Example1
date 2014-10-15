using System;
using System.Runtime.InteropServices;

namespace OpenGLHelper
{
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct Tuple3fs
	{
		public float x, y, z;

		public Tuple3fs(float x=0, float y=0, float z=0)
		{ this.x=x; this.y=y; this.z=z; }

		public Tuple3fs(float[] p)
		{ x=p[0]; y=p[1]; z=p[2]; }

		public Tuple3fs(Tuple2fs p, float z=0)
		{
			x=p.x;
			y=p.y;
			this.z=z;
		}

		public bool EpsilonEquals(Tuple3fs a, float d)
		{
			float d1=x-a.x; if((d1>=0.0?d1:-d1)>d) return false;
			d1=y-a.y; if((d1>=0.0?d1:-d1)>d) return false;
			d1=z-a.z; return (d1>=0.0?d1:-d1)<=d;
		}

		public bool Check()
		{
			if(float.IsNaN(x)||float.IsNaN(y)||float.IsNaN(z)) return false;
			return true;
		}

		#region Unary Operators
		//////////////////////////////////////////////////////////////////////
		// Unary Operators
		//////////////////////////////////////////////////////////////////////
		public static Tuple3fs operator+(Tuple3fs t) // does nothing
		{
			return t;
		}

		public static Tuple3fs operator-(Tuple3fs t) // inverts the sign of x, y and z
		{
			return new Tuple3fs(-t.x, -t.y, -t.z);
		}

		public static float operator!(Tuple3fs t) // returns the magnitude
		{
			return (float)Math.Sqrt(t.x*t.x+t.y*t.y+t.z*t.z);
		}

		public static Tuple3fs operator~(Tuple3fs t) // returns the normalized Tuple3d (vector)
		{
			float f=!t;
			if(f==0) return new Tuple3fs(1, 0, 0);
			return new Tuple3fs(t.x/f, t.y/f, t.z/f);
		}
		#endregion

		#region Binary Operators
		//////////////////////////////////////////////////////////////////////
		// Binary Operators
		//////////////////////////////////////////////////////////////////////
		public static Tuple3fs operator+(Tuple3fs a, Tuple3fs b)
		{
			return new Tuple3fs(a.x+b.x, a.y+b.y, a.z+b.z);
		}

		public static Tuple3fs operator-(Tuple3fs a, Tuple3fs b)
		{
			return new Tuple3fs(a.x-b.x, a.y-b.y, a.z-b.z);
		}

		public static float operator*(Tuple3fs a, Tuple3fs b) // dot product
		{
			return a.x*b.x+a.y*b.y+a.z*b.z;
		}

		public static Tuple3fs operator*(Tuple3fs a, float b) // scale
		{
			return new Tuple3fs(a.x*b, a.y*b, a.z*b);
		}

		public static Tuple3fs operator^(Tuple3fs a, Tuple3fs b) // cross product
		{
			return new Tuple3fs(a.y*b.z-a.z*b.y, a.z*b.x-a.x*b.z, a.x*b.y-a.y*b.x);
		}
		#endregion
	}
}
