using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by Coral
/// This class manages the UIs and their effects in all menu scenes
/// </summary>
public class MenuButtonScript : MonoBehaviour {
	public GameObject MenuUI, OptionsUI, SelectUI, SelectObjects;
	public static MenuButtonScript personInstance;
	public Sprite transparent;
	public Image myImageComponent;
	CameraShutterScript cameraShutterScript;
	float timeWait;

	private void Awake() {
		personInstance = this;
		DontDestroyOnLoad(gameObject);
		cameraShutterScript = GameObject.Find("Camera Controller").GetComponent<CameraShutterScript>();
	}

	/// <summary>
	/// Plays the full camera aniamation to cover up changes within the scene
	/// or plays the camera close animation for a seamless scene transition 
	/// </summary>
	public void ButtonClicked() {
		if (name == "Play Button" || name == "Menu Button" || name == "Options Button") {
			cameraShutterScript.CameraBoth();
		} else if (name == "Main Button" || name == "Still Life Button" || name == "Portrait Button" || name == "Draw Button" || name == "Exit Button") {
			cameraShutterScript.CameraClose();
		}
		timeWait = 1;
	}
	
	/// <summary>
	/// Changes the scene or the scene layot while the camerashutter is closed
	/// may change this to happen once the animation has stopped 
	/// or is half way through insted of using timings 
	/// </summary>
	void Update() {
		if (timeWait > 0) {
			timeWait -= Time.deltaTime;

			if (timeWait <= 0) {

				if (name == "Play Button") {
					MenuUI.transform.gameObject.SetActive(false);
					SelectUI.transform.gameObject.SetActive(true);
					if (SelectObjects != null) {
						SelectObjects.transform.gameObject.SetActive(true);
					}

				} else if (name == "Menu Button") {
					MenuUI.transform.gameObject.SetActive(true);
					OptionsUI.transform.gameObject.SetActive(false);
					SelectUI.transform.gameObject.SetActive(false);
					if (SelectObjects != null) {
						SelectObjects.transform.gameObject.SetActive(false);
					}
				} else if (name == "Options Button") {
					MenuUI.transform.gameObject.SetActive(false);
					OptionsUI.transform.gameObject.SetActive(true);
				} else if (name == "Main Button") {
					SceneManager.LoadScene("MenuScene");
				} else if (name == "Still Life Button") {
					SceneManager.LoadScene("TableScene");
				} else if (name == "Portrait Button") {
					SceneManager.LoadScene("StreetScene");
				} else if (name == "Draw Button") {
					SceneManager.LoadScene("PortraitScene");
				} else if (name == "Exit Button") {
					Application.Quit();
				}
			}
		}
	}
}