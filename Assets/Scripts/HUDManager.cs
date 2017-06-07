using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public Vector3 screenPos;
	public Vector2 onScreenPos;
	private float max;
	public Camera camera;
	public GameObject penseesPrefab;
	public GameObject dialoguesPrefab;	
	public GameObject[] penseesHuds;
	public GameObject[] dialoguesHuds;
	public Text name;
	public Image glyph;
	public string[] names;
	public Sprite[] glyphs;
	public CharactersManager charManag;
	public Transform parent;
	public float radius;


	// Use this for initialization
	void Start () {
		penseesHuds = new GameObject[9];
		dialoguesHuds = new GameObject[9];
		for (int i = 0 ; i < charManag.characters.Length; i++) {
			penseesHuds [i] = GameObject.Instantiate (penseesPrefab, parent);
			penseesHuds [i].transform.position = Vector3.zero;
			penseesHuds [i].transform.GetComponentInChildren<Image> ().enabled = false;
			dialoguesHuds [i] = GameObject.Instantiate (dialoguesPrefab, parent);
			dialoguesHuds [i].transform.position = Vector3.zero;
			dialoguesHuds [i].transform.GetComponentInChildren<Image> ().enabled = false;
		} 
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < penseesHuds.Length ; i++) {

			screenPos = Camera.main.WorldToViewportPoint(charManag.heads[i].position);
			float angle = 0;
			if (screenPos.z > 0 && screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1 ) {
				screenPos = new Vector2 (screenPos.x*Screen.width - Screen.width/2, screenPos.y*Screen.height - Screen.height/2);
			} 
			else {
				if (screenPos.z < 0) {
					//screenPos *= -1;
					screenPos.x = - screenPos.x;
					screenPos.y = - screenPos.y;
				}

				Vector3 screenCenter = new Vector3 (Screen.width, Screen.height, 0) / 2;
				//screenPos -= screenCenter;

				angle = Mathf.Atan2 (screenPos.y, screenPos.x);
				angle -= 90 * Mathf.Deg2Rad;

				float cos = Mathf.Cos (angle);
				float sin = -Mathf.Sin (angle);

				screenPos = screenCenter + new Vector3 (sin * 150, cos * 150, 0);

				float m = cos / sin;

				Vector3 screenBounds = screenCenter * 0.9f;
				//Debug.Log (screenBounds);

				if (cos > 0) {
					screenPos = new Vector3(screenBounds.y/m, screenBounds.y, 0);
				}
				else{
					screenPos = new Vector3(-screenBounds.y/m, -screenBounds.y,0);
				}

				if (screenPos.x > screenBounds.x) {
					screenPos = new Vector3(screenBounds.x, screenBounds.x*m, 0);
				}else if (screenPos.x < -screenBounds.x) {
					screenPos = new Vector3 (-screenBounds.x, -screenBounds.x * m,0);
				}
				//screenPos += screenCenter;


				//Debug.Log ("Calc");
				//onScreenPos = new Vector2 (screenPos.x - 0.5f, screenPos.y - 0.5f) * 2;
				//max = Mathf.Max (Mathf.Abs (onScreenPos.x), Mathf.Abs (onScreenPos.y));
				//onScreenPos = (onScreenPos / (max * 2))+new Vector2(0.5f, 0.5f);
				//screenPos = new Vector2 (screenPos.x*Screen.width - Screen.width/2, screenPos.y*Screen.height - Screen.height/2);
				//Debug.Log (screenPos);
			}
				
			penseesHuds [i].transform.localPosition = screenPos;
			dialoguesHuds [i].transform.localPosition = screenPos;
			penseesHuds [i].transform.localRotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg);
			dialoguesHuds [i].transform.localRotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg);
		}
	}

	public void ActualiseHudState (int index, int type, bool state, float timer) {
		timer = (state) ? timer = 1 : timer -= 0.01f; 
		if (type == 0) {
			penseesHuds [index].transform.GetComponentInChildren<Image> ().enabled = true;
			penseesHuds [index].transform.GetComponentInChildren<Image> ().color = new Color (255, 255, 255, timer);
		} else if (type == 1) {
			dialoguesHuds [index].transform.GetComponentInChildren<Image> ().enabled = true;
			dialoguesHuds [index].transform.GetComponentInChildren<Image> ().color = new Color (255, 255, 255, timer);
		} 
	}

	public void Show (int index, bool state) {
		name.enabled = state;
		glyph.enabled = state;
		name.text = names [index];
		glyph.sprite = glyphs [index];
	}

}
