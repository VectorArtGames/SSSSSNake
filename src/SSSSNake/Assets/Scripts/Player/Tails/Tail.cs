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
		public int Index;

		public Tail(TailHandle handle, GameObject tailObject, int index) =>
			(Handle, TailObject, Index) = (handle, tailObject, index);


		public void UpdatePosition(Vector3 pos)
		{
			TailObject.transform.localPosition = pos;
			TailObject.SetActive(true);
		}
	}
}
