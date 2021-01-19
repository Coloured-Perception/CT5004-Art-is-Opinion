using UnityEngine;

/// <summary>
/// Changes the character in the Paint scene to match the person in the street scene.
/// Author:	Kane Adams
/// </summary>
public class ChangePersonPaintScript : MonoBehaviour {
	public SpriteRenderer personImage;  // The player image that changes

	/// <summary>
	/// Changes person to draw to be person generated in street scene
	/// </summary>
	void Start() {
		personImage.sprite = DialogueManager.personInstance.myImageComponent.sprite;
	}
}