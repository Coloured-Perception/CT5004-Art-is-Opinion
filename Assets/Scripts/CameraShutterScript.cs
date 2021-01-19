using UnityEngine;

/// <summary>
/// Created by Coral
/// this class sets when the camera shutter animations should be activated 
/// </summary>
public class CameraShutterScript : MonoBehaviour {

	float timeWait;
	public GameObject cameraShutterOpen;
	public CameraShutterScript CameraShutterScriptOpen;

	/// <summary>
	/// Sets time to count from
	/// </summary>
	private void Start() {
		timeWait = 2;
	}

	/// <summary>
	/// after 2 seconds, camera shutter close finishes its animation and activates camera shutter open
	/// both have to be deactivated to stop interfering with the UI
	/// </summary>
	void Update() {

		if (timeWait > 0) {
			timeWait -= Time.deltaTime;
			if (timeWait <= 0) {
				if (this != cameraShutterOpen) {
					cameraShutterOpen.transform.gameObject.SetActive(true);
					CameraShutterScriptOpen.timeWait = 2;
					timeWait = 2;
				}
				transform.gameObject.SetActive(false);
			}
		}
	}
}