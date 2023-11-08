//using System.Collections.Generic;
//using System.IO;
//using System.Text.RegularExpressions;
//using static Assignment_3.GUI_Interface;

//namespace Assignment_3
//{
//	public class ConsoleApplication : FileManagerInterface
//	{
//		// User Interface strings
//		const string REQUEST = "Please select one of the following:";

//		// Default file path strings
//		private const string RELATIVE_PATH = "../../../"; // goes back 3 directories from net7.0
//		//private static string RELATIVE_PATH = Directory.GetCurrentDirectory() + "/"; // Directory for executable. 
//		private static string INPUT_TXT = $"{RELATIVE_PATH}Input.txt";
//		private static string SEQUENCE_TXT = $"{RELATIVE_PATH}Sequence.txt";
//		private static string EARLIEST_TIMES_TXT = $"{RELATIVE_PATH}EarliestTimes.txt";

//		// Properties
//		private string fileName = ""; // A file name where the list of tasks exists
//		public TaskCollection Collection = new TaskCollection(); // A task collection that starts off empty

//		// Fields
//		public string FileName
//		{
//			get
//			{
//				return fileName;
//			}
//			private set
//			{
//				fileName = value;
//			}
//		}

//		public ConsoleApplication() { }

//		/// <summary>
//		/// Information regarding how the application works that may be
//		/// important to the user
//		/// </summary>
//		private static void AppNotice()
//		{
//			Console.WriteLine("CAB301: Algorithms and Complexity Assignment 3");
//			Console.WriteLine("    By: Shridhar Thorat, n10817239\n");

//			Console.WriteLine("Keep in mind files exist in the project directory" + "\n" +
//							  "and not where the executable is.\n" +
//							  "This can be changed by commenting line 14, and uncommenting line 15 of ConsoleApplication.cs\n");

//			Console.WriteLine("Also keep in mind that TaskIDs are not-case sensitive" + "\n" +
//							  "    For example the tasks 't1' and 'T1' will be treated\n" +
//							  "    as the same. And the earliest one in the input file\n" +
//							  "    will be treated as the original when reading\n");
//		}

//		/// <summary>
//		/// Runs the functionality of the app
//		/// </summary>
//		private void RunApp()
//		{
//			AppNotice();
//			bool runApp = true, runStartUpMenu = true, runMain = false;
//			while (runApp)
//			{
//				if (runStartUpMenu) { StartUpMenu(ref runApp, ref runStartUpMenu, ref runMain); }
//				if (runMain) { MainMenu(ref runApp, ref runStartUpMenu, ref runMain); }
//			}
//		}

//		/// <summary>
//		/// Runs the menu that requests reading files
//		/// </summary>
//		/// <param name="runApp"></param>
//		/// <param name="runStartUpMenu"></param>
//		/// <param name="runMain"></param>
//		private void StartUpMenu(ref bool runApp, ref bool runStartUpMenu, ref bool runMain)
//		{
			
//			const int CREATE_NEW_TASK_COLLECTION = 0; const string NEW = "Create new task collection";
//			const int LOAD_TASK_COLLECTION = 1; const string LOAD_1 = "Load Tasks from some file in the project directory";
//			const int LOAD_FIXED_TASK_COLLECTION = 2; const string LOAD_2 = "Load Tasks from the Input.txt file";
//			const int EXIT = 3; const string EX = "Exit Application";

//			int choice = GetOption(REQUEST, NEW, LOAD_1, LOAD_2, EX);

//			Console.WriteLine();
//			switch (choice)
//			{
//				case CREATE_NEW_TASK_COLLECTION:
//					MainMenu(ref runApp, ref runStartUpMenu, ref runMain);
//					break;
//				case LOAD_TASK_COLLECTION:
//					Load_Collection_From_Any_txt(ref runStartUpMenu, ref runMain);
//					break;
//				case LOAD_FIXED_TASK_COLLECTION:
//					Load_Collection_From_Input_txt(ref runStartUpMenu, ref runMain);
//					break;
//				case EXIT:
//					runApp = false;
//					break;
//					//default:
//					//	InvalidInput(1, 3);
//					//	break;
//			}
//		}

//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="runApp"></param>
//		/// <param name="runStartUpMenu"></param>
//		/// <param name="runMain"></param>
//		private void MainMenu(ref bool runApp, ref bool runStartUpMenu, ref bool runMain)
//		{
//			runStartUpMenu = false; runMain = true;

//			const int VIEW_COLLECTION = 0; const string V_C = "View Current Task Collection";
//			const int ADD_TASK = 1; const string A_T = "Add a Task to the collection";
//			const int REMOVE_TASK = 2; const string R_T = "Remove Task from the collection";
//			const int CHANGE_TASK_TIME_REQ = 3; const string C_R = "Change Time Required for a Task";
//			const int UPDATE_FILE_PATH = 4; const string U_F = $"Update Input File with Latest Tasks";
//			const int TOPOLOGICAL_SORT = 5; const string T_S = "Sort the Collection Topologically; without violating dependencies";
//			const int EARLIEST_COMMENCE = 6; const string E_S = "Determine Earliest Commencement Time";
//			const int DELETE_AND_RETURN = 7; const string D_R = "Delete the collection and return";

//			int choice = GetOption(REQUEST, V_C, A_T, R_T, C_R, U_F, T_S, E_S, D_R);
//			Console.WriteLine();
//			switch (choice)
//			{				
//				case VIEW_COLLECTION:
//					TaskCollection.ViewCollection(Collection);
//					break;

//				case ADD_TASK:
//					//Console.WriteLine();
//					CreateNewTaskToAdd();
//					break;

//				case REMOVE_TASK:
//					//Console.WriteLine();
//					RemoveTaskFromCollection();
//					break;

//				case CHANGE_TASK_TIME_REQ:
//					//Console.WriteLine();
//					ChangeRequiredTime();
//					break;

//				case UPDATE_FILE_PATH:
//					//Console.WriteLine();
//					UpdateFileWithTasks();
//					break;

//				case TOPOLOGICAL_SORT:
//					//Console.WriteLine();
//					SortTopologically();
//					break;

//				case EARLIEST_COMMENCE:
//					//Console.WriteLine();
//					EarliestTiming();
//					break;

//				case DELETE_AND_RETURN:
//					//Console.WriteLine();
//					ClearCollectionAndReturn(ref runStartUpMenu, ref runMain);
//					break;

//					//default:
//					//	InvalidInput(1, 8);
//					//	break;
//			}
//			Console.WriteLine();
//		}

//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="runStartUpMenu"></param>
//		/// <param name="runMain"></param>
//		private void Load_Collection_From_Input_txt(ref bool runStartUpMenu, ref bool runMain)
//		{
//			Console.WriteLine();

//			// Check validity (dependent on person that wrote the program)
//			FileName = INPUT_TXT;
//			Message($"FileName.txt: {FileName}", MessageType.Information);
//			if (!IsValidFilePath(FileName))
//			{
//				Message($"No Tasks exist in the file path", MessageType.Warning);
//				return;
//			}

//			Collection = ReadInputFile(FileName);

//			if (Collection.TaskList.Any())
//			{
//				runStartUpMenu = false; runMain = true;
//			}
//			// Check for any cycles and stop loading if there is so the
//			// user can make changes
//			List<Task>? topologicalSort = Collection.TopologicalSort();
//			if (topologicalSort == null)
//			{
//				runStartUpMenu = true; runMain = false;
//				Message("File Unloaded do to circular dependency", MessageType.Error);
//			}		
//		}

//		private void Load_Collection_From_Any_txt(ref bool runStartUpMenu, ref bool runMain)
//		{
//			Console.WriteLine();
//			bool validFileName = false;
//			string? filePath = "";

//			// Get a valid filepath 
//			while (validFileName == false)
//			{
//				Console.Write("FileName.txt: ");
//				filePath = Console.ReadLine();
//				if (string.IsNullOrEmpty(filePath))
//				{
//					runStartUpMenu = true;
//					Message("File name is empty or null. Enter a valid file path", MessageType.Error);
//				}
//				else { validFileName = true; }
//			}

//			// filePath is something so check if it exists
//			string relativePath = RELATIVE_PATH + filePath;
//			if (IsValidFilePath(relativePath))
//			{
//				FileName = relativePath;
//				Collection = ReadInputFile(FileName);				
//				// Check for any cycles and stop loading if there is so the
//				// user can make changes
//				List<Task>? topologicalSort = Collection.TopologicalSort();
//				if (topologicalSort == null)
//				{
//					runStartUpMenu = true; runMain = false;
//					Message("File Unloaded do to circular dependency", MessageType.Error);
//				}
//				else
//				{
//					runStartUpMenu = false; runMain = true;
//				}
//			}
//			else
//			{
//				Message($"File does not exist in the {RELATIVE_PATH} location of the project.\n" +
//				$"Ensure files are in the same location as the .csproj/sln file.", MessageType.Error);
//			}

//		}

//		// ADD_TASK, "Add a Task to the collection"
//		private Task CreateNewTaskToAdd()
//		{
//			// View Task Ids of existing tasks
//			TaskCollection.ViewCollection(Collection);
//			Message("Ensure Task ID is unique", MessageType.Information);

//			// Get task ID
//			string taskId = Get_Task_That_Is("New");
//			uint timeRequired = GetRequiredTime();

//			List<Task> dependencies = ChooseDependenciesToAdd();
//			Task task = new Task(taskId, timeRequired, dependencies);
//			// Output information to the user about the dependencies
//			Collection.AddTask(task);
//			UpdateFileWithTasks();
//			return task;

//		}
//		private string Get_Task_That_Is(string typeOfTask)
//		{
//			bool isValidId = false; string output = "";

//			while (isValidId == false)
//			{
//				string? taskId = GetInput("Task ID");

//				if (taskId == null) { Message("Null input is not allowed. Enter a valid Task ID", MessageType.Error); }
//				else if (taskId == "") { Message("No task typed, enter a valid task ID", MessageType.Warning); }
//				else
//				{
//					switch (typeOfTask)
//					{
//						case "New":
//							isValidId = NewTaskIsValid(ref output, taskId);
//							break;
//						case "Existing":
//							string[] dependencyIDs = taskId.Split(",");
//							isValidId = CheckExistingTask(ref output, dependencyIDs);
//							break;
//						default:
//							throw new Exception("Invalid type of task chosen. Fix the input");
//					}
//				}

//			}
//			return output;
//		}
//		private bool NewTaskIsValid(ref string toSet, string value)
//		{
//			if (Collection.FindTaskID(value) == null)
//			{
//				toSet = value; return true;
//			}
//			else
//			{
//				Message($"Collection already has a task with ID '{value}'. Choose another one", MessageType.Warning);
//				return false;
//			}
//		}

//		private bool CheckExistingTask(ref string toSet, string[] dependencyList)
//		{
//			bool isValid = false;
//			foreach (string dependencyID in dependencyList)
//			{
//				string dependencyID_noWhitespace = dependencyID.Replace(" ", "");
//				if (Collection.FindTaskID(dependencyID_noWhitespace) == null)
//				{
//					Message($"The task {dependencyID_noWhitespace} does not exist in the collection and will be ignored.", MessageType.Warning);
//					if (dependencyList.Last() == dependencyID) // if the invalid task is the only one, it is invalid.
//					{
//						return isValid;
//					}
//				}
//				else { toSet += dependencyID_noWhitespace + ","; isValid = true; }
//			}
//			return isValid;
//		}


//		private List<Task> ChooseDependenciesToAdd()
//		{
//			// View Task Ids of existing tasks
			
//			const string PROMPT = "Choose one of the following options for Dependendencies";
//			const int NONE = 0; const string None = "This task has no dependencies";
//			const int NEW = 1; const string New = "Add one dependency that doesn't exist in the task list";
//			const int EXISTING = 2; const string Existing = "Choose one, or multiple tasks that exist";

//			int choice = GetOption(PROMPT, None, New, Existing);
//			List<Task> dependencyList = new List<Task>();
//			Console.WriteLine();
//			switch (choice)
//			{
//				case NONE:
//					Message("\nTask has no dependencies\n", MessageType.Information);
//					break;

//				case NEW:
//					Task newDependency = CreateNewTaskToAdd();
//					dependencyList.Add(newDependency);
//					break;

//				case EXISTING:
//					TaskCollection.ViewCollection(Collection);
//					Message("You can choose one, or multiple Task ID's separated by a comma", MessageType.Information);
//					string dependencyChoices = Get_Task_That_Is("Existing");
//					List<string> validDependencyIDs = dependencyChoices.Split(new char[] { ',' }).ToList();
//					dependencyList = ParseDependencyString(validDependencyIDs);
//					break;

//					//default:
//					//	InvalidInput(1, 3);
//					//	break;
//			}
//			return dependencyList;
//		}

//		private List<Task> ParseDependencyString(List<string> dependencyIDs)
//		{
//			List<Task> dependencies = new List<Task>();

//			foreach (string dependencyId in dependencyIDs)
//			{
//				string replaceWSpace = dependencyId.Replace(" ", "");
//				Task? dependency = Collection.FindTaskID(replaceWSpace);
//				if (dependency != null) { dependencies.Add(dependency); }
//			}

//			return dependencies;

//		}

//		// REMOVE_TASK,  "Remove Task from the collection"
//		private void RemoveTaskFromCollection()
//		{
//			if (Collection.Number != 0)
//			{
//				TaskCollection.ViewCollection(Collection);
//				Message("Type the Task ID to remove", MessageType.Information);
//				string input = Get_Task_That_Is("Existing");

//				bool removed = false;
//				while (removed == false)
//				{
//					if (string.IsNullOrEmpty(input)) { return; }

//					// Didn't quick return, so actually remove it
//					input = input.Replace(",", "");
//					Task? chosenTask = Collection.FindTaskID(input);
//					if (chosenTask != null)
//					{
//						removed = Collection.RemoveTask(chosenTask);
//						if (removed)
//						{
//							UpdateFileWithTasks();
//						}
//					}
//				}
//			}
//			else { Message("Collection has no tasks to remove.\nTry again after adding some tasks", MessageType.Warning); }
//		}


//		// CHANGE_TASK_TIME_REQ, "Change Time Required for a Task"
//		private void ChangeRequiredTime()
//		{
//			TaskCollection.ViewCollection(Collection);

//			string chosenID = Get_Task_That_Is("Existing");
//			Task? chosenTask = Collection.FindTaskID(chosenID); // will always be a valid Task but signature-wise still requires Task?
//			Message($"Current Time required: {chosenTask?.TimeRequired}", MessageType.Information);

//			uint newTimeRequired = GetRequiredTime();
//			if (chosenTask != null)
//			{
//				chosenTask.ChangeTimeRequired(newTimeRequired);
//				UpdateFileWithTasks();
//			}

//		}
//		private static uint GetRequiredTime()
//		{
//			bool valid = false; uint timeRequired = 0;
//			while (valid == false)
//			{
//				timeRequired = GetUInteger("Required Time");
//				if (Task.IsValidTimeRequired(timeRequired)) { valid = true; }
//				else { Message($"{timeRequired} is Invalid", MessageType.Warning); }
//			}
//			return timeRequired;
//		}

//		private void UpdateFileWithTasks()
//		{
//			string prompt = "Do you want to update the input file with the latest changes? [y/n]";
//			string? input = GetInput(prompt);
//			if (input == "y")
//			{
//				if (IsValidFilePath(FileName))
//				{
//					UpdateInputFile(Collection, FileName);
//					Message("File Updated to below:", MessageType.Information);
//					Message(Collection.GetCollectionDetails(), MessageType.Information);
//				}
//			}
//			else { Message("Returning to Main Menu\n", MessageType.Information); }

//		}
//		// TOPOLOGICAL_SORT, "Sort the Collection Topologically; without violating dependencies"
//		private void SortTopologically()
//		{
//			Message("A Topological Sorting of the Tasks is:", MessageType.Information);
//			List<Task>? sorting = Collection.TopologicalSort();

//			// Display sorting 
//			if (sorting != null)
//			{
//				Message(string.Join(", ", sorting.Select(task => task.TaskID)), MessageType.Information); // Display the information

//				// Save File if possible
//				string? input = GetInput("Would you like to save this topological sorting in Sequence.txt? [y/n]");
//				if (input == "y")
//				{
//					if (IsValidFilePath(SEQUENCE_TXT))
//					{
//						SaveSequenceToFile(sorting, ',', SEQUENCE_TXT);
//						return;
//					}
//					else
//					{
//						input = GetInput("Sequence.txt does not exist, would you like to create it and then save? [y/n]");
//						if (input == "y")
//						{
//							FileStream sequenceFile = File.Create(SEQUENCE_TXT);
//							sequenceFile.Close();
//							SaveSequenceToFile(sorting, ',', SEQUENCE_TXT);
//							return;
//						}
//					}
//				}
//				// Runs if the return statment's didn't
//				Message("Writing terminated.", MessageType.Information);
//			}
//		}

//		// EARLIEST_COMMENCE, "Determine Earliest Commencement Time"
//		private void EarliestTiming()
//		{
//			Console.WriteLine("\nThe Earliest Commencement Time for each task is below:");
//			Dictionary<Task, (uint, uint)>? earliestTimes = Collection.EarliestTimes();

//			// Display sorting 
//			if (earliestTimes == null) { return; }

//			// Don't need an if statement, since this will run if the above doesn't
//			string earliestTimeString = TaskCollection.EarliestTimeString(earliestTimes);
//			Console.WriteLine(earliestTimeString);

//			// Save File if possible
//			string? input = GetInput("Would you like to save these earliest commencement time values in EarliestTimes.txt? [y/n]");
//			if (input == "y")
//			{
//				if (IsValidFilePath(EARLIEST_TIMES_TXT))
//				{
//					SaveEarliestTimesToFile(earliestTimes, EARLIEST_TIMES_TXT);
//					return;
//				}
//				else
//				{
//					input = GetInput("EarliestTimes.txt does not exist, would you like to create it and then save? [y/n]");
//					if (input == "y")
//					{
//						FileStream earliestTimesFile = File.Create(EARLIEST_TIMES_TXT);
//						earliestTimesFile.Close();
//						SaveEarliestTimesToFile(earliestTimes, EARLIEST_TIMES_TXT);
//						return;
//					}
//				}
//			}
//			// Runs if the return statment's didn't
//			Message("Writing terminated.", MessageType.Information);
//		}

//		// DELETE_AND_RETURN, "Delete the collection and return"
//		private void ClearCollectionAndReturn(ref bool runStartUpMenu, ref bool runMain)
//		{
//			Console.Write("\nAre you sure you want to delete all the tasks [y/n]: ");
//			string? input = Console.ReadLine();
//			if (input == "y") { Collection.TaskList.Clear(); runStartUpMenu = true; runMain = false; }
//		}
//		private static void Main()
//		{
//			ConsoleApplication app = new ConsoleApplication();
//			app.RunApp();
//		}
//	}
//}