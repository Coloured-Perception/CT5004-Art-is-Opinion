using UnityEngine;
using UnityEngine.UI;

namespace unitycoder_MobilePaint {
	public class BrushSizeScript : MonoBehaviour {

		// Size boundaries for brush
		private int maxDifference = 3;
		private int minDifference = -2;
		private int sizeDifference;
		private string PreviewName;
		public int customSize;

		[SerializeField]
		GameObject brushPreview;

		MobilePaint mobilePaint;
		public CustomBrushesUI customBrushesUI;

		void Start() {
			mobilePaint = PaintManager.mobilePaint; // Gets reference to mobilePaint through PaintManager
			customSize = 2;
		}

		public void DecreaseBrushSize() {

			if (sizeDifference > minDifference) {
				sizeDifference -= 1;
				PreviewName = brushPreview.GetComponent<RawImage>().texture.name;

				if (PreviewName.StartsWith("Circle")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 0 + sizeDifference];
				} else if (PreviewName.StartsWith("Square")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 1 + sizeDifference];
				} else if (PreviewName.StartsWith("Soft")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 2 + sizeDifference];
				} else if (PreviewName.StartsWith("Splater")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 3 + sizeDifference];
				} else if (PreviewName.StartsWith("Spoty")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 4 + sizeDifference];
				} else if (PreviewName.StartsWith("Dots")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 5 + sizeDifference];
				} else if (PreviewName.StartsWith("Angled Splat")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 6 + sizeDifference];
				} else if (PreviewName.StartsWith("Sharp Stones")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 7 + sizeDifference];
				}
				customSize -= 1;
			}
		}

		public void IncreaseBrushSize() {

			if (sizeDifference < maxDifference) {
				sizeDifference += 1;
				PreviewName = brushPreview.GetComponent<RawImage>().texture.name;

				if (PreviewName.StartsWith("Circle")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 0 + sizeDifference];
				} else if (PreviewName.StartsWith("Square")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 1 + sizeDifference];
				} else if (PreviewName.StartsWith("Soft")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 2 + sizeDifference];
				} else if (PreviewName.StartsWith("Splater")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 3 + sizeDifference];
				} else if (PreviewName.StartsWith("Spoty")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 4 + sizeDifference];
				} else if (PreviewName.StartsWith("Dots")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 5 + sizeDifference];
				} else if (PreviewName.StartsWith("Angled Splat")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 6 + sizeDifference];
				} else if (PreviewName.StartsWith("Sharp Stones")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[2 + 6 * 7 + sizeDifference];
				}
				customSize += 1;
			}
		}
	}
}