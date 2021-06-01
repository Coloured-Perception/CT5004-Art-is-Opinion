using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeAwareStillLife : MonoBehaviour
{

    public GameObject buttonTop;
    GameObject objHit;
    public GameObject isEyeTracker;
    public GameObject tobiiTime;
    public GameObject escOptions;

    public Camera mainCam;
    Vector2 filteredPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker && !escOptions.activeInHierarchy)
        {
            if (Input.GetKey("space"))
            {
                Ray ray = mainCam.ScreenPointToRay(filteredPoint);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    objHit = hit.transform.gameObject;
                }

                if (tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    if (GameObject.ReferenceEquals(objHit, buttonTop))
                    {
                        mainCam.GetComponent<LandscapeCameraScript>().ButtonPressed();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                }
            }
        }
    }
}
