using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Coral
/// This class 
/// </summary>
public class WalkController : MonoBehaviour {
	float timeWaitPeople = 0.2f;
	float timeWaitClouds = 0.2f;
	int location;
	float startX, z;
	public List<GameObject> peoplePrefabs, cloudPrefabs;
	GameObject Prefabs = null;

	/// <summary>
	/// This instantiates people and clouds at random positions in the scene to make the street look populated on start up
	/// startx and z decide the location
	/// </summary>
	private void Start() {
		for (int i = 0; i < 3; i++) {
			startX = Random.Range(-200, 200);
			z = Random.Range(160, 210);
			if (z > 185) {
				z += 85;
			}
			Prefabs = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(startX, 24, z), Quaternion.identity);
		}

		for (int i = 0; i < 6; i++) {
			startX = Random.Range(-300, 300);
			z = Random.Range(300, 900);
			Prefabs = GameObject.Instantiate(cloudPrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(startX, 200, z), Quaternion.identity);
		}
	}

	/// <summary>
	/// instantiates new random people and clouds at random times
	/// </summary>
	private void Update() {
		if (timeWaitPeople > 0) {
			timeWaitPeople -= Time.deltaTime;
			if (timeWaitPeople <= 0) {

				z = Random.Range(160, 210);
				if (z > 185) {
					z += 85;
				}
				location = Random.Range(0, 2);
				if (location == 0) {
					Prefabs = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(-260, 24, z), Quaternion.identity);
				} else {
					Prefabs = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Count)], new Vector3(260, 24, z), Quaternion.identity);
				}
				timeWaitPeople = Random.Range(1, 3);
			}
		}
		if (timeWaitClouds > 0) {
			timeWaitClouds -= Time.deltaTime;
			if (timeWaitClouds <= 0) {

				z = Random.Range(300, 900);
				location = Random.Range(0, 2);
				if (location == 0) {
					Prefabs = GameObject.Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Count)], new Vector3(-400, 200, z), Quaternion.identity);
				} else {
					Prefabs = GameObject.Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Count)], new Vector3(400, 200, z), Quaternion.identity);
				}
				timeWaitClouds = Random.Range(10, 30);
			}
		}
	}
}