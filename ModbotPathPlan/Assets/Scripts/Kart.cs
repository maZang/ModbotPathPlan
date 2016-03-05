using UnityEngine;
using System;
using System.Collections.Generic;

public class Kart : MonoBehaviour 
{
	public List<Vector3> wayPoints; //list of waypoints for the car
	public int current_point; //the current waypoint

	public void SetUpKart (List<Vector3> wayPoints, int current_point) {
		this.wayPoints = wayPoints;
		this.current_point = current_point;
	}
}