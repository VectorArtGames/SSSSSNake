using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Point;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
	#region Singleton

	public static PointHandler Instance { get; set; }

	private void Awake()
	{
		Instance = this;
	}

	#endregion

	public GameObject PointPrefab;

	public Point point;

	public BorderField border;

	// Start is called before the first frame update
    public void Start()
    {
	    border = BorderField.Instance;
    }

    private void LateUpdate()
    {
	    if (point != null) return;
		var p = new Vector2(0, 0);
		var obj = Instantiate(PointPrefab, transform, false);
		obj.transform.localPosition = p;
		point = new Point(obj);
    }
}
