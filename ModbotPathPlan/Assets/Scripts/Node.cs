using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	public Vector3 position;
	public List<Node> neighbors;

	public Node(Vector3 position) {
		this.position = position;
		this.neighbors = new List<Node>();
	}

	public Node(Vector3 position, Node n) {
		this.position = position;
		this.neighbors = new List<Node>();
		neighbors.Add(n);
	}

	public static float distanceBetweenNodes(Node n1, Node n2) {
		return Vector3.Distance(n1.position, n2.position);
	}

	public override string ToString() {
		return position.ToString ();
	}
}