using UnityEngine;
using System.Collections;

public class GenerateGraph {

	public Triangle[] nodes; 

	public GenerateGraph() {

		//get nav mesh characteristics from pre-made nav mesh. Will write script later that generates a nav-mesh for any map.
		NavMeshTriangulation navmesh = NavMesh.CalculateTriangulation ();

		//initialize triangles array
		nodes = new Triangle[navmesh.indices.Length/3];

		//Made sure nav mesh indices is a multiple of 3
		for (int i = 0; i < navmesh.indices.Length / 3; i++) {
			nodes[i] = new Triangle(navmesh.vertices[navmesh.indices[i*3]], navmesh.vertices[navmesh.indices[i*3 + 1]], 
			               navmesh.vertices[navmesh.indices[i*3 + 2]], NavMesh.GetAreaCost(navmesh.areas[i]));
		}
	}

	public override string ToString() {
		string return_string = "";
		foreach (Triangle triangle in nodes) {
			 return_string += "\n" + (triangle.ToString ());
		}
		return return_string;
	}
}
