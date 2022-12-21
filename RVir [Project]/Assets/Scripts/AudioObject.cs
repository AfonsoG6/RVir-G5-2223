using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
	[SerializeField] private float delay = 0f;

	private AudioSource audioSource;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(PlayAudio());
		audioSource = GetComponent<AudioSource>();
	}

	IEnumerator PlayAudio()
	{
		yield return new WaitForSeconds(delay);
		audioSource.Play();
	}

	// Use if we want the delay after the sonar wave hits the object
	IEnumerator PlayAudioOnce()
	{
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(audioSource.clip);
    }

	public void PlayAudioOnceOnCollision() {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
