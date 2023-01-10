using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
	public void OnObjectiveAchieved()
	{
		GameObject obj = GameObject.Find("XR Origin");
		if (!obj) obj = GameObject.Find("XR Origin (Persistent)");

		obj.GetComponent<LevelManager>().ObjectiveAchieved();
	}
}
