using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Created by Mobile Paint		Edited by Coral and Matt Pe
/// </summary>
namespace unitycoder_MobilePaint {
	/// <summary>
	/// this class controls the custom brush ui
	/// </summary>
	public class CustomBrushesUI : MonoBehaviour {
		public MobilePaint mobilePaint;
		public Button buttonTemplate;
		public BrushSizeScript brushSizeScript;
		public GameObject panel;
		Button[] newButton;
		Vector3[] Positions;
		Rect[] rects;
		float[] XMin;
		float[] XMax;
		float[] YMin;
		float[] YMax;
		GameObject[] children;

        public bool isEyeTracker;
        public Camera mainCam;
        Rect camRect;

		float timeBeforeClick;
		float timeBetweenClicks = 1;
		int brushSizeLast = 100; // any number that cant actually be the size 

		Vector2 filteredPoint;

		[SerializeField] private int padding = 8;

		public GameObject brushPreview;
		public int sizeReference = 3;


		void Start() {
			timeBeforeClick = timeBetweenClicks;
			newButton = new Button[mobilePaint.customBrushes.Length];
			if (mobilePaint == null) Debug.LogError("No MobilePaint assigned at " + transform.name);
			if (buttonTemplate == null) Debug.LogError("No buttonTemplate assigned at " + transform.name);
		}

		private void Update() {
            // Coral
            // this tests wether the brush has changed size and so new custom brushes are needed
            // the brushes needed are decided by the for loop and then instantiated in the right position
			if (brushSizeLast != brushSizeScript.customSize) {

				for (int i = transform.childCount - 1; i >= 0; --i) {
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
						// NOTE: maximum Y is not checked..so dont put too many custom brushes.. would need to add paging or scrolling
					}
					rectTrans.anchoredPosition = newPos;
					newPos.x += rectTrans.rect.width + padding;

					// assign brush image
					// NOTE: have to use rawimage, instead of image (because cannot cast Texture2D into Image)
					// i've read that rawimage causes extra drawcall per drawimage, thats not nice if there are tens of images..
					newButton[i].GetComponent<RawImage>().texture = mobilePaint.customBrushes[i];
					var index = i;

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

            if(isEyeTracker)
            {
                camRect = mainCam.pixelRect;
                timeBeforeClick -= Time.deltaTime;

                Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
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
                            if ((Positions[loopPos].x + XMin[loopPos]) < filteredPoint.x && filteredPoint.x < (Positions[loopPos].x + XMax[loopPos]) && (Positions[loopPos].y + YMin[loopPos]) < filteredPoint.y && filteredPoint.y < (Positions[loopPos].y + YMax[loopPos]) && timeBeforeClick <= 0)
                            {
                                SetCustomBrush(loopPos);
                                timeBeforeClick = timeBetweenClicks;
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
		/// KANE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		/// if we can set the prieview image to the pattern chosen and get it to scale the same way ive got the menu images to scale 
		/// then we can get mobile paint script to change the brush to whatever the preview looks like whenever the menu or arrows are pressed
		/// </summary>
		/// <param name="index"></param>
		// send current brush index to mobilepaint
		public void SetCustomBrush(int index) {
			mobilePaint.selectedBrush = index;
			mobilePaint.ReadCurrentCustomBrush(); // tell mobile paint to read custom brush pixel data
			mobilePaint.SetDrawModeShapes();

			//CloseCustomBrushPanel();
			gameObject.SetActive(false);

			sizeReference = index;

			brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[index];
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