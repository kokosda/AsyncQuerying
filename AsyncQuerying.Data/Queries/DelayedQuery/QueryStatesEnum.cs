
namespace AsyncQuerying.Data.Queries.DelayedQuery
{
    public enum QueryStatesEnum
    {
        Created = 0x4,
        Queued = 0x8,
        Executing = 0x16,
        Completed = 0x32,
        Failed = 0x64
    }
}
