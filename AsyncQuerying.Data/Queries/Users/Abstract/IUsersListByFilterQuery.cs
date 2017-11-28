using AsyncQuerying.Data.Models.Users;
using AsyncQuerying.Data.Queries.DelayedQuery.Abstract;
using System.Collections.Generic;

namespace AsyncQuerying.Data.Queries.Users.Abstract
{
    public interface IUsersListByFilterQuery : IDelayedQuery<IList<UserModel>>
    {
        UserFilterModel Filter { get; set; }
    }
}
