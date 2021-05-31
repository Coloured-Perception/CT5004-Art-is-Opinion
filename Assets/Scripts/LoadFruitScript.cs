using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// This class loads the image of the fruit assortment from the table scene
/// Author: Kane Adams
/// </summary>
public class LoadFruitScript : MonoBehaviour {
	public GameObject fruit;
	private Sprite fruitSprite;

	private void Start() {
		if (PlayerPrefs.GetString("LastScene") == "GalleryScene") {
			fruit.GetComponent<SpriteRenderer>().sprite = null;
		} else {
			if (Directory.Exists(Application.persistentDataPath + "/Table")) {
				Debug.Log("Table folder");
			} else {
				Directory.CreateDirectory(Application.persistentDataPath + "/Table");
			}

			string filePath = Application.persistentDataPath + "//Table/FruitImage.png";
			ScreenCapture.CaptureScreenshot(filePath);
			fruitSprite = LoadSprite(filePath);
			fruit.GetComponent<SpriteRenderer>().sprite = fruitSprite;
		}
	}

	private Sprite LoadSprite(string a_filePath) {
		if (string.IsNullOrEmpty(a_filePath)) { return null; }
		if (System.IO.File.Exists(a_filePath)) {
			byte[] bytes = System.IO.File.ReadAllBytes(a_filePath);
			Texture2D texture = new Texture2D(1, 1);
			texture.LoadImage(bytes);
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			return sprite;
		}
		return null;
	}
}