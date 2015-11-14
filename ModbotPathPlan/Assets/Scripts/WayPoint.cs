using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {

	static WayPoint start = null;
	public WayPoint next;
	public bool isStart = false;
	
	void Awake () {
		if (isStart)
			start = this;
	}
	
	public void OnDrawGizmosSelected() {
		Gizmos.color = new Color (0.0f, 0.0f, 1.0f, 0.3f);
		Gizmos.DrawCube (transform.position, new Vector3(5.0f, 5.0f, 5.0f));

		if (next) {
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine (transform.position, next.transform.position);
		}
	}

}
