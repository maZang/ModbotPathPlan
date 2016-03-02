using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface CarControllerInt {
	Tuple<int, int> speedAndTurn(GameObject car);
}

public class CarController : CarControllerInt {

	public Tuple<int, int> speedAndTurn(GameObject car) {
		ObstacleAvoid obstacleAvoid = car.GetComponent<ObstacleAvoid> ();
		int speed, steer;
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
		speed = Mathf.Sqrt (1 - steer ** 2);
		return Tuple<int, int> (speed, steer);
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