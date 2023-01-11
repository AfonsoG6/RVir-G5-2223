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
		audioSource = GetComponent<AudioSource>();
		if (transform.parent.name != "Objective") CustomizeAudioSource();
		StartCoroutine(PlayAudio());
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

	public void PlayAudioOnceOnCollision()
	{
		audioSource.PlayOneShot(audioSource.clip);
	}

	public void CustomizeAudioSource()
	{
		// Change delay and audio source pitch depending on the object's material
		Material mat = transform.parent.GetComponent<MeshRenderer>().materials[0];
		delay = HashMaterialIntoDelay(mat);
		audioSource.pitch = HashMaterialIntoPitch(mat);
	}

	public float HashMaterialIntoPitch(Material mat)
	{
		int hash = 0;
		foreach (char c in mat.name)
		{
			hash += c * 137;
		}
		return ((hash % 200) - 100) / 100f;
	}

	public float HashMaterialIntoDelay(Material mat)
	{
		int hash = 0;
		foreach (char c in mat.name)
		{
			hash += c * 137;
		}
		return (hash % 100) / 100f;
	}
}
