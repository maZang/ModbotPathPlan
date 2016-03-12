using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface CarControllerInt {
	Tuple<float, float> speedAndTurn(GameObject car);
}

public class CarController : CarControllerInt {

	public Tuple<float, float> speedAndTurn(GameObject car) {
		ObstacleAvoid obstacleAvoid = car.GetComponent<ObstacleAvoid> ();
		float speed = 0; 
		float steer = 0;
		if (obstacleAvoid.leftObs && !obstacleAvoid.rightObs)
			steer = Mathf.Max (1.2f/obstacleAvoid.LeftDis, 0.35f);
		if (obstacleAvoid.rightObs && !obstacleAvoid.leftObs) 
			steer = -1 * Mathf.Max (1.2f/obstacleAvoid.RightDis, 0.35f);
		if (obstacleAvoid.centerObs && !obstacleAvoid.rightObs && !obstacleAvoid.leftObs) {
			steer = Mathf.Max (1.2f/obstacleAvoid.CenterDis, 0.35f); 
		}
		if (obstacleAvoid.centerObs && (obstacleAvoid.leftObs || obstacleAvoid.rightObs)) {
			steer = steer * 1.42f; 
		}

		Kart kart = car.GetComponent<Kart> ();
		Vector3 travelDirection = car.transform.InverseTransformPoint(new Vector3 (kart.wayPoints[kart.current_point].x, 
		                                                                           car.transform.position.y, 
		                                                                           kart.wayPoints[kart.current_point].z));
		// For skipping if the waypoint is behind the car
		Vector3 relPosition = car.transform.InverseTransformPoint (kart.wayPoints [kart.current_point]);
		if (relPosition.z <= 0) {
			int see_ahead = kart.current_point + 1;
			if (see_ahead >= kart.wayPoints.Count)
				see_ahead = 0;
			Vector3 seeDirection = car.transform.InverseTransformPoint (new Vector3 (kart.wayPoints[see_ahead].x, 
			                                                                     car.transform.position.y, 
			                                                                     kart.wayPoints[see_ahead].z));
			if (seeDirection.z > 0) {
				kart.current_point = see_ahead;
				return speedAndTurn (car); 
			} 
		}

		steer = travelDirection.x / travelDirection.magnitude + steer;
		if (steer > 1) {
			//input_steer = 1;
			steer = Mathf.Min (steer, 1.25f);
		} 
		if (steer < -1) {
			//input_steer = -1; 
			steer = Mathf.Max (steer, -1.25f); 
		}
		
		if (travelDirection.magnitude < 12) {
			kart.current_point = kart.current_point + 1;
			
			if (kart.current_point >= kart.wayPoints.Count) {
				kart.current_point = 0;
			}
		}

		speed = Mathf.Sqrt (1 - (steer * steer));

		return new Tuple<float, float> (speed, steer);
	}
}

public class Tuple<T1, T2> {
	public T1 First { get; private set;}
	public T2 Second {get; private set;}
	public Tuple(T1 first, T2 second) {
		First = first;
		Second = second;
	}
}