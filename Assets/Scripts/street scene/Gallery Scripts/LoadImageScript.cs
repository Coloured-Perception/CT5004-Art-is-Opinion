using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// This class Loads previously saved PNGs/paintings (from select filePath) onto RawImage game objects that act as painting frames.
/// Author: Kane Adams
/// </summary>
public class LoadImageScript : MonoBehaviour {
	public GameObject galleryBoard;

	// The different painting frames that can have an image loaded onto
	public RawImage painting1;
	public RawImage painting2;
	public RawImage painting3;
	public RawImage painting4;
	public RawImage painting5;
	public RawImage painting6;
	public RawImage painting7;
	public RawImage painting8;
	public RawImage painting9;
	public RawImage painting10;

	// Start is called before the first frame update
	/// <summary>
	/// Goes through the saved PNGs and loads the most recent ones
	/// </summary>
	/// <returns></returns>
	private IEnumerator Start() {
		Debug.Log(Application.persistentDataPath);

		int numOfImages = 10;
		int numOfPNGs = 1;                                  // Used to load the last PNG in the folder (loads "SavedImage" + numOfPNGs)
		string filePath = Application.persistentDataPath;   // Where the images are stored
		string individualFilePath;                          // The file directory of a specific image to load

		List<string> PNGImages = new List<string>();	// Stores all PNG files
		RawImage[] paintings = { painting1, painting2, painting3, painting4, painting5, painting6, painting7, painting8, painting9, painting10 };
		Texture loadedTexture;

		// Stores the info on what is saved in the filePath to an array
		DirectoryInfo info = new DirectoryInfo(filePath);
		FileInfo[] fileInfo = info.GetFiles();
		

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// Need to change code so it goes through each individual file and moves all the numbers down

		// Goes through each file within the array, if the file extension is a '.png', numOfPNGs increments
		foreach (FileInfo file in fileInfo) {
			if (File.Exists(filePath + "/SavedImage" + numOfPNGs + ".png")) {
				Debug.Log("Yay!");
				PNGImages.Add(filePath + "/SavedImage" + numOfPNGs + ".png");   // Stores only the png files
				numOfPNGs++;
			} else {
				Debug.Log("UHOH! Big DUDU!");
				if (File.Exists(filePath + "/SavedImage" + (numOfPNGs + 1) + ".png")) {
					File.Move("SavedImage" + (numOfPNGs + 1), "SavedImage" + numOfPNGs);
				}
			}
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		

		// After all the player's drawings our added to list, if there are still less than number of required images, add default image to list
		while (numOfPNGs <= numOfImages) {
			PNGImages.Add(Application.dataPath + "/CPProfile.png");
			numOfPNGs++;
		}

		PNGImages.Reverse();    // Reverses array have last object loaded first

		if (numOfPNGs > 0) {
			// For every image frame, a savedImage is loaded
			for (int i = 0; i < numOfImages; i++) {
				individualFilePath = PNGImages[i];

				using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(individualFilePath)) {
					yield return uwr.SendWebRequest();

					if (uwr.isNetworkError || uwr.isHttpError) {
						loadedTexture = null;
						Debug.Log("Error loading texture.");
						Debug.Log(uwr.error);
					} else {
						loadedTexture = DownloadHandlerTexture.GetContent(uwr);
						//Debug.Log("Succesfully loaded texture!");
					}
					paintings[i].texture = loadedTexture;
				}
			}
		}
	}
}