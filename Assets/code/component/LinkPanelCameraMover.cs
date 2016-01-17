using UnityEngine;
using System.Collections;

public class LinkPanelCameraMover : MonoBehaviour {
	public float speed = 30.0f;
	public Transform pivot;

	// Use this for initialization
	//	void Start () {
	//	}

	void Update () {
		transform.RotateAround(pivot.position, Vector3.up, speed * Time.deltaTime);
		transform.LookAt(pivot);
	}

}
