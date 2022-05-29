namespace TrenchesRTS.ECS
{
    public class ComponentMap<T> : ComponentMap where T : struct
    {

        private readonly T[] _components;

        public T this[int index]
        {
            get => _components[index];
            set => _components[index] = value;
        }

        public ComponentMap(int maxEntityCount)
        {
            _components = new T[maxEntityCount];
        }
    }

    public abstract class ComponentMap
    {
        // The component map is used to manage the components of a given type.
        // Maps the component with the entity's id.
        // Components are allocated as structs so that they can be localized and to reduce cache misses.
        // Since structs can not inherit classes, this makes declaring generics difficult. Thus this dummy abstract class and explicit
        // casting is to allow for polymorphism with the structs despite the limitation of not being able to declare the type statically.
    }
}