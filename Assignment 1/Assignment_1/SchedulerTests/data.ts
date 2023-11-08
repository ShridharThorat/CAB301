private IJob job1 = new Job(1, 525, 1, 1);
        private IJob job2 = new Job(2, 412, 1, 1);
        private IJob job3 = new Job(3, 823, 1, 1);
        private IJob job4 = new Job(4, 789, 1, 1);
        private IJob job5 = new Job(5, 431, 1, 1);
        private IJob job6 = new Job(6, 392, 1, 1);
        private IJob job7 = new Job(7, 948, 1, 1);
        private IJob job8 = new Job(8, 431, 1, 5); // same timeReceived as job5
        private IJob job9 = new Job(9, 525, 1, 1);  // same timeReceived as job1



        private IJob job1 = new Job(1, 1, 52, 1);
        private IJob job2 = new Job(2, 1, 41, 1);
        private IJob job3 = new Job(3, 1, 82, 1);
        private IJob job4 = new Job(4, 1, 78, 1);
        private IJob job5 = new Job(5, 1, 43, 1);
        private IJob job6 = new Job(6, 1, 39, 1);
        private IJob job7 = new Job(7, 1, 94, 1);
        private IJob job8 = new Job(8, 1, 43, 5); // same executionTime as job5
        private IJob job9 = new Job(9, 1, 52, 1);  // same executionTime as job1
        private uint capacity = 7;


        private IJob job1 = new Job(1, 1, 52, 5);
        private IJob job2 = new Job(2, 1, 41, 7);
        private IJob job3 = new Job(3, 1, 82, 2);
        private IJob job4 = new Job(4, 1, 78, 3);
        private IJob job5 = new Job(5, 1, 43, 5);
        private IJob job6 = new Job(6, 1, 39, 9);
        private IJob job7 = new Job(7, 1, 94, 1);
        private IJob job8 = new Job(8, 1, 43, 5); // same priority as job5
        private IJob job9 = new Job(9, 1, 52, 4);  // same priority as job1



        //[TestMethod]
        //public void empty_collection()
        //{
        //    /*---------------------------Test Data----------------------------*/
        //    IJobCollection jobCollection = new JobCollection(capacity);
        //    IScheduler schedule = new Scheduler(jobCollection);
        //    IJob[] expected = new IJob[schedule.Jobs.Capacity];

        //    Console.WriteLine("Input Data");
        //    foreach (IJob job in jobCollection.ToArray()) Console.WriteLine(job);
        //    Console.WriteLine();

        //    Console.WriteLine("Expected Data");
        //    foreach (IJob job in expected) Console.WriteLine(job);
        //    Console.WriteLine();

        //    /*-------------------------Test Results---------------------------*/
        //    IJob[] actual = schedule.ShortestJobFirst();

        //    Console.WriteLine("Actual Results");
        //    foreach (IJob job in actual) Console.WriteLine(job);
        //    Console.WriteLine();
        //    /*-----------------------------Assert-----------------------------*/
        //    bool assertion = Enumerable.SequenceEqual(expected, actual);
        //    Assert.IsTrue(assertion, " null_Schedule_Jobs failed");
        //}



[TestMethod]
        public void empty_collection()
{
    /*---------------------------Test Data----------------------------*/
    IJobCollection jobCollection = new JobCollection(capacity);
    IScheduler schedule = new Scheduler(jobCollection);
    IJob[] expected = new IJob[schedule.Jobs.Capacity];

    Console.WriteLine("Input Data");
    foreach(IJob job in jobCollection.ToArray()) Console.WriteLine(job);
    Console.WriteLine();

    Console.WriteLine("Expected Data");
    foreach(IJob job in expected) Console.WriteLine(job);
    Console.WriteLine();

    /*-------------------------Test Results---------------------------*/
    IJob[] actual = schedule.Priority();

    Console.WriteLine("Actual Results");
    foreach(IJob job in actual) Console.WriteLine(job);
    Console.WriteLine();
    /*-----------------------------Assert-----------------------------*/
    bool assertion = Enumerable.SequenceEqual(expected, actual);
    Assert.IsTrue(assertion, " null_Schedule_Jobs failed");
}



[TestMethod]
        public void empty_collection()
{
    /*---------------------------Test Data----------------------------*/
    IJobCollection jobCollection = new JobCollection(capacity);
    IScheduler schedule = new Scheduler(jobCollection);
    IJob[] expected = new IJob[schedule.Jobs.Capacity];

    Console.WriteLine("Input Data");
    foreach(IJob job in jobCollection.ToArray()) Console.WriteLine(job);
    Console.WriteLine();

    Console.WriteLine("Expected Data");
    foreach(IJob job in expected) Console.WriteLine(job);
    Console.WriteLine();

    /*-------------------------Test Results---------------------------*/
    IJob[] actual = schedule.FirstComeFirstServed();

    Console.WriteLine("Actual Results");
    foreach(IJob job in actual) Console.WriteLine(job);
    Console.WriteLine();
    /*-----------------------------Assert-----------------------------*/
    bool assertion = Enumerable.SequenceEqual(expected, actual);
    Assert.IsTrue(assertion, " null_Schedule_Jobs failed");
}
