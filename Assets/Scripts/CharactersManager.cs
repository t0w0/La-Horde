using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public Transform[] characters;
	public Transform[] heads;
	//public Font f;
	//public GameObject text;
	public AudioClip[] pensees;
	public AudioClip[] dialogues;
	public AudioMixerGroup gr;

	// Use this for initialization
	void Awake () {

		heads = new Transform[characters.Length];

		for (int i = 0 ; i < characters.Length ; i++) {
				
			characters[i].gameObject.AddComponent <CharacterCaracteristics> ();
			characters[i].gameObject.GetComponent <CharacterCaracteristics> ().enabled = true;

			heads [i] = characters [i].GetComponentInChildren<SkinnedMeshRenderer>().transform.GetChild (0).GetChild (0).GetChild (0).GetChild (1).GetChild(0).GetChild(0);

			GameObject pensee = new GameObject ("pensee");
			pensee.transform.parent = heads[i];
			pensee.AddComponent<AudioSource> ();
			pensee.GetComponent<AudioSource> ().clip = pensees[i];
			pensee.GetComponent<AudioSource> ().volume = 0;
			pensee.transform.localPosition = Vector3.zero;
			characters[i].GetComponent <CharacterCaracteristics> ().thought = pensee.GetComponent<AudioSource> ();
			characters[i].GetComponent <CharacterCaracteristics> ().thought.Play ();

			GameObject dialogue = new GameObject ("dialogue");
			dialogue.transform.parent = heads[i];
			dialogue.AddComponent<AudioSource> ();
			dialogue.GetComponent<AudioSource> ().clip = dialogues[i];
			dialogue.GetComponent<AudioSource> ().spatialBlend = 1;
			dialogue.GetComponent<AudioSource> ().minDistance = 1;
			dialogue.GetComponent<AudioSource> ().maxDistance = 5;
			dialogue.transform.localPosition = Vector3.zero;
			dialogue.GetComponent<AudioSource> ().outputAudioMixerGroup = gr;
			characters[i].GetComponent <CharacterCaracteristics> ().saying = dialogue.GetComponent<AudioSource> ();
			characters[i].GetComponent <CharacterCaracteristics> ().saying.Play ();
			characters[i].GetComponent <CharacterCaracteristics> ().cameraParent = heads[i];

		}	
	}
}
