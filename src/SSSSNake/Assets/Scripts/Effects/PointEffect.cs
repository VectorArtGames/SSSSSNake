using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PointEffect : MonoBehaviour
{
	public GameObject CameraObject;
	public PostProcessVolume volume;
	public ChromaticAberration chrome;
	public void Start()
	{
		PointHandler.OnPoint += PointHandler_OnPoint;
		CameraObject.TryGetComponent(out volume);
		volume.profile.TryGetSettings(out chrome);
	}

	private void PointHandler_OnPoint(object sender, EventArgs e)
	{
		#if UNITY_EDITOR
			Debug.Log("Run Effect");
		#endif
	}
}
