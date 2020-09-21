using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.Tails;
using UnityEngine;

public class TailHandle : MonoBehaviour
{
	public GameObject TailObject;

    private int _length;
    public int Length
	{
        get => _length;
        set
        {
            OnLengthChanged(_length, value);
	        _length = value;
            Array.Resize(ref Tails, value);
        }
	}

    public Tail[] Tails;
    public List<Vector2> Positions = new List<Vector2>();

    private void OnLengthChanged(int old, int current)
    {
        SpawnTail(current);
    }

    public void PositionUpdate(Vector3 p)
    {
	    if (Positions.Count >= Mathf.Max(10, Length))
	    {
		    Positions.Insert(0, p);
		    Positions.RemoveAt(Positions.Count - 1);
	    }
	    else
		    Positions.Add(p);
    }

    private void FixedUpdate()
    {
	    for (var i = 0; i < Tails.Length; i++)
	    {
		    var t = Tails[i];
		    if (t == null) continue;
		    var p = Positions[i];
            t.UpdatePosition(p);
	    }
    }

    private void SpawnTail(int index)
    {
	    var obj = Instantiate(TailObject, Positions[index], Quaternion.identity);
	    if (Tails.Length > index) return;

	    Tails[index] = new Tail(obj);
    }
}
