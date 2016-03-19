using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeuristicD {

	private GenerateGraph graph;
	private Dictionary<Node, float> heuristicCost;

	public HeuristicD(GenerateGraph graph) {
		this.graph = graph;
		heuristicCost = new Dictionary<Node, float>();

		//PriorityQueue<Node> pq = new PriorityQueue<Node>(graph.Size ());
		SortedDictionary<float, Node> pq = new SortedDictionary<float,Node>();
		Dictionary<Node, float> cost_so_far = new Dictionary<Node, float> ();
		pq.Add (0.0f, graph.endNode);
		cost_so_far.Add (graph.endNode, 0.0f);
		//pq.queue(0.0f, graph.endNode);
		Debug.Log (pq);
		//while (pq.getSize() > 0) {
		while (pq.Count > 0) {
			//Node current = pq.dequeue();
			var keys = pq.GetEnumerator();
			var keyNode = keys.Current;
			Node current = keyNode.Value;

			heuristicCost[current] = cost_so_far[current];
			for (int i = 0; i < current.neighbors.Count; i++) {
				float new_cost = cost_so_far[current] + Node.distanceBetweenNodes(current, current.neighbors[i]);
				if (!cost_so_far.ContainsKey(current.neighbors[i]) || new_cost < cost_so_far[current.neighbors[i]]) {
					cost_so_far[current.neighbors[i]] = new_cost;
					//pq.queue(new_cost, current.neighbors[i]);
					pq.Add (new_cost, current.neighbors[i]);
				}	
			}
		}
	}
	
	public float Estimate(Node n) {
		return heuristicCost[n];
	}
}