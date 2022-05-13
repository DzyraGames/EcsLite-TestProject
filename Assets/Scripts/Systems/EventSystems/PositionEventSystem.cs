using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class PositionEventSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<PositionListenerComponent> _positionListenerComponentPool;
        private EcsPool<PositionComponent> _positionComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PositionListenerComponent>().Inc<PositionComponent>().End();

            _positionComponentPool = _world.GetPool<PositionComponent>();
            _positionListenerComponentPool = _world.GetPool<PositionListenerComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                PositionComponent positionComponent = _positionComponentPool.Get(entity);
                PositionListenerComponent positionListenerComponent = _positionListenerComponentPool.Get(entity);

                positionListenerComponent.PositionListener.OnPositionChanged(entity, positionComponent.Position);
            }
        }
    }
}