// This Class is converted from python to CSharp. The python files were provided by
//             Unit:  CAB203: Discrete Structures
// Unit Coordinator:  Dr Matthew Mackague (cab203admin@qut.edu.au)
//             Year:  2023, Semester 1

using System;
using System.Diagnostics;

namespace Assignment_3
{
	public abstract class Graphs
	{
		public static HashSet<Task> NS(List<Task> V, Dictionary<(Task, Task), uint> E, HashSet<Task> S)
		{
			// Returns the set of vertices in V that are adjacent to a vertex in S given edges E.
			// If (V, E) is the entire graph, returns the neighborhood of S.
			HashSet<Task> result = new HashSet<Task>();
			foreach (Task v in V)
			{
				foreach (Task u in S)
				{
					if (E.ContainsKey((u, v)))
					{
						result.Add(v);
						break;
					}
				}
			}
			return result;
		}

		public static int Degree(List<Task> V, Dictionary<(Task, Task), uint> E, Task u)
		{
			// Returns the degree of the vertex u in the graph (V, E), i.e. the size of its neighborhood.
			return NS(V, E, new HashSet<Task> { u }).Count;
		}

		public static List<HashSet<Task>> DistanceClasses(List<Task> V, Dictionary<(Task, Task), uint> E, Task? u = null, List<HashSet<Task>>? D = null)
		{
			// Given a graph (V, E) and a starting vertex u, outputs a list of distance classes.
			// Returns a partition of the vertices into sets of fixed distances from u, where u is in the distance class for distance 0.
			// Behavior is undefined if the graph is disconnected.

			if (D == null)
			{
				Task?[] taskArray = new Task?[1];
				taskArray[0] = u;
				HashSet<Task?> set = new HashSet<Task?> { u };
				D = new List<HashSet<Task>> { set };  // D[0] = D_0 = {u}
				return DistanceClasses(V, E, null, D);  // Recurse to get remaining distance classes
			}

			HashSet<Task> Vnew = new HashSet<Task>(V.Except(D.Last()));  // V_{j} = V_{j-1} / D_{j-1}
			HashSet<Task> Dnew = NS(Vnew.ToList(), E, D.Last());  // D_{j} = N_{V_j}(D_{j-1})
			D.Add(Dnew);

			if (Dnew.Count == 0)
			{
				return D;  // Didn't find any more vertices. All done or G is disconnected.
			}

			return DistanceClasses(Vnew.ToList(), E, null, D);
		}

		public static List<Task>? ShortestPath(List<Task> V, Dictionary<(Task, Task), uint> E, Task start, Task end, List<HashSet<Task>>? D = null)
		{
			// Solve the shortest path problem in graph (V,E) from vertex start to vertex end.
			// Path is returned as a list of vertices. If there is no such path then null is returned.

			if (start == end)
			{
				return new List<Task> { start };  // Base case: start is the same as end
			}

			// Get the distance classes in the outer function call
			if (D == null)
			{
				D = DistanceClasses(V, E, end);
			}

			int j = D.FindIndex(Dj => Dj.Contains(start));  // Find start's distance class
			if (j == -1)
			{
				return null;  // No path from start to end
			}

			HashSet<Task> neighbors = new HashSet<Task>();
			neighbors.UnionWith(NS(V, E, new HashSet<Task>() { start }));
			neighbors.UnionWith(D[j - 1]);

			foreach (Task v in neighbors)
			{
				List<Task>? path = ShortestPath(V, E, v, end, D);  // Recurse to find remaining steps
				if (path != null)
				{
					List<Task> fullPath = new List<Task> { start };
					fullPath.AddRange(path);
					return fullPath;  // Path found, return the full path
				}
			}

			return null;  // No path found from start to end
		}

	}
}

