using UnityEngine;
using Tobii.Gaming;

public class GazeAwareColour : MonoBehaviour {
	public static Color newColor { get; private set; }

	public GameObject colourPanel;
	Vector3 panelPos;
	Rect panelRect;

	float panelXMin;
	float panelXMax;
	float panelYMin;
	float panelYMax;

	float timeBeforeClick;
	float timeBetweenClicks = 1;
	Vector2 filteredPoint;

	// Update is called once per frame
	void Update() {
		//Only click if spacebar is down
		if (Input.GetKey("space")) {
			timeBetweenClicks -= Time.deltaTime;

			//Find position and size in x and y directions of the buttons
			panelPos = colourPanel.transform.position;
			panelRect = colourPanel.GetComponent<RectTransform>().rect;

			panelXMin = panelRect.xMin;
			panelXMax = panelRect.xMax;
			panelYMin = panelRect.yMin;
			panelYMax = panelRect.yMax;

			Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker           
			filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);
			//If player is looknig at the colour panel, select pixel colour of area looked at as brush colour
			if ((panelPos.x + panelXMin) < filteredPoint.x && filteredPoint.x < (panelPos.x + panelXMax) && (panelPos.y + panelYMin) < filteredPoint.y && filteredPoint.y < (panelPos.y + panelYMax) && timeBetweenClicks <= 0) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit)) {
					ColorPicker picker = hit.collider.GetComponent<ColorPicker>();

					if (picker != null) {
						Renderer rend = hit.transform.GetComponent<Renderer>();
						MeshCollider meshCollider = hit.collider as MeshCollider;

						if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
							return;

						Texture2D tex = rend.material.mainTexture as Texture2D;
						Vector2 pixelUV = hit.textureCoord;
						pixelUV.x *= tex.width;
						pixelUV.y *= tex.height;
						newColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
					}
				}
				timeBeforeClick = timeBetweenClicks;
			}
		}
	}
}