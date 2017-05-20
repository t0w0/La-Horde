using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour {

	public GameObject object1;
	public AudioSource audioSource;
	public AudioClip clip1;

	// Use this for initialization
	void Start () {
		object1 = this.gameObject;
		audioSource = object1.AddComponent<AudioSource> ();

		clip1 = (AudioClip) Resources.Load("sounds/pensees/caracole1");
		audioSource.clip = clip1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
