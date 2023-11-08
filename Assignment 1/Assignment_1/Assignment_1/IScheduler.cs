namespace Assignment_1
{

    /// <summary>
    /// This ADT schedules jobs in an associated job collection.
    /// </summary>
    public interface IScheduler
    {


        /// <summary>
        /// Gets the collection of jobs in this scheduler.
        /// </summary>
        public IJobCollection Jobs { get; }


        /// <summary>
        /// Sort the jobs in non-descending order of execution time.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns an array which contains all the jobs in this scheduler and the jobs in the array are sorted 
        /// in non-descending order of execution time 
        /// 
        /// Interpretation: In all circumstances, returns an array containing 
        /// all of jobs present in this scheduler, the elements of which are 
        /// sorted in non-descending order of execution time.
        /// </summary>

        public IJob[] ShortestJobFirst();


        /// <summary>
        /// Sort the jobs in non-ascending order of priority.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns an array which contains all the jobs in this scheduler and the jobs in the array are sorted 
        /// in non-ascending order of the job priority 
        /// 
        /// Interpretation: In all circumstances, returns an array containing 
        /// all of jobs present in this scheduler, the elements of which are 
        /// sorted in non-ascending order of the job priority.
        /// </summary>
        public IJob[] Priority();



        /// <summary>
        /// Sort the jobs in non-descending order of receipt time.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns an array which contains all the jobs in this scheduler and they are sorted in non-descending order of the time received of the job
        /// 
        /// Interpretation: In all circumstances, returns an  array containing 
        /// all of jobs present in this scheduler, the elements of which are 
        /// sorted in non-descending order of receipt time.
        /// </summary>

        public IJob[] FirstComeFirstServed();
    }

}