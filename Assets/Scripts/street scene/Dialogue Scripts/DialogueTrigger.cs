using UnityEngine;

/// <summary>
/// This class controls how the character pops up
/// </summary>
public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public Animator anim;
	public GameObject Canvas;
	float timeWait;

	private void Update() {
		//if (timeWait > 0) {
		//	timeWait -= Time.deltaTime;
		//	if (timeWait <= 0) {
		//		TriggerDialogue();
		//	}
		//}
	}

	public void StartDialog() {
		//timeWait = Random.Range(2, 4);
	}

	public void StopDialog() {
		//FindObjectOfType<DialogueManager>().EndDialogue();
		//timeWait = 0;
	}

	//public void No() {
	//	FindObjectOfType<DialogueManager>().EndDialogue();
	//	timeWait = Random.Range(2, 4);
	//}

	public void TriggerDialogue() {
	//	FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}