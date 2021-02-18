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
		cameraShutterScript = GameObject.Find("Transition Controller").GetComponent<CameraShutterScript>();
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

}
