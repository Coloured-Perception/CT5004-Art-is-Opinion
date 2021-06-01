using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

namespace unitycoder_MobilePaint
{
    public class GazeTutorialStill : MonoBehaviour
    {
        public GameObject buttonTop;
        GameObject objHit;
        public GameObject isEyeTracker;
        public GameObject tobiiTime;
        public GameObject escOptions;
        public GameObject brushSize;
        public GameObject transitionController;
        public Button increaseSizeButton;
        public Button decreaseSizeButton;
        public Button clearImageButton;
        public Button finishButton;
        public GameObject customBrushes;
        public GameObject customBrushesPanel;
        //public Camera saveCamera;
        public Camera mainCam;

        public GameObject tutorialManager;

        Vector3 increasePos;
        Vector3 decreasePos;
        Vector3 clearPos;
        Vector3 finishPos;
        Vector3 customBrushPos;

        Rect increaseRect;
        Rect decreaseRect;
        Rect clearRect;
        Rect finishRect;
        Rect customBrushRect;

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

        float customBrushXMin;
        float customBrushXMax;
        float customBrushYMin;
        float customBrushYMax;

        // Size boundaries for brush
        int minBrushSize = 10;
        int maxBrushSize = 64;
        Vector2 filteredPoint;
        MobilePaint mobilePaint;

        Rect camRect;
        // Start is called before the first frame update
        void Start()
        {
            mobilePaint = PaintManager.mobilePaint; // Gets reference to mobilePaint through PaintManager

            // Sets the Default Brush Size
            currentBrushSize = 20;
            mobilePaint.SetBrushSize(currentBrushSize);
        }

        // Update is called once per frame
        void Update()
        {
            if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker && !escOptions.activeInHierarchy)
            {
                if (Input.GetKey("space"))
                {
                    camRect = mainCam.pixelRect;
                    increasePos = increaseSizeButton.transform.position;
                    decreasePos = decreaseSizeButton.transform.position;
                    clearPos = clearImageButton.transform.position;
                    finishPos = finishButton.transform.position;
                    customBrushPos = customBrushes.transform.position;

                    increaseRect = increaseSizeButton.GetComponent<RectTransform>().rect;
                    decreaseRect = decreaseSizeButton.GetComponent<RectTransform>().rect;
                    clearRect = clearImageButton.GetComponent<RectTransform>().rect;
                    finishRect = finishButton.GetComponent<RectTransform>().rect;
                    customBrushRect = customBrushes.GetComponent<RectTransform>().rect;

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

                    customBrushXMin = customBrushRect.xMin;
                    customBrushXMax = customBrushRect.xMax;
                    customBrushYMin = customBrushRect.yMin;
                    customBrushYMax = customBrushRect.yMax;
                    Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                    gazePoint.x *= camRect.width;
                    gazePoint.y *= camRect.height;
                    filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

                    if ((increasePos.x + increaseXMin) < filteredPoint.x && filteredPoint.x < (increasePos.x + increaseXMax) && (increasePos.y + increaseYMin) < filteredPoint.y && filteredPoint.y < (increasePos.y + increaseYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                    {
                        brushSize.GetComponent<BrushSizeScript>().IncreaseBrushSize();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }

                    if ((decreasePos.x + decreaseXMin) < filteredPoint.x && filteredPoint.x < (decreasePos.x + decreaseXMax) && (decreasePos.y + decreaseYMin) < filteredPoint.y && filteredPoint.y < (decreasePos.y + decreaseYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                    {
                        brushSize.GetComponent<BrushSizeScript>().DecreaseBrushSize();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }

                    if ((clearPos.x + clearXMin) < filteredPoint.x && filteredPoint.x < (clearPos.x + clearXMax) && (clearPos.y + clearYMin) < filteredPoint.y && filteredPoint.y < (clearPos.y + clearYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                    {
                        mobilePaint.ClearImage();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }

                    if ((finishPos.x + finishXMin) < filteredPoint.x && filteredPoint.x < (finishPos.x + finishXMax) && (finishPos.y + finishYMin) < filteredPoint.y && filteredPoint.y < (finishPos.y + finishYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                    {
                        tutorialManager.GetComponent<TutorialManager>().FinishedButtonClick();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }

                    if ((customBrushPos.x + customBrushXMin) < filteredPoint.x && filteredPoint.x < (customBrushPos.x + customBrushXMax) && (customBrushPos.y + customBrushYMin) < filteredPoint.y && filteredPoint.y < (customBrushPos.y + customBrushYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                    {
                        if (customBrushesPanel.activeInHierarchy)
                        {
                            customBrushesPanel.SetActive(false);
                        }
                        else
                        {
                            customBrushesPanel.SetActive(true);
                        }
                        tutorialManager.GetComponent<TutorialManager>().ColourPrieviewClick();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }

                    Ray ray = mainCam.ScreenPointToRay(filteredPoint);
                    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                    {
                        objHit = hit.transform.gameObject;
                    }

                    if (tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                    {
                        if (GameObject.ReferenceEquals(objHit, buttonTop))
                        {
                            tutorialManager.GetComponent<TutorialManager>().TopButtonClick();
                            mainCam.GetComponent<LandscapeCameraScript>().ButtonPressed();
                            tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                        }
                    }
                }
            }
        }
    }
}
