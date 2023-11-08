namespace Assignment_1
{

    /// <summary>
    /// This ADT represents a collection of computing jobs.
    ///
    /// 
    /// Invariants: Capacity >=1 and Count <= Capacity and no duplicate in this job collection
    /// 

    /// </summary>

    public interface IJobCollection
    {

        /// <summary>
        /// Get the capacity of this job collection
        /// </summary>
        public uint Capacity { get; }

        /// <summary>
        /// Get the number of jobs currently present in this job collection. 
        /// </summary>
        public uint Count { get; }



        /// <summary>
        /// Add a job to this job collection.
        /// 
        /// Pre: Count < Capacity 
        /// 
        /// Post: returns true if job has been added into this job collection, new Count = old Count + 1, and new Capacity = old Capacity; 
        /// returns false, if job is present in this job collection,new Count = old Count, and new Capacity = old Capacity 
        /// 
        /// Interpretation: if there is room in this job collection to add a new job, and the 
        /// new job is not null, and none of the previously existing jobs has the same 
        /// ID as the new job, this method promises that: the collection has grown by one 
        /// element; all previously existing elements of the collection remain unchanged; the new job is in the 
        /// collection. 
        public bool Add(IJob job);



        /// <summary>
        /// Reveals whether or not a job is present in this job collection
        /// 
        /// Pre: nil
        /// 
        /// Post: return true, if the supplied job is in this job collection; return false otherwise. This job collection remains unchanged. 

        /// 
        /// Interpretation: Under all circumstances, this method promises to return true if and only
        /// if there is a job in this job collection which matches the supplied job id, and 
        /// that all existing contents in the collection will be as it was previously. 
        /// </summary>
        public bool Contains(uint id);




        /// <summary>
        /// Returns the reference of the job in this job collection which has the same id as the 
        /// supplied job id. If no such a job exists, returns null.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns a copy of the job in this job collection which has the same id as the 
        /// supplied job id; if no such a job exists, returns null. All existing content in this collection 
        /// will be as it waspreviously. 
        /// </summary>
        public IJob? Find(uint id);



        /// <summary>
        /// Removes a job from the collection, if present.
        /// 
        /// Pre: nil 
        /// 
        /// Post: returns true, if the job is present in this job coolection and the job has been removed from this job collection, new Count = old Count - 1 and new Cacpity = old Capcity; otherwise, returns false, new Count = old Count  and new Cacpity = old Capcity;
        /// if not present, this job collection remains unchanged, the total number of jobs remains the same and return false.
        /// </summary>
        public bool Remove(uint id);



        /// <summary>
        /// Gets an independent array which contains the same jobs as stored in 
        /// this collection.
        /// 
        /// Pre: nil
        /// 
        /// Post: returns an independent array which 
        /// contains the same jobs as in this collection 
        ///	 
        /// </summary>

        public IJob[] ToArray();
    }

}