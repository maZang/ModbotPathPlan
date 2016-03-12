using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AStar{
	public class Heuristic {

		private Graph graph;
		private Dictionary<Node, float> heuristicCost;

		public Heuristic(Graph graph) {
			this.graph = graph;
			heuristicCost = new Dictionary<Node, float>();

			PriorityQueue<Node> pq = new PriorityQueue<Node>(graph.Size ());
			Dictionary<Node, float> cost_so_far = new Dictionary<Node, float> ();
			pq.queue(0.0f, graph.endNode);
			while (pq.getSize() > 0) {
				Node current = pq.dequeue();
				heuristicCost[current] = cost_so_far[current];
				for (int i = 0; i < current.neighbors.Count; i++) {
					float new_cost = cost_so_far[current] + Node.distanceBetweenNodes(current, current.neighbors[i]);
					if (!cost_so_far.ContainsKey(current.neighbors[i]) || new_cost < cost_so_far[current.neighbors[i]]) {
						cost_so_far[current.neighbors[i]] = new_cost;
						pq.queue(new_cost, current.neighbors[i]);
					}	
				}
			}
		}
		
		public float Estimate(Node n) {
			return heuristicCost[n];
		}
	}
}