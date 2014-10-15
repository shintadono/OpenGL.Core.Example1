using System;
using System.Runtime.InteropServices;

namespace OpenGLHelper
{
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct Tuple2fs
	{
		public float x, y;

		public Tuple2fs(float x=0, float y=0)
		{ this.x=x; this.y=y; }

		public Tuple2fs(float[] p)
		{ x=p[0]; y=p[1]; }

		public bool EpsilonEquals(Tuple2fs a, float d)
		{
			float d1=x-a.x; if((d1>=0.0?d1:-d1)>d) return false;
			d1=y-a.y; return (d1>=0.0?d1:-d1)<=d;
		}

		public bool Check()
		{
			if(float.IsNaN(x)||float.IsNaN(y)) return false;
			return true;
		}

		#region Unary Operators
		//////////////////////////////////////////////////////////////////////
		// Unary Operators
		//////////////////////////////////////////////////////////////////////
		public static Tuple2fs operator+(Tuple2fs t) // does nothing
		{
			return t;
		}

		public static Tuple2fs operator-(Tuple2fs t) // inverts the sign of x and y
		{
			return new Tuple2fs(-t.x, -t.y);
		}

		public static float operator!(Tuple2fs t) // returns the magnitude
		{
			return (float)Math.Sqrt(t.x*t.x+t.y*t.y);
		}

		public static Tuple2fs operator~(Tuple2fs t) // returns the normalized Tuple2f (vector)
		{
			float f=!t;
			if(f==0) return new Tuple2fs(1, 0);
			return new Tuple2fs(t.x/f, t.y/f);
		}
		#endregion

		#region Binary Operators
		//////////////////////////////////////////////////////////////////////
		// Binary Operators
		//////////////////////////////////////////////////////////////////////
		public static Tuple2fs operator+(Tuple2fs a, Tuple2fs b)
		{
			return new Tuple2fs(a.x+b.x, a.y+b.y);
		}

		public static Tuple2fs operator-(Tuple2fs a, Tuple2fs b)
		{
			return new Tuple2fs(a.x-b.x, a.y-b.y);
		}

		public static float operator*(Tuple2fs a, Tuple2fs b) // dot product
		{
			return a.x*b.x+a.y*b.y;
		}

		public static Tuple2fs operator*(Tuple2fs a, float b) // scale
		{
			return new Tuple2fs(a.x*b, a.y*b);
		}

		public static Tuple3fs operator^(Tuple2fs a, Tuple2fs b) // cross product
		{
			return new Tuple3fs(0, 0, a.x*b.y-a.y*b.x);
		}
		#endregion
	}
}
