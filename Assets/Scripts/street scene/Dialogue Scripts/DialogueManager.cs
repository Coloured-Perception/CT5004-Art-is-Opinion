using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Created by Tom	Edited by Coral and Matt Pe
/// </summary>
public class DialogueManager : MonoBehaviour {

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



	private Image CharacterImage;

	private Text nameText;
	private Text dialogueText;
	public Dialogue dialogue;

	private Animator CharacterAnim, TranAnim;
	private Queue<string> speech;


	private CameraShutterScript cameraShutterScript;


	Vector3 startPos;

	public List<Sprite> images, specialImages;
	public Image myImageComponent;
	public static DialogueManager personInstance;

	public DialogList dialogList;
	int rand, numImages;
	int randomNameNumber, randomSurnameNumber, randomGreetingNumber, randomSpeechNumber, randomRequestNumber, randomDogSpeechNumber;
	string randomName, randomSurname, randomSentence;
	private string Special = "S_", Animal = "A_", Male = "M_", Female = "F_", Mr = "Mr_", Ms = "Ms_";
	private string ImageName;
	bool down = false;

	private float textWait, appearWait;

	private GameObject TransitionController;


	private void Awake() {
		personInstance = this;  // kane do you still need this?
		TransitionController = GameObject.Find("Transition Controller");
		TranAnim = TransitionController.GetComponent<Animator>();

		CharacterImage = GameObject.Find("Character Image").GetComponent<Image>();

		CharacterAnim = gameObject.GetComponent<Animator>();
		nameText = GameObject.Find("Name").GetComponent<Text>();
		dialogueText = GameObject.Find("Dialogue").GetComponent<Text>();
		speech = new Queue<string>();
	}

	/// <summary>
	/// if charcter is rejected, reset to reappear 
	/// </summary>
	public void NoButton() {
		CharacterAnim.Play("CharacterOut");
		appearWait = Random.Range(2, 4);
	}

	public void YesButton() {
		TransitionController.gameObject.SetActive(true);
		TranAnim.Play("Camera Shutter Close Ani");
	}

	/// <summary>
	/// choses a random character
	/// </summary>
	public void ChangeImage() {
		/// chooses a random image
		rand = Random.Range(0, images.Count);
		if (rand == 0) {
			rand = Random.Range(0, specialImages.Count);
			myImageComponent.sprite = specialImages[rand];
		} else {
			myImageComponent.sprite = images[rand];
		}
		///clears previous text and sets a new name
		dialogueText.text = " ";
		ImageName = CharacterImage.transform.GetComponent<Image>().sprite.name;
		/// chooses random surname 
		randomSurnameNumber = Random.Range(0, dialogList.surnames.Count);
		randomSurname = dialogList.surnames[randomSurnameNumber]; 
		/// tests the beginiing of the image name to determine the forname or replace the whole name
		if (ImageName.StartsWith(Special)) {
			dialogue.name = ImageName.Replace(Special, "");
		} else if (ImageName.StartsWith(Male)) {
			randomNameNumber = Random.Range(0, dialogList.maleFirstNames.Count);
			randomName = dialogList.maleFirstNames[randomNameNumber];
					dialogue.name = randomName + " " + randomSurname;
		} else if (ImageName.StartsWith(Female)) {
			randomNameNumber = Random.Range(0, dialogList.femaleFirstNames.Count);
			randomName = dialogList.femaleFirstNames[randomNameNumber];
			dialogue.name = randomName + " " + randomSurname;
		} else if (ImageName.StartsWith(Mr)) {
			dialogue.name = "Mr. " + randomSurname;
		} else if (ImageName.StartsWith(Ms)) {
			dialogue.name = "Ms. " + randomSurname;
		} else if (ImageName.StartsWith(Animal)) {
			if (ImageName.StartsWith("A_Dog_")) {
				randomNameNumber = Random.Range(0, dialogList.dogNames.Count);
				randomName = dialogList.dogNames[randomNameNumber];
				dialogue.name = randomName;
			}
		}
		nameText.text = dialogue.name;
		CharacterAnim.Play("CharacterIn");
	}

	/// <summary>
	/// animation event called when character is in place
	/// </summary>
	public void CharacterInEnd() {
		StartDialogue();
	}

	public void StartDialogue() {
		/// chooses a random sentence from dialoge list 
		randomGreetingNumber = Random.Range(0, dialogList.greeting.Count);
		randomSpeechNumber = Random.Range(0, dialogList.speech.Count);
		randomRequestNumber = Random.Range(0, dialogList.request.Count);
		randomSentence = dialogList.greeting[randomGreetingNumber] + " " + dialogList.speech[randomSpeechNumber] + " " + dialogList.request[randomRequestNumber];

		ImageName = CharacterImage.transform.GetComponent<Image>().sprite.name;
		if (ImageName.StartsWith(Animal)) {
			if (ImageName.StartsWith("A_Dog_")) {
				randomSentence = dialogList.dogSpeech[randomDogSpeechNumber];
			}
		}
		speech.Enqueue(randomSentence);
		textWait = 0.3f;
	}

	private void Update() {
		if (appearWait > 0) {
			appearWait -= Time.deltaTime;
			if (appearWait <= 0) {
				ChangeImage();
			}
		}

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
			//		DisplayNextSentence();
			timeBeforeClick = timeBetweenClicks;
		}
	}

	IEnumerator TypeSentence(string sentence) {
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}
}