using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by Coral
/// this class sends the player back to the street scene after they have finished painting 
/// </summary>
public class BackToStreetScript : MonoBehaviour {

	float sceneChangeWait;

	/// <summary>
	/// if the player clicks don't save, the scene will change in two seconds,
	/// this way the camera shutter closed animation can play for a seemless transition but the scene change is not dependent on it
	/// </summary>
	public void DontSave() {
		sceneChangeWait = 2;
	}

	private void Update() {
		if (sceneChangeWait > 0) {
			sceneChangeWait -= Time.deltaTime;
			if (sceneChangeWait <= 0) {
				if (SceneManager.GetActiveScene().name == "StillLifePaintScene") {
					SceneManager.LoadScene("TableScene");
				} else if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
					SceneManager.LoadScene("StreetScene");
				}
			}
		}
	}
}