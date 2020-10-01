using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Main_Menu.UI.Scripts.Settings
{
	[RequireComponent(typeof(Slider))]
	public class SpeedSlider : MonoBehaviour
	{
		public Slider Speed;
		public TextMeshProUGUI valuePreview;

		private void Awake()
		{
			TryGetComponent(out Speed);

			Speed.onValueChanged.AddListener(SpeedChanged);
			var speed = Speed.value = GetSpeed;
			UpdateText((int)speed);
		}

		private void OnEnable()
		{
			Speed.value = GetSpeed;
		}

		private void SpeedChanged(float speed)
		{
			UpdateText((int)speed);

			PlayerPrefs.SetInt("Speed", (int)speed);
			PlayerPrefs.Save();
		}

		private void UpdateText(int speed)
		{
			valuePreview.text = $"{GetDifficulty(speed)}\nSpeed: {speed}";
		}

		public static float GetSpeed =>
			PlayerPrefs.GetInt("Speed", 20);

		public string GetDifficulty(int value)
		{
			var str = "<Unknown>";

			if (value >= 0 && value < 10)
				str = "<b>HAHAHA, TOO EASY!</b>";
			else if (value >= 10 && value < 20)
				str = "You're getting there..";
			else if (value >= 20 && value < 30)
				str = $"Ok, You're getting faster. {(value == 20 ? "(default)" : "")}";
			else if (value >= 30 && value < 40)
				str = "Ok, you got my attention.";
			else if (value >= 40 && value < 50)
				str = "Are you the new Usain bolt?";
			else if (value >= 50 && value <= 60)
				str = "<i>QUEUE SPEEDY BOI MUSIC</i>";

			return str;
		}
	}

}
