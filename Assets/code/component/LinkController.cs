using UnityEngine;
using System.Collections;

public class LinkController : MonoBehaviour {

	public GameObject linkPanelCamera;

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

		yield return new WaitForSeconds(0.5f);

		Camera.main.transform.position = linkPanelCamera.transform.position;
		Camera.main.transform.rotation = linkPanelCamera.transform.rotation;
	}
}
