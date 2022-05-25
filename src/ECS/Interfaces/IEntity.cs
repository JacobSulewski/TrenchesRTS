using System;

namespace TrenchesRTS.ECS.Interfaces
{
    public interface IEntity : IDisposable
    {
        public int Id { get; }
        public int ComponentBitMask { get; }

        public IEntity AddComponent<T>(T component) where T : struct;
        public bool GetComponent<T>(out T component) where T : struct;
    }
}
