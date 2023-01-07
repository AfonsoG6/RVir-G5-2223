using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sonar : MonoBehaviour
{
	public WristUI wristUI;
	public GameObject sonarWave;
	private LineRenderer lineRenderer;
	public float sonarVelocity;

	public InputActionReference shootRef;

	void Start()
	{
		wristUI ??= GameObject.Find("WristUICanvas").GetComponent<WristUI>();

		lineRenderer = transform.parent.GetComponent<LineRenderer>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ShootSonarWave(new InputAction.CallbackContext());
		}

		LineRenderer newLineRenderer = transform.parent.GetComponent<LineRenderer>();
		if(lineRenderer != newLineRenderer)
		{
			lineRenderer = newLineRenderer;
		}
	}

	void OnDestroy()
	{
		shootRef.action.performed -= ShootSonarWave;
	}

	public void ShootSonarWave(InputAction.CallbackContext ctx)
	{
		// Get rotation resulting from 90 degree rotation around the x-axis over the parent's rotation
		Quaternion rotation = Quaternion.FromToRotation(Vector3.up, lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0));

		GameObject wave = Instantiate(sonarWave, transform.position, rotation);
		wave.transform.localScale = new Vector3(wristUI.sonarRadiusValue * 0.1f, wave.transform.localScale.y, wristUI.sonarRadiusValue * 0.1f);
		wave.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, sonarVelocity, 0));
	}

    public void Subscribe()
    {
        if (shootRef == null) return;
        shootRef.action.Enable();
        shootRef.action.performed += ShootSonarWave;
    }

    public void Unsubscribe()
    {
        if (shootRef == null) return;
        shootRef.action.performed -= ShootSonarWave;
    }

    void OnEnable()
    {
        Subscribe();
    }

    void OnDisable()
    {
        Unsubscribe();
    }

    public void SetInputAction(InputActionReference newActionRef)
    {
        if (newActionRef == null || shootRef == newActionRef) return;

        Unsubscribe();
        shootRef = newActionRef;
        Subscribe();
    }
}
