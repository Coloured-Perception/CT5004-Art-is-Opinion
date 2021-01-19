using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by Coral
/// this class manages the menu and street UIs
/// </summary>
public class MenuButtonScript : MonoBehaviour {
	float timeWait;
	public GameObject MenuUI;
	public GameObject StreetUI;
	public GameObject OptionsUI;
	public static MenuButtonScript personInstance;
	public Sprite transparent;
	public Image myImageComponent;

	//private void Awake() {
	//	personInstance = this;
	//	DontDestroyOnLoad(gameObject);
	//}

	public void ButtonClicked() {
		timeWait = 2;
	}

	public void Drawclicked() {
		myImageComponent.sprite = transparent;
		ButtonClicked();
	}

	/// <summary>
	/// Closes game
	/// </summary>
	public void Exit() {
		Application.Quit();
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
					StreetUI.transform.gameObject.SetActive(true);
				} else if (name == "Menu Button") {
					MenuUI.transform.gameObject.SetActive(true);
					StreetUI.transform.gameObject.SetActive(false);
					OptionsUI.transform.gameObject.SetActive(false);
				} else if (name == "Options Button") {
					MenuUI.transform.gameObject.SetActive(false);
					OptionsUI.transform.gameObject.SetActive(true);
				}
			}
		}
	}
}