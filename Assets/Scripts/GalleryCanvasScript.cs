﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryCanvasScript : MonoBehaviour
{
	CameraScript cameraScript;


	public void Paintingclicked() {
		cameraScript.GalleryCanvas = this.transform.position;
		Debug.Log(cameraScript.GalleryCanvas);
	}
}
