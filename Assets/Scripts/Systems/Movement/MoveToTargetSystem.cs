using EcsLiteTestProject.Events;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MoveToTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<TargetPositionMoveComponent> _targetPositionPool;
        private EcsPool<SpeedComponent> _speedPool;
        private EcsPool<DirectionComponent> _directionComponentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<TransformComponent>().Inc<TargetPositionMoveComponent>().End();
            _transformPool = _world.GetPool<TransformComponent>();
            _targetPositionPool = _world.GetPool<TargetPositionMoveComponent>();
            _speedPool = _world.GetPool<SpeedComponent>();
            _directionComponentPool = _world.GetPool<DirectionComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformPool.Get(entity);
                TargetPositionMoveComponent targetPositionMoveComponent = _targetPositionPool.Get(entity);
                
                _speedPool.AddIfNone(entity);
                ref SpeedComponent speedComponent = ref _speedPool.Get(entity);

                Vector3 currentPosition = transformComponent.Transform.position;
                Vector3 targetPosition = targetPositionMoveComponent.TargetPosition;

                currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, speedComponent.Speed * Time.deltaTime);
                transformComponent.Transform.position = currentPosition;

                if (transformComponent.Transform.position == targetPosition)
                {
                    _targetPositionPool.Del(entity);
                    _directionComponentPool.DeleteIfHas(entity);

                    speedComponent.Speed = 0f;
                }
            }
        }
    }
}