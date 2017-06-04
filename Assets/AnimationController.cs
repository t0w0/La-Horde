using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	public Animator anim;
	public bool walk = false;
	public bool idle = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (walk) {
			anim.SetBool ("Walk", true);
			anim.SetBool ("Idle", false);
		}
		if (idle) {
			anim.SetBool ("Walk", false);
			anim.SetBool ("Idle", true);
		}
	}
}
