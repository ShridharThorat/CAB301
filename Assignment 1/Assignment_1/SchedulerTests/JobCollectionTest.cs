namespace ADT_Tests
{
    [TestClass]
    public class JobCollectionTest
    {
        const bool false_expectation = false;
        const bool true_expectation = true;

        IJob job1 = new Job(1, 400, 10, 2);
        IJob job2 = new Job(2, 20, 14, 5);
        IJob job3 = new Job(3, 101, 1, 2);
        IJob job4 = new Job(4, 40, 78, 4);
        IJob job5 = new Job(5, 18, 54, 3);
        IJob job6 = new Job(6, 2, 213, 8);
        const uint capacity = 5;
        IJobCollection coll = new JobCollection(capacity);

        [TestMethod]
        public void Add_to_empty_arrray()
        {
            Assert.IsTrue(coll.Add(job1) == true);
        }

        [TestMethod]
        public void Add_to_full_arrray()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            Assert.IsTrue(coll.Add(job6) == false);
        }

        [TestMethod]
        public void Add_duplicate_job()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4);
            Assert.IsTrue(coll.Add(job1) == false);
        }

        [TestMethod]
        public void Add_null_job()
        {
            // forces null input to not show warning
            Assert.IsTrue(coll.Add(null!) == false, $"expected false");
        }

        //[TestMethod]
        //public void Contains_collection_capacity_1()
        //{
        //    /*-----------------------------Arrange----------------------------*/
        //    uint capacity = 1;
        //    // Create unsorted JobCollection
        //    IJobCollection coll = new JobCollection(capacity); // Full array
        //    coll.Add(a);
        //    /*-----------------------------Act--------------------------------*/
        //    //bool actual = coll.Contains(a.Id);
        //    /*-----------------------------Assert-----------------------------*/
        //    Assert.IsTrue((coll.Contains(a.Id) == true), "Collection with capacity 1, fails Contain");
        //}

        [TestMethod]
        public void Contains_count_0()
        {
            bool actual = coll.Contains(job1.Id);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Contains_job_in_collection()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            bool actual = coll.Contains(job1.Id);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Contains_job_not_in_collection()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            bool actual = coll.Contains(job6.Id);
            Assert.IsFalse(actual);
        }


        [TestMethod]
        public void Find_job_not_contained()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            Assert.IsNull(coll.Find(job6.Id));
        }

        [TestMethod]
        public void Find_job_contained()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            Assert.IsTrue((coll.Find(job5.Id) == job5));
        }

        //[TestMethod]
        //public void Find_job_contains_single_job()
        //{
        //    /*-----------------------------Arrange----------------------------*/
        //    uint capacity = 1;
        //    IJobCollection coll = new JobCollection(capacity); // Full array
        //    coll.Add(a);

        //    IJob? expected = a;
        //    /*-----------------------------Act--------------------------------*/
        //    IJob? actual = coll.Find(a.Id);
        //    /*-----------------------------Assert-----------------------------*/
        //    Assert.AreSame(expected, actual, "Job is contained and returned");
        //}


        [TestMethod]
        public void Remove_unsuccessful()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            uint previousCount = coll.Count;
            Assert.IsTrue((coll.Remove(job6.Id) == false) && (coll.Count == previousCount));
        }

        [TestMethod]
        public void Remove_successful()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            uint previousCount = coll.Count;
            // Remove returns true, count decrements by one, and the removed job
            // can no longer be found
            Assert.IsTrue((coll.Remove(job3.Id) == true)
                && (coll.Count == previousCount - 1)
                && (coll.Find(job3.Id) == null));
        }

        [TestMethod]
        public void ToArray_successful()
        {
            coll.Add(job1); coll.Add(job2); coll.Add(job3); coll.Add(job4); coll.Add(job5);
            // We know that collection contains jobs in the order [job1, job2, job3, job4, job5]
            // As we cannot look at the jobs parameter, we need to forcefully create
            // an IJob[] array with these jobs
            IJob[] actual_Copy = new IJob[5]{job1,job2,job3,job4,job5};
            
            // IJob[] array from ToArray() method
            IJob[] coll_ToArray = coll.ToArray();

            bool ToArrayCorrect = Enumerable.SequenceEqual(actual_Copy, coll_ToArray);
            Assert.IsTrue(ToArrayCorrect);
        }

    }
}

