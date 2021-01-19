using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

namespace unitycoder_MobilePaint {
	public class GazeAwareButtons : MonoBehaviour {

		public bool isEyeTracker;
		public GameObject brushSize;
		public GameObject optionsCanvas;
		public GameObject paintCanvas;
		public Button increaseSizeButton;
		public Button decreaseSizeButton;
		public Button clearImageButton;
		public Button finishButton;
		public Button saveButton;
		public Button notSaveButton;
		public Camera saveCamera;
		public Camera mainCam;
		Rect camRect;
		public GameObject cameraShutterClose;
		Vector3 increasePos;
		Vector3 decreasePos;
		Vector3 clearPos;
		Vector3 finishPos;
		Vector3 savePos;
		Vector3 notSavePos;

		Rect increaseRect;
		Rect decreaseRect;
		Rect clearRect;
		Rect finishRect;
		Rect saveRect;
		Rect notSaveRect;

		float increaseXMin;
		float increaseXMax;
		float increaseYMin;
		float increaseYMax;

		float decreaseXMin;
		float decreaseXMax;
		float decreaseYMin;
		float decreaseYMax;

		int currentBrushSize;

		float clearXMin;
		float clearXMax;
		float clearYMin;
		float clearYMax;

		float finishXMin;
		float finishXMax;
		float finishYMin;
		float finishYMax;

		float saveXMin;
		float saveXMax;
		float saveYMin;
		float saveYMax;

		float notSaveXMin;
		float notSaveXMax;
		float notSaveYMin;
		float notSaveYMax;

		float timeBeforeClick;
		float timeBetweenClicks = 1;

		// Size boundaries for brush
		int minBrushSize = 10;
		int maxBrushSize = 64;
		Vector2 filteredPoint;
		MobilePaint mobilePaint;

		// Start is called before the first frame update
		void Start() {
			mobilePaint = PaintManager.mobilePaint; // Gets reference to mobilePaint through PaintManager

			// Sets the Default Brush Size
			currentBrushSize = 20;
			mobilePaint.SetBrushSize(currentBrushSize);

			timeBeforeClick = timeBetweenClicks;

			isEyeTracker = PlayerPrefs.GetInt("EyeTracking") == 1 ? true : false;
		}

		private void Update() {

			if (isEyeTracker) {
				camRect = mainCam.pixelRect;
				timeBeforeClick -= Time.deltaTime;
				//Only click buttons if spacebar is down
				if (Input.GetKey("space")) {

					//Find position and size in x and y directions of the buttons
					increasePos = increaseSizeButton.transform.position;
					decreasePos = decreaseSizeButton.transform.position;
					clearPos = clearImageButton.transform.position;
					finishPos = finishButton.transform.position;
					savePos = saveButton.transform.position;
					notSavePos = notSaveButton.transform.position;

					increaseRect = increaseSizeButton.GetComponent<RectTransform>().rect;
					decreaseRect = decreaseSizeButton.GetComponent<RectTransform>().rect;
					clearRect = clearImageButton.GetComponent<RectTransform>().rect;
					finishRect = finishButton.GetComponent<RectTransform>().rect;
					saveRect = saveButton.GetComponent<RectTransform>().rect;
					notSaveRect = notSaveButton.GetComponent<RectTransform>().rect;

					increaseXMin = increaseRect.xMin;
					increaseXMax = increaseRect.xMax;
					increaseYMin = increaseRect.yMin;
					increaseYMax = increaseRect.yMax;

					decreaseXMin = decreaseRect.xMin;
					decreaseXMax = decreaseRect.xMax;
					decreaseYMin = decreaseRect.yMin;
					decreaseYMax = decreaseRect.yMax;

					clearXMin = clearRect.xMin;
					clearXMax = clearRect.xMax;
					clearYMin = clearRect.yMin;
					clearYMax = clearRect.yMax;

					finishXMin = finishRect.xMin;
					finishXMax = finishRect.xMax;
					finishYMin = finishRect.yMin;
					finishYMax = finishRect.yMax;

					saveXMin = saveRect.xMin;
					saveXMax = saveRect.xMax;
					saveYMin = saveRect.yMin;
					saveYMax = saveRect.yMax;

					notSaveXMin = notSaveRect.xMin;
					notSaveXMax = notSaveRect.xMax;
					notSaveYMin = notSaveRect.yMin;
					notSaveYMax = notSaveRect.yMax;

					Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
					gazePoint.x *= camRect.width;
					gazePoint.y *= camRect.height;
					filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);
					//Find if buttons are active and whether the eye is looking at them and space is down, do button code.
					if (paintCanvas.activeInHierarchy) {
						if ((increasePos.x + increaseXMin) < filteredPoint.x && filteredPoint.x < (increasePos.x + increaseXMax) && (increasePos.y + increaseYMin) < filteredPoint.y && filteredPoint.y < (increasePos.y + increaseYMax) && timeBeforeClick <= 0) {
							brushSize.GetComponent<BrushSizeScript>().IncreaseBrushSize();
							timeBeforeClick = timeBetweenClicks;
						}

						if ((decreasePos.x + decreaseXMin) < filteredPoint.x && filteredPoint.x < (decreasePos.x + decreaseXMax) && (decreasePos.y + decreaseYMin) < filteredPoint.y && filteredPoint.y < (decreasePos.y + decreaseYMax) && timeBeforeClick <= 0) {
							brushSize.GetComponent<BrushSizeScript>().DecreaseBrushSize();
							timeBeforeClick = timeBetweenClicks;
						}

						if ((clearPos.x + clearXMin) < filteredPoint.x && filteredPoint.x < (clearPos.x + clearXMax) && (clearPos.y + clearYMin) < filteredPoint.y && filteredPoint.y < (clearPos.y + clearYMax) && timeBeforeClick <= 0) {
							mobilePaint.ClearImage();
							timeBeforeClick = timeBetweenClicks;
						}

						if ((finishPos.x + finishXMin) < filteredPoint.x && filteredPoint.x < (finishPos.x + finishXMax) && (finishPos.y + finishYMin) < filteredPoint.y && filteredPoint.y < (finishPos.y + finishYMax) && timeBeforeClick <= 0) {
							paintCanvas.SetActive(false);
							optionsCanvas.SetActive(true);
							timeBeforeClick = timeBetweenClicks;
						}
					}
					if (optionsCanvas.activeInHierarchy) {
						if ((savePos.x + saveXMin) < filteredPoint.x && filteredPoint.x < (savePos.x + saveXMax) && (savePos.y + saveYMin) < filteredPoint.y && filteredPoint.y < (savePos.y + saveYMax) && timeBeforeClick <= 0) {
							saveButton.GetComponent<SaveImageScript>().Save();
							cameraShutterClose.SetActive(true);
							timeBeforeClick = timeBetweenClicks;
						}
						if ((notSavePos.x + notSaveXMin) < filteredPoint.x && filteredPoint.x < (notSavePos.x + notSaveXMax) && (notSavePos.y + notSaveYMin) < filteredPoint.y && filteredPoint.y < (notSavePos.y + notSaveYMax) && timeBeforeClick <= 0) {
							notSaveButton.GetComponent<BackToStreetScript>().DontSave();
							cameraShutterClose.SetActive(true);
							timeBeforeClick = timeBetweenClicks;
						}
					}
				}
			}
		}

		// Changes the size of the brush by 5
		public void IncreaseBrushSize() {
			// If the new size is bigger than the maxBrushSize, it won't change size
			if (currentBrushSize <= maxBrushSize - 5) {
				currentBrushSize += 5;
				mobilePaint.SetBrushSize(currentBrushSize);
			}
		}

		// Changes the size of the brush by -5
		public void DecreaseBrushSize() {
			// If the new size is smaller than the minBrushSize, it won't change size
			if (currentBrushSize >= minBrushSize + 5) {
				currentBrushSize -= 5;
				mobilePaint.SetBrushSize(currentBrushSize);
			}
		}
	}
}