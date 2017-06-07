using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

	public GameObject hudPrefab;	
	public GameObject[] hud;
	public CharactersManager charManag;
	public Transform parent;
	public float radius;


	// Use this for initialization
	void Start () {
		hud = new GameObject[9];
		for (int i = 0 ; i < charManag.characters.Length; i++) {
			hud [i] = GameObject.Instantiate (hudPrefab, parent);
			hud [i].transform.position = Vector3.zero;
			hud [i].transform.GetComponentInChildren<MeshRenderer> ().enabled = false;
		} 
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < hud.Length ; i++) {
			hud [i].transform.LookAt (Camera.main.transform);
			//hud [i].transform.position = charManag.meshes [i].bounds.center;
			hud [i].transform.position = Camera.main.transform.position;
			hud [i].transform.position = Vector3.MoveTowards (hud [i].transform.position, charManag.meshes [i].bounds.center, radius);
		}
	}

	public void Show (int index) {
		for (int i = 0; i < hud.Length; i++) {
			if (i == index) hud [i].transform.GetComponentInChildren<MeshRenderer> ().enabled = true;
			else hud [i].transform.GetComponentInChildren<MeshRenderer> ().enabled = false;
		} 
	}
}
