using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class GazeTutorialTest : MonoBehaviour
{

    public GameObject isEyeTracker;
    public GameObject tobiiTime;
    public Button yesButton;

    public Camera mainCam;

    Rect camRect;

    public GameObject tutorialManager;


    Vector3 yesPos;

    Rect yesRect;
    float yesXMin;
    float yesXMax;
    float yesYMin;
    float yesYMax;

    Vector2 filteredPoint;
    GameObject objHit;

    public GameObject canvasButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker)
        {
            camRect = mainCam.pixelRect;
            //Only click buttons if spacebar is down
            if (Input.GetKey("space"))
            {
                yesPos = yesButton.transform.position;
                yesRect = yesButton.GetComponent<RectTransform>().rect;

                yesXMin = yesRect.xMin;
                yesXMax = yesRect.xMax;
                yesYMin = yesRect.yMin;
                yesYMax = yesRect.yMax;

                Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                gazePoint.x *= camRect.width;
                gazePoint.y *= camRect.height;
                filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

                if ((yesPos.x + yesXMin) < filteredPoint.x && filteredPoint.x < (yesPos.x + yesXMax) && (yesPos.y + yesYMin) < filteredPoint.y && filteredPoint.y < (yesPos.y + yesYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0 && yesButton.IsActive())
                {
                    tutorialManager.GetComponent<TutorialManager>().YesButtonClick();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }
                Ray ray = mainCam.ScreenPointToRay(filteredPoint);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    objHit = hit.transform.gameObject;
                }

                if (tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    if (GameObject.ReferenceEquals(objHit, canvasButton))
                    {
                        tutorialManager.GetComponent<TutorialManager>().CanvasButtonClick();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                }
            }
        }
    }
}
