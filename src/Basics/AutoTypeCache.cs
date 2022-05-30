#nullable enable
using System;
using System.Collections.Generic;

namespace TrenchesRTS.Basics
{
    /// <summary>
    /// Cache that maintains the lifecycle of an object.
    /// Automatically creates an object based on the given type and args provided.
    /// Typically used for stateless objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AutoTypeCache<T>
    {
        private readonly IDictionary<(Type, object?[]?), T> _cache = new Dictionary<(Type, object?[]?), T>();

        /// <summary>
        /// Gets the object of the supplied type
        /// </summary>
        /// <typeparam name="TChild"> The type of the object to get from the cache. </typeparam>
        /// <param name="args"> The construction parameters associated with the object to get. </param>
        /// <returns> Returns the object in cache if present, otherwise instantiates a new version of the object. </returns>
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

        /// <summary>
        /// Removes (destroys) the object of the given type from the cache.
        /// </summary>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool Remove<TChild>(params object?[]? args) where TChild : T
        {
            if (!_cache.ContainsKey((typeof(TChild), args)))
                return false;
            _cache.Remove((typeof(TChild), args));
            return true;
        }
    }
}