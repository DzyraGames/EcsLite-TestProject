using EcsLiteTestProject.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class AlignInDirectionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<RotationComponent> _rotationComponentPool; 
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

            _filter = _world.Filter<RotationComponent>().Inc<DirectionComponent>().Inc<RotationSpeedComponent>().End();

            _rotationComponentPool = _world.GetPool<RotationComponent>();
            _directionComponentPool = _world.GetPool<DirectionComponent>();
            _turnSpeedComponentPool = _world.GetPool<RotationSpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref RotationComponent rotationComponent = ref _rotationComponentPool.Get(entity);
                DirectionComponent directionComponent = _directionComponentPool.Get(entity);
                RotationSpeedComponent rotationSpeedComponent = _turnSpeedComponentPool.Get(entity);

                Quaternion currentRotation = rotationComponent.Rotation;
                Quaternion targetRotation = Quaternion.LookRotation(directionComponent.Direction, Vector3.up);
                rotationComponent.Rotation = Quaternion.RotateTowards(currentRotation, targetRotation,
                    rotationSpeedComponent.RotationSpeed * _timeService.DeltaTime);
            }
        }
    }
}