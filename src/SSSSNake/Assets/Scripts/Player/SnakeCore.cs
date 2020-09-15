using Assets.Scripts.Player.Tails;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.Mathematics;

using Random = UnityEngine.Random;

public class SnakeCore : MonoBehaviour
{
	[Header("Debug")] 
	public Sprite spriter;
	public Transform borderL;
	public RectTransform debugRect;


	public GameObject[,] grid;

	[Header("Information"), Space(5)] 
	public int vertical;
	public int horizontal;
	public int rows, columns;

	private void Awake()
	{
		debugRect = borderL.GetComponent<RectTransform>();
		//rect.rect.position = rect.anchoredPosition;
	}

	public void Start()
	{
		if (!(Camera.main is Camera cam && debugRect.rect is Rect r)) return;

		var b = cam.ScreenToWorldPoint(new Vector2(0, 0));


		vertical = (int)Camera.main.orthographicSize;
		horizontal = vertical * (Screen.width / Screen.height);
		columns = horizontal * 2;
		rows = vertical * 2;
		grid = new GameObject[columns, rows];
		for (var i = 0; i < columns; i++)
		{
			for (var j = 0; j < rows; j++)
			{
				SpawnTile(i, j, out grid[i, j]);
			}
		}
	}

	private void SpawnTile(int x, int y, out GameObject value)
	{
		if (!(Camera.main is Camera cam && debugRect.rect is Rect r))
		{
			value = null;
			return;
		}

		var b = cam.ScreenToWorldPoint(new Vector2(0, 0));
		b -= borderL.position;

		var obj = new GameObject($"Tile_{x}_{y}");

		var spr = obj.AddComponent<SpriteRenderer>();
		spr.sprite = spriter;
		spr.color = Color.black;

		obj.AddComponent<Tail>();
		obj.transform.position = new Vector2(x - (horizontal - 0.5f), y - (vertical - 0.5f));
		value = obj;
	}
}