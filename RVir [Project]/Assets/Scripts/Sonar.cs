using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sonar : MonoBehaviour
{
    public InputActionAsset inputActions;
    public GameObject sonarWave;
    public float sonarVelocity;

    private InputAction shoot;

    void Start()
    {
        shoot = inputActions.FindActionMap("XRI RightHand Interaction").FindAction("Shoot");
    }

    void Update()
    {
        if(shoot.IsPressed())
        {
            GameObject wave = Instantiate(sonarWave, transform.position, sonarWave.transform.rotation);

            wave.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, sonarVelocity, 0));        
        }
    }
}
