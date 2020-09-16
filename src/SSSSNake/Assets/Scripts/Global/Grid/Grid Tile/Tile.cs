using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Global.Grid.Grid_Tile
{
	public class Tile
	{
		public Vector2 Position { get; set; }
		public GameObject AttachedObject { get; set; }

		public int X { get; }
		public int Y { get; }

		public Tile(int x, int y) =>
			(X, Y) = (x, y);
	}
}
