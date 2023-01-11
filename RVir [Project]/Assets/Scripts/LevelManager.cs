using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private Dictionary<Room, List<Vector3>> userSpawnPoints = new Dictionary<Room, List<Vector3>>();
	private Dictionary<Room, List<Vector3>> objectiveSpawnPoints = new Dictionary<Room, List<Vector3>>();

	void Awake()
	{
		// If object with same name but persistent already exists destroy self
		if (GameObject.Find(gameObject.name + " (Persistent)")) Destroy(gameObject);
	}

	// Start is called before the first frame update
	void Start()
	{
		DontDestroyOnLoad(gameObject);
		gameObject.name += " (Persistent)";
		// If current scene is not called "Tutorial Scene", then collect spawn points and start level
		spawnAudioObjects();
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
		{
			collectSpawnPoints();
			startLevel();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			ObjectiveAchieved();
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex != 0)
		{
			spawnAudioObjects();
			collectSpawnPoints();
			startLevel();
		}
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void ObjectiveAchieved()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
		else
		{
			startLevel();
		}
	}

	private void spawnAudioObjects()
	{
		GameObject audioSourcePrefab = Resources.Load<GameObject>("Prefabs/AudioSource");
		GameObject audioSourceHapticPrefab = Resources.Load<GameObject>("Prefabs/AudioSourceHaptic");
		foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
		{
			if (!obstacle.GetComponent<AudioFollowPlayer>()) obstacle.AddComponent<AudioFollowPlayer>();
			if (!obstacle.GetComponent<HapticObject>()) obstacle.AddComponent<HapticObject>();
			if (obstacle.GetComponentInChildren<AudioSource>()) continue;
			GameObject audioSource = Instantiate(audioSourcePrefab, obstacle.transform.position, Quaternion.identity);
			audioSource.transform.parent = obstacle.transform;
			GameObject audioSourceHaptic = Instantiate(audioSourceHapticPrefab, obstacle.transform.position, Quaternion.identity);
			audioSourceHaptic.transform.parent = obstacle.transform;
		}
	}

	private void collectSpawnPoints()
	{
		// for each possible room value, create a new list of spawn points
		foreach (Room room in System.Enum.GetValues(typeof(Room)))
		{
			if (userSpawnPoints.ContainsKey(room))
				userSpawnPoints[room].Clear();
			else
				userSpawnPoints.Add(room, new List<Vector3>());

			if (objectiveSpawnPoints.ContainsKey(room))
				objectiveSpawnPoints[room].Clear();
			else
				objectiveSpawnPoints.Add(room, new List<Vector3>());
		}

		foreach (GameObject spObj in GameObject.FindGameObjectsWithTag("SpawnPoint"))
		{
			SpawnPoint spawnPoint = spObj.GetComponent<SpawnPoint>();
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
			Debug.Log("Collected Spawn point in room " + spawnPoint.getRoom() + " of type " + spawnPoint.getType() + " at " + spawnPoint.transform.position);
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
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			return;
		}
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

		Debug.Log("User is in room " + userRoom + " and objective is in room " + objectiveRoom);

		// Spawn the user and objective
		transform.position = userSpawnPoint;
		GameObject obj = GameObject.Find("Objective");
		obj.transform.position = objectiveSpawnPoint;
		obj.GetComponent<Rigidbody>().velocity = Vector3.zero;

		// Set y rotation of camera to random multiple of 10 degrees
		transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, Random.Range(0, 36) * 10, 0);
	}
}