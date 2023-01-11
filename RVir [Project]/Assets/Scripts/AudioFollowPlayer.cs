using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFollowPlayer : MonoBehaviour
{
	public Transform playerHead;
	public Transform audioSource;
	public Transform audioSourceHaptic;

	private void Start()
	{
		playerHead = GameObject.Find("Main Camera").transform;
		audioSource = gameObject.transform.Find("AudioSource");
		if (audioSource == null) audioSource = gameObject.transform.Find("AudioSource(Clone)");
		audioSourceHaptic = gameObject.transform.Find("AudioSourceHaptic");
		if (audioSourceHaptic == null) audioSourceHaptic = gameObject.transform.Find("AudioSourceHaptic(Clone)");

	}

	void Update()
	{
		PositionAudioClosestToPlayer();
	}

	public void PositionAudioClosestToPlayer()
	{

		Collider collider = GetComponent<Collider>();

		Vector3 bestCandidate = Vector3.zero;
		float bestDistance = float.MaxValue;

		if (collider != null && (collider is BoxCollider
					|| collider is SphereCollider
					|| collider is CapsuleCollider
					|| (collider is MeshCollider && ((MeshCollider)collider).convex)))
		{
			bestCandidate = collider.ClosestPoint(playerHead.position);
			bestDistance = Vector3.Distance(playerHead.position, bestCandidate);
		}

		List<Collider> childColliders = new List<Collider>(GetComponentsInChildren<Collider>());

		foreach (var col in childColliders)
		{
			if (!(collider is BoxCollider
					|| collider is SphereCollider
					|| collider is CapsuleCollider
					|| (collider is MeshCollider && ((MeshCollider)collider).convex)))
				continue;
			var candidate = col.ClosestPoint(playerHead.position);
			var distance = Vector3.Distance(playerHead.position, candidate);
			if (distance < bestDistance)
			{
				bestDistance = distance;
				bestCandidate = candidate;
			}

		}

		if (bestDistance == float.MaxValue) return;

		audioSource.position = bestCandidate;
		audioSourceHaptic.position = bestCandidate;
	}
}
