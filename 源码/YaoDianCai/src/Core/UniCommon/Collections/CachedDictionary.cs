using System;
using System.Web.Caching;
using System.Web;


namespace Uni.Core.Common.Collections
{
    public class CachedDictionary<T> : CachedDictionaryBase<T>
    {
        private readonly DateTime _absoluteExpiration;
        private readonly TimeSpan _slidingExpiration;

        public CachedDictionary(string baseKey, DateTime absoluteExpiration, TimeSpan slidingExpiration, Func<T, bool> cacheCodition)
        {
            if (cacheCodition == null) throw new ArgumentNullException("cacheCodition");

            _baseKey = baseKey;
            _absoluteExpiration = absoluteExpiration;
            _slidingExpiration = slidingExpiration;
            _cacheCodition = cacheCodition;
            InsertRootKey(_baseKey);
        }

        public CachedDictionary(string baseKey)
            : this(baseKey, (x) => true)
        {
        }

        public CachedDictionary(string baseKey, Func<T, bool> cacheCodition)
            : this(baseKey, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, cacheCodition)
        {
        }

        protected override void InsertRootKey(string rootKey)
        {
            HttpRuntime.Cache.Insert(rootKey, DateTime.UtcNow.Ticks, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        }

        public override void Reset(string rootKey, string key)
        {
            HttpRuntime.Cache.Remove(BuildKey(key, rootKey));
        }

        protected override object GetObjectFromCache(string fullKey)
        {
            return HttpRuntime.Cache.Get(fullKey);
        }

        public override void Add(string rootkey, string key, T newValue)
        {
            var builtrootkey = BuildKey(string.Empty, string.IsNullOrEmpty(rootkey) ? "root" : rootkey);
            if (HttpRuntime.Cache[builtrootkey] == null)
            {
                //Insert root if no present
                HttpRuntime.Cache.Insert(builtrootkey, DateTime.UtcNow.Ticks, null, _absoluteExpiration, _slidingExpiration, CacheItemPriority.NotRemovable, null);
            }
            CacheItemRemovedCallback removeCallBack = null;
            if (newValue != null)
            {
                HttpRuntime.Cache.Insert(BuildKey(key, rootkey), newValue, new CacheDependency(null, new[] { _baseKey, builtrootkey }), _absoluteExpiration, _slidingExpiration, CacheItemPriority.Normal, removeCallBack);
            }
            else
            {
                HttpRuntime.Cache.Remove(BuildKey(key, rootkey));//Remove if null
            }
        }
    }
}
