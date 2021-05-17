using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Created by Mobile Paint		Edited by Coral and Matt Pe
/// </summary>
namespace unitycoder_MobilePaint
{
    /// <summary>
    /// This class controls the custom brush UI
    /// </summary>
    public class CustomBrushesUI : MonoBehaviour
    {
        public MobilePaint mobilePaint;
        public Button buttonTemplate;
        public BrushSizeScript brushSizeScript;
        public GameObject panel;
        Button[] newButton;
        public GameObject tobiiTime;
        Vector3[] Positions;
        Rect[] rects;
        float[] XMin;
        float[] XMax;
        float[] YMin;
        float[] YMax;
        GameObject[] children;

        public GameObject isEyeTracker;
        public Camera mainCam;
        Rect camRect;

        int brushSizeLast = 100;    // any number that cant actually be the size 

        Vector2 filteredPoint;

        [SerializeField] private int padding = 8;

        public GameObject brushPreview;
        public int sizeReference = 3;

        // Make scale of dots texture look right
        int previousIndex = 2;


        void Start()
        {
            newButton = new Button[mobilePaint.customBrushes.Length];
            if (mobilePaint == null) { Debug.LogError("No MobilePaint assigned at " + transform.name); }
            if (buttonTemplate == null) { Debug.LogError("No buttonTemplate assigned at " + transform.name); }
        }

        private void Update()
        {
            // Coral
            // this tests whether the brush has changed size and so new custom brushes are needed
            // the brushes needed are decided by the for loop and then instantiated in the right position
            if (brushSizeLast != brushSizeScript.customSize)
            {

                for (int i = transform.childCount - 1; i >= 0; --i)
                {
                    var child = transform.GetChild(i).gameObject;
                    Destroy(child);
                }

                Vector2 newPos = new Vector2(padding, -padding * 4);
                for (int i = brushSizeScript.customSize; i < mobilePaint.customBrushes.Length; i = i + 6)
                {

                    Quaternion rot = Quaternion.Euler(0, 0, 90);
                    newButton[i] = Instantiate(buttonTemplate, Vector3.zero, rot) as Button;
                    newButton[i].transform.SetParent(transform, false);
                    RectTransform rectTrans = newButton[i].GetComponent<RectTransform>();
                    //Coral

                    // wrap inside panel width
                    if (newPos.x + rectTrans.rect.width >= GetComponent<RectTransform>().rect.width)
                    {
                        newPos.x = 0 + padding;
                        newPos.y -= rectTrans.rect.height + padding;
                        // NOTE: maximum Y is not checked..so don't put too many custom brushes.. would need to add paging or scrolling
                    }
                    rectTrans.anchoredPosition = newPos;
                    newPos.x += rectTrans.rect.width + padding;

                    // assign brush image
                    // NOTE: have to use RawImage, instead of image (because cannot cast Texture2D into Image)
                    // I've read that RawImage causes extra drawcall per drawimage, thats not nice if there are tens of images..
                    newButton[i].GetComponent<RawImage>().texture = mobilePaint.customBrushes[i];
                    var index = i;

                    // Fix dots image to look correct and be in correct position
                    if (i >= 30 && i <= 35)
                    {
                        newButton[i].transform.localScale = new Vector3(newButton[i].transform.localScale.x / 5, newButton[i].transform.localScale.y, newButton[i].transform.localScale.z);
                        newButton[i].transform.position = new Vector3(newButton[i].transform.position.x, newButton[i].transform.position.y + 25, newButton[i].transform.position.z);
                    }

                    // event listener for button clicks, pass custom brush array index number as parameter
                    newButton[i].onClick.AddListener(delegate { this.SetCustomBrush(index); });
                }
                brushSizeLast = brushSizeScript.customSize;

                Positions = new Vector3[newButton.Length];
                rects = new Rect[newButton.Length];
                XMin = new float[newButton.Length];
                XMax = new float[newButton.Length];
                YMin = new float[newButton.Length];
                YMax = new float[newButton.Length];
            }

            if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker)
            {
                camRect = mainCam.pixelRect;

                Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;   // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                gazePoint.x *= camRect.width;
                gazePoint.y *= camRect.height;
                filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

                int loopPos = 0;
                foreach (Button brush in newButton)
                {
                    if (brush)
                    {
                        //MattP
                        Positions[loopPos] = newButton[loopPos].transform.position;
                        rects[loopPos] = newButton[loopPos].GetComponent<RectTransform>().rect;
                        XMin[loopPos] = rects[loopPos].xMin;
                        XMax[loopPos] = rects[loopPos].xMax;
                        YMin[loopPos] = rects[loopPos].yMin;
                        YMax[loopPos] = rects[loopPos].yMax;

                        if (Input.GetKey("space"))
                        {
                            if ((Positions[loopPos].x + XMin[loopPos] - (padding/2)) < filteredPoint.x && filteredPoint.x < (Positions[loopPos].x + XMax[loopPos] + (padding / 2)) && (Positions[loopPos].y + YMin[loopPos] - (padding / 2)) < filteredPoint.y && filteredPoint.y < (Positions[loopPos].y + YMax[loopPos] + (padding / 2)) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0)
                            {
                                SetCustomBrush(loopPos);
                                tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                                mobilePaint.textureNeedsUpdate = true;
                                break;
                            }
                        }
                    }
                    loopPos += 1;
                }
            }
        }

        //Coral

        /// <summary>
        /// Send current brush index to MobilePaint
        /// </summary>
        /// <param name="index"></param>
        public void SetCustomBrush(int index)
        {
            mobilePaint.selectedBrush = index;
            mobilePaint.ReadCurrentCustomBrush();   // tell mobile paint to read custom brush pixel data
            mobilePaint.SetDrawModeShapes();

            //CloseCustomBrushPanel();
            gameObject.SetActive(false);

            sizeReference = index;

            brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[index];

            // Dots texture is not square, so scale of the preview needs to be changed when going to or from it
            if ((previousIndex < 30 || previousIndex > 35) && index >= 30 && index <= 35)
                brushPreview.transform.localScale = new Vector3(brushPreview.transform.localScale.x / 5, brushPreview.transform.localScale.y, brushPreview.transform.localScale.z);

            if ((index < 30 || index > 35) && previousIndex >= 30 && previousIndex <= 35)
                brushPreview.transform.localScale = new Vector3(brushPreview.transform.localScale.x * 5, brushPreview.transform.localScale.y, brushPreview.transform.localScale.z);

            previousIndex = index;
        }

        //public void CloseCustomBrushPanel()
        //{
        //	//if (transform.childCount > 0)
        //	//{
        //	//	GameObject.Destroy(transform.GetChild(0));
        //	//}
        //	gameObject.SetActive(false);
        //}

        //public void OpenCustomBrushPanel()
        //{
        //	gameObject.SetActive(true);
        //}

        //Coral
    }
}