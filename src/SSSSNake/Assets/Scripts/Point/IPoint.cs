using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Point
{
	public interface IPoint
	{
		GameObject AttachedObject { get; set; }
		bool Collected { get; set; }
		bool CanCollect(Vector2 point, Vector2 size);
	}
}
