using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class GazeAwareTutorial : MonoBehaviour
{
    public GameObject isEyeTracker;
    public GameObject tobiiTime;
    public Button nextButton;
    public Button yesButton;
    public Button noButton;

    public Camera mainCam;

    Rect camRect;

    public GameObject tutorialManager;

    Vector3 nextPos;
    Vector3 yesPos;
    Vector3 noPos;

    Rect nextRect;
    Rect yesRect;
    Rect noRect;

    float nextXMin;
    float nextXMax;
    float nextYMin;
    float nextYMax;

    float yesXMin;
    float yesXMax;
    float yesYMin;
    float yesYMax;

    float noXMin;
    float noXMax;
    float noYMin;
    float noYMax;

    Vector2 filteredPoint;

    // Update is called once per frame
    void Update()
    {
        if(isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker)
        {
            camRect = mainCam.pixelRect;
            //Only click buttons if spacebar is down
            if (Input.GetKey("space"))
            {
                nextPos = nextButton.transform.position;
                yesPos = yesButton.transform.position;
                noPos = noButton.transform.position;

                nextRect = nextButton.GetComponent<RectTransform>().rect;
                yesRect = yesButton.GetComponent<RectTransform>().rect;
                noRect = noButton.GetComponent<RectTransform>().rect;

                nextXMin = nextRect.xMin;
                nextXMax = nextRect.xMax;
                nextYMin = nextRect.yMin;
                nextYMax = nextRect.yMax;

                yesXMin = yesRect.xMin;
                yesXMax = yesRect.xMax;
                yesYMin = yesRect.yMin;
                yesYMax = yesRect.yMax;

                noXMin = noRect.xMin;
                noXMax = noRect.xMax;
                noYMin = noRect.yMin;
                noYMax = noRect.yMax;

                Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                gazePoint.x *= camRect.width;
                gazePoint.y *= camRect.height;
                filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

                if ((nextPos.x + nextXMin) < filteredPoint.x && filteredPoint.x < (nextPos.x + nextXMax) && (nextPos.y + nextYMin) < filteredPoint.y && filteredPoint.y < (nextPos.y + nextYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0 && nextButton.IsActive())
                {
                    tutorialManager.GetComponent<TutorialManager>().NextButtonClicked();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }

                if ((yesPos.x + yesXMin) < filteredPoint.x && filteredPoint.x < (yesPos.x + yesXMax) && (yesPos.y + yesYMin) < filteredPoint.y && filteredPoint.y < (yesPos.y + yesYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0 && yesButton.IsActive())
                {
                    tutorialManager.GetComponent<TutorialManager>().YesButtonClick();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }     
           
                if ((noPos.x + noXMin) < filteredPoint.x && filteredPoint.x < (noPos.x + noXMax) && (noPos.y + noYMin) < filteredPoint.y && filteredPoint.y < (noPos.y + noYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0 && noButton.IsActive())
                {
                    tutorialManager.GetComponent<TutorialManager>().NoButtonClick();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }
            }
        }

    }
}
