using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace UnitTesting
{
    [TestClass]
    public class MovieCollectionTests
    {
        [TestMethod]
        public void IsEmpty_EmptyTree()
        {
            IMovieCollection collection = new MovieCollection();

            string testCase = "Check if IsEmpty returns false on empty tree.";
            IMovieCollection input = collection.CloneCollection();

            bool expected = true;
            bool actual = collection.IsEmpty();

            UnitTesting.AssertIsEqual(testCase, input, expected, actual,
                "IsEmpty incorrectly returns false on an empty collection.");
        }

        [TestMethod]
        public void IsEmpty_NonEmptyTree()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            
            collection.Insert(movie1);
            IMovieCollection input1 = collection.CloneCollection();

            string testCase1 = "Check if IsEmpty returns true on tree with one node.";
            string testCase2 = "Check if IsEmpty returns true on tree with multiple nodes.";

            bool expected1 = false;
            bool actual1 = collection.IsEmpty();

            collection.Insert(movie2);
            collection.Insert(movie3);
            collection.Insert(movie4);
            IMovieCollection input2 = collection.CloneCollection();

            bool expected2 = false;
            bool actual2 = collection.IsEmpty();

            UnitTesting.AssertIsEqual(testCase1, input1, expected1, actual1,
                "IsEmpty incorrectly returns true on a collection with one node.");
            UnitTesting.AssertIsEqual(testCase2, input2, expected2, actual2,
                "IsEmpty incorrectly returns true on a collection with multiple nodes.");
        }

        [TestMethod]
        public void IsEmpty_AfterDeletingNode()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");
            collection.Insert(movie);

            string testCase = "Check if IsEmpty returns true after deleting root node.";
            IMovieCollection input = collection.CloneCollection();

            collection.Delete(movie);
            bool expected = true;
            bool actual = collection.IsEmpty();

            UnitTesting.AssertIsEqual(testCase, input, expected, actual,
                "IsEmpty incorrectly returns false on an empty collection.");
        }

        [TestMethod]
        public void Clear_EmptyTree()
        {
            IMovieCollection collection = new MovieCollection();

            string testCase = "Clear an empty tree, and ensure that Number = 0.";

            IMovieCollection before = collection.CloneCollection();
            collection.Clear();
            IMovieCollection after = collection;

            IMovie[] expected = Array.Empty<IMovie>();
            IMovie[] actual = collection.ToArray();

            int expectedPost = 0;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Clear does not clear an empty collection.",
                "Clear does not result in Number = 0.");
        }

        [TestMethod]
        public void Clear_TreeWithRootNode()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");
            collection.Insert(movie);

            string testCase = "Clear a tree with root node, and ensure that Number = 0.";

            IMovieCollection before = collection.CloneCollection();
            collection.Clear();
            IMovieCollection after = collection;

            IMovie[] expected = Array.Empty<IMovie>();
            IMovie[] actual = collection.ToArray();

            int expectedPost = 0;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Clear does not clear a collection with 1 movie.",
                "Clear does not result in Number = 0.");
        }

        [TestMethod]
        public void Clear_TreeWithMultipleNodes()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie3);
            collection.Insert(movie5);
            collection.Insert(movie7);
            collection.Insert(movie6);
            collection.Insert(movie4);
            collection.Insert(movie2);
            collection.Insert(movie8);
            collection.Insert(movie1);

            string testCase = "Clear a tree with multiple nodes, and ensure that Number = 0.";

            IMovieCollection before = collection.CloneCollection();
            collection.Clear();
            IMovieCollection after = collection;

            IMovie[] expected = Array.Empty<IMovie>();
            IMovie[] actual = collection.ToArray();

            int expectedPost = 0;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Clear does not clear a collection with 8 movies.",
                "Clear does not result in Number = 0.");
        }

        [TestMethod]
        public void Search_ContainsMovie_TreeWithRootNode()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");

            collection.Insert(movie);
            int numberOfNodes = collection.Number;

            string testCase = "Search a tree with root node for movie with title \"A\" that exists in tree and ensure Number is unchanged.";

            IMovieCollection before = collection.CloneCollection();
            IMovie? searchedMovie = collection.Search(movie.Title);
            IMovieCollection after = collection;

            IMovie? expected = movie;
            IMovie? actual = searchedMovie;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Search does not find movie from collection with 1 movie.",
                "Search modifies Number.");
        }

        [TestMethod]
        public void Search_ContainsMovie_TreeWithMultipleNodes()
        {
            IMovieCollection collection = new MovieCollection();

            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");

            IMovie[] movies = { movie1, movie2, movie3 };

            IEnumerable<int> availableIndices = new List<int>() { 0, 1, 2 };
            IEnumerable<IEnumerable<int>> permutations = ListExtension.GetPermutations<int>(availableIndices);

            foreach (IEnumerable<int> indices in permutations)
            {
                foreach (int index in indices)
                    collection.Insert(movies[index]);

                int numberOfNodes = collection.Number;

                string testCase = "Search a tree with multiple nodes for movie with title \"A\" that exists in tree and ensure Number is unchanged.";

                IMovieCollection before = collection.CloneCollection();
                IMovie? searchedMovie = collection.Search("A");
                IMovieCollection after = collection;

                IMovie? expected = movies[0];
                IMovie? actual = searchedMovie;

                int expectedPost = numberOfNodes;
                int actualPost = collection.Number;

                UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                    "Search does not find a movie from collection of 3 movies.",
                    "Search modifies Number.");

                collection.Clear();
            }
        }

        [TestMethod]
        public void Search_DoesNotContainMovie_Empty()
        {
            IMovieCollection collection = new MovieCollection();

            int numberOfNodes = collection.Number;

            string testCase = "Search an empty tree for movie with title \"X\" that does not exist in tree and ensure Number is unchanged.";

            IMovieCollection before = collection.CloneCollection();
            IMovie? searchedMovie = collection.Search("X");
            IMovieCollection after = collection;

            IMovie? expected = null;
            IMovie? actual = searchedMovie;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Search does not return null when searching for a nonexistent movie from an empty collection.",
                "Search modifies Number.");
        }

        [TestMethod]
        public void Search_DoesNotContainMovie_TreeWithRootNode()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");

            collection.Insert(movie);
            int numberOfNodes = collection.Number;

            string testCase = "Search a tree with root node for movie with title \"X\" that does not exist in tree and ensure Number is unchanged.";

            IMovieCollection before = collection.CloneCollection();
            IMovie? searchedMovie = collection.Search("X");
            IMovieCollection after = collection;

            IMovie? expected = null;
            IMovie? actual = searchedMovie;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Search does not return null when searching for a nonexistent movie from collection with 1 movie.",
                "Search modifies Number.");
        }

        [TestMethod]
        public void Search_DoesNotContainMovie_TreeWithMultipleNodes()
        {
            IMovieCollection collection = new MovieCollection();

            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");

            IMovie[] movies = { movie1, movie2, movie3 };

            IEnumerable<int> availableIndices = new List<int>() { 0, 1, 2 };
            IEnumerable<IEnumerable<int>> permutations = ListExtension.GetPermutations<int>(availableIndices);

            foreach (IEnumerable<int> indices in permutations)
            {
                foreach (int index in indices)
                    collection.Insert(movies[index]);

                int numberOfNodes = collection.Number;

                string testCase = "Search a tree with multiple nodes for movie with title \"X\" that does not exist in tree and ensure Number is unchanged.";

                IMovieCollection before = collection.CloneCollection();
                IMovie? searchedMovie = collection.Search("X");
                IMovieCollection after = collection;

                IMovie? expected = null;
                IMovie? actual = searchedMovie;

                int expectedPost = numberOfNodes;
                int actualPost = collection.Number;

                UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                    "Search does not return null when searching for a nonexistent movie from collection of 3 movies.",
                    "Search modifies Number.");

                collection.Clear();
            }
        }

        [TestMethod]
        public void Insert_Valid_EmptyTree()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");

            int numberOfNodes = collection.Number;
            string testCase = "Insert node \"A\" into empty tree and ensure Number is incremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Insert(movie);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes + 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Insert does not return true when inserting a distinct movie into an empty collection.",
                "Insert does not increment Number.");
        }

        [TestMethod]
        public void Insert_Valid_TreeWithRootNode_InsertLeft()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("B");
            IMovie movie2 = new Movie("A");
            collection.Insert(movie1);

            int numberOfNodes = collection.Number;
            string testCase = "Insert node \"A\" left of root node and ensure Number is incremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Insert(movie2);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes + 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Insert does not return true when inserting a distinct movie into an empty collection.",
                "Insert does not increment Number.");
        }

        [TestMethod]
        public void Insert_Valid_TreeWithRootNode_InsertRight()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            collection.Insert(movie1);

            int numberOfNodes = collection.Number;
            string testCase = "Insert node \"B\" right of root node and ensure Number is incremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Insert(movie2);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes + 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Insert does not return true when inserting a distinct movie into a collection with 1 movie.",
                "Insert does not increment Number.");
        }

        [TestMethod]
        public void Insert_Valid_TreeWithMultipleNodes()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie3);
            collection.Insert(movie5);
            collection.Insert(movie7);
            collection.Insert(movie6);
            collection.Insert(movie4);
            collection.Insert(movie2);
            collection.Insert(movie8);

            int numberOfNodes = collection.Number;
            string testCase = "Insert node \"A\" given a tree with multiple nodes and ensure Number is incremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Insert(movie1);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes + 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Insert does not return true when inserting a distinct movie into a collection with multiple movies.",
                "Insert does not increment Number.");
        }

        [TestMethod]
        public void Insert_Invalid_TreeWithRootNode()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("A");
            collection.Insert(movie1);

            int numberOfNodes = collection.Number;
            string testCase = "Insert duplicate node \"A\" given tree with root node and ensure Number is invariant.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Insert(movie2);
            IMovieCollection after = collection;

            bool expected = false;
            bool actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Insert does not return false when inserting a duplicate movie into a collection with 1 movie.",
                "Insert inappropriately modifies Number.");
        }

        [TestMethod]
        public void Insert_Invalid_TreeWithMultipleNodes()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie3);
            collection.Insert(movie5);
            collection.Insert(movie7);
            collection.Insert(movie6);
            collection.Insert(movie4);
            collection.Insert(movie2);
            collection.Insert(movie8);
            collection.Insert(movie1);

            int numberOfNodes = collection.Number;
            string testCase = "Insert duplicate node \"A\" given tree with multiple nodes and ensure Number is invariant.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Insert(movie1);
            IMovieCollection after = collection;

            bool expected = false;
            bool actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Insert does not return false when inserting a duplicate movie into a collection with multiple movies.",
                "Insert inappropriately modifies Number.");
        }

        [TestMethod]
        public void Delete_Valid_Root_TreeWithRoot()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");
            collection.Insert(movie);

            int numberOfNodes = collection.Number;
            string testCase = "Delete root node \"A\" from tree with root, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with 1 movie.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_Root_TreeWithRootAndLeft()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("B");
            IMovie movie2 = new Movie("A");
            collection.Insert(movie1);
            collection.Insert(movie2);

            int numberOfNodes = collection.Number;
            string testCase = "Delete root node \"B\" from tree with root and left child, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie1);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with 2 movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_Root_TreeWithRootAndRight()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            collection.Insert(movie1);
            collection.Insert(movie2);

            int numberOfNodes = collection.Number;
            string testCase = "Delete root node \"A\" from tree with root and right child, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie1);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with 2 movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_Root_TreeWithRootLeftAndRight()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("B");
            IMovie movie2 = new Movie("A");
            IMovie movie3 = new Movie("C");
            collection.Insert(movie1);
            collection.Insert(movie2);
            collection.Insert(movie3);

            int numberOfNodes = collection.Number;
            string testCase = "Delete root node \"B\" from tree with root and left/right children, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie1);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with 3 movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_LeftLeaf()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("B");
            IMovie movie2 = new Movie("A");
            IMovie movie3 = new Movie("C");
            collection.Insert(movie1);
            collection.Insert(movie2);
            collection.Insert(movie3);

            int numberOfNodes = collection.Number;
            string testCase = "Delete left leaf \"A\" from tree with root and left/right children, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie2);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with 3 movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_RightLeaf()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("B");
            IMovie movie2 = new Movie("A");
            IMovie movie3 = new Movie("C");
            collection.Insert(movie1);
            collection.Insert(movie2);
            collection.Insert(movie3);

            int numberOfNodes = collection.Number;
            string testCase = "Delete right leaf \"C\" from tree with root and left/right children, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie3);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with 3 movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_NodeWithLeftSubtree()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie3);
            collection.Insert(movie5);
            collection.Insert(movie8);
            collection.Insert(movie7);
            collection.Insert(movie6);
            collection.Insert(movie4);
            collection.Insert(movie2);
            collection.Insert(movie1);
            collection.PrettyPrint();

            int numberOfNodes = collection.Number;
            string testCase = "Delete node \"H\" with left subtree from tree, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie8);
            IMovieCollection after = collection;
            collection.PrettyPrint();

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with multiple movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_NodeWithRightSubtree()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie3);
            collection.Insert(movie5);
            collection.Insert(movie6);
            collection.Insert(movie7);
            collection.Insert(movie4);
            collection.Insert(movie2);
            collection.Insert(movie8);
            collection.Insert(movie1);
            collection.PrettyPrint();

            int numberOfNodes = collection.Number;
            string testCase = "Delete node \"F\" with right subtree from tree, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie6);
            IMovieCollection after = collection;
            collection.PrettyPrint();

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with multiple movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_Node_ImmediateSuccessor()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie3);
            collection.Insert(movie5);
            collection.Insert(movie7);
            collection.Insert(movie6);
            collection.Insert(movie4);
            collection.Insert(movie2);
            collection.Insert(movie8);
            collection.Insert(movie1);
            collection.PrettyPrint();
            int numberOfNodes = collection.Number;
            string testCase = "Delete node \"E\" with left child and right subtree, where the successor is the left child, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie5); collection.PrettyPrint();
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with multiple movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Valid_Node()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie2);
            collection.Insert(movie6);
            collection.Insert(movie4);
            collection.Insert(movie3);
            collection.Insert(movie7);
            collection.Insert(movie5);
            collection.Insert(movie1);
            collection.Insert(movie8);

            int numberOfNodes = collection.Number;
            string testCase = "Delete node \"F\" with left/right subtree, where the successor is the rightmost node in the left subtree, and ensure Number is decremented.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie6);
            IMovieCollection after = collection;

            bool expected = true;
            bool actual = returnValue;

            int expectedPost = numberOfNodes - 1;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete does not return true when deleting an existent movie from a collection with multiple movies.",
                "Delete does not decrement Number.");
        }

        [TestMethod]
        public void Delete_Invalid_EmptyTree()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");

            int numberOfNodes = collection.Number;
            string testCase = "Delete nonexistent node \"A\" from empty tree, and ensure Number is invariant.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movie);
            IMovieCollection after = collection;

            bool expected = false;
            bool actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete returns true when deleting a non-existent movie from an empty collection.",
                "Delete inappropriately modifies Number.");
        }

        [TestMethod]
        public void Delete_Invalid_NonEmptyTree()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie1 = new Movie("A");
            IMovie movie2 = new Movie("B");
            IMovie movie3 = new Movie("C");
            IMovie movie4 = new Movie("D");
            IMovie movie5 = new Movie("E");
            IMovie movie6 = new Movie("F");
            IMovie movie7 = new Movie("G");
            IMovie movie8 = new Movie("H");

            collection.Insert(movie2);
            collection.Insert(movie6);
            collection.Insert(movie4);
            collection.Insert(movie3);
            collection.Insert(movie7);
            collection.Insert(movie5);
            collection.Insert(movie1);
            collection.Insert(movie8);

            IMovie movieToDelete = new Movie("X");

            int numberOfNodes = collection.Number;
            string testCase = "Delete nonexistent node \"X\" from nonempty tree, and ensure Number is invariant.";

            IMovieCollection before = collection.CloneCollection();
            bool returnValue = collection.Delete(movieToDelete);
            IMovieCollection after = collection;

            bool expected = false;
            bool actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "Delete returns true when deleting a non-existent movie from an empty collection.",
                "Delete inappropriately modifies Number.");
        }

        [TestMethod]
        public void NoDVDs_EmptyTree()
        {
            IMovieCollection collection = new MovieCollection();

            int numberOfNodes = collection.Number;
            string testCase = "NoDVDs returns 0 on empty tree, ensuring the tree and Number are invariant.";

            IMovieCollection before = collection.CloneCollection();
            int returnValue = collection.NoDVDs();
            IMovieCollection after = collection;

            int expected = 0;
            int actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                "NoDVDs does not return 0 on an empty collection.",
                "NoDVDs inappropriately modifies Number.");
        }

        [TestMethod]
        public void NoDVDs_TreeWithRoot()
        {
            IMovieCollection collection = new MovieCollection();

            int expected = 0;

            int copies = 5;
            IMovie movie = new Movie("A", 0, 0, 0, copies);
            expected += copies;

            collection.Insert(movie);

            int numberOfNodes = collection.Number;
            string testCase = $"NoDVDs returns {expected} on tree with root node, ensuring the tree and Number are invariant.";

            IMovieCollection before = collection.CloneCollection();
            int returnValue = collection.NoDVDs();
            IMovieCollection after = collection;

            int actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                $"NoDVDs does not return {expected} on a collection with 1 movie.",
                "NoDVDs inappropriately modifies Number.");
        }

        [TestMethod]
        public void NoDVDs_TreeWithMultipleNodes()
        {
            IMovieCollection collection = new MovieCollection();

            int expected = 0;

            string[] titles = { "A", "B", "C", "D", "E", "F", "G", "H" };

            for (int copies = 0; copies < titles.Length; copies++)
            {
                collection.Insert(new Movie(titles[copies], 0, 0, 0, copies));
                expected += copies;
            }
            collection.PrettyPrint();

            int numberOfNodes = collection.Number;
            string testCase = $"NoDVDs returns {expected} on tree with root node, ensuring the tree and Number are invariant.";

            IMovieCollection before = collection.CloneCollection();
            int returnValue = collection.NoDVDs();
            IMovieCollection after = collection;

            int actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                $"NoDVDs does not return {expected} on a collection with multiple movies.",
                "NoDVDs inappropriately modifies Number.");
        }

        [TestMethod]
        public void NoDVDs_AfterDeletingNode()
        {
            IMovieCollection collection = new MovieCollection();

            int expected = 0;

            string[] titles = { "A", "B", "C", "D", "E", "F", "G", "H" };

            for (int copies = 0; copies < titles.Length; copies++)
            {
                collection.Insert(new Movie(titles[copies], 0, 0, 0, copies));
                expected += copies;
            }

            string testCase = $"NoDVDs returns {expected} on tree after deleting a node, ensuring the tree and Number are invariant.";

            IMovieCollection before = collection.CloneCollection();
            collection.Delete(new Movie("H"));
            expected -= 7;

            int numberOfNodes = collection.Number;
            int returnValue = collection.NoDVDs();
            IMovieCollection after = collection;

            int actual = returnValue;

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, before, after, expected, actual, expectedPost, actualPost,
                $"NoDVDs does not return {expected} on a collection with multiple movies.",
                "NoDVDs inappropriately modifies Number.");
        }

        [TestMethod]
        public void ToArray_IsSorted()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movieLower1 = new Movie("a");
            IMovie movieLower2 = new Movie("b");
            IMovie movieLower3 = new Movie("c");

            IMovie movieUpper1 = new Movie("A");
            IMovie movieUpper2 = new Movie("B");
            IMovie movieUpper3 = new Movie("C");

            IMovie movieNumeric1 = new Movie("0");
            IMovie movieNumeric2 = new Movie("1");
            IMovie movieNumeric3 = new Movie("2");

            collection.Insert(movieLower1);
            collection.Insert(movieUpper2);
            collection.Insert(movieNumeric1);
            collection.Insert(movieLower2);
            collection.Insert(movieUpper1);
            collection.Insert(movieNumeric3);
            collection.Insert(movieLower3);
            collection.Insert(movieUpper3);
            collection.Insert(movieNumeric2);

            int numberOfNodes = collection.Number;
            string testCase = "ToArray returns a lexicographically sorted array of movies, given a tree containing nodes with lowercase, uppercase, and numeric characters, when inserted in a random order, esnsuring the tree and Number are invariant.";

            IMovie[] expected = { 
                movieNumeric1, 
                movieNumeric2,
                movieNumeric3,
                movieUpper1 , 
                movieUpper2 , 
                movieUpper3,
                movieLower1 , 
                movieLower2,
                movieLower3
            };

            IMovie[] actual = collection.ToArray();

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, collection, expected, actual, expectedPost, actualPost,
                "ToArray does not return the correct array.",
                "ToArray inappropriately modifies Number.");
        }

        [TestMethod]
        public void ToArray_EmptyTree()
        {
            IMovieCollection collection = new MovieCollection();

            int numberOfNodes = collection.Number;
            string testCase = "ToArray returns a lexicographically sorted array of movies, given an empty tree, esnsuring the tree and Number are invariant.";

            IMovie[] expected = { };
            IMovie[] actual = collection.ToArray();

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, collection, expected, actual, expectedPost, actualPost,
                "ToArray does not return the correct array.",
                "ToArray inappropriately modifies Number.");
        }

        [TestMethod]
        public void ToArray_TreeWithRoot()
        {
            IMovieCollection collection = new MovieCollection();
            IMovie movie = new Movie("A");
            collection.Insert(movie);

            int numberOfNodes = collection.Number;
            string testCase = "ToArray returns a lexicographically sorted array of movies, given a tree with a root node, esnsuring the tree and Number are invariant.";

            IMovie[] expected = { movie };
            IMovie[] actual = collection.ToArray();

            int expectedPost = numberOfNodes;
            int actualPost = collection.Number;

            UnitTesting.AssertIsEqual(testCase, collection, expected, actual, expectedPost, actualPost,
                "ToArray does not return the correct array.",
                "ToArray inappropriately modifies Number.");
        }
    }
}