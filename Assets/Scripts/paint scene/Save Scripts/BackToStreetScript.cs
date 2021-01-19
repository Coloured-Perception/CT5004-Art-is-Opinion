using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by Coral
/// This class sends the player back to the street scene after they have finished painting 
/// </summary>
public class BackToStreetScript : MonoBehaviour {
	float sceneChangeWait;

	/// <summary>
	/// If the player clicks don't save, the scene will change in two seconds,
	/// this way the camera shutter closed animation can play for a seamless transition but the scene change is not dependent on it
	/// </summary>
	public void DontSave() {
		sceneChangeWait = 2;
	}

	/// <summary>
	/// Counts down to zero before changing scene
	/// </summary>
	private void Update() {
		if (sceneChangeWait > 0) {
			sceneChangeWait -= Time.deltaTime;
			if (sceneChangeWait <= 0) {
				SceneManager.LoadScene("StreetScene");
			}
		}
	}
}