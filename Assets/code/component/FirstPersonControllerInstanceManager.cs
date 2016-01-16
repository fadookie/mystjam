using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPersonControllerInstanceManager : MonoBehaviour {
	void Awake() {
		Services.instance.Set<FirstPersonController>(GetComponent<FirstPersonController>());
		Destroy(this);
	}
}
