using AsyncQuerying.Data.Queries.DelayedQuery;
using AsyncQuerying.Data.Queries.DelayedQuery.Abstract;
using AsyncQuerying.Data.Queries.DelayedQuery.Executors.Abstract;
using AsyncQuerying.Web.Models.Builders.Abstract;
using AsyncQuerying.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsyncQuerying.Web.Models.Builders
{
    public sealed class UsersListModelBuilder : IUsersListModelBuilder
    {
        public UsersListModelBuilder(IUsersQueryExecutor usersQueryExecutor)
        {
            this.usersQueryExecutor = usersQueryExecutor;
        }

        public UsersListModel Build(UserFilterModel filter, Boolean? delayed = null)
        {
            filter = filter ?? new UserFilterModel();

            Data.Models.Users.UserFilterModel dataFilter = filter.ToDataOne();

            var usersListResult = usersQueryExecutor.UsersListByFilter(dataFilter) as IDelayedQueryExecutionResult<IList<Data.Models.Users.UserModel>>;

            var result = new UsersListModel
                                {
                                    Filter = filter
                                };

            result.Filter.DelayedQueryState = DelayedQueryStateModel.FromDelayedExecutionResult(usersListResult);

            if (result.Filter.DelayedQueryState.StateCode == (Int32)QueryStatesEnum.Completed && (!delayed.HasValue || delayed.Value == false))
            {
                result.Items = BuildItems(usersListResult);
            }

            return result;
        }

        #region private

        private readonly IUsersQueryExecutor usersQueryExecutor;

        private IList<UserModel> BuildItems(IDelayedQueryExecutionResult<IList<Data.Models.Users.UserModel>> usersListResult)
        {
            var result = usersListResult.Result.Select(u => new UserModel
                                                                {
                                                                    Id = u.Id,
                                                                    Name = u.Name
                                                                })
                                               .ToList();

            return result;
        }

        #endregion
    }
}