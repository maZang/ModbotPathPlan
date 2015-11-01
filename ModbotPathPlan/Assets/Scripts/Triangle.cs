using UnityEngine;
using System.Collections;

/* Class used to represent the triangulation of the NavMeshes */
public class Triangle  {

	public Vector3 vertex1;
	public Vector3 vertex2;
	public Vector3 vertex3; 
	public float cost; 

	/* Basic constructor for the triangle */
	public Triangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, float cost) {
		this.vertex1 = vertex1;
		this.vertex2 = vertex2;
		this.vertex3 = vertex3; 
		this.cost = cost; 
	}

	/* Helper function that tells us whether or not two points are on the same side of a triangle*/ 
	private bool SameSide(Vector3 p1, Vector3 p2, Vector3 A, Vector3 B) {
		Vector3 cp1 = Vector3.Cross (B - A, p1 - A);
		Vector3 cp2 = Vector3.Cross (B - A, p2 - A); 
		return (Vector3.Dot (cp1, cp2) >= 0);
	}

	/* Function that tells whether or not a point is inside a given triangle */
	public bool PointInTriangle(Vector3 p) {
		return SameSide (p, vertex1, vertex2, vertex3) && SameSide (p, vertex2, vertex1, vertex3) &&
			SameSide (p, vertex3, vertex1, vertex2);
	}

	/* Returns center of triangle; neccessary as A* will be run on the centroids of the triangles */
	public Vector3 Centroid() {
		return new Vector3 ((vertex1.x + vertex2.x + vertex3.x) / 3, (vertex1.y + vertex2.y + vertex3.y) / 3,
		                    (vertex1.z + vertex2.z + vertex3.z) / 3);
	}

	//print function for debugging purposes
	public override string ToString() {
		return "Triangle: \n\tVertex1: " + vertex1.ToString () + "\n\tVertex2: " + vertex2.ToString () + "\n\tVertex3: " + vertex3.ToString () + "\n\tCost: " + cost;
	}
}