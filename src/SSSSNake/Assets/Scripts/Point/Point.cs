using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Point
{
	public class Point : IPoint
	{
		private RectTransform rect;

		private GameObject attachedObject;
		public GameObject AttachedObject
		{
			get => attachedObject;
			set
			{
				attachedObject = value;
				value.TryGetComponent(out rect);
			}
		}

		public Point(GameObject @object)
		{
			AttachedObject = @object;
		}


		public bool CanCollect(Vector2 point)
		{
			if (rect == null) return false;

			Debug.Log("hi");
			return true;
		}
	}
}
