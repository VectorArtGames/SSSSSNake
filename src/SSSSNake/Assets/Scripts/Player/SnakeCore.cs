using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCore : MonoBehaviour
{
    [SerializeField]
    private int _length = 1;
    public int Length
	{
        get => _length;
        set => _length = value;
	}

    private Vector2 _position;
    public Vector2 Position
	{
        get => _position;
        set
		{
            Reposition(value);
            _position = value;
		}
	}

	public void Awake()
	{
		Reposition(transform.position);
	}

	public void Reposition(Vector2 value)
	{
        transform.position = new Vector2((int)value.x, (int)value.y);
	}
}
