using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class RotationEventSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<RotationListenerComponent> _rotationListenerComponentPool;
        private EcsPool<RotationComponent> _rotationComponentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RotationListenerComponent>().Inc<RotationComponent>().End();

            _rotationListenerComponentPool = _world.GetPool<RotationListenerComponent>();
            _rotationComponentPool = _world.GetPool<RotationComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                RotationComponent rotationComponent = _rotationComponentPool.Get(entity);
                RotationListenerComponent rotationListenerComponent = _rotationListenerComponentPool.Get(entity);
                
                rotationListenerComponent.RotationListener.OnRotationChanged(entity, rotationComponent.Rotation);
            }
        }
    }
}