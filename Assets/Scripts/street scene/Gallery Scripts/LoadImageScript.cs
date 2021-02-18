/// <summary>
/// Name:           LoadImageScript.css
/// Purpose:        To load .PNG images from resource folder in assets into a scene
/// Author:         Kane Adams
/// Date Created:   22/03/2020
/// </summary>

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
	private IEnumerator Start() {
		int numOfImages = 10;
		int numOfPNGs = 0;                                  // Used to load the last PNG in the folder (loads "SavedImage" + numOfPNGs)
		string filePath = Application.persistentDataPath;   // Where the images are stored
		string individualFilePath;                          // The file directory of a specific image to load

		List<string> PNGImages = new List<string>();    // Stores all PNG files
		RawImage[] paintings = { painting1, painting2, painting3, painting4, painting5, painting6, painting7, painting8, painting9, painting10 };
		Texture loadedTexture;

		// Stores the info on what is saved in the filePath to an array
		DirectoryInfo info = new DirectoryInfo(filePath);
		FileInfo[] fileInfo = info.GetFiles();

		// Goes through each file within he array, if the file extension is a '.png', numOfPNGs increments
		foreach (FileInfo file in fileInfo) {
			if (file.Extension == ".png") {
				numOfPNGs++;
				PNGImages.Add(filePath + "/SavedImage" + numOfPNGs + ".png");   // Stores only the png files
			}
		}
		PNGImages.Reverse();    // Reverses array have last object loaded first

		if (numOfPNGs != 0) {
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
				//		Debug.Log("Succesfully loaded texture!");
					}
					paintings[i].texture = loadedTexture;
				}
			}
		}
	}
}