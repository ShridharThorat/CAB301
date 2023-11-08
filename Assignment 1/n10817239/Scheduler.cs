public class Scheduler : IScheduler
{
	public Scheduler(IJobCollection jobs)
	{
		Jobs = jobs;
	}

	public IJobCollection Jobs { get; }

	// Cant read IJob in JobCollection and therefore must create a duplicate
	// IJob for indexing and swapping, and duplicate JobCollection to 

	public IJob[] FirstComeFirstServed()
	{
		// Copy Jobs to an array with same capacity -> ToArray() does this
		IJob[] sortedJobs = this.Jobs.ToArray(); 
		// n would be this.Jobs.Count as described by pseudocode
		for (int i = 0; i <= (int)this.Jobs.Count - 2; i++)
		{
			int min_index = i; 
			for (int j = i + 1; j <= (int)this.Jobs.Count - 1; j++)
			{
				if ((sortedJobs[j].TimeReceived < sortedJobs[min_index].TimeReceived))
				{
					min_index = j; // index of the smallest item for iteration i
				}
			}
			(sortedJobs[i], sortedJobs[min_index]) = (sortedJobs[min_index], sortedJobs[i]);
		}
		return sortedJobs;
	}

	public IJob[] ShortestJobFirst()
	{
		IJob[] sortedJobs = this.Jobs.ToArray();

		for (int i = 0; i <= (int)this.Jobs.Count - 2; i++)
		{
			int min_index = i;
			for (int j = i + 1; j <= (int)this.Jobs.Count - 1; j++)
			{
				if (sortedJobs[j].ExecutionTime < sortedJobs[min_index].ExecutionTime)
				{
					min_index = j;
				}
			}
			(sortedJobs[i], sortedJobs[min_index]) = (sortedJobs[min_index], sortedJobs[i]);
		}
		return sortedJobs;
	}

	public IJob[] Priority()
	{
		IJob[] sortedJobs = this.Jobs.ToArray();

		for (int i = 0; i <= (int)this.Jobs.Count - 2; i++)
		{
			int min_index = i;
			for (int j = i + 1; j <= this.Jobs.Count - 1; j++)
			{
				if (sortedJobs[j].Priority > sortedJobs[min_index].Priority)
				{
					min_index = j;
				}
			}
			(sortedJobs[i], sortedJobs[min_index]) = (sortedJobs[min_index], sortedJobs[i]);
		}
		return sortedJobs;
	}

}
