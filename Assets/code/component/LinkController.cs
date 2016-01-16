﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class LinkController : MonoBehaviour {

	public Image screenOverlay;

	[System.Serializable]
	public class AgeData {
		public string name;
		public Camera linkPanelCamera;
		public GameObject linkLocation;
	}

	public AgeData[] ages;

	// Use this for initialization
	void Awake () {
		Services.instance.Set<LinkController>(this);
	}

	void Start() {
//		pushAgeSettings(ages[0]);
	}
	
	public void LinkTo(string ageName) {
		Debug.LogFormat("LinkController: Link to {0}", ageName);
		StartCoroutine(linkRoutine(ageName));
	}

	IEnumerator linkRoutine(string ageName) {
		Services.instance.Get<SoundManager>().playOneShot(SoundManager.Sound.Link);

		const float fadeDurationS = 0.5f;
		float routineElapsedTime = 0;
		float fadeStartTime = 0;
		float fadeEndTime = fadeDurationS;
		float lerpPct = 0;

		while (routineElapsedTime < fadeEndTime)
		{
			lerpPct =  routineElapsedTime / fadeEndTime;
//			Debug.LogFormat("0 routineElapsedTime={0} fadeEndTime={1} lerpPct={2}", routineElapsedTime, fadeEndTime, lerpPct);
			Color overlayColor = new Color(0, 0, 0, Mathf.Lerp(0, 1, lerpPct));
			screenOverlay.color = overlayColor;
			yield return null;
			routineElapsedTime += Time.deltaTime;
		}

		AgeData age = getAge(ageName);
		pushAgeSettings(age);
		yield return new WaitForSeconds(0.5f);

		fadeStartTime = routineElapsedTime;
		fadeEndTime = routineElapsedTime + fadeDurationS;
		while (routineElapsedTime < fadeEndTime)
		{
			lerpPct =  (routineElapsedTime - fadeStartTime) / (fadeEndTime - fadeStartTime);
//			Debug.LogFormat("1 routineElapsedTime={0} fadeStartTime={1} fadeEndTime={2} lerpPct={3}", routineElapsedTime, fadeStartTime, fadeEndTime, lerpPct);
			Color overlayColor = new Color(0, 0, 0, Mathf.Lerp(1, 0, lerpPct));
			screenOverlay.color = overlayColor;
			yield return null;
			routineElapsedTime += Time.deltaTime;
		}
	}

	AgeData getAge(string ageName) {
		return System.Array.Find(ages, (AgeData obj) => {
			return obj.name.Equals(ageName);
		});
	}

	void pushAgeSettings(AgeData age) {
		//Make sure FPSController is enabled as it's off in start age
		var fpc = Services.instance.Get<FirstPersonController>();
		fpc.GetComponent<CharacterController>().enabled = true;
		fpc.enabled = true;

		//Update FPSController settings
		fpc.transform.position = age.linkLocation.transform.position;
		fpc.transform.rotation = age.linkLocation.transform.rotation;

		//Update camera settings
		Camera.main.cullingMask = age.linkPanelCamera.cullingMask;

		Skybox ageSkybox = age.linkPanelCamera.GetComponent<Skybox>();
		if (ageSkybox != null) {
			Camera.main.GetComponent<Skybox>().material = ageSkybox.material;
		}
	}
}
