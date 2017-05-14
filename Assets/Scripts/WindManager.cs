using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour {

	public GameObject particlePrefab;
	public Material [] materials;

	// Use this for initialization
	void Start () {
		foreach (Material mat in materials) {
			GameObject go = Instantiate (particlePrefab, transform) as GameObject;
			go.GetComponent<ParticleSystemRenderer> ().material = materials [transform.childCount-1];
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
