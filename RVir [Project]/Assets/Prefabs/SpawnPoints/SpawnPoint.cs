using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	[SerializeField] private Room room;
	[SerializeField] private Type type;

	public Room getRoom() { return room; }
	public Type getType() { return type; }

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public enum Type
	{
		User,
		Objective
	}
}
