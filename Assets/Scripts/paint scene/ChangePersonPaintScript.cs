/// <summary>
/// Name:           ChangePersonPaintScript.cs
/// Purpose:        Changes the character in the Paint scene to match the person in the street scene
/// Author:         Kane Adams
/// Date Created:   25/04/2020
/// </summary>

using UnityEngine;

public class ChangePersonPaintScript : MonoBehaviour {
	public SpriteRenderer personImage;  // The player image that changes

	// Start is called before the first frame update
	void Start() {
		personImage.sprite = DialogueManager.personInstance.myImageComponent.sprite;
	}
}