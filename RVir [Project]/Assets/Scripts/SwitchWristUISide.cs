using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

// Just an extension method for the Transform class. Syntactic sugar.
public static class SwitchExtensions
{
    public static void reparentTo(this Transform original, Transform target)
    {
        Vector3 localPosition = original.transform.localPosition;
        Quaternion localRotation = original.transform.localRotation;
        original.parent = target;
        original.localPosition = localPosition;
        original.localRotation = localRotation;
    }
}

public class SwitchWristUISide : MonoBehaviour
{
    [SerializeField]
    GameObject rightController;
    [SerializeField]
    GameObject leftController;
    [SerializeField]
    WristUI wristUI;
    [SerializeField]
    Sonar spawnPoint;
    [SerializeField]
    InputActionReference leftButton;
    [SerializeField]
    InputActionReference rightButton;

    void Awake()
    {
        rightController ??= GameObject.Find("RightHand Controller");
        leftController ??= GameObject.Find("LeftHand Controller");
        wristUI ??= GameObject.Find("WristUICanvas").GetComponent<WristUI>();
        spawnPoint ??= GameObject.Find("SpawnPoint").GetComponent<Sonar>();
    }

    public void SwitchSideToRight()
    {
        wristUI.transform.reparentTo(rightController.transform);
        spawnPoint.transform.reparentTo(leftController.transform);

        if (rightButton != null) wristUI.SetInputAction(rightButton);
        if (leftButton != null) spawnPoint.SetInputAction(leftButton);
    }

    public void SwicthSideToLeft()
    {
        wristUI.transform.reparentTo(leftController.transform);
        spawnPoint.transform.reparentTo(rightController.transform);

        if (leftButton != null) wristUI.SetInputAction(leftButton);
        if (rightButton != null) spawnPoint.SetInputAction(rightButton);
    }
}