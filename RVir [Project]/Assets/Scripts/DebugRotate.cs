using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRotate : MonoBehaviour
{
	[SerializeField] private float _rotationSpeed = 1f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
	}
}
