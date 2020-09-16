using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Global.Grid.Grid_Tile;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIGrid : MonoBehaviour
{
	public Tile[,] Grid;
	public RectTransform Transform;

	public const int BorderSize = 20;

	public int columns;
	public int rows;
	public int w = 40;

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
	    var width = rect.width - BorderSize * 2;
	    var height = rect.height - BorderSize * 2;

	    columns = Mathf.FloorToInt(width / w);
	    rows = Mathf.FloorToInt(height / w);

        var tiles = new Tile[columns, rows];
        //SpawnTile(0,0, out tiles[0, 0]);

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
        var obj = new GameObject($"Tail_{i}_{j}");
        obj.transform.SetParent(transform);
        var r = Transform.rect;

        var rect = obj.AddComponent<RectTransform>();
        var sprite = obj.AddComponent<RawImage>();

        var x = w;
        var y = w;

        Debug.Log($"X: {x}, Y: {y}");

        var pos = new Vector2(x, y);

        rect.anchorMax = Vector2.zero;
        rect.anchorMin = Vector2.zero;
        rect.pivot = Vector2.zero;

        // Set Scale
        rect.localScale = Vector3.one;
        rect.sizeDelta = new Vector3(w, w);

        obj.transform.localPosition = pos;

        var t = new Tile(x, y)
        {
            AttachedObject = obj,
            Position = pos,
        };

        value = t;
    }
}
