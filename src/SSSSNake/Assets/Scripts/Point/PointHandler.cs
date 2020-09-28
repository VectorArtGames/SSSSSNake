using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Point;
using UnityEngine;
using Random = UnityEngine.Random;

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

	public SnakeCore core;
	public ScoreTracker score;

	public Point point;

	public BorderField border;

	// Start is called before the first frame update
    public void Start()
    {
	    border = BorderField.Instance;
		core = SnakeCore.Instance;
		score = ScoreTracker.Instance;
    }

    private void LateUpdate()
    {
	    if (point != null) return;
		NewPoint();
    }

	private void OnCollect(object sender, EventArgs e)
	{
		if (!(sender is Point pnt && core.tail is TailHandle handle && score != null))
		{
			Debug.Log("Something is null!");
			return;
		}

		handle.Length++;
		score.Score++;

		pnt.OnCollected -= OnCollect;
		Destroy(pnt.AttachedObject);
		Debug.Log("Collected!");
		NewPoint();
	}

	private void NewPoint()
	{
		if (point != null && point.AttachedObject == null)
			point = null;

		var p = GetLocation();
		var obj = Instantiate(PointPrefab, transform, false);
		obj.transform.localPosition = p;
		var pnt = new Point(obj);
		pnt.OnCollected += OnCollect;
		point = pnt;
	}

	private Vector2 GetLocation()
	{
		if (!(border.Border is Rect rect)) return Vector2.zero;
		var rX = Random.Range(-1.0f, 1.0f);
		var rY = Random.Range(-1.0f, 1.0f);
		var x = (rect.width - 50) * rX;
		var y = (rect.height - 50) * rY;
		Debug.Log($"X: {x}, Y: {y}");

		var v = new Vector2(x, y);

		var tails = core.tail.Tails.Select(t => t.position);
		var exists = tails.Any(tail => 
			tail.x > v.x && tail.x + 50 < v.x && // X
			tail.y > v.y && tail.y + 50 > v.y
		);

		if (exists)
			Debug.LogError($"Exists!\n{v}");

		var pos = exists ? GetLocation() : v;

		return pos;
	}
}
