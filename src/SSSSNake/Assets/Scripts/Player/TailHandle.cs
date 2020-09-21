using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.Tails;
using UnityEngine;

public class TailHandle : MonoBehaviour
{
	public GameObject TailObject;

    [SerializeField]
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
	    if (Length <= 0) return;

	    if (Positions.Count > Length)
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
	    var pos = Positions.ToArray();
        if (Tails == null) return;

        for (var i = 0; i < Tails.Length - 1; i++)
        {
	        if (!(pos.Length > i + 1 && pos[i + 1] is Vector2 p)) return;

	        if (SpawnTail(i)) continue;

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

		Tails[index] = new Tail(this, obj, index);
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
