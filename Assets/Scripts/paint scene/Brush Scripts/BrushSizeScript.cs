using UnityEngine;
using UnityEngine.UI;

namespace unitycoder_MobilePaint {
	public class BrushSizeScript : MonoBehaviour {

		private int maxBrush = 5;
		private int minBrush = 0;
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

		/// <summary>
		/// changes the brush preview
		/// 6 is the number of sizes for each brush
		/// note that the custom brushes are listed on DrawingPlaneCanvas 
		/// and the element numbers are listed in the reverse order of the sprite numbers 
		/// to add new brushes just copy and paste the last else if and change the name and multiplyer 
		/// </summary>
		public void DecreaseBrushSize() {
			if (customSize > minBrush) {
				customSize -= 1;
				PreviewName = brushPreview.GetComponent<RawImage>().texture.name;

				if (PreviewName.StartsWith("Circle")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 0 + customSize];
				} else if (PreviewName.StartsWith("Square")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 1 + customSize];
				} else if (PreviewName.StartsWith("Soft")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 2 + customSize];
				} else if (PreviewName.StartsWith("Splater")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 3 + customSize];
				} else if (PreviewName.StartsWith("Spoty")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 4 + customSize];
				} else if (PreviewName.StartsWith("Dots")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 5 + customSize];
				} else if (PreviewName.StartsWith("Angled Splat")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 6 + customSize];
				} else if (PreviewName.StartsWith("Sharp Stones")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 7 + customSize];
				}
			}
		}

		public void IncreaseBrushSize() {
			if (customSize < maxBrush) {
				customSize += 1;
				PreviewName = brushPreview.GetComponent<RawImage>().texture.name;

				if (PreviewName.StartsWith("Circle")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 0 + customSize];
				} else if (PreviewName.StartsWith("Square")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 1 + customSize];
				} else if (PreviewName.StartsWith("Soft")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 2 + customSize];
				} else if (PreviewName.StartsWith("Splater")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 3 + customSize];
				} else if (PreviewName.StartsWith("Spoty")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 4 + customSize];
				} else if (PreviewName.StartsWith("Dots")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 5 + customSize];
				} else if (PreviewName.StartsWith("Angled Splat")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 6 + customSize];
				} else if (PreviewName.StartsWith("Sharp Stones")) {
					brushPreview.GetComponent<RawImage>().texture = mobilePaint.customBrushes[ 6 * 7 + customSize];
				}
			}
		}
	}
}