using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {

	public static HeuristicD heuristic;
	public static GenerateGraph graph;

	static AStar() {
		graph = new GenerateGraph();
		heuristic = new HeuristicD(graph);
	}

	public static List<Node> GetPath(Vector3 start) {
		Node startNode = graph.getClosestNode(start);
		PriorityQueue<Node> open = new PriorityQueue<Node>(graph.Size());
		HashSet<Node> closed = new HashSet<Node>();
		Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
		Dictionary<Node, float> cost_so_far = new Dictionary<Node, float>();
		came_from.Add(startNode, null);
		cost_so_far.Add(startNode, 0);
		open.queue(heuristic.Estimate(startNode), startNode);
		
		while (open.getSize() > 0) {
			Node current = open.dequeue();

			if (current.Equals(graph.endNode)) {
				break;
			}

			foreach (Node n in current.neighbors) {
				
				float graph_cost = cost_so_far[current] + Node.distanceBetweenNodes(current, n);
				
				if (cost_so_far.ContainsKey(n) == false ||  graph_cost < cost_so_far[n]) {
					cost_so_far[n] = graph_cost;
					float priority = graph_cost + heuristic.Estimate(n);
					open.queue(priority, n);
					came_from[n] = current;
				}
			}
		}

		//Put nodes of the path into the list
		List<Node> path = new List<Node> ();
		Node currentNode = graph.endNode;
		path.Add (currentNode);
		while (currentNode.Equals(startNode) == false) {
			currentNode = came_from[currentNode];
			path.Add (currentNode);
		}
		path.Reverse();
		return path;
	}

}