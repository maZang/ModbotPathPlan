using UnityEngine;
using System.Collections;

public class GenerateMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NavMeshTriangulation navmesh = NavMesh.CalculateTriangulation();

		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		mesh.vertices = navmesh.vertices;
		mesh.triangles = navmesh.indices;

		Vector2[] uvs = new Vector2[mesh.vertices.Length];

		for (int i = 0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
		}
		mesh.uv = uvs;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
