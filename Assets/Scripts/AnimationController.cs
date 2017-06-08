using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	public Animator[] anims = new Animator[9];

	public bool walkAoi;
	public bool idleAoi;
	public bool handAoi;
	public bool walkCaracole;
	public bool idleCaracole;
	public bool handCaracole;
	public bool walkCoriolis;
	public bool idleCoriolis;
	public bool handCoriolis;
	public bool walkGolgoth;
	public bool idleGolgoth;
	public bool handGolgoth;
	public bool walkOroshi;
	public bool idleOroshi;
	public bool handOroshi;
	public bool walkPietro;
	public bool idlePietro;
	public bool handPietro;
	public bool walkSov;
	public bool idleSov;
	public bool handSov;
	public bool walkSteppe;
	public bool idleSteppe;
	public bool handSteppe;
	public bool walkTalweg;
	public bool idleTalweg;
	public bool handTalweg;
	public bool[] walk = new bool[9];
	public bool[] idle = new bool[9];
	public bool[] hand = new bool[9];


	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < anims.Length; i++) {
			anims [i] = transform.GetChild (i).GetChild (0).GetComponent<Animator> ();
		}
		Debug.Log (idleAoi.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		fillBools ();
		for (int i = 0; i < anims.Length; i++) {
			anims[i].SetBool("Walk", walk[i]);
			anims[i].SetBool("Idle", idle[i]);
			anims[i].SetBool("Hand", hand[i]);
		}
	}

	void fillBools() {
		
		walk [0] = walkAoi;
		idle [0] = idleAoi;
		hand [0] = handAoi;

		walk [1] = walkCaracole;
		idle [1] = idleCaracole;
		hand [1] = handCaracole;

		walk [2] = walkCoriolis;
		idle [2] = idleCoriolis;
		hand [2] = handCoriolis;

		walk [3] = walkGolgoth;
		idle [3] = idleGolgoth;
		hand [3] = handGolgoth;

		walk [4] = walkOroshi;
		idle [4] = idleOroshi;
		hand [4] = handOroshi;

		walk [5] = walkPietro;
		idle [5] = idlePietro;
		hand [5] = handPietro;

		walk [6] = walkSov;
		idle [6] = idleSov;
		hand [6] = handSov;

		walk [7] = walkSteppe;
		idle [7] = idleSteppe;
		hand [7] = handSteppe;

		walk [8] = walkTalweg;
		idle [8] = idleTalweg;
		hand [8] = handTalweg;
	}
}
