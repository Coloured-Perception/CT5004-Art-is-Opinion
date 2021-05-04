using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by Coral
/// this class sets when the camera shutter animations should be activated 
/// </summary>
public class CameraShutterScript : MonoBehaviour {

	float timeWait;
	public string toScene;
	Animator TranAnim, CameraAnim;
	TableControllerScript tableControllerScript;
	DialogueManager dialogeManager;
	TutorialDialogeManager tutorialDialogeManager;
	GameObject Canvas, CharacterCanvas, GalleryModel, Construction, Portrait;


	private void Awake() {
		//		PlayerPrefs.SetInt("fromGallery", 1);    /// remove later   
		///set the variable to 1 in the scene transition from the gallery and 
		///then change it back to 0 once the new scene is loaded 

		//		PlayerPrefs.SetInt("fromOutside", 1);    /// remove later   
		///set the variable to 1 in the scene transition from street scene and 
		///then change it back to 0 once the new scene is loaded 

		//Debug.Log(PlayerPrefs.GetInt("fromGallery") + "    Gall A");
		//Debug.Log(PlayerPrefs.GetInt("fromOutside") + "    Out A");
		//Debug.Log(PlayerPrefs.GetInt("fromFree") + "    Free A");
		//Debug.Log(PlayerPrefs.GetInt("fromTutorial") + "    Tu A");

		//PlayerPrefs.SetInt("portraitLevel", 1);    /// remove later   

		TranAnim = gameObject.GetComponent<Animator>();
		CameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();


		if (SceneManager.GetActiveScene().name == "GalleryScene") {
			if (PlayerPrefs.GetInt("fromOutside") == 1) {
				CameraAnim.Play("CameraGallerySceneEnter");
				TranAnim.Play("BlinkOpen");
			} else if (PlayerPrefs.GetInt("fromFree") == 1) {
				CameraAnim.Play("CameraRestrictedStart");
				TranAnim.Play("Camera Shutter Open Ani");
			} else if (PlayerPrefs.GetInt("fromTutorial") == 1) {
				CameraAnim.Play("CameraTutorialStart");
				TranAnim.Play("BlinkOpen");
				PlayerPrefs.SetInt("fromTutorial", 0);
			} else {
				CameraAnim.Play("CameraRestrictedStart");
				TranAnim.Play("BlinkOpen");
			}
			if (PlayerPrefs.GetInt("portraitLevel") == 0) {
				// portrait level block enabled
			} else if (PlayerPrefs.GetInt("portraitLevel") == 1) {
				// portrait level block disabled
				Canvas = GameObject.Find("Main Canvas");
				tutorialDialogeManager = Canvas.transform.Find("Tutorial UI").GetComponent<TutorialDialogeManager>();
				GalleryModel = GameObject.Find("Gallery version 6");
				Construction = GalleryModel.transform.Find("Construction").gameObject;
				Portrait = Construction.transform.Find("Portrait").gameObject;
				Portrait.gameObject.SetActive(false);
			} else {
				// portrait level block disabled
				GalleryModel = GameObject.Find("Gallery version 6");
				Construction = GalleryModel.transform.Find("Construction").gameObject;
				Portrait = Construction.transform.Find("Portrait").gameObject;
				Portrait.gameObject.SetActive(false);
			}

		} else if (SceneManager.GetActiveScene().name == "TableScene" || SceneManager.GetActiveScene().name == "StreetScene") {
			if (SceneManager.GetActiveScene().name == "TableScene") {
				tableControllerScript = GameObject.Find("TableController").GetComponent<TableControllerScript>();
			} else {
				CharacterCanvas = GameObject.Find("Character Canvas");

				dialogeManager = CharacterCanvas.transform.Find("Character UI").GetComponent<DialogueManager>();
			}
			if (PlayerPrefs.GetInt("fromGallery") == 1) {
				TranAnim.Play("BlinkOpen");
			} else {
				TranAnim.Play("Camera Shutter Open Ani");
			}
		} else if (SceneManager.GetActiveScene().name == "StillLifePaintScene" || SceneManager.GetActiveScene().name == "PortraitPaintScene") {
			TranAnim.Play("Camera Shutter Open Ani");
		} else if (SceneManager.GetActiveScene().name == "tutorial test") {
			tutorialDialogeManager = GameObject.Find("dialoge manager").GetComponent<TutorialDialogeManager>();
			if (PlayerPrefs.GetInt("banana") == 1 || PlayerPrefs.GetInt("apple") == 1) {
				TranAnim.Play("Camera Shutter Open Ani");
			}

		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			tutorialDialogeManager = GameObject.Find("dialoge manager").GetComponent<TutorialDialogeManager>();
			TranAnim.Play("Camera Shutter Open Ani");
		}
	}


	/// <summary>
	/// animation events -----------------------------------------------------------------------------------------------
	/// </summary>
	/// Blink open is used to change area scenes. resets "from" prefs 
	public void BlinkOpenEnd() {
		if (SceneManager.GetActiveScene().name == "StreetScene") {
			CameraAnim.Play("CameraStreetSceneLook");
		//	dialogeManager.ChangeImage();
		} else if (SceneManager.GetActiveScene().name == "TableScene") {
			CameraAnim.Play("CameraTableSceneLook");
			tableControllerScript.ChangeTables();
		} else if (SceneManager.GetActiveScene().name == "GalleryScene") {
			if (PlayerPrefs.GetInt("portraitLevel") == 1) {
				tutorialDialogeManager.StartDialogue();
				PlayerPrefs.SetInt("portraitLevel", 2);
			}
		}
		PlayerPrefs.SetInt("fromOutside", 0);
		gameObject.SetActive(false);
	}
	/// <summary>
	/// Blink closed is used to change area scenes. "from" pref is set depending on current scene
	/// </summary>
	public void BlinkCloseEnd() {
		if (SceneManager.GetActiveScene().name == "GalleryScene") {
			PlayerPrefs.SetInt("fromGallery", 1);
		} else if (SceneManager.GetActiveScene().name == "StreetScene") {
			PlayerPrefs.SetInt("fromOutside", 1);
		} else if (SceneManager.GetActiveScene().name == "tutorial test") {
			PlayerPrefs.SetInt("fromTutorial", 1);
			toScene = "Gallery";
		}
		//Debug.Log(toScene);
		SceneManager.LoadScene(toScene + "Scene");
	}

	public void CameraCloseEnd() {
		if (SceneManager.GetActiveScene().name == "StreetScene") {
			SceneManager.LoadScene("PortraitPaintScene");
		} else if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
			if (PlayerPrefs.GetInt("fromGallery") == 1) {
				PlayerPrefs.SetInt("fromGallery", 0);
				PlayerPrefs.SetInt("fromOutside", 0);
				PlayerPrefs.SetInt("fromFree", 1);
				SceneManager.LoadScene("GalleryScene");
			} else {
				SceneManager.LoadScene("StreetScene");
			}
		} else if (SceneManager.GetActiveScene().name == "TableScene") {
			SceneManager.LoadScene("StillLifePaintScene");
		} else if (SceneManager.GetActiveScene().name == "StillLifePaintScene") {
			if (PlayerPrefs.GetInt("fromGallery") == 1) {
				PlayerPrefs.SetInt("fromGallery", 0);
				PlayerPrefs.SetInt("fromOutside", 0);
				PlayerPrefs.SetInt("fromFree", 1);
				SceneManager.LoadScene("GalleryScene");
			} else {
				SceneManager.LoadScene("TableScene");
			}
		} else if (SceneManager.GetActiveScene().name == "GalleryScene") {
			PlayerPrefs.SetInt("fromGallery", 1);
			PlayerPrefs.SetInt("fromOutside", 0);
			SceneManager.LoadScene(toScene + "Scene");
		} else if (SceneManager.GetActiveScene().name == "tutorial test") {
			SceneManager.LoadScene("StillLifeTutorialPaintScene");
		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			SceneManager.LoadScene(toScene + "tutorial test");
		}
	}
	
	

	public void CameraOpenEnd() {
		CameraAnim.enabled = true;
		if (SceneManager.GetActiveScene().name == "StreetScene") {
			CameraAnim.Play("CameraStreetSceneReaction");
		} else if (SceneManager.GetActiveScene().name == "TableScene") {
			CameraAnim.Play("CameraTableSceneReaction");
			tableControllerScript.ChangeTables();
		} else if (SceneManager.GetActiveScene().name == "tutorial test" && PlayerPrefs.GetInt("banana") != 0) {
			tutorialDialogeManager.StartDialogue();


		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			if (PlayerPrefs.GetInt("banana") == 1) {
				TutorialDialogeManager.sentenceNumber -= 1;
			}
			tutorialDialogeManager.StartDialogue();
		}
		//PlayerPrefs.SetInt("fromGallery", 0);
		//	PlayerPrefs.SetInt("fromOutside", 0);
		PlayerPrefs.SetInt("fromFree", 0);

		gameObject.SetActive(false);
	}

	public void BlinkWakeEnd() {
		gameObject.SetActive(false);

	}


	/// <summary>
	/// buttons -----------------------------------------------------------------------------------------------
	/// </summary>
	public void ReturnButton() {
		toScene = "Gallery";
		gameObject.SetActive(true);
		TranAnim.Play("BlinkClose");
	}

	public void StreetButton() {
		toScene = "Street";
		gameObject.SetActive(true);
		TranAnim.Play("BlinkClose");
	}






	public void CameraClose() {
	}
	public void CameraBoth() {
	}

	public void BlinkOpen() {
		TranAnim.Play("BlinkSleep");
	}

	public void BlinkSleep() {
		TranAnim.Play("BlinkSleep");
	}
	//public void BlinkWake() {
	//	TranAnim.Play("BlinkWakeUp");
	//}
}