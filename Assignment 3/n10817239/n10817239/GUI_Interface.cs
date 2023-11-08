// This Class is provided by
//             Unit:  CAB201: Programming Principles
// Unit Coordinator:  Dr Alan Woodley (cab201admin@qut.edu.au)
//             Year:  2022, Semester 1

// Some modifications are done by Shridhar Thorat (n10817239) and un-used methods
// have been removed for simplicity sake
using System;
using System.Collections.Generic;

namespace Assignment_3
{
	/// <summary>
	/// Static support methods for a simple interactive menu syste,
	/// </summary>
	public interface GUI_Interface
	{
		/// CHANGED BY SHRIDHAR:
		/// Line 101, added ")"
		/// original: Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)} {options[i]}");
		/// changed:  Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)}) {options[i]}");
		/// <summary>
		/// Displays a menu, with the options numbered from 1 to options.Length,
		/// the gets a validated integer in the range 1..options.Length. 
		/// Subtracts 1, then returns the result. If the supplied list of options 
		/// is empty, returns an error value (-1).
		/// </summary>
		/// <param name="title">A heading to display before the menu is listed.</param>
		/// <param name="options">The list of objects to be displayed.</param>
		/// <returns>Return value is either -1 (if no options are provided) or a 
		/// value in 0 .. (options.Length-1).</returns>
		public static int GetOption(string title, params object[] options)
		{
			if (options.Length == 0)
			{
				return -1;
			}

			int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Length)));

			Console.WriteLine(title);

			for (int i = 0; i < options.Length; i++)
			{
				Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)}) {options[i]}");
			}

			int option = GetInteger($"Please enter a choice between 1 and {options.Length}", 1, options.Length);

			return option - 1;
		}


		/// <summary>
		/// Prompts user to enter a line of text.
		/// </summary>
		/// <param name="prompt">The label displayed to request input.</param>
		/// <returns>The next line of input from the standard input stream.</returns>
		public static string? GetInput(string prompt)
		{
			Console.Write("{0}: ", prompt);
			return Console.ReadLine();
		}


		/// <summary>
		/// Prompts user to get an integer value, blocking until valid input is obtained.
		/// </summary>
		/// <param name="prompt">The label displayed to request input.</param>
		/// <returns>The next line of input from the standard input stream, parsed into an int.</returns>
		public static uint GetUInteger(string prompt)
		{
			while (true)
			{
				var response = GUI_Interface.GetInput(prompt);

				if (uint.TryParse(response, out uint intVal))
				{
					return intVal;
				}
				else
				{
					Message("Number must be greater than or equal to 0", MessageType.Error);
				}
			}
		}


		/// <summary>
		/// Gets a validated integer between the designated lower and upper bounds.
		/// </summary>
		/// <param name="prompt">Text used to ask the user for input.</param>
		/// <param name="min">The lower bound.</param>
		/// <param name="max">The upper bound.</param>
		/// <returns>A value x such that lowerBound <= x <= upperBound.</returns>
		public static int GetInteger(string prompt, int min, int max)
		{
			if (min > max)
			{
				int t = min;
				min = max;
				max = t;
			}

			while (true)
			{
				int result = (int) GetUInteger(prompt);

				if (min <= result && result <= max)
				{
					return result;
				}
				else
				{
					Message($"Supplied value is out of range {min} to {max}", MessageType.Error);
				}
			}
		}

		/// <remarks>Designed by Shridhar</remarks>
		/// <summary>
		/// Defines the different types of messages
		/// </summary>
		public enum MessageType
		{
			Information, // Just gives feedback to the user
			Warning, // Minor issue
			Error, // Is a big issue
		}

		/// <remarks>Designed by Shridhar</remarks>
		/// <summary>
		/// Displays a message including some specific text
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="type"></param>
		/// <exception cref="Exception"></exception>
		public static void Message(string msg, MessageType type)
		{
			switch (type)
			{
				case MessageType.Error:
					Console.WriteLine($"\nError: {msg}\n");
					break;
				case MessageType.Information:
					Console.WriteLine(msg);
					break;
				case MessageType.Warning:
					Console.WriteLine($"\nWARNING: {msg}\n");
					break;
				default:
					throw new Exception();
			}
		}
	}
}
