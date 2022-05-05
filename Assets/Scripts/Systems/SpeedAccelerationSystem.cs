using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class SpeedAccelerationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<TargetSpeedComponent> _targetSpeedComponentPool;
        private EcsPool<SpeedComponent> _speedComponentPool;
        private EcsPool<AccelerationComponent> _accelerationComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<TargetSpeedComponent>().Inc<AccelerationComponent>().Inc<TargetPositionMoveComponent>().End();

            _targetSpeedComponentPool = _world.GetPool<TargetSpeedComponent>();
            _speedComponentPool = _world.GetPool<SpeedComponent>();
            _accelerationComponentPool = _world.GetPool<AccelerationComponent>();
        }

        public void Run(EcsSystems systems)
        {
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
    }
}