using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public Transform[] characters;
	//public Font f;
	//public GameObject text;
	public AudioClip[] pensees;
	public AudioClip[] dialogues;
	public AudioMixerGroup gr;

	// Use this for initialization
	void Awake () {

		for (int i = 0 ; i < characters.Length ; i++) {
				
			characters[i].gameObject.AddComponent <CharacterCaracteristics> ();
			characters[i].gameObject.GetComponent <CharacterCaracteristics> ().enabled = true;

			GameObject pensee = new GameObject ("pensee");
			pensee.transform.parent = characters[i];
			pensee.AddComponent<AudioSource> ();
			pensee.GetComponent<AudioSource> ().clip = pensees[i];
			pensee.GetComponent<AudioSource> ().volume = 0;
			pensee.transform.localPosition = new Vector3 (0, characters[i].gameObject.GetComponent<MeshRenderer> ().bounds.extents.y*3, 0);
			characters[i].GetComponent <CharacterCaracteristics> ().thought = pensee.GetComponent<AudioSource> ();
			characters[i].GetComponent <CharacterCaracteristics> ().thought.Play ();

			GameObject dialogue = new GameObject ("dialogue");
			dialogue.transform.parent = characters[i];
			dialogue.AddComponent<AudioSource> ();
			dialogue.GetComponent<AudioSource> ().clip = dialogues[i];
			dialogue.GetComponent<AudioSource> ().spatialBlend = 1;
			dialogue.GetComponent<AudioSource> ().minDistance = 1;
			dialogue.GetComponent<AudioSource> ().maxDistance = 5;
			dialogue.transform.localPosition = new Vector3 (0, characters[i].gameObject.GetComponent<MeshFilter> ().mesh.bounds.extents.y*1.8f, 0);
			dialogue.GetComponent<AudioSource> ().outputAudioMixerGroup = gr;
			characters[i].GetComponent <CharacterCaracteristics> ().saying = dialogue.GetComponent<AudioSource> ();
			characters[i].GetComponent <CharacterCaracteristics> ().saying.Play ();
			characters[i].GetComponent <CharacterCaracteristics> ().cameraParent = dialogue.GetComponent<Transform> ();

		}	
	}
}
