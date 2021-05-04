using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this sccript is to be added to any object in the scene with animation events that doesnt already have its oen script 
/// </summary>
public class TableSceneAniEvents : MonoBehaviour
{
	private Animator TableUIAnim;

	private void Awake() {
		TableUIAnim = GameObject.Find("TableUI").GetComponent<Animator>();
	}

   public void ShowTableUIEnd() {
		TableUIAnim.enabled = false;
	}
}
