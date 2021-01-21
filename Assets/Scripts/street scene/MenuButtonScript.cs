using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

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


	///////////////////////////////////////////////////////////////////
	/// This code is an attempt at returning back to street instead of menu when you leave painting area

	//StackTrace stackTrace;

	//string lastScene;

	///// <summary>
	///// Get's information on what previous scene was
	///// </summary>
	//private void Awake() {
	//	lastScene = PlayerPrefs.GetString("LastScene", null);
	//	//PlayerPrefs.SetString("LastScene", null);

	//	if (name == "Play Button") {
	//		if (lastScene != null) {
	//			if (lastScene == "PaintScene") {
	//				UnityEngine.Debug.Log("Reacting");

	//				ButtonClicked();
	//				PlayerPrefs.SetString("LastScene", null);
	//				lastScene = null;
	//			} else {
	//				UnityEngine.Debug.Log("Not Reacting");

	//				//MenuUI.transform.gameObject.SetActive(true);
	//				//StreetUI.transform.gameObject.SetActive(false);
	//			}
	//		}
	//	}
	//}

	//private void Start() {

	//}

	///////////////////////////////////////////////////////////////////


	/// <summary>
	/// When a button is clicked in menu screen, a timer is created
	/// </summary>
	public void ButtonClicked() {
		//stackTrace = new StackTrace();
		//UnityEngine.Debug.Log(stackTrace.GetFrame(1).GetMethod().Name);
		//if (stackTrace.GetFrame(1).GetMethod().Name == "Awake") {
		//	timeWait = 5.0f;
		//} else {
			timeWait = 2;
		//}
	}

	/// <summary>
	/// When the players clicks to go to free-draw, 
	/// no character sprite will appear in paint scene
	/// </summary>
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
				UnityEngine.Debug.Log("Time!");

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