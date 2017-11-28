using AsyncQuerying.Helpers.Caching.Abstract;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Runtime.Caching;

namespace AsyncQuerying.Helpers.Caching
{
    public static class InMemoryCache
    {
        static InMemoryCache()
        {
            cacheKeyGenerator = ServiceLocator.Current.GetInstance<ICacheKeyGenerator>();
        }

        public static T GetOrSet<T>(Object keyObject, Func<T> getItemCallback) where T : class
        {
            var cacheKey = cacheKeyGenerator.Generate(keyObject);

            var result = GetOrSet(cacheKey, getItemCallback);

            return result;
        }

        public static T GetOrSet<T>(String cacheKey, Func<T> getItemCallback) where T : class
        {
            T result = MemoryCache.Default.Get(cacheKey) as T;
            if (result == null && getItemCallback != null)
            {
                result = getItemCallback();

                if (cacheKey != null && result != null)
                {
                    var cachePolicy = new CacheItemPolicy
                                            {
                                                AbsoluteExpiration = DateTimeOffset.Now.Add(new TimeSpan(0, 1, 0))
                                            };

                    MemoryCache.Default.Set(cacheKey, result, cachePolicy);
                    MemoryCache.Default.Set(String.Format(cacheKeyPolicyPostFixFormat, cacheKey), cachePolicy, cachePolicy);
                }
            }

            return result;
        }

        public static T GetOrSet<T>(String cacheKey, Func<T> getItemCallback, CacheItemPolicy cacheItemPolicy) where T : class
        {
            T result = MemoryCache.Default.Get(cacheKey) as T;
            if (result == null && getItemCallback != null)
            {
                result = getItemCallback();

                if (cacheKey != null && result != null)
                {

                    var cachePolicy = cacheItemPolicy ?? new CacheItemPolicy
                                                                {
                                                                    AbsoluteExpiration = 
                                                                    DateTimeOffset.Now.Add(new TimeSpan(0, 1, 0))
                                                                };

                    MemoryCache.Default.Set(cacheKey, result, cachePolicy);
                    MemoryCache.Default.Set(String.Format(cacheKeyPolicyPostFixFormat, cacheKey), cachePolicy, cachePolicy);
                }
            }

            return result;
        }

        public static void Remove(String cacheKey)
        {
            MemoryCache.Default.Remove(cacheKey);
            MemoryCache.Default.Remove(String.Format(cacheKeyPolicyPostFixFormat, cacheKey));
        }

        public static String GenerateCacheKey(Object keyObject)
        {
            var result = cacheKeyGenerator.Generate(keyObject);

            return result;
        }

        #region private

        private const String cacheKeyPolicyPostFixFormat = "{0}_policy";

        private static readonly ICacheKeyGenerator cacheKeyGenerator;

        #endregion
    }
}
