using AsyncQuerying.Data.Queries.Abstract;
using System;

namespace AsyncQuerying.Data.Queries.DelayedQuery.Abstract
{
    public abstract class DelayedQueryBase<T> : QueryBase<T>, IDelayedQuery<T> where T:class
    {
        public override Func<IQueryExecutionResult<T>> Criteria
        {
            get
            {
                throw new NotSupportedException("Query should be executed asynchronously.");
            }
        }

        public abstract Func<IDelayedQueryExecutionResult<T>> DelayedCriteria { get; }
    }
}
