using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by Coral
/// this class manages the menu and street uis
/// </summary>
public class MenuButtonScript : MonoBehaviour {
	float timeWait;
	public GameObject MenuUI;
	public GameObject OptionsUI;
	public GameObject SelectUI;
	public GameObject SelectObjects;
	public static MenuButtonScript personInstance;
	public Sprite transparent;
	public Image myImageComponent;
	CameraShutterScript cameraShutterScript;

	private void Awake() {
		personInstance = this;
		DontDestroyOnLoad(gameObject);
		cameraShutterScript = GameObject.Find("Camera Controller").GetComponent<CameraShutterScript>();
	}

	public void ButtonClicked() {
		if (name == "Play Button" || name == "Menu Button" || name == "Options Button") {
			cameraShutterScript.CameraBoth();
			timeWait = 1;

		} else if (name == "Main Button" || name == "Still Life Button" || name == "Portrait Button" || name == "Draw Button" || name == "Exit Button") {
			cameraShutterScript.CameraClose();
			timeWait = 1;
		}
	}


	/// <summary>
	/// waits 2 seconds for the camera shutter animation to play to have a seamless transition but is not dependent on it 
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