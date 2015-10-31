using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {

	public Transform[] wheels;
	private Rigidbody rb; 
	public float speed; 

	private float power;
	private float steer;
	public float maxSteer = 25.0f;
	public float enginePower = 150.0f;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = new Vector3(0.0f,0.3f,0.7f);
	}

	void Update() {
		power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 250.0f;
		steer = Input.GetAxis("Horizontal") * maxSteer;

		GetCollider(0).steerAngle = steer;
		GetCollider(1).steerAngle = steer;
		GetCollider(0).motorTorque = power;
		GetCollider(1).motorTorque = power;
		GetCollider(2).motorTorque = power;
		GetCollider(3).motorTorque = power;
	}

	private WheelCollider GetCollider(int n) {
		return wheels[n].gameObject.GetComponent<WheelCollider>();
	}

}
