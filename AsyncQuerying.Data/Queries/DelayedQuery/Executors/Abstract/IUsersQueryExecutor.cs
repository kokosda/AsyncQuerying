using AsyncQuerying.Data.Models.Users;
using AsyncQuerying.Data.Queries.DelayedQuery.Abstract;
using AsyncQuerying.Data.Queries.Users;

namespace AsyncQuerying.Data.Queries.DelayedQuery.Executors.Abstract
{
    public interface IUsersQueryExecutor : IDelayedQueryExecutor
    {
        UsersListByFilterQueryResult UsersListByFilter(UserFilterModel filterModel);
    }
}
