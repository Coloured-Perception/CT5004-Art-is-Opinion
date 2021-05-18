using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// This class loads the image of the fruit assortment from the table scene
/// </summary>
public class LoadFruitScript : MonoBehaviour {
	public RawImage fruitImage;
	//Texture loadedTexture;
	//string filePath;

	//private void Awake() {
	//	Debug.Log("Function Called");

	//	if (Directory.Exists(Application.persistentDataPath + "/Table")) {
	//		Debug.Log("Table folder");
	//	} else {
	//		Directory.CreateDirectory(Application.persistentDataPath + "/Table");
	//	}

	//	filePath = Application.persistentDataPath + "/Table/FruitImage.png";
	//}

	///// <summary>
	///// Loads the image saved in the folder
	///// </summary>
	///// <returns></returns>
	//private IEnumerator Start() {
	//	using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath)) {
	//		yield return uwr.SendWebRequest();

	//		if (uwr.isNetworkError || uwr.isHttpError) {
	//			loadedTexture = null;
	//			Debug.Log("Error loading texture.");
	//			Debug.Log(uwr.error);
	//		} else {
	//			loadedTexture = DownloadHandlerTexture.GetContent(uwr);
	//			Debug.Log("Successfully loaded texture!");
	//		}
	//		fruitImage.texture = loadedTexture;
	//	}
	//}

	//public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
}