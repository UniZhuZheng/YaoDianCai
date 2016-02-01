using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


namespace Uni.Core.Common.Collections
{
    [Serializable]
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection
    {
        private readonly IDictionary<TKey, TValue> source;

        public int Count { get { return this.source.Count; } }

        public ICollection<TKey> Keys { get { return this.source.Keys; } }
        public ICollection<TValue> Values { get { return this.source.Values; } }
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get { return true; } }
        bool ICollection.IsSynchronized { get { return false; } }

        public TValue this[TKey key] { get { return this.source[key]; } set { ThrowNotSupportedException(); } }

        private object syncRoot;
        object ICollection.SyncRoot {
            get
            {
                if (this.syncRoot == null)
                {
                    ICollection collection = this.source as ICollection;

                    if (collection != null)
                    {
                        this.syncRoot = collection.SyncRoot;
                    }
                    else
                    {
                        Interlocked.CompareExchange(ref this.syncRoot, new object(), null);
                    }
                }

                return this.syncRoot;
            }
        }


        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionaryToWrap)
        {
            if (dictionaryToWrap == null)
            {
                throw new ArgumentNullException("dictionaryToWrap");
            }

            this.source = dictionaryToWrap;
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            ThrowNotSupportedException();
        }

        public bool ContainsKey(TKey key)
        {
            return this.source.ContainsKey(key);
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            ThrowNotSupportedException();
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.source.TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(
            KeyValuePair<TKey, TValue> item)
        {
            ThrowNotSupportedException();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            ThrowNotSupportedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            ICollection<KeyValuePair<TKey, TValue>> collection = this.source;

            return collection.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ICollection<KeyValuePair<TKey, TValue>> collection = this.source;
            collection.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            ThrowNotSupportedException();
            return false;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            IEnumerable<KeyValuePair<TKey, TValue>> enumerator = this.source;

            return enumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ICollection collection = new List<KeyValuePair<TKey, TValue>>(this.source);

            collection.CopyTo(array, index);
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException("This Dictionary is read-only");
        }
    }

    public static class DictionaryExtension
    {
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}
