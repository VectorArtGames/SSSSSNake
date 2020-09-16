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

		private GameObject _attachedobj;
		public GameObject AttachedObject
		{
			get => _attachedobj;
			set
			{
				_attachedobj = value;
				Reposition(value);
			}
		}

		public int X { get; }
		public int Y { get; }

		public Tile(int x, int y, Vector2 position) =>
			(X, Y, Position) = (x, y, position);

		public void Reposition(GameObject @object)
		{
			@object.TryGetComponent(out RectTransform rect);
			if (rect == null) return;
			@object.transform.localPosition = Position;
			var anchor = Vector2.zero;
			rect.anchorMax = anchor;
			rect.anchorMin = anchor;
			rect.pivot = anchor;
		}
	}
}
