using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticAudioObject : MonoBehaviour
{
    [SerializeField]
    Transform playerHead;
    [SerializeField]
    float maxDistance = 1.5f;

    AudioSource audioSource;
    float delay;

    void Start()
    {
        playerHead = GameObject.Find("Main Camera").transform;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerHead.position, transform.position);
        if(distanceToPlayer < maxDistance)
        {
            delay = distanceToPlayer;
        } else
        {
            delay = maxDistance;
        }
    }

    IEnumerator PlaySound()
    {
        while(true)
        {
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log("choo" + delay);
            yield return new WaitForSeconds(delay);
        }

    }
}
