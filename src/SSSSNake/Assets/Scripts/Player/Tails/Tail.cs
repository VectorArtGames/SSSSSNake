﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Tails
{
	public class Tail
	{
		public GameObject TailObject;

		public Tail(GameObject tailObject) =>
			(TailObject) = (tailObject);


		public void UpdatePosition(Vector3 pos)
		{
			TailObject.transform.localPosition = pos;
			TailObject.SetActive(true);
		}
	}
}
