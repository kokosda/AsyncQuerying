using AsyncQuerying.Data.Contexts.Abstract;
using AsyncQuerying.Domain.Abstractions.Entities;
using Microsoft.Practices.ServiceLocation;
using System;

namespace AsyncQuerying.Data.Queries.Abstract
{
    public abstract class QueryBase<T> : IQuery<T> where T:class
    {
        public abstract Func<IQueryExecutionResult<T>> Criteria { get; }

        protected IAsyncQueryingContext<TEntity> GetContext<TEntity>() where TEntity : Entity
        {
            return ServiceLocator.Current.GetInstance<IAsyncQueryingContext<TEntity>>();
        }
    }
}
