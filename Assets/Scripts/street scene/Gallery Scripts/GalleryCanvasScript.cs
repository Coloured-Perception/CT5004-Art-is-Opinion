using UnityEngine;

public class GalleryCanvasScript : MonoBehaviour {
	CameraScript cameraScript;

	public void Paintingclicked() {
		cameraScript.GalleryCanvas = transform.position;
		Debug.Log(cameraScript.GalleryCanvas);
	}
}