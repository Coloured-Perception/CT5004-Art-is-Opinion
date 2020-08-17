using UnityEngine;

/// <summary>
/// Created by Coral
/// this class manages the menu and street uis
/// </summary>
public class MenuButtonScript : MonoBehaviour {
	float timeWait;
	public GameObject MenuUI;
	public GameObject Street;

	public void ButtonClicked() {
		timeWait = 2;
	}

	/// <summary>
	/// waits 2 seconds for the camera shutter animation to play to have a seamless transition but is not dependent on it 
	/// </summary>
	void Update() {
		if (timeWait > 0) {
			timeWait -= Time.deltaTime;

			if (timeWait <= 0) {
				MenuUI.transform.gameObject.SetActive(true);
				Street.transform.gameObject.SetActive(false);
			}
		}
	}
}