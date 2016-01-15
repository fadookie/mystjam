using UnityEngine;
using System.Collections;

public class TestMover : MonoBehaviour {

	public float moveAmplitude = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.localPosition;
		newPos.x = Mathf.Sin(Time.time) * moveAmplitude;
		transform.localPosition = newPos;
	}
}
