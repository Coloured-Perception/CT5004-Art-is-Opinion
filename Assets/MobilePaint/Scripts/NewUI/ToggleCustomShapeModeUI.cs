using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tobii.Gaming;

namespace unitycoder_MobilePaint
{
	public class ToggleCustomShapeModeUI : MonoBehaviour
	{
		MobilePaint mobilePaint;
		public GameObject customBrushPanel;
        public bool isEyeTracker;
        public bool isActive = false;
        Vector2 filteredPoint;
        public Button toggleButton;
        Vector3 Position;
        Rect rect;
        float XMin;
        float XMax;
        float YMin;
        float YMax;

        public Camera mainCam;
        Rect camRect;

        float timeBeforeClick;
        float timeBetweenClicks = 1;

        void Start()
		{
			mobilePaint = PaintManager.mobilePaint;

			if (mobilePaint == null) { Debug.LogError("No MobilePaint assigned", gameObject); }
			if (customBrushPanel == null) { Debug.LogError("No customBrushPanel assigned", gameObject); }

            timeBeforeClick = timeBetweenClicks;


            //var toggle = GetComponent<Toggle>();
            //if (toggle == null) { Debug.LogError("No Toggle component founded", gameObject); }

            //// disable button if not using custom brushes
            //toggle.interactable = mobilePaint.useCustomBrushes;

            //if (toggle.IsInteractable()) { toggle.onValueChanged.AddListener(delegate { SetMode(); }); }
        }

        private void Update()
        {
            if (isEyeTracker)
            {
                camRect = mainCam.pixelRect;
                timeBeforeClick -= Time.deltaTime;

                Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
                gazePoint.x *= camRect.width;
                gazePoint.y *= camRect.height;
                filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);
                Position = toggleButton.transform.position;
                rect = toggleButton.GetComponent<RectTransform>().rect;
                XMin = rect.xMin;
                XMax = rect.xMax;
                YMin = rect.yMin;
                YMax = rect.yMax;

                if (Input.GetKey("space"))
                {
                    if ((Position.x + XMin) < filteredPoint.x && filteredPoint.x < (Position.x + XMax) && (Position.y + YMin) < filteredPoint.y && filteredPoint.y < (Position.y + YMax) && timeBeforeClick <= 0)
                    {
                        SetMode();
                        timeBeforeClick = timeBetweenClicks;
                    }
                }
            }
        }

        public void SetMode()
		{
			//if (GetComponent<Toggle>().isOn)
			//{
			//	customBrushPanel.SetActive(true);
			//	//mobilePaint.SetDrawModeShapes();
			//}
			//else
			//{
			//	customBrushPanel.SetActive(false);
			//}

            if (!customBrushPanel.activeInHierarchy)
            {
                customBrushPanel.SetActive(true);
                //mobilePaint.SetDrawModeShapes();
            }
            else
            {
                customBrushPanel.SetActive(false);
            }
		}
	}
}