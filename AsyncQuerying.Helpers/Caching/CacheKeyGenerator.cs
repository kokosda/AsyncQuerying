using AsyncQuerying.Helpers.Caching.Abstract;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AsyncQuerying.Helpers.Caching
{
    public sealed class CacheKeyGenerator : ICacheKeyGenerator
    {
        public String Generate<T>(T keyObject)
        {
            String result = null;

            if (keyObject != null)
            {
                result = String.Format("{0}", keyObject.GetType().Name);

                var sb = new StringBuilder(result);

                GenerateKeyForObject(keyObject, sb);

                result = sb.ToString();
            }

            return result;
        }

        public String Generate(Type keyObject, params Object[] parameters)
        {
            String result = null;

            if (keyObject != null)
            {
                result = String.Format("{0}", keyObject.Name);

                if (parameters.Length > 0)
                {
                    var sb = new StringBuilder(result);

                    for(var i = 0; i < parameters.Length; i++)
                    {
                        sb.AppendFormat("_{0}", parameters[i] != null ? parameters[i].ToString() : String.Empty);
                    }

                    result = sb.ToString();
                }
            }

            return result;
        }

        public String GenerateEntityIdKey(Type contextType, Type entityType, Int32 entityId)
        {
            String result = null;

            if (entityType != null)
            {
                if (contextType == null)
                {
                    result = String.Format("{0}_{1}", entityType.Name, entityId.ToString());
                }
                else
                {
                    result = String.Format("{0}_{1}_{2}", contextType.FullName, entityType.Name, entityId.ToString());
                }
            }

            return result;
        }

        #region private

        private void GenerateKeyForObject<T>(T keyObject, StringBuilder sb)
        {
            var properties = keyObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                .Where(p => p.PropertyType.IsPrimitive || p.PropertyType.IsEnum || 
                                                            !p.PropertyType.IsInterface && 
                                                             p.PropertyType.FullName.StartsWith("AsyncQuerying", StringComparison.Ordinal) ||
                                                            p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ||
                                                            p.PropertyType.IsArray ||
                                                            p.PropertyType.Equals(typeof(String)))
                                                .Where(p => p.GetCustomAttribute(typeof(CacheKeyGenIngoreAttribute)) == null);

            foreach (var p in properties)
            {
                if (p.PropertyType.IsPrimitive || p.PropertyType.IsEnum || p.PropertyType.IsGenericType || p.PropertyType.Equals(typeof(String)))
                {
                    var obj = p.GetValue(keyObject);
                    if (obj != null)
                    {
                        sb.AppendFormat("_{0}_{1}", p.Name, obj.ToString());
                    }
                }
                else
                {
                    var obj = p.GetValue(keyObject);
                    if (obj != null)
                    {
                        if (!p.PropertyType.IsArray)
                        {
                            sb.AppendFormat("_{0}_{{", p.PropertyType.Name);
                            GenerateKeyForObject(obj, sb);
                            sb.Append("}");
                        }
                        else
                        {
                            var arrayOfValues = obj as Array;
                            
                            if (arrayOfValues.Length > 0)
                            {
                                var arrType = arrayOfValues.GetValue(0).GetType();
                                if (arrType.IsPrimitive || arrType.IsEnum)
                                {
                                    sb.AppendFormat("_{0}_[", p.Name);
                                    foreach(var o in arrayOfValues)
                                    {
                                        sb.AppendFormat("_{0}", o.ToString());
                                    }
                                    sb.Append("]");
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
