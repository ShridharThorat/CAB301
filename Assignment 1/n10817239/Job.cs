public class Job : IJob
{
    public Job(uint jobId, uint timeReceived, uint executionTime, uint priority)
    {
        Id = jobId;
        TimeReceived = timeReceived;
        ExecutionTime = executionTime;
        Priority = priority;
    }

    private uint id;
    private uint timeReceived;
    private uint executionTime;
    private uint priority;

    public uint Id
    {
        get
        {
            return id;
        }
        private set
        {
            if (!IsValidId(value))
                throw new ArgumentOutOfRangeException(nameof(Id));
            id = value;
        }
    }

    public uint TimeReceived
    {
        get
        {
            return timeReceived;
        }
        private set
        {
            if (!IsTimeReceived(value))
                throw new ArgumentOutOfRangeException(nameof(TimeReceived));
            timeReceived = value;
        }
    }

    public uint ExecutionTime
    {
        get
        {
            return executionTime;
        }
        private set
        {
            if (!IsValidExecutionTime(value))
                throw new ArgumentOutOfRangeException(nameof(ExecutionTime));
            executionTime = value;
        }
    }

    public uint Priority
    {
        get
        {
            return priority;
        }
        private set
        {
            if (!IsValidPriority(value))
                throw new ArgumentException(nameof(Priority));
            priority = value;
        }
    }

    public static bool IsValidId(uint id)
    {
        return (id >= 1 && id <= 999);
    }

    public static bool IsValidExecutionTime(uint executiontime)
    {
        return (executiontime > 0);
    }

    public static bool IsValidPriority(uint priority)
    {
        return (priority >= 1 && priority <= 9);
    }

    public static bool IsTimeReceived(uint time)
    {
        return (time > 0);
    }

    public override string ToString()
    {
        return $"Job(jobId: {id}, timeReceived: {timeReceived}, executionTime: {executionTime}, priority: {priority})";
    }
}
