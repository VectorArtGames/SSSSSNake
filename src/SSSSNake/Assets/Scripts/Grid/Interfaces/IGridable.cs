using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Grid.Interfaces
{
	public interface IGridable
	{
		int X { get; set; }
		int Y { get; set; }
		GameObject AttachedObject { get; set; }
		Vector2 Position { get; set; }
	}
}
