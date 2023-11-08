using System;

namespace ADT_Tests
{
    [TestClass]
    public class JobTest
    {
        const bool false_expectation = false;
        const bool true_expectation = true;

        [TestMethod]
        public void Id_outside_lower_bounds()
        {            
            uint id = 0;
            bool actual = Job.IsValidId(id);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Id_on_lower_bounds()
        {
            uint id = 1;
            bool actual = Job.IsValidId(id);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Id_on_upper_bounds()
        {
            uint id = 999;
            bool actual = Job.IsValidId(id);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Id_outside_upper_bounds()
        {
            uint id = 1000;
            bool actual = Job.IsValidId(id);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ExecutionTime_outside_lower_bounds()
        {
            uint executiontime = 1;
            bool actual = Job.IsValidExecutionTime(executiontime);
            Assert.IsTrue(actual, "Valid_ExecutionTime_min failed");
        }

        [TestMethod]
        public void ExecutionTime_on_lower_bounds()
        {
            uint executiontime = 0;
            bool actual = Job.IsValidExecutionTime(executiontime);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Priority_on_lower_bounds()
        {
            uint priority = 0;
            bool actual = Job.IsValidPriority(priority);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Priority_outside_lower_bounds()
        {
            uint priority = 1;
            bool actual = Job.IsValidPriority(priority);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Priority_on_upper_bounds()
        {
            uint priority = 9;
            bool actual = Job.IsValidPriority(priority);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Priority_outside_upper_bounds()
        {             
            uint priority = 10;
            bool actual = Job.IsValidPriority(priority);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReceivedTime_outside_lower_bounds()
        {
            uint timeReceived = 0;
            bool actual = Job.IsTimeReceived(timeReceived);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ReceivedTime_on_lower_bounds()
        {
            uint timeReceived = 1;
            bool actual = Job.IsTimeReceived(timeReceived);
            Assert.IsTrue(actual);
        }
    }
}

