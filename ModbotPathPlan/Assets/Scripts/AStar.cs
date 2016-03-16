using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {

	public static Heuristic heuristic;
	public static GenerateGraph graph;

	static AStar() {
		graph = new GenerateGraph();
		heuristic = new Heuristic(graph);
	}

	public static List<Node> GetPath(Vector3 start) {
		Node startNode = graph.getClosestNode(start);
		SortedList<Integer, Node> open = new SortedList<Integer,Node>();
		HashSet<Node> closed = new HashSet<Node>();
		Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
		Dictionary<Node, float> cost_so_far = new Dictionary<Node, float>();
		came_from.Add(startNode, null);
		cost_so_far.Add(startNode, 0);
		open.Add(heuristic.Estimate(startNode), startNode);

		while (open.Count > 0) {
			KeyValuePair<Integer, Node> minvalues = Min (open);
			Node current = minvalues.Value;
			int current_key = minvalues.Key;

			if (current.Equals(graph.endNode)) {
				break;
			}

			closed.Add(current);

			foreach (Node n in current.neighbors) {

				if (closed.Contains(n)) {
					continue;
				}

				float graph_cost = cost_so_far[current] + Node.distanceBetweenNodes(current, n);

				if (cost_so_far.ContainsKey(n) == false ||  graph_cost < cost_so_far[n]) {
					cost_so_far[n] = graph_cost;
					float priority = graph_cost + heuristic.Estimate(n);
					open.Add(priority, n);
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

	public static KeyValuePair<K, V> Min<K, V>(this SortedList<K, V> dict)
	{
		return new KeyValuePair<K, V>(dict.Keys[0], dict.Values[0]);
	}

}