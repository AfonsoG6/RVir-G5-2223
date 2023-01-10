using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WristUI : MonoBehaviour
{
	private Color GREEN = new Color(130, 200, 125, 1);
	private Color RED = new Color(200, 125, 125, 1);
	private bool passiveSoundEnabled = true;
	private bool helperRingEnabled = true;
	// --------------------------------------------------------------------------------------------
	private Canvas wristUICanvas;
	public InputActionReference menuToggleRef;
	// --------------------------------------------------------------------------------------------
	public int volumeValue;
	private Slider volumeSlider;
	private TMP_Text volumeText;
	// --------------------------------------------------------------------------------------------
	public int sonarRadiusValue;
	private Slider sonarRadiusSlider;
	private TMP_Text sonarRadiusText;
	// --------------------------------------------------------------------------------------------
	private GameObject helperRingObject;
	// --------------------------------------------------------------------------------------------
	private GameObject lightsObject;
	private GameObject directionalLight;
	// --------------------------------------------------------------------------------------------


	void Start()
	{
		wristUICanvas = GetComponent<Canvas>();
		wristUICanvas.enabled = false;

		volumeSlider = transform.GetChild(2).GetComponent<Slider>();
		volumeValue = (int)volumeSlider.value;
		volumeText = volumeSlider.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
		volumeText.SetText(volumeValue.ToString());

		sonarRadiusSlider = transform.GetChild(3).GetComponent<Slider>();
		sonarRadiusValue = (int)sonarRadiusSlider.value;
		sonarRadiusText = sonarRadiusSlider.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
		sonarRadiusText.SetText(sonarRadiusValue.ToString());

		// If we're in the tutorial scene, then turn on the lights
		string sceneName = SceneManager.GetActiveScene().name;

		directionalLight = GameObject.Find("Directional Light");
		directionalLight.SetActive(false);

		lightsObject = GameObject.Find("Lighting");
		lightsObject.SetActive(false);

		helperRingObject = GameObject.Find("ProximityRingManager");

		if (sceneName == "Tutorial Scene")
		{
			directionalLight.SetActive(true);
			lightsObject.SetActive(true);
		}
	}

	private void OnDestroy()
	{
		menuToggleRef.action.performed -= ToggleMenu;
	}

	public void ToggleMenu(InputAction.CallbackContext ctx)
	{
		wristUICanvas.enabled = !wristUICanvas.enabled;
	}

	public void updateVolume()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PassiveSound"))
		{
			AudioSource audioSource = obj.GetComponent<AudioSource>();
			audioSource.volume = volumeValue / (volumeSlider.maxValue - volumeSlider.minValue);
		}
	}

	public void IncreaseVolume()
	{
		if (volumeValue < volumeSlider.maxValue)
		{
			volumeValue++;
		}
		volumeSlider.value = volumeValue;
		volumeText.SetText(volumeValue.ToString());
		updateVolume();
	}

	public void DecreaseVolume()
	{
		if (volumeValue > volumeSlider.minValue)
		{
			volumeValue--;
		}
		volumeSlider.value = volumeValue;
		volumeText.SetText(volumeValue.ToString());
		updateVolume();
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

	public void ResetLevel()
	{
		GameObject.Find("LevelManager").GetComponent<LevelManager>().startLevel();
	}

	public void ToggleLights()
	{
		lightsObject.SetActive(!lightsObject.activeSelf);
		directionalLight.SetActive(!directionalLight.activeSelf);
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

	public void ToggleProximityRing(GameObject buttonObject)
	{
		helperRingEnabled = !helperRingEnabled;
		Image buttonImage = buttonObject.GetComponent<Image>();
		buttonImage.color = helperRingEnabled ? RED : GREEN;

		helperRingObject.SetActive(helperRingEnabled);
	}

	public void Subscribe()
	{
		if (menuToggleRef == null) return;
		menuToggleRef.action.Enable();
		menuToggleRef.action.performed += ToggleMenu;
	}

	public void Unsubscribe()
	{
		if (menuToggleRef == null) return;
		menuToggleRef.action.performed -= ToggleMenu;
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
		if (newActionRef == null || menuToggleRef == newActionRef) return;

		Unsubscribe();
		menuToggleRef = newActionRef;
		Subscribe();
	}


}
