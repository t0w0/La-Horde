using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Targetting : MonoBehaviour 
{

	public MeshCollider targettedObject;	// Objet visé, null si aucun objet visé
	private cameraManager camManag;
	public HUDManager hudManag;
	public CharactersManager charManag;

	private int RAYCASTLENGTH = 100;	// Longueur du rayon issu de la caméra
	public float timer = 0;
	public float more = 0.0025f;
	public float less = 0.01f;

	public Material[] hoverMaterial;
	public Material[] originalMat;
	public Transform hoverCharacter;
	public Image circle;

	void Start () 
	{	
		camManag = transform.parent.GetComponent<cameraManager> ();
	}

	void Update ()
	{
		// Le raycast attache un objet séléctionné
		RaycastHit hitInfo;
		Ray ray = GetComponentInChildren<Camera> ().ScreenPointToRay (new Vector3 (Screen.width/2, Screen.height/2, 0));
		//Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(transform.forward);
		Debug.DrawRay (ray.origin, ray.direction * RAYCASTLENGTH, Color.blue);
		bool rayCasted = Physics.Raycast (ray, out hitInfo, RAYCASTLENGTH);

		if (rayCasted) {
			rayCasted = hitInfo.transform.CompareTag ("Character");
			if (rayCasted) {
				if (hoverCharacter == null) {

					targettedObject = hitInfo.transform.GetComponent<MeshCollider>();
					originalMat = hitInfo.transform.GetComponentInChildren<SkinnedMeshRenderer>().materials;
					hitInfo.transform.GetComponentInChildren<SkinnedMeshRenderer>().materials = hoverMaterial;
					hoverCharacter = hitInfo.transform;
					hudManag.Show(charManag.GetCharacterIndex (targettedObject));
				}
				Transit ();
			} 
		}
		else {
			Untransit ();

			if (hoverCharacter != null) {
				hoverCharacter.GetComponentInChildren<SkinnedMeshRenderer>().materials = originalMat;
				hoverCharacter = null;
			}

		}
}

	public void Transit () {
		if (timer < 1f) {
			timer += more;
			circle.fillAmount = timer;
		}
		else {
			//Debug.Log (targettedObject);
			camManag.ActualiseCharacter (targettedObject);
			timer = 0;
			circle.fillAmount = timer;
		}
	}

	public void Untransit () {
		if (timer > 0f) {
			timer -= less;
			circle.fillAmount = timer;
		}
		else {
			circle.fillAmount = timer;
		}
	}
		// rayCasted est true si un objet possédant le tag character est détécté
		/*
		if (Input.GetButtonDown ("Fire1")) { 	// L'utilisateur appuye sur le click

			if (rayCasted) {
				Cursor.SetCursor (cursorSelected, hotSpot, cursorMode);

				targettedObject = hitInfo.transform.GetComponent<CapsuleCollider>();
				//Debug.Log (targettedObject);
				camManag.ActualiseCharacter (targettedObject);

			} 
			else {
				Cursor.SetCursor (cursorOff, hotSpot, cursorMode);
			}

		}

		else  // L'utilisateur bouge la souris sans cliquer 
		{
			if (rayCasted) 
			{
				
				if (hoverCharacter == null) {
					Cursor.SetCursor (cursorHover, hotSpot, cursorMode);
					originalMat = hitInfo.transform.GetComponentInChildren<SkinnedMeshRenderer>().materials;
					hitInfo.transform.GetComponentInChildren<SkinnedMeshRenderer>().materials = hoverMaterial;
					//hitInfo.transform.GetComponentInChildren<SkinnedMeshRenderer>().transform.gameObject.layer = 5;
					//hitInfo.transform.GetComponent<CharacterCaracteristics> ().name.characterSize = 0.01f;
					hoverCharacter = hitInfo.transform;
				}
			} 
			else 
			{
				Cursor.SetCursor (cursorOff, hotSpot, cursorMode);
				if (hoverCharacter != null) {
					hoverCharacter.GetComponentInChildren<SkinnedMeshRenderer>().materials = originalMat;
					//hoverCharacter.GetComponentInChildren<SkinnedMeshRenderer>().transform.gameObject.layer = 0;
					//hoverCharacter.GetComponent<CharacterCaracteristics> ().name.characterSize = 0f;
					hoverCharacter = null;
				}

			}

		}
	}

	/*void LateUpdate () {

		if (accepted) {
			AcceptedTransition ();
		} 
		else if (selected) {
			ChargingTransition ();
		}
		else {
			if (Vector3.Distance (transform.position, lastTargettedObject.GetComponent<AgentsParameters> ().anchor.position) > 0.05f) {
				myTransform.position = Vector3.Lerp (transform.position, lastTargettedObject.GetComponent<AgentsParameters> ().anchor.position, chargingTime);

				AgentsParameters parameters = lastTargettedObject.GetComponent<AgentsParameters> ();

				GetComponentInChildren<Camera> ().fieldOfView = Mathf.Lerp (GetComponentInChildren<Camera> ().fieldOfView, parameters.fov, chargingTime);
				GetComponentInChildren<ColorCorrectionCurves> ().saturation = Mathf.Lerp (GetComponentInChildren<ColorCorrectionCurves> ().saturation, parameters.saturation, chargingTime);
				GetComponentInChildren<BlurOptimized> ().blurSize = Mathf.Lerp (GetComponentInChildren<BlurOptimized> ().blurSize, parameters.blurSize, chargingTime);
				GetComponentInChildren<DepthOfField> ().focalSize = Mathf.Lerp (GetComponentInChildren<DepthOfField> ().focalSize, parameters.focalSize, chargingTime);
				//world.transform.localScale = Vector3.Lerp (world.transform.localScale, new Vector3 (10/parameters.scaleFactor, 10/parameters.scaleFactor, 10/parameters.scaleFactor), chargingTime);

				chargingCounter = 0;
			} else {

				myTransform.position = lastTargettedObject.GetComponent<AgentsParameters> ().anchor.position;

			}
		}
	}

	public void ChargingTransition () {

		if (Vector3.Distance (myTransform.position,  lastTargettedObject.GetComponent<AgentsParameters> ().anchor.position) < chargingDist) {
		myTransform.position = Vector3.Lerp (transform.position, targettedObject.GetComponent<AgentsParameters> ().anchor.position, chargingTime);

		AgentsParameters parameters = targettedObject.GetComponent<AgentsParameters> ();

		GetComponentInChildren<Camera> ().fieldOfView = Mathf.Lerp (GetComponentInChildren<Camera> ().fieldOfView, parameters.fov, chargingTime);
		GetComponentInChildren<ColorCorrectionCurves> ().saturation = Mathf.Lerp (GetComponentInChildren<ColorCorrectionCurves> ().saturation, parameters.saturation, chargingTime);
		GetComponentInChildren<BlurOptimized> ().blurSize = Mathf.Lerp (GetComponentInChildren<BlurOptimized> ().blurSize, parameters.blurSize, chargingTime);
		GetComponentInChildren<DepthOfField> ().focalSize = Mathf.Lerp (GetComponentInChildren<DepthOfField> ().focalSize, parameters.focalSize, chargingTime);
		//world.transform.localScale = Vector3.Lerp (world.transform.localScale, new Vector3 (10/parameters.scaleFactor, 10/parameters.scaleFactor, 10/parameters.scaleFactor), chargingTime);
		}

	}

	public void AcceptedTransition () {

		myTransform.position = Vector3.Lerp (myTransform.position, targettedObject.GetComponent<AgentsParameters> ().anchor.position, transitionTime);

		AgentsParameters parameters = targettedObject.GetComponent<AgentsParameters> ();

		GetComponentInChildren<Camera> ().fieldOfView = Mathf.Lerp (GetComponentInChildren<Camera> ().fieldOfView, parameters.fov, transitionTime);
		GetComponentInChildren<ColorCorrectionCurves> ().saturation = Mathf.Lerp (GetComponentInChildren<ColorCorrectionCurves> ().saturation, parameters.saturation, transitionTime);
		GetComponentInChildren<BlurOptimized> ().blurSize = Mathf.Lerp (GetComponentInChildren<BlurOptimized> ().blurSize, parameters.blurSize, transitionTime);
		GetComponentInChildren<DepthOfField> ().focalSize = Mathf.Lerp (GetComponentInChildren<DepthOfField> ().focalSize, parameters.focalSize, transitionTime);
		//world.transform.localScale = Vector3.Lerp (world.transform.localScale, new Vector3 (20/(parameters.scaleFactor*4), 20/(parameters.scaleFactor*4), 20/(parameters.scaleFactor*4)), transitionTime);

		if (Vector3.Distance (myTransform.position, targettedObject.GetComponent<AgentsParameters> ().anchor.position) < 0.05f) {
			accepted = false;
			selected = false;
			lastTargettedObject = targettedObject;
			//chargingDist = baseDist / parameters.scaleFactor;
		}
	}*/
}