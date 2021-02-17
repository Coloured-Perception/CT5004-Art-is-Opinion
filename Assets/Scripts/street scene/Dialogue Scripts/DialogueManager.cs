﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Created by Tom	Edited by Coral and Matt Pe
/// This class randomly selects the character and their dialogue
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
	public List<Sprite> specialImages;
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

	private string Special = "S_";
	private string Animal = "A_";
	private string Male = "M_";
	private string Female = "F_";
	private string Mr = "Mr_";
	private string Ms = "Ms_";
	private string ImageName;

	bool down = false;

	public DialogueList dialogueList;

	public string lastScene;

	//Coral
	private void Awake() {
		personInstance = this;
		lastScene = PlayerPrefs.GetString("LastScene");
	}

	void Start() {
		speech = new Queue<string>();
		startPos = dialogueBox.transform.position;
	}

	/// <summary>
	/// Selects the image for the character that appears in the street scene
	/// </summary>
	public void ChangeImage() {
		//Debug.Log(lastScene);
		// If the previous scene was the paint scene, then the previous character is used, otherwise a random character is selected
		if (lastScene == "PaintScene") {
			if (PlayerPrefs.GetInt("IsSpecialPerson") == 1) {
				myImageComponent.sprite = specialImages[PlayerPrefs.GetInt("Person")];
			} else {
				myImageComponent.sprite = images[PlayerPrefs.GetInt("Person")];
			}
		} else {
			rand = Random.Range(0, images.Count);
			//Debug.Log(rand);
			if (rand == 0) {
				rand = Random.Range(0, specialImages.Count);
				myImageComponent.sprite = specialImages[rand];

				//Debug.Log(rand);
				PlayerPrefs.SetInt("IsSpecialPerson", 1);
				PlayerPrefs.SetInt("Person", rand);
			} else {
				myImageComponent.sprite = images[rand];

				//Debug.Log(rand);
				PlayerPrefs.SetInt("IsSpecialPerson", 0);
				PlayerPrefs.SetInt("Person", rand);
			}
		}
	}

	/// <summary>
	/// If player chooses to free draw,  no character (image) is selected
	/// </summary>
	public void Drawclicked() {
		myImageComponent.sprite = images[0];
	}

	/// <summary>
	/// this void takes the random character generated by changePerson and
	/// changes the name and dialog accordingly
	/// some characters have specific names or dialog 
	/// some have their dialog randomly chosen 
	/// </summary>
	/// <param name="dialogue"></param>
	public void StartDialogue(Dialogue dialogue) {
		if (lastScene == "PaintScene") {
			ReactionDialogue();
		} else {
			animator.SetBool("IsOpen", true);
			speech.Clear();
			dialogueText.text = " ";
			nameText.text = " ";

			randomGreetingNumber = Random.Range(0, dialogueList.greeting.Count);
			randomSpeechNumber = Random.Range(0, dialogueList.speech.Count);
			randomRequestNumber = Random.Range(0, dialogueList.request.Count);
			randomSentence = dialogueList.greeting[randomGreetingNumber] + " " + dialogueList.speech[randomSpeechNumber] + " " + dialogueList.request[randomRequestNumber];


			ImageName = image.transform.GetComponent<Image>().sprite.name;

			if (ImageName.StartsWith(Special)) {
				dialogue.name = ImageName.Replace(Special, "");
			} else if (ImageName.StartsWith(Male)) {
				randomNameNumber = Random.Range(0, dialogueList.maleFirstNames.Count);
				randomSurnameNumber = Random.Range(0, dialogueList.surnames.Count);
				randomName = dialogueList.maleFirstNames[randomNameNumber];
				randomSurname = dialogueList.surnames[randomSurnameNumber];
				dialogue.name = randomName + " " + randomSurname;
			} else if (ImageName.StartsWith(Female)) {
				randomNameNumber = Random.Range(0, dialogueList.femaleFirstNames.Count);
				randomSurnameNumber = Random.Range(0, dialogueList.surnames.Count);
				randomName = dialogueList.femaleFirstNames[randomNameNumber];
				randomSurname = dialogueList.surnames[randomSurnameNumber];
				dialogue.name = randomName + " " + randomSurname;
			} else if (ImageName.StartsWith(Mr)) {
				randomSurnameNumber = Random.Range(0, dialogueList.surnames.Count);
				randomSurname = dialogueList.surnames[randomSurnameNumber];
				dialogue.name = "Mr. " + randomSurname;
			} else if (ImageName.StartsWith(Ms)) {
				randomSurnameNumber = Random.Range(0, dialogueList.surnames.Count);
				randomSurname = dialogueList.surnames[randomSurnameNumber];
				dialogue.name = "Ms. " + randomSurname;
			} else if (ImageName.StartsWith(Animal)) {
				if (ImageName.StartsWith("A_Dog_")) {
					randomNameNumber = Random.Range(0, dialogueList.dogNames.Count);
					randomName = dialogueList.dogNames[randomNameNumber];
					dialogue.name = randomName;
					randomSentence = dialogueList.dogSpeech[randomDogSpeechNumber];
				}
			}

			nameText.text = dialogue.name;
			PlayerPrefs.SetString("PersonName", nameText.text);

			speech.Enqueue(randomSentence);

			DisplayNextSentence();
		}
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
		if (lastScene != "PaintScene") {
			if (dialogueBox.transform.position.y == startPos.y && down == false) {
				down = true;
				ChangeImage();
			} else if (dialogueBox.transform.position.y != startPos.y && down == true) {
				down = false;
			}
		}

		// Coral

	}


	//////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Sets up dialogue for a character reacting to art drawn
	/// </summary>
	void ReactionDialogue() {
		//myImageComponent.sprite = personInstance.myImageComponent.sprite;

		ChangeImage();

		//Debug.Log(PlayerPrefs.GetString("PersonName"));
		//Debug.Log(PlayerPrefs.GetInt("IsSpecialPerson"));
		//Debug.Log(PlayerPrefs.GetInt("Person"));

		//Debug.Log("Noice!");

		PlayerPrefs.SetString("LastScene", null);

		nameText.text = PlayerPrefs.GetString("PersonName");

		animator.SetBool("IsOpen", true);
		speech.Clear();

		randomSentence = "Hey, Dat's pretty Gud!";

		speech.Enqueue(randomSentence);

		DisplayNextSentence();
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////
	

	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	/// <summary>
	/// Resets dialogue and previous scene for new character
	/// </summary>
	public void EndDialogue() {
		animator.SetBool("IsOpen", false);
		lastScene = null;
	}
}