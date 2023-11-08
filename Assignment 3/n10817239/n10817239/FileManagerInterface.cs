using System;
using System.Text.RegularExpressions;
using static Assignment_3.GUI_Interface;
namespace Assignment_3
{
	/// <summary>
	/// Static support methods for reading and writing to files
	/// </summary>
	public class FileManagerInterface
	{
		/// <summary>
		/// Checks if a filePath exists
		/// </summary>
		/// <param name="filePath">Is a relative, or absolute filepath</param>
		/// <returns>True if it exists and false otherwise, as well as if the file is not a .txt file</returns>
		public static bool IsValidFilePath(string filePath)
		{
			Regex txtMatch = new("\\w*txt\\b");
			Match isText = txtMatch.Match(filePath);
			if (isText.Success == false)
			{
				Message("File must be a text (.txt) file", MessageType.Warning);
				return false;
			}
			else
			{
				if (File.Exists(filePath))
				{
					Message("File exists.", MessageType.Information);
					return true;
				}
				else
				{
					Message("File does not exist.", MessageType.Error);
					return false;
				}
			}
		}

		/// <summary>
		/// Writes information about a TaskCollection into a FilePath
		/// </summary>
		/// <param name="Collection">A TaskCollection</param>
		/// <param name="FilePath">An absolute or relative filepath</param>
		public static void UpdateInputFile(TaskCollection Collection, string FilePath)
		{
			using (StreamWriter writer = File.CreateText(FilePath))
			{
				string information = Collection.GetCollectionDetails();
				writer.WriteLine(information);
				writer.Close();
			}
		}

		/// <summary>
		/// Reads information from a 
		/// </summary>
		/// <param name="Collection"></param>
		/// <param name="FilePath"></param>
		public static TaskCollection ReadInputFile(string FilePath)
		{
			Message("-------- Writing Tasks into the collection --------", MessageType.Information);
			using StreamReader reader = new(FilePath);
			Dictionary<Task, string[]?> TaskWithStringDependencies = new Dictionary<Task, string[]?>();
			int lineNumber = 1; // Used for telling the user where an error may be

			// Read the file once and create Tasks with a list of string dependencies
			while (!reader.EndOfStream)
			{
				string? line = reader.ReadLine();
				CreateTasksWithStringDependencies(line, ',', ref lineNumber, ref TaskWithStringDependencies, FilePath);
			}
			Console.WriteLine();

			// Actually create the collection, finding the ID's of the dependencies
			// In the dictionary since they already exist and adding via references
			TaskCollection collection = CreateTaskCollection(TaskWithStringDependencies);
			if (collection.TaskList.Any())
			{
				Message("-------- File finished reading --------\n", MessageType.Information);
			}
			else
			{
				Message("-------- File TERMINATED reading --------\n", MessageType.Information);
			}
			return collection;
		}

		/// <summary>
		/// Creates a dictionary with key Task object and value is a list of string ID's for the Tasks's dependencies
		/// Outputs an error message if lines are not separated by commas
		/// </summary>
		/// <param name="line">A string that corresponds to a line from a text file</param>
		/// <param name="lineSeparator">A character that separates all words in the line</param>
		/// <param name="lineNumber">The current line in the file</param>
		/// <param name="TaskWithStringDependencies">A dictionary with tasks and their dependencies as just their IDs</param>
		/// <param name="FilePath">The file that information is read from</param>
		private static void CreateTasksWithStringDependencies
			(string? line,
			Char LineSeparator,
			ref int lineNumber,
			ref Dictionary<Task, string[]?> TaskWithStringDependencies,
			string FilePath
			)
		{
			if (line != null && line != "") // skips empty lines
			{
				int parts = line.Split(",").Length;
				if (parts >= 2)
				{
					ReadFileLine(line, lineNumber, LineSeparator, ref TaskWithStringDependencies);
				}
				else { Console.WriteLine($"ERROR: Line {lineNumber}: '{line} is incorrectly formatted"); }
			}
			lineNumber++;
		}

		/// <summary>
		/// Reads a particular line from a file and adds a key, value pair to a dictionary
		/// </summary>
		/// <param name="line">A particular line from a file</param>
		/// <param name="lineNumber">The current line in the file</param>
		/// <param name="seperator">A character that separates all words in a line</param>
		/// <param name="TaskWithStringDependencies">A dictionary with key; Task, and value; string with dependency TaskIDs</param>
		private static void ReadFileLine(string line, int lineNumber, Char lineSeparator, ref Dictionary<Task, string[]?> TaskWithStringDependencies)
		{
			string[] values = line.Split(lineSeparator);
			string taskId = values[0].Trim();
			uint requiredTime;
			if(uint.TryParse(values[1].Trim(),out requiredTime) == false)
			{
				Message($"The line number {lineNumber} '{line}'\nIs not a valid Task, and will be ignored.", MessageType.Error);
				return;
			}

			string[]? dependencies = values.Skip(2).Select(d => d.Trim()).ToArray();
			if (dependencies.Any() == false) { dependencies = null; }

			TaskCollection currentCollection = new TaskCollection(new List<Task>(TaskWithStringDependencies.Keys));
			Task newTask = new Task(taskId, requiredTime);

			if (currentCollection.IsValidTask(newTask))
			{
				TaskWithStringDependencies[newTask] = dependencies;
			}
		}

		/// <summary>
		/// Creates a TaskCollection corresponding to the information from some file
		/// </summary>
		/// <param name="TaskWithStringDependencies">Initial information read from the file as task names and the TaskID's of their dependencies</param>
		/// <returns>A TaskCollection</returns>
		private static TaskCollection CreateTaskCollection(Dictionary<Task, string[]?> TaskWithStringDependencies)
		{
			TaskCollection collection = new TaskCollection();
			TaskCollection allTasks = new TaskCollection(new List<Task>(TaskWithStringDependencies.Keys));
			int lineNumber = 1; bool dependencyAdded = true;
			foreach (var pair in TaskWithStringDependencies)
			{
				Task task = pair.Key;
				if (pair.Value != null)
				{
					foreach (string depString in pair.Value)
					{
						if (depString != "")
						{
							Task? dependency = allTasks.FindTaskID(depString);

							if (dependency != null) { dependencyAdded = task.AddDependency(dependency); }
							else
							{
								Console.WriteLine($"ERROR:(File line {lineNumber}): The dependency '{depString}' doesn't exist. Fix this before reading again.");
								return new TaskCollection();
							}
						}
					}
				}
				collection.AddTask(task);
				Console.WriteLine(); // space between each task being added
			}
			return collection;
		}


		/// <summary>
		/// Saves a list of tasks to a file as a string separated by commas
		/// </summary>
		/// <param name="sequence">A list of Tasks</param>
		/// <param name="lineSeparator">The character that separates each task</param>
		/// <param name="filePath">The relative or absolute file path that the list is written to</param>
		public static void SaveSequenceToFile(List<Task> sequence, Char lineSeparator, string filePath)
		{
			using (StreamWriter writer = new StreamWriter(filePath))
			{
				string stringSequence = string.Join(lineSeparator, sequence.Select(task => task.TaskID));
				writer.WriteLine(stringSequence);
				writer.Close();
			}
			Message($"Topological sort saved to {filePath}", MessageType.Information);
		}


		/// <summary>
		/// Saves the earliest starting times for each task to a file
		/// </summary>
		/// <param name="Task_start_finish">A dictionary with key; Task, and value; start and finish time</param>
		/// <param name="filePath">he relative or absolute file path that the dictionary is written to</param>
		public static void SaveEarliestTimesToFile(Dictionary<Task, (uint, uint)> Task_start_finish, string filePath)
		{
			using (StreamWriter writer = new StreamWriter(filePath))
			{
				string stringSequence = TaskCollection.EarliestTimeString(Task_start_finish);
				writer.WriteLine(stringSequence);
				writer.Close();
			}
			Message($"Earliest Times saved to {filePath}", MessageType.Information);
		}
	}
}

