#nullable enable
using System;
using System.Collections.Generic;

namespace TrenchesRTS.Basics
{
    public class AutoTypeCache<T>
    {
        private readonly IDictionary<(Type, object?[]?), T> _cache = new Dictionary<(Type, object?[]?), T>();

        public TChild Get<TChild>(params object?[]? args) where TChild : T
        {
            if (!_cache.TryGetValue((typeof(TChild), args), out var item))
            {
                var obj = Activator.CreateInstance(typeof(TChild), args);
                if (obj == null)
                    throw new ArgumentNullException(nameof(TChild), "Could not find constructor with:" + args);
                item = (T)obj;
                _cache.Add((typeof(TChild), args), item);
            }

            return (TChild)item!;
        }

        public bool Remove<TChild>(params object?[]? args) where TChild : T
        {
            if (!_cache.ContainsKey((typeof(TChild), args)))
                return false;
            _cache.Remove((typeof(TChild), args));
            return true;
        }
    }
}