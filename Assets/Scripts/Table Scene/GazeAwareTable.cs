using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class GazeAwareTable : MonoBehaviour
{
    public GameObject isEyeTracker;
    public GameObject tobiiTime;
    public GameObject Gallery;
    public GameObject escOptions;
    public Button yesButton;
    public Button noButton;
    public GameObject tableController;

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

    GameObject objHit;
    public GameObject transitionController;

    public Camera mainCam;
    Rect camRect;

    Vector2 filteredPointViewport;
    Vector2 filteredPointScreen;

    // Update is called once per frame
    void Update()
    {
        if(isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker && !escOptions.activeInHierarchy)
        {
            //Only click buttons if spacebar is down
            if (Input.GetKey("space"))
            {
                camRect = mainCam.pixelRect;
                //Find position and size in x and y directions of the buttons
                yesPos = yesButton.transform.position;
                noPos = noButton.transform.position;
                yesRect = yesButton.GetComponent<RectTransform>().rect;
                noRect = noButton.GetComponent<RectTransform>().rect;

                yesXMin = yesRect.xMin;
                yesXMax = yesRect.xMax;
                yesYMin = yesRect.yMin;
                yesYMax = yesRect.yMax;

                noXMin = noRect.xMin;
                noXMax = noRect.xMax;
                noYMin = noRect.yMin;
                noYMax = noRect.yMax;

                Vector2 gazePointViewport = TobiiAPI.GetGazePoint().Viewport;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                gazePointViewport.x *= camRect.width;
                gazePointViewport.y *= camRect.height;
                filteredPointViewport = Vector2.Lerp(filteredPointViewport, gazePointViewport, 0.5f);

                if ((yesPos.x + yesXMin) < filteredPointViewport.x && filteredPointViewport.x < (yesPos.x + yesXMax) && (yesPos.y + yesYMin) < filteredPointViewport.y && filteredPointViewport.y < (yesPos.y + yesYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    tableController.GetComponent<TableControllerScript>().TickButton();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }

                if ((noPos.x + noXMin) < filteredPointViewport.x && filteredPointViewport.x < (noPos.x + noXMax) && (noPos.y + noYMin) < filteredPointViewport.y && filteredPointViewport.y < (noPos.y + noYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    tableController.GetComponent<TableControllerScript>().ChangeTables();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }

                Vector2 gazePointScreen = TobiiAPI.GetGazePoint().Screen;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                filteredPointScreen = Vector2.Lerp(filteredPointScreen, gazePointScreen, 0.5f);

                Ray ray = mainCam.ScreenPointToRay(filteredPointViewport);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    objHit = hit.transform.gameObject;
                }

                if (tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    if (GameObject.ReferenceEquals(objHit, Gallery))
                    {
                        transitionController.SetActive(true);
                        Gallery.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                }
            }
        }
    }
}