using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class OpenDoorButtonInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsPool<OpenDoorButtonComponent> _openDoorButtonPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            int entity = _world.NewEntity();
            _openDoorButtonPool = _world.GetPool<OpenDoorButtonComponent>();

            _openDoorButtonPool.Add(entity);
        }
    }
}