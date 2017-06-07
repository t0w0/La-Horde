using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public GameObject penseesPrefab;
	public GameObject dialoguesPrefab;	
	public GameObject[] penseesHuds;
	public GameObject[] dialoguesHuds;
	public Text name;
	public Image glyph;
	public string[] names;
	public Sprite[] glyphs;
	public CharactersManager charManag;
	public Transform parent;
	public float radius;


	// Use this for initialization
	void Start () {
		penseesHuds = new GameObject[9];
		dialoguesHuds = new GameObject[9];
		for (int i = 0 ; i < charManag.characters.Length; i++) {
			penseesHuds [i] = GameObject.Instantiate (penseesPrefab, parent);
			penseesHuds [i].transform.position = Vector3.zero;
			penseesHuds [i].transform.GetComponentInChildren<MeshRenderer> ().enabled = false;
			dialoguesHuds [i] = GameObject.Instantiate (dialoguesPrefab, parent);
			dialoguesHuds [i].transform.position = Vector3.zero;
			dialoguesHuds [i].transform.GetComponentInChildren<MeshRenderer> ().enabled = false;
		} 
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < penseesHuds.Length ; i++) {
			penseesHuds [i].transform.LookAt (Camera.main.transform);
			penseesHuds [i].transform.position = Camera.main.transform.position;
			penseesHuds [i].transform.position = Vector3.MoveTowards (penseesHuds [i].transform.position, charManag.pensees[i].transform.position, radius);
			dialoguesHuds [i].transform.LookAt (Camera.main.transform);
			dialoguesHuds [i].transform.position = Camera.main.transform.position;
			dialoguesHuds [i].transform.position = Vector3.MoveTowards (dialoguesHuds [i].transform.position, charManag.dialogues[i].transform.position, radius);
		}
	}

	public void ActualiseHudState (int index, int type, bool state) {
		if (type == 0) {
			penseesHuds [index].transform.GetComponentInChildren<MeshRenderer> ().enabled = state;
		} else if (type == 1) {
			penseesHuds [index].transform.GetComponentInChildren<MeshRenderer> ().enabled = state;
		} 
	}

	public void Show (int index, bool state) {
		name.enabled = state;
		glyph.enabled = state;
		name.text = names [index];
		glyph.sprite = glyphs [index];
	}

}
