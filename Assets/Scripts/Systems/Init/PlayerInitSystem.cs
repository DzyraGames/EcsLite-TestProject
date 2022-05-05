using EcsLiteTestProject.Events;
using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _sharedData;
        private EcsPool<PlayerComponent> _playerPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<TargetSpeedComponent> _speedPool;
        private EcsPool<RotationSpeedComponent> _turnSpeedComponentPool;
        private EcsPool<AnimatorComponent> _animatorComponentPool;
        private EcsPool<AccelerationComponent> _accelerationComponentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>();
            
            int entity = _world.NewEntity();

            _playerPool = _world.GetPool<PlayerComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _speedPool = _world.GetPool<TargetSpeedComponent>();
            _turnSpeedComponentPool = _world.GetPool<RotationSpeedComponent>();
            _animatorComponentPool = _world.GetPool<AnimatorComponent>();
            _accelerationComponentPool = _world.GetPool<AccelerationComponent>();

            _playerPool.Add(entity);
            _transformPool.Add(entity);
            _speedPool.Add(entity);
            _turnSpeedComponentPool.Add(entity);
            _animatorComponentPool.Add(entity);
            _accelerationComponentPool.Add(entity);

            ref TransformComponent transformComponent = ref _transformPool.Get(entity);
            ref TargetSpeedComponent targetSpeedComponent = ref _speedPool.Get(entity);
            ref RotationSpeedComponent rotationSpeedComponent = ref _turnSpeedComponentPool.Get(entity);
            ref AnimatorComponent animatorComponent = ref _animatorComponentPool.Get(entity);
            ref AccelerationComponent accelerationComponent = ref _accelerationComponentPool.Get(entity);

            transformComponent.Transform = _sharedData.PlayerTransform;
            targetSpeedComponent.TargetSpeed = _sharedData.PlayerData.MoveSpeed;
            rotationSpeedComponent.Speed = _sharedData.PlayerData.TurnSpeed;
            animatorComponent.Animator = _sharedData.PlayerAnimator;
            accelerationComponent.Acceleration = _sharedData.PlayerData.Acceleration;
        }
    }
}