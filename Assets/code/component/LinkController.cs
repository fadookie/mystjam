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

		Camera.main.transform.position = linkPanelCamera.transform.position;
		Camera.main.transform.rotation = linkPanelCamera.transform.rotation;
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
}
