using System.Collections.Generic;

namespace OpenGL.Helper
{
	public static class FloatListExtension
	{
		public static void Add(this List<float> list, Tuple2fs tuple2)
		{
			list.Add(tuple2.x);
			list.Add(tuple2.y);
		}

		public static void Add(this List<float> list, Tuple3fs tuple3)
		{
			list.Add(tuple3.x);
			list.Add(tuple3.y);
			list.Add(tuple3.z);
		}
	}
}
