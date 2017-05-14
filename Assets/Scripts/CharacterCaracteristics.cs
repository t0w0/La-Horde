﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCaracteristics : MonoBehaviour {

	public AudioClip thoughtClip;
	public AudioClip sayingClip;

	public AudioSource thought;
	public AudioSource saying;

	public Material vifMaterial;
	public ParticleSystemRenderer vif;

	public Transform cameraParent;



	// Use this for initialization
	void Start () {
		
		thought = transform.GetChild (0).GetComponent<AudioSource>();
		if (thoughtClip != null) thought.clip = thoughtClip;
		thought.Play ();

		saying = transform.GetChild (1).GetComponent<AudioSource>();
		if (sayingClip != null) saying.clip = sayingClip;
		saying.Play ();

		/*vif = transform.GetChild (3).GetComponent<ParticleSystemRenderer>();
		vif.material = vifMaterial;*/

		cameraParent = saying.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
