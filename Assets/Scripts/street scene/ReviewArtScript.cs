﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewArtScript : MonoBehaviour {
	public GameObject MenuUI;
	public GameObject StreetUI;

	public DialogueTrigger dialogueTrigger;

	string lastScene;

	/// <summary>
	/// Get's information on what previous scene was
	/// </summary>
	private void Awake() {
		Debug.Log("Time: " + PlayerPrefs.GetInt("PaintTime"));

		lastScene = PlayerPrefs.GetString("LastScene", null);
	}

	private void Start() {
		if (lastScene != null) {
			if (lastScene == "PaintScene") {
				//Debug.Log("Reacting");

				MenuUI.transform.gameObject.SetActive(false);
				StreetUI.transform.gameObject.SetActive(true);

				dialogueTrigger.StartDialog();
			} else {
				//Debug.Log("Not Reacting");

				MenuUI.transform.gameObject.SetActive(true);
				StreetUI.transform.gameObject.SetActive(false);
			}
		} else {
			Debug.Log("NULL");
		}
	}
}