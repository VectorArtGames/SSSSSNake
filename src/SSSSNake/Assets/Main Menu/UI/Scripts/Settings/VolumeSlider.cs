using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
	public Slider Volume;
	public TextMeshProUGUI valuePreview;

	private void Awake()
	{
		TryGetComponent(out Volume);

		Volume.onValueChanged.AddListener(VolumeChanged);
		Volume.value = GetVolume;
	}

	private void OnEnable()
	{
		Volume.value = GetVolume;
	}

	private void VolumeChanged(float volume)
	{
		valuePreview.text = $"Volume: {(volume * 100):0}%";

		PlayerPrefs.SetFloat("Volume", volume);
		PlayerPrefs.Save();
	}

	public static float GetVolume =>
		PlayerPrefs.GetFloat("Volume", 1.0f);
}
