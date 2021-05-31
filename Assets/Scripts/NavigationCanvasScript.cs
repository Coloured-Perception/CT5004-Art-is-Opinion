using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationCanvasScript : MonoBehaviour {
	GameObject transitionController;
	CameraShutterScript cameraShutterScript;
	GameObject camera;
	Animator cameraAnim;

	private void Awake() {
		transitionController = GameObject.Find("Transition Controller");
		camera = GameObject.Find("Main Camera");
		cameraShutterScript = transitionController.GetComponent<CameraShutterScript>();
		cameraAnim = camera.GetComponent<Animator>();
		if (PlayerPrefs.GetInt("PaintingAmount") < 5) {
			if (gameObject.name == "Street" || gameObject.name == "PortraitPaint") {
				gameObject.SetActive(false);
			}
		}
	}

	public void ChangeScene() {
		Debug.Log(gameObject.name + "  name");
		cameraShutterScript.toScene = gameObject.name;
		Debug.Log(cameraShutterScript.toScene + "  set");

	}

	public void Move() {
		//Debug.Log(gameObject.name);
		if (gameObject.name == "SLtoP" && PlayerPrefs.GetInt("portraitLevel") == 0) {

		} else if (gameObject.name == "Street" && PlayerPrefs.GetInt("portraitLevel") == 0) {

		} else {
			cameraAnim.enabled = true;
			cameraAnim.applyRootMotion = false;
			cameraAnim.Play(gameObject.name);
		}
	}
}