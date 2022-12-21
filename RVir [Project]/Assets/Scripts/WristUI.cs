using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WristUI : MonoBehaviour
{
	public InputActionAsset inputActions;
	private Canvas wristUICanvas;
	private InputAction menuInputAction;
	// --------------------------------------------------------------------------------------------
	private int sonarRadiusValue;
	private Slider sonarRadiusSlider;
	private TMP_Text sonarRadiusText;
	// --------------------------------------------------------------------------------------------
	private GameObject lightsObject;
	// --------------------------------------------------------------------------------------------


	void Start()
	{
		wristUICanvas = GetComponent<Canvas>();
		wristUICanvas.enabled = false;
		menuInputAction = inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("Menu");
		menuInputAction.Enable();
		menuInputAction.performed += ToggleMenu;

		sonarRadiusSlider = GetComponentInChildren<Slider>();
		sonarRadiusValue = (int)sonarRadiusSlider.value;

		sonarRadiusText = sonarRadiusSlider.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
		sonarRadiusText.SetText(sonarRadiusValue.ToString());

		lightsObject = GameObject.Find("Lighting");
	}

	void OnDestroy()
	{
		menuInputAction.performed -= ToggleMenu;
	}

	public void ToggleMenu(InputAction.CallbackContext ctx)
	{
		wristUICanvas.enabled = !wristUICanvas.enabled;
	}

	public void IncreaseSonarRadius()
	{
		if (sonarRadiusValue < 10)
		{
			sonarRadiusValue++;
		}
		sonarRadiusSlider.value = sonarRadiusValue;
		sonarRadiusText.SetText(sonarRadiusValue.ToString());
	}

	public void DecreaseSonarRadius()
	{
		if (sonarRadiusValue > 0)
		{
			sonarRadiusValue--;
		}
		sonarRadiusSlider.value = sonarRadiusValue;
		sonarRadiusText.SetText(sonarRadiusValue.ToString());
	}

	public void ToggleLights()
	{
		lightsObject.SetActive(!lightsObject.activeSelf);
	}

}
