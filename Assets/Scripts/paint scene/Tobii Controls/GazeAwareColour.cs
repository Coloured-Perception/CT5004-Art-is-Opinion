using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Tobii.Gaming;

namespace unitycoder_MobilePaint
{
        public class GazeAwareColour : MonoBehaviour {

        MobilePaint mobilePaint;
        public GameObject preview;
        public GameObject tobiiTime;
        public GameObject isEyeTracker;
        public GameObject escOptions;

        public Button[] colorpickers;
        Vector3[] Positions;
        Rect[] rects;
        float[] XMin;
        float[] XMax;
        float[] YMin;
        float[] YMax;

	    Vector2 filteredPoint;

        void Awake()
        {
            mobilePaint = PaintManager.mobilePaint;

            if (mobilePaint == null) Debug.LogError("No MobilePaint assigned at " + transform.name, gameObject);
            if (colorpickers.Length < 1) Debug.LogWarning("No colorpickers assigned at " + transform.name, gameObject);
        }

        private void Start()
        {
            Positions = new Vector3[colorpickers.Length];
            rects = new Rect[colorpickers.Length];
            XMin = new float[colorpickers.Length];
            XMax = new float[colorpickers.Length];
            YMin = new float[colorpickers.Length];
            YMax = new float[colorpickers.Length];

        }
        // Update is called once per frame
        void Update() {

            if(isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker && !escOptions.activeInHierarchy)
            {
                for (int i = 0; i < colorpickers.Length; i++)
                {
                    Positions[i] = colorpickers[i].transform.position;
                    rects[i] = colorpickers[i].GetComponent<RectTransform>().rect;
                    XMin[i] = rects[i].xMin;
                    XMax[i] = rects[i].xMax;
                    YMin[i] = rects[i].yMin;
                    YMax[i] = rects[i].yMax;
                }


                //Only click if spacebar is down
                if (Input.GetKey("space")) {
                    

			        Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
			        filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

                    for (int i = 0; i < colorpickers.Length; i++)
                    {
                        if ((Positions[i].x + XMin[i]) < filteredPoint.x && filteredPoint.x < (Positions[i].x + XMax[i]) && (Positions[i].y + YMin[i]) < filteredPoint.y && filteredPoint.y < (Positions[i].y + YMax[i]) && tobiiTime.GetComponent<TobiiTime>().timeBeforeClick <= 0 && colorpickers[i].IsActive())
                        {
                            Color newColor = colorpickers[i].gameObject.GetComponent<Image>().color;

                            preview.gameObject.GetComponent<RawImage>().color = newColor; // set current color image

                            // send new color
                            mobilePaint.SetPaintColor(newColor);
                            tobiiTime.GetComponent<TobiiTime>().timeBeforeClick = tobiiTime.GetComponent<TobiiTime>().timeBetweenClicks;
                        }
                    }

                }
            }
	    }
    }
}