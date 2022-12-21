using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
	[SerializeField] private GameObject audioSourcePrefab;
	float aliveTimer;

	private void Start()
	{
		aliveTimer = 0f;
	}

	private void Update()
	{
		// Change this value to set the time the wave is allowed to live
		if (aliveTimer >= 1f)
		{
			Destroy(gameObject);
		}

		aliveTimer += Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			AudioSource passiveAudioSource = other.gameObject.GetComponentInChildren<AudioSource>();
			AudioSource audioSource = Instantiate(audioSourcePrefab, other.ClosestPoint(transform.position), Quaternion.identity).GetComponent<AudioSource>();
			// Make all audio source parameters the same as the passive audio source except some differences
			audioSource.clip = passiveAudioSource.clip;
			audioSource.volume = passiveAudioSource.volume;
			audioSource.pitch = passiveAudioSource.pitch;
			audioSource.spatialBlend = passiveAudioSource.spatialBlend;
			audioSource.maxDistance = 15;
			audioSource.minDistance = passiveAudioSource.minDistance;
			audioSource.rolloffMode = passiveAudioSource.rolloffMode;
			// Play the audio source and destroy it after it's done playing
			audioSource.Play();
			Destroy(audioSource.gameObject, audioSource.clip.length + 5f);
		}
	}
}
