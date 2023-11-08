using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Assignment_3.GUI_Interface;

namespace Assignment_3
{
	public class Task
	{

		/// <summary>
		/// A unique identifier for this task. That is not case-sensitive (See CompareTo() and FindTaskID() in TaskCollection.cs)
		/// </summary>
		private string taskID;
		public string TaskID
		{
			get
			{
				return taskID;
			}
			private set
			{
				if (IsValidID(value))
				{
					this.taskID = value;
				}
			}
		}

		/// <summary>
		/// The time required to complete the task in unspecified, but non-negative units
		/// </summary>
		private uint timeRequired;
		public uint TimeRequired
		{
			get
			{
				return timeRequired;
			}
			set
			{
				if (IsValidTimeRequired(value))
				{
					this.timeRequired = value;
				}
			}
		}


		/// <summary>
		/// A list of tasks that must be completed before this task can be started. 
		/// i.e. its dependencies
		/// </summary>
		private List<Task> dependencyList = new List<Task>();
		public List<Task> DependencyList
		{
			get
			{
				return dependencyList;
			}
			set
			{
				if (IsValidDependencyList(value))
				{
					dependencyList = value;
					foreach (Task dependency in dependencyList)
					{
						Message($"{TaskID} is dependent on {dependency.TaskID}", MessageType.Information);
					}
				}
			}
		}

		// Validation methods: check if the given parameter is valid

		/// <summary>
		/// Checks if the taskID is not null.
		/// </summary>
		/// <param name="id">The taskID</param>
		/// <returns>True if valid, and false otherwise</returns>
		public static bool IsValidID(string id)
		{
			if (id != null) return true;
			else
			{
				Message("The id should not be null", MessageType.Error);
				return false;
			}
		}

		/// <summary>
		/// Checks if the time required is not null and not negative.
		/// </summary>
		/// <param name="timeRequired">The time required</param>
		/// <returns>True if valid, and false otherwise</returns>
		public static bool IsValidTimeRequired(uint? timeRequired)
		{
			if (timeRequired == null) { Console.WriteLine("Time cannot be null\n"); return false; }
			else if (timeRequired >= 0) { return true; }
			else
			{
				Message("Time required cannot be negative", MessageType.Error);
				return false;
			}
		}

		/// <summary>
		/// Checks if the dependency list is a list of Tasks
		/// </summary>
		/// <param name="list">The dependency list</param>
		/// <returns>True if valid, and false otherwise</returns>
		public bool IsValidDependencyList(List<Task> list)
		{
			if ((list.GetType().Name).Equals((new List<Task>()).GetType().Name) == false)
			{
				Message("Invalid type of list", MessageType.Error); return false;
			}			
			if (list.Contains(this) == true)
			{
				Message("A Task canot depend on itself", MessageType.Error); return false;				
			}

			return true;
			
		}


		// Constructors: create a new Task object with the given parameters

		/// <summary>
		/// Creates a new Task object with all properties
		/// </summary>
		public Task(string taskID, uint timeRequired, List<Task> dependencyList)
		{
			this.taskID = taskID;
			this.timeRequired = timeRequired;
			DependencyList = dependencyList;
		}
		/// <summary>
		/// Creates a new Task object with only the taskID and timeRequired
		/// </summary>
		public Task(string taskID, uint timeRequired)
		{
			this.taskID = taskID;
			this.timeRequired = timeRequired;
		}


		/// <summary>
		/// Changes the current TimeRequired to a newTime
		/// </summary>
		/// <param name="newTime">The new time required</param>
		public void ChangeTimeRequired(uint newTime)
		{
			if (IsValidTimeRequired(newTime))
			{
				uint previousTime = TimeRequired;
				TimeRequired = newTime;
				Message($"Time required for task '{TaskID}' is changed from {previousTime} to {TimeRequired}", MessageType.Information);
			}
		}

		/// <summary>
		/// Adds a dependency to the current dependencyList
		/// </summary>
		/// <param name="dependency">The dependency to be added</param>
		/// <returns>True if the dependency was added, and false otherwise</returns>
		public bool AddDependency(Task dependency)
		{
			if (HasDependency(dependency))
			{
				Message($"The task: {TaskID} is already dependent on {dependency.TaskID}", MessageType.Warning);
				return false;
			}
			else if (dependency.TaskID == this.TaskID)
			//else
			{
				Message($"The Task: {TaskID} cannot depend on itself and so it will be ignored.", MessageType.Warning);
				return false;
			}

			dependencyList.Add(dependency);
			Message($"Task {TaskID} is now dependent on {dependency.TaskID}", MessageType.Information);
			return true;
		}

		/// <summary>
		/// Removes a dependency from the current dependencyList if it exists
		/// </summary>
		/// <param name="dependency">The dependency to be removed</param>
		/// <returns>True if the dependency was removed, and false otherwise</returns>
		public bool RemoveDependency(Task dependency)
		{
			if (HasDependency(dependency))
			{
				dependencyList.Remove(dependency);
				Message($"Task {this.TaskID} is no longer dependent on {dependency.TaskID}", MessageType.Information);
				return true;
			}
			else
			{
				Message($"Task {this.TaskID} was never dependent on {dependency.TaskID}", MessageType.Warning);
				return false;
			}
		}

		/// <summary>
		/// Checks if the current dependencyList contains the given task
		/// </summary>
		/// <param name="task">The task to be checked</param>
		/// <returns>True if the dependencyList contains the task, and false otherwise</returns>
		public bool HasDependency(Task task)
		{
			if (dependencyList.Contains(task))
			{
				return true;
			}
			return false;
		}


		/// <summary>
		/// Creates a string representation of the Task object formatted by separating
		/// each property by a comma, and an empty string if the task is null
		/// </summary>
		/// <returns>A string representation of the Task object</returns>
		public override string ToString()
		{
			if (this == null) { return ""; }

			string formattedString = "";
			if (this.DependencyList.Any())
			{
				string dependencyString = string.Join(", ", DependencyList.Select(task => task.TaskID));
				formattedString = $"{this.TaskID}, {this.timeRequired.ToString()}, {dependencyString}";
			}
			else
			{
				formattedString = $"{this.TaskID}, {this.timeRequired.ToString()} ";
			}
			return formattedString;
		}
	}
}

