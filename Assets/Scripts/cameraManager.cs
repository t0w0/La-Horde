using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

	public Transform Characters;
	public Transform Setting;
	public int beginCharacter = 0;
	private int actualCharacter;

	public Vector3 startRotation;

	private Camera m_Camera;
	private Transform m_Head;
	private Animator m_Animator;

	private AudioSource lastThought;
	private AudioSource newThought;

	private bool vifView = false;

	// Use this for initialization
	void Start () {
		m_Camera = Camera.main;
		m_Head = m_Camera.transform.parent;
		m_Animator = m_Camera.GetComponent<Animator> ();
		actualCharacter = beginCharacter;
		m_Head.transform.SetParent (Characters.GetChild(actualCharacter).GetComponent<CharacterCaracteristics> ().cameraParent);
		m_Head.transform.localPosition = Vector3.zero;
		newThought = Characters.GetChild(actualCharacter).GetComponent<CharacterCaracteristics> ().thought;
		newThought.volume = 1;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			voirLeVif ();
		}

		if (!m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("CloseEyes")) {
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				lastThought = Characters.GetChild(actualCharacter).GetComponent<CharacterCaracteristics> ().thought;
				m_Animator.SetTrigger ("Close");
				actualCharacter++;
				if (actualCharacter >= Characters.transform.childCount) {
					actualCharacter = 0;
				} 
				newThought = Characters.GetChild(actualCharacter).GetComponent<CharacterCaracteristics> ().thought;
			}

			else if (Input.GetKeyDown(KeyCode.DownArrow)) {
				lastThought = Characters.GetChild(actualCharacter).GetComponent<CharacterCaracteristics> ().thought;
				m_Animator.SetTrigger ("Close");
				actualCharacter--;
				if (actualCharacter < 0) {
					actualCharacter = Characters.childCount - 1;
				}
				newThought = Characters.GetChild(actualCharacter).GetComponent<CharacterCaracteristics> ().thought;
			}
		}

		if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName("CloseEyes") && !m_Animator.IsInTransition(0)) {
			
			if (m_Animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1){
		
				m_Head.transform.SetParent (Characters.GetChild(actualCharacter).GetComponent<CharacterCaracteristics> ().cameraParent);
				m_Head.transform.localPosition = Vector3.zero;
				lastThought.volume = 0;
				newThought.volume = 1;

				m_Animator.SetTrigger ("Open");
			}
		} 
		
	}

	void voirLeVif() {
		if (!vifView) {
			foreach (Transform tr in Characters) {
				tr.GetComponent<SkinnedMeshRenderer> ().enabled = false;
				tr.GetComponentInChildren<ParticleSystem> ().Play();

			}
			foreach (Transform tr in Setting) {
				tr.GetComponent<MeshRenderer> ().enabled = false;
			}
			m_Camera.GetComponent<VignetteAndChromaticAberration> ().intensity = 0.4f ;
			m_Camera.GetComponent<VignetteAndChromaticAberration> ().blur = 0.5f ;
			m_Camera.GetComponent<VignetteAndChromaticAberration> ().chromaticAberration = 25 ;
			vifView = true;
		} 
		else {
			foreach (Transform tr in Characters) {
				tr.GetComponent<SkinnedMeshRenderer> ().enabled = true;
				tr.GetComponentInChildren<ParticleSystem> ().Stop();

			}
			foreach (Transform tr in Setting) {
				tr.GetComponent<MeshRenderer> ().enabled = true;
			}
			m_Camera.GetComponent<VignetteAndChromaticAberration> ().intensity =0.036f ;
			m_Camera.GetComponent<VignetteAndChromaticAberration> ().blur = 0 ;
			m_Camera.GetComponent<VignetteAndChromaticAberration> ().chromaticAberration = 0.2f ;
			vifView = false;
		}

	}
}
