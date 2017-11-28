using System.Collections.Generic;

namespace AsyncQuerying.Web.Models.Users
{
    public sealed class UsersListModel
    {
        public IList<UserModel> Items { get; set; }

        public UserFilterModel Filter { get; set; }
    }
}