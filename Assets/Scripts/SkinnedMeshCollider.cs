using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshCollider : MonoBehaviour {

	SkinnedMeshRenderer meshRenderer;
	MeshCollider collider;

	void Start () {
		meshRenderer = GetComponent<SkinnedMeshRenderer> ();
		collider = GetComponent<MeshCollider> ();
	}

	void Update () {
		UpdateCollider ();
		//if (Input.GetButtonDown ("Fire1"))
			//UpdateCollider ();
	}

	public void UpdateCollider() {
		Mesh colliderMesh = new Mesh();
		meshRenderer.BakeMesh(colliderMesh);
		collider.sharedMesh = null;
		collider.sharedMesh = colliderMesh;
	}
}
