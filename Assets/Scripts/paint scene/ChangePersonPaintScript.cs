using UnityEngine;

/// <summary>
/// Changes the character in the Paint Scene to match the person in the Street Scene
/// If the tutorial, chooses the fruit that should appear in Still Life Paint Scene
/// Author: Kane Adams, edited by Coral Weston
/// </summary>
public class ChangePersonPaintScript : MonoBehaviour {
	public SpriteRenderer personImage;  // The player image that changes
	public Sprite Banana;
	public Sprite Apple;

	// Start is called before the first frame update
	void Start() {
		Debug.Log(PlayerPrefs.GetString("LastScene"));
		if (PlayerPrefs.GetString("LastScene") == "GalleryScene") {
			personImage.sprite = null;
		} else {
			Debug.Log(PlayerPrefs.GetInt("intro") + "  intro");
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
}