using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tobii.Gaming;

/// <summary>
/// this script controls when the text in dialogue list is shown and when the curators name is visible 
/// </summary>
public class TutorialDialogeManager : MonoBehaviour {

	//MattP
	//public Button continueButton;
	//Vector3 continuePos;
	Rect continueRect;

	float continueXMin;
	float continueXMax;
	float continueYMin;
	float continueYMax;

	float timeBeforeClick;
	float timeBetweenClicks = 1;
	Vector2 filteredPoint;
	
	
	DialogList dialogList;
	private Queue<string> speech;
	Text nameText;
	Text dialogueText;
	private float textWait;


	public static int sentenceNumber = 0;   // 0  14  25  28  35  52

	private void Awake() {
		speech = new Queue<string>();

		if (SceneManager.GetActiveScene().name != "GalleryScene") {
			dialogList = GameObject.Find("dialoge manager").GetComponent<DialogList>();

			GameObject Curator = GameObject.Find("Curator");
			GameObject Box = Curator.transform.Find("Box").gameObject;
			nameText = Box.transform.Find("Name").GetComponent<Text>();
			dialogueText = Box.transform.Find("Dialogue").GetComponent<Text>();

		} else {
			dialogList = GameObject.Find("dialoge manager").GetComponent<DialogList>();

			GameObject Curator = GameObject.Find("Curator");
			GameObject Box = Curator.transform.Find("Box").gameObject;
			nameText = Box.transform.Find("Name").GetComponent<Text>();
			dialogueText = Box.transform.Find("Dialogue").GetComponent<Text>();
		}
		dialogueText.text = " ";
	}

	public void StartDialogue() {
		sentenceNumber += 1;
		Debug.Log("dm    " + sentenceNumber);

		if (sentenceNumber != 5 && sentenceNumber != 12) {
			/// deletes the last text
			speech.Clear();
			dialogueText.text = " ";
			speech.Enqueue(dialogList.tutorial[sentenceNumber]);
			DisplayNextSentence();
		}
		if (sentenceNumber <= 3) {
			nameText.text = "?????";
		} else if (sentenceNumber == 14 || sentenceNumber == 25 || sentenceNumber == 28 || sentenceNumber == 34) {
			nameText.text = " ";
		} else {
			nameText.text = "Museum Curator";
		}
	}

	/// <summary>
	/// dont delete, we may need this for the tutorial 
	/// </summary>
	public void DisplayNextSentence() {
	
		textWait = 1;

	}

	private void Update() {
		if (textWait > 0) {
			textWait -= Time.deltaTime;
			if (textWait <= 0) {
				string sentence = speech.Dequeue();

				StopAllCoroutines();
				StartCoroutine(TypeSentence(sentence));
			}
		}

		//MattP
		timeBetweenClicks -= Time.deltaTime;

		//continuePos = continueButton.transform.position;
		//continueRect = continueButton.GetComponent<RectTransform>().rect;

		//continueXMin = continueRect.xMin;
		//continueXMax = continueRect.xMax;
		//continueYMin = continueRect.yMin;
		//continueYMax = continueRect.yMax;

		//Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
		//filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

		//if ((continuePos.x + continueXMin) < filteredPoint.x && filteredPoint.x < (continuePos.x + continueXMax) && (continuePos.y + continueYMin) < filteredPoint.y && filteredPoint.y < (continuePos.y + continueYMax) && timeBetweenClicks <= 0) {
		//	DisplayNextSentence();
		//	timeBeforeClick = timeBetweenClicks;
		//}		
	}

	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}	
}