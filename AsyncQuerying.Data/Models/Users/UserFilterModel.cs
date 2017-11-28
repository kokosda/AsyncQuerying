using AsyncQuerying.Helpers.Caching;
using System;

namespace AsyncQuerying.Data.Models.Users
{
    public sealed class UserFilterModel
    {
        public String Name { get; set; }

        [CacheKeyGenIngore]
        public String Token { get; set; }

        [CacheKeyGenIngore]
        public Boolean IsEmpty
        {
            get
            {
                return Name == null;
            }
        }
    }
}