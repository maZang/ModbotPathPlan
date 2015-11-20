using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class ObstacleAvoid : MonoBehaviour {
	
	public float angleRange =  90f;
	//public bool avoidObstacle;
	//public float angleObstacle;
	//public Vector3 obstaclePosition;
	public bool leftObs;
	public bool rightObs;
	public bool centerObs;
	public float LeftDis;
	public float CenterDis;
	public float RightDis; 
	public float carRadiusApprox = 1;

	private SphereCollider col;

	void Awake () {
		col = GetComponent<SphereCollider>();
	}

	void Update () {
		//print (avoidObstacle);
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "non-obstacle") {
			//avoidObstacle = false; 
			// make sure front wheels are first two components of the car 
			Vector3 frontposition = 0.5f * (transform.GetChild(0).position + transform.GetChild (1).position) + Vector3.up;

			RaycastHit hitleft;
			RaycastHit hitmiddle; 
			RaycastHit hitright; 
			Vector3 leftBeam = 10* transform.TransformDirection (-0.3f, 0, 0.5f);
			Vector3 rightBeam = 10 * transform.TransformDirection (0.3f, 0, 0.5f); 
			Debug.DrawRay(frontposition, leftBeam, Color.red, 0.03f);
			Debug.DrawRay (frontposition, 10* transform.forward, Color.green, 0.03f);	
			Debug.DrawRay (frontposition, rightBeam, Color.blue, 0.03f); 
			if (Physics.SphereCast(frontposition, carRadiusApprox, transform.forward, out hitmiddle, col.radius)) {
				if (hitmiddle.collider.gameObject.tag != "non-obstacle") {
					centerObs = true; 
					CenterDis = hitmiddle.distance;
				}
				else {
					centerObs = false; 
					CenterDis = 0;
				}
			} else {
				centerObs = false;
				CenterDis = 0;
			}
			if (Physics.SphereCast(frontposition, carRadiusApprox, leftBeam, out hitleft, col.radius)) {
				if (hitleft.collider.gameObject.tag != "non-obstacle") {
					leftObs = true; 
					LeftDis = hitleft.distance;
				}
				else {
					leftObs = false; 
					LeftDis = 0;
				}
			} else {
				leftObs = false; 
				LeftDis = 0;
			}
			if (Physics.SphereCast(frontposition, carRadiusApprox, rightBeam, out hitright, col.radius)) {
				if (hitright.collider.gameObject.tag != "non-obstacle") {
					rightObs = true; 
					RightDis = hitright.distance;
				}
				else {
					rightObs = false; 
					RightDis = 0;
				}
			} else {
				rightObs = false; 
				RightDis = 0;
			}
			/*RaycastHit hit;
			if (Physics.SphereCast(frontposition, carRadiusApprox, transform.forward, out hit, col.radius)) {
				if (hit.collider.gameObject.tag != "non-obstacle") {
					Vector3 obstacle = hit.point;
					Debug.DrawLine(frontposition, obstacle, Color.green, 0.1f, false);
					Vector3 direction = obstacle - frontposition; 
					float angle = Vector3.Angle(transform.forward, direction); 
					if (Vector3.Cross (transform.forward, direction).z > 0) 
						angle = angle * 1;
					else 
						angle = angle * - 1; 
					print (angle);
					if (angle < angleRange / 2.0f) {
						avoidObstacle = true;
						angleObstacle = angle; 
						obstaclePosition = hit.point;
						print ("object: " + hit.collider.gameObject + " angle: " + angle);
					}
				}
			} */

		}
	}


}