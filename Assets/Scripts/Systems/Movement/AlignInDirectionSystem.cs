using EcsLiteTestProject.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class AlignInDirectionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<DirectionComponent> _directionComponentPool;
        private EcsPool<RotationSpeedComponent> _turnSpeedComponentPool;

        private ITimeService _timeService;

        public AlignInDirectionSystem(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<TransformComponent>().Inc<DirectionComponent>().Inc<RotationSpeedComponent>().End();

            _transformComponentPool = _world.GetPool<TransformComponent>();
            _directionComponentPool = _world.GetPool<DirectionComponent>();
            _turnSpeedComponentPool = _world.GetPool<RotationSpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                DirectionComponent directionComponent = _directionComponentPool.Get(entity);
                RotationSpeedComponent rotationSpeedComponent = _turnSpeedComponentPool.Get(entity);

                Quaternion currentRotation = transformComponent.Transform.localRotation;
                Quaternion targetRotation = Quaternion.LookRotation(directionComponent.Direction, Vector3.up);
                transformComponent.Transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation,
                    rotationSpeedComponent.RotationSpeed * _timeService.DeltaTime);
            }
        }
    }
}