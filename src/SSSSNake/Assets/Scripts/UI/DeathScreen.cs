using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
	#region Singleton

	public static DeathScreen Instance { get; set; }
	public void Awake()
	{
		Instance = this;
	}

	#endregion

	[Header("Required"), Space(5.0f)]
	public GameObject GameOverScreen;
	public GameObject GameScreen;
	public TextMeshProUGUI ScoreText;

	private SnakeCore PlayerCore;
	private ScoreTracker scoreTracker;

	public DeathScreenState State = DeathScreenState.None;

	private void Start()
	{
		scoreTracker = ScoreTracker.Instance;
		PlayerCore = SnakeCore.Instance;
	}

	public void OnDeath()
	{
		GameScreen.SetActive(false);
		GameOverScreen.SetActive(true);
		Time.timeScale = 0;
		if (ScoreText != null && scoreTracker != null)
			ScoreText.text = $"Game Over.\nScore:{scoreTracker.Score:00000000}\nHighscore: 00000102";

		State = DeathScreenState.Dead;
	}

	private void Update()
	{
		if (State != DeathScreenState.Dead) return;
		if (Input.GetKeyUp(KeyCode.KeypadEnter) 
			|| Input.GetKeyUp(KeyCode.Space) 
			|| Input.GetKeyUp(KeyCode.Return)
			|| Input.GetKeyUp(KeyCode.R)) Restart();
		else if (Input.GetKeyUp(KeyCode.Escape)) Quit();
	}

	public void Restart()
	{
		if (PlayerCore == null) return;
		PlayerCore.Restart();
		GameOverScreen.SetActive(false);
		GameScreen.SetActive(true);
		Time.timeScale = 1;
		State = DeathScreenState.Game;
	}

	public void Quit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit(1);
		#endif
	}

	public enum DeathScreenState
	{
		None,
		Dead,
		Game
	}
}
