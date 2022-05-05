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

        private EcsPool<TargetSpeedComponent> _targetSpeedComponentPool;
        private EcsPool<SpeedComponent> _speedComponentPool;
        private EcsPool<AccelerationComponent> _accelerationComponentPool;
        private EcsFilter _targetPositionFiler;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;
            _filter = _world.Filter<TargetSpeedComponent>().Inc<AccelerationComponent>().Inc<TargetPositionComponent>().End();

            _targetSpeedComponentPool = _world.GetPool<TargetSpeedComponent>();
            _speedComponentPool = _world.GetPool<SpeedComponent>();
            _accelerationComponentPool = _world.GetPool<AccelerationComponent>();
        }

        public void Run(EcsSystems systems)
        {
            ResetSpeedOnReachedPosition();
            
            foreach (int entity in _filter)
            {
                TargetSpeedComponent targetSpeedComponent = _targetSpeedComponentPool.Get(entity);
                AccelerationComponent accelerationComponent = _accelerationComponentPool.Get(entity);

                _speedComponentPool.AddIfNone(entity);
                ref SpeedComponent speedComponent = ref _speedComponentPool.Get(entity);

                speedComponent.Speed = Mathf.Lerp(speedComponent.Speed, targetSpeedComponent.TargetSpeed,
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
                    ref SpeedComponent speedComponent = ref _speedComponentPool.Get(entity);
                    speedComponent.Speed = 0f;
                }
            }
        }
    }
}