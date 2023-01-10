using System.Collections;
using System.Collections.Generic;
// Import the unity shapes namespace
using Shapes;
using UnityEngine;

public class ProximityRingManager : MonoBehaviour
{
	[SerializeField] private GameObject ringPrefab;
	Dictionary<Collider, GameObject> rings;
	float radius;
	// Start is called before the first frame update
	void Start()
	{
		radius = GetComponent<CapsuleCollider>().radius;
		rings = new Dictionary<Collider, GameObject>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Triggered by " + other.gameObject.name);

		if (!other.gameObject.CompareTag("Obstacle")) return;

		if (!rings.ContainsKey(other)) createRing(other);
		updateRing(other);
	}

	void OnTriggerStay(Collider other)
	{
		if (!other.gameObject.CompareTag("Obstacle")) return;

		if (!rings.ContainsKey(other)) createRing(other);
		updateRing(other);
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Obstacle")) return;

		if (rings.ContainsKey(other)) destroyRing(other);
	}

	void createRing(Collider other)
	{
		GameObject ring = Instantiate(ringPrefab, transform.position, Quaternion.identity, transform);
		ring.transform.localRotation = Quaternion.Euler(0, 0, 0);
		rings.Add(other, ring);
	}

	void updateRing(Collider other)
	{
		Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
		Disc disc = rings[other].GetComponent<Disc>();

		closestPoint.y = transform.position.y;

		float distanceRatio = Vector3.Distance(transform.position, closestPoint) / radius;
		if (distanceRatio < 0.05) disc.Thickness = 0f;
		else disc.Thickness = 0.1f * (1 - distanceRatio);

		Vector3 direction = closestPoint - transform.position;

		Debug.DrawLine(transform.position, closestPoint, Color.red, 1f);
		float angle = Quaternion.FromToRotation(transform.right, direction).eulerAngles.y;

		float halfAngleRange = 10 + 20 * (1 - distanceRatio);

		disc.AngRadiansStart = Mathf.Deg2Rad * (angle - halfAngleRange);
		disc.AngRadiansEnd = Mathf.Deg2Rad * (angle + halfAngleRange);
	}

	void destroyRing(Collider other)
	{
		Destroy(rings[other]);
		rings.Remove(other);
	}
}
