using AsyncQuerying.Data.Models.Users;
using AsyncQuerying.Data.Queries.DelayedQuery.Abstract;
using AsyncQuerying.Data.Queries.Users.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncQuerying.Data.Queries.Users
{
    public sealed class UsersListByFilterQueryResult : DelayedQueryExecutionResultBase<IList<UserModel>>
    {
        public UsersListByFilterQueryResult(IUsersListByFilterQuery query, Func<Task<IList<UserModel>>> resultAsync, String token)
            : base(query, resultAsync, token)
        {
        }
    }
}
