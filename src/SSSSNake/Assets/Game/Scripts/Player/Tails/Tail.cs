using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Tails
{
	[Serializable]
	public class Tail
	{
		public TailHandle Handle;
		public GameObject TailObject;
		public RectTransform rect;
		public Vector3 position;

		public int Index;

		public Tail(TailHandle handle, GameObject tailObject, int index)
		{
			(Handle, TailObject, Index) = (handle, tailObject, index);
			tailObject.TryGetComponent(out rect);
		}


		public void UpdatePosition()
		{
			if (TailObject == null || Handle == null) return;
			if (TailObject.transform == null) return;
			if (Handle != null && Handle.Positions.Count <= Index + 1) return;
			if (!(Handle.Positions[Index + 1] is Vector2 p)) return;

			TailObject.transform.localPosition = p;
			position = p;
			TailObject.SetActive(true);
		}
	}
}
