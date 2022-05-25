﻿using System;
using TrenchesRTS.ECS.Interfaces;
using TrenchesRTS.Managers;

namespace TrenchesRTS.ECS
{
    public abstract class System
    {
        private readonly ComponentFilter _filter;
        private int _includeBitMask = 0;
        private int _excludeBitMask = 0;

        protected World World;

        protected System(ComponentFilter filter)
        {
            _filter = filter;
        }

        public void Init(World world, ComponentManager componentManager)
        {
            foreach (var type in _filter.IncludedTypes)
                _includeBitMask += 1 << componentManager.GetComponentId(type);

            foreach (var type in _filter.ExcludedTypes)
                _excludeBitMask += 1 << componentManager.GetComponentId(type);
        }

        // TODO: update based filtering? vs event based filtering? 
        public bool Verify(IEntity entity)
        {
            if ((entity.ComponentBitMask & _includeBitMask) != _includeBitMask)
                return false;
            if ((entity.ComponentBitMask & _excludeBitMask) != 0)
                return false;
            return true;
        }
    }

    public struct ComponentFilter
    {
        public Type[] IncludedTypes;
        public Type[] ExcludedTypes;

        public ComponentFilter(Type[] includedTypes, Type[] excludedTypes)
        {
            IncludedTypes = includedTypes;
            ExcludedTypes = excludedTypes;
        }
    }
}