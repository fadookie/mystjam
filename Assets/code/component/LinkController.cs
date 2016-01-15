using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LinkController : MonoBehaviour {

	public GameObject linkPanelCamera;
	public Image screenOverlay;

	// Use this for initialization
	void Awake () {
		Services.instance.Set<LinkController>(this);
	}
	
	public void LinkTo(string linkName) {
		Debug.LogFormat("LinkController: Link to {0}", linkName);
		StartCoroutine(linkRoutine(linkName));
	}

	IEnumerator linkRoutine(string linkName) {
		Services.instance.Get<SoundManager>().playOneShot(SoundManager.Sound.Link);

		float routineStartTime = Time.time;
		const float fadeDurationS = 0.5f;
		float routineElapsedTime = 0;
		float lerpPct = 0;

		while (routineElapsedTime < fadeDurationS)
		{
			lerpPct =  routineElapsedTime / fadeDurationS;
			Color overlayColor = new Color(0, 0, 0, Mathf.Lerp(0, 1, lerpPct));
			screenOverlay.color = overlayColor;
			yield return null;
			routineElapsedTime += Time.deltaTime;
		}

		Camera.main.transform.position = linkPanelCamera.transform.position;
		Camera.main.transform.rotation = linkPanelCamera.transform.rotation;

		while (routineElapsedTime < fadeDurationS * 2)
		{
			lerpPct =  routineElapsedTime / (fadeDurationS * 2);
			Color overlayColor = new Color(0, 0, 0, Mathf.Lerp(1, 0, lerpPct));
			screenOverlay.color = overlayColor;
			yield return null;
			routineElapsedTime += Time.deltaTime;
		}
	}
}
