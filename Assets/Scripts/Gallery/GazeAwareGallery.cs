using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class GazeAwareGallery : MonoBehaviour
{
    public GameObject EntranceButton;
    public GameObject TableButton;
    public GameObject MagazineButton;
    public GameObject StillLifeButton;
    public GameObject PortraitButton;
    public GameObject TutorialButton;
    //public GameObject OptionsButton;
    public GameObject ShopButton;
    public GameObject EtoSLButton;
    public GameObject SLtoEButton;
    public GameObject EtoRButton;
    public GameObject RtoEButton;
    public GameObject RtoOButton;
    public GameObject OtoRButton;
    public GameObject SLtoPButton;
    public GameObject PtoSLButton;
    public GameObject EtoFPButton;
    public GameObject FPtoEButton;
    public GameObject FLtoRButton;
    public GameObject RtoFLButton;
    public GameObject FLtoFPButton;
    public GameObject FPtoFLButton;

    GameObject objHit;
    public GameObject transitionController;

    public Camera mainCam;
    //Rect camRect;

    Vector2 filteredPoint;

    public GameObject isEyeTracker;
    public GameObject tobiiTime;
    public GameObject escOptions;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker && !escOptions.activeInHierarchy)
        {
            if (Input.GetKey("space"))
            {
                //camRect = mainCam.pixelRect;

                Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                //gazePoint.x *= camRect.width;
                //gazePoint.y *= camRect.height;
                filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

                Ray ray = mainCam.ScreenPointToRay(filteredPoint);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    objHit = hit.transform.gameObject;
                }

                if (tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                {
                    if (GameObject.ReferenceEquals(objHit, EntranceButton))
                    {
                        transitionController.SetActive(true);
                        EntranceButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, TableButton))
                    {
                        transitionController.SetActive(true);
                        TableButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, MagazineButton))
                    {
                        transitionController.SetActive(true);
                        MagazineButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, StillLifeButton))
                    {
                        transitionController.SetActive(true);
                        StillLifeButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, PortraitButton))
                    {
                        transitionController.SetActive(true);
                        PortraitButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, TutorialButton))
                    {
                        transitionController.SetActive(true);
                        TutorialButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    //else if (GameObject.ReferenceEquals(objHit, OptionsButton))
                    //{
                    //    transitionController.SetActive(true);
                    //    OptionsButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    //    transitionController.GetComponent<Animator>().Play("BlinkClose");
                    //    timeBeforeClick = timeBetweenClicks;
                    //}
                    else if (GameObject.ReferenceEquals(objHit, ShopButton))
                    {
                        transitionController.SetActive(true);
                        ShopButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                        transitionController.GetComponent<Animator>().Play("BlinkClose");
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, EtoSLButton))
                    {
                        EtoSLButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, SLtoEButton))
                    {
                        SLtoEButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, EtoRButton))
                    {
                        EtoRButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, RtoEButton))
                    {
                        RtoEButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, RtoOButton))
                    {
                        RtoOButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, OtoRButton))
                    {
                        OtoRButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, SLtoPButton))
                    {
                        SLtoPButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, PtoSLButton))
                    {
                        PtoSLButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, EtoFPButton))
                    {
                        EtoFPButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, FPtoEButton))
                    {
                        FPtoEButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, FLtoRButton))
                    {
                        FLtoRButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, RtoFLButton))
                    {
                        RtoFLButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, FLtoFPButton))
                    {
                        FLtoFPButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                    else if (GameObject.ReferenceEquals(objHit, FPtoFLButton))
                    {
                        FPtoFLButton.GetComponent<NavigationCanvasScript>().Move();
                        tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                    }
                }
            }
        }
    }
}
