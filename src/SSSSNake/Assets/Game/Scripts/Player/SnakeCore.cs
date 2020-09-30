﻿using System;
using Assets.Scripts.Player.Tails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SnakeCore.MoveDirection;
using Assets.Scripts.Player.Extension;
using Assets.Scripts.Point;

[RequireComponent(typeof(TailHandle))]
public class SnakeCore : MonoBehaviour
{
	public TailHandle tail;
	public ScoreTracker score;
	public DeathScreen deathScreen;

	public float FPS;

	public float w;

	public MoveDirection Direction = Up;

	#region Singleton

	public static SnakeCore Instance { get; set; }

	private void Awake()
	{
		Instance = this;
		TryGetComponent(out tail);
	}

	#endregion

	// Start is called before the first frame update
	private void Start()
	{
		score = ScoreTracker.Instance;
		deathScreen = DeathScreen.Instance;

		InvokeRepeating(nameof(MovePlayer), 0, (float)60 / 60 / FPS);
		SFXManager.PlayByType(SFXManager.SoundType.Start);
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
		if (!(transform is Transform t)) return;

		var dir = GetDirection(direction);
		var p = t.localPosition + (Vector3)(dir * (w * steps));

		if (!(tail is TailHandle handle)) return;
		if (!tail.Tails.CanMove(p))
		{
			#if UNITY_EDITOR
				Debug.Log("No");
			#endif
			deathScreen.OnDeath();
			SFXManager.PlayByType(SFXManager.SoundType.Death);
			return;
		}

		t.localPosition = p;
		handle.PositionUpdate(p, steps);
		OnSnakeMove();
		if (!(PointHandler.Instance is PointHandler pHandle && pHandle.point is Point point && score != null)) return;
		point.CanCollect(p, new Vector2(w, w));
	}

	private Vector2 GetDirection(MoveDirection direction)
	{
		switch (direction)
		{
			case Left:
				return Vector2.left;
			case Right:
				return Vector2.right;
			case Up:
				return Vector2.up;
			case Down:
				return Vector2.down;
		}

		return Vector2.zero;
	}

	public void OnSnakeMove()
	{
		if (!(BorderField.Instance is BorderField instance && transform is Transform t)) return;
		var border = instance.Border;

		// Left
		if (border.x - w > t.localPosition.x)
		{
			t.localPosition = 
				new Vector2((int)border.width + w, (int)t.localPosition.y);
		}

		// Right
		if (border.width + w < t.localPosition.x)
		{
			t.localPosition =
				new Vector2((int)border.x - w, (int)t.localPosition.y);
		}

		// Top
		if (border.y + w < t.localPosition.y)
		{
			t.localPosition =
				new Vector2((int)t.localPosition.x, (int)-border.y - w);
		}

		// Bottom
		if (-border.height - w > t.localPosition.y)
		{
			t.localPosition =
				new Vector2((int)t.localPosition.x, (int)border.y + w);
		}
	}

	public void Restart()
	{
		if (!(tail is TailHandle handle)) return;
		handle.Positions = new List<Vector2>();
		transform.localPosition = Vector2.zero;
		score.Score = handle.Length = 0;
		SFXManager.PlayByType(SFXManager.SoundType.Start);
	}

	public enum MoveDirection
	{
		Left,
		Right,
		Up,
		Down
	}
}