using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Point
{
	public class Point : IPoint
	{
		public event EventHandler OnCollected;

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

		private bool collected;
		public bool Collected
		{
			get => collected;
			set
			{
				collected = value;
				if (value)
					OnCollected?.Invoke(this, EventArgs.Empty);
			}
		}

		public Point(GameObject @object)
		{
			AttachedObject = @object;
		}


		public bool CanCollect(Vector2 point, Vector2 size)
		{
			if (rect == null) return false;

			if (!(rect.rect is Rect r)) return false;
			var self = new Rect(rect.localPosition, size);

			var status = self.Overlaps(new Rect(point, size));
			if (status)
			{
				Collected = status;
			}
			return status;
		}
	}
}
