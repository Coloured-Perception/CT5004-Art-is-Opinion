/// <summary>
/// Name:			SaveImageScript.css
/// Purpose:		To save an image to a folder
/// Author:			Kane Adams
/// Date Created:	06/02/2020
/// </summary>

using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveImageScript : MonoBehaviour {
	int numOfPNGs;
	string filePath;

	public RenderTexture SaveTexture;

	private GameObject UICanvas, TransitionController;
	private Animator TranAnim;

	private void Awake() {
		UICanvas = GameObject.Find("UICanvas");

		TransitionController = GameObject.Find("Transition Controller").gameObject;
		TranAnim = TransitionController.GetComponent<Animator>();

	}

	// Start is called before the first frame update
	public void Start() {
		numOfPNGs = 1;
		filePath = Application.persistentDataPath;  // Where the image is to be saved

		// Stores the info on what is saved in the filePath to an array
		DirectoryInfo info = new DirectoryInfo(filePath);
		FileInfo[] fileInfo = info.GetFiles();

		// Goes through each file within the array, if the file is a .png, numOfPNGs increments
		foreach (FileInfo file in fileInfo) {
			if (file.Extension == ".png") {
				numOfPNGs++;
			}
		}
	}

	/// <summary>
	/// Calls the co-routine that saves the image that has been drawn (when save button is clicked)
	/// </summary>
	public void Save() {
		StartCoroutine(CoSave());
	}

	/// <summary>
	/// Once the current frame ends, the SaveCamera's image is saved to Assets folder
	/// </summary>
	/// <returns>Waits until the endd of the rendered frame</returns>
	private IEnumerator CoSave() {
		yield return new WaitForEndOfFrame();

		RenderTexture.active = SaveTexture;

		Texture2D texture2D = new Texture2D(SaveTexture.width, SaveTexture.height);
		texture2D.ReadPixels(new Rect(0, 0, SaveTexture.width, SaveTexture.height), 0, 0);
		texture2D.Apply();

		byte[] saveData = texture2D.EncodeToPNG();  // Turns the image seen in the SaveCamera to a PNG

		File.WriteAllBytes(filePath + "/SavedImage" + numOfPNGs + ".png", saveData);  // Saves the .PNG to the desired directory

		ChangeScene();
	}

	// Coral
	public void ChangeScene() {
		TransitionController.gameObject.SetActive(true);
		TranAnim.Play("Camera Shutter Close Ani");
	}	
}