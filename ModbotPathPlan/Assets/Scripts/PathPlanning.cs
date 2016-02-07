using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathPlanning : MonoBehaviour {

	/* Set true for debug purposes */
	public bool debug; 

	/* Object representing the map */
	public GenerateGraph map;

	/* The path of the car represented as a list of nodes*/
	public List<GenerateGraph.Node> path;

	// Use this for initialization
	public void SetUpPathPlanning (Vector3 start) {
		map = new GenerateGraph(start);
		//print (map.ToStringWithNeighbors());
		//print (map.nodes.Count);
		determinePath(map.startNode, map.endNode);
	}

	//draw nodes as spheres for debugging purposes
	/*
	public void OnDrawGizmosSelected() {
		print (map.meshTriangles);
		foreach (Triangle triangle in map.meshTriangles) {
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
	*/


	// <summary>
	// Given two Node objects, determines a path between the startNode and the endNode
	// </summary>
	// <param name="startNode"> the start Node </param>
	// <param name="endNode"> the end Node </param>
	public void determinePath(GenerateGraph.Node startNode, GenerateGraph.Node endNode) {
		if (startNode == null || endNode == null)
			return;
		PriorityQueue<GenerateGraph.Node> pq = new PriorityQueue<GenerateGraph.Node>(map.nodes.Count);
		pq.queue(startNode);
		Dictionary<GenerateGraph.Node, GenerateGraph.Node> came_from = new Dictionary<GenerateGraph.Node, GenerateGraph.Node>();
		Dictionary<GenerateGraph.Node, float> cost_so_far = new Dictionary<GenerateGraph.Node, float> ();
		came_from.Add(startNode, null);
		cost_so_far.Add (startNode, 0);

		Dictionary<GenerateGraph.Node, int> nodeToId = new Dictionary<GenerateGraph.Node, int>();
		for (int i = 0; i < map.nodes.Count; i++) {
			nodeToId.Add (map.nodes[i], i);
		}
		GenerateGraph.Node current;

		while (pq.getSize() > 0) {
			current = pq.dequeue();
			print ("Current Node: " + nodeToId[current]);
			if (current.Equals(endNode)) {
				print ("Reached End. End Node: " + endNode.triangle.ToString () + " Came From: " + came_from[endNode].triangle.ToString());
				break;
			}

			for (int i = 0; i < current.neighbors.Count; i++) {
				float new_cost = cost_so_far[current] + 
					distanceBetweenNodes(current, current.neighbors[i]);
				if (cost_so_far.ContainsKey(current.neighbors[i]) == false ||
				    new_cost < cost_so_far[current.neighbors[i]]) {
					cost_so_far[current.neighbors[i]] = new_cost;
					current.neighbors[i].priority = new_cost;
					pq.queue(current.neighbors[i]);
					print ("Added New Node: " + nodeToId[current.neighbors[i]] + " Priority: " + current.neighbors[i].priority);
					came_from[current.neighbors[i]] = current;
				}
			}
		}

		//Put nodes of the path into the list
		path = new List<GenerateGraph.Node> ();
		GenerateGraph.Node currentNode = endNode;
		path.Add (currentNode);
		while (currentNode.Equals(startNode) == false) {
			currentNode = came_from[currentNode];
			path.Add (currentNode);
		}

		path.Reverse();
		print ("Path Nodes: ");
		for (int i = 0; i < path.Count; i++) {
			print(nodeToId[path[i]] + "\n");
		}
	}

	// <summary>
	// Prints all nodes in the calculated path
	// </summary>
	public void printPath() {
		for (int i = 0; i < path.Count; i++) {
			print(path[i].triangle.ToString() + "\n");
		}
	}

	// <summary>
	// Given two Node objects, determines the distance between them. Specifically, the
	// distance between the centroids of their triangles is returned.
	// </summary>
	// <param name="n1"> the first given Node </param>
	// <param name="n2"> the second given Node </param>
	public float distanceBetweenNodes(GenerateGraph.Node n1, GenerateGraph.Node n2) {
		return Vector3.Distance(n1.triangle.Centroid (), n2.triangle.Centroid());
	}
 


}
