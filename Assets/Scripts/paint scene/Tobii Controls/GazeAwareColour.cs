using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Tobii.Gaming;


namespace unitycoder_MobilePaint
{
    public class GazeAwareColour : MonoBehaviour
    {

        MobilePaint mobilePaint;
        public Button[] colourPickers;    // colors are taken from these buttons
        public bool offsetSelected = true;  // should we move the pencil when its selected
        Button[] colours;
        public GameObject preview;

        Vector3[] Positions;
        Rect[] rects;
        float[] XMin;
        float[] XMax;
        float[] YMin;
        float[] YMax;

        public Camera mainCam;
        Rect camRect;

        float timeBeforeClick = 1;
        float timeBetweenClicks = 1;

        Vector2 filteredPoint;

        private void Start()
        {
            colours = new Button[colourPickers.Length];
            Positions = new Vector3[colourPickers.Length];
            rects = new Rect[colourPickers.Length];
            XMin = new float[colourPickers.Length];
            XMax = new float[colourPickers.Length];
            YMin = new float[colourPickers.Length];
            YMax = new float[colourPickers.Length];

            for (int i = 0; i < colourPickers.Length; i++)
            {
                colours[i] = colourPickers[i];
            }
        }

        private void Update()
        {
            camRect = mainCam.pixelRect;
            timeBeforeClick -= Time.deltaTime;

            Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
            gazePoint.x *= camRect.width;
            gazePoint.y *= camRect.height;
            filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

            int loopPos = 0;
            foreach (Button brush in colours)
            {
                Positions[loopPos] = colours[loopPos].gameObject.transform.position;
                rects[loopPos] = colours[loopPos].GetComponent<RectTransform>().rect;
                XMin[loopPos] = rects[loopPos].xMin;
                XMax[loopPos] = rects[loopPos].xMax;
                YMin[loopPos] = rects[loopPos].yMin;
                YMax[loopPos] = rects[loopPos].yMax;

               
                if (Input.GetKey("space"))
                {
                    if ((Positions[loopPos].x + XMin[loopPos]) < filteredPoint.x && filteredPoint.x < (Positions[loopPos].x + XMax[loopPos]) && (Positions[loopPos].y + YMin[loopPos]) < filteredPoint.y && filteredPoint.y < (Positions[loopPos].y + YMax[loopPos]) && timeBeforeClick <= 0)
                    {
                        Color newColor = colourPickers[loopPos].GetComponent<Image>().color;

                        preview.gameObject.GetComponent<RawImage>().color = newColor; // set current color image

                        // send new color
                        mobilePaint.SetPaintColor(newColor);
                        timeBeforeClick = timeBetweenClicks;
                    }
                }
                loopPos += 1;
            }
        }

        public void SetCurrentColor(GameObject button)
        {
            Color newColor = button.gameObject.GetComponent<Image>().color;

            preview.gameObject.GetComponent<RawImage>().color = newColor; // set current color image

            // send new color
            mobilePaint.SetPaintColor(newColor);
            //mobilePaint.paintColor = newColor;

            //if (offsetSelected)
            //{
            //    ResetAllOffsets();
            //    SetButtonOffset(button, moveOffsetX);
            //}

        }

        //public static Color newColor { get; private set; }

        //public GameObject colourPanel;
        //Vector3 panelPos;
        //Rect panelRect;

        //float panelXMin;
        //float panelXMax;
        //float panelYMin;
        //float panelYMax;

        //float timeBeforeClick;
        //float timeBetweenClicks = 1;
        //Vector2 filteredPoint;

        //// Update is called once per frame
        //void Update() {
        //	//Only click if spacebar is down
        //	if (Input.GetKey("space")) {
        //		timeBetweenClicks -= Time.deltaTime;

        //		//Find position and size in x and y directions of the buttons
        //		panelPos = colourPanel.transform.position;
        //		panelRect = colourPanel.GetComponent<RectTransform>().rect;

        //		panelXMin = panelRect.xMin;
        //		panelXMax = panelRect.xMax;
        //		panelYMin = panelRect.yMin;
        //		panelYMax = panelRect.yMax;

        //		Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
        //		filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);
        //		//If player is looknig at the colour panel, select pixel colour of area looked at as brush colour
        //		if ((panelPos.x + panelXMin) < filteredPoint.x && filteredPoint.x < (panelPos.x + panelXMax) && (panelPos.y + panelYMin) < filteredPoint.y && filteredPoint.y < (panelPos.y + panelYMax) && timeBetweenClicks <= 0) {
        //			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //			RaycastHit hit;

        //			if (Physics.Raycast(ray, out hit)) {
        //				ColorPicker picker = hit.collider.GetComponent<ColorPicker>();

        //				if (picker != null) {
        //					Renderer rend = hit.transform.GetComponent<Renderer>();
        //					MeshCollider meshCollider = hit.collider as MeshCollider;

        //					if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null) { return; }

        //					Texture2D tex = rend.material.mainTexture as Texture2D;
        //					Vector2 pixelUV = hit.textureCoord;
        //					pixelUV.x *= tex.width;
        //					pixelUV.y *= tex.height;
        //					newColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        //				}
        //			}
        //			timeBeforeClick = timeBetweenClicks;
        //		}
        //	}
        //}
    }
}