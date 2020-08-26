using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class GazeMenuUI : MonoBehaviour {
	public Button PlayButton;
	public Button DrawButton;
	public Button GalleryButton;
	public Button ExitButton;
	public Button GalleryMenuButton;
	public Button StreetMenuButton;

	public Camera mainCam;

	public GameObject dialogueManager;
	public GameObject menuUI;
	public GameObject galleryUI;
	public GameObject streetUI;
	public GameObject image;
	public GameObject yesNoButton;
	public GameObject cameraShutterClose;

	Vector3 PlayPos;
	Vector3 DrawPos;
	Vector3 GalleryPos;
	Vector3 ExitPos;
	Vector3 GalleryMenuPos;
	Vector3 StreetMenuPos;

	Rect PlayRect;
	Rect DrawRect;
	Rect GalleryRect;
	Rect ExitRect;
	Rect GalleryMenuRect;
	Rect StreetMenuRect;

	float PlayXMin;
	float PlayXMax;
	float PlayYMin;
	float PlayYMax;
	float DrawXMin;
	float DrawXMax;
	float DrawYMin;
	float DrawYMax;

	float GalleryXMin;
	float GalleryXMax;
	float GalleryYMin;
	float GalleryYMax;

	float ExitXMin;
	float ExitXMax;
	float ExitYMin;
	float ExitYMax;

	float GalleryMenuXMin;
	float GalleryMenuXMax;
	float GalleryMenuYMin;
	float GalleryMenuYMax;

	float StreetMenuXMin;
	float StreetMenuXMax;
	float StreetMenuYMin;
	float StreetMenuYMax;

	float timeBeforeClick;
	float timeBetweenClicks = 1;

	Vector2 filteredPoint;

	// Start is called before the first frame update
	void Start() {
		timeBeforeClick = timeBetweenClicks;
	}

	private void Update() {
		//Only click buttons if spacebar is down
		if (Input.GetKey("space")) {
			timeBetweenClicks -= Time.deltaTime;
			//Find position and size in x and y directions of the buttons
			PlayPos = PlayButton.transform.position;
			DrawPos = DrawButton.transform.position;
			GalleryPos = GalleryButton.transform.position;
			ExitPos = ExitButton.transform.position;
			GalleryMenuPos = GalleryMenuButton.transform.position;
			PlayRect = PlayButton.GetComponent<RectTransform>().rect;
			DrawRect = DrawButton.GetComponent<RectTransform>().rect;
			GalleryRect = GalleryButton.GetComponent<RectTransform>().rect;
			ExitRect = ExitButton.GetComponent<RectTransform>().rect;
			GalleryMenuRect = GalleryMenuButton.GetComponent<RectTransform>().rect;

			PlayXMin = PlayRect.xMin;
			PlayXMax = PlayRect.xMax;
			PlayYMin = PlayRect.yMin;
			PlayYMax = PlayRect.yMax;

			DrawXMin = DrawRect.xMin;
			DrawXMax = DrawRect.xMax;
			DrawYMin = DrawRect.yMin;
			DrawYMax = DrawRect.yMax;

			GalleryXMin = GalleryRect.xMin;
			GalleryXMax = GalleryRect.xMax;
			GalleryYMin = GalleryRect.yMin;
			GalleryYMax = GalleryRect.yMax;

			ExitXMin = ExitRect.xMin;
			ExitXMax = ExitRect.xMax;
			ExitYMin = ExitRect.yMin;
			ExitYMax = ExitRect.yMax;

			GalleryMenuXMin = GalleryMenuRect.xMin;
			GalleryMenuXMax = GalleryMenuRect.xMax;
			GalleryMenuYMin = GalleryMenuRect.yMin;
			GalleryMenuYMax = GalleryMenuRect.yMax;

			Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
			filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

			//Find if buttons are active and whether the eye is looking at them and space is down, do button code.
			if (menuUI.activeInHierarchy) {
				if ((PlayPos.x + PlayXMin) < filteredPoint.x && filteredPoint.x < (PlayPos.x + PlayXMax) && (PlayPos.y + PlayYMin) < filteredPoint.y && filteredPoint.y < (PlayPos.y + PlayYMax) && timeBetweenClicks <= 0) {
					PlayButton.GetComponent<MenuButtonScript>().ButtonClicked();
					dialogueManager.GetComponent<DialogueTrigger>().StartDialog();
					timeBeforeClick = timeBetweenClicks;
				}

				if ((DrawPos.x + DrawXMin) < filteredPoint.x && filteredPoint.x < (DrawPos.x + DrawXMax) && (DrawPos.y + DrawYMin) < filteredPoint.y && filteredPoint.y < (DrawPos.y + DrawYMax) && timeBetweenClicks <= 0) {
					image.GetComponent<DialogueManager>().Drawclicked();
					yesNoButton.GetComponent<YesScript>().YesButton();
					cameraShutterClose.SetActive(true);
					timeBeforeClick = timeBetweenClicks;
				}

				if ((GalleryPos.x + GalleryXMin) < filteredPoint.x && filteredPoint.x < (GalleryPos.x + GalleryXMax) && (GalleryPos.y + GalleryYMin) < filteredPoint.y && filteredPoint.y < (GalleryPos.y + GalleryYMax) && timeBetweenClicks <= 0) {
					mainCam.GetComponent<CameraScript>().Gallery();
					timeBeforeClick = timeBetweenClicks;
				}

				if ((ExitPos.x + ExitXMin) < filteredPoint.x && filteredPoint.x < (ExitPos.x + ExitXMax) && (ExitPos.y + ExitYMin) < filteredPoint.y && filteredPoint.y < (ExitPos.y + ExitYMax) && timeBetweenClicks <= 0) {
					Application.Quit();
					timeBeforeClick = timeBetweenClicks;
				}
			}
			if (galleryUI.activeInHierarchy) {
				if ((GalleryMenuPos.x + GalleryMenuXMin) < filteredPoint.x && filteredPoint.x < (GalleryMenuPos.x + GalleryMenuXMax) && (GalleryMenuPos.y + GalleryMenuYMin) < filteredPoint.y && filteredPoint.y < (GalleryMenuPos.y + GalleryMenuYMax) && timeBetweenClicks <= 0) {
					mainCam.GetComponent<CameraScript>().Menu();
					timeBeforeClick = timeBetweenClicks;
				}
			}
			if (streetUI.activeInHierarchy) {
				if ((StreetMenuPos.x + StreetMenuXMin) < filteredPoint.x && filteredPoint.x < (StreetMenuPos.x + StreetMenuXMax) && (StreetMenuPos.y + StreetMenuYMin) < filteredPoint.y && filteredPoint.y < (StreetMenuPos.y + StreetMenuYMax) && timeBetweenClicks <= 0) {
					mainCam.GetComponent<CameraScript>().Menu();
					timeBeforeClick = timeBetweenClicks;
				}
			}
		}
	}
}