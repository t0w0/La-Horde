using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

	public Transform Characters;
	public Transform Setting;
	public int beginCharacter = 0;
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
		actualCharacter = Characters.GetChild(beginCharacter);
		m_Head.transform.SetParent (actualCharacter.GetComponentInChildren<CharacterCaracteristics> ().cameraParent);
		m_Head.transform.localPosition = Vector3.zero;
		newThought = actualCharacter.GetComponentInChildren<CharacterCaracteristics> ().thought;
		newThought.volume = 1;
		actualCharacter.GetComponent<MeshCollider> ().enabled = false;
		actualCharacter.GetComponent<MeshRenderer> ().enabled = false;
	}

	void Update () {

		if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName("CloseEyes") && !m_Animator.IsInTransition(0)) {
			
			if (m_Animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1){
		
				m_Head.transform.SetParent (actualCharacter.GetComponent<CharacterCaracteristics> ().cameraParent);
				m_Head.transform.localPosition = Vector3.zero;
				lastThought.volume = 0;
				newThought.volume = 1;
				actualCharacter.GetComponent<MeshCollider> ().enabled = false;
				actualCharacter.GetComponent<MeshRenderer> ().enabled = false;

				m_Animator.SetTrigger ("Open");
			}
		} 
	}

	public void ActualiseCharacter (Transform charact) {
		if (!m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("CloseEyes")) {
			lastThought = actualCharacter.GetComponentInChildren<CharacterCaracteristics> ().thought;
			actualCharacter.GetComponent<MeshCollider> ().enabled = true;
			actualCharacter.GetComponent<MeshRenderer> ().enabled = true;
			m_Animator.SetTrigger ("Close");
			newThought = charact.GetComponent<CharacterCaracteristics> ().thought;
			actualCharacter = charact;
		}
	}
}
