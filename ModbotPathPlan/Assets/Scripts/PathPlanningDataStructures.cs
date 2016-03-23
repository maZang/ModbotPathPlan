using UnityEngine;

// PathPlanningDataStructures is a class that contains the essential data structures necessary
// for implementing the path planning approach

public class PathPlanningDataStructures {
	public static HeuristicD heuristic; // Heuristic object containing the dijkstra movement costs
	public static GenerateGraph graph; // Graph object representing the map and containing all possible
									   // way points
	public static object globalLock; // Global lock for synchronizing access to nodes (only one car
									 // claim a node at any given time)

	// <summary>
	// Constructor that initializes the data structures necessary for the path planning approach
	// </summary>
	static PathPlanningDataStructures () {
		graph = new GenerateGraph();
		heuristic = new HeuristicD(graph);
		globalLock = new object();
		Debug.Log ("Heuristic Finished");
	}

}
