// sends picked color to MobilePaint script

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace unitycoder_MobilePaint
{

    public class ColorUIManager : MonoBehaviour
    {
        MobilePaint mobilePaint;
        public Button[] colorpickers;       // colors are taken from these buttons

        public bool offsetSelected = true;  // should we move the pencil when its selected
        //public float defaultOffset=-46;
        //public float moveOffsetX=-24;

     //   [HideInInspector]
		public GameObject preview;


        void Awake()
        {
            mobilePaint = PaintManager.mobilePaint;

            if (mobilePaint == null) Debug.LogError("No MobilePaint assigned at " + transform.name, gameObject);
            if (colorpickers.Length < 1) Debug.LogWarning("No colorpickers assigned at " + transform.name, gameObject);

            //preview = GetComponent<RawImage>();
            //if (preview == null) Debug.LogError("No image component founded at " + transform.name, gameObject);


            // Add event listeners to pencil buttons
            for (int i = 0; i < colorpickers.Length; i++)
            {
                var button = colorpickers[i];
                if (button != null)
                {
                    button.onClick.AddListener(delegate { this.SetCurrentColor(button); });
				}
			}
        }


        // some button was clicked, lets take color from it and send to mobilepaint canvas 
        public void SetCurrentColor(Button button)
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


		//public void Update()
		//{
		//    if (Input.GetMouseButton(0))
		//    {
		//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//        RaycastHit hit;

		//        if (Physics.Raycast(ray, out hit))
		//        {
		//            var picker = hit.collider.GetComponent<ColorPicker>();

		//            if (picker != null)
		//            {
		//                Renderer rend = hit.transform.GetComponent<Renderer>();
		//                MeshCollider meshCollider = hit.collider as MeshCollider;

		//                if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
		//                    return;
		//                Texture2D tex = rend.material.mainTexture as Texture2D;
		//                Vector2 pixelUV = hit.textureCoord;
		//                pixelUV.x *= tex.width;
		//                pixelUV.y *= tex.height;
		//                Color newColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
		//            }
		//        }
		//    }
		//}


		//      void ResetAllOffsets()
		//{
		//	for (int i=0;i<colorpickers.Length;i++)
		//	{
		//		SetButtonOffset(colorpickers[i],defaultOffset); 
		//	}
		//}


		//void SetButtonOffset(Button button,float offsetX)
		//{
		//	RectTransform rectTransform = button.GetComponent<RectTransform>();
		//	rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,offsetX,rectTransform.rect.width);
		//}

	}   // class
}   // namespace