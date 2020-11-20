﻿using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class GazeMenuUI : MonoBehaviour {
	public Button PlayButton;
	public Button DrawButton;
	public Button GalleryButton;
	public Button ExitButton;
	public Button GalleryMenuButton;
	public Button StreetMenuButton;
    public Button optionsButton;
    public Button tobiiButton;
    public Button mouseButton;
    public Button optionsMenuButton;

    public Camera mainCam;

    Rect camRect;
    public GameObject canvas;
	public GameObject dialogueManager;
	public GameObject menuUI;
	public GameObject galleryUI;
	public GameObject streetUI;
    public GameObject optionsUI;
    public GameObject image;
	public GameObject yesNoButton;
	public GameObject cameraShutterClose;

	Vector3 PlayPos;
	Vector3 DrawPos;
	Vector3 GalleryPos;
    Vector3 optionsPos;
	Vector3 ExitPos;
	Vector3 GalleryMenuPos;
	Vector3 StreetMenuPos;
    Vector3 tobiiPos;
    Vector3 mousePos;
    Vector3 optionsMenuPos;

    Rect PlayRect;
	Rect DrawRect;
	Rect GalleryRect;
	Rect ExitRect;
    Rect optionsRect;
	Rect GalleryMenuRect;
	Rect StreetMenuRect;
    Rect tobiiRect;
    Rect mouseRect;
    Rect optionsMenuRect;

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

    float optionsXMin;
    float optionsXMax;
    float optionsYMin;
    float optionsYMax;

    float GalleryMenuXMin;
	float GalleryMenuXMax;
	float GalleryMenuYMin;
	float GalleryMenuYMax;

	float StreetMenuXMin;
	float StreetMenuXMax;
	float StreetMenuYMin;
	float StreetMenuYMax;

    float tobiiXMin;
    float tobiiXMax;
    float tobiiYMin;
    float tobiiYMax;

    float mouseXMin;
    float mouseXMax;
    float mouseYMin;
    float mouseYMax;

    float optionsMenuXMin;
    float optionsMenuXMax;
    float optionsMenuYMin;
    float optionsMenuYMax;

    float timeBeforeClick;
	float timeBetweenClicks = 1;

	Vector2 filteredPoint;

	// Start is called before the first frame update
	void Start() {
		timeBeforeClick = timeBetweenClicks;

        bool isTobii = true;
        PlayerPrefs.SetInt("EyeTracking", isTobii ? 1 : 0);
    }

	private void Update() {

        camRect = mainCam.pixelRect;
        timeBetweenClicks -= Time.deltaTime;
        //Only click buttons if spacebar is down
        if (Input.GetKey("space")) {
			
			//Find position and size in x and y directions of the buttons
			PlayPos = PlayButton.transform.position;
			DrawPos = DrawButton.transform.position;
			GalleryPos = GalleryButton.transform.position;
            optionsPos = optionsButton.transform.position;
			ExitPos = ExitButton.transform.position;
			GalleryMenuPos = GalleryMenuButton.transform.position;
            tobiiPos = tobiiButton.transform.position;
            mousePos = mouseButton.transform.position;
			PlayRect = PlayButton.GetComponent<RectTransform>().rect;
			DrawRect = DrawButton.GetComponent<RectTransform>().rect;
			GalleryRect = GalleryButton.GetComponent<RectTransform>().rect;
            optionsRect = optionsButton.GetComponent<RectTransform>().rect;
			ExitRect = ExitButton.GetComponent<RectTransform>().rect;
			GalleryMenuRect = GalleryMenuButton.GetComponent<RectTransform>().rect;
            tobiiRect = tobiiButton.GetComponent<RectTransform>().rect;
            mouseRect = mouseButton.GetComponent<RectTransform>().rect;
            optionsMenuRect = optionsMenuButton.GetComponent<RectTransform>().rect;

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

            optionsXMin = optionsRect.xMin;
            optionsXMax = optionsRect.xMax;
            optionsYMin = optionsRect.yMin;
            optionsYMax = optionsRect.yMax;

            GalleryMenuXMin = GalleryMenuRect.xMin;
			GalleryMenuXMax = GalleryMenuRect.xMax;
			GalleryMenuYMin = GalleryMenuRect.yMin;
			GalleryMenuYMax = GalleryMenuRect.yMax;

            tobiiXMin = tobiiRect.xMin;
            tobiiXMax = tobiiRect.xMax;
            tobiiYMin = tobiiRect.yMin;
            tobiiYMax = tobiiRect.yMax;

            mouseXMin = mouseRect.xMin;
            mouseXMax = mouseRect.xMax;
            mouseYMin = mouseRect.yMin;
            mouseYMax = mouseRect.yMax;

            optionsMenuXMin = optionsMenuRect.xMin;
            optionsMenuXMax = optionsMenuRect.xMax;
            optionsMenuYMin = optionsMenuRect.yMin;
            optionsMenuYMax = optionsMenuRect.yMax;

            Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
            gazePoint.x *= camRect.width;
            gazePoint.y *= camRect.height;
            filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

			//Find if buttons are active and whether the eye is looking at them and space is down, do button code.
			if (menuUI.activeInHierarchy)
            {
				if ((PlayPos.x + PlayXMin) < filteredPoint.x && filteredPoint.x < (PlayPos.x + PlayXMax) && (PlayPos.y + PlayYMin) < filteredPoint.y && filteredPoint.y < (PlayPos.y + PlayYMax) && timeBetweenClicks <= 0) {
					PlayButton.GetComponent<MenuButtonScript>().ButtonClicked();
					dialogueManager.GetComponent<DialogueTrigger>().StartDialog();
                    cameraShutterClose.SetActive(true);
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

                if ((optionsPos.x + optionsXMin) < filteredPoint.x && filteredPoint.x < (optionsPos.x + optionsXMax) && (optionsPos.y + optionsYMin) < filteredPoint.y && filteredPoint.y < (optionsPos.y + optionsYMax) && timeBetweenClicks <= 0)
                {
                    optionsButton.GetComponent<MenuButtonScript>().ButtonClicked();
                    cameraShutterClose.SetActive(true);
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
            if (optionsUI.activeInHierarchy)
            {
                if ((tobiiPos.x + tobiiXMin) < filteredPoint.x && filteredPoint.x < (tobiiPos.x + tobiiXMax) && (tobiiPos.y + tobiiYMin) < filteredPoint.y && filteredPoint.y < (tobiiPos.y + tobiiYMax) && timeBetweenClicks <= 0)
                {
                    canvas.GetComponent<SettingsScript>().UseTobiiButtonClicked();
                    timeBeforeClick = timeBetweenClicks;
                }

                if ((mousePos.x + mouseXMin) < filteredPoint.x && filteredPoint.x < (mousePos.x + mouseXMax) && (mousePos.y + mouseYMin) < filteredPoint.y && filteredPoint.y < (mousePos.y + mouseYMax) && timeBetweenClicks <= 0)
                {
                    canvas.GetComponent<SettingsScript>().UseMouseButtonClicked();
                    timeBeforeClick = timeBetweenClicks;
                }

                if ((optionsMenuPos.x + optionsMenuXMin) < filteredPoint.x && filteredPoint.x < (optionsMenuPos.x + optionsMenuXMax) && (optionsMenuPos.y + optionsMenuYMin) < filteredPoint.y && filteredPoint.y < (optionsMenuPos.y + optionsMenuYMax) && timeBetweenClicks <= 0)
                {
                    optionsMenuButton.GetComponent<MenuButtonScript>().ButtonClicked();
                    cameraShutterClose.SetActive(true);
                    timeBeforeClick = timeBetweenClicks;
                }
            }
		}
	}
}