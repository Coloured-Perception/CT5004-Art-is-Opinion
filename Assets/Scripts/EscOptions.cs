using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class EscOptions : MonoBehaviour
{
    public GameObject escOptionsPanel;
    public GameObject returnButton;
    public GameObject tobiiButton;
    public GameObject mouseButton;
    public GameObject quitButton;

    public Camera mainCam;
    Rect camRect;

    Vector3 returnPos;
    Vector3 tobiiPos;
    Vector3 mousePos;
    Vector3 quitPos;

    Rect returnRect;
    Rect tobiiRect;
    Rect mouseRect;
    Rect quitRect;

    float returnXMin;
    float returnXMax;
    float returnYMin;
    float returnYMax;

    float tobiiXMin;
    float tobiiXMax;
    float tobiiYMin;
    float tobiiYMax;

    float mouseXMin;
    float mouseXMax;
    float mouseYMin;
    float mouseYMax;

    float quitXMin;
    float quitXMax;
    float quitYMin;
    float quitYMax;

    Vector2 filteredPoint;

    public GameObject isEyeTracker;
    public GameObject tobiiTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escOptionsPanel.SetActive(!escOptionsPanel.activeInHierarchy);
        }

        if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker && escOptionsPanel.activeInHierarchy)
        {
            camRect = mainCam.pixelRect;
            
            //Only click buttons if spacebar is down
            if (Input.GetKey("space"))
            {

                //Find position and size in x and y directions of the buttons
                returnPos = returnButton.transform.position;
                tobiiPos = tobiiButton.transform.position;
                mousePos = mouseButton.transform.position;
                quitPos = quitButton.transform.position;

                returnRect = returnButton.GetComponent<RectTransform>().rect;
                tobiiRect =tobiiButton.GetComponent<RectTransform>().rect;
                mouseRect = mouseButton.GetComponent<RectTransform>().rect;
                quitRect = quitButton.GetComponent<RectTransform>().rect;

                returnXMin = returnRect.xMin;
                returnXMax = returnRect.xMax;
                returnYMin = returnRect.yMin;
                returnYMax = returnRect.yMax;

                tobiiXMin = tobiiRect.xMin;
                tobiiXMax = tobiiRect.xMax;
                tobiiYMin = tobiiRect.yMin;
                tobiiYMax = tobiiRect.yMax;

                mouseXMin = mouseRect.xMin;
                mouseXMax = mouseRect.xMax;
                mouseYMin = mouseRect.yMin;
                mouseYMax = mouseRect.yMax;

                quitXMin = quitRect.xMin;
                quitXMax = quitRect.xMax;
                quitYMin = quitRect.yMin;
                quitYMax = quitRect.yMax;

                Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;
                //gazePoint.x *= camRect.width;
                //gazePoint.y *= camRect.height;// Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);
                //Find if buttons are active and whether the eye is looking at them and space is down, do button code.

                if ((returnPos.x + returnXMin) < filteredPoint.x && filteredPoint.x < (returnPos.x + returnXMax) && (returnPos.y + returnYMin) < filteredPoint.y && filteredPoint.y < (returnPos.y + returnYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    Return();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }

                if ((tobiiPos.x + tobiiXMin) < filteredPoint.x && filteredPoint.x < (tobiiPos.x + tobiiXMax) && (tobiiPos.y + tobiiYMin) < filteredPoint.y && filteredPoint.y < (tobiiPos.y + tobiiYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    UseTobii();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }

                if ((mousePos.x + mouseXMin) < filteredPoint.x && filteredPoint.x < (mousePos.x + mouseXMax) && (mousePos.y + mouseYMin) < filteredPoint.y && filteredPoint.y < (mousePos.y + mouseYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    UseMouse();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }

                if ((quitPos.x + quitXMin) < filteredPoint.x && filteredPoint.x < (quitPos.x + quitXMax) && (quitPos.y + quitYMin) < filteredPoint.y && filteredPoint.y < (quitPos.y + quitYMax) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    Quit();
                    tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                }
            }
        }
    }

    public void Return()
    {
        escOptionsPanel.SetActive(false);
    }

    public void UseTobii()
    {
        PlayerPrefs.SetInt("isEyeTracker", 1);
    }

    public void UseMouse()
    {
        PlayerPrefs.SetInt("isEyeTracker", 0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
