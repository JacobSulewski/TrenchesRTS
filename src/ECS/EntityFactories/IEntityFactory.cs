using TrenchesRTS.ECS.Interfaces;

namespace TrenchesRTS.ECS.EntityFactories
{
    public interface IEntityFactory
    {
        public IEntity SpawnInfantryman();
        public IEntity SpawnMachineGunner();
        public IEntity SpawnEngineer();
        public IEntity SpawnMortarMan();
    }

}