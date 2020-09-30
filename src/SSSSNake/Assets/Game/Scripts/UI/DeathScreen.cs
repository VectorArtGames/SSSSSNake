using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		GameScreen.SetActive(false); // disable game
		GameOverScreen.SetActive(true); // enable death screen
		Time.timeScale = 0; // Freeze time
		if (ScoreText == null || scoreTracker == null)
		{
			#if UNITY_EDITOR
				Debug.LogError("Unexpected error occurred!\nDeathScreen");
			#endif
			return;
		}

		/* Highscore - Get highscore.
		 * compare with current score,
		 * if higher than current score..
		 * Set highscore, if lower.. Do nothing.
		 */
		var score = ScoreTracker.GetHighscore();
		if (scoreTracker.Score > score)
			ScoreTracker.SetHighscore(Convert.ToInt32(scoreTracker.Score));

		// Set highscore
		ScoreText.text = $@"     Game Over.
Score    : {scoreTracker.Score:00000000}
Highscore: {ScoreTracker.GetHighscore():00000000}";


		State = DeathScreenState.Dead;

	}

	private void Update()
	{
		if (State != DeathScreenState.Dead) return;
		if (Input.GetKeyUp(KeyCode.KeypadEnter) 
			|| Input.GetKeyUp(KeyCode.Space) 
			|| Input.GetKeyUp(KeyCode.Return)
			|| Input.GetKeyUp(KeyCode.R)) Restart();
		else if (Input.GetKeyUp(KeyCode.Escape)) ExitToMainMenu();
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

	public void ExitToMainMenu()
	{
		SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
		Time.timeScale = 1;
	}

	public enum DeathScreenState
	{
		None,
		Dead,
		Game
	}
}
