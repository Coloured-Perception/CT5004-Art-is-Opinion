using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Created by Coral
/// This class 
/// </summary>
public class WalkController : MonoBehaviour {
	float timeWaitPeople = 0.2f;
	float timeWaitClouds = 0.2f;
	int location;
	float startX, x, z, y;
	public List<GameObject> peoplePrefabs, cloudPrefabs;
	GameObject Prefabs = null;

	/// <summary>
	/// This instantiates people and clouds at random positions in the scene to make the street look populated on start up
	/// startx and z decide the location
	/// </summary>
	private void Start() {
		if (SceneManager.GetActiveScene().name == "StreetScene") {
			for (int i = 0; i < 3; i++) {
				startX = Random.Range(-15, 15);
				z = Random.Range(5.3f, 8.7f);
				if (z > 7f) {
					z += 5.3f;
				}
				Prefabs = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(startX, -0.2f, z), Quaternion.identity);
			}
			for (int i = 0; i < 6; i++) {
				startX = Random.Range(-30, 30);
				z = Random.Range(22, 25);
				y = Random.Range(5, 15);
				Prefabs = GameObject.Instantiate(cloudPrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(startX, y, z), Quaternion.identity);
			}
		} else if (SceneManager.GetActiveScene().name == "TitleScene") {	
			for (int i = 0; i < 6; i++) {
				startX = Random.Range(-45, 45);
				y = Random.Range(9, 27);
				z = Random.Range(26, 30);
				Prefabs = GameObject.Instantiate(cloudPrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(startX, y, z), Quaternion.identity);
			}
		}
	}

	/// <summary>
	/// instantiates new random people and clouds at random times
	/// </summary>
	private void Update() {

		if (timeWaitPeople > 0) {
			timeWaitPeople -= Time.deltaTime;
			if (timeWaitPeople <= 0) {
				if (SceneManager.GetActiveScene().name == "StreetScene") {
					x = 15;
					y = 0.2f;
					z = Random.Range(5.3f, 8.7f);
					if (z > 7f) {
						z += 5.3f;
					}

					location = Random.Range(0, 2);
					if (location == 0) {
						Prefabs = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(-x, -y, z), Quaternion.identity);
					} else {
						Prefabs = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(x, -y, z), Quaternion.identity);
					}
					timeWaitPeople = Random.Range(1, 3);

				}
			}
		}
		if (timeWaitClouds > 0) {
			timeWaitClouds -= Time.deltaTime;
			if (timeWaitClouds <= 0) {
				if (SceneManager.GetActiveScene().name == "StreetScene") {
					x = 30;
					y = Random.Range(5, 15);
					z = Random.Range(22, 25);
				} else if (SceneManager.GetActiveScene().name == "TitleScene") {
					x = 45;
					y = Random.Range(9, 27);
					z = Random.Range(26, 30);
				}
				location = Random.Range(0, 2);
				if (location == 0) {
					Prefabs = GameObject.Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Count)], new Vector3(-x, y, z), Quaternion.identity);
				} else {
					Prefabs = GameObject.Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Count)], new Vector3(x, y, z), Quaternion.identity);
				}
				timeWaitClouds = Random.Range(10, 30);
			}
		}

	}
}