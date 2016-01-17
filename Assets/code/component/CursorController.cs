using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CursorController : MonoBehaviour {
	public GameObject cursor;
	public LayerMask raycastLayers;
	public float raycastDistance = Mathf.Infinity;

	//For interactable objects
	public GameObject journalPopup;

	Vector2 screenPoint;
	bool viewLocked = false;

	void Start () {
		//Always cast from center of screen
		screenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
	}

	// Update is called once per frame
	void Update () {
		if (viewLocked) {
			return;
		}

		Ray cursorProjection = Camera.main.ScreenPointToRay(screenPoint);
//		Debug.DrawRay(cursorProjection.origin, cursorProjection.direction, Color.red, 10f);

		RaycastHit hitInfo;
		if(Physics.Raycast(
			cursorProjection,
			out hitInfo,
			raycastDistance,
			raycastLayers)
		) {
//			Debug.LogFormat("Raycast hit {0}", hitInfo.collider.gameObject.name);
			//hit

			//Check if it's interactable
			if (hitInfo.collider.gameObject.name.Equals("journal_uv_shrink")) {
				setCursorActive(true);

				if(Input.GetButtonDown("Fire1")) {
					Services.instance.Get<SoundManager>().playOneShot(SoundManager.Sound.PageFlip);
					journalPopup.SetActive(true);
					lockView(true);
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

	public void lockView(bool lockView) {
		viewLocked = lockView;

		Services.instance.Get<FirstPersonController>().m_MouseLook.SetCursorLock(!lockView);

		if(lockView) {
			setCursorActive(false);
		}
	}
}
