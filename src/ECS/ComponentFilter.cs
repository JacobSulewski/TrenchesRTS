using System;

namespace TrenchesRTS.ECS
{
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