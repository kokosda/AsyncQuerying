using System;
using System.Web;

namespace AsyncQuerying.Infrastructure.Extensions
{
    public static class RequestExtensions
    {
        public static Boolean AcceptsJson(this HttpRequestBase request)
        {
            var result = false;

            if (request != null)
            {
                var acceptHeaderValue = request.Headers["Accept"];

                result = acceptHeaderValue != null && acceptHeaderValue.IndexOf("application/json") == 0;
            }

            return result;
        }
    }
}
