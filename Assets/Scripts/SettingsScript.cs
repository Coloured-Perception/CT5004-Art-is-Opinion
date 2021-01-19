using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls setting in option screen.
/// Author: Kane Adams
/// </summary>
public class SettingsScript : MonoBehaviour {
	static bool isTobii = true;

	// Start is called before the first frame update
	/// <summary>
	/// Loads previous setting for controls
	/// </summary>
	void Start() {
		isTobii = PlayerPrefs.GetInt("EyeTracking") == 1 ? true : false;
	}

	// Update is called once per frame
	void Update() {

	}

	/// <summary>
	/// When the player clicks (or gazes) the 'UseTobiiButton', enable Tobii controls
	/// </summary>
	public void UseTobiiButtonClicked() {
		// if the isTobii boolean is false, turn it true and save to PlayerPrefs
		isTobii = true;
		PlayerPrefs.SetInt("EyeTracking", isTobii ? 1 : 0);
		Debug.Log("You are now using Tobii eye-tracking!");
	}

	/// <summary>
	/// When the player clicks (or gazes) the 'UseMouseButton', disable Tobii controls
	/// </summary>
	public void UseMouseButtonClicked() {
		// if the isTobii boolean is false, turn it false and save to PlayerPrefs
		isTobii = false;
		PlayerPrefs.SetInt("EyeTracking", isTobii ? 1 : 0);

		Debug.Log("You are now using Mouse!");
	}
}