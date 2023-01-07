using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private Dictionary<Room, List<Vector3>> userSpawnPoints = new Dictionary<Room, List<Vector3>>();
	private Dictionary<Room, List<Vector3>> objectiveSpawnPoints = new Dictionary<Room, List<Vector3>>();

	// Start is called before the first frame update
	void Start()
	{
		collectSpawnPoints();

		startLevel();
	}

	private void collectSpawnPoints()
	{
		// for each possible room value, create a new list of spawn points
		foreach (Room room in System.Enum.GetValues(typeof(Room)))
		{
			userSpawnPoints.Add(room, new List<Vector3>());
			objectiveSpawnPoints.Add(room, new List<Vector3>());
		}

		foreach (SpawnPoint spawnPoint in FindObjectsOfType<SpawnPoint>())
		{
			if (spawnPoint.getType() == SpawnPoint.Type.User)
			{
				Vector3 spawnPointPosition = spawnPoint.transform.position;
				spawnPointPosition.y = 1;
				userSpawnPoints[spawnPoint.getRoom()].Add(spawnPointPosition);
			}
			else if (spawnPoint.getType() == SpawnPoint.Type.Objective)
			{
				objectiveSpawnPoints[spawnPoint.getRoom()].Add(spawnPoint.transform.position);
			}
			// Deactivate the spawn point
			spawnPoint.gameObject.SetActive(false);
		}

		// for each room, print the number of user and objective spawn points
		foreach (Room room in System.Enum.GetValues(typeof(Room)))
		{
			Debug.Log("Room " + room + " has " + userSpawnPoints[room].Count + " user spawn points and " + objectiveSpawnPoints[room].Count + " objective spawn points");
		}

	}

	public void startLevel()
	{
		// Select a random room for the user to start in
		Room userRoom = (Room)Random.Range(0, System.Enum.GetValues(typeof(Room)).Length);
		// Select a different random room for the objective to be in
		Room objectiveRoom;
		objectiveRoom = (Room)Random.Range(0, System.Enum.GetValues(typeof(Room)).Length - 1);
		if (objectiveRoom >= userRoom)
		{
			objectiveRoom++;
		}

		// Select a random spawn point from the user's room
		Vector3 userSpawnPoint = userSpawnPoints[userRoom][Random.Range(0, userSpawnPoints[userRoom].Count)];
		// Select a random spawn point from the objective's room
		Vector3 objectiveSpawnPoint = objectiveSpawnPoints[objectiveRoom][Random.Range(0, objectiveSpawnPoints[objectiveRoom].Count)];

		// Spawn the user and objective
		GameObject.Find("XR Origin").transform.position = userSpawnPoint;
		GameObject.Find("Objective").transform.position = objectiveSpawnPoint;
	}
}
