using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip linkSound;
	public AudioSource source;

	public enum Sound {
		Link,
	}

	void Awake() {
		Services.instance.Set<SoundManager>(this);
	}

	public void playOneShot(Sound sound) {
		source.PlayOneShot(getClip(sound));
	}

	AudioClip getClip(Sound sound) {
		switch(sound) {
			case Sound.Link:
				return linkSound;
			default:
				return null;
		}
	}
}
