using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectiveTutorial : MonoBehaviour
{
	public Vector3 originalPosition;
	public GameObject objective;

	void Start()
	{
		originalPosition = objective.transform.position;
		objective.transform.position = new Vector3(originalPosition.x, originalPosition.y + 100, originalPosition.z);
		objective.GetComponent<Rigidbody>().useGravity = false;
	}

	void OnTriggerEnter(Collider other)
	{
		objective.GetComponent<Rigidbody>().useGravity = true;
		objective.transform.position = originalPosition;
	}
}
