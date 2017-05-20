using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public Transform[] characters;

	// Use this for initialization
	void Awake () {
		characters = GetComponentsInChildren<Transform> ();
		foreach (Transform tr in characters) {
			if (tr != transform) {
				tr.gameObject.AddComponent <CharacterCaracteristics> ();
				tr.gameObject.GetComponent <CharacterCaracteristics> ().enabled = true;

				GameObject pensee = new GameObject ("pensee");
				pensee.transform.parent = tr;
				pensee.AddComponent<AudioSource> ();
				pensee.GetComponent<AudioSource> ().clip = (AudioClip)Resources.Load ("sounds/pensees/P-" + tr.name);
				pensee.transform.localPosition = new Vector3 (0, tr.gameObject.GetComponent<MeshRenderer> ().bounds.extents.y*3, 0);
				tr.GetComponent <CharacterCaracteristics> ().thought = pensee.GetComponent<AudioSource> ();
				tr.GetComponent <CharacterCaracteristics> ().thought.Play ();

				GameObject dialogue = new GameObject ("dialogue");
				dialogue.transform.parent = tr;
				dialogue.AddComponent<AudioSource> ();
				dialogue.GetComponent<AudioSource> ().clip = (AudioClip)Resources.Load ("sounds/dialogues/D-" + tr.name);
				dialogue.transform.localPosition = new Vector3 (0, tr.gameObject.GetComponent<MeshRenderer> ().bounds.extents.y*3, 0);
				tr.GetComponent <CharacterCaracteristics> ().saying = dialogue.GetComponent<AudioSource> ();
				tr.GetComponent <CharacterCaracteristics> ().saying.Play ();
				tr.GetComponent <CharacterCaracteristics> ().cameraParent = dialogue.GetComponent<Transform> ();

			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
