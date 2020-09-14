
using UnityEngine;

public class LandscapeCameraScript : MonoBehaviour {

	private Animator anim;
	private bool ZoomedIn;

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	private void Start() {
		anim.Play("CameraLandscapeStart");
	}

	public void ButtonPressed() {
		if (ZoomedIn) {
			ZoomOut();
		} else {
			ZoomIn();
		}
	}

	private void ZoomIn() {
		anim.Play("CameraLandscapeZoomIn");
		ZoomedIn = true;
	}

	private void ZoomOut() {
		anim.Play("CameraLandscapeZoomOut");
		ZoomedIn = false;
	}
}