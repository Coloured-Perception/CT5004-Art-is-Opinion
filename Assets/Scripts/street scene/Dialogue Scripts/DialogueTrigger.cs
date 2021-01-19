using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls animations for dialogue box
/// </summary>
public class DialogueTrigger : MonoBehaviour {
	public Dialogue dialogue;
	float timeWait;
	float sceneChangeWait;
	public Animator anim;
	public GameObject Canvas;

	private void Update() {
		if (timeWait > 0) {
			timeWait -= Time.deltaTime;
			if (timeWait <= 0) {
				TriggerDialogue();
			}
		}
		if (sceneChangeWait > 0) {
			sceneChangeWait -= Time.deltaTime;
			if (sceneChangeWait <= 0) {
				SceneManager.LoadScene("PaintScene");
			}
		}
	}

	public void StartDialog() {
		timeWait = Random.Range(2, 4);
	}

	public void StopDialog() {
		FindObjectOfType<DialogueManager>().EndDialogue();

		timeWait = 0;
	}

	public void No() {
		FindObjectOfType<DialogueManager>().EndDialogue();

		timeWait = Random.Range(2, 4);
	}

	public void Yes() {
		sceneChangeWait = 2;
	}

	public void TriggerDialogue() {
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}