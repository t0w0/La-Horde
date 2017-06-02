using UnityEngine;
using UnityEngine.Audio;

public class AudioParticle : MonoBehaviour {

	public CharactersManager charManager;
	public AudioSource audioSource;
	public float updateStep = 0.05f;
	public int sampleDataLength = 1024;
	public float loudnessThreshold = 0.1f;
	public Color col;
	public Color maskColor;
	public Material maskMat;
	public Color maskActif;

	public int burst = 1;

	private float currentUpdateTime = 0f;

	private float clipLoudness;
	private float[] clipSampleData;

	// Use this for initialization
	void Awake () {
		clipSampleData = new float[sampleDataLength];
		audioSource = transform.parent.GetComponent<AudioSource> ();
		col = GetComponent<ParticleSystem>().startColor;
		maskMat = transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponentInChildren<SkinnedMeshRenderer> ().materials [3];
		maskColor = maskMat.color;
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

			GetComponent<ParticleSystem>().startColor = new Color (col.r, col.g, col.b, clipLoudness.Remap (0, 0.2f, 0, 1));
			GetComponent<ParticleSystem>().startSize = clipLoudness.Remap (0, 0.3f, 0, 0.2f);

			GetComponent<ParticleSystem> ().Emit (burst);
			maskMat.color = maskActif;
		} 

		else maskMat.color = maskColor;
	}

}
public static class ExtensionsMethods {
	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}

