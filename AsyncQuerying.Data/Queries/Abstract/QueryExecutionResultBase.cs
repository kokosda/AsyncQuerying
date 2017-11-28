using AsyncQuerying.Data.Queries.Abstract;

namespace AsyncQuerying.Data.Queries.Abstract
{
    public abstract class QueryExecutionResultBase<T> : IQueryExecutionResult<T> where T:class
    {
        protected QueryExecutionResultBase(IQuery<T> query, T result)
        {
            this.query = query;
            this.result = result;
        }

        public T Result
        {
            get
            {
                return result;
            }
        }

        public IQuery<T> Query
        {
            get
            {
                return query;
            }
        }


        #region private

        private readonly IQuery<T> query;
        protected volatile T result;

        #endregion
    }
}
