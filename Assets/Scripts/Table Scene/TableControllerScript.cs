using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TableControllerScript : MonoBehaviour {
	public GameObject TablePreFab, FruitPreFab;
	SpriteRenderer fruitSprite;
	private Animator tableAnim, tranAnim;
	public List<Sprite> images, specialImages;
	static bool tablesMove = false;
	bool moveThis = false;
	static int aniNumber;
	int rand, randFruit;
	static float cancelMoveWait;
	float destroyWait, randscale;

	TableScript tableScript;
	GameObject tableIn, tableOut;
	private GameObject TransitionController;
	public Button YesButton, NoButton;


	private void Awake() {
		TransitionController = GameObject.Find("Transition Controller");
		tranAnim = TransitionController.GetComponent<Animator>();

		YesButton = GameObject.Find("Yes Button").GetComponent<Button>();
		NoButton = GameObject.Find("No Button").GetComponent<Button>();
	}

	/// <summary>
	/// Triggered by clicking the cross, this instantiates a new table
	/// and sets the current one to move out the way
	/// 
	/// //////////////////table move out must finish before the x is clicked again
	/// </summary>
	public void ChangeTables() {
		YesButton.interactable = false;
		NoButton.interactable = false;
		tableIn = Instantiate(TablePreFab, transform.position, Quaternion.identity) as GameObject;
		tableOut = gameObject.transform.GetChild(0).gameObject;
		tableAnim = tableOut.GetComponent<Animator>();
		tableAnim.Play("TableMoveOut");
		tableIn.transform.parent = transform;
	}

	public void TickButton() {
		TransitionController.gameObject.SetActive(true);
		tranAnim.Play("Camera Shutter Close Ani");
	}
}
