using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCustomBrushScript : MonoBehaviour {
	TutorialManager tutorialManager;

	private void Awake() {
		tutorialManager = GameObject.Find("dialoge manager").GetComponent<TutorialManager>();

		Button btn = this.GetComponent<Button>(); //Grabs the button component
		btn.onClick.AddListener(TaskOnClick); //Adds a listner on the button
	}

	void TaskOnClick() {
		Debug.Log("You have clicked the button!");
		tutorialManager.ColourPrieviewClick();

	}
}
