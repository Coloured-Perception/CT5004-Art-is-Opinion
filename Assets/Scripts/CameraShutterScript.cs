using UnityEngine;

/// <summary>
/// Created by Coral
/// this class sets when the camera shutter animations should be activated 
/// </summary>
public class CameraShutterScript : MonoBehaviour {

	float timeWait;
	GameObject cameraOpen, cameraClose, cameraBoth, blink;
	private Animator BlinkAnim;

	private void Awake() {

		cameraOpen = transform.Find("Camera Shutter Open").gameObject;
		cameraClose = transform.Find("Camera Shutter Close").gameObject;
		cameraBoth = transform.Find("Camera Shutter Both").gameObject;
		blink = transform.Find("Blink").gameObject;
			   

		BlinkAnim = gameObject.GetComponent<Animator>();

		cameraOpen.gameObject.SetActive(true);
		cameraClose.gameObject.SetActive(false);
		cameraBoth.gameObject.SetActive(false);
		blink.gameObject.SetActive(false);
	}

	public void CameraClose() {
		cameraClose.gameObject.SetActive(true);
	}
	public void CameraBoth() {
		cameraBoth.gameObject.SetActive(true);
	}

	public void BlinkSleep() {
	//	blink.gameObject.SetActive(true);
		BlinkAnim.Play("BlinkSleep");
	}
	public void BlinkWake() {
//		blink.gameObject.SetActive(true);
		BlinkAnim.Play("BlinkWakeUp");
	}
}