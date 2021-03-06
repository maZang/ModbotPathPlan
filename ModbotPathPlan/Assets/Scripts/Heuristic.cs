using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeuristicD {
	
	private Dictionary<Node, float> heuristicCost;
	//public List<GameObject> itemList;
	private ItemController ic = GameObject.Find("ItemControllers").GetComponent<ItemController>();

	public HeuristicD(GenerateGraph graph) {
		heuristicCost = new Dictionary<Node, float>();
		//itemList = new List<GameObject> ();
		//GameObject item = GameObject.Find ("Item1");
		//itemList.Add (item);

		PriorityQueue<Node> pq = new PriorityQueue<Node>(graph.Size ());
		Dictionary<Node, float> cost_so_far = new Dictionary<Node, float> ();
		pq.queue(0.0f, graph.endNode);
		cost_so_far.Add (graph.endNode, 0.0f);
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
		float reduction = ic.getReduction(n.position);

		/*foreach (GameObject item in itemList) {
			if (!item.activeSelf) {
				continue;
			} else {
				if (item.GetComponent<SphereCollider>().bounds.Contains(n.position)) {
					reduction = 2.0f;
				}
			}
		}*/
		/* if (item.GetComponent<SphereCollider> ().bounds.Contains (n.position)) {
			reduction = 2.0f;
		} */ 
		if (reduction < 0) {
			Debug.Log ("!!!!!!! " + n.position);
			return reduction;
		} else {
			return heuristicCost [n];
		}
		//Debug.Log (heuristicCost[n] / reduction);
		//return heuristicCost[n] / reduction;
	}
}