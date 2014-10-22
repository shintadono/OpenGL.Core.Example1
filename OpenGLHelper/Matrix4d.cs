using System;

namespace OpenGL.Helper
{
	public class Matrix4d
	{
		// Matrix 4x4 doubles
		//  _               _
		// | m00 m01 m02 m03 |
		// | m10 m11 m12 m13 |
		// | m20 m21 m22 m23 |
		// |_m30 m31 m32 m33_|

		internal double m00, m01, m02, m03;
		internal double m10, m11, m12, m13;
		internal double m20, m21, m22, m23;
		internal double m30, m31, m32, m33;

		#region Construction/Destruction
		//////////////////////////////////////////////////////////////////////
		// Construction/Destruction
		//////////////////////////////////////////////////////////////////////
		public Matrix4d() : this(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1) { }
		public Matrix4d(double m00, double m01, double m02, double m03,
			double m10, double m11, double m12, double m13,
			double m20, double m21, double m22, double m23,
			double m30, double m31, double m32, double m33)
		{
			this.m00=m00; this.m01=m01; this.m02=m02; this.m03=m03;
			this.m10=m10; this.m11=m11; this.m12=m12; this.m13=m13;
			this.m20=m20; this.m21=m21; this.m22=m22; this.m23=m23;
			this.m30=m30; this.m31=m31; this.m32=m32; this.m33=m33;
		}

		public Matrix4d(double[] matrix) // [0]=m00, [1]=m01, [2]=m02, [3]=m03, [4]=m10, [5]=m11 ...
		{
			m00=matrix[0]; m01=matrix[1]; m02=matrix[2]; m03=matrix[3];
			m10=matrix[4]; m11=matrix[5]; m12=matrix[6]; m13=matrix[7];
			m20=matrix[8]; m21=matrix[9]; m22=matrix[10]; m23=matrix[11];
			m30=matrix[12]; m31=matrix[13]; m32=matrix[14]; m33=matrix[15];
		}

		public Matrix4d(float[] matrix) // [0]=m00, [1]=m01, [2]=m02, [3]=m03, [4]=m10, [5]=m11 ...
		{
			m00=matrix[0]; m01=matrix[1]; m02=matrix[2]; m03=matrix[3];
			m10=matrix[4]; m11=matrix[5]; m12=matrix[6]; m13=matrix[7];
			m20=matrix[8]; m21=matrix[9]; m22=matrix[10]; m23=matrix[11];
			m30=matrix[12]; m31=matrix[13]; m32=matrix[14]; m33=matrix[15];
		}

		public Matrix4d(Matrix4d matrix)
		{
			m00=matrix.m00; m01=matrix.m01; m02=matrix.m02; m03=matrix.m03;
			m10=matrix.m10; m11=matrix.m11; m12=matrix.m12; m13=matrix.m13;
			m20=matrix.m20; m21=matrix.m21; m22=matrix.m22; m23=matrix.m23;
			m30=matrix.m30; m31=matrix.m31; m32=matrix.m32; m33=matrix.m33;
		}

		public override int GetHashCode()
		{
			long bits=BitConverter.DoubleToInt64Bits(m00);
			int hash=(int)(bits^(bits>>32));

			bits=BitConverter.DoubleToInt64Bits(m01); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m02); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m03); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m10); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m11); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m12); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m13); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m20); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m21); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m22); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m23); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m30); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m31); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m32); hash^=(int)(bits^(bits>>32));
			bits=BitConverter.DoubleToInt64Bits(m33); hash^=(int)(bits^(bits>>32));

			return hash;
		}

		public override string ToString()
		{
			return "[Matrix4d]";
		}
		#endregion

		#region Set Functions
		//////////////////////////////////////////////////////////////////////
		// Set Functions
		//////////////////////////////////////////////////////////////////////
		public Matrix4d Set(double m00, double m01, double m02, double m03,
			double m10, double m11, double m12, double m13,
			double m20, double m21, double m22, double m23,
			double m30, double m31, double m32, double m33)
		{
			this.m00=m00; this.m01=m01; this.m02=m02; this.m03=m03;
			this.m10=m10; this.m11=m11; this.m12=m12; this.m13=m13;
			this.m20=m20; this.m21=m21; this.m22=m22; this.m23=m23;
			this.m30=m30; this.m31=m31; this.m32=m32; this.m33=m33;
			return this;
		}

		public Matrix4d Set(Matrix4d matrix)
		{
			m00=matrix.m00; m01=matrix.m01; m02=matrix.m02; m03=matrix.m03;
			m10=matrix.m10; m11=matrix.m11; m12=matrix.m12; m13=matrix.m13;
			m20=matrix.m20; m21=matrix.m21; m22=matrix.m22; m23=matrix.m23;
			m30=matrix.m30; m31=matrix.m31; m32=matrix.m32; m33=matrix.m33;
			return this;
		}

		public Matrix4d Set(double[] matrix)
		{
			m00=matrix[0]; m01=matrix[1]; m02=matrix[2]; m03=matrix[3];
			m10=matrix[4]; m11=matrix[5]; m12=matrix[6]; m13=matrix[7];
			m20=matrix[8]; m21=matrix[9]; m22=matrix[10]; m23=matrix[11];
			m30=matrix[12]; m31=matrix[13]; m32=matrix[14]; m33=matrix[15];
			return this;
		}

		public Matrix4d Set(float[] matrix)
		{
			m00=matrix[0]; m01=matrix[1]; m02=matrix[2]; m03=matrix[3];
			m10=matrix[4]; m11=matrix[5]; m12=matrix[6]; m13=matrix[7];
			m20=matrix[8]; m21=matrix[9]; m22=matrix[10]; m23=matrix[11];
			m30=matrix[12]; m31=matrix[13]; m32=matrix[14]; m33=matrix[15];
			return this;
		}

		public Matrix4d Set(int row, int column, double val)
		{
			if(row==0)
			{
				if(column==0) m00=val;
				else if(column==1) m01=val;
				else if(column==2) m02=val;
				else if(column==3) m03=val;
			}
			else if(row==1)
			{
				if(column==0) m10=val;
				else if(column==1) m11=val;
				else if(column==2) m12=val;
				else if(column==3) m13=val;
			}
			else if(row==2)
			{
				if(column==0) m20=val;
				else if(column==1) m21=val;
				else if(column==2) m22=val;
				else if(column==3) m23=val;
			}
			else if(row==3)
			{
				if(column==0) m30=val;
				else if(column==1) m31=val;
				else if(column==2) m32=val;
				else if(column==3) m33=val;
			}
			return this;
		}

		public Matrix4d SetColumn(double x, double y, double z, double w, int column)
		{
			if(column==0) { m00=x; m10=y; m20=z; m30=w; }
			else if(column==1) { m01=x; m11=y; m21=z; m31=w; }
			else if(column==2) { m02=x; m12=y; m22=z; m32=w; }
			else if(column==3) { m03=x; m13=y; m23=z; m33=w; }
			return this;
		}

		public Matrix4d SetColumn(double[] doubles, int column)
		{
			if(column==0) { m00=doubles[0]; m10=doubles[1]; m20=doubles[2]; m30=doubles[3]; }
			else if(column==1) { m01=doubles[0]; m11=doubles[1]; m21=doubles[2]; m31=doubles[3]; }
			else if(column==2) { m02=doubles[0]; m12=doubles[1]; m22=doubles[2]; m32=doubles[3]; }
			else if(column==3) { m03=doubles[0]; m13=doubles[1]; m23=doubles[2]; m33=doubles[3]; }
			return this;
		}

		public Matrix4d SetRow(double x, double y, double z, double w, int row)
		{
			if(row==0) { m00=x; m01=y; m02=z; m03=w; }
			else if(row==1) { m10=x; m11=y; m12=z; m13=w; }
			else if(row==2) { m20=x; m21=y; m22=z; m23=w; }
			else if(row==3) { m30=x; m31=y; m32=z; m33=w; }
			return this;
		}

		public Matrix4d SetRow(double[] doubles, int row)
		{
			if(row==0) { m00=doubles[0]; m01=doubles[1]; m02=doubles[2]; m03=doubles[3]; }
			if(row==1) { m10=doubles[0]; m11=doubles[1]; m12=doubles[2]; m13=doubles[3]; }
			if(row==2) { m20=doubles[0]; m21=doubles[1]; m22=doubles[2]; m23=doubles[3]; }
			if(row==3) { m30=doubles[0]; m31=doubles[1]; m32=doubles[2]; m33=doubles[3]; }
			return this;
		}

		public Matrix4d SetIdentity()
		{
			m00=1; m01=0; m02=0; m03=0;
			m10=0; m11=1; m12=0; m13=0;
			m20=0; m21=0; m22=1; m23=0;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetZero()
		{
			m00=0; m01=0; m02=0; m03=0;
			m10=0; m11=0; m12=0; m13=0;
			m20=0; m21=0; m22=0; m23=0;
			m30=0; m31=0; m32=0; m33=0;
			return this;
		}

		public Matrix4d SetRotX(double angle)
		{
			double s=Math.Sin(angle);
			double c=Math.Cos(angle);
			m00=1; m01=0; m02=0; m03=0;
			m10=0; m11=c; m12=-s; m13=0;
			m20=0; m21=s; m22=c; m23=0;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetRotY(double angle)
		{
			double s=Math.Sin(angle);
			double c=Math.Cos(angle);
			m00=c; m01=0; m02=s; m03=0;
			m10=0; m11=1; m12=0; m13=0;
			m20=-s; m21=0; m22=c; m23=0;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetRotZ(double angle)
		{
			double s=Math.Sin(angle);
			double c=Math.Cos(angle);
			m00=c; m01=-s; m02=0; m03=0;
			m10=s; m11=c; m12=0; m13=0;
			m20=0; m21=0; m22=1; m23=0;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetRotation(double x, double y, double z, double angle)
		{
			double s=Math.Sin(angle); double c=Math.Cos(angle); double t=1-c;
			double mag=Math.Sqrt(x*x+y*y+z*z);
			x/=mag; y/=mag; z/=mag;
			m00=t*x*x+c; m01=t*x*y-s*z; m02=t*x*z+s*y;
			m10=t*x*y+s*z; m11=t*y*y+c; m12=t*y*z-s*x;
			m20=t*x*z-s*y; m21=t*y*z+s*x; m22=t*z*z+c;
			m03=m13=m23=0; m30=m31=m32=0; m33=1;
			return this;
		}

		public Matrix4d SetRotation(Tuple3fs axis, double angle)
		{
			double s=Math.Sin(angle); double c=Math.Cos(angle); double t=1-c;
			Tuple3fs myp=~axis; double x=myp.x; double y=myp.y; double z=myp.z;

			m00=t*x*x+c; m01=t*x*y-s*z; m02=t*x*z+s*y;
			m10=t*x*y+s*z; m11=t*y*y+c; m12=t*y*z-s*x;
			m20=t*x*z-s*y; m21=t*y*z+s*x; m22=t*z*z+c;
			m03=m13=m23=0; m30=m31=m32=0; m33=1;
			return this;
		}

		public Matrix4d SetTranslation(double x, double y, double z)
		{
			m00=1; m01=0; m02=0; m03=x;
			m10=0; m11=1; m12=0; m13=y;
			m20=0; m21=0; m22=1; m23=z;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetTranslation(Tuple3fs tuple)
		{
			m00=1; m01=0; m02=0; m03=tuple.x;
			m10=0; m11=1; m12=0; m13=tuple.y;
			m20=0; m21=0; m22=1; m23=tuple.z;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetScale(double x, double y, double z)
		{
			m01=m02=m03=0; m10=m12=m13=0;
			m20=m21=m23=0; m30=m31=m32=0;
			m00=x; m11=y; m22=z; m33=1;
			return this;
		}

		public Matrix4d SetScale(Tuple3fs tuple)
		{
			m01=m02=m03=0; m10=m12=m13=0;
			m20=m21=m23=0; m30=m31=m32=0;
			m00=tuple.x; m11=tuple.y; m22=tuple.z; m33=1;
			return this;
		}

		public static Matrix4d RotXMatrix(double angle)
		{
			return new Matrix4d().SetRotX(angle);
		}

		public static Matrix4d RotYMatrix(double angle)
		{
			return new Matrix4d().SetRotY(angle);
		}

		public static Matrix4d RotZMatrix(double angle)
		{
			return new Matrix4d().SetRotZ(angle);
		}

		public static Matrix4d RotationMatrix(double x, double y, double z, double angle)
		{
			return new Matrix4d().SetRotation(x, y, z, angle);
		}

		public static Matrix4d RotationMatrix(Tuple3fs axis, double angle)
		{
			return new Matrix4d().SetRotation(axis, angle);
		}

		public static Matrix4d TranslationMatrix(double x, double y, double z)
		{
			return new Matrix4d().SetTranslation(x, y, z);
		}

		public static Matrix4d TranslationMatrix(Tuple3fs tuple)
		{
			return new Matrix4d().SetTranslation(tuple);
		}

		public static Matrix4d ScaleMatrix(double x, double y, double z)
		{
			return new Matrix4d().SetScale(x, y, z);
		}

		public static Matrix4d ScaleMatrix(Tuple3fs tuple)
		{
			return new Matrix4d().SetScale(tuple);
		}
		#endregion

		#region Get Functions
		//////////////////////////////////////////////////////////////////////
		// Get Functions
		//////////////////////////////////////////////////////////////////////
		public double Get(int row, int column)
		{
			if(row==0)
			{
				if(column==0) return m00;
				else if(column==1) return m01;
				else if(column==2) return m02;
				else if(column==3) return m03;
			}
			else if(row==1)
			{
				if(column==0) return m10;
				else if(column==1) return m11;
				else if(column==2) return m12;
				else if(column==3) return m13;
			}
			else if(row==2)
			{
				if(column==0) return m20;
				else if(column==1) return m21;
				else if(column==2) return m22;
				else if(column==3) return m23;
			}
			else if(row==3)
			{
				if(column==0) return m30;
				else if(column==1) return m31;
				else if(column==2) return m32;
				else if(column==3) return m33;
			}

			return 0;
		}

		public void GetElements(
			out double m00, out double m01, out double m02, out double m03,
			out double m10, out double m11, out double m12, out double m13,
			out double m20, out double m21, out double m22, out double m23,
			out double m30, out double m31, out double m32, out double m33)
		{
			m00=this.m00; m01=this.m01; m02=this.m02; m03=this.m03;
			m10=this.m10; m11=this.m11; m12=this.m12; m13=this.m13;
			m20=this.m20; m21=this.m21; m22=this.m22; m23=this.m23;
			m30=this.m30; m31=this.m31; m32=this.m32; m33=this.m33;
		}

		public void GetElements(float[] mat)
		{
			mat[0]=(float)m00; mat[1]=(float)m01; mat[2]=(float)m02; mat[3]=(float)m03;
			mat[4]=(float)m10; mat[5]=(float)m11; mat[6]=(float)m12; mat[7]=(float)m13;
			mat[8]=(float)m20; mat[9]=(float)m21; mat[10]=(float)m22; mat[11]=(float)m23;
			mat[12]=(float)m30; mat[13]=(float)m31; mat[14]=(float)m32; mat[15]=(float)m33;
		}

		public void GetElementsTransposed(float[] mat)
		{
			mat[0]=(float)m00; mat[4]=(float)m01; mat[8]=(float)m02; mat[12]=(float)m03;
			mat[1]=(float)m10; mat[5]=(float)m11; mat[9]=(float)m12; mat[13]=(float)m13;
			mat[2]=(float)m20; mat[6]=(float)m21; mat[10]=(float)m22; mat[14]=(float)m23;
			mat[3]=(float)m30; mat[7]=(float)m31; mat[11]=(float)m32; mat[15]=(float)m33;
		}

		public void GetElements(double[] mat)
		{
			mat[0]=m00; mat[1]=m01; mat[2]=m02; mat[3]=m03;
			mat[4]=m10; mat[5]=m11; mat[6]=m12; mat[7]=m13;
			mat[8]=m20; mat[9]=m21; mat[10]=m22; mat[11]=m23;
			mat[12]=m30; mat[13]=m31; mat[14]=m32; mat[15]=m33;
		}

		public void GetElementsTransposed(double[] mat)
		{
			mat[0]=m00; mat[1]=m10; mat[2]=m20; mat[3]=m30;
			mat[4]=m01; mat[5]=m11; mat[6]=m21; mat[7]=m31;
			mat[8]=m02; mat[9]=m12; mat[10]=m22; mat[11]=m32;
			mat[12]=m03; mat[13]=m13; mat[14]=m23; mat[15]=m33;
		}

		public float[] ToFloatArrayRowMajor()
		{
			float[] mat=new float[16];
			GetElements(mat);
			return mat;
		}

		public float[] ToFloatArrayColumnMajor()
		{
			float[] mat=new float[16];
			GetElementsTransposed(mat);
			return mat;
		}

		public double[] ToArrayRowMajor()
		{
			double[] mat=new double[16];
			GetElements(mat);
			return mat;
		}

		public double[] ToArrayColumnMajor()
		{
			double[] mat=new double[16];
			GetElementsTransposed(mat);
			return mat;
		}

		public double M00 { get { return m00; } }
		public double M01 { get { return m01; } }
		public double M02 { get { return m02; } }
		public double M03 { get { return m03; } }
		public double M10 { get { return m10; } }
		public double M11 { get { return m11; } }
		public double M12 { get { return m12; } }
		public double M13 { get { return m13; } }
		public double M20 { get { return m20; } }
		public double M21 { get { return m21; } }
		public double M22 { get { return m22; } }
		public double M23 { get { return m23; } }
		public double M30 { get { return m30; } }
		public double M31 { get { return m31; } }
		public double M32 { get { return m32; } }
		public double M33 { get { return m33; } }
		#endregion

		#region Operators
		//////////////////////////////////////////////////////////////////////
		// Operators
		//////////////////////////////////////////////////////////////////////
		public static Matrix4d operator*(Matrix4d a, Matrix4d b)
		{
			Matrix4d ret=new Matrix4d();
			ret.m00=a.m00*b.m00+a.m01*b.m10+a.m02*b.m20+a.m03*b.m30;
			ret.m01=a.m00*b.m01+a.m01*b.m11+a.m02*b.m21+a.m03*b.m31;
			ret.m02=a.m00*b.m02+a.m01*b.m12+a.m02*b.m22+a.m03*b.m32;
			ret.m03=a.m00*b.m03+a.m01*b.m13+a.m02*b.m23+a.m03*b.m33;

			ret.m10=a.m10*b.m00+a.m11*b.m10+a.m12*b.m20+a.m13*b.m30;
			ret.m11=a.m10*b.m01+a.m11*b.m11+a.m12*b.m21+a.m13*b.m31;
			ret.m12=a.m10*b.m02+a.m11*b.m12+a.m12*b.m22+a.m13*b.m32;
			ret.m13=a.m10*b.m03+a.m11*b.m13+a.m12*b.m23+a.m13*b.m33;

			ret.m20=a.m20*b.m00+a.m21*b.m10+a.m22*b.m20+a.m23*b.m30;
			ret.m21=a.m20*b.m01+a.m21*b.m11+a.m22*b.m21+a.m23*b.m31;
			ret.m22=a.m20*b.m02+a.m21*b.m12+a.m22*b.m22+a.m23*b.m32;
			ret.m23=a.m20*b.m03+a.m21*b.m13+a.m22*b.m23+a.m23*b.m33;

			ret.m30=a.m30*b.m00+a.m31*b.m10+a.m32*b.m20+a.m33*b.m30;
			ret.m31=a.m30*b.m01+a.m31*b.m11+a.m32*b.m21+a.m33*b.m31;
			ret.m32=a.m30*b.m02+a.m31*b.m12+a.m32*b.m22+a.m33*b.m32;
			ret.m33=a.m30*b.m03+a.m31*b.m13+a.m32*b.m23+a.m33*b.m33;
			return ret;
		}

		public static Tuple3fs operator*(Matrix4d a, Tuple3fs p)
		{
			double x=a.m00*p.x+a.m01*p.y+a.m02*p.z+a.m03;
			double y=a.m10*p.x+a.m11*p.y+a.m12*p.z+a.m13;
			double z=a.m20*p.x+a.m21*p.y+a.m22*p.z+a.m23;
			double w=a.m30*p.x+a.m31*p.y+a.m32*p.z+a.m33;
			return new Tuple3fs((float)(x/w), (float)(y/w), (float)(z/w));
		}

		public static Matrix4d operator*(Matrix4d a, double d)
		{
			return new Matrix4d(
				a.m00*d, a.m01*d, a.m02*d, a.m03*d,
				a.m10*d, a.m11*d, a.m12*d, a.m13*d,
				a.m20*d, a.m21*d, a.m22*d, a.m23*d,
				a.m30*d, a.m31*d, a.m32*d, a.m33*d);
		}

		public static Matrix4d operator+(Matrix4d a, Matrix4d m)
		{
			return new Matrix4d(
				a.m00+m.m00, a.m01+m.m01, a.m02+m.m02, a.m03+m.m03,
				a.m10+m.m10, a.m11+m.m11, a.m12+m.m12, a.m13+m.m13,
				a.m20+m.m20, a.m21+m.m21, a.m22+m.m22, a.m23+m.m23,
				a.m30+m.m30, a.m31+m.m31, a.m32+m.m32, a.m33+m.m33);
		}

		public static Matrix4d operator-(Matrix4d a, Matrix4d m)
		{
			return new Matrix4d(
				a.m00-m.m00, a.m01-m.m01, a.m02-m.m02, a.m03-m.m03,
				a.m10-m.m10, a.m11-m.m11, a.m12-m.m12, a.m13-m.m13,
				a.m20-m.m20, a.m21-m.m21, a.m22-m.m22, a.m23-m.m23,
				a.m30-m.m30, a.m31-m.m31, a.m32-m.m32, a.m33-m.m33);
		}

		public static Matrix4d operator+(Matrix4d a)
		{
			return a;
		}

		public static Matrix4d operator-(Matrix4d a)
		{
			return new Matrix4d(
				-a.m00, -a.m01, -a.m02, -a.m03,
				-a.m10, -a.m11, -a.m12, -a.m13,
				-a.m20, -a.m21, -a.m22, -a.m23,
				-a.m30, -a.m31, -a.m32, -a.m33);
		}

		public static Matrix4d operator~(Matrix4d a)
		{
			return new Matrix4d(
				a.m00, a.m10, a.m20, a.m30,
				a.m01, a.m11, a.m21, a.m31,
				a.m02, a.m12, a.m22, a.m32,
				a.m03, a.m13, a.m23, a.m33);
		}

		public static Matrix4d operator!(Matrix4d a)
		{
			double d=a.Det;
			if(Math.Abs(d)<1e-10) throw new ArithmeticException("Determinate of the matrix is 0, this matrix can't be inverted");
			return new Matrix4d(
				(a.m11*(a.m22*a.m33-a.m23*a.m32)+a.m12*(a.m23*a.m31-a.m21*a.m33)+a.m13*(a.m21*a.m32-a.m22*a.m31))/d,
				(a.m21*(a.m02*a.m33-a.m03*a.m32)+a.m22*(a.m03*a.m31-a.m01*a.m33)+a.m23*(a.m01*a.m32-a.m02*a.m31))/d,
				(a.m31*(a.m02*a.m13-a.m03*a.m12)+a.m32*(a.m03*a.m11-a.m01*a.m13)+a.m33*(a.m01*a.m12-a.m02*a.m11))/d,
				(a.m01*(a.m13*a.m22-a.m12*a.m23)+a.m02*(a.m11*a.m23-a.m13*a.m21)+a.m03*(a.m12*a.m21-a.m11*a.m22))/d,
				(a.m12*(a.m20*a.m33-a.m23*a.m30)+a.m13*(a.m22*a.m30-a.m20*a.m32)+a.m10*(a.m23*a.m32-a.m22*a.m33))/d,
				(a.m22*(a.m00*a.m33-a.m03*a.m30)+a.m23*(a.m02*a.m30-a.m00*a.m32)+a.m20*(a.m03*a.m32-a.m02*a.m33))/d,
				(a.m32*(a.m00*a.m13-a.m03*a.m10)+a.m33*(a.m02*a.m10-a.m00*a.m12)+a.m30*(a.m03*a.m12-a.m02*a.m13))/d,
				(a.m02*(a.m13*a.m20-a.m10*a.m23)+a.m03*(a.m10*a.m22-a.m12*a.m20)+a.m00*(a.m12*a.m23-a.m13*a.m22))/d,
				(a.m13*(a.m20*a.m31-a.m21*a.m30)+a.m10*(a.m21*a.m33-a.m23*a.m31)+a.m11*(a.m23*a.m30-a.m20*a.m33))/d,
				(a.m23*(a.m00*a.m31-a.m01*a.m30)+a.m20*(a.m01*a.m33-a.m03*a.m31)+a.m21*(a.m03*a.m30-a.m00*a.m33))/d,
				(a.m33*(a.m00*a.m11-a.m01*a.m10)+a.m30*(a.m01*a.m13-a.m03*a.m11)+a.m31*(a.m03*a.m10-a.m00*a.m13))/d,
				(a.m03*(a.m11*a.m20-a.m10*a.m21)+a.m00*(a.m13*a.m21-a.m11*a.m23)+a.m01*(a.m10*a.m23-a.m13*a.m20))/d,
				(a.m10*(a.m22*a.m31-a.m21*a.m32)+a.m11*(a.m20*a.m32-a.m22*a.m30)+a.m12*(a.m21*a.m30-a.m20*a.m31))/d,
				(a.m20*(a.m02*a.m31-a.m01*a.m32)+a.m21*(a.m00*a.m32-a.m02*a.m30)+a.m22*(a.m01*a.m30-a.m00*a.m31))/d,
				(a.m30*(a.m02*a.m11-a.m01*a.m12)+a.m31*(a.m00*a.m12-a.m02*a.m10)+a.m32*(a.m01*a.m10-a.m00*a.m11))/d,
				(a.m00*(a.m11*a.m22-a.m12*a.m21)+a.m01*(a.m12*a.m20-a.m10*a.m22)+a.m02*(a.m10*a.m21-a.m11*a.m20))/d);
		}
		#endregion

		#region Arithmetic Functions
		//////////////////////////////////////////////////////////////////////
		// Arithmetic Functions
		//////////////////////////////////////////////////////////////////////
		public Matrix4d Transpose()
		{
			return ~this;
		}

		public Matrix4d Invert()
		{
			return !this;
		}

		public double Det
		{
			get
			{
				return (m00*m11-m01*m10)*(m22*m33-m23*m32)
						-(m00*m12-m02*m10)*(m21*m33-m23*m31)
						+(m00*m13-m03*m10)*(m21*m32-m22*m31)
						+(m01*m12-m02*m11)*(m20*m33-m23*m30)
						-(m01*m13-m03*m11)*(m20*m32-m22*m30)
						+(m02*m13-m03*m12)*(m20*m31-m21*m30);
			}
		}

		public double Determinate { get { return Det; } }
		#endregion

		#region OpenGL
		public Matrix4d SetFrustumMatrix(double left, double right, double bottom, double top, double near)
		{
			m00=2*near/(right-left); m01=0; m02=(right+left)/(right-left); m03=0;
			m10=0; m11=2*near/(top-bottom); m12=(top+bottom)/(top-bottom); m13=0;
			m20=0; m21=0; m22=-1; m23=-2*near;
			m30=0; m31=0; m32=-1; m33=0;
			return this;
		}

		public Matrix4d SetFrustumMatrixSymmetric(double right, double top, double near)
		{
			m00=near/right; m01=0; m02=0; m03=0;
			m10=0; m11=near/top; m12=0; m13=0;
			m20=0; m21=0; m22=-1; m23=-2*near;
			m30=0; m31=0; m32=-1; m33=0;
			return this;
		}

		public Matrix4d SetFrustumMatrixFOV(double fov, double aspect, double near)
		{
			double right=near*Math.Tan(fov/2);
			SetFrustumMatrixSymmetric(right, right/aspect, near);
			return this;
		}

		public Matrix4d SetFrustumMatrix(double left, double right, double bottom, double top, double near, double far)
		{
			m00=2*near/(right-left); m01=0; m02=(right+left)/(right-left); m03=0;
			m10=0; m11=2*near/(top-bottom); m12=(top+bottom)/(top-bottom); m13=0;
			m20=0; m21=0; m22=-(far+near)/(far-near); m23=-2*far*near/(far-near);
			m30=0; m31=0; m32=-1; m33=0;
			return this;
		}

		public Matrix4d SetFrustumMatrixSymmetric(double right, double top, double near, double far)
		{
			m00=near/right; m01=0; m02=0; m03=0;
			m10=0; m11=near/top; m12=0; m13=0;
			m20=0; m21=0; m22=-(far+near)/(far-near); m23=-2*far*near/(far-near);
			m30=0; m31=0; m32=-1; m33=0;
			return this;
		}

		public Matrix4d SetFrustumMatrixFOV(double fov, double aspect, double near, double far)
		{
			double right=near*Math.Tan(fov/2);
			SetFrustumMatrixSymmetric(right, right/aspect, near, far);
			return this;
		}

		public Matrix4d SetOrthoMatrix(double left, double right, double bottom, double top, double near, double far)
		{
			m00=2/(right-left); m01=0; m02=0; m03=-(right+left)/(right-left);
			m10=0; m11=2/(top-bottom); m12=0; m13=-(top+bottom)/(top-bottom);
			m20=0; m21=0; m22=-2/(far-near); m23=-(far+near)/(far-near);
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetOrthoMatrixSymmetric(double right, double top, double near, double far)
		{
			m00=1/right; m01=0; m02=0; m03=0;
			m10=0; m11=1/top; m12=0; m13=0;
			m20=0; m21=0; m22=-2/(far-near); m23=-(far+near)/(far-near);
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public static Matrix4d FrustumMatrix(double left, double right, double bottom, double top, double near)
		{
			return new Matrix4d().SetFrustumMatrix(left, right, bottom, top, near);
		}

		public static Matrix4d FrustumMatrixSymmetric(double right, double top, double near)
		{
			return new Matrix4d().SetFrustumMatrixSymmetric(right, top, near);
		}

		public static Matrix4d FrustumMatrixFOV(double fov, double aspect, double near)
		{
			return new Matrix4d().SetFrustumMatrixFOV(fov, aspect, near);
		}

		public static Matrix4d FrustumMatrix(double left, double right, double bottom, double top, double near, double far)
		{
			return new Matrix4d().SetFrustumMatrix(left, right, bottom, top, near, far);
		}

		public static Matrix4d FrustumMatrixSymmetric(double right, double top, double near, double far)
		{
			return new Matrix4d().SetFrustumMatrixSymmetric(right, top, near, far);
		}

		public static Matrix4d FrustumMatrixFOV(double fov, double aspect, double near, double far)
		{
			return new Matrix4d().SetFrustumMatrixFOV(fov, aspect, near, far);
		}

		public static Matrix4d OrthoMatrix(double left, double right, double bottom, double top, double near, double far)
		{
			return new Matrix4d().SetOrthoMatrix(left, right, bottom, top, near, far);
		}

		public static Matrix4d OrthoMatrixSymmetric(double right, double top, double near, double far)
		{
			return new Matrix4d().SetOrthoMatrixSymmetric(right, top, near, far);
		}

		public Matrix4d SetColumns(Tuple3fs column0, Tuple3fs column1, Tuple3fs column2)
		{
			m00=column0.x; m01=column1.x; m02=column2.x; m03=0;
			m10=column0.y; m11=column1.y; m12=column2.y; m13=0;
			m20=column0.z; m21=column1.z; m22=column2.z; m23=0;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetColumns(Tuple3fs column0, Tuple3fs column1, Tuple3fs column2, Tuple3fs column3)
		{
			m00=column0.x; m01=column1.x; m02=column2.x; m03=column3.x;
			m10=column0.y; m11=column1.y; m12=column2.y; m13=column3.y;
			m20=column0.z; m21=column1.z; m22=column2.z; m23=column3.z;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetRows(Tuple3fs row0, Tuple3fs row1, Tuple3fs row2)
		{
			m00=row0.x; m01=row0.y; m02=row0.z; m03=0;
			m10=row1.x; m11=row1.y; m12=row1.z; m13=0;
			m20=row2.x; m21=row2.y; m22=row2.z; m23=0;
			m30=0; m31=0; m32=0; m33=1;
			return this;
		}

		public Matrix4d SetRows(Tuple3fs row0, Tuple3fs row1, Tuple3fs row2, Tuple3fs row3)
		{
			m00=row0.x; m01=row0.y; m02=row0.z; m03=0;
			m10=row1.x; m11=row1.y; m12=row1.z; m13=0;
			m20=row2.x; m21=row2.y; m22=row2.z; m23=0;
			m30=row3.x; m31=row3.y; m32=row3.z; m33=1;
			return this;
		}
		#endregion
	}
}
