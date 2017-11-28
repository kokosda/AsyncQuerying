
namespace AsyncQuerying.Data.Queries.Abstract
{
    public interface IQueryExecutionResult<T> where T: class
    {
        T Result { get; }

        IQuery<T> Query { get; }
    }
}
