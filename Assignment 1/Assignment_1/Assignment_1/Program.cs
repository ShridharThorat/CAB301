namespace Assignment_1
{
    class Program
    {
        private static void initialiseJobParam(ref uint id, ref int timeRec, ref uint execTime, ref uint p)
        {
            id = 1;
            timeRec = 0;
            execTime = 1;
            p = 1;
        }
        static void Main()
        {
            // Create Jobs
            IJob a = new Job(123, 1, 60, 2);
            IJob b = new Job(1, 101, 10, 9);
            IJob c = new Job(7, 20, 1, 6);
            IJob d = new Job(12, 40, 15, 7);
            IJob e = new Job(40, 18, 20, 8);
            IJob f = new Job(900, 2, 30, 2);
            IJob g = new Job(90, 2, 30, 2);

            //IJob a = new Job(1, 10, 1, 2);
            //IJob b = new Job(3, 10, 100, 2);
            //IJob c = new Job(2, 10, 20, 2);
            //IJob d = new Job(4, 10, 3, 2);
            //IJob e = new Job(5, 10, 7, 2);
            //IJob f = new Job(6, 10, 999, 2);

            // Create Job Collections
            JobCollection coll1 = new JobCollection(6); // Full array
            IJobCollection coll2 = new JobCollection(10); // 1 free
            IJobCollection coll3 = new JobCollection(7); // 4 free

            coll1.Add(a); coll1.Add(b); coll1.Add(c); coll1.Add(d); coll1.Add(e); coll1.Add(f);
            //Console.WriteLine("Add" + coll1.Add(a));            

            //coll2.Add(a); coll2.Add(b); coll2.Add(c); coll2.Add(d); coll2.Add(e);
            //coll3.Add(a); coll3.Add(b); coll3.Add(c);
            //Console.Write(coll1.Contains(g.Id)+" " + coll1.Capacity);

            foreach (IJob job in coll1.ToArray())
            {
                if (job != null) Console.WriteLine(job.ToString());
                else Console.WriteLine($"null, ");
            }
 

            //Console.WriteLine("\n");
            //coll1.Remove(7);
            //foreach (IJob job in coll1.ToArray())
            //{
            //    if (job != null) Console.WriteLine(job.ToString());
            //    else Console.Write($"{null}, ");

            //}


            //IScheduler schedule1 = new Scheduler(coll1);
            //IScheduler schedule2 = new Scheduler(coll2);

            //Print(coll1);
            //PrintSelection(schedule1, "FirstComeFirstServed");
            //PrintSelection(schedule1, "ShortestJobFirst");
            //PrintSelection(schedule1, "Priority");
            //Console.WriteLine("\n");

            ////Print(coll2);
            //PrintSelection(schedule2, "FirstComeFirstServed");
            //PrintSelection(schedule2, "ShortestJobFirst");
            //PrintSelection(schedule2, "Priority");
            //Console.WriteLine("\n");
        }

        private static void Print(IJobCollection coll) {
            //Console.Write($"Initial              ");
            //Console.Write("|");
            foreach (IJob job in coll.ToArray())
            {
                if (job != null) Console.WriteLine($"{job.ToString()}, ");
                else Console.WriteLine($"{null}, ");

            }
            //Console.WriteLine();
        }

        private static void PrintSelection(IScheduler scheduledCollectionOfJobs, String scheduleType)
        {
            //IJobCollection? temp = scheduledCollectionOfJobs.Jobs;
            //uint temp_size = temp.Capacity;
            //IJob?[] FCFS = new IJob?[temp_size];
            //FCFS = scheduledCollectionOfJobs.FirstComeFirstServed();
            //if (FCFS[0] == null) { Console.WriteLine("null input"); return; }
            switch (scheduleType)
            {
                case "FirstComeFirstServed":
                    //Console.WriteLine(scheduledCollectionOfJobs.FirstComeFirstServed().GetType());
                    Console.Write($"{scheduleType} ");
                    Console.Write("|");                    
                    foreach (IJob job in scheduledCollectionOfJobs.FirstComeFirstServed())
                    {
                        if (job != null) Console.Write($"{job.TimeReceived}, ");
                        else Console.Write($"{null}, ");
                    }
                    Console.WriteLine();
                    break;

                case "ShortestJobFirst":
                    Console.Write($"{scheduleType}     ");
                    Console.Write("|");
                    foreach (IJob job in scheduledCollectionOfJobs.ShortestJobFirst())
                    {
                        if (job != null) Console.Write($"{job.ExecutionTime}, ");
                        else Console.Write($"{null}, ");
                    }
                    Console.WriteLine();
                    break;


                case "Priority":
                    Console.Write($"{scheduleType}             ");
                    Console.Write("|");
                    foreach (IJob job in scheduledCollectionOfJobs.Priority())
                    {
                        if (job != null) Console.Write($"{job.Priority}, ");
                        else Console.Write($"{null}, ");
                    }
                    Console.WriteLine();
                    break;


                default:
                    Console.WriteLine($"{scheduleType} is an invalid type of algorithm. " +
                        $"\nChoose between 'FirstComeFirstServed', 'ShortestJobFirst' and 'Priority'\n$");
                    break;
            }
        }

    }
}