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
		public static bool CanMove(this Tail[] tails, Vector2 position)
		{
			var b = tails.Any(x => 
				x.x < position.x && position.x > x.x + 50 &&
				x.y > position.y && x.y < position.y - 50
				);
			return b;
		}
	}
}
