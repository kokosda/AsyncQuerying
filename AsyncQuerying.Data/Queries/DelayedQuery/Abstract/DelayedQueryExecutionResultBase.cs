using AsyncQuerying.Data.Queries.Abstract;
using System;
using System.Threading.Tasks;

namespace AsyncQuerying.Data.Queries.DelayedQuery.Abstract
{
    public abstract class DelayedQueryExecutionResultBase<T> : QueryExecutionResultBase<T>, IDelayedQueryExecutionResult<T> where T:class
    {
        protected DelayedQueryExecutionResultBase(IQuery<T> query, Func<Task<T>> resultAsync, String token) 
            : base(query, null)
        {
            this.state = QueryStatesEnum.Created;
            this.resultAsync = resultAsync;
            this.token = token;
        }

        public QueryStatesEnum State
        {
            get
            {
                return state;
            }
        }

        public void StartQuery()
        {
            if (state == QueryStatesEnum.Created)
            {
                Task.Factory.StartNew(async () =>
                                            {
                                                state = QueryStatesEnum.Executing;

                                                try
                                                {
                                                    result = await resultAsync();

                                                    state = QueryStatesEnum.Completed;
                                                }
                                                catch
                                                {
                                                    state = QueryStatesEnum.Failed;
                                                }
                                            }, 
                                       TaskCreationOptions.PreferFairness);
            }
        }

        public string Token
        {
            get 
            {
                return token;
            }
        }

        #region private

        private volatile QueryStatesEnum state;
        private readonly Func<Task<T>> resultAsync;
        private readonly String token;

        #endregion
    }
}
