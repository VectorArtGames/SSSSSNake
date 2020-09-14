using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Extensions
{
	public static class ArrayExtension
	{
		public static void Shift<T>(this T[] array, int by, T value)
		{
			var arr = array.Skip(by).ToArray();
			arr.CopyTo(array, 0);
		}
	}
}
