﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFacing : MonoBehaviour {

	private Transform cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (cam);
		transform.localRotation = Quaternion.Euler (0, cam.transform.parent.rotation.eulerAngles.y - 90, 0);
	}
}
