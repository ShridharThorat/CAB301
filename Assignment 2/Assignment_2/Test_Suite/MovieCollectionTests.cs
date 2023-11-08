using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Test_Suite
{
    [TestClass]
    public class MovieCollectionTests : IMovieCollectionTests
    {
        MovieCollection coll1 = new MovieCollection();
        Movie mov0 = new Movie("Ar", MovieGenre.Action, MovieClassification.M, 301, 1);
        Movie mov1 = new Movie("Av", MovieGenre.Action, MovieClassification.M, 301, 1);
        Movie mov2 = new Movie("B",  MovieGenre.Action, MovieClassification.M, 301, 1);
        Movie mov3 = new Movie("C",  MovieGenre.Action, MovieClassification.M, 301, 1);
        Movie mov4 = new Movie("D",  MovieGenre.Action, MovieClassification.M, 301, 1);
        Movie mov5 = new Movie("Ca", MovieGenre.Action, MovieClassification.M, 301, 1);
        Movie mov6 = new Movie("E",  MovieGenre.Action, MovieClassification.M, 301, 1);



        // IsEmpty
        [TestMethod]
        public void IsEmpty_True_empty_collection()
        {
            int oldNumber = coll1.Number;
            bool result = coll1.IsEmpty();
            int newNumber = coll1.Number;
            Assert.IsTrue((result == true) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void IsEmpty_True_after_deleting_single_collection()
        {            
            coll1.Insert(mov0); // Insert a Movie so it isn't empty
            coll1.Delete(mov0); // Delete the movie so it is empty again
            int oldNumber = coll1.Number;
            bool result = coll1.IsEmpty();
            int newNumber = coll1.Number;
            Assert.IsTrue((result == true) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void IsEmpty_True_after_deleting_large_collection()
        {            
            // Insert lots of movies
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);

            // Delete all the movies so it is empty again
            coll1.Delete(mov1); coll1.Delete(mov3); coll1.Delete(mov6);
            coll1.Delete(mov5); coll1.Delete(mov4);
            int oldNumber = coll1.Number;
            bool result = coll1.IsEmpty();
            int newNumber = coll1.Number;
            Assert.IsTrue((result == true) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void IsEmpty_False_single_collection()
        {            
            coll1.Insert(mov0); // Insert a Movie so it isn't empty
            int oldNumber = coll1.Number;
            bool result = coll1.IsEmpty();
            int newNumber = coll1.Number;
            Assert.IsTrue((result == false) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void IsEmpty_False_large_collection()
        {
            // Insert lots of movies
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);
            int oldNumber = coll1.Number;
            bool result = coll1.IsEmpty();
            int newNumber = coll1.Number;
            Assert.IsTrue((result == false) && (oldNumber == newNumber));
        }


        // Insert
        [TestMethod]
        public void Insert_root()
        {
            int oldNumber = coll1.Number;
            bool result = coll1.Insert(mov0);
            int newNumber = coll1.Number;
            
            Assert.IsTrue((result == true) && ((oldNumber+1) == newNumber));
        }
        [TestMethod]
        public void Insert_root_LChild()
        {
            // Get the console output stream
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut); // Start reading

            int oldNumber = coll1.Number;
            coll1.Insert(mov2); // root
            coll1.Insert(mov1); // LChild of mov2
            int newNumber = coll1.Number;

            ////Read the last line from the console output stream
            //var consoleLines = consoleOut.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            //var actual = consoleLines[consoleLines.Length - 1];
            //var expected = "movie is a LChild"; // From insert method

            //coll1.PrettyPrint();
            //Assert.IsTrue(actual == expected && ((oldNumber+2) == newNumber));

        }
        [TestMethod]
        public void Insert_root_RChild()
        {
            // Get the console output stream
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut); // Start reading

            int oldNumber = coll1.Number;
            coll1.Insert(mov2); // root
            coll1.Insert(mov3); // RChild of mov2
            int newNumber = coll1.Number;

            //// Read the last line from the console output stream
            //var consoleLines = consoleOut.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            //var actual = consoleLines[consoleLines.Length - 1];
            //var expected = "movie is a RChild"; // From insert method

            //coll1.PrettyPrint();
            //Assert.IsTrue(actual == expected && ((oldNumber + 2) == newNumber));
        }
        [TestMethod]
        public void Insert__duplicate()
        {
            
            coll1.Insert(mov0);
            int oldNumber = coll1.Number;
            bool result = coll1.Insert(mov0);
            int newNumber = coll1.Number;
            Assert.IsTrue( (result == false) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void Insert_multiple_movies()
        {
            int oldNumber = coll1.Number;
            bool result = (coll1.Insert(mov3) && coll1.Insert(mov1)
                && coll1.Insert(mov0) && coll1.Insert(mov2));
            int newNumber = coll1.Number;
            Assert.IsTrue((result == true) && ((oldNumber+4) == newNumber));
        }


        // ToArray
        [TestMethod]
        public void ToArray_empty_collection()
        {
            IMovie[] expected = Array.Empty<IMovie>();
            IMovie[] actual = coll1.ToArray();
            Console.WriteLine("Expected");
            foreach (IMovie movie in expected) Console.WriteLine(movie);

            Console.WriteLine("\nActual");
            foreach (IMovie movie in actual) Console.WriteLine(movie);

            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }
        [TestMethod]
        public void ToArray_single_collection()
        {
            coll1.Insert(mov0);
            IMovie[] expected = new IMovie[1] { mov0 };
            IMovie[] actual = coll1.ToArray();

            Console.WriteLine("Expected");
            foreach (IMovie movie in expected) Console.WriteLine(movie);

            Console.WriteLine("\nActual");
            foreach (IMovie movie in actual) Console.WriteLine(movie);

            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }
        [TestMethod]
        public void ToArray_large_collection()
        {
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);
            IMovie[] expected = new IMovie[5] { mov1, mov3, mov5, mov4, mov6 };
            IMovie[] actual = coll1.ToArray();

            Console.WriteLine("Expected");
            foreach (IMovie movie in expected) Console.WriteLine(movie);

            Console.WriteLine("\nActual");
            foreach (IMovie movie in actual) Console.WriteLine(movie);

            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }


        // Delete        
        [TestMethod]
        public void Delete_node_not_in_single_collection()
        {
            coll1.Insert(mov0);

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov2); // "B"  doesn't exist
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[1] { mov0 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == false) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber));
        }
        [TestMethod]
        public void Delete_node_not_in_large_collection()
        {
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov2); // "B"  doesn't exist
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[5] { mov1, mov3, mov5, mov4, mov6 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == false) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber));
        }
        [TestMethod]
        public void Delete_node_from_empty_collection()
        {
            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov0); // "Ar"  doesn't exist
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[0];
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == false) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber));
        }
        [TestMethod]
        public void Delete_null_node_in_single_collection()
        {
            coll1.Insert(mov0);

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(null); // null doesn't exist'
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[1] { mov0 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == false) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber));
        }
        [TestMethod]
        public void Delete_root_single_collection()
        {
            coll1.Insert(mov0); // "Ar"  root

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov0); // "Ar"  root
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[0];
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov0.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_root_has_only_LChild_leaf()
        {
            coll1.Insert(mov2); // "B"  root
            coll1.Insert(mov1); // "Av" root.LChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov2); // "B"  root
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[1] { mov1 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov2.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_root_has_only_RChild_leaf()
        {
            coll1.Insert(mov2); // "B" root
            coll1.Insert(mov3); // "C" root.RChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov2); // "B" root
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[1] { mov3 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov2.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_root_has_LChild_and_RChild_as_leaves()
        {
            coll1.Insert(mov3); // "C"  root
            coll1.Insert(mov4); // "D"  root.RChild
            coll1.Insert(mov2); // "Av" root.LChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov3); // "C"  root
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[2] { mov2, mov4 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov3.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_node_has_only_LChild_leaf()
        {
            coll1.Insert(mov2); // "B"  root
            coll1.Insert(mov3); // "C" root.RChild 
            coll1.Insert(mov1); // "Av" root.LChild
            coll1.Insert(mov0); // "Ar" root.LChild.LChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov1); // "Av" root.LChild
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[3] { mov0, mov2, mov3 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov1.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_node_has_only_RChild_leaf()
        {
            coll1.Insert(mov0); // "Ar" root
            coll1.Insert(mov1); // "Av" root.RChild
            coll1.Insert(mov2); // "B"  root.RChild.RChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov2); // "Ar" root.RChild.RChild
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[2] { mov0, mov1 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov2.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_node_has_LChild_and_RChild_as_leaves()
        {
            coll1.Insert(mov1); // "Av" root
            coll1.Insert(mov3); // "C"  root.RChild        
            coll1.Insert(mov2); // "B"  root.RChild.LChild
            coll1.Insert(mov4); // "D"  root.RChild.RChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov3); // "C" root.LChild        
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[3] { mov1, mov2, mov4 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov3.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_node_has_LChild_with_LeftSkewedtree__and_RChild_as_leaf()
        {
            coll1.Insert(mov0); // "Ar"       root
            coll1.Insert(mov3); // "C"        root.RChild        
            coll1.Insert(mov2); // "B"        root.RChild.LChild
            coll1.Insert(mov5); // "Ca"       root.RChild.RChild
            coll1.Insert(mov1); // "Av"       root.RChild.LChild.RChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov3); // "C" root.LChild        
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[4] { mov0, mov1, mov2, mov5 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov3.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }
        [TestMethod]
        public void Delete_node_has_LChild_with_RightSkewedtree_and_RChild_as_leaf()
        {
            coll1.Insert(mov6); // "E"  root
            coll1.Insert(mov3); // "C"  root.LChild
            coll1.Insert(mov4); // "D"  root.LChild.RChild
            coll1.Insert(mov0); // "Ar" root.LChild.LChild
            coll1.Insert(mov1); // "Av" root.LChild.LChild.RChild
            coll1.Insert(mov2); // "B"  root.LChild.LChild.RChild.RChild

            int oldNumber = coll1.Number;
            bool deletedResult = coll1.Delete(mov3); // "C" root.LChild        
            int newNumber = coll1.Number;

            IMovie[] expectedArray = new IMovie[5] { mov0, mov1, mov2, mov4, mov6 };
            IMovie[] actualArray = coll1.ToArray();

            bool arrayAssertion = Enumerable.SequenceEqual(expectedArray, actualArray);
            Assert.IsTrue((deletedResult == true) && (coll1.Search(mov3.Title) == null) &&
                           (arrayAssertion == true) &&
                           (newNumber == oldNumber - 1));
        }



        // Search
        [TestMethod]
        public void Search_empty_collection()
        {
            int oldNumber = coll1.Number;
            IMovie? result = coll1.Search(mov0.Title);
            int newNumber = coll1.Number;
            Assert.IsTrue((result == null) && (oldNumber==newNumber));
        }
        [TestMethod]
        public void Search_exists_single_collection()
        {
            coll1.Insert(mov0); 
            int oldNumber = coll1.Number;
            IMovie? result = coll1.Search(mov0.Title);
            int newNumber = coll1.Number;
            Assert.IsTrue((object.ReferenceEquals(mov0,result))
                && (oldNumber == newNumber));
        }
        [TestMethod]
        public void Search_doesnt_exist_single_collection()
        {
            coll1.Insert(mov0);
            int oldNumber = coll1.Number;
            IMovie? result = coll1.Search(mov1.Title);
            int newNumber = coll1.Number;
            Assert.IsTrue((result == null) && (oldNumber == newNumber));

        }
        [TestMethod]
        public void Search_exists_large_collection()
        {
            coll1.Insert(mov1); coll1.Insert(mov0); coll1.Insert(mov2);
            int oldNumber = coll1.Number;
            IMovie? result = coll1.Search(mov1.Title);
            int newNumber = coll1.Number;
            Assert.IsTrue((object.ReferenceEquals(mov1, result))
                && (oldNumber == newNumber));
        }
        [TestMethod]
        public void Search_doesnt_exist_large_collection()
        {
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);
            int oldNumber = coll1.Number;
            IMovie? result = coll1.Search(mov1.Title);
            int newNumber = coll1.Number;
            Assert.IsTrue((result == null) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void Search_null_in_single_collection()
        {
            coll1.Insert(mov0);
            int oldNumber = coll1.Number;
            IMovie? result = coll1.Search(null);
            int newNumber = coll1.Number;
            Assert.IsTrue((result == null) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void Search_null_in_large_collection()
        {
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);
            int oldNumber = coll1.Number;
            IMovie? result = coll1.Search(null);
            int newNumber = coll1.Number;
            Assert.IsTrue((result == null) && (oldNumber == newNumber));
        }


        // Clear
        [TestMethod]
        public void Clear_single_collection()
        {
            coll1.Insert(mov0);
            int oldNumber = coll1.Number; // 1
            coll1.Clear();
            int newNumber = coll1.Number; // 0
            Assert.IsTrue(newNumber == 0);
        }
        [TestMethod]
        public void Clear_empty_collection()
        {
            int oldNumber = coll1.Number; // 0
            coll1.Clear();
            int newNumber = coll1.Number; // 0

            Assert.IsTrue(newNumber == 0);
        }
        [TestMethod]
        public void Clear_large_collection()
        {
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);
            int oldNumber = coll1.Number; // 5
            coll1.Clear();
            int newNumber = coll1.Number; // 0

            Assert.IsTrue(newNumber == 0);
        }


        //NoDVDS
        [TestMethod]
        public void NoDVDS_empty_collection()
        {
            int oldNumber = coll1.Number; // 0
            int dvds = coll1.NoDVDs();    // 0
            int newNumber = coll1.Number; // 0
            Assert.IsTrue( (dvds == 0) && (oldNumber == newNumber) );
        }
        [TestMethod]
        public void NoDVDS_single_collection()
        {
            coll1.Insert(mov0);
            int oldNumber = coll1.Number; // 5
            int dvds = coll1.NoDVDs();    // 1
            int newNumber = coll1.Number; // 5
            Assert.IsTrue((dvds == 1) && (oldNumber == newNumber));
        }
        [TestMethod]
        public void NoDVDS_large_collection()
        {
            coll1.Insert(mov1); coll1.Insert(mov3); coll1.Insert(mov6);
            coll1.Insert(mov5); coll1.Insert(mov4);

            int oldNumber = coll1.Number; // 5
            int dvds = coll1.NoDVDs();
            int newNumber = coll1.Number; // 5
            Assert.IsTrue((dvds == 5) && (oldNumber == newNumber));
        }


    }
}

