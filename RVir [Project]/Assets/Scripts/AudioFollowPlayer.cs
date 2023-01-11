using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFollowPlayer : MonoBehaviour
{
	public Transform playerHead;
	public Transform audioSource;

	private void Start()
	{
		playerHead = GameObject.Find("Main Camera").transform;
		audioSource = GetComponentInChildren<AudioSource>().transform;
	}

	void Update()
	{
		PositionAudioClosestToPlayer();
	}

	public void PositionAudioClosestToPlayer()
	{

		Collider collider = GetComponent<Collider>();

		Vector3 bestCandidate = collider.ClosestPoint(playerHead.position);
		float bestDistance = Vector3.Distance(playerHead.position, bestCandidate);

		List<Collider> childColliders = new List<Collider>(GetComponentsInChildren<Collider>());

		foreach (var col in childColliders)
		{
			var candidate = col.ClosestPoint(playerHead.position);
			var distance = Vector3.Distance(playerHead.position, candidate);
			if (distance < bestDistance)
			{
				bestDistance = distance;
				bestCandidate = candidate;
			}

		}

		audioSource.position = bestCandidate;
	}
}
