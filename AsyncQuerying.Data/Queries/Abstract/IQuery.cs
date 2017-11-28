
using System;

namespace AsyncQuerying.Data.Queries.Abstract
{
    public interface IQuery<T> where T:class
    {
        Func<IQueryExecutionResult<T>> Criteria { get; }
    }
}
