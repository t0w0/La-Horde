using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMaterial : MonoBehaviour {

	public ParticleSystemRenderer partSystRend;
	public Material[] particlesMaterials;
	
	// Update is called once per frame
	void Update () {
		transform.GetComponent<ParticleSystemRenderer> ().material = particlesMaterials [Mathf.FloorToInt(Random.Range (0, particlesMaterials.Length))];
	}
}
