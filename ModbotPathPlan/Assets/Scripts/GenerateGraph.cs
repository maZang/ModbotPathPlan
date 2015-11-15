using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateGraph {

	public Triangle[] meshTriangles;
	public List<Node> nodes; //a Node contains a triangle and adjacent Nodes
	public Node startNode;
	public Node endNode;

	public GenerateGraph() {

		//get nav mesh characteristics from pre-made nav mesh. Will write script later that generates 
		//a nav-mesh for any map.
		NavMeshTriangulation navmesh = NavMesh.CalculateTriangulation ();

		//initialize triangles array
		meshTriangles = new Triangle[navmesh.indices.Length/3];

		//will contain mapping from Vector3 pair represented with the string (ex: (1,2,3) and (4,5,6)
		//will be respresented as "1,2,3 - 4,5,6" with the smaller Vector3 coming first) to the list
		//of nodes that have that pair as a side. Used for calculating neighbors of a node.
		Dictionary<string, List<Node>> pairToNodes =
			new Dictionary<string, List<Node>>();

		nodes = new List<Node>();

		//Made sure nav mesh indices is a multiple of 3
		for (int i = 0; i < navmesh.indices.Length / 3; i++) {
			Vector3 v1 = navmesh.vertices[navmesh.indices[i*3]];
			Vector3 v2 = navmesh.vertices[navmesh.indices[i*3 + 1]];
			Vector3 v3 = navmesh.vertices[navmesh.indices[i*3 + 2]];
			meshTriangles[i] = new Triangle(v1, v2, v3, NavMesh.GetAreaCost(navmesh.areas[i]));
			Node currentNode = new Node(meshTriangles[i]);
			nodes.Add(currentNode);
			AddToDictionary(ref pairToNodes, GetPairString(v1, v2), currentNode);
			AddToDictionary(ref pairToNodes, GetPairString(v2, v3), currentNode);
			AddToDictionary(ref pairToNodes, GetPairString(v1, v3), currentNode);
		}

		//set neighbors of each node
		for (int j = 0; j < nodes.Count; j++) {
			Triangle currentTriangle = nodes[j].triangle;
			string[] keys = new string[3];
			keys[0] = GetPairString(currentTriangle.vertex1, currentTriangle.vertex2);
			keys[1] = GetPairString(currentTriangle.vertex2, currentTriangle.vertex3);
			keys[2] = GetPairString(currentTriangle.vertex1, currentTriangle.vertex3);
			for (int k = 0; k < keys.Length; k++) {
				if (pairToNodes.ContainsKey(keys[k]) && pairToNodes[keys[k]].Count > 1) {
					for (int l = 0; l < pairToNodes[keys[k]].Count; l++) {
						if (((pairToNodes[keys[k]][l]).Equals(nodes[j])) == false) {
							nodes[j].neighbors.Add(pairToNodes[keys[k]][l]);
							break;
						}
					}
				}
			}
		}

		//set start node of the car
		startNode = nodes[0];
		//set end node of the car
		endNode = nodes[10];
	}

	// <summary>
	// Given a dictionary that maps a string representing a Vector3 pair
	// to the list of nodes that have that pair as a side, key, and value,
	// add the value to the list of nodes that the key currently maps to
	// </summary>
	// <param name="dict"> 
	// the dictionary that maps a string representing a Vector3 pair
	// to the list of nodes that have that pair as a side
	// </param>
	// <param name="key"> a string representing a Vector3 pair </param>
	// <param name="value"> a Node to add to the list of nodes that the key currently maps to </param>
	public void AddToDictionary(ref Dictionary<string, List<Node>> dict, string key, Node value) {
		if (dict.ContainsKey (key)) {
			List<Node> currentNodes = dict[key];
			currentNodes.Add(value);
			dict[key] = currentNodes;
		} else {
			List<Node> newNodes = new List<Node>();
			newNodes.Add(value);
			dict.Add(key, newNodes);
		}
	}

	// <summary>
	// Given two Vector3 objects, creates a string representation of that pair with the
	// smaller Vector3 object coming first (ex: (1,2,3) and (4,5,6) will be respresented 
	// as "1,2,3 - 4,5,6" with the smaller Vector3 coming first)
	// </summary>
	// <param name="v1"> the first given Vector3 </param>
	// <param name="v2"> the second given Vector3 </param>
	public string GetPairString(Vector3 v1, Vector3 v2) {
		float[] v1Components = new float[3];
		v1Components[0] = v1.x;
		v1Components[1] = v1.y;
		v1Components[2] = v1.z;
		float[] v2Components = new float[3];
		v2Components[0] = v2.x;
		v2Components[1] = v2.y;
		v2Components[2] = v2.z;
		Vector3 first = v1;
		Vector3 second = v2;
		for (int i = 0; i < 3; i++) {
			if (v1Components[i] > v2Components[i]) {
				Vector3 temp = second;
				second = first;
				first = temp;
				break;
			} else if(v1Components[i] < v2Components[i]) {
				break;
			}
		}
		return first.x + "," + first.y + "," + first.z + " - " + second.x + "," +
			second.y + "," + second.z;
	}

	// <summary>
	// Returns a string representation of all the triangles of the nodes
	// </summary>
	public override string ToString() {
		string return_string = "";
		foreach (Node node in nodes) {
			 return_string += "\n" + (node.triangle.ToString());
		}
		return return_string;
	}

	public class Node : System.IComparable {
		public Triangle triangle;
		public List<Node> neighbors;
		public float priority;

		public Node(Triangle t) {
			triangle = t;
			neighbors = new List<Node>();
			priority = 0;
		}

		public int CompareTo(object obj) {
			Node node2 = obj as Node;
			return this.priority.CompareTo(node2.priority);
		}

	}

}