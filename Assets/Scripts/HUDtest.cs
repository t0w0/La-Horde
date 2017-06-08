using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDtest : MonoBehaviour {

	public Transform worldObject;
	public GameObject UIelem;
	public Vector3 screenPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		screenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, worldObject.position);
		UIelem.transform.localPosition = new Vector2 (screenPos.x - Screen.width/2, screenPos.y - Screen.height/2);
	}
}
