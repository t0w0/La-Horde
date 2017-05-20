using System.Collections;
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
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
