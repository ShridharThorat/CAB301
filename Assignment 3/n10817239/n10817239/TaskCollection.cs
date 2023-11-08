using System.Collections.Generic;
using System.Linq;
using static Assignment_3.GUI_Interface;

namespace Assignment_3
{
	/// <summary>
	/// A class that represents a collection of tasks that have relationships with each other
	/// </summary>
	public class TaskCollection
	{

		private int count;          // the number of tasks in the collection 
		public int Number { get { return count; } }

		/// <summary>
		/// A list of each task in the collection that is initially empty
		/// </summary>
		private List<Task> taskList;
		public List<Task> TaskList
		{
			get
			{
				return taskList;
			}
			set
			{
				taskList = value;
			}
		}

		/// <summary>
		/// A dictionary of outgoing edges with weights that is initially empty
		/// </summary>
		private Dictionary<(Task, Task), uint> OutGoingEdges;

		/// <summary>
		/// Creates a TaskCollection with an empty list of tasks
		public TaskCollection()
		{
			this.taskList = new List<Task>();
			OutGoingEdges = new Dictionary<(Task, Task), uint>();
		}
		/// <summary>
		/// Creates a TaskCollection with a pre-created list of tasks
		/// And defined edges
		/// </summary>
		public TaskCollection(List<Task> preCreatedList)
		{
			this.taskList = preCreatedList;
			count = preCreatedList.Count;
			OutGoingEdges = GetOutGoingEdgesWithWeights();
		}

		// Validity testers for properties

		/// <summary>
		/// Checks if a taskId is non-null and unique 
		/// </summary>
		/// <param name="id">A string ID being checked</param>
		/// <returns>True if the string is valid, false otherwise</returns>
		public bool IsValidTask(Task task)
		{
			if (Task.IsValidID(task.TaskID))
			{
				if (IsUniqueTaskID(task.TaskID))
				{
					return true;
				}
				else
				{
					Message($"The task {task} already exists in the collection", MessageType.Error);
					return false;
				}
			}
			else
			{
				Message($"The task '{task?.ToString()}' is invalid", MessageType.Error);
				return false;
			}
		}

		/// <summary>
		/// Checks if a taskId exists in the collection
		/// </summary>
		/// <param name="id">A string ID being checked</param>
		/// <returns>True if unique and false immediately if not</returns>
		public bool IsUniqueTaskID(string taskID)
		{
			foreach (Task task in TaskList)
			{
				int comparison = CompareTo(taskID, task.TaskID);
				if (comparison == 0)
				{
					return false;
				}
			}
			return true;
		}


		/// <summary>
		/// Compares two strings and returns an integer indicating their relative position in the sort order
		/// </summary>
		/// <param name="firstTaskId">The first taskId being compared</param>
		/// <param name="secondTaskId">The second string being compared</param>
		/// <returns>
		/// <br>1 if the firstTaskId is before secondTaskId lexicographically.</br>
		/// <br>-1 if the firstTaskId is after secondTaskId lexicographically. </br>
		/// <br>0 if the firstTaskId is the same as secondTaskId lexicographically</br>
		/// </returns>
		public static int CompareTo(string? firstTaskId, string secondTaskId)
		{
			if (firstTaskId == null) { return 0; }

			int comparison = String.CompareOrdinal(firstTaskId.ToUpper(), secondTaskId.ToUpper());
			if (comparison < 0) { return -1; }
			else if (comparison > 0) { return 1; }
			else { return 0; }
		}



		// Task list management methods: Adding, removing, tasks 

		/// <summary>
		/// Adds a task to the collection if it is valid
		/// </summary>
		/// <param name="task">The task being added</param>
		/// <returns>True if the task is added, false otherwise</returns>
		public bool AddTask(Task task)
		{
			if (FindTaskID(task.TaskID) == null)
			{
				TaskList.Add(task);
				this.count++;
				Message($"The Task '{task.TaskID}' is added to the collection", MessageType.Information);
				OutGoingEdges = GetOutGoingEdgesWithWeights();
				return true;
			}
			else
			{
				Message($"The Task '{task.TaskID}' already exists", MessageType.Warning);
				return false;
			}


		}

		/// <summary>
		/// <br>Removes a task from the collection if it exists</br>
		/// <br>Also removes any tasks that depend on it</br>
		/// </summary>
		/// <param name="task">The task being removed</param>
		/// <returns>True if the task is removed, false otherwise</returns>
		public bool RemoveTask(Task task)
		{
			Task? thisTask = this.FindTaskID(task.TaskID);
			if (thisTask != null)
			{
				// First make every task that dependends on thisTask
				// not dependent on it anymore
				foreach (Task dependant in TaskList)
				{
					if (dependant.HasDependency(thisTask))
					{
						dependant.RemoveDependency(thisTask);
					}
				}
				TaskList.Remove(thisTask);
				this.count--;
				OutGoingEdges = GetOutGoingEdgesWithWeights();
				return true;
			}
			else
			{
				Message($"The task '{task.TaskID}' does not exist", MessageType.Warning);
				return false;
			}
		}

		/// <summary>
		/// Returns a reference to a Task with the specified id if it exists
		/// and null otherwise
		/// </summary>
		/// <param name="taskId">The TaskId being searched for</param>
		/// <returns>A reference to a Task in a TaskList</returns>
		public Task? FindTaskID(string taskId)
		{
			if (Task.IsValidID(taskId))
			{
				foreach (Task task in TaskList)
				{
					if (CompareTo(taskId, task.TaskID) == 0)
					{
						return task;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// <br>Shows a string with the details of each task in the collection</br>
		/// <br>or a message if there are no tasks</br>
		/// </summary>
		public static void ViewCollection(TaskCollection taskCollection)
		{
			string prompt = "\nExisting Tasks have the following relationships\n";
			string collection = taskCollection.GetCollectionDetails();
			if (collection == "")
			{
				Message("Task Collection is currently empty", MessageType.Warning);
			}
			else
			{
				Message(prompt + collection, MessageType.Information);
			}
		}

		/// <summary>
		/// Writes the list of tasks formatted correctly for saving to a text file
		/// </summary>
		/// <returns>A string that can be used to write to a text file</returns>
		public string GetCollectionDetails()
		{
			if (TaskList.Count == 0)
			{
				return "";
			}

			string spanningTree = "";
			for (int i = 0; i < TaskList.Count; i++)
			{
				spanningTree += TaskList[i].ToString() + "\n";
			}
			return spanningTree;
		}


		// Helper methods utilised by the graphing methods

		/// <summary>
		/// Using a list of Vertices, it create a list edges from a dependency
		/// to a task with a TimeRequired the same as the dependency
		/// </summary>
		/// <returns>A Dictionary (dependency, task): dependency.TimeRequired</returns>
		private Dictionary<(Task, Task), uint> GetOutGoingEdgesWithWeights()
		{
			Dictionary<(Task, Task), uint> outGoingEdgesWithWeights = new Dictionary<(Task, Task), uint>();

			foreach (Task task in TaskList)
			{
				foreach (Task dependency in task.DependencyList)
				{
					outGoingEdgesWithWeights.Add((dependency, task), dependency.TimeRequired);
				}
			}
			return outGoingEdgesWithWeights;
		}

		/// <summary>
		/// Creates a dictionary with key: Task, and value: number of dependencies
		/// Functionaly the same as the other overload
		/// </summary>
		/// <returns></returns>
		private Dictionary<Task, int> GetDegrees()
		{
			Dictionary<Task, int> inDegreeList = new Dictionary<Task, int>();
			foreach (Task task in TaskList)
			{
				int numberOfDependencies = task.DependencyList.Count;
				inDegreeList[task] = numberOfDependencies;
			}

			return inDegreeList;
		}

		/// <summary>
		/// Creates a dictionary with key: Task and value: number of edges coming
		/// into the vertex. i.e the number of dependencies
		///
		/// Different to above as this uses a dictionary of edges rather than
		/// just tasks with their dependencies
		/// 
		/// </summary>
		/// <param name="vertices"></param>
		/// <param name="weightedEdges"></param>
		/// <returns></returns>
		private static Dictionary<Task, int> GetDegrees(List<Task> vertices, Dictionary<(Task, Task), uint> weightedEdges)
		{
			Dictionary<Task, int> inDegreeList = new Dictionary<Task, int>();
			foreach (Task task in vertices) { inDegreeList[task] = 0; }
			foreach (var edge in weightedEdges)
			{
				Task dependency = edge.Key.Item1, task = edge.Key.Item2;
				inDegreeList[task]++;
			}
			return inDegreeList;
		}

		/// <summary>
		/// Finds each independent task in a dictionary of Tasks with their
		/// in-degree value
		/// </summary>
		/// <param name="inDegreeList">A dictionary with a Task as the key and its in-degree as the value</param>
		/// <returns>A list of tasks with an in-degree of 0</returns>
		private static List<Task> VerticesWith_In_Degree0(Dictionary<Task, int> inDegreeList)
		{
			List<Task> queue = new List<Task>();
			foreach (var pair in inDegreeList)
			{
				if (pair.Value == 0)
				{
					queue.Add(pair.Key);
				}
			}

			return queue;
		}


		// Graphing methods used to solve for the assignment specifications

		/// <summary>
		/// Implements the Topological Sort Algorithm in Week 8. Slide 32
		/// Finds a sequence of the tasks that does not violate any job dependencies
		/// </summary>
		/// <returns>A list of tasks List<Task></returns>
		public List<Task>? TopologicalSort()
		{
			// Soft copies (may be unecessary)
			List<Task> vertices = new List<Task>(TaskList);
			Dictionary<(Task, Task), uint> weightedEdges = OutGoingEdges;

			Dictionary<Task, int> inDegree = GetDegrees();
			List<Task> independentTasks = VerticesWith_In_Degree0(inDegree);

			List<Task>? sortedTasks = TopologicalSort(weightedEdges, independentTasks, inDegree);
			return sortedTasks;
		}


		/// <summary>
		/// Implements the Topological Sort Algorithm in Week 8. Slide 32
		/// Finds a topological sequence of Tasks given a set of parameters
		/// </summary>
		/// <param name="weightedEdges">A dictionary Dictionary<(Task, Task), uint> of outgoing edges with weights</param>
		/// <param name="independentTasks">A list of Tasks with no dependencies</param>
		/// <param name="inDegree">A dictionary Dictionary<Task, int> with a Task and the In-degree of that Vertex</param>
		/// <returns>A list of Tasks sorted topologically List<Task></returns>
		/// <exception cref="ArgumentException">Throws an exception if the Graph is Cyclic</exception>
		private List<Task>? TopologicalSort
		(
		Dictionary<(Task, Task), uint> weightedEdges,
		List<Task> independentTasks,
		Dictionary<Task, int> inDegree
		)
		{
			List<Task> topologicalSort = new List<Task>(); // result list

			// While there are still independent tasks to add to the topological sort
			while (independentTasks.Any())
			{
				// Not necessary but simply prioritises tasks by time (as well as satisfying dependencies)
				// eg, if T1 takes 100 and T7 takes 50 and both are independent, then T7 will be listed first
				Task currentTask = GetTaskWithLowestTimeRequired(independentTasks);

				// Add the task to the topological sort and remove it from the independent tasks
				topologicalSort.Add(currentTask); independentTasks.Remove(currentTask);

				// For each edge in the graph
				foreach (var pair in weightedEdges)
				{
					Task dependency = pair.Key.Item1, nextTask = pair.Key.Item2;
					// 'Remove' the currentTask from the graph by decreasing the
					// number of degrees for each Task connected to the dependency
					if (dependency == currentTask)
					{
						inDegree[nextTask] -= 1;
						if (inDegree[nextTask] == 0)
						{
							independentTasks.Add(nextTask); // nextTask is now independent
						}
					}
				}
			}

			// By the end of the sorting, each task should be in the topological sort list
			// if not, then there must be a task(s) that is still dependent on something else
			if (topologicalSort.Count != this.Number)
			{
				Message("Graph is Cyclic: Manage Task dependencies to remove any cycles", MessageType.Error);
				return null;
			}
			return topologicalSort;
		}

		private static Task GetTaskWithLowestTimeRequired(List<Task> independentTasks)
		{
			Task currentTask = independentTasks.First();
			foreach (Task task in independentTasks)
			{
				if (currentTask.TimeRequired > task.TimeRequired)
				{
					currentTask = task;
				}
			}
			return currentTask;
		}


		/// <summary>
		/// Finds the earliest possible commencement time for each of the tasks in the project
		/// </summary>
		/// <returns>A Dictionary with a Task and an uint tuple (starting Time, Finishing Time)</returns>
		public Dictionary<Task, (uint, uint)>? EarliestTimes()
		{
			List<Task>? TopologicalSorting = TopologicalSort();
			if (TopologicalSorting != null)
			{
				// Initialise each task to have a start and finish time of 0
				Dictionary<Task, (uint, uint)> Start_and_FinishTimes = new Dictionary<Task, (uint, uint)>();
				foreach (Task task in TaskList) { Start_and_FinishTimes[task] = (0, 0); }

				// Calculating the earliest time for each Task
				foreach (var v in Start_and_FinishTimes)
				{
					Task task = v.Key;
					if (Start_and_FinishTimes[task] == (0, 0)) // i.e. still the initialisation
					{
						EarliestTimes(task, ref Start_and_FinishTimes);
					}
				}
				return Start_and_FinishTimes;
			}
			else { return null; }
		}

		/// <summary>
		/// Recursively finds the earliest times for each task in a dictionary
		/// by finding both, the earliest start and latest finish time
		/// </summary>
		/// <param name="task">The Task the starting and finishing time is being calculated for</param>
		/// <param name="Max_Flow_In_Out">A dictionary with key: Task, and uint tuple value: (start, finish)</param>
		private void EarliestTimes(Task task, ref Dictionary<Task, (uint, uint)> Max_Flow_In_Out)
		{
			// If the task has dependencies, then calculate
			// the earliest starting time and finishing time for them first
			if (task.DependencyList.Any())
			{
				// Just need the finish times for each dependency, the IDs and start times are irrelevant
				List<uint> allDependencyFinishingTimes = new List<uint>();

				foreach (Task dependency in task.DependencyList)
				{
					if (Max_Flow_In_Out[dependency] == (0, 0)) // i.e still the initialisation
					{
						EarliestTimes(dependency, ref Max_Flow_In_Out);
					}

					allDependencyFinishingTimes.Add(Max_Flow_In_Out[dependency].Item2); // key (Item1, Item2)
				}

				// The starting time is the Maximum flow into the node (from its dependencies)
				uint thisStartingTime = allDependencyFinishingTimes.Max();
				// Finishing time is the max flow in, as well as the time for the task itself
				uint thisFinishingTime = thisStartingTime + task.TimeRequired;

				Max_Flow_In_Out[task] = (thisStartingTime, thisFinishingTime);
			}
			// If the task has no dependencies (is independent)
			// then starting is 0 (it can start immediately)
			// and finish is just its own required time
			else
			{
				Max_Flow_In_Out[task] = (0, task.TimeRequired);
			}
		}

		/// <summary>
		/// Simply converts a dictionary with start and finish times for each node into a string
		/// </summary>
		/// <param name="earliestTimes">A dictionary with a Task and its start and finish times</param>
		/// <returns>A string</returns>
		public static string EarliestTimeString(Dictionary<Task, (uint, uint)> earliestTimes)
		{
			string earliestTimeString = "";
			foreach (var task_time in earliestTimes)
			{
				earliestTimeString += String.Join(", ", task_time.Key.TaskID, task_time.Value.Item1) + "\n";
			}
			return earliestTimeString;
		}

	}
}

