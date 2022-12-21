using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WristUI : MonoBehaviour
{
	private Color GREEN = new Color(130, 200, 125, 1);
	private Color RED = new Color(200, 125, 125, 1);
	private bool passiveSoundEnabled = true;
	// --------------------------------------------------------------------------------------------
	public InputActionAsset inputActions;
	private Canvas wristUICanvas;
	private InputAction menuInputAction;
	// --------------------------------------------------------------------------------------------
	public int sonarRadiusValue;
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

		sonarRadiusText = sonarRadiusSlider.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
		sonarRadiusText.SetText(sonarRadiusValue.ToString());

		lightsObject = GameObject.Find("Lighting");
		lightsObject.SetActive(false);
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
		if (sonarRadiusValue < sonarRadiusSlider.maxValue)
		{
			sonarRadiusValue++;
		}
		sonarRadiusSlider.value = sonarRadiusValue;
		sonarRadiusText.SetText(sonarRadiusValue.ToString());
	}

	public void DecreaseSonarRadius()
	{
		if (sonarRadiusValue > sonarRadiusSlider.minValue)
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

	public void TogglePassiveSound(GameObject buttonObject)
	{
		passiveSoundEnabled = !passiveSoundEnabled;
		Image buttonImage = buttonObject.GetComponent<Image>();
		buttonImage.color = passiveSoundEnabled ? GREEN : RED;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PassiveSound"))
		{
			Debug.Log("Passive sound enabled for " + obj.name);
			AudioSource audioSource = obj.GetComponent<AudioSource>();
			audioSource.enabled = passiveSoundEnabled;
			if (passiveSoundEnabled) audioSource.Play();
		}
	}


}
