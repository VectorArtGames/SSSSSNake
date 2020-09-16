using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Global.Grid.Grid_Tile;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RectTransform))]
public class UIGrid : MonoBehaviour
{
	public Tile[,] Grid;
	public RectTransform Transform;

	public const int BorderSize = 20;

	public int columns;
	public int rows;
	public int w = 40;
    public float BufferX;
    public float BufferY;


    #region Singleton

    public static UIGrid Instance { get; set; }

	private void Awake()
	{
		Instance = this;
		TryGetComponent(out Transform);

	}

    #endregion

    public void Start()
    {
	    if (Transform == null) return;

	    RegenerateGrid();
    }

    public void RegenerateGrid()
    {
	    var rect = Transform.rect;
        var width = rect.width;
        var height = rect.height;

        var nC = width / w;
        var nR = height / w;

        columns = Mathf.FloorToInt(nC);
	    rows = Mathf.FloorToInt(nR);

        BufferX = width - columns * w;
        BufferY = height - rows * w;

        Debug.Log($"Buffer X: {BufferX}, Buffer Y: {BufferY}");


        var tiles = new Tile[columns, rows];

		//SpawnTile(0, 0, out tiles[0, 0]);
  //      SpawnTile(columns, rows, out tiles[columns - 1, rows - 1]);

  //      SpawnTile(0, rows, out tiles[0, rows - 1]);
  //      SpawnTile(columns, 0, out tiles[columns - 1, 0]);

		for (var j = 0; j < rows; j++)
		{
			for (var i = 0; i < columns; i++)
			{
				SpawnTile(i, j, out tiles[i, j]);
			}
		}

		Grid = tiles;
        Debug.Log("Done!");
    }

    private void SpawnTile(int i, int j, out Tile value)
    {
        var obj = new GameObject($"Tile_{i}_{j}");
        obj.transform.SetParent(transform);
        var r = Transform.rect;

        var rect = obj.AddComponent<RectTransform>();
        var sprite = obj.AddComponent<RawImage>();

        var rnd = Mathf.Sin(i) % 255;
        sprite.color = new Color(rnd, rnd, rnd);

        var x = i * w + BufferX / 2;
        var y = j * w + BufferY / 2;

        //Debug.Log($"X: {x}, Y: {y}");

        var pos = new Vector2(x, y);


        // Set Scale
        rect.localScale = Vector3.one;
        rect.sizeDelta = new Vector3(w, w);

        obj.transform.localPosition = pos;

  //      var anchor = Vector2.zero;
		//rect.anchorMax = anchor;
		//rect.anchorMin = anchor;
		//rect.pivot = anchor;

		var t = new Tile(i, j, pos)
        {
            AttachedObject = obj,
        };

        value = t;
    }

    public void Update()
	{
  //      if (Input.GetKey(KeyCode.W))
		//{
  //          Grid[0, 0].
  //      }
	}
}
