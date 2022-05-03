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
        private EcsPool<TargetPositionReachedEvent> _targetPositionReachedEventPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _transformPool = _world.GetPool<TransformComponent>();
            _targetPositionPool = _world.GetPool<TargetPositionMoveComponent>();
            _targetPositionReachedEventPool = _world.GetPool<TargetPositionReachedEvent>();
            _speedPool = _world.GetPool<SpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            _filter = _world.Filter<TransformComponent>().Inc<TargetPositionMoveComponent>().Inc<SpeedComponent>().Exc<TargetPositionReachedEvent>().End();

            foreach (int entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformPool.Get(entity);
                TargetPositionMoveComponent targetPositionMoveComponent = _targetPositionPool.Get(entity);
                SpeedComponent speedComponent = _speedPool.Get(entity);

                Vector3 currentPosition = transformComponent.Transform.position;
                Vector3 targetPosition = targetPositionMoveComponent.TargetPosition;

                currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, speedComponent.Speed * Time.deltaTime);
                transformComponent.Transform.position = currentPosition;

                if (transformComponent.Transform.position == targetPosition)
                {
                    _targetPositionReachedEventPool.AddIfNone(entity);
                }
            }
        }
    }
}