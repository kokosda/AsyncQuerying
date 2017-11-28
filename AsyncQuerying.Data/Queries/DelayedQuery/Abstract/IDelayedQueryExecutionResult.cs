using AsyncQuerying.Data.Queries.Abstract;
using System;
using System.Threading.Tasks;

namespace AsyncQuerying.Data.Queries.DelayedQuery.Abstract
{
    public interface IDelayedQueryExecutionResult<T> : IQueryExecutionResult<T> where T:class
    {
        QueryStatesEnum State { get; }

        void StartQuery();

        String Token { get; }
    }
}
