using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    public float duration;

    public void TriggerHaptic(XRController controller, float intensity)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}

public class HapticObject : MonoBehaviour
{
    XRController rightController;

    XRController leftController;

    public Haptic onDistance;

    public float maxDistance;

    void Awake()
    {
        rightController = GameObject.Find("RightHand Controller").GetComponent<XRController>();
        leftController = GameObject.Find("LeftHand Controller").GetComponent<XRController>();
    }

    void Update()
    {
        //float distanceToRight = Vector3.Distance(transform.position, rightController.transform.position);
        //float distanceToLeft = Vector3.Distance(transform.position, leftController.transform.position);

        //float scaledDistanceToLeft = Mathf.InverseLerp(0, maxDistance, distanceToLeft);

        onDistance.TriggerHaptic(leftController, 1);
        /*
        if (distanceToLeft < maxDistance)
        {
            
        }

        if (distanceToRight <= maxDistance)
        {
            float scaledDistanceToRight = Mathf.InverseLerp(0, maxDistance, distanceToRight);

            onDistance.TriggerHaptic(rightController, 1 - scaledDistanceToRight);
        }
        */
    }
}