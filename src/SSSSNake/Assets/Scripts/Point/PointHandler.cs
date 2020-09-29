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

	#region Constant Values

	/// <summary>
	/// Max Points for each point
	/// </summary>
	public const int MaxPoints = 255;

	#endregion


	public GameObject PointPrefab;

	public SnakeCore core;
	public ScoreTracker score;

	public Point point;

	public BorderField border;

	/// <summary>
	/// Time since last collect
	/// </summary>
	private DateTime TimeCollect = DateTime.Now;

	// Start is called before the first frame update
    public void Start()
    {
	    border = BorderField.Instance;
		core = SnakeCore.Instance;
		score = ScoreTracker.Instance;

		if (point == null)
			NewPoint();
	}

	private void OnCollect(object sender, EventArgs e)
	{
		if (!(sender is Point pnt && core.tail is TailHandle handle && score != null))
		{
			Debug.Log("Something is null!");
			return;
		}

		var time = DateTime.Now;
		var calculatedScore = Mathf.CeilToInt(MaxPoints / Mathf.Max(1f, (float)(time - TimeCollect).TotalSeconds));
		#if UNITY_EDITOR
			Debug.Log($"Score: {calculatedScore}");
		#endif

		handle.Length++;
		score.Score += calculatedScore;

		pnt.OnCollected -= OnCollect;
		Destroy(pnt.AttachedObject);
		#if UNITY_EDITOR
			Debug.Log("Collected!");
		#endif
		TimeCollect = time;
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

		#if UNITY_EDITOR
			Debug.Log($"X: {x}, Y: {y}");
		#endif

		var v = new Vector2(x, y);

		var tails = core.tail.Tails.Select(t => t.position);
		var exists = tails.Any(tail => 
			tail.x > v.x && tail.x + 50 < v.x && // X
			tail.y > v.y && tail.y + 50 > v.y
		);
		#if UNITY_EDITOR
			if (exists) Debug.LogError($"Exists!\n{v}");
		#endif

		var pos = exists ? GetLocation() : v;

		return pos;
	}
}
