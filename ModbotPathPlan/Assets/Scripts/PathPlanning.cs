using UnityEngine;
using System.Collections;

public class PathPlanning : MonoBehaviour {

	public GenerateGraph map;
	// Use this for initialization
	void Start () {
		map = new GenerateGraph ();
		print (map.ToString ());
	}

	//draw nodes as spheres for debugging purposes
	void OnDrawGizmosSelected() {
		foreach (Triangle triangle in map.nodes) {
			Gizmos.color = new Color(2f, 2f, 2f);
			Gizmos.DrawSphere (triangle.Centroid(), 0.3f);
			Gizmos.color = Color.red;
			Gizmos.DrawLine	(triangle.vertex1, triangle.vertex2);
			Gizmos.DrawLine (triangle.vertex1, triangle.vertex3);
			Gizmos.DrawLine (triangle.vertex2, triangle.vertex3);
			Gizmos.DrawSphere (triangle.vertex1, 0.2f);
			Gizmos.DrawSphere (triangle.vertex2, 0.2f);
			Gizmos.DrawSphere (triangle.vertex3, 0.2f);
		}
	}

}
