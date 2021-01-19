using UnityEngine;
using UnityEngine.UI;

namespace unitycoder_MobilePaint {
	/// <summary>
	/// This class is used to change the size of a custom brush and the brush preview
	/// </summary>
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

		/// <summary>
		/// Gets reference to MobilePaint script (and it's methods) through PaintManager.
		///	Assigns initial values for brush
		/// </summary>
		void Start() {
			mobilePaint = PaintManager.mobilePaint; // Gets reference to mobilePaint through PaintManager
			customSize = 2;
			mobilePaint.selectedBrush = 2;
			mobilePaint.ReadCurrentCustomBrush();   // Tell mobile paint to read custom brush pixel data
			mobilePaint.SetDrawModeShapes();
		}

		/// <summary>
		/// When called, custom brush and it's preview will decrease in size by set amount
		/// </summary>
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
				mobilePaint.selectedBrush -= 1;
				mobilePaint.ReadCurrentCustomBrush();   // tell mobile paint to read custom brush pixel data
				mobilePaint.SetDrawModeShapes();
			}
		}

		/// <summary>
		/// When called, custom brush and it's preview will increase in size by set amount
		/// </summary>
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
				mobilePaint.selectedBrush += 1;
				mobilePaint.ReadCurrentCustomBrush();   // tell mobile paint to read custom brush pixel data
				mobilePaint.SetDrawModeShapes();
			}
		}
	}
}