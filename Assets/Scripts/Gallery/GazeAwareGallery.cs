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
    public GameObject OptionsButton;
    public GameObject ShopButton;
    public GameObject EtoSLButton;
    public GameObject SLtoEButton;
    public GameObject EtoRButton;
    public GameObject RtoEButton;

    GameObject objHit;
    public GameObject transitionController;

    public Camera mainCam;
    Rect camRect;

    float timeBeforeClick;
    float timeBetweenClicks = 1;

    Vector2 filteredPoint;

    // Start is called before the first frame update
    void Start()
    {
        timeBeforeClick = timeBetweenClicks;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            timeBetweenClicks -= Time.deltaTime;
            camRect = mainCam.pixelRect;
           
            Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
            gazePoint.x *= camRect.width;
            gazePoint.y *= camRect.height;
            filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

            Ray ray = mainCam.ViewportPointToRay(filteredPoint);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                objHit = hit.transform.gameObject;
            }

            if (timeBetweenClicks <= 0)
            {
                if (GameObject.ReferenceEquals(objHit, EntranceButton))
                {
                    transitionController.SetActive(true);
                    EntranceButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, TableButton))
                {
                    transitionController.SetActive(true);
                    TableButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, MagazineButton))
                {
                    transitionController.SetActive(true);
                    MagazineButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, StillLifeButton))
                {
                    transitionController.SetActive(true);
                    StillLifeButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, PortraitButton))
                {
                    transitionController.SetActive(true);
                    PortraitButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, TutorialButton))
                {
                    transitionController.SetActive(true);
                    TutorialButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, OptionsButton))
                {
                    transitionController.SetActive(true);
                    OptionsButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, ShopButton))
                {
                    transitionController.SetActive(true);
                    ShopButton.GetComponent<NavigationCanvasScript>().ChangeScene();
                    transitionController.GetComponent<Animator>().Play("Blink Close");
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, EtoSLButton))
                {
                    EtoSLButton.GetComponent<NavigationCanvasScript>().Move();
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, SLtoEButton))
                {
                    SLtoEButton.GetComponent<NavigationCanvasScript>().Move();
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, EtoRButton))
                {
                    EtoRButton.GetComponent<NavigationCanvasScript>().Move();
                    timeBeforeClick = timeBetweenClicks;
                }
                else if (GameObject.ReferenceEquals(objHit, RtoEButton))
                {
                    RtoEButton.GetComponent<NavigationCanvasScript>().Move();
                    timeBeforeClick = timeBetweenClicks;
                }
            }
        }
    }
}
