using System;

namespace AsyncQuerying.Web.Models.Users
{
    public sealed class UserFilterModel
    {
        public String Name { get; set; }

        public DelayedQueryStateModel DelayedQueryState { get; set; }

        public AsyncQuerying.Data.Models.Users.UserFilterModel ToDataOne()
        {
            var result = new AsyncQuerying.Data.Models.Users.UserFilterModel
                                {
                                    Name = Name,
                                    Token = DelayedQueryState != null ? DelayedQueryState.Token : null
                                };

            return result;
        }
    }
}