using System;
using System.Diagnostics;

namespace Assignment_1
{
    public class JobCollection : IJobCollection
    {
        private IJob[] jobs;
        private uint count;

        public JobCollection(uint capacity)
        {
            if (!(capacity >= 1)) throw new ArgumentException();
            jobs = new IJob[capacity];
            count = 0;
        }

        public uint Capacity
        {
            get { return (uint)jobs.Length; }
        }

        public uint Count
        {
            get { return count; }
        }


        public bool Add(IJob job)
        {
            // if there is room in this job collection to add a new job
            if (Count < Capacity)
            {
                // and the new job is not null
                if (job != null)
                {
                    // and none of the previously existing jobs has the same ID as the new job
                    if (Contains(job.Id) == false)
                    {
                        // jobCollection starts empty (i.e. count = 0)
                        jobs[count] = job;
                        count += 1;
                        return true;
                    }
                    // Note: Remove to streamline code output in testing
                    else { Console.WriteLine("\nJob to add has the same id as another. Invalid\n"); return false; }
                }
                else { Console.WriteLine("\nJob to add is null. Invalid\n"); return false; }
            }
            else { Console.WriteLine("\nNo room in the collection. Invalid\n"); return false; }
            // NOTE: Can obviously be smaller, but this is intuitive to me
        }


        public bool Contains(uint id)
        {
            // Sequential Search
            if (count == 0) return false; // If jobs is empty, don't check

            // Check each job in the collection of jobs
            uint i = 0;
            while (i < Capacity)
            {
                // for jobs that exist, compare id's
                if (this.jobs[i] != null && this.jobs[i].Id == id)
                {
                    return true;
                }
                i++;
            }
            return false;
        }

        public IJob? Find(uint id)
        {
            // Sequential Search extension
            if (Contains(id))
            {
                uint i = 0;
                while (i < Capacity)
                {
                    if (jobs[i].Id == id) return jobs[i];
                    i++;
                }
            }
            Console.WriteLine($"\nJob with id: {id} doesn't exist in the collection & thus cannot be found\n");
            return null;
        }

        public bool Remove(uint id)
        {
            IJob? jobToRemove = Find(id);

            // If the job to remove exists in the collection
            if (jobToRemove != null)
            {
                uint jobToRemove_index = 0;
                while ((jobToRemove_index < count) && jobs[jobToRemove_index].Id != jobToRemove.Id) jobToRemove_index++;

                // replace job to remove and subsequent jobs with their right neighbour
                for (uint j = jobToRemove_index; j < Count - 1; j++)
                {
                    jobs[j] = jobs[j + 1];
                }
                count--;
                // Although there is a duplicate, the collection allows adding
                // a job to the end of the array -> thus removing the duplicate
                return true;
            }
            Console.WriteLine($"\nJob with id: {id} doesn't exist in the collection\n");
            return false;

        }

        public IJob[] ToArray()
        {
            IJob[] replica = new IJob[Capacity]; // Instantiate an array with the same capacity
            for (int i = 0; i < Count; i++) // Copy each job into the replica
            {
                replica[i] = jobs[i];
            }
            return replica;

        }
    }
}