using System;

namespace Test_Suite
{
    public class ObjectGenerator
    {
        /// <summary>
        /// Returns an IMovie array with the same length as the inputString
        /// and Movie objects with titles for each character in inputString
        /// </summary>
        /// <param name="inputString">A string with more than 0 characteres</param>
        /// <returns>An IMovie[] array of length: inputString, with Movie objects</returns>
        public static IMovie[] MovieGenerator(String inputString)
        {
            IMovie[] moviesArray = new Movie[inputString.Length];
            int i = 0;
            foreach (char c in inputString)
            {
                moviesArray[i] = new Movie(c.ToString());
                i++;
            }
            return moviesArray;
        }

        /// <summary>
        /// Returns an integer array with some length and each value being the
        /// expectation
        /// </summary>
        /// <param name="length">The length of an int[] array</param>
        /// <param name="expectation">The value for each item in the array</param>
        /// <returns></returns>
        public static int[] expectedArrayGenerator(int length, int expectation)
        {
            // Expected array with all -1 values
            int[] expectedComparisons = new int[length];
            for (int i = 0; i < length; i++)
            {
                expectedComparisons[i] = expectation;
            }
            return expectedComparisons;
        }
    }

    [TestClass]
    public class MovieTests : ObjectGenerator, IMovieTests
    {       
        string str = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        [TestMethod]
        public void CompareTo_Lower()
        {
            // Create a list of movies with titles as characters in string str
            IMovie[] moviesArray = MovieGenerator(str);

            // Test Movie i against every other movie
            // The total number of comparisons made
            int numComparisons = moviesArray.Length * (moviesArray.Length - 1) / 2;
            int[] allComparisons = new int[numComparisons];

            // Compare every movie with the movie after it
            // i.e. every character vs every character after it
            int k = 0; // increment for index of allComparisons
            for (int i = 0; i < moviesArray.Length; i++)
            {
                for (int j = i + 1; j < moviesArray.Length; j++)
                {
                    int comparisonResult = moviesArray[i].CompareTo(moviesArray[j]);
                    if (comparisonResult != -1)
                    {
                        Console.WriteLine($"{moviesArray[i]} CompareTo {moviesArray[j]} is: {comparisonResult}");
                    }
                    allComparisons[k] = comparisonResult;
                    k++;
                }                
            }


            // Expected array with all -1 values
            int[] expectedComparisons = expectedArrayGenerator(numComparisons,-1);

            // Assert
            bool assertion = Enumerable.SequenceEqual(expectedComparisons, allComparisons);
            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void CompareTo_Same_with_same_object()
        {
            // Create a list of movies with titles as characters in string str
            IMovie[] moviesArray = MovieGenerator(str);

            int numComparisons = moviesArray.Length;
            int[] allComparisons = new int[numComparisons];

            int k = 0; // increment for index of allComparisons
            for (int i = 0; i < moviesArray.Length; i++)
            {
                int comparisonResult = moviesArray[i].CompareTo(moviesArray[i]);
                if (comparisonResult != 0) Console.WriteLine($"{moviesArray[i]} CompareTo {moviesArray[i]} is: {comparisonResult}");
                allComparisons[k] = comparisonResult;
                k++;                
            }

            // Expected array with all -1 values
            int[] expectedComparisons = expectedArrayGenerator(numComparisons, 0);

            // Assert
            bool assertion = Enumerable.SequenceEqual(expectedComparisons, allComparisons);
            Assert.IsTrue(assertion);

        }

        [TestMethod]
        public void CompareTo_Same_with_different_object()
        {
            // Create a list of movies with titles as characters in string str
            IMovie[] moviesArray = MovieGenerator(str);
            IMovie[] moviesArray2 = MovieGenerator(str);

            int numComparisons = moviesArray.Length;
            int[] allComparisons = new int[numComparisons];

            for (int i = 0; i < moviesArray.Length; i++)
            {
                for (int j = 0; j < moviesArray.Length; j++)
                {
                    int comparisonResult = moviesArray[j].CompareTo(moviesArray2[j]);
                    if (comparisonResult != 0) Console.WriteLine($"{moviesArray[i]} CompareTo {moviesArray[j]} is: {comparisonResult}");
                    allComparisons[i] = comparisonResult;
                }
            }

            // Expected array with all -1 values
            int[] expectedComparisons = expectedArrayGenerator(numComparisons, 0);

            // Assert
            bool assertion = Enumerable.SequenceEqual(expectedComparisons, allComparisons);
            Assert.IsTrue(assertion);

        }

        [TestMethod]
        public void CompareTo_Upper()
        {
            // Create a list of movies with titles as characters in string str
            IMovie[] moviesArray = MovieGenerator(str);

            // Test Movie i against every other movie
            // The total number of comparisons made
            int numComparisons = moviesArray.Length * (moviesArray.Length - 1) / 2;
            int[] allComparisons = new int[numComparisons];
            
            int k = 0; // increment for index of allComparisons
            for (int i = 0; i < moviesArray.Length; i++)
            {
                for (int j = i + 1; j < moviesArray.Length; j++)
                {
                    int comparisonResult = moviesArray[j].CompareTo(moviesArray[i]);
                    if (comparisonResult != 1) Console.WriteLine($"{moviesArray[j]} CompareTo {moviesArray[i]} is: {comparisonResult}");
                    allComparisons[k] = comparisonResult;
                    k++;
                }
            }

            // Expected array with all -1 values
            int[] expectedComparisons = expectedArrayGenerator(numComparisons, 1);

            // Assert
            bool assertion = Enumerable.SequenceEqual(expectedComparisons, allComparisons);
            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void ToString_all_properties()
        {
            Movie mov1 = new Movie("Batman", MovieGenre.Action, MovieClassification.M, 100, 2);
            string expected = "Movie(Title: Batman, Genre: Action, Classification: M, Duration: 100)";
            string actual = mov1.ToString();

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_only_Movie_Title()
        {
            Movie mov1 = new Movie("Batman");
            string expected = "Movie(Title: Batman, Genre: 0, Classification: 0, Duration: 0)";
            string actual = mov1.ToString();

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ToString_null_titled_movie()
        {
            IMovie mov = new Movie(null);
            string expected = "Movie(Title: , Genre: 0, Classification: 0, Duration: 0)";
            string actual = mov.ToString();

            Console.WriteLine(expected);
            Console.WriteLine(actual);


            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ToString_empty_string_title()
        {
            IMovie mov = new Movie("");
            string expected = "Movie(Title: , Genre: 0, Classification: 0, Duration: 0)";
            string actual = mov.ToString();

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }
    }
}

