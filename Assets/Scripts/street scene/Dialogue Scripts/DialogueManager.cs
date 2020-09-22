using System.Collections;
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

	public List<Sprite> images, specialImages;
	public Image myImageComponent;
	public float changewait;
	public static DialogueManager personInstance;

	public DialogList dialogList;
	int rand, numImages;
	int randomNameNumber, randomSurnameNumber, randomGreetingNumber, randomSpeechNumber, randomRequestNumber, randomDogSpeechNumber;
	string randomName, randomSurname, randomSentence;
	private string Special = "S_", Animal = "A_", Male = "M_", Female = "F_", Mr = "Mr_", Ms = "Ms_";
	private string ImageName;
	bool down = false;

	private void Awake() {
		personInstance = this;
	}

	void Start() {
		speech = new Queue<string>();
		startPos = dialogueBox.transform.position;
		ChangeImage();
	}

	// choses a random character
	public void ChangeImage() {
		rand = Random.Range(0, images.Count);
		if (rand == 0) {
			rand = Random.Range(0, specialImages.Count);
			myImageComponent.sprite = specialImages[rand];
		} else {
			myImageComponent.sprite = images[rand];
		}
	}

	// changes the character to a transparent sprite for free draw mode
	public void Drawclicked() {
		myImageComponent.sprite = images[0];
	}

	public void StartDialogue(Dialogue dialogue) {

		// deletes the last text
		animator.SetBool("IsOpen", true);
		speech.Clear();
		dialogueText.text = " ";
		nameText.text = " ";

		// chooses a random sentence from dialoge list 
		randomGreetingNumber = Random.Range(0, dialogList.greeting.Count);
		randomSpeechNumber = Random.Range(0, dialogList.speech.Count);
		randomRequestNumber = Random.Range(0, dialogList.request.Count);
		randomSentence = dialogList.greeting[randomGreetingNumber] + " " + dialogList.speech[randomSpeechNumber] + " " + dialogList.request[randomRequestNumber];

		// chooses a name based on the image chosen
		// if character has a unique sentence, the current oe is replaced 
		ImageName = image.transform.GetComponent<Image>().sprite.name;

		randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
		randomSurname = dialogList.surnames[randomSurnameNumber];
		if (ImageName.StartsWith(Special)) {
			dialogue.name = ImageName.Replace(Special, "");
		} else if (ImageName.StartsWith(Male)) {
			randomNameNumber = Random.Range(0, dialogList.maleFirstNames.Count);
			//		randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			randomName = dialogList.maleFirstNames[randomNameNumber];
			//		randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = randomName + " " + randomSurname;
		} else if (ImageName.StartsWith(Female)) {
			randomNameNumber = Random.Range(0, dialogList.femaleFirstNames.Count);
			//		randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			randomName = dialogList.femaleFirstNames[randomNameNumber];
			//		randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = randomName + " " + randomSurname;
		} else if (ImageName.StartsWith(Mr)) {
			//		randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			//		randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = "Mr. " + randomSurname;
		} else if (ImageName.StartsWith(Ms)) {
			//		randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
			//		randomSurname = dialogList.surnames[randomSurnameNumber];
			dialogue.name = "Ms. " + randomSurname;
		} else if (ImageName.StartsWith(Animal)) {
			if (ImageName.StartsWith("A_Dog_")) {
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

	/// <summary>
	/// dont delete, we may need this for the tutorial 
	/// </summary>
	public void DisplayNextSentence() {
		if (speech.Count == 0) {
			//    EndDialogue();
			return;
		}
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

		// This changes the character once when it is out of shot of the camera
		if (dialogueBox.transform.position.y == startPos.y && down == false) {
			down = true;
			ChangeImage();

		} else if (dialogueBox.transform.position.y != startPos.y && down == true) {
			down = false;
		}
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