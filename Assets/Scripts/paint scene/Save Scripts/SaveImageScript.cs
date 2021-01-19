using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Saves the image created by player to set filePath so that it can be loaded later.
/// Author:	Kane Adams
/// </summary>
public class SaveImageScript : MonoBehaviour {
	float sceneChangeWait;  // Time to wait before scene change, Coral
	int numOfPNGs;
	string filePath;

	public RenderTexture SaveTexture;

	/// <summary>
	/// Creates filePath and finds previous PNGs
	/// </summary>
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
		sceneChangeWait = 2;
		StartCoroutine(CoSave());
	}

	/// <summary>
	/// Once the current frame ends, the SaveCamera's image is saved to Assets folder
	/// </summary>
	/// <returns>Waits until the end of the rendered frame</returns>
	private IEnumerator CoSave() {
		yield return new WaitForEndOfFrame();

		RenderTexture.active = SaveTexture;

		Texture2D texture2D = new Texture2D((SaveTexture.width), SaveTexture.height);
		texture2D.ReadPixels(new Rect(0, 0, (SaveTexture.width), SaveTexture.height), 0, 0);
		texture2D.Apply();

		byte[] saveData = texture2D.EncodeToPNG();  // Turns the image seen in the SaveCamera to a PNG

		File.WriteAllBytes(filePath + "/SavedImage" + numOfPNGs + ".png", saveData);  // Saves the .PNG to the desired directory
	}

	// Coral
	/// <summary>
	/// Counts down to zero before changing scene
	/// </summary>
	private void Update() {
		if (sceneChangeWait > 0) {
			sceneChangeWait -= Time.deltaTime;
			if (sceneChangeWait <= 0) {
				SceneManager.LoadScene("StreetScene");
			}
		}
	}
	// Coral
}