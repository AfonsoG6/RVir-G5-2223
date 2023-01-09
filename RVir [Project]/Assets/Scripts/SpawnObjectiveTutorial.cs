using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectiveTutorial : MonoBehaviour
{
    public GameObject objective;

    void Start()
    {
        objective.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        objective.SetActive(true);
    }
}
