using System;
using System.Drawing;

namespace Assignment_2
{

    public class EmpiricalAnalysis
    {
        public const string allASCIIChars = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        public static List<string> UniqueNameGenerator(double numberOfNodes)
        {
            Random random = new Random(25);
            HashSet<string> generatedStrings = new HashSet<string>();
            while (generatedStrings.Count < numberOfNodes)
            {
                string randomString = new string(Enumerable.Repeat(allASCIIChars, 20)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                if (!generatedStrings.Contains(randomString))
                {
                    generatedStrings.Add(randomString);
                }
            }
            return generatedStrings.ToList();
        }

        public static MovieCollection RandomCollectionDesign(double numberOfNodes)
        {
            // Get numberOfNodes string names
            List<string> randomNames = UniqueNameGenerator(numberOfNodes);
            MovieCollection collection = new MovieCollection();
            var total = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < randomNames.Count; i++)
            {
                IMovie mov = new Movie(randomNames[i], MovieGenre.Action, MovieClassification.M, 100, 1);
                collection.Insert(mov);
            }
            total.Stop();
            return collection;
        }

        public static MovieCollection IterativeCollectionDesign(double numberOfNodes)
        {
            MovieCollection collection = new MovieCollection();
            var total = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < numberOfNodes; i++)
            {
                if (i >= 0 && i <= 9)
                {
                    IMovie mov = new Movie($"0{i}", MovieGenre.Action, MovieClassification.M, 100, 1);
                    collection.Insert(mov);
                }
                else
                {
                    IMovie mov = new Movie($"{i}", MovieGenre.Action, MovieClassification.M, 100, 1);
                    collection.Insert(mov);
                }
            }
            total.Stop();
            //Console.WriteLine($"\n{collection.Number} movies took {total.Elapsed.TotalMilliseconds} ms to insert");
            return collection;
        }

        public static void IterativeCollection_NoDVDs_Test(double nodes, int sampleSize)
        {
            MovieCollection collection = IterativeCollectionDesign(nodes);
            double[] iterations = new double[sampleSize];           // and array with time for each test
            int noDvd = 0;
            var totalWatch = System.Diagnostics.Stopwatch.StartNew(); // Start watch
            for (int i = 0; i < sampleSize; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew(); // Start watch
                noDvd = collection.NoDVDs();
                watch.Stop();                                        // Stop watch
                iterations[i] = watch.Elapsed.TotalMilliseconds;     // Add the time taken to a list
                
            }
            totalWatch.Stop();

            double averageTime = Queryable.Average(iterations.AsQueryable());            
            string write = $"Average time for {collection.Number} nodes taking {sampleSize} samples: {averageTime.ToString("F3")}ms and has {noDvd} dvds: {sampleSize} iterations took {(totalWatch.Elapsed.TotalMinutes).ToString("F3")} seconds";
            Console.WriteLine(write);

        }

        public static void RandomCollection_NoDVDs_Test(double nodes, int sampleSize)
        {
            MovieCollection collection = new();
            double[] iterations = new double[sampleSize];           // and array with time for each test
            //collection = RandomCollectionDesign(nodes);
            int noDvd = 0; int size = 0;
            var totalWatch = System.Diagnostics.Stopwatch.StartNew(); // Start watch for total test time
            for (int i = 0; i < sampleSize; i++)
            {
                collection = RandomCollectionDesign(nodes);
                size = collection.Number;
                var NoDVDsWatch = System.Diagnostics.Stopwatch.StartNew(); // Start watch for NoDVDS
                noDvd = collection.NoDVDs();
                NoDVDsWatch.Stop();
                iterations[i] = NoDVDsWatch.Elapsed.TotalMilliseconds;     // Add the time taken to a list
                collection.Clear();
            }
            totalWatch.Stop();
            double averageTime = Queryable.Average(iterations.AsQueryable());
            string write = $"NoDVDs average time for {size} nodes taking {sampleSize} samples: {averageTime.ToString("F3")}ms. {sampleSize} iterations took {(totalWatch.Elapsed.TotalSeconds).ToString("F3")} seconds";
            Console.WriteLine(write);

        }
    }

    public class EmpiricalRun : EmpiricalAnalysis
    {

        public static void Main()
        {
            IMovie movie1 = new Movie("A");

            int sampleSize = 100; // the number of samples to average
            int amount = 110;       // amount of tests
            double start = 20000;  // the starting size of the colleciton

            RandomCollection_NoDVDs_Test(start, sampleSize);
            IterativeCollection_NoDVDs_Test(start, sampleSize);


            // Does amount number of tests for randomly designed collections
            //Console.WriteLine("Random");
            //double nodes = start;
            //for (int i = 1; i < amount; i++)
            //{
            //    RandomCollection_NoDVDs_Test(nodes, sampleSize);
            //    nodes = start + (10000 * i);
            //}

            // Does amount number of tests for iteratively designed collections
            //Console.WriteLine("Iterative");
            //nodes = start;
            //for (int i = 1; i <= amount; i++)
            //{
            //    IterativeCollection_NoDVDs_Test(nodes, sampleSize);
            //    nodes = start + (10000 * i);
            //}
        }
    }
}

