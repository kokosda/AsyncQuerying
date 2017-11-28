using AsyncQuerying.Data.Models.Users;
using AsyncQuerying.Data.Queries.DelayedQuery.Abstract;
using AsyncQuerying.Data.Queries.Users.Abstract;
using AsyncQuerying.Domain.Entities;
using AsyncQuerying.Helpers.Caching;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncQuerying.Data.Queries.Users
{
    public sealed class UsersListByFilterQuery : DelayedQueryBase<IList<UserModel>>, IUsersListByFilterQuery
    {
        public UserFilterModel Filter { get; set; }

        public override Func<IDelayedQueryExecutionResult<IList<UserModel>>> DelayedCriteria
        {
            get 
            {
                Func<IDelayedQueryExecutionResult<IList<UserModel>>> result = 
                    () =>
                    {
                        UsersListByFilterQueryResult queryResult = null;

                        var context = GetContext<User>();

                        IQueryable<User> query = context.Entities.AsQueryable();

                        if (Filter != null && (!Filter.IsEmpty))
                        {
                            if (!String.IsNullOrEmpty(Filter.Name))
                            {
                                query = query.Where(e => e.Name.Contains(Filter.Name));
                            }
                        }
                        else
                        {
                            query = query.Take(10);
                        }

                        var queryUm = query.Select(u => new UserModel
                                                            {
                                                                Id = u.Id,
                                                                Name = u.Name
                                                            });

                        Func<Task<IList<UserModel>>> resultAsync = async () =>
                                                                            {
                                                                                var usersList = await queryUm.ToListAsync();

                                                                                context.Dispose();

                                                                                return usersList;
                                                                            };

                        var token = InMemoryCache.GenerateCacheKey(this);

                        queryResult = new UsersListByFilterQueryResult(this, resultAsync, token);

                        return queryResult;
                    };

                return result;
            }
        }
    }
}
