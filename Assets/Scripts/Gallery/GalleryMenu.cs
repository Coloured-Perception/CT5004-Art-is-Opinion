using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class GalleryMenu : MonoBehaviour {
	public GameObject tobiiButton;
	public GameObject mouseButton;

	GameObject objHit;

	public Camera mainCam;

	Vector2 filteredPoint;

	public GameObject isEyeTracker;
	public GameObject tobiiTime;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker) {
			if (Input.GetKey("space")) {
				//camRect = mainCam.pixelRect;

				Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
																	  //gazePoint.x *= camRect.width;
																	  //gazePoint.y *= camRect.height;
				filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

				Ray ray = mainCam.ScreenPointToRay(filteredPoint);
				if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
					objHit = hit.transform.gameObject;
				}

				if (tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0) {
					if (GameObject.ReferenceEquals(objHit, mouseButton)) {
						UseMouse();
						tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
					} else if (GameObject.ReferenceEquals(objHit, tobiiButton)) {
						UseTobii();
						tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
					}
				}
			}
		}
	}

	public void UseTobii() {
		PlayerPrefs.SetInt("isEyeTracker", 1);
	}

	public void UseMouse() {
		PlayerPrefs.SetInt("isEyeTracker", 0);
	}
}