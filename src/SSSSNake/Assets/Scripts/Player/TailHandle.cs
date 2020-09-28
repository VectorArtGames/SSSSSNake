using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.Tails;
using UnityEngine;

public class TailHandle : MonoBehaviour
{

	public GameObject TailObject;
	public Transform TailContainer;

    [SerializeField]
    private int _length;
    public int Length
	{
        get => _length;
        set
        {
			if (value < 0) return;
			var old = _length;
	        _length = value;
			DestroyLeftover(old, value);
		}
	}

    public Tail[] Tails;
    public List<Vector2> Positions = new List<Vector2>();

	private void DestroyLeftover(int old, int current)
	{
		var remainder = old - current;
		if (remainder <= 0)
		{
			Array.Resize(ref Tails, current);
			TailUpdate();
			return;
		}

		for (var i = 0; i < remainder; i++)
		{
			if (!(Tails[Tails.Length - (1 + i)] is Tail t && t.TailObject is GameObject obj))
			{
				Array.Resize(ref Tails, current);
				TailUpdate();
				return;
			}

			Destroy(obj);
		}

		Array.Resize(ref Tails, current);
		TailUpdate();
	}
	/* TODO Optimization - Move only the last object to the first position, Increment by X (steps)
	 */
	public void PositionUpdate(Vector3 p, int steps)
    {
	    if (Length <= 0) return;

		Positions.Insert(0, p);

	    if (Positions.Count > Length + 1)
		{
			while (Positions.Count > Length + 1)
			{
				Positions.RemoveAt(Positions.Count - 1);
			}
		}

		TailUpdate();
    }

	public void TailUpdate()
	{
		if (TailContainer == null) return;

		for (var i = 0; i < Length; i++)
		{
			var index = i;
			if (Tails[index] == null)
			{
				var obj = Instantiate(TailObject, TailContainer, false);
				obj.transform.localPosition = Vector2.zero;
				obj.SetActive(false);
				Tails[index] = new Tail(this, obj, index);
			}
			else
			{
				Tails[index].UpdatePosition();
			}
		}
	}
}
