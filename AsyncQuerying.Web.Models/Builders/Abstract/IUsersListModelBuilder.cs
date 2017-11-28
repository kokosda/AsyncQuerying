using AsyncQuerying.Web.Models.Users;
using System;

namespace AsyncQuerying.Web.Models.Builders.Abstract
{
    public interface IUsersListModelBuilder
    {
        UsersListModel Build(UserFilterModel filter, Boolean? delayed = null);
    }
}
