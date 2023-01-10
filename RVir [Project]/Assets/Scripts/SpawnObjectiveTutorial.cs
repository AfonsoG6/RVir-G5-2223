using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectiveTutorial : MonoBehaviour
{
    public GameObject objective;

    void Awake()
    {
        objective.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        objective.SetActive(true);
    }
}
