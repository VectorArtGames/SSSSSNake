using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
	public Camera cam;
	public AudioSource source;

	#region Singleton

	public static SFXManager Instance { get; set; }

	private void Awake()
	{
		Instance = this;
		cam.TryGetComponent(out source);
	}

	#endregion

	public SoundClip[] SoundClips;

	public IEnumerable<SoundClip> FindBySoundType(SoundType type)
	{
		return SoundClips.Where(x => x.soundType == type && x.Sound != null);
	}

	public static void PlayByType(SoundType type)
	{
		if (!(Instance is SFXManager man)) return;
		var sounds = man.FindBySoundType(type);
		var sound = sounds?.Random();
		if (sound == null) return;
		if (man.source == null) return;
		man.source.PlayOneShot(sound.Sound);
	}

	public enum SoundType
	{
		Point,
		Death,
		Start
	}
}

[Serializable]
public class SoundClip
{
	public SFXManager.SoundType soundType;
	public AudioClip Sound;
}
