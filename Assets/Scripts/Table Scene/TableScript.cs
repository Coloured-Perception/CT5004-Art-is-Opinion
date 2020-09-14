using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour {


	static bool tablesMove = false;
	bool moveThis = false;
	static int aniNumber;
	public GameObject TablePreFab;
	static float cancelMoveWait;
	float destroyWait;
	SpriteRenderer fruitSprite;

	private Animator anim;

	public List<Sprite> images;
	public List<Sprite> specialImages;
	int rand;


	private void Awake() {
		anim = GetComponent<Animator>();
		if (tag == "Table") {

			fruitSprite = this.GetComponentInChildren<SpriteRenderer>();
			fruitSprite.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			Debug.Log(fruitSprite.shadowCastingMode);
			ChangeImage();

		}
	}

	public void ChangeImage() {
		rand = Random.Range(0, images.Count);
		if (rand == 0) {
			rand = Random.Range(0, specialImages.Count);
			fruitSprite.sprite = specialImages[rand];
		} else {
			fruitSprite.sprite = images[rand];
		}
	}

	void Update() {	

		if (tag == "Table") {
			if (transform.position.x == 0 && tablesMove) {
				destroyWait = 3;
				tablesMove = false;
				anim.Play("MoveOut");

			} else if (destroyWait > 0) {
				destroyWait -= Time.deltaTime;
				if (destroyWait <= 0) {
					Destroy(gameObject);
				}
			}
		}
	}	   	 

	public void MoveTables() {
		tablesMove = true;
		GameObject table = Instantiate(TablePreFab, transform.position, Quaternion.identity) as GameObject;
		table.transform.parent = this.transform;

	}
}











//if (name == "Table Controller" && tablesMove && cancelMoveWait > 0) {
//		cancelMoveWait -= Time.deltaTime;
//		if (cancelMoveWait <= 0) {
//			tablesMove = false;
//		}
//	}




//	if (cancelMoveWait == 1) {
//		if (tablesMove) {
//			aniNumber += 1;
//			if (aniNumber == 1) {
//				anim.Play("MoveIn");
//			} else if (aniNumber == 2) {
//				anim.Play("MoveOut");
//			} else {
//				Destroy(gameObject);
//			}
//			Debug.Log(aniNumber);
//		}
//	}
//} else if (tablesMove && cancelMoveWait > 0) {
//	cancelMoveWait -= Time.deltaTime;
//	if (cancelMoveWait <= 0) {
//		tablesMove = false;
//	}
