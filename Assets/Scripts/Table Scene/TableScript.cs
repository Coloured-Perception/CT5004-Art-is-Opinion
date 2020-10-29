using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the tables and their contents in Table Scene
/// </summary>

public class TableScript : MonoBehaviour {

	public GameObject TablePreFab, FruitPreFab;
	SpriteRenderer fruitSprite;
	private Animator anim;
	public List<Sprite> images, specialImages;
	static bool tablesMove = false;
	bool moveThis = false;
	static int aniNumber;
	int rand, randFruit;
	static float cancelMoveWait;
	float destroyWait, randX, randZ, randscale;

	/// <summary>
	/// Instantiates a random number of fruit onto the table in random places
	/// all fruit slightly float as a reminder to remind me to line it up exactly
	/// once i've modeled the tables in max
	/// </summary>
	private void Awake() {
		anim = GetComponent<Animator>();
		if (tag == "Table") {
			randFruit = Random.Range(1, 5);
			for (int i = 0; i < randFruit; i++) {
                // +5 in either direction is the corners of the table
                // the more fruit there is, the more spread out the fruit
                // the less fruit there is, the closer to the centre they are
                float rand = randFruit;
				randX = Random.Range(-rand + 2 * i, -rand + 2 * i + 2);
				randZ = Random.Range(-rand/2, rand/2);

                fruitSprite = FruitPreFab.GetComponent<SpriteRenderer>();
                fruitSprite.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                ChangeImage();

                if(fruitSprite.sprite == specialImages[0])
                {
                    randZ = Random.Range(rand / 2 -0.1f, rand / 2);
                }

                GameObject fruit = Instantiate(FruitPreFab, new Vector3(gameObject.transform.position.x + randX, gameObject.transform.position.y + 5f, gameObject.transform.position.z + randZ), Quaternion.identity) as GameObject;

				fruit.transform.parent = transform;
				randscale = Random.Range(0.07f, 0.085f);
				fruit.transform.localScale = new Vector3(randscale, randscale, 1);
				

			}
		}
	}

	/// <summary>
	/// Changes the fruit sprite to a random fruit
	/// </summary>
	public void ChangeImage() {
		rand = Random.Range(0, images.Count);
		if (rand == 0) {
			rand = Random.Range(0, specialImages.Count);
			fruitSprite.sprite = specialImages[rand];
		} else {
			fruitSprite.sprite = images[rand];
		}
	}

	/// <summary>
	/// If the table is at x = 0 (the current table) 
	/// it slides out the way and deletes itself 
	/// </summary>
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

	/// <summary>
	/// Triggered by clicking the cross, this instantiates a new table
	/// and sets the current one to move out the way
	/// </summary>
	public void MoveTables() {
		tablesMove = true;
		GameObject table = Instantiate(TablePreFab, transform.position, Quaternion.identity) as GameObject;
		table.transform.parent = this.transform;
	}
}