using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
	[Range(0.1f, 10)]
	public float duration = 1f;

	public void TriggerHaptic(XRBaseController controller, float intensity)
	{
		if (intensity > 0)
		{
			controller.SendHapticImpulse(intensity, duration);
		}
	}
}

public class HapticObject : MonoBehaviour
{
	[SerializeField]
	ActionBasedController rightController;
	[SerializeField]
	ActionBasedController leftController;
	[SerializeField]
	AudioSource source;

	public Haptic onDistance;

	public float maxDistance = 1.5f;

	void Start()
	{
		Transform sourceTransform = transform.Find("AudioSourceHaptic");
		if (source == null) sourceTransform = transform.Find("AudioSourceHaptic(Clone)");
		source = sourceTransform.GetComponent<AudioSource>();
		rightController = GameObject.Find("RightHand Controller").GetComponent<ActionBasedController>();
		leftController = GameObject.Find("LeftHand Controller").GetComponent<ActionBasedController>();
		if (onDistance == null) onDistance = new Haptic();
	}

	void Update()
	{
		float distanceToRight = Vector3.Distance(source.transform.position, rightController.transform.position);
		float distanceToLeft = Vector3.Distance(source.transform.position, leftController.transform.position);

		if (distanceToLeft <= maxDistance)
		{
			float scaledDistanceToLeft = Mathf.InverseLerp(0, maxDistance, distanceToLeft);

			onDistance.TriggerHaptic(leftController, 1 - (scaledDistanceToLeft * scaledDistanceToLeft));
		}

		if (distanceToRight <= maxDistance)
		{
			float scaledDistanceToRight = Mathf.InverseLerp(0, maxDistance, distanceToRight);

			onDistance.TriggerHaptic(rightController, 1 - (scaledDistanceToRight * scaledDistanceToRight));
		}

	}
}