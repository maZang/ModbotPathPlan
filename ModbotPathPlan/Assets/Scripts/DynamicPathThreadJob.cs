using UnityEngine;
using System.Collections.Generic;

// DynamicPathThreadJob is a class that implements the thread to dynamically calculate/modify
// the path of the car as it moves through the map
public class DynamicPathThreadJob : ThreadJob
{
	public Node startNode;
	public Node endNode;
	public Node destinationNode;
	private List<Vector3> pathWayPoints; 
	private const double pathLength = 20;

	// <summary>
	// Constructor that initializes the start node and end node of the path planning
	// </summary>
	// <param name="startNode">Node object where the path planning starts from</param>
	// <param name="endNode">Node object representing where the path should move towards</param> 
	public DynamicPathThreadJob(Node startNode, Node endNode) {
		this.startNode = startNode;
		this.endNode = endNode;
		destinationNode = null;
	}

	// <summary>
	// Performs an A* traversal from the start node to the end node; however, the end node
	// only provides a direction for the path finding, as once the current node in the 
	// traversal is pathLength away from the start node, the traversal is complete
	// </summary>
	protected override void ThreadFunction() {
		PriorityQueue<Node> open = new PriorityQueue<Node> (AStar.graph.Size ());
		HashSet<Node> closed = new HashSet<Node> ();
		Dictionary<Node, Node> came_from = new Dictionary<Node, Node> ();
		Dictionary<Node, float> cost_so_far = new Dictionary<Node, float> ();
		came_from.Add (startNode, null);
		cost_so_far.Add (startNode, 0);
		open.queue (PathPlanningDataStructures.heuristic.Estimate (startNode), startNode);
	
		while (open.getSize() > 0) {
			Node current = open.dequeue ();
			Debug.Log (current.ToString());
			if (current.Equals (PathPlanningDataStructures.graph.endNode) || 
				Node.distanceBetweenNodes (startNode, current) >= pathLength) {
				destinationNode = current;
				break;
			}
		
			foreach (Node n in current.neighbors) {
			
				float graph_cost = cost_so_far [current] + Node.distanceBetweenNodes (current, n);
			
				if (cost_so_far.ContainsKey (n) == false || graph_cost < cost_so_far [n]) {
					cost_so_far [n] = graph_cost;
					float priority = graph_cost + PathPlanningDataStructures.heuristic.Estimate (n);
					open.queue (priority, n);
					came_from [n] = current;
				}
			}
		}
	
		//Put nodes of the path into the list
		List<Node> path = new List<Node> ();
		Node currentNode = destinationNode;
		path.Add (currentNode);
		while (currentNode.Equals(startNode) == false) {
			currentNode = came_from [currentNode];
			path.Add (currentNode);
		}
		path.Reverse ();
	}

	// <summary>
	// Returns the list of path way points that were determined from ThreadFunction
	// </summary>
	public List<Vector3> getPathWayPoints() {
		return pathWayPoints;
	}
}