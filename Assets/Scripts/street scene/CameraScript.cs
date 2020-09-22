﻿using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by Coral
/// This class controlls the camera movements in menu scenes 
/// </summary>
public class CameraScript : MonoBehaviour {
	bool gallery = false;
	bool menu = false;
	float menuWait;
	float galleryWait;
	public GameObject MenuUI;
	public GameObject GalleryUI;
	public Vector3 GalleryCanvas;

	private void Awake() {
		if (SceneManager.GetActiveScene().name == "StreetScene") {
			transform.position = new Vector3(0, 44, 0);
		}
		else if (SceneManager.GetActiveScene().name == "TableScene") {
			transform.position = new Vector3(0, 0, -1.6f);
		}
	}

	/// <summary>
	/// Gallery() and Menu() set the camera to spin and controls the gallery and menu uis
	/// </summary>
	public void Gallery() {
		if (menu == false) {
			gallery = true;
			galleryWait = 3;
			GalleryUI.transform.gameObject.SetActive(true);
			MenuUI.transform.gameObject.SetActive(false);
		}
	}

	public void Menu() {
		if (gallery == false) {
			menu = true;
			menuWait = 3;
			GalleryUI.transform.gameObject.SetActive(false);
			MenuUI.transform.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// This spins the camera and makes sure that it has stopped moving before its moved again
	/// Will change this to an animation to be more accurate
	/// May change the uis to slide in with the camera turn instaed of just appearing 
	/// </summary>
	private void Update() {
		if (gallery == true) {
			Quaternion newRotation = Quaternion.AngleAxis(-90, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .04f);
		}
		if (galleryWait > 0) {
			galleryWait -= Time.deltaTime;
			if (galleryWait <= 0) {
				gallery = false;
			}
		}
		if (menu == true) {
			Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.04f);
		}
		if (menuWait > 0) {
			menuWait -= Time.deltaTime;
			if (menuWait <= 0) {
				menu = false;
			}
		}
	}
}