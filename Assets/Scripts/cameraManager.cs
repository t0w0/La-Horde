using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

	public CharactersManager charManag;

	public Transform Characters;
	public Transform Setting;
	public int beginCharacter = 0;
	public int indexCharacter;
	public Transform actualCharacter;

	public Vector3 startRotation;

	private Camera m_Camera;
	private Transform m_Head;
	private Animator m_Animator;

	private AudioSource lastThought;
	private AudioSource newThought;

	void Start () {
		m_Camera = Camera.main;
		m_Head = m_Camera.transform.parent;
		m_Animator = m_Camera.GetComponent<Animator> ();		

		indexCharacter = beginCharacter;

		m_Head.transform.SetParent (charManag.heads[indexCharacter]);
		m_Head.transform.localPosition = Vector3.zero;

		newThought = charManag.pensees[indexCharacter];
		newThought.volume = 1;
		GetComponent<AudioSourceLoudnessTester> ().audioSource = newThought;

		charManag.colliders[indexCharacter].enabled = false;
		charManag.meshes[indexCharacter].enabled = false;

	}

	void Update () {

		if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName("CloseEyes") && !m_Animator.IsInTransition(0)) {
			
			if (m_Animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1){
		
				m_Head.transform.SetParent (charManag.heads[indexCharacter]);
				m_Head.transform.localPosition = Vector3.zero;
				lastThought.volume = 0;
				newThought.volume = 1;
				charManag.colliders[indexCharacter].enabled = false;
				charManag.meshes[indexCharacter].enabled = false;

				m_Animator.SetTrigger ("Open");
			}
		} 
	}

	public void ActualiseCharacter (CapsuleCollider coll) {
		if (!m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("CloseEyes")) {
			lastThought = charManag.pensees[indexCharacter];
			charManag.colliders[indexCharacter].enabled = true;
			charManag.meshes[indexCharacter].enabled = true;
			m_Animator.SetTrigger ("Close");

			indexCharacter = charManag.GetCharacterIndex (coll);
			newThought = charManag.pensees[indexCharacter];
			GetComponent<AudioSourceLoudnessTester> ().audioSource = newThought;
		}
	}
}
