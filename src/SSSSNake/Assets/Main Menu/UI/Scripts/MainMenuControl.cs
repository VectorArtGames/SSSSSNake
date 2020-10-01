using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MainMenuSoundType;

public class MainMenuControl : MonoBehaviour
{
	public AudioSource source;

	public List<GameObject> ObjectsToDisable = 
		new List<GameObject>();

	public List<Dialogue> Dialogues = 
		new List<Dialogue>();

	public List<MainMenuSoundClip> SoundClips = 
		new List<MainMenuSoundClip>();



	public void Exit()
    {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit(1);
		#endif
    }

    public void StartGame()
	{
		SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
	}

    public void ToggleDialogSettings()
    {
	    var dia =Dialogues
		    .FirstOrDefault(x => x.dialogtype == DialogeTypes.Settings && x.dialogueObject != null);
	    var status = dia?.ToggleDialog(ObjectsToDisable.ToArray());
	    if (!(status is bool value)) return;
		PlaySound(value ? Open : Close);
    }

    public void DeleteSettings()
    {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		PlaySound(Delete);
	}

    public void PlaySound(MainMenuSoundType soundType)
    {
	    var clip = SoundClips.Where(x => x.soundType == soundType && x.Sound != null).Random();
	    if (clip == null) return;
		source.PlayOneShot(clip.Sound, VolumeSlider.GetVolume);
    }
}

[Serializable]
public class Dialogue
{
	public DialogeTypes dialogtype;
	public GameObject dialogueObject;



	public bool? ToggleDialog(GameObject[] objectsToDisable)
	{
		if (dialogueObject == null) return null;

		var active = !dialogueObject.activeSelf;
		#if UNITY_EDITOR
			Debug.Log($"Dialog Active: {(active ? "Yes" : "No")}");
		#endif
		dialogueObject.SetActive(active);

		foreach (var obj in objectsToDisable)
		{
			obj.SetActive(!active);
		}

		return active;
	}
}

public enum DialogeTypes
{
	Settings,
}

public enum MainMenuSoundType
{
	Close,
	Open,
	Delete
}

[Serializable]
public class MainMenuSoundClip
{
	public MainMenuSoundType soundType;
	public AudioClip Sound;
}
