using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public Transform[] characters;
	public Font f;
	public GameObject text;

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
				dialogue.GetComponent<AudioSource> ().spatialBlend = 1;
				dialogue.GetComponent<AudioSource> ().minDistance = 1;
				dialogue.GetComponent<AudioSource> ().maxDistance = 5;
				tr.GetComponent <CharacterCaracteristics> ().saying = dialogue.GetComponent<AudioSource> ();
				tr.GetComponent <CharacterCaracteristics> ().saying.Play ();

				dialogue.transform.localPosition = new Vector3 (0, tr.gameObject.GetComponent<MeshFilter> ().mesh.bounds.extents.y*1.8f, 0);
				tr.GetComponent <CharacterCaracteristics> ().cameraParent = dialogue.GetComponent<Transform> ();


				GameObject name = GameObject.Instantiate (text);
				name.transform.parent = tr;
				name.transform.localPosition = new Vector3 (0, tr.gameObject.GetComponent<MeshFilter> ().mesh.bounds.extents.y*2.2f, 0);
				name.GetComponent<TextMesh>().text = tr.name;
				name.GetComponent<TextMesh> ().characterSize = 0;
				tr.GetComponent<CharacterCaracteristics> ().name = name.GetComponent<TextMesh>();

			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
