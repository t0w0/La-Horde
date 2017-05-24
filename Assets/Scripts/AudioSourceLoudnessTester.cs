using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceLoudnessTester : MonoBehaviour {

	public AudioSource audioSource;
	public AudioMixer master;
	public float updateStep = 0.1f;
	public int sampleDataLength = 1024;
	public float loudnessThreshold = 0.01f;
	public int timeAheadToLookAt;
	public float freqMax;
	public float freqMin;
	public float freqStep;

	private float currentUpdateTime = 0f;

	private float clipLoudness;
	private float[] clipSampleData;

	// Use this for initialization
	void Awake () {

		if (!audioSource) {
			Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
		}
		clipSampleData = new float[sampleDataLength];

	}

	// Update is called once per frame
	void Update () {

		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep) {
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples + timeAheadToLookAt*12800); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
			clipLoudness = 0f;
			foreach (var sample in clipSampleData) {
				clipLoudness += Mathf.Abs(sample);
			}
			clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
			Debug.Log(clipLoudness);
		}
		if (clipLoudness > loudnessThreshold) {
			float freq;
			master.GetFloat("lowpassfreq", out freq);
			if (freq > freqMin) {
				freq -= freq/freqStep;
				master.SetFloat("lowpassfreq", freq);
			}

		} 
		else {
			float freq;
			master.GetFloat("lowpassfreq", out freq);
			if (freq < freqMax) {
				freq += freq/freqStep;
				master.SetFloat("lowpassfreq", freq);
			}
		}

	}

}
