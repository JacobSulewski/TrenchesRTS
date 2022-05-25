using System;
using System.Collections.Generic;

namespace TrenchesRTS.Basics
{
    public class Pool<T>
    {
        private readonly Func<T> _createItem;
        private readonly Action<T> _resetItem;
        private readonly Stack<T> _freeItems = new();
        private readonly int _maximum;

        public Pool(Func<T> createItem, Action<T> resetItem, int initialSize = 32, int maximum = 1028)
        {
            _createItem = createItem;
            _resetItem = resetItem;
            _maximum = maximum;
            while (initialSize-- > 0)
                _freeItems.Push(createItem());
        }

        public Pool(Func<T> createItem, int initialSize = 32, int maximum = 1028)
            : this(createItem, _ => { }, initialSize, maximum)
        {
        }

        public T Obtain() => _freeItems.Count > 0 ? _freeItems.Pop() : _createItem();
        public void Free(T item)
        {
            if (_freeItems.Count > _maximum)
                _freeItems.Push(item);
            _resetItem(item);
        }
    }
}