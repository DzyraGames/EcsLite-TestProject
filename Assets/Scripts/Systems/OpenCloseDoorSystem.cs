using EcsLiteTestProject.Events;
using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class OpenCloseDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<TargetPositionMoveComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DoorComponent>().Inc<TargetPositionMoveComponent>().Inc<PingPongMoveComponent>()
                .Exc<DoorOpenedEvent>().End();

            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionMoveComponentPool.Get(entity);
                ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(entity);

                targetPositionMoveComponent.TargetPosition = pingPongMoveComponent.EndPosition;
            }
        }
    }
}