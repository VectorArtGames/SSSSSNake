using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
	public static class ListExtension
	{
		public static T Random<T>(this IEnumerable<T> arr)
		{
			var enumerable = arr as T[] ?? arr.ToArray();
			var num = UnityEngine.Random.Range(0, enumerable.Length);
			return enumerable.Length > num ? enumerable.ElementAt(num) : default;
		}
	}
}
