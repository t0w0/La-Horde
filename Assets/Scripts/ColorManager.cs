using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

	public Color[] palette;
	public GameObject[] ObjectGroup1;
	public GameObject[] ObjectGroup2;
	public GameObject[] ObjectGroup3;
	public int fogGroup = 0;


	// Use this for initialization
	void Start () {
		ApplyColor (ObjectGroup1, palette [0]);
		ApplyColor (ObjectGroup2, palette [1]);
		ApplyColor (ObjectGroup3, palette [2]);
		RenderSettings.fogColor = palette [fogGroup];
	}

	void ApplyColor (GameObject[] gr, Color c) {
		foreach (GameObject go in gr) {
			if (go.GetComponent<Camera> () != null) {
				go.GetComponent<Camera> ().backgroundColor = c;
			}
			if (go.GetComponent<ParticleSystem> ()  != null) {
				//go.GetComponent<ParticleSystem> ().main.startColor.color = c;
			}
			if (go.GetComponent<MeshRenderer> () != null) {
				go.GetComponent<MeshRenderer> ().material.color = c;
			}
			if (go.name == "Setting") {
				foreach (Transform tr in go.transform) {
					if (tr.GetComponent<MeshRenderer> () != null) {
						tr.GetComponent<MeshRenderer> ().material.color = c;
						//Debug.Log (go.name);
					}
				}
			}
			if (go.name == "Characters") {
				foreach (Transform tr in go.transform) {
					if (tr.GetComponent<SkinnedMeshRenderer> () != null) {
						foreach (Material mat in tr.GetComponent<SkinnedMeshRenderer> ().materials) {
							mat.color = c;
							//Debug.Log (tr.name);
						}
					}
				}
			}
		}
	}
}
