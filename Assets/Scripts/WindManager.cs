using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour {

	public float windStrength = 1.0f;
	public float windDir = 1.0f;
	public float delta = 0.1f;
	public bool rain = false;

	public GameObject horde;
	private WindZone wz;
	private ParticleSystem.EmissionModule[] pe;
	private ParticleSystem.EmissionModule r;

	// Use this for initialization
	void Start () {
		horde = GameObject.Find("Horde");
		wz = GetComponent<WindZone> ();
		pe = new ParticleSystem.EmissionModule[2];
		pe[0] = transform.GetChild (0).GetChild (0).GetComponent<ParticleSystem>().emission;
		pe [1] = transform.GetChild (0).GetChild (1).GetComponent<ParticleSystem> ().emission;
		r =  transform.GetChild (2).GetComponent<ParticleSystem>().emission;
		ActualiseWind ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = horde.transform.position;
		if (Input.GetKeyDown (KeyCode.UpArrow)) { 
			windStrength += delta;
			ActualiseWind ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) { 
			windStrength -= delta;
			ActualiseWind ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			transform.Rotate (new Vector3 (0, delta*150, 0));
			ActualiseWind ();
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			transform.Rotate (new Vector3 (0, -delta*150, 0));

		}
		else if (Input.GetKeyDown (KeyCode.Space)) {
			if (rain == false)
				rain = true; 
			else
				rain = false;
			ActualiseWind ();
		}
		
	}

	void ActualiseWind () {
		wz.windMain = windStrength * 10;
		pe[0].rate = windStrength * 100;
		pe[1].rate = windStrength * 100;
		if (rain == true)
			r.rate = 250;
		else 
			r.rate  = 0;
	}
}
