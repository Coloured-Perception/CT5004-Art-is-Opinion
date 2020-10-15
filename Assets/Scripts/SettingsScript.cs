/// <summary>
/// Name:           SettingsScript.cs
/// Purpose:        Controls settings in option screen
///					Contains settings to change controls between mouse and Tobii eye-tracking
/// Author:         Kane Adams
/// Date Created:   15/10/2020
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour {
	bool isTobii = true;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	/// <summary>
	/// When the player clicks (or gazes) the 'UseTobiiButton', enable Tobii controls
	/// </summary>
	public void UseTobiiButtonClicked() {
		// if the isTobii boolean is false, turn it true and save to PlayerPrefs
		if (!isTobii) {
			isTobii = true;
			PlayerPrefs.SetInt("EyeTracking", isTobii ? 1 : 0);

			Debug.Log("You are now using Tobii eye-tracking!");
		}
	}

	/// <summary>
	/// When the player clicks (or gazes) the 'UseMouseButton', disable Tobii controls
	/// </summary>
	public void UseMouseButtonClicked() {
		// if the isTobii boolean is false, turn it false and save to PlayerPrefs
		if (isTobii) {
			isTobii = false;
			PlayerPrefs.SetInt("EyeTracking", isTobii ? 1 : 0);

			Debug.Log("You are now using Mouse!");
		}
	}
}