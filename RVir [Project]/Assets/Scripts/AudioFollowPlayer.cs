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
        audioSourceHaptic = gameObject.transform.Find("AudioSourceHaptic");

    }

    void Update()
    {
        PositionAudioClosestToPlayer();
    }

    public void PositionAudioClosestToPlayer()
    {
        
        Collider collider = GetComponent<BoxCollider>();
        if (transform.name == "Objective") collider = GetComponent<SphereCollider>();

        Vector3 bestCandidate = collider.ClosestPoint(playerHead.position);
        float bestDistance = Vector3.Distance(playerHead.position, bestCandidate);

        List<BoxCollider> childColliders = new List<BoxCollider>(GetComponentsInChildren<BoxCollider>());

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
        audioSourceHaptic.position = bestCandidate;
    }
}
