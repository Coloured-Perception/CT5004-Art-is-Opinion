﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePerson : MonoBehaviour {
	public static ChangePerson personInstance;

	//public Sprite[] images;
	public List<Sprite> images;
	int numImages;
	public Image myImageComponent;
	int rand;
	public float changewait;


	/// <summary>
	/// Allows the script to be accessable in other scenes.
	/// To allow the character to be the same in both scenes.
	/// Added by Kane
	/// </summary>
	private void Awake() {
		personInstance = this;

		DontDestroyOnLoad(gameObject);
	}


	// Start is called before the first frame update
	void Start() {
		myImageComponent = GetComponent<Image>();
		ChangeImage();
	}

	public void ChangeImage() {
		rand = Random.Range(0, images.Count);
		myImageComponent.sprite = images[rand];
		Debug.Log(rand);
	}

	//public void NewImage() {
	//	changewait = 2;
	//}

	//public void FixedUpdate() {


	//	if (changewait > 0) {
	//		changewait -= Time.deltaTime;
	//		{
	//			if (changewait <= 0) {
	//				ChangeImage();
	//				Debug.Log("jyfulydfkuydkluydclkudc");
	//			}
	//		}
	//	}
	//}
}
