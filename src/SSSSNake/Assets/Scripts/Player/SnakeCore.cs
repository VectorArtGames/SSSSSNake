using Assets.Scripts.Player.Tails;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SnakeCore : MonoBehaviour
{
	public Transform Border_L;
	public Rect debug_rect;

	public GameObject[,] Grid;
	public int Vertical, Horizontal;
	public int Rows, Columns;

	private void Awake()
	{
		var rect = Border_L.GetComponent<RectTransform>();
		//rect.rect.position = rect.anchoredPosition;
	}

	public void Start()
	{
		var b = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

		Debug.Log(b);

		Vertical = (int)Camera.main.orthographicSize;
		Horizontal = Vertical * (Screen.width / Screen.height);
		Columns = Horizontal * 2;
		Rows = Vertical * 2;
		Grid = new GameObject[Columns, Rows];
		for (var i = 0; i < Columns; i++)
		{
			for (var j = 0; j < Rows; j++)
			{
				SpawnTile(i, j, ref Grid[i, j]);
			}
		}
	}

	private void SpawnTile(int x, int y, ref GameObject value)
	{
		var obj = new GameObject($"Tile_{x}_{y}");
		obj.AddComponent<Tail>();
		obj.transform.position = new Vector2(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
		value = obj;
	}
}