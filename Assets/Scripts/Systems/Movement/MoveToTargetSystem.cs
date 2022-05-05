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
        private EcsPool<TargetPositionMoveComponent> _targetPositionComponentPool;
        private EcsPool<MoveSpeedComponent> _speedComponentPool;
        private EcsPool<DirectionComponent> _directionComponentComponentPool;
        private EcsPool<ConstantMoveSpeedComponent> _constantSpeedComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>();
            _filter = _world.Filter<TransformComponent>().Inc<TargetPositionMoveComponent>().End();

            _transformComponentPool = _world.GetPool<TransformComponent>();
            _targetPositionComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _speedComponentPool = _world.GetPool<MoveSpeedComponent>();
            _directionComponentComponentPool = _world.GetPool<DirectionComponent>();
            _constantSpeedComponentPool = _world.GetPool<ConstantMoveSpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                TargetPositionMoveComponent targetPositionMoveComponent = _targetPositionComponentPool.Get(entity);

                Vector3 currentPosition = transformComponent.Transform.position;
                Vector3 targetPosition = targetPositionMoveComponent.TargetPosition;

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
                ConstantMoveSpeedComponent constantMoveSpeedComponent = _constantSpeedComponentPool.Get(entity);
                currentSpeed = constantMoveSpeedComponent.Speed;
            }
            else if (_speedComponentPool.Has(entity))
            {
                MoveSpeedComponent moveSpeedComponent = _speedComponentPool.Get(entity);
                currentSpeed = moveSpeedComponent.Speed;
            }

            return currentSpeed;
        }
    }
}