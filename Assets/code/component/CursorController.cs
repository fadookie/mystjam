using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {
	public GameObject cursor;
	public LayerMask raycastLayers;

	Vector2 screenPoint;

	void Start () {
		//Always cast from center of screen
		screenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
	}

	// Update is called once per frame
	void Update () {
		Ray cursorProjection = Camera.main.ScreenPointToRay(screenPoint);
//		Debug.DrawRay(cursorProjection.origin, cursorProjection.direction, Color.red, 10f);

		RaycastHit hitInfo;
		if(Physics.Raycast(
			cursorProjection,
			out hitInfo,
			Mathf.Infinity,
			raycastLayers)
		) {
//			Debug.LogFormat("Raycast hit {0}", hitInfo.collider.gameObject.name);
			//hit

			//Check if it's interactable
			if (hitInfo.collider.gameObject.name.Equals("journal_uv_shrink")) {
				setCursorActive(true);

				if(Input.GetButtonDown("Fire1")) {
					Debug.Log("Click journal");
				}
			} else {
				setCursorActive(false);
			}
		} else {
			//miss
				setCursorActive(false);
		}

	}

	void setCursorActive(bool show) {
		cursor.SetActive(show);
	}
}
