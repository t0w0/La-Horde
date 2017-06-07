using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

	public CharactersManager charManag;

	public Transform Characters;
	public Mesh charMesh;
	public Transform Setting;
	public int beginCharacter = 0;
	public int indexCharacter;
	public Transform actualCharacter;

	public Vector3 startRotation;

	private Camera m_Camera;
	private Transform m_Head;
	private Animator m_Animator;

	public Vector3 posInHead;

	public AudioSource lastThought;
	public AudioSource newThought;

	void Start () {
		m_Camera = Camera.main;
		m_Head = m_Camera.transform.parent;
		m_Animator = m_Camera.GetComponent<Animator> ();		

		indexCharacter = beginCharacter;
		charManag.charIndex = indexCharacter;

		m_Head.transform.SetParent (charManag.heads[indexCharacter]);
		m_Head.transform.localPosition = posInHead;

		newThought = charManag.pensees[indexCharacter];
		newThought.volume = 1;
		GetComponent<AudioSourceLoudnessTester> ().audioSource = newThought;

		charManag.colliders[indexCharacter].enabled = false;
		charManag.meshes [indexCharacter].sharedMesh = charManag.charactersWOHead [indexCharacter];

	}

	void Update () {

		if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName("CloseEyes") && !m_Animator.IsInTransition(0)) {
			
			if (m_Animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1){
		
				m_Head.transform.SetParent (charManag.heads[indexCharacter]);
				m_Head.transform.localPosition = posInHead;
				lastThought.volume = 0;
				newThought.volume = 1;
				charManag.colliders[indexCharacter].enabled = false;
				charManag.meshes[indexCharacter].sharedMesh = charManag.charactersWOHead[indexCharacter];

				m_Animator.SetTrigger ("Open");
			}
		} 
	}

	public void ActualiseCharacter (MeshCollider coll) {
		if (!m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("CloseEyes")) {
			Debug.Log (indexCharacter);
			lastThought = charManag.pensees[indexCharacter];
			charManag.colliders[indexCharacter].enabled = true;
			charManag.meshes[indexCharacter].sharedMesh = charManag.charactersMeshes[indexCharacter];

			m_Animator.SetTrigger ("Close");

			indexCharacter = charManag.GetCharacterIndex (coll);
			//Debug.Log (indexCharacter);
			newThought = charManag.pensees[indexCharacter];
			charManag.charIndex = indexCharacter;
			GetComponent<AudioSourceLoudnessTester> ().audioSource = newThought;
		}
	}
}
