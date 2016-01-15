using UnityEngine;
using System.Collections;

public class LinkController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Services.instance.Set<LinkController>(this);
	}
	
	public void LinkTo(string linkName) {
		Debug.LogFormat("LinkController: Link to {0}", linkName);
		Services.instance.Get<SoundManager>().playOneShot(SoundManager.Sound.Link);
	}
}
