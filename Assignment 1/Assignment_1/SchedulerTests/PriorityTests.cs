namespace ADT_Tests
{
    [TestClass]
    public class PriorityTests : ITests
    {
        private IJob job1 = new Job(1, 1, 52, 5);
        private IJob job2 = new Job(2, 1, 41, 7);
        private IJob job3 = new Job(3, 1, 82, 2);
        private IJob job4 = new Job(4, 1, 78, 3);
        private IJob job5 = new Job(5, 1, 43, 5);
        private IJob job6 = new Job(6, 1, 39, 9);
        private IJob job7 = new Job(7, 1, 94, 1);
        private IJob job8 = new Job(8, 1, 43, 5); // same priority as job5
        private IJob job9 = new Job(9, 1, 52, 4);  // same priority as job1
        private uint capacity = 7;
        // Order is below. Whatever parameter values you pick,
        // these should be in the correct order
        // jobs order: 6 2 5 1 4 3 7
        //private IJob job6;
        //private IJob job2;
        //private IJob job5;
        //private IJob job8; // same timeReceived as job5
        //private IJob job1;
        //private IJob job9;  // same timeReceived as job1   
        //private IJob job4;
        //private IJob job3;
        //private IJob job7;

        [TestMethod]
        public void unique_jobs_in_a_full_collection()
        {
            /*---------------------------Test Data----------------------------*/
            // Create unsorted JobCollection
            IJobCollection coll = new JobCollection(capacity); // Full array
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4);
            coll.Add(job5); coll.Add(job6); coll.Add(job7);

            Console.WriteLine("Input Data");
            foreach (IJob job in coll.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Create schedule for the JobCollection
            IScheduler schedule = new Scheduler(coll);

            // Expected Result -> to be compared to actual results
            IJobCollection coll_ex = new JobCollection(capacity);
            coll_ex.Add(job6); coll_ex.Add(job2); coll_ex.Add(job5);
            coll_ex.Add(job1); coll_ex.Add(job4); coll_ex.Add(job3); coll_ex.Add(job7);
            IJob[] IJob_expected = coll_ex.ToArray();

            Console.WriteLine("Expected results");
            foreach (IJob job in coll_ex.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Expected sorted sequence of TimeReceived
            uint?[] expected = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_expected[i] != null) expected[i] = IJob_expected[i].TimeReceived;
                else { expected[i] = null; }
            }
            /*-------------------------Test Results---------------------------*/
            IJob[] IJob_actual = schedule.Priority();

            // Actual sorted sequence of TimeReceived
            uint?[] actual = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_actual[i] != null) actual[i] = IJob_actual[i].TimeReceived;
                else { actual[i] = null; }
            }

            Console.WriteLine("Actual Results");
            foreach (IJob job in IJob_actual) Console.WriteLine(job);
            Console.WriteLine();

            /*-----------------------------Assert-----------------------------*/
            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void unique_jobs_in_a_partial_collection()
        {
            /*---------------------------Test Data----------------------------*/
            // Doesn't contain job6 and 7 (count = 5, capacity = 7)
            IJobCollection coll = new JobCollection(capacity);
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4);
            coll.Add(job5);

            Console.WriteLine("Input Data");
            foreach (IJob job in coll.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Create schedule for the JobCollection
            IScheduler schedule = new Scheduler(coll);

            // Expected Result -> to be compared to actual results
            IJobCollection coll_ex = new JobCollection(capacity);
            coll_ex.Add(job2); coll_ex.Add(job5);
            coll_ex.Add(job1); coll_ex.Add(job4); coll_ex.Add(job3);
            IJob[] IJob_expected = coll_ex.ToArray();

            Console.WriteLine("Expected results");
            foreach (IJob job in coll_ex.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Expected sorted sequence of TimeReceived
            uint?[] expected = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_expected[i] != null) expected[i] = IJob_expected[i].TimeReceived;
                else { expected[i] = null; }
            }
            /*-------------------------Test Results---------------------------*/
            IJob[] IJob_actual = schedule.Priority();

            // Actual sorted sequence of TimeReceived
            uint?[] actual = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_actual[i] != null) actual[i] = IJob_actual[i].TimeReceived;
                else { actual[i] = null; }
            }

            Console.WriteLine("Actual Results");
            foreach (IJob job in IJob_actual) Console.WriteLine(job);
            Console.WriteLine();

            /*-----------------------------Assert-----------------------------*/
            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void common_jobs_in_a_full_collection()
        {
            /*---------------------------Test Data----------------------------*/
            //IJob job8 = new Job(8, 431, 1, 5); // same timeReceived as job5
            //IJob job9 = new Job(9, 525, 1, 1);  // same timeReceived as job1            

            // Create unsorted JobCollection without job2 and job7
            IJobCollection coll = new JobCollection(capacity);
            coll.Add(job1); coll.Add(job8); coll.Add(job3); coll.Add(job4);
            coll.Add(job5); coll.Add(job6); coll.Add(job9);
            // Create schedule for the JobCollection
            IScheduler schedule = new Scheduler(coll);

            Console.WriteLine("Input Data");
            foreach (IJob job in coll.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Expected Result -> to be compared to actual results
            IJobCollection coll_ex = new JobCollection(capacity);
            coll_ex.Add(job6); coll_ex.Add(job8); coll_ex.Add(job5);
            coll_ex.Add(job1); coll_ex.Add(job9); coll_ex.Add(job4); coll_ex.Add(job3);
            IJob[] IJob_expected = coll_ex.ToArray();

            Console.WriteLine("Expected results");
            foreach (IJob job in coll_ex.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Expected sorted sequence of TimeReceived
            uint?[] expected = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_expected[i] != null) expected[i] = IJob_expected[i].TimeReceived;
                else { expected[i] = null; }
            }
            /*-------------------------Test Results---------------------------*/
            IJob[] IJob_actual = schedule.Priority();

            // Actual sorted sequence of TimeReceived
            uint?[] actual = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_actual[i] != null) actual[i] = IJob_actual[i].TimeReceived;
                else { actual[i] = null; }
            }

            Console.WriteLine("Actual Results");
            foreach (IJob job in IJob_actual) Console.WriteLine(job);
            Console.WriteLine();

            /*-----------------------------Assert-----------------------------*/
            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void common_jobs_in_a_partial_collection()
        {
            /*---------------------------Test Data----------------------------*/
            // Doesn't contain job6 and 3 (count = 5, capacity = 7)
            //IJob job8 = new Job(8, 431, 1, 5); // same timeReceived as job5
            //IJob job9 = new Job(9, 525, 1, 1);  // same timeReceived as job1

            // Create unsorted JobCollection without job2 and job7
            IJobCollection coll = new JobCollection(capacity);
            coll.Add(job1); coll.Add(job8); coll.Add(job4);
            coll.Add(job5); coll.Add(job9);
            // Create schedule for the JobCollection
            IScheduler schedule = new Scheduler(coll);

            Console.WriteLine("Input Data");
            foreach (IJob job in coll.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Expected Result -> to be compared to actual results
            IJobCollection coll_ex = new JobCollection(capacity);
            coll_ex.Add(job8); coll_ex.Add(job5);
            coll_ex.Add(job1); coll_ex.Add(job9); coll_ex.Add(job4);
            IJob[] IJob_expected = coll_ex.ToArray();

            Console.WriteLine("Expected results");
            foreach (IJob job in IJob_expected) Console.WriteLine(job);
            Console.WriteLine();

            // Expected sorted sequence of TimeReceived
            uint?[] expected = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_expected[i] != null) expected[i] = IJob_expected[i].TimeReceived;
                else { expected[i] = null; }
            }
            /*-------------------------Test Results---------------------------*/
            IJob[] IJob_actual = schedule.Priority();

            // Actual sorted sequence of TimeReceived
            uint?[] actual = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_actual[i] != null) actual[i] = IJob_actual[i].TimeReceived;
                else { actual[i] = null; }
            }

            Console.WriteLine("Actual Results");
            foreach (IJob job in IJob_actual) Console.WriteLine(job);
            Console.WriteLine();

            /*-----------------------------Assert-----------------------------*/
            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void collection_capacity_one()
        {
            /*---------------------------Test Data----------------------------*/
            IJobCollection coll = new JobCollection(capacity); coll.Add(job1);
            IScheduler schedule = new Scheduler(coll);

            Console.WriteLine("Input Data");
            foreach (IJob job in coll.ToArray()) Console.WriteLine(job);
            Console.WriteLine();

            // Expected Result -> to be compared to actual results
            IJobCollection coll_ex = new JobCollection(capacity); coll_ex.Add(job1);
            IJob[] IJob_expected = coll_ex.ToArray();

            Console.WriteLine("Expected results");
            foreach (IJob job in IJob_expected) Console.WriteLine(job);
            Console.WriteLine();

            // Expected sorted sequence of TimeReceived
            uint?[] expected = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_expected[i] != null) expected[i] = IJob_expected[i].TimeReceived;
                else { expected[i] = null; }
            }
            /*-------------------------Test Results---------------------------*/
            IJob[] IJob_actual = schedule.Priority();

            // Actual sorted sequence of TimeReceived
            uint?[] actual = new uint?[capacity];
            for (int i = 0; i < capacity; i++)
            {
                if (IJob_actual[i] != null) actual[i] = IJob_actual[i].TimeReceived;
                else { actual[i] = null; }
            }

            Console.WriteLine("Actual Results");
            foreach (IJob job in IJob_actual) Console.WriteLine(job);
            Console.WriteLine();

            /*-----------------------------Assert-----------------------------*/
            bool assertion = Enumerable.SequenceEqual(expected, actual);
            Assert.IsTrue(assertion);
        }
    }
}