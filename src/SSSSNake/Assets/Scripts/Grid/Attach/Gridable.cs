using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Grid.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Grid.Attach
{
	public class Gridable : MonoBehaviour, IGridable
	{
		private GameObject _attachedObject;

		public GameObject AttachedObject
		{
			get => _attachedObject;
			set
			{
				_attachedObject = value;
				RepositionObject();
			}
		}

		private Vector2 _position;
		public Vector2 Position
		{
			get => _position;
			set
			{
				_position = value;
				RepositionObject();
			}
		}

		public int X { get; set; }
		public int Y { get; set; }

		public Gridable(int x, int y, GameObject attached, Vector2 position) =>
			(X, Y, AttachedObject, Position) = (x, y, attached, position);

		public Gridable(int x, int y) =>
			(X, Y) = (x, y);

		private void RepositionObject()
		{
			if (AttachedObject == null) return;
			AttachedObject.transform.position = Position;
		}
	}
}
