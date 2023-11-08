using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting
{
    [TestClass]
    public class MovieTest
    {
        // this < other = -1
        // this = other = 0
        // this > other = 1

        [TestMethod]
        public void CompareTo_Uppercase()
        {
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");

            string testCase1 = "Compare uppercase characters: A < B";
            string testCase2 = "Compare uppercase characters: A == A";
            string testCase3 = "Compare uppercase characters: B > A";

            string input1 = "Movie(Title=A).CompareTo(Movie(Title=B))";
            string input2 = "Movie(Title=A).CompareTo(Movie(Title=A))";
            string input3 = "Movie(Title=B).CompareTo(Movie(Title=A))";

            UnitTesting.AssertIsEqual(testCase1, input1, -1, movie1.CompareTo(movie2), $"CompareTo does not return -1 when comparing {movie2.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase2, input2, 0, movie1.CompareTo(movie1), $"CompareTo does not return 0 when comparing {movie1.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase3, input3, 1, movie2.CompareTo(movie1), $"CompareTo does not return 1 when comparing {movie1.Title} to {movie2.Title}.");
        }

        [TestMethod]
        public void CompareTo_Lowercase()
        {
            IMovie movie1 = new Movie("a");
            IMovie movie2 = new Movie("b");

            string testCase1 = "Compare uppercase characters: a < b";
            string testCase2 = "Compare uppercase characters: a == b";
            string testCase3 = "Compare uppercase characters: b > a";

            string input1 = "Movie(Title=a).CompareTo(Movie(Title=b))";
            string input2 = "Movie(Title=a).CompareTo(Movie(Title=a))";
            string input3 = "Movie(Title=b).CompareTo(Movie(Title=a))";

            UnitTesting.AssertIsEqual(testCase1, input1, -1, movie1.CompareTo(movie2), $"CompareTo does not return -1 when comparing {movie2.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase2, input2, 0, movie1.CompareTo(movie1), $"CompareTo does not return 0 when comparing {movie1.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase3, input3, 1, movie2.CompareTo(movie1), $"CompareTo does not return 1 when comparing {movie1.Title} to {movie2.Title}.");
        }

        [TestMethod]
        public void CompareTo_Numeric()
        {
            IMovie movie1 = new Movie("0");
            IMovie movie2 = new Movie("1");

            string testCase1 = "Compare uppercase characters: 0 < 1";
            string testCase2 = "Compare uppercase characters: 0 == 1";
            string testCase3 = "Compare uppercase characters: 1 > 0";

            string input1 = "Movie(Title=0).CompareTo(Movie(Title=1))";
            string input2 = "Movie(Title=0).CompareTo(Movie(Title=0))";
            string input3 = "Movie(Title=1).CompareTo(Movie(Title=0))";

            int expected1 = -1;
            int expected2 = 0;
            int expected3 = 1;

            int actual1 = movie1.CompareTo(movie2);
            int actual2 = movie1.CompareTo(movie1);
            int actual3 = movie2.CompareTo(movie1);

            UnitTesting.AssertIsEqual(testCase1, input1, expected1, actual1, $"CompareTo does not return -1 when comparing {movie2.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase2, input2, expected2, actual2, $"CompareTo does not return 0 when comparing {movie1.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase3, input3, expected3, actual3, $"CompareTo does not return 1 when comparing {movie1.Title} to {movie2.Title}.");
        }

        [TestMethod]
        public void CompareTo_Mixed()
        {
            IMovie movie1 = new Movie("0");
            IMovie movie2 = new Movie("A");
            IMovie movie3 = new Movie("a");

            string testCase1 = "Compare number with uppercase character: 0 < A";
            string testCase2 = "Compare uppercase character with lowercase character: A < a";
            string input1 = "new Movie(Title=0).CompareTo(new Movie(Title=A))";
            string input2 = "new Movie(Title=A).CompareTo(new Movie(Title=a))";

            int expected = -1;
            int actual1 = movie1.CompareTo(movie2);
            int actual2 = movie2.CompareTo(movie3);

            UnitTesting.AssertIsEqual(testCase1, input1, expected, actual1, $"CompareTo does not return -1 when comparing {movie2.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase2, input2, expected, actual2, $"CompareTo does not return -1 when comparing {movie3.Title} to {movie2.Title}.");
        }

        [TestMethod]
        public void CompareTo_MultipleCharacters()
        {
            IMovie movie1 = new Movie("avatar 1");
            IMovie movie2 = new Movie("Avatar 1");
            IMovie movie3 = new Movie("Avatar 2");

            string testCase1 = "Compare two strings: \"avatar 1\" < \"Avatar 1\"";
            string testCase2 = "Compare two string: \"Avatar 1\" < \"Avatar 2\"";

            string input1 = "new Movie(Title=avatar 1).CompareTo(new Movie(Title=Avatar 1))";
            string input2 = "new Movie(Title=Avatar 1).CompareTo(new Movie(Title=Avatar 2))";

            int expected1 = 1;
            int expected2 = -1;
            int actual1 = movie1.CompareTo(movie2);
            int actual2 = movie2.CompareTo(movie3);

            UnitTesting.AssertIsEqual(testCase1, input1, expected1, actual1, $"CompareTo does not return 1 when comparing \"{movie2.Title}\" to \"{movie1.Title}\".");
            UnitTesting.AssertIsEqual(testCase2, input2, expected2, actual2, $"CompareTo does not return -1 when comparing \"{movie3.Title}\" to \"{movie2.Title}\".");
        }

        [TestMethod]
        public void CompareTo_EmptyAndAny()
        {
            IMovie movie1 = new Movie("");
            IMovie movie2_0 = new Movie("0");
            IMovie movie2_A = new Movie("A");
            IMovie movie2_a = new Movie("a");

            string testCase_0 = "Compare empty string with numeric character:  < 0";
            string testCase_A = "Compare empty string with uppercase character:  < A";
            string testCase_a = "Compare empty string with lowercase character:  < a";
            string input_0 = "new Movie(Title=).CompareTo(new Movie(Title=0))";
            string input_A = "new Movie(Title=).CompareTo(new Movie(Title=A))";
            string input_a = "new Movie(Title=).CompareTo(new Movie(Title=a))";

            int expected = -1;
            int actual_0 = movie1.CompareTo(movie2_0);
            int actual_A = movie1.CompareTo(movie2_A);
            int actual_a = movie1.CompareTo(movie2_a);

            UnitTesting.AssertIsEqual(testCase_0, input_0, expected, actual_0, $"CompareTo does not return -1 when comparing {movie2_0.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase_A, input_A, expected, actual_A, $"CompareTo does not return -1 when comparing {movie2_A.Title} to {movie1.Title}.");
            UnitTesting.AssertIsEqual(testCase_a, input_a, expected, actual_a, $"CompareTo does not return -1 when comparing {movie2_a.Title} to {movie1.Title}.");
        }

        [TestMethod]
        public void ToString_FullConstructor()
        {
            IMovie movie = new Movie("Avatar: The Way of Water", MovieGenre.Action, MovieClassification.PG, 192, 20);

            string testCase = "Print Movie when all parameters are supplied.";
            string input = "new Movie(\"Avatar: The Way of Water\", MovieGenre.Action, MovieClassification.PG, 192, 20).ToString()";

            string expected = "Movie(Title=Avatar: The Way of Water, Genre=Action, Classification=PG, Duration=192, AvailableCopies=20)";
            string actual = movie.ToString();

            UnitTesting.AssertIsEqual(testCase, input, expected, actual, $"ToString does not return the correct string.");
        }

        [TestMethod]
        public void ToString_PartialConstructor()
        {
            IMovie movie = new Movie("Avatar: The Way of Water");

            string testCase = "Print Movie when only Title is supplied.";
            string input = "new Movie(\"Avatar: The Way of Water\").ToString()";

            string expected = "Movie(Title=Avatar: The Way of Water, Genre=0, Classification=0, Duration=0, AvailableCopies=0)";
            string actual = movie.ToString();

            UnitTesting.AssertIsEqual(testCase, input, expected, actual, $"ToString does not return the correct string.");
        }
    
        [TestMethod]
        public void ToString_PartialConstructor_EmptyTitle()
        {
            IMovie movie = new Movie("");

            string testCase = "Print Movie when only Title is supplied and the Title is an empty string.";
            string input = "new Movie(\"\").ToString()";

            string expected = "Movie(Title=, Genre=0, Classification=0, Duration=0, AvailableCopies=0)";
            string actual = movie.ToString();

            UnitTesting.AssertIsEqual(testCase, input, expected, actual, $"ToString does not return the correct string.");
        }
    }
}