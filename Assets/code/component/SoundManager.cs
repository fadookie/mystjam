using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip linkSound;
	public AudioClip pageFlipSound;
	public AudioSource source;

	public enum Sound {
		Link,
		PageFlip,
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
			case Sound.PageFlip:
				return pageFlipSound;
			default:
				return null;
		}
	}
}
