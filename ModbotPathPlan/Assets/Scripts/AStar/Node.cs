using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AStar{
	public class Node {

		public enum LaneOptions {
			Middle, Outer, Misc
		};

		public Vector3 position;
		public List<Node> neighbors;
		public LaneOptions laneType;

		public Node(Vector3 position) {
			this.position = position;
			this.neighbors = new List<Node>();
			laneType = LaneOptions.Misc;
		}

		public Node(Vector3 position, Node n) {
			this.position = position;
			this.neighbors = new List<Node>();
			neighbors.Add(n);
		}

		public static float distanceBetweenNodes(Node n1, Node n2) {
			return Vector3.Distance(n1.position, n2.position);
		}
	}
}