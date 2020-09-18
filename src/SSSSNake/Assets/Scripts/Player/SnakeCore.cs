using Assets.Scripts.Player.Tails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SnakeCore.MoveDirection;

public class SnakeCore : MonoBehaviour
{
	public int Length;

	public Tail[] Tails;
	public List<Vector2> Positions = new List<Vector2>();

	public float FPS;

	public float w;

	public MoveDirection Direction = Up;

    // Start is called before the first frame update
    private void Start()
    {
		InvokeRepeating(nameof(MovePlayer), 0, 60 / 60 / FPS);
    }

    // Update is called once per frame
    private void Update()
    {
		if (Input.GetKeyUp(KeyCode.W))
			Direction = Up;
		else if (Input.GetKeyUp(KeyCode.S))
			Direction = Down;
		else if (Input.GetKeyUp(KeyCode.A))
			Direction = Left;
		else if (Input.GetKeyUp(KeyCode.D))
			Direction = Right;

    }

	public void MovePlayer()
	{
		Move(Direction, 1);
	}

    public void Move(MoveDirection direction, int steps)
	{
        Vector2 pos = Vector2.zero;

		switch (direction)
		{
			case Left:
				pos = Vector2.left;
				break;
			case Right:
				pos = Vector2.right;
				break;
			case Up:
				pos = Vector2.up;
				break;
			case Down:
				pos = Vector2.down;
				break;
		}

		var p = transform.localPosition += (Vector3)(pos * (w * steps));
		if (Positions.Count >= 10)
		{
			Positions.Insert(0, p);
			Positions.RemoveAt(Positions.Count - 1);
		}
		else
			Positions.Add(p);

		OnSnakeMove();
	}

    public void OnSnakeMove()
    {
	    if (!(BorderField.Instance is BorderField instance)) return;
	    var border = instance.Border;

		// Left
		if (border.x - w > transform.localPosition.x)
		{
			transform.localPosition = 
				new Vector2((int)border.width, (int)transform.localPosition.y);
		}

		// Right
		if (border.width + w < transform.localPosition.x)
		{
			transform.localPosition =
				new Vector2((int)border.x, (int)transform.localPosition.y);
		}

		// Top
		if (border.y + w < transform.localPosition.y)
		{
			transform.localPosition =
				new Vector2(transform.localPosition.x, -border.y);
		}

		// Bottom
		if (-border.height - w > transform.localPosition.y)
		{
			transform.localPosition =
				new Vector2(transform.localPosition.x, border.y);
		}
	}

	public enum MoveDirection
	{
        Left,
        Right,
        Up,
        Down
	}
}
