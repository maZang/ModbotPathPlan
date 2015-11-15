using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class ObstacleAvoid : MonoBehaviour {

	public float visionRange;
	public GameObject map; 
	public bool avoidObstacle;

	private List<RaycastHit> filterRaycastMap(List<RaycastHit> hits) {
		List<GameObject> travelable = new List<GameObject>(GameObject.FindGameObjectsWithTag ("non-obstacle"));
		List<RaycastHit> filteredhits = new List<RaycastHit>(); 
		foreach (RaycastHit hit in hits) {
			if (travelable.Contains(hit.collider.gameObject)) {
				continue; 
			}
			filteredhits.Add(hit); 
		}
		return filteredhits;
	}

	private Collider findClosestObstacle() {
		Collider closetObstacle = null; 
		List<RaycastHit> hits = filterRaycastMap(new List<RaycastHit>(Physics.RaycastAll (transform.position, transform.forward, visionRange)));
		float minimumDistance = visionRange;
		foreach (RaycastHit hit in hits) {
			if (hit.distance < minimumDistance) {
				closetObstacle = hit.collider; 
			}
		}
		return closetObstacle;
	}

	private void BingBangAvoidance(Collider obstacle) {

	}

	void FixedUpdate () {
		Collider obstacle = findClosestObstacle ();
		if (obstacle != null) {
			avoidObstacle = true; 
			BingBangAvoidance (obstacle);
		} else {
			avoidObstacle = false; 
		}
		//print (obstacle); 
	}
}