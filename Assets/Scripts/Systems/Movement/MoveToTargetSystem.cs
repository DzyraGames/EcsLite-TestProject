using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MoveToTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private SharedData _sharedData;
        private EcsFilter _filter;

        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<TargetPositionComponent> _targetPositionComponentPool;
        private EcsPool<SpeedComponent> _speedComponentPool;
        private EcsPool<DirectionComponent> _directionComponentComponentPool;
        private EcsPool<ConstantSpeedComponent> _constantSpeedComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>();
            _filter = _world.Filter<TransformComponent>().Inc<TargetPositionComponent>().End();

            _transformComponentPool = _world.GetPool<TransformComponent>();
            _targetPositionComponentPool = _world.GetPool<TargetPositionComponent>();
            _speedComponentPool = _world.GetPool<SpeedComponent>();
            _directionComponentComponentPool = _world.GetPool<DirectionComponent>();
            _constantSpeedComponentPool = _world.GetPool<ConstantSpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                TargetPositionComponent targetPositionComponent = _targetPositionComponentPool.Get(entity);

                Vector3 currentPosition = transformComponent.Transform.position;
                Vector3 targetPosition = targetPositionComponent.TargetPosition;

                float currentSpeed = GetSpeed(entity);
                transformComponent.Transform.position =
                    Vector3.MoveTowards(currentPosition, targetPosition, currentSpeed * Time.deltaTime);

                if (transformComponent.Transform.position == targetPosition)
                {
                    _targetPositionComponentPool.Del(entity);
                    _directionComponentComponentPool.DeleteIfHas(entity);
                    
                    _sharedData.EventsBus.NewEvent<TargetPositionReachedEvent>().Entity = entity;
                }
            }
        }

        private float GetSpeed(int entity)
        {
            float currentSpeed = Constants.DefaultMovementSpeed;
            if (_constantSpeedComponentPool.Has(entity))
            {
                ConstantSpeedComponent constantSpeedComponent = _constantSpeedComponentPool.Get(entity);
                currentSpeed = constantSpeedComponent.Speed;
            }
            else if (_speedComponentPool.Has(entity))
            {
                SpeedComponent speedComponent = _speedComponentPool.Get(entity);
                currentSpeed = speedComponent.Speed;
            }

            return currentSpeed;
        }
    }
}