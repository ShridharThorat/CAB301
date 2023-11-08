namespace Assignment_1
{


    /// This ADT represents a computing job
    ///
    /// 
    /// Invariants: 1<= Id <= 999 and TimeReceived > 0 and ExecutiinTime > 0 and 1<= Priority <= 9 
    /// 
    /// </summary>
    /// 

    public interface IJob
    {

        /// <summary>
        /// Get the id of this job.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns the id of this job.
        /// 
        /// </summary>
        public uint Id { get; }


        /// <summary>
        /// Get the received time of this job.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns the received time of this job.
        /// 
        /// </summary>
        public uint TimeReceived { get; }


        /// <summary>
        /// Get the execution time of this job.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns the execution time of this job.
        /// </summary>
        public uint ExecutionTime { get; }



        /// <summary>
        /// Get the priority of this job.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns the priority of this job.
        /// </summary>
        public uint Priority { get; }


        /// <summary>
        /// Determines whether a supplied job id is valid.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns true if 1<= id <=999; return false, otherwise.
        /// 
        /// Interpretation: In all circumstances, the method returns true if the supplied
        /// job id is between 001 and 999; return false otherwise.
        /// </summary>
        public static bool IsValidId(uint id)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Determines whether a supplied job execution time is valid.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns true if executiontime > 0; return false, otherwise.
        /// 
        /// Interpretation: In all circumstances, the method returns true if the supplied
        /// execution time is greater than 0; return false otherwise.
        /// </summary>
        public static bool IsValidExecutionTime(uint executiontime)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Determines whether a supplied job priority is valid.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns true if 1 <= priority <= 9; returns false otherwise.
        /// 
        /// Interpretation: In all circumstances, the method returns true if the supplied
        /// job priority is between 1 and 9, inclusive; return false otherwise.
        /// </summary>
        public static bool IsValidPriority(uint priority)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Determines whether a supplied job priority is valid.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns true if time > 0; retuns false otherwise. 
        /// 
        /// Interpretation: In all circumstances, the method returns true if the supplied
        /// time is greater than 0; returns false otherwise.
        /// </summary>
        public static bool IsTimeReceived(uint time)
        {
            throw new System.NotImplementedException();
        }
    }

}