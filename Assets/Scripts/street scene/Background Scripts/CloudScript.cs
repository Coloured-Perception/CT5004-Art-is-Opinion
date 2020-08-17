using UnityEngine;

/// <summary>
/// Created by Coral
/// this class controlls the clouds moving in the sky during the street scene
/// </summary>
public class CloudScript : MonoBehaviour
{
	float timeWait = 300;
	float direction;
	float flip;

	/// <summary>
	/// randomly flips the sprite and sets the speed in the direction it has furthest to travel
	/// </summary>
	private void Start() {

		flip = Random.Range(0, 2);
		if (flip == 0) {
			GetComponent<SpriteRenderer>().flipX = false;
		} else {
			GetComponent<SpriteRenderer>().flipX = true;
		}

		if (transform.position.x < 0) {
			direction = Random.Range(0.02f, 0.1f);
		} else {
			direction = Random.Range(-0.02f, -0.1f);
		}
	}

	/// <summary>
	/// moves cloud till it clears the screen
	/// </summary>
	private void Update() {

		transform.Translate(direction, 0, 0);
		if (timeWait > 0) {
			timeWait -= Time.deltaTime;
			if (timeWait <= 0) {
				Destroy(gameObject);
			}
		}
	}
}