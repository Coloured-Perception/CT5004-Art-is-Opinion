using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Saves the image created by player to set filePath so that it can be loaded later.
/// Author:	Kane Adams
/// </summary>
public class SaveImageScript : MonoBehaviour {
	int numOfPNGs;
	string filePath;
	string fileName;

	public RenderTexture SaveTexture;

	private GameObject UICanvas, TransitionController;
	private Animator TranAnim;

	public PaintDetailsScript paintTime;

	private void Awake() {
		UICanvas = GameObject.Find("Main Canvas");

		//TransitionController = GameObject.Find("Transition Controller");
		//TranAnim = TransitionController.GetComponent<Animator>();
	}

	/// <summary>
	/// Creates filePath and finds previous PNGs
	/// </summary>
	public void Start() {
		numOfPNGs = 1;
		filePath = Application.persistentDataPath;  // Where the image is to be saved

		// Checks whether the folders needed to save different images are available, otherwise it creates them
		if (Directory.Exists(filePath + "/Table")) {
			Debug.Log("Table folder");
		} else {
			Directory.CreateDirectory(filePath + "/Table");
		}
		if (Directory.Exists(filePath + "/Portraits")) {
			Debug.Log("Portrait folder");
		} else {
			Directory.CreateDirectory(filePath + "/Portraits");
		}
		if (Directory.Exists(filePath + "/StillLifes")) {
			Debug.Log("Still-life folder");
		} else {
			Directory.CreateDirectory(filePath + "/StillLifes");
		}

		if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
			filePath += "/Portraits";
			fileName = "/PortraitImage";
		} else if (SceneManager.GetActiveScene().name == "TableScene") {
			filePath += "/Table";
			fileName = "/FruitImage";
		} else if (SceneManager.GetActiveScene().name == "StillLifePaintScene" || SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			filePath += "/StillLifes";
			fileName = "/StillImage";
		}

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
		if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
			paintTime.SaveTime();
		}
		int numOfPaintings = PlayerPrefs.GetInt("PaintingAmount");
		numOfPaintings++;
		PlayerPrefs.SetInt("PaintingAmount", numOfPaintings);
		StartCoroutine(CoSave());
	}

	/// <summary>
	/// Once the current frame ends, the SaveCamera's image is saved to Assets folder
	/// </summary>
	/// <returns>Waits until the end of the rendered frame</returns>
	private IEnumerator CoSave() {
		yield return new WaitForEndOfFrame();

		RenderTexture.active = SaveTexture;

		Texture2D texture2D = new Texture2D(SaveTexture.width, SaveTexture.height);
		texture2D.ReadPixels(new Rect(0, 0, SaveTexture.width, SaveTexture.height), 0, 0);
		texture2D.Apply();

		byte[] saveData = texture2D.EncodeToPNG();  // Turns the image seen in the SaveCamera to a PNG

		if (SceneManager.GetActiveScene().name == "TableScene") {
			File.WriteAllBytes(filePath + fileName + ".png", saveData);    // Saves the .PNG to the desired directory
		} else {
			File.WriteAllBytes(filePath + fileName + numOfPNGs + ".png", saveData);    // Saves the .PNG to the desired directory
		}

		PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
		ChangeScene();
	}

	// Coral
	public void ChangeScene() {
		//TransitionController.gameObject.SetActive(true);
		//TranAnim.Play("Camera Shutter Close Ani");
	}
}