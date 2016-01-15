using UnityEngine;
using System.Collections;

public class LinkController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LinkTo(string linkName) {
		Debug.LogFormat("LinkController: Link to {0}", linkName);
		SoundManager.instance.playOneShot(SoundManager.Sound.Link);
	}
}
