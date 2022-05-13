using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class PositionEventSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<ChangedValueListenerComponent<PositionComponent>> _positionListenerComponentPool;
        private EcsPool<PositionComponent> _positionComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ChangedValueListenerComponent<PositionComponent>>().Inc<PositionComponent>().End();

            _positionComponentPool = _world.GetPool<PositionComponent>();
            _positionListenerComponentPool = _world.GetPool<ChangedValueListenerComponent<PositionComponent>>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                PositionComponent positionComponent = _positionComponentPool.Get(entity);
                ChangedValueListenerComponent<PositionComponent> changedValueListenerComponent = _positionListenerComponentPool.Get(entity);

                changedValueListenerComponent.ChangedValueListener.OnValueChanged(entity, positionComponent);
            }
        }
    }
}