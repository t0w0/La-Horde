using UnityEngine;
using System.Collections;

/**
 * Cette classe permet de créer un rayon partant de la caméra en direction de la position du curseur dans l'environnement 3D.
 * L'objet portant se script peut séléctionner des objets et se téléporter à leur position.
 * Trois curseurs sont implémentés : 
 * Un curseur cursorOff lorsqu'aucun objet selectionnable (tag "Target") n'est détecté par le rayon.
 * Un curseur cursorHover lorsqu'un objet séléctionnable est détécté mais non séléctionné
 * Un curseur cursorSelected lorsqu'un objet est selectionné
**/
public class Targetting: MonoBehaviour 
{

	public GameObject world;
	private Transform myTransform;
	public Transform lastTargettedObject;
	public Transform targettedObject;	// Objet visé, null si aucun objet visé
	private cameraManager camManag;

	private int RAYCASTLENGTH = 100;	// Longueur du rayon issu de la caméra

	private CursorMode cursorMode = CursorMode.Auto;
	private Vector2 hotSpot = new Vector2(256, 256);	// Offset du centre du curseur
	public Texture2D cursorOff, cursorSelected, cursorHover;	// Textures à appliquer aux curseurs

	public bool selected = false;
	public bool accepted = false;

	public float chargingDist = 10;
	private float baseDist;
	public int chargingCounter = 0;
	public float transitionTime = 0.2f;
	public float chargingTime = 0.05f;

	public Material[] hoverMaterial;
	public Material[] originalMat;
	public Transform hoverCharacter;

	void Start () 
	{	
		camManag = transform.parent.GetComponent<cameraManager> ();
		baseDist = chargingDist;
		myTransform = transform.parent.parent;
		//lastTargettedObject = transform.GetComponent<MenuManager>().startAgent;
		//targettedObject = transform.GetComponent<MenuManager>().startAgent;

		Cursor.SetCursor (cursorOff, hotSpot, cursorMode);
		Cursor.visible = true;
	}

	void Update () 
	{
		// Le raycast attache un objet séléctionné
		RaycastHit hitInfo;
		Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
		//Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(transform.forward);
		Debug.DrawRay (ray.origin, ray.direction * RAYCASTLENGTH, Color.blue);
		bool rayCasted = Physics.Raycast (ray, out hitInfo, RAYCASTLENGTH);

		if (rayCasted) 
		{
			rayCasted = hitInfo.transform.CompareTag("Character");
		}
		// rayCasted est true si un objet possédant le tag character est détécté

		if (Input.GetButtonDown ("Fire1")) { 	// L'utilisateur appuye sur le click

			if (rayCasted) {
				Cursor.SetCursor (cursorHover, hotSpot, cursorMode);

				selected = true;

				targettedObject = hitInfo.transform;
				Debug.Log (targettedObject);
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
					//hitInfo.transform.GetComponent<CharacterCaracteristics> ().name.characterSize = 0.01f;
					hoverCharacter = hitInfo.transform;
				}
			} 
			else 
			{
				Cursor.SetCursor (cursorOff, hotSpot, cursorMode);
				if (hoverCharacter != null) {
					hoverCharacter.GetComponentInChildren<SkinnedMeshRenderer>().materials = originalMat;
					//hoverCharacter.GetComponent<CharacterCaracteristics> ().name.characterSize = 0f;
					hoverCharacter = null;
				}

			}
			selected = false;

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