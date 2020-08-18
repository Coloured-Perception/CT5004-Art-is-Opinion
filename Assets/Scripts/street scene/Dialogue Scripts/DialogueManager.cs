﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Created by Tom	Edited by Coral and Matt Pe
/// </summary>
public class DialogueManager : MonoBehaviour {
	public Text nameText;
	public Text dialogueText;
	public float textWait;

	public Animator animator;

	private Queue<string> speech;

	//MattP
	public Button continueButton;
	Vector3 continuePos;
	Rect continueRect;

	float continueXMin;
	float continueXMax;
	float continueYMin;
	float continueYMax;

	float timeBeforeClick;
	float timeBetweenClicks = 1;
	Vector2 filteredPoint;

	public GameObject dialogueBox;
	public GameObject image;
	Vector3 startPos;
	public ChangePerson changePerson;

	public List<Sprite> images;
	public Sprite transparent;
	int numImages;
	public Image myImageComponent;
	int rand;
	public float changewait;
	public static DialogueManager personInstance;


	int randomNameNumber;
	int randomSurnameNumber;
	int randomGreetingNumber;
	int randomSpeechNumber;
	int randomRequestNumber;
	int randomDogSpeechNumber;
	string randomName;
	string randomSurname;
	string randomSentence;

	private string Special = "S ";
	private string Animal = "A ";
	private string Male = "M ";
	private string Female = "F ";
	private string Mr = "Mr ";
	private string Ms = "Ms ";
	private string ImageName;

	bool down = false;

	public DialogList dialogList;


	//Coral
	private void Awake() {
		personInstance = this;
	}

	void Start() {
		speech = new Queue<string>();
		startPos = dialogueBox.transform.position;
		ChangeImage();
	}

	public void ChangeImage() {
		rand = Random.Range(0, images.Count);
		myImageComponent.sprite = images[rand];
	}

	public void Drawclicked() {
		myImageComponent.sprite = transparent;
	}


	/// <summary>
	/// this void takes the random character generated by changePerson and
	/// changes the name and dialogue accordingly
	/// some characters have specific names or dialogue 
	/// some have their dialogue randomly chosen 
	/// </summary>
	/// <param name="dialogue"></param>
	public void StartDialogue(Dialogue dialogue) {
		animator.SetBool("IsOpen", true);
		speech.Clear();
		dialogueText.text = " ";
		nameText.text = " ";

		randomGreetingNumber = Random.Range(0, dialogList.greeting.Count);
		randomSpeechNumber = Random.Range(0, dialogList.speech.Count);
		randomRequestNumber = Random.Range(0, dialogList.request.Count);
		randomSentence = dialogList.greeting[randomGreetingNumber] + " " + dialogList.speech[randomSpeechNumber] + " " + dialogList.request[randomRequestNumber];


		ImageName = image.transform.GetComponent<Image>().sprite.name;

		if (ImageName.StartsWith(Special)) {
			dialogue.name = ImageName.Replace(Special, "");
		} else if (ImageName.StartsWith(Male)) {
			randomNameNumber = Random.Range(0, dialogList.maleFirstNames.Count);
			randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			randomName = dialogList.maleFirstNames[randomNameNumber];
			randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = randomName + " " + randomSurname;
		} else if (ImageName.StartsWith(Female)) {
			randomNameNumber = Random.Range(0, dialogList.femaleFirstNames.Count);
			randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			randomName = dialogList.femaleFirstNames[randomNameNumber];
			randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = randomName + " " + randomSurname;
		} else if (ImageName.StartsWith(Mr)) {
			randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = "Mr. " + randomSurname;
		} else if (ImageName.StartsWith(Ms)) {
			randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = "Ms. " + randomSurname;
		} else if (ImageName.StartsWith(Animal)) {
			if (ImageName.StartsWith("A Dog ")) {
				randomNameNumber = Random.Range(0, dialogList.dogNames.Count);
				randomName = dialogList.dogNames[randomNameNumber];
				dialogue.name = randomName;
				randomSentence = dialogList.dogSpeech[randomDogSpeechNumber];
			}
		}


		nameText.text = dialogue.name;

			speech.Enqueue(randomSentence);
	
		DisplayNextSentence();
	}
	//Coral

	public void DisplayNextSentence() {
		if (speech.Count == 0) {
			//    EndDialogue();
			return;
		}
		// Coral
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
		// Coral 
		//MattP
		timeBetweenClicks -= Time.deltaTime;

		continuePos = continueButton.transform.position;
		continueRect = continueButton.GetComponent<RectTransform>().rect;

		continueXMin = continueRect.xMin;
		continueXMax = continueRect.xMax;
		continueYMin = continueRect.yMin;
		continueYMax = continueRect.yMax;

		Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
		filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

		if ((continuePos.x + continueXMin) < filteredPoint.x && filteredPoint.x < (continuePos.x + continueXMax) && (continuePos.y + continueYMin) < filteredPoint.y && filteredPoint.y < (continuePos.y + continueYMax) && timeBetweenClicks <= 0) {
			DisplayNextSentence();
			timeBeforeClick = timeBetweenClicks;
		}

		// Coral
		// this changes the character once when it is out of shot of the camera
		if (dialogueBox.transform.position.y == startPos.y && down == false) {
			down = true;
			ChangeImage();

		} else if (dialogueBox.transform.position.y != startPos.y && down == true) {
			down = false;
		}
		// Coral
	}

	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue() {
		animator.SetBool("IsOpen", false);
	}
}