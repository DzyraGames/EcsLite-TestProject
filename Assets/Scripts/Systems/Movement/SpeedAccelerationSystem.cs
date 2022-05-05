using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class SpeedAccelerationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;
        private EcsFilter _filter;

        private EcsPool<TargetMoveSpeedComponent> _targetSpeedComponentPool;
        private EcsPool<MoveSpeedComponent> _speedComponentPool;
        private EcsPool<AccelerationComponent> _accelerationComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;
            _filter = _world.Filter<TargetMoveSpeedComponent>().Inc<AccelerationComponent>().Inc<TargetPositionMoveComponent>().End();

            _targetSpeedComponentPool = _world.GetPool<TargetMoveSpeedComponent>();
            _speedComponentPool = _world.GetPool<MoveSpeedComponent>();
            _accelerationComponentPool = _world.GetPool<AccelerationComponent>();
        }

        public void Run(EcsSystems systems)
        {
            ResetSpeedOnReachedPosition();
            
            foreach (int entity in _filter)
            {
                TargetMoveSpeedComponent targetMoveSpeedComponent = _targetSpeedComponentPool.Get(entity);
                AccelerationComponent accelerationComponent = _accelerationComponentPool.Get(entity);

                _speedComponentPool.AddIfNone(entity);
                ref MoveSpeedComponent moveSpeedComponent = ref _speedComponentPool.Get(entity);

                moveSpeedComponent.Speed = Mathf.Lerp(moveSpeedComponent.Speed, targetMoveSpeedComponent.TargetSpeed,
                    accelerationComponent.Acceleration * Time.deltaTime);
            }
        }

        private void ResetSpeedOnReachedPosition()
        {
            foreach (int eventBody in _eventsBus.GetEventBodies<TargetPositionReachedEvent>(
                out var targetPositionReachedEventPool))
            {
                TargetPositionReachedEvent targetPositionReachedEvent = targetPositionReachedEventPool.Get(eventBody);
                int entity = targetPositionReachedEvent.Entity;

                if (_speedComponentPool.Has(entity))
                {
                    ref MoveSpeedComponent moveSpeedComponent = ref _speedComponentPool.Get(entity);
                    moveSpeedComponent.Speed = 0f;
                }
            }
        }
    }
}