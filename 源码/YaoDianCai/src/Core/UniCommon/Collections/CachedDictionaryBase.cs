using System;
using System.Linq;


namespace Uni.Core.Common.Collections
{
    public abstract class CachedDictionaryBase<T>
    {
        protected string _baseKey;
        protected Func<T, bool> _cacheCodition;

        public T this[string key] { get { return Get(key); } }
        public T this[Func<T> @default] { get { return Get(@default); } }

        protected abstract void InsertRootKey(string rootKey);
        protected abstract object GetObjectFromCache(string fullKey);
        public abstract void Add(string rootkey, string key, T newValue);
        public abstract void Reset(string rootKey, string key);


        public T Get(string key)
        {
            return Get(string.Empty, key, null);
        }

        public T Get(string key, Func<T> defaults)
        {
            return Get(string.Empty, key, defaults);
        }

        public T Get(Func<T> @default)
        {
            string key = string.Format("func {0} {2}.{1}({3})", @default.Method.ReturnType, @default.Method.Name,
                                       @default.Method.DeclaringType.FullName,
                                       string.Join(",", @default.Method.GetGenericArguments().Select(x => x.FullName).ToArray()));
            return Get(key, @default);
        }

        public virtual T Get(string rootkey, string key, Func<T> defaults)
        {
            string fullKey = BuildKey(key, rootkey);
            object objectCache = GetObjectFromCache(fullKey);
            if (FitsCondition(objectCache))
            {
                return ReturnCached(objectCache);
            }

            if (defaults != null)
            {
                T newValue = defaults();
                if (_cacheCodition == null || _cacheCodition(newValue))
                {
                    Add(rootkey, key, newValue);
                }
                return newValue;
            }

            return default(T);
        }

        public void Add(string key, T newValue)
        {
            Add(string.Empty, key, newValue);
        }

        public bool HasItem(string key)
        {
            return !Equals(Get(key), default(T));
        }

        public void Reset(string key)
        {
            Reset(string.Empty, key);
        }

        public void Clear()
        {
            InsertRootKey(_baseKey);
        }

        public void Clear(string rootKey)
        {
            InsertRootKey(BuildKey(string.Empty, rootKey));
        }

        protected string BuildKey(string key, string rootkey)
        {
            return string.Format("{0}-{1}-{2}", _baseKey, rootkey, key);
        }

        protected virtual bool FitsCondition(object cached)
        {
            return cached != null && cached is T;
        }

        protected virtual T ReturnCached(object objectCache)
        {
            return (T)objectCache;
        }
    }
}
