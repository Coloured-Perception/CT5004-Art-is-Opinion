using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// this script goes on gameobjects that have animations that animation events
/// </summary>
public class AnimationEventsScript : MonoBehaviour {
	TutorialManager tutorialManager;
	TutorialDialogeManager tutorialDialogeManager;
	CameraShutterScript cameraShutterScript;
	GameObject Canvas, TransitionParent;
	public Animator GalleryAnim;

	private void Awake() {
		if (SceneManager.GetActiveScene().name != "TitleScene") {
			tutorialManager = GameObject.Find("dialoge manager").GetComponent<TutorialManager>();
		}

		if (SceneManager.GetActiveScene().name == "GalleryScene") {
			Canvas = GameObject.Find("Main Canvas");
			tutorialDialogeManager = Canvas.transform.Find("Tutorial UI").GetComponent<TutorialDialogeManager>();
			TransitionParent = Canvas.transform.Find("Transition Parent").gameObject;
			cameraShutterScript = TransitionParent.transform.Find("Transition Controller").GetComponent<CameraShutterScript>();


		} else if (SceneManager.GetActiveScene().name != "TitleScene") {
			tutorialDialogeManager = GameObject.Find("dialoge manager").GetComponent<TutorialDialogeManager>();
			cameraShutterScript = GameObject.Find("Transition Controller").GetComponent<CameraShutterScript>();
		}
	}

	public void BlinkSleepEnd() {
		tutorialManager.Asleep();
		tutorialDialogeManager.StartDialogue();
	}

	public void BoxCentreHideEnd() {
		tutorialManager.WakeUp();

	}

	public void BlinkWakeEnd() {
		tutorialManager.IsAwake();
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

	public void DoorOpen() {
		GalleryAnim.Play("DoorOpen");

	}
}
