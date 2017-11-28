using System;

namespace AsyncQuerying.Helpers.Caching.Abstract
{
    public interface ICacheKeyGenerator
    {
        String Generate<T>(T keyObject);

        String Generate(Type keyObject, params Object[] properties);

        String GenerateEntityIdKey(Type contextType, Type entityType, Int32 entityId);
    }
}
