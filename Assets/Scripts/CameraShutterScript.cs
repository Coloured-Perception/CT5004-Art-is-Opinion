using UnityEngine;

/// <summary>
/// Created by Coral
/// this class sets when the camera shutter animations should be activated 
/// </summary>
public class CameraShutterScript : MonoBehaviour {

	float timeWait;
	GameObject cameraOpen;
	GameObject cameraClose;
	GameObject cameraBoth;

	private void Awake() {
		cameraOpen = GameObject.Find("Camera Shutter Open");
		cameraClose = GameObject.Find("Camera Shutter Close");
		cameraBoth = GameObject.Find("Camera Shutter Both");
	}

	private void Start() {
		cameraClose.gameObject.SetActive(false);
		cameraBoth.gameObject.SetActive(false);
		timeWait = 3;
	}

	public void CameraClose() {
		cameraClose.gameObject.SetActive(true);
		timeWait = 3;
	}
	public void CameraBoth() {
		cameraBoth.gameObject.SetActive(true);
		timeWait = 3;
	}


	/// <summary>
	/// after 2 seconds, camera shutter close finishes its animation and activates camera shutter open
	/// both have to be deactivated to stop interfearing with the ui
	/// </summary>
	void Update() {

		if (timeWait > 0) {
			timeWait -= Time.deltaTime;
			if (timeWait <= 2) {
				cameraOpen.gameObject.SetActive(false);
			}
			if (timeWait <= 1) {
				cameraClose.gameObject.SetActive(false);
			}
			if (timeWait <= 0) {
				cameraBoth.gameObject.SetActive(false);
			}
		}
	}
}