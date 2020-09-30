using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Player.Tails;
using UnityEngine;

namespace Assets.Scripts.Player.Extension
{
	public static class PositionExtension
	{
		private static Vector2 size = new Vector2(50, 50);
		public static bool CanMove(this Tail[] tails, Vector2 position)
		{
			var r = new Rect(position, size);
			var b = tails.Where(x => x.TailObject != null && x.TailObject.activeSelf).Select(x => new Rect(x.position, size)).Any(x => x.Overlaps(r));
			return !b;
		}
	}
}
