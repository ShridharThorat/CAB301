using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Reflection;

namespace UnitTesting
{
    public static class MovieCollectionExtension
    {
        public static IMovieCollection CloneCollection(this IMovieCollection collection)
        {
            IMovieCollection newCollection = new MovieCollection();

            // Get private field "root"
            var field = collection.GetType().GetField("root", BindingFlags.NonPublic | BindingFlags.Instance);
            BTreeNode root = (BTreeNode)field!.GetValue(collection)!;

            CloneTree(root, ref newCollection);

            return newCollection;
        }

         public static BTreeNode? CloneTree(BTreeNode? node, ref IMovieCollection collection)
        {
            if (node is null) return null;

            collection.Insert(node.Movie);
            BTreeNode newRoot = new(node.Movie)
            {
                LChild = CloneTree(node.LChild, ref collection),
                RChild = CloneTree(node.RChild, ref collection)
            };

            return newRoot;
        }
    }

    public static class ListExtension
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            var factorials = Enumerable.Range(0, array.Length + 1)
                .Select(Factorial)
                .ToArray();

            for (var i = 0L; i < factorials[array.Length]; i++)
            {
                var sequence = GenerateSequence(i, array.Length - 1, factorials);

                yield return GeneratePermutation(array, sequence);
            }
        }

        private static IEnumerable<T> GeneratePermutation<T>(T[] array, IReadOnlyList<int> sequence)
        {
            var clone = (T[])array.Clone();

            for (int i = 0; i < clone.Length - 1; i++)
                Swap(ref clone[i], ref clone[i + sequence[i]]);

            return clone;
        }

        private static int[] GenerateSequence(long number, int size, IReadOnlyList<long> factorials)
        {
            var sequence = new int[size];

            for (var j = 0; j < sequence.Length; j++)
            {
                var facto = factorials[sequence.Length - j];

                sequence[j] = (int)(number / facto);
                number = (int)(number % facto);
            }

            return sequence;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            (b, a) = (a, b);
        }

        private static long Factorial(int n)
        {
            long result = n;

            for (int i = 1; i < n; i++)
                result *= i;

            return result;
        }
    }

    public static class SystemExtension
    {
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }

    public class UnitTesting
    {
        private static bool wasLogFileCleared = false;
        private static string TEST_PATH = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName.ToString() + "/TestOutput.txt";
        private static int testID = 0;

        public static string PrintMovie(IMovie? movie)
        {
            return (movie is null) ? "null" : movie.Title;
        }

        public static string PrintMovieArray(IMovie[] movies)
        {
            string output = "{ ";

            for (int i = 0; i < movies.Length; i++)
            {
                output += PrintMovie(movies[i]);
                if (i < movies.Length - 1)
                    output += ", ";
            }

            output += " }";
            return output;
        }

        public static bool AreMoviesEqual(IMovie? movie1, IMovie? movie2)
        {
            if (movie1 == null || movie2 == null)
                if (movie1 is null && movie2 is null) return true; 
                else return false;
            else
                return (movie1.Title == movie2.Title) &&
                    (movie1.Genre == movie2.Genre) &&
                    (movie1.Classification == movie2.Classification) &&
                    (movie1.AvailableCopies == movie2.AvailableCopies) &&
                    (movie1.TotalCopies == movie2.TotalCopies);
        }

        public static bool AreMovieArraysEqual(IMovie[] array1, IMovie[] array2)
        {
            if (array1.Length != array2.Length) return false;

            foreach ((IMovie movie1, IMovie movie2) in array1.Zip(array2))
                if (!AreMoviesEqual(movie1, movie2)) return false;

            return true;
        }

        public static bool AreMovieCollectionsEqual(IMovieCollection collection1, IMovieCollection collection2)
        {
            IMovie[] movies1 = collection1.ToArray();
            IMovie[] movies2 = collection2.ToArray();

            return AreMovieArraysEqual(movies1, movies2);
        }

        public static void AssertIsEqual<T>(string testCase, IMovieCollection input, T expected, T actual, string message)
        {
            T expected1 = (T)(object)expected!;
            T actual1 = (T)(object)actual!;

            if (typeof(IMovie[]).IsAssignableFrom(typeof(T)))
            {
                // TestLog(testCase, PrintMovieArray(input.ToArray()), PrintMovieArray((IMovie[])(object)expected1), PrintMovieArray((IMovie[])(object)actual1));
                Assert.IsTrue(AreMovieArraysEqual((IMovie[])(object)expected1, (IMovie[])(object)actual1), message);
            }
            else if (typeof(string).IsAssignableFrom(typeof(T)))
            {
                // TestLog(testCase, PrintMovieArray(input.ToArray()), (string)(object)expected1, (string)(object)actual1);
                Assert.IsTrue((string)(object)expected1 == (string)(object)actual1, message);
            }
            else if (typeof(int).IsAssignableFrom(typeof(T)))
            {
                // TestLog(testCase, PrintMovieArray(input.ToArray()), ((int)(object)expected1).ToString(), ((int)(object)actual1).ToString());
                Assert.IsTrue((int)(object)expected1 == (int)(object)actual1, message);
            }
        }

        public static void AssertIsEqual<T>(string testCase, string input, T expected, T actual, string message)
        {
            T expected1 = (T)(object)expected!;
            T actual1 = (T)(object)actual!;

            if (typeof(IMovie[]).IsAssignableFrom(typeof(T)))
            {
                // TestLog(testCase, input, PrintMovieArray((IMovie[])(object)expected1), PrintMovieArray((IMovie[])(object)actual1));
                Assert.IsTrue(AreMovieArraysEqual((IMovie[])(object)expected1, (IMovie[])(object)actual1), message);
            }
            else if (typeof(string).IsAssignableFrom(typeof(T)))
            {
                // TestLog(testCase, input, (string)(object)expected1, (string)(object)actual1);
                Assert.IsTrue((string)(object)expected1 == (string)(object)actual1, message);
            }
            else if (typeof(int).IsAssignableFrom(typeof(T)))
            {
                // TestLog(testCase, input, ((int)(object)expected1).ToString(), ((int)(object)actual1).ToString());
                Assert.IsTrue((int)(object)expected1 == (int)(object)actual1, message);
            }
        }

        public static void AssertIsEqual(string testCase, IMovieCollection collection, IMovie[] expected, IMovie[] actual, int expectedPost, int actualPost, string message, string messagePost)
        {
            // TestLog(testCase, collection.PrettyPrint(), PrintMovieArray(collection.ToArray()), PrintMovieArray(expected), PrintMovieArray(actual), expectedPost.ToString(), actualPost.ToString());

            Assert.IsTrue(AreMovieArraysEqual(expected, actual), message);
            Assert.IsTrue(expectedPost == actualPost, messagePost);
        }

        public static void AssertIsEqual(string testCase, IMovieCollection before, IMovieCollection after, IMovie[] expected, IMovie[] actual, int expectedPost, int actualPost, string message, string messagePost)
        {
            // TestLog(testCase, before.PrettyPrint(), after.PrettyPrint(), PrintMovieArray(before.ToArray()), PrintMovieArray(after.ToArray()), PrintMovieArray(expected), PrintMovieArray(actual), expectedPost.ToString(), actualPost.ToString());

            Assert.IsTrue(AreMovieArraysEqual(expected, actual), message);
            Assert.IsTrue(expectedPost == actualPost, messagePost);
        }

        public static void AssertIsEqual(string testCase, IMovieCollection before, IMovieCollection after, IMovie? expected, IMovie? actual, int expectedPost, int actualPost, string message, string messagePost)
        {
            // TestLog(testCase, before.PrettyPrint(), after.PrettyPrint(), PrintMovieArray(before.ToArray()), PrintMovieArray(after.ToArray()), PrintMovie(expected), PrintMovie(actual), expectedPost.ToString(), actualPost.ToString());

            Assert.IsTrue(AreMoviesEqual(expected, actual), message);
            Assert.IsTrue(expectedPost == actualPost, messagePost);
        }

        public static void AssertIsEqual<T>(string testCase, IMovieCollection before, IMovieCollection after, T expected, T actual, int expectedPost, int actualPost, string message, string messagePost)
        {
            // TestLog(testCase, before.PrettyPrint(), after.PrettyPrint(), PrintMovieArray(before.ToArray()), PrintMovieArray(after.ToArray()), expected.ToString(), actual.ToString(), expectedPost.ToString(), actualPost.ToString());

            Assert.IsTrue(expected.Equals(actual), message);
            Assert.IsTrue(expectedPost == actualPost, messagePost);
        }

        public static void ClearLogFile()
        {
            if (!wasLogFileCleared)
            {
                using (File.Create(TEST_PATH)) ;
                FileStream fileStream = File.Open(TEST_PATH, FileMode.Open);

                fileStream.SetLength(0);
                fileStream.Close();

                wasLogFileCleared = true;
            }

        }

        public static void  TestLog(string testCase, string input, string expected, string actual)
        {
            Console.WriteLine($"{testID} {(new System.Diagnostics.StackTrace())?.GetFrame(2)?.GetMethod()?.Name}");
            Console.WriteLine($"Test case: {testCase}");
            Console.WriteLine($"Input    : {input}");
            Console.WriteLine($"Expected : {expected}");
            Console.WriteLine($"Actual   : {actual}");
            Console.WriteLine();

            ClearLogFile();
            using StreamWriter writetext = new(TEST_PATH, true);
            writetext.WriteLine($"{testID} {(new System.Diagnostics.StackTrace())?.GetFrame(2)?.GetMethod()?.Name}");
            writetext.WriteLine($"Test case: {testCase}");
            writetext.WriteLine($"Input    : {input}");
            writetext.WriteLine($"Expected : {expected}");
            writetext.WriteLine($"Actual   : {actual}");
            writetext.WriteLine();

            testID++;
        }

        public static void  TestLog(string testCase, string beforeTree, string afterTree, string before, string after, string expected, string actual, string expectedPost, string actualPost)
        {
            Console.WriteLine($"{testID} {(new System.Diagnostics.StackTrace())?.GetFrame(2)?.GetMethod()?.Name}");
            Console.WriteLine($"Test case    : {testCase}");
            Console.WriteLine($"Before       : {before}\n{beforeTree}");
            Console.WriteLine($"After        : {after}\n{afterTree}");
            Console.WriteLine($"Expected     : {expected}");
            Console.WriteLine($"Actual       : {actual}");
            Console.WriteLine($"Expected Post: {expectedPost}");
            Console.WriteLine($"Actual Post  : {actualPost}");
            Console.WriteLine();

            ClearLogFile();
            using StreamWriter writetext = new(TEST_PATH, true);
            writetext.WriteLine($"{testID} {(new System.Diagnostics.StackTrace())?.GetFrame(2)?.GetMethod()?.Name}");
            writetext.WriteLine($"Test case    : {testCase}");
            writetext.WriteLine($"Before       : {before}\n{beforeTree}");
            writetext.WriteLine($"After        : {after}\n{afterTree}");
            writetext.WriteLine($"Expected     : {expected}");
            writetext.WriteLine($"Actual       : {actual}");
            writetext.WriteLine($"Expected Post: {expectedPost}");
            writetext.WriteLine($"Actual Post  : {actualPost}");
            writetext.WriteLine();

            testID++;
        }

        public static void  TestLog(string testCase, string inputTree, string input, string expected, string actual, string expectedPost, string actualPost)
        {
            Console.WriteLine($"{testID} {(new System.Diagnostics.StackTrace())?.GetFrame(2)?.GetMethod()?.Name}");
            Console.WriteLine($"Test case    : {testCase}");
            Console.WriteLine($"Input        : {input}\n{inputTree}");
            Console.WriteLine($"Expected     : {expected}");
            Console.WriteLine($"Actual       : {actual}");
            Console.WriteLine($"Expected Post: {expectedPost}");
            Console.WriteLine($"Actual Post  : {actualPost}");
            Console.WriteLine();

            ClearLogFile();
            using StreamWriter writetext = new(TEST_PATH, true);
            writetext.WriteLine($"{testID} {(new System.Diagnostics.StackTrace())?.GetFrame(2)?.GetMethod()?.Name}");
            writetext.WriteLine($"Test case    : {testCase}");
            writetext.WriteLine($"Input        : {input}\n{inputTree}");
            writetext.WriteLine($"Expected     : {expected}");
            writetext.WriteLine($"Actual       : {actual}");
            writetext.WriteLine($"Expected Post: {expectedPost}");
            writetext.WriteLine($"Actual Post  : {actualPost}");
            writetext.WriteLine();

            testID++;
        }
    }
}
