using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class ObstacleAvoid : MonoBehaviour {
	
	public float angleRange = 90f;
	public bool avoidObstacle;
	public float angleObstacle;
	public Vector3 obstaclePosition;
	public Collider obstacleObject; 
	public float carRadiusApprox = 3;

	private SphereCollider col;

	void Awake () {
		col = GetComponent<SphereCollider>();
	}

	void Update () {
		//print (avoidObstacle);
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "non-obstacle") {
			avoidObstacle = false; 
			Vector3 frontposition = 0.5f * (transform.GetChild(0).position + transform.GetChild (1).position);
			RaycastHit hit;
			if (Physics.SphereCast(frontposition, carRadiusApprox, transform.forward, out hit, col.radius)) {
				if (hit.collider.gameObject.tag != "non-obstacle") {
					Vector3 obstacle = hit.point;
					Vector3 direction = obstacle - frontposition; 
					float angle = Vector3.Angle(direction, transform.forward); 
					if (angle < angleRange / 2.0f) {
						avoidObstacle = true;
						angleObstacle = angle; 
						obstaclePosition = hit.point;
						obstacleObject = hit.collider;
						print ("object: " + hit.collider.gameObject + " angle: " + angle);
					}
				}
			}

		}
	}
}