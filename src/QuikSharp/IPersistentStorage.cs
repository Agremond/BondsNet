// Licensed under the Apache License, Version 2.0. See LICENSE.txt in the project root for license information.

using System.Runtime.Caching;
using System.Collections.Generic;

namespace QuikSharp {
    /// <summary>
    /// 
    /// </summary>
    public interface IPersistentStorage {
        /// <summary>
        /// 
        /// </summary>
        void Set<T>(string key, T value);
        /// <summary>
        /// 
        /// </summary>
        T Get<T>(string key);
        /// <summary>
        /// 
        /// </summary>
        bool Contains(string key);
        /// <summary>
        /// 
        /// </summary>
        bool Remove(string key);
        /// <summary>
        /// 
        /// </summary>
        ICollection<string> Keys();

    }


    /// <summary>
    /// Thread-unsafe
    /// </summary>
    public class InMemoryStorage : IPersistentStorage {
        private static readonly IDictionary<string, object> Dic
            = new Dictionary<string, object>();

        private object syncRoot = new object();

        /// <summary>
        /// Useful for more advanced manipulation than IPersistentStorage
        /// QuikSharp depends only on IPersistentStorage
        /// </summary>
        private static IDictionary<string, object> Storage { get { return Dic; } }

        public void Set<T>(string key, T value) {
            lock (syncRoot) {
                Dic[key] = value;
            }
        }

        public T Get<T>(string key) {
            lock (syncRoot) {
                var v = (T)Dic[key];
                return (T)v;
            }
        }

        public bool Contains(string key) {
            lock (syncRoot) {
                if (Dic.ContainsKey(key)) {
                    return true;
                }
                return false;
            }
        }

        public bool Remove(string key) {
            lock (syncRoot) {
                var s = Dic.Remove(key);
                return s;
            }
        }
        public ICollection<string> Keys()
        {
            lock (syncRoot)
            {
                var s = (ICollection<string>)Dic.Keys;
                return s;
            }
        }
    }
}