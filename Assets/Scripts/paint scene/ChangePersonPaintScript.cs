/// <summary>
/// Name:           ChangePersonPaintScript.cs
/// Purpose:        Changes the character in the Paint scene to match the person in the street scene
/// Author:         Kane Adams
/// Date Created:   25/04/2020
/// </summary>

using UnityEngine;

public class ChangePersonPaintScript : MonoBehaviour {
	public SpriteRenderer personImage;  // The player image that changes
	public Sprite Banana;
	public Sprite Apple;

	// Start is called before the first frame update
	void Start() {
		if (PlayerPrefs.GetInt("intro") == 0) {

			if (TutorialDialogeManager.sentenceNumber < 17) {
				personImage.sprite = Banana;
			} else {
				personImage.sprite = Apple;
			}
		} else {
			personImage.sprite = DialogueManager.personInstance.myImageComponent.sprite;
		}
		//personImage.sprite = DialogueManager.personInstance.myImageComponent.sprite;
	}
}