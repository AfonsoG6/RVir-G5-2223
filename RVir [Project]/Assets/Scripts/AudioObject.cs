using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
	[SerializeField] private float delay = 0f;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(PlayAudio());
	}

	IEnumerator PlayAudio()
	{
		yield return new WaitForSeconds(delay);
		GetComponent<AudioSource>().Play();
	}

}
