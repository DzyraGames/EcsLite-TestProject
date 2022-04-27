using EcsLiteTestProject.Extensions;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsLiteTestProject
{
    public class MoveToTargetSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<TargetMovePositionComponent> _targetPositionPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _transformPool = _world.GetPool<TransformComponent>();
            _targetPositionPool = _world.GetPool<TargetMovePositionComponent>();
        }

        public void Run(EcsSystems systems)
        {
            _filter = _world.Filter<PlayerTagComponent>().Inc<TargetMovePositionComponent>().End();
            
            foreach (int entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformPool.Get(entity);
                TargetMovePositionComponent targetMovePositionComponent = _targetPositionPool.Get(entity);
                transformComponent.Transform.position = Vector3.MoveTowards(transformComponent.Transform.position,
                    targetMovePositionComponent.TargetPosition.SetY(transformComponent.Transform.position.y), 10 * Time.deltaTime);
            }
        }
    }
}