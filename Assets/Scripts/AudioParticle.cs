using UnityEngine;
using UnityEngine.Audio;

public class AudioParticle : MonoBehaviour {

	public CharactersManager charManager;
	public HUDManager hudManager;
	private AudioSource audioSource;
	public float updateStep = 0.05f;
	public int sampleDataLength = 1024;
	public float loudnessThreshold = 0.1f;
	private Color col;
	public int index;
	public int type;
	public float timer;

	public int burst = 1;
	public float maxSize = 0.5f;
	public float minSize = 0.1f;
	public float maxAlpha = 0;
	public float minAlpha = 1;

	private float currentUpdateTime = 0f;

	private float clipLoudness;
	private float[] clipSampleData;

	// Use this for initialization
	void Awake () {
		clipSampleData = new float[sampleDataLength];
		audioSource = transform.parent.GetComponent<AudioSource> ();
		hudManager = GameObject.Find ("HudManager").GetComponent<HUDManager> ();
		col = GetComponent<ParticleSystem>().startColor;
		//maskMat = transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponentInChildren<SkinnedMeshRenderer> ().materials [3];
		//maskColor = maskMat.color;
	}

	// Update is called once per frame
	void Update () {

		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep) {
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
			clipLoudness = 0f;
			foreach (var sample in clipSampleData) {
				clipLoudness += Mathf.Abs(sample);
			}

			clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
			//Debug.Log(clipLoudness);
		}
		if (clipLoudness > loudnessThreshold) {
			//Debug.Log (clipLoudness);
			if (clipLoudness > 0.1f) {
				timer = 1;
				hudManager.ActualiseHudState (index, type, true, timer);
			}
			GetComponent<ParticleSystem> ().startColor = new Color (col.r, col.g, col.b, clipLoudness.Remap (0, 0.2f, minAlpha, maxAlpha));
			GetComponent<ParticleSystem> ().startSize = clipLoudness.Remap (0, 0.3f, minSize, maxSize);

			GetComponent<ParticleSystem> ().Emit (burst);
			//maskMat.color = maskActif;
		} else { 
			timer -= 0.01f;
			hudManager.ActualiseHudState (index, type, false, timer);

		}

		//else maskMat.color = maskColor;
	}

}
public static class ExtensionsMethods {
	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}

