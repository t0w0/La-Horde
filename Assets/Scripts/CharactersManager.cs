using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public int charIndex = 0;
	public Transform[] characters;
	public Mesh[] charactersMeshes = new Mesh[9];
	public Mesh[] charactersWOHead = new Mesh[9];
	public SkinnedMeshRenderer[] meshes;
	public MeshCollider[] colliders;
	public Transform[] heads;
	public bool[] walk = new bool[9];
	public bool[] idle  = new bool[9];
	private bool[] m_walk = new bool[9];
	private bool[] m_idle = new bool[9];

	public AudioSource[] pensees;
	public AudioSource[] dialogues;

	public GameObject particlePensee;
	public GameObject particleDialogue;

	public AudioClip[] penseesClips;
	public AudioClip[] dialoguesClips;
	public AudioMixerGroup gr;

	// Use this for initialization
	void Awake () {

		heads = new Transform[characters.Length];
		meshes = new SkinnedMeshRenderer[characters.Length];
		colliders = new MeshCollider[characters.Length];
		pensees = new AudioSource[characters.Length];
		dialogues = new AudioSource[characters.Length];


		for (int i = 0 ; i < characters.Length ; i++) {
				
			meshes [i] = characters [i].GetComponentInChildren<SkinnedMeshRenderer> ();
			charactersMeshes [i] = meshes [i].sharedMesh;
			colliders [i] = characters [i].GetComponentInChildren<MeshCollider> ();
			heads [i] = meshes [i].transform.GetChild (0).GetChild (0).GetChild (0).GetChild (1).GetChild (0).GetChild (0);

			GameObject pensee = new GameObject ("pensee");
			pensee.transform.parent = heads[i];
			pensee.AddComponent<AudioSource> ();
			pensee.GetComponent<AudioSource> ().clip = penseesClips[i];
			pensee.GetComponent<AudioSource> ().volume = 0;
			pensee.transform.localPosition = Vector3.zero;
			pensees[i] = pensee.GetComponent<AudioSource> ();
			pensees[i].Play ();
			GameObject.Instantiate (particlePensee, pensee.transform);
			pensee.transform.GetChild(0).GetComponent<AudioParticle> ().index = i;
			pensee.transform.GetChild(0).GetComponent<AudioParticle> ().type = 0;


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

			GameObject.Instantiate (particleDialogue, dialogue.transform);
			dialogue.transform.GetChild(0).GetComponent<AudioParticle> ().index = i;
			dialogue.transform.GetChild(0).GetComponent<AudioParticle> ().type = 1;

		}	
	}

	void Update () {
		for (int i = 0; i < characters.Length; i++) {
			if (m_walk [i] != walk [i]) {
				m_walk [i] = walk [i];
				ActualiseState ("Walk", true, i);
			}
			if (m_idle [i] != idle [i]) {
				m_idle [i] = idle [i];
				ActualiseState ("Idle", true, i);
			}
		}
	}

	public int GetCharacterIndex (MeshCollider coll) {
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

	public void ActualiseState (string str, bool b, int index){
		
		Animator anim = meshes [index].GetComponent<Animator> ();

		if (str == "Walk") {
			anim.SetBool ("Walk", b);
			anim.SetBool ("Idle", !b);
		}
		if (str == "Idle") {
			anim.SetBool ("Walk", !b);
			anim.SetBool ("Idle", b);
		}
	}
}


