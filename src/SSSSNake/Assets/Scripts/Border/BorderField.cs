using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderField : MonoBehaviour
{
	public Rect Border;

    [Header("Debug - Borders"), Space(5)]
	[SerializeField]
    private RectTransform BorderLeft;
	[SerializeField]
    private RectTransform BorderRight;
	[SerializeField]
    private RectTransform BorderTop;
    [SerializeField]
	private RectTransform BorderBottom;

    #region Singleton

    public static BorderField Instance { get; set; }

    public void Awake()
    {
	    Instance = this;
	    var left = BorderLeft;
	    var top = BorderTop;

        var x = left.localPosition.x;
	    var y = top.localPosition.y;

	    var width = (BorderRight.localPosition - BorderLeft.localPosition).x;
	    var height = (BorderTop.localPosition - BorderBottom.localPosition).y;
        Debug.Log($"{width} x {height}");
        var rect = new Rect(x, y, width, height);

        Border = rect;
    }

    #endregion
}
