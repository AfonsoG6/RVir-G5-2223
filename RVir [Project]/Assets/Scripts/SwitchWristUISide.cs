using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void Awake()
    {
        rightController ??= GameObject.Find("RightHand Controller");
        leftController ??= GameObject.Find("LeftHand Controller");
        wristUI ??= GameObject.Find("WristUICanvas").GetComponent<WristUI>();
        spawnPoint ??= GameObject.Find("SpawnPoint").GetComponent<Sonar>();
    }

    public void SwitchSideToRight()
    {
        wristUI.gameObject.transform.parent = rightController.transform;
        spawnPoint.gameObject.transform.parent = leftController.transform;
        SwitchInputActions();
    }

    public void SwicthSideToLeft()
    {
        wristUI.gameObject.transform.parent = leftController.transform;
        spawnPoint.gameObject.transform.parent = rightController.transform;
        SwitchInputActions();
    }

    private void SwitchInputActions()
    {
        wristUI.SetInputAction(spawnPoint.shootRef);
        spawnPoint.SetInputAction(wristUI.menuToggleRef);
    }
}
