using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDetailsScript : MonoBehaviour {
	const int startTime = 0;
	float time;

	// Start is called before the first frame update
	void Start() {
		time = startTime;
	}

	// Update is called once per frame
	void FixedUpdate() {
		time += Time.deltaTime;
		Debug.Log((int)time);
	}

	public void SaveTime() {
		PlayerPrefs.SetInt("PaintTime", (int)time);
	}
}
