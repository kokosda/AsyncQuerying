using AsyncQuerying.Data.Models.Users;
using AsyncQuerying.Data.Queries.DelayedQuery.Executors.Abstract;
using AsyncQuerying.Data.Queries.Users;
using AsyncQuerying.Data.Queries.Users.Abstract;
using AsyncQuerying.Helpers.Caching;
using Microsoft.Practices.ServiceLocation;
using System;

namespace AsyncQuerying.Data.Queries.DelayedQuery.Executors
{
    public sealed class UsersQueryExecutor : DelayedQueryExecutorBase, IUsersQueryExecutor
    {
        public UsersListByFilterQueryResult UsersListByFilter(UserFilterModel filterModel)
        {
            UsersListByFilterQueryResult result = null;

            if (filterModel != null && filterModel.Token != null)
            {
                result = InMemoryCache.GetOrSet(filterModel.Token, (Func<UsersListByFilterQueryResult>)null);
            }

            if (result == null)
            {
                var query = ServiceLocator.Current.GetInstance<IUsersListByFilterQuery>();

                query.Filter = filterModel;

                result = query.DelayedCriteria() as UsersListByFilterQueryResult;

                result = InMemoryCache.GetOrSet(result.Token, () => result);

                result.StartQuery();
            }

            return result;
        }
    }
}
