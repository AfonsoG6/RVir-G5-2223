using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	[SerializeField] private Room room;
	[SerializeField] private Type type;

	public Room getRoom() { return room; }
	public Type getType() { return type; }

	public enum Type
	{
		User,
		Objective
	}
}
