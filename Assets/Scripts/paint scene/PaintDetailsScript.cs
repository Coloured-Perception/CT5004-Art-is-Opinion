using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDetailsScript : MonoBehaviour {
	const int startTime = 0;
	float time;

	public List<Color> coloursUsed;
	public List<int> brushesUsed;

	// Start is called before the first frame update
	void Start() {
		time = startTime;
	}

	// Update is called once per frame
	void FixedUpdate() {
		time += Time.deltaTime;
	}

	public void SaveTime() {
		PlayerPrefs.SetInt("PaintTime", (int)time);
		PlayerPrefs.SetInt("ColoursUsed", coloursUsed.Count);
		PlayerPrefs.SetInt("BrushesUsed", brushesUsed.Count - 1);
	}
}
