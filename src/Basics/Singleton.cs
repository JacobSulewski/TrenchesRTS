using System;

#nullable enable
namespace TrenchesRTS.Basics
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        public static readonly Lazy<T> InstanceHolder = new(() => new T(), true);
        public static T Instance => InstanceHolder.Value;
    }
}