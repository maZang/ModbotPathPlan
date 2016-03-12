using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AStar {
	public static class AStar {

		public static Heuristic heuristic;
		public static Graph graph;

		static AStar() {
			graph = new Graph();
			heuristic = new Heuristic(graph);
		}

		public static List<Node> GetPath(Vector3 start) {
			Node startNode = graph.getClosestNode(start);
			PriorityQueue<Node> open = new PriorityQueue<Node>(graph.Size());
			HashSet<Node> closed = new HashSet<Node>();
			Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
			Dictionary<Node, float> current_cost_start = new Dictionary<Node, float>();
			came_from.Add(startNode, null);
			current_cost_start.Add(startNode, 0);
			open.queue(heuristic.Estimate(startNode), startNode);

			while (open.getSize() > 0) {
				Node current = open.dequeue();
				if (current.Equals(graph.endNode)) {
					break;
				}

				closed.Add(current);

				foreach (Node n in current.neighbors) {

					if (closed.Contains(n)) {
						continue;
					}

					float g_score = current_cost_start[current] + Node.distanceBetweenNodes(current, n);
					float h_score = heuristic.Estimate(n);
					float f_score = g_score + h_score;

					if (current_cost_start.ContainsKey(n)) {
						if (current_cost_start[n] >= g_score) {
							open.queue(f_score, n);
						}
					} else {
						open.queue(f_score, n);
					}
					came_from[n] = current;
					current_cost_start[n] = g_score;
				}
			}

			Node backtracker = graph.endNode;
			List<Node> path = new List<Node> ();
			while (came_from.ContainsKey(backtracker)) {
				path.Insert (0, backtracker);
			}
			return path;

		}

	}
}