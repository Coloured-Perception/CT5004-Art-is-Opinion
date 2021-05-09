using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by Coral
/// This class sends the player back to the street scene after they have finished painting 
/// Can be merged with save image script 
/// </summary>
public class BackToStreetScript : MonoBehaviour {

	//float sceneChangeWait;
	//CameraShutterScript cameraShutterScript;

	//private void Awake() {
	//	cameraShutterScript = GameObject.Find("Camera Controller").GetComponent<CameraShutterScript>();
	//}

	///// <summary>
	///// If the player clicks don't save, the scene will change in two seconds,
	///// this way the camera shutter closed animation can play for a seemless transition 
	///// but the scene change is not dependent on it
	///// </summary>
	//public void DontSave() {
	//	//cameraShutterScript.CameraClose();
	//	sceneChangeWait = 2;
	//}

	//private void Update() {
	//	if (sceneChangeWait > 0) {
	//		sceneChangeWait -= Time.deltaTime;
	//		if (sceneChangeWait <= 0) {
	//			if (SceneManager.GetActiveScene().name == "StillLifePaintScene") {
	//				SceneManager.LoadScene("TableScene");
	//			} else if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
	//				SceneManager.LoadScene("StreetScene");
	//			}
	//		}
	//	}
	//}
}