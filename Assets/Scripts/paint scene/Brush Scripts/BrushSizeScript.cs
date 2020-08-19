using UnityEngine;
using UnityEngine.UI;

namespace unitycoder_MobilePaint {
	public class BrushSizeScript : MonoBehaviour {
		// Current size of the brush and it's preview
		float brushPreviewSize;
		int currentBrushSize;

		// Size boundaries for brush
		int minDifference = 3;
		int maxDifference = 2;

		//// Size boundaries for brush preview
		//float maxPreviewSize = 2f;
		//float minPreviewSize = 0.2f;

		public int customSize;

		[SerializeField]
		GameObject brushPreview;

		MobilePaint mobilePaint;
		public CustomBrushesUI customBrushesUI;
		int sizeDifference;

		// Start is called before the first frame update
		void Start() {
			mobilePaint = PaintManager.mobilePaint; // Gets reference to mobilePaint through PaintManager

			// Sets the default Brush Size
			currentBrushSize = 20;
			mobilePaint.SetBrushSize(currentBrushSize);

			//// sets the default for brush preview
			//brushPreviewSize = 0.3f;
			//brushPreview.transform.localScale = new Vector2(brushPreviewSize, brushPreviewSize);

			customSize = 3;
			Debug.Log(customSize);

		}

		// Changes the size of the brush by 5
		public void IncreaseBrushSize() {
			Debug.Log(customSize);
			// If the new size is bigger than the maxBrushSize, it won't change size
			if (sizeDifference >= maxDifference - 1) {
				// Increase brush size
				currentBrushSize += 5;
				mobilePaint.SetBrushSize(currentBrushSize);

				//// Increase preview
				//brushPreviewSize += 0.05f;
				//brushPreview.transform.localScale = new Vector2(brushPreviewSize, brushPreviewSize);


				sizeDifference -= 1;

				brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[customBrushesUI.sizeReference + sizeDifference];

			}
			
				customSize -= 1;
			Debug.Log(customSize);

		}

		// Changes the size of the brush by -5
		public void DecreaseBrushSize() {
			Debug.Log(customSize);

			// If the new size is smaller than the minBrushSize, it won't change size
			if (sizeDifference <= minDifference + 1) {
				// Decrease brush size
				currentBrushSize -= 5;
				mobilePaint.SetBrushSize(currentBrushSize);

				//// Decrease preview
				//brushPreviewSize -= 0.05f;
				//brushPreview.transform.localScale = new Vector2(brushPreviewSize, brushPreviewSize);

				sizeDifference += 1;

				brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[customBrushesUI.sizeReference + sizeDifference];

				customSize += 1;
			}
			Debug.Log(customSize);

		}
	}
}