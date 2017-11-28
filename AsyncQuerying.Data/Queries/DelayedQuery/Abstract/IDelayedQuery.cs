using AsyncQuerying.Data.Queries.Abstract;
using System;
using System.Threading.Tasks;

namespace AsyncQuerying.Data.Queries.DelayedQuery.Abstract
{
    public interface IDelayedQuery<T> : IQuery<T> where T:class
    {
        Func<IDelayedQueryExecutionResult<T>> DelayedCriteria { get; }
    }
}
