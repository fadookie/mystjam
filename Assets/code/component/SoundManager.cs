using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip linkSound;
	public AudioClip pageFlipSound;
	public LoopData windLoop;
	public LoopData oceanLoop;
	public AudioSource source;
	public AudioSource loopSource;


	public enum LoopType {
		None,
		Wind,
		Ocean
	}

	[System.Serializable]
	public class LoopData {
		public AudioClip clip;
		public float volume;
	}

	public enum Sound {
		None,
		Link,
		PageFlip,
	}

	void Awake() {
		Services.instance.Set<SoundManager>(this);
		loopSource.loop = true;
	}

	public void playOneShot(Sound sound) {
		if (sound != Sound.None) {
			source.PlayOneShot(getClip(sound));
		}
	}

	public void playLoop(LoopType type, float delay) {
		if (type == LoopType.None) {
			loopSource.Stop();
		} else {
			Debug.LogFormat("play loop {0}", type);
			LoopData loop = getLoop(type);
			loopSource.clip = loop.clip;
			loopSource.volume = loop.volume;
			loopSource.PlayDelayed(delay);
		}
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

	LoopData getLoop(LoopType type) {
		switch(type) {
			case LoopType.Wind:
				return windLoop;
			case LoopType.Ocean:
				return oceanLoop;
			default:
				return null;
		}
	}
}
