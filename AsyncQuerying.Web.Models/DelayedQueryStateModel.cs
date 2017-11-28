using AsyncQuerying.Data.Queries.DelayedQuery.Abstract;
using System;

namespace AsyncQuerying.Web.Models
{
    public sealed class DelayedQueryStateModel
    {
        public String Token { get; set; }

        public Int32 StateCode { get; set; }

        public String StateString { get; set; }

        public static DelayedQueryStateModel FromDelayedExecutionResult<T>(IDelayedQueryExecutionResult<T> er) where T:class
        {
            DelayedQueryStateModel result = null;

            if (er != null)
            {
                var state = er.State;

                result = new DelayedQueryStateModel
                                {
                                    StateCode = (Int32)state,
                                    StateString = state.ToString(),
                                    Token = er.Token
                                };
            }

            return result;
        }
    }
}
