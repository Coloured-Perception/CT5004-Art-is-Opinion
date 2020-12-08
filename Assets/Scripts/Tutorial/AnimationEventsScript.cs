using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// this script goes on gameobjects that have animations that animation events
/// </summary>
public class AnimationEventsScript : MonoBehaviour {
	TutorialManager tutorialManager;
	TutorialDialogeManager tutorialDialogeManager;
	CameraShutterScript cameraShutterScript;

	private void Awake() {
		tutorialManager = GameObject.Find("dialoge manager").GetComponent<TutorialManager>();
		tutorialDialogeManager = GameObject.Find("dialoge manager").GetComponent<TutorialDialogeManager>();
		cameraShutterScript = GameObject.Find("Camera Controller").GetComponent<CameraShutterScript>();
	}

	public void BlinkSleepEnd() {
		tutorialManager.Asleep();
		tutorialDialogeManager.StartDialogue();
	}

	public void BlinkWakeEnd() {
		tutorialManager.WakeUp();
		tutorialDialogeManager.StartDialogue();
	}

	public void EaselShowEnd() {
		tutorialDialogeManager.StartDialogue();
	}

	public void CameraEaselToTableEnd() {
		tutorialManager.MoveTable();
	}

	public void TutorialEnd() {
		PlayerPrefs.SetInt("intro", 1);
		cameraShutterScript.CameraClose();
	}

	public void CameraOpenEnd() {
		Debug.Log("fasfehgrh");
		if (SceneManager.GetActiveScene().name == "tutorial test" && PlayerPrefs.GetInt("banana") != 0) {
			tutorialDialogeManager.StartDialogue();
	

		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene" && PlayerPrefs.GetInt("banana") == 0) {
			tutorialDialogeManager.StartDialogue();
		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene" && PlayerPrefs.GetInt("banana") == 1) {
			TutorialDialogeManager.sentenceNumber -= 1;
			tutorialDialogeManager.StartDialogue();
		}
		gameObject.SetActive(false);
	}

	public void CameraCloseEnd() {
		if (SceneManager.GetActiveScene().name == "StillLifePaintScene") {
			SceneManager.LoadScene("TableScene");
		} else if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
			SceneManager.LoadScene("StreetScene");
		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") { 
			SceneManager.LoadScene("tutorial test");
		} else if (SceneManager.GetActiveScene().name == "tutorial test") {
			SceneManager.LoadScene("GalleryScene");
		}
	}

	public void CameraBothClosed() {

	}

	public void CameraBothEnd() {
		gameObject.SetActive(false);
	}
}
