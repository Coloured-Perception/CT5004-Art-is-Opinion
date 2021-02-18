using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controlls what happens after a character is accepted or rejected 
/// </summary>
/// 

	///////////////////////////////////////////////////////// this script may be oboslete 
public class ChoiceScript : MonoBehaviour {

	float sceneChangeWait;

	//MattP
	public Button yesButton;
	public Button noButton;
	Vector3 yesPos;
	Vector3 noPos;
	Rect yesRect;
	Rect noRect;

	float yesXMin;
	float yesXMax;
	float yesYMin;
	float yesYMax;
	float noXMin;
	float noXMax;
	float noYMin;
	float noYMax;

	float timeBeforeClick;
	float timeBetweenClicks = 1;
	Vector2 filteredPoint;

	//CameraShutterScript cameraShutterScript;

	private void Awake() {
	//	cameraShutterScript = GameObject.Find("Camera Controller").GetComponent<CameraShutterScript>();
	}

	private void Update() {
		if (sceneChangeWait > 0) {
			sceneChangeWait -= Time.deltaTime;
			if (sceneChangeWait <= 0) {
				if (SceneManager.GetActiveScene().name == "StreetScene") {
					SceneManager.LoadScene("PortraitPaintScene");
				} else if (SceneManager.GetActiveScene().name == "TableScene") {
					SceneManager.LoadScene("StillLifePaintScene");
				}
			}
		}

		//Only click buttons if spacebar is down
		if (Input.GetKey("space")) {
			timeBetweenClicks -= Time.deltaTime;
			//Find position and size in x and y directions of the buttons
			yesPos = yesButton.transform.position;
			noPos = noButton.transform.position;
			yesRect = yesButton.GetComponent<RectTransform>().rect;
			noRect = noButton.GetComponent<RectTransform>().rect; ;

			yesXMin = yesRect.xMin;
			yesXMax = yesRect.xMax;
			yesYMin = yesRect.yMin;
			yesYMax = yesRect.yMax;

			noXMin = noRect.xMin;
			noXMax = noRect.xMax;
			noYMin = noRect.yMin;
			noYMax = noRect.yMax;

			Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
			filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

			//Find if buttons are active and whether the eye is looking at them and space is down, do button code.
			if ((yesPos.x + yesXMin) < filteredPoint.x && filteredPoint.x < (yesPos.x + yesXMax) && (yesPos.y + yesYMin) < filteredPoint.y && filteredPoint.y < (yesPos.y + yesYMax) && timeBetweenClicks <= 0) {
				YesButton();
				timeBeforeClick = timeBetweenClicks;
			}

			if ((noPos.x + noXMin) < filteredPoint.x && filteredPoint.x < (noPos.x + noXMax) && (noPos.y + noYMin) < filteredPoint.y && filteredPoint.y < (noPos.y + noYMax) && timeBetweenClicks <= 0) {
				NoButton();
				timeBeforeClick = timeBetweenClicks;
			}
		}
	}

	public void NoButton() {
		//if (SceneManager.GetActiveScene().name == "StreetScene") {
		//	FindObjectOfType<DialogueTrigger>().No();
		//}
	}

	public void YesButton() {
		//sceneChangeWait = 2;
		//cameraShutterScript.CameraClose();
	}
}