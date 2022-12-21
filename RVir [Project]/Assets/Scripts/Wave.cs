using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    float aliveTimer;

    private void Start()
    {
        aliveTimer = 0f;
    }

    private void Update()
    {
        // Change this value to set the time the wave is allowed to live
        if(aliveTimer >= 1f)
        {
            Destroy(gameObject);
        }

        aliveTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.GetComponentInChildren<AudioObject>().PlayAudioOnceOnCollision();
        }
    }
}
