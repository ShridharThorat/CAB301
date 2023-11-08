using System;
using Assignment_1;
namespace ADT_Tests
{
    public class TestHelper
    {
        public static uint?[] ComparisonArray(uint capacity, IJob[] jobs, String scheduleType)
        {
            uint?[] jobParamArr = new uint?[capacity];
            switch (scheduleType)
            {
                case "FCFS":
                    for (int i = 0; i < capacity; i++)
                    {
                        if (jobs[i] != null) jobParamArr[i] = jobs[i].TimeReceived;
                        else { jobParamArr[i] = null; }
                    }
                    break;
                case "SJF":
                    for (int i = 0; i < capacity; i++)
                    {
                        if (jobs[i] != null) jobParamArr[i] = jobs[i].ExecutionTime;
                        else { jobParamArr[i] = null; }
                    }
                    break;
                case "Priority":
                    for (int i = 0; i < capacity; i++)
                    {
                        if (jobs[i] != null) jobParamArr[i] = jobs[i].Priority;
                        else { jobParamArr[i] = null; }
                    }
                    break;
                default:
                    throw new Exception("Invalid schedule type.\nChoose between 'FCFS', 'SJF' and 'Priority'.");
            }
            return jobParamArr;
        }


    }
}

