using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class GazeTutorialStill : MonoBehaviour
{
    public GameObject buttonTop;
    GameObject objHit;
    public GameObject isEyeTracker;
    public GameObject tobiiTime;
    public Camera mainCam;

    public GameObject tutorialManager;

    Vector2 filteredPoint;

    Rect camRect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker)
        {
            camRect = mainCam.pixelRect;
            Vector2 gazePointViewport = TobiiAPI.GetGazePoint().Viewport;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
            gazePointViewport.x *= camRect.width;
            gazePointViewport.y *= camRect.height;

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
