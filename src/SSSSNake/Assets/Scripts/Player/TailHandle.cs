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
            var old = _length;
	        _length = value;
            Array.Resize(ref Tails, value);
            OnLengthChanged(old, value);
        }
	}

    public Tail[] Tails;
    public List<Vector2> Positions = new List<Vector2>();

    public Queue<Tail> SpawnTails = new Queue<Tail>();

    private void OnLengthChanged(int old, int current)
    {
        
    }

    public void PositionUpdate(Vector3 p)
    {
	    if (Positions.Count > Mathf.Max(10, Length))
	    {
		    Positions.Insert(0, p);
		    Positions.RemoveAt(Positions.Count - 1);
	    }
	    else
		    Positions.Add(p);

        OnPlayerMove();
    }

    public void OnPlayerMove()
	{
        if (Tails == null) return;

        for (var i = 0; i < Tails.Length - 1; i++)
        {
            if (!(Positions.Count > i + 1 && Positions[i + 1] is Vector2 p))
			{
                Debug.Log($"Index:{i + 1}\nCount: {Positions.Count}");
                return;
            }

            if (SpawnTail(i)) return;

            var t = Tails[i];
            t.UpdatePosition(p);
        }
    }

    private bool SpawnTail(int index)
    {
        if (Tails.Length >= index && Tails[index] != null) return false;

        if (Positions.Count < index ) return false;

	    var obj = Instantiate(TailObject, transform.parent, false);
        obj.SetActive(false);

		if (Tails.Length < index || 
            Tails.Length >= index && 
            Tails[index] != null) return false;
		Tails[index] = new Tail(obj);
        return true;
	}

	private void Start()
	{
        Length = 5;
        InvokeRepeating(nameof(Increment), 0, 3);
	}

    private void Increment()
	{
        Length++;
	}
}
