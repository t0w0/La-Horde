﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public Transform[] characters;
	public SkinnedMeshRenderer[] meshes;
	public CapsuleCollider[] colliders;
	public Transform[] heads;

	public AudioSource[] pensees;
	public AudioSource[] dialogues;

	public AudioClip[] penseesClips;
	public AudioClip[] dialoguesClips;
	public AudioMixerGroup gr;

	// Use this for initialization
	void Awake () {

		heads = new Transform[characters.Length];
		meshes = new SkinnedMeshRenderer[characters.Length];
		colliders = new CapsuleCollider[characters.Length];
		pensees = new AudioSource[characters.Length];
		dialogues = new AudioSource[characters.Length];


		for (int i = 0 ; i < characters.Length ; i++) {
				
			meshes [i] = characters [i].GetComponentInChildren<SkinnedMeshRenderer> ();
			colliders [i] = characters [i].GetComponentInChildren<CapsuleCollider> ();
			heads [i] = characters [i].GetComponentInChildren<SkinnedMeshRenderer>().transform.GetChild (0).GetChild (0).GetChild (0).GetChild (1).GetChild(0).GetChild(0);

			GameObject pensee = new GameObject ("pensee");
			pensee.transform.parent = heads[i];
			pensee.AddComponent<AudioSource> ();
			pensee.GetComponent<AudioSource> ().clip = penseesClips[i];
			pensee.GetComponent<AudioSource> ().volume = 0;
			pensee.transform.localPosition = Vector3.zero;
			pensees[i] = pensee.GetComponent<AudioSource> ();
			pensees[i].Play ();

			GameObject dialogue = new GameObject ("dialogue");
			dialogue.transform.parent = heads[i];
			dialogue.AddComponent<AudioSource> ();
			dialogue.GetComponent<AudioSource> ().clip = dialoguesClips[i];
			dialogue.GetComponent<AudioSource> ().spatialBlend = 1;
			dialogue.GetComponent<AudioSource> ().minDistance = 1;
			dialogue.GetComponent<AudioSource> ().maxDistance = 5;
			dialogue.transform.localPosition = Vector3.zero;
			dialogue.GetComponent<AudioSource> ().outputAudioMixerGroup = gr;
			dialogues[i] = dialogue.GetComponent<AudioSource> ();
			dialogues[i] .Play ();

		}	
	}

	public int GetCharacterIndex (CapsuleCollider coll) {
		int index = -1;
		for (int i = 0; i < colliders.Length; i++) {
			if (coll == colliders [i]) {
				index = i;
				break;
			}
		}
		if (index == -1)
			Debug.Log ("Impossible to find Character's Index");
		return index;
	}
}
