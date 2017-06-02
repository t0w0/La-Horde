using UnityEngine;
using UnityEngine.Audio;

public class AudioParticle : MonoBehaviour {

	public AudioSource audioSource;
	public float updateStep = 0.05f;
	public int sampleDataLength = 1024;
	public float loudnessThreshold = 0.1f;

	public int burst = 1;

	private float currentUpdateTime = 0f;

	private float clipLoudness;
	private float[] clipSampleData;

	// Use this for initialization
	void Awake () {
		clipSampleData = new float[sampleDataLength];
		audioSource = transform.parent.GetComponent<AudioSource> ();
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

			transform.GetComponentInChildren<ParticleSystem>().startColor = new Color (140,192,145, clipLoudness.Remap (0, 0.3f, 0, 1));
			transform.GetComponentInChildren<ParticleSystem>().startSize = clipLoudness.Remap (0, 0.3f, 0, 0.2f);

			transform.GetChild (0).GetComponent<ParticleSystem> ().Emit (burst);
		} 
	}

}
public static class ExtensionsMethods {
	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}

