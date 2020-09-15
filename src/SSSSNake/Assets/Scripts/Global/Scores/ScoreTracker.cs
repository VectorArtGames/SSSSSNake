using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTracker : MonoBehaviour
{
	#region Singleton

	public static ScoreTracker Instance { get; set; }

	public void Awake()
	{
		Instance = this;
	}

	#endregion


	[Header("On Score Updated")]
	public UnityEvent eventHandle;

	[Header("Text"),Space(5)]
	public TextMeshProUGUI[] texts;

	[SerializeField]
	private long score;
	public long Score
	{
		get => score;
		set
		{
			score = value;
			eventHandle.Invoke();
		}
	}

	private void FixedUpdate()
	{
		Score++;
	}

	public void Restart()
	{
		Score = 0;
	}

	private void Start()
	{
		Score = 9999;
	}

	public void UpdateText()
	{
		foreach (var obj in texts)
		{
			obj.text = Score.ToString("00000000");
		}
	}
}
