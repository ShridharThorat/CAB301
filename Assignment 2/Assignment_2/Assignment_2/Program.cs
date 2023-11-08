//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics.Metrics;
//using System.Drawing;
//using System.Xml.Linq;

//namespace Assignment_2
//{
//    public class Program
//    {

//        public const string alp = "abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+~`{}[]|;'<>?,./";
//        public static void Main()
//        {
//            // IMovie movieA = new Movie("A"      ,MovieGenre.Action, MovieClassification.M, 100, 2);
//            // IMovie movieB = new Movie("B"        ,MovieGenre.Action, MovieClassification.M, 100, 2);
//            // IMovie movieC = new Movie("C"         ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieD = new Movie("D"         ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieE = new Movie("E"      ,MovieGenre.Action, MovieClassification.M, 100, 2);
//            // IMovie movieF = new Movie("F"          ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieG = new Movie("G"        ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieH = new Movie("H"          ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieI = new Movie("I"    ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieJ = new Movie("J"            ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieK = new Movie("K"       ,MovieGenre.Action, MovieClassification.M ,100, 2);
//            // IMovie movieL = new Movie("L"  ,MovieGenre.Action, MovieClassification.M ,100, 2);


//            MovieCollection coll1 = new MovieCollection();

//            //coll1.Insert(movieE);
//            //coll1.Insert(movieI);
//            //coll1.Insert(movieC);
//            //coll1.Insert(movieB);
//            //coll1.Insert(movieK);
//            //coll1.Insert(movieL);
//            //coll1.Insert(movieJ);
//            //coll1.Insert(movieH);
//            //coll1.Insert(movieF);
//            //coll1.Insert(movieA);
//            //coll1.Insert(movieG); // og J before
//            //coll1.Insert(movieD);

//            //Console.WriteLine("E vs I " + movieE.CompareTo(movieI));
//            //Console.WriteLine("E vs C " + movieE.CompareTo(movieC));
//            //Console.WriteLine($"{mov1.Title} vs {mov2.Title} is {mov1.CompareTo(mov2)}");
//            //Console.WriteLine($"{mov2.Title} vs {mov1.Title} is {mov2.CompareTo(mov1)}");
//            //Console.WriteLine($"{mov1.Title} vs {mov1.Title} is {mov1.CompareTo(mov1)}");


//            //coll1.Insert(movieB);
//            //coll1.Insert(movieA);
//            //coll1.Insert(movieI);
//            //coll1.Insert(movieD);
//            //coll1.Insert(movieC);
//            //coll1.Insert(movieE);
//            //coll1.Insert(movieH);
//            //coll1.Insert(movieG); // og J before
//            //coll1.Insert(movieF);
//            //coll1.Insert(movieK);
//            //coll1.Insert(movieJ);

//            //coll1.PrettyPrint();
//            //coll1.Delete(movieB);
//            //coll1.PrettyPrint();
//            //coll1.Delete(movieI);
//            //coll1.PrettyPrint();
//            //coll1.Delete(movieD);
//            //coll1.PrettyPrint();
//            //coll1.Delete(movieE);
//            //coll1.PrettyPrint();
//            //coll1.Delete(movieJ);




//            //// Printing 
//            //Console.WriteLine("Binary tree Visual");
//            //BTreeNode.PrintTreeWithSymbols(coll1.Root, null);
//            //coll1.PrettyPrint();
//            //Console.WriteLine(coll1.NoDVDs());

//            //Console.WriteLine();
//            //coll1.Delete(movieE);
//            //BTreeNode.PrintTreeWithSymbols(coll1.Root, null);

//            //Console.WriteLine();
//            //coll1.Clear();
//            //BTreeNode.PrintTreeWithSymbols(coll1.Root, null);

//            //IMovie[] array = coll1.ToArray();
//            //foreach (IMovie movie in array) Console.WriteLine(movie.Title);


//            // Count test
//            //coll1.Clear();
//            //coll1 = CollectionDesign(10, 1, coll1);
//            //Console.WriteLine("Length: "+coll1.Number);
//            //int count = 0;
//            //coll1.NoDVDs(ref count);
//            //Console.WriteLine($"Count:  {count}");

//            // 10,  20,   100,  200,   400,  1000,  8000,   10000
//            // 53,  103,  503,  1003, 2003,  5003, 40003,  50003

//            // t(2n)/t(n)    = c(200)/c(100)   = 1003/503    = 1.99

//            // Consider that there is a +3 for any input size n
//            // 10,  20,   100,  2000,    1000,   10000
//            // 50,  100,  500,  1000,    5000,   50000 (+3)

//            // t(2n)/t(n)   = c(2000)/c(1000)  = 1000/500 + 3/3 = 2 + 1

//            // Comparing to theoretical values
//            // c(2n)/c(n) = 2n + 3/n+3
//            // c(2*100)/c(100) = 5*2*100 + 3/ 5*100 + 3 = 1000+3/500+3 = 1003/503

//            // The ratio between t(2n) and t(n) is about 1.99, i.e, there is approximately
//            // a 2 fold increase in the amount of operations with a 2 fold increase in input size.
//            // Compared to the theoretical complexity, we can see that the empirical results are
//            // actually greater. The emperical results follow the formula C(n) = 5n + 3
//            // rather than just C(n) = n+3. It is worth mentioning that since
//            // the ratio between t(2n) and t(n) is slightly less than 2,
//            // it is slightly more efficient than O(n).

//            //Time test
//            int startAmount = 625;
//            int maxMultiply = 2;
//            int amount = 10;
//            int sampleSize = 1000;

//            var total = System.Diagnostics.Stopwatch.StartNew();
//            NumerousRuns(startAmount, maxMultiply, amount, sampleSize);
//            total.Stop();
//            Console.WriteLine($"\nTotal time: {total.Elapsed.TotalSeconds}s");

//        }

//        public static MovieCollection CollectionDesign(double amount, MovieCollection collection)
//        {
//            var total = System.Diagnostics.Stopwatch.StartNew();
//            for (int i = 0; i < amount; i++)
//            {
//                IMovie mov = new Movie($"A{i}", MovieGenre.Action, MovieClassification.M, 100, 2);

//                collection.Insert(mov);
//            }
//            total.Stop();
//            Console.WriteLine($"\n{amount} movies took {total.Elapsed.TotalMilliseconds} ms to insert");
//            return collection;
//        }

//        private static void NumerousRuns(double start, double maxMultiply, int amount, int sample)
//        {
//            double max = start * Math.Pow(maxMultiply, amount);
//            Console.WriteLine($"Average ms from {start} to {max} movies with {sample} samples\n");

//            //string file = "/Users/shridhar/Library/CloudStorage/OneDrive-QueenslandUniversityofTechnology/Bachelor of Mathematics and Information Technology/Year 4/CAB301 Algorithms/Assignment 2/Assignment_2/Assignment_2/data.txt";
//            //File.WriteAllText(file, string.Empty);
//            //using (StreamWriter writer = new StreamWriter(file))
//            //{
//            for (double i = 0; i <= amount; i++)
//            {
//                double size = start * Math.Pow(maxMultiply, i);
//                MovieCollection coll1 = new MovieCollection();
//                coll1 = CollectionDesign(size, coll1);
//                //MovieCollection coll1 = GenerateTree(sample);
//                SingleRun(coll1, sample, size); //file, writer);
//            }
//            //}
//        }

//        private static void SingleRun(MovieCollection coll1, int sample, double size) //string file, StreamWriter writer)
//        {
//            var watch = System.Diagnostics.Stopwatch.StartNew();
//            watch.Stop();
//            double[] iterations = new double[sample];
//            int noDvd = 0;
//            //writer.AutoFlush=true; writer.WriteLine(size);
//            for (int i = 0; i < sample; i++)
//            {
//                watch = System.Diagnostics.Stopwatch.StartNew();
//                noDvd = coll1.NoDVDs();
//                watch.Stop();
//                iterations[i] = watch.Elapsed.TotalMilliseconds;
//                //writer.WriteLine(watch.Elapsed.TotalMilliseconds);
//            }
//            //writer.WriteLine("\n");

//            double avg = Queryable.Average(iterations.AsQueryable());
//            Console.WriteLine($"Average time from {sample} samples: {avg.ToString("F3")} ms has {noDvd} dvds");
//        }

//        private static string[][] Order(string[][] array)
//        {
//            //// Using system stuff
//            //string[][] sortedMovies = new string[array.Length][];
//            //Array.Copy(array, 0, sortedMovies, 0, array.Length);

//            string[][] sortedMovies = Copy(array);
//            int n = sortedMovies.Length;
//            for (int i = 0; i <= n - 2; i++)
//            {
//                int min_index = i;
//                for (int j = i + 1; j <= n - 1; j++)
//                {

//                    if (sortedMovies[j][1].Length < sortedMovies[min_index][1].Length)
//                    {
//                        min_index = j; // index of the smallest item for iteration i
//                    }
//                }
//                (sortedMovies[i], sortedMovies[min_index]) = (sortedMovies[min_index], sortedMovies[i]);
//            }
//            return sortedMovies;
//        }

//        private static string[][] Copy(string[][] first)
//        {
//            string[][] second = new string[first.Length][];
//            for (int i = 0; i < first.Length; i++)
//            {
//                second[i] = new string[first[i].Length];
//                second[i][0] = first[i][0];
//                second[i][1] = first[i][1];
//                //Array.Copy(first[i], second[i], first[i].Length);
//            }
//            return second;
//        }



//    }


//}