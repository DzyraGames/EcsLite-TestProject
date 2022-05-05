using EcsLiteTestProject.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class PlayerMonoEntity : MonoEntity
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerData _playerData;
        
        private EcsPool<PlayerComponent> _playerPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<TargetSpeedComponent> _speedPool;
        private EcsPool<RotationSpeedComponent> _turnSpeedComponentPool;
        private EcsPool<AnimatorComponent> _animatorComponentPool;
        private EcsPool<AccelerationComponent> _accelerationComponentPool; 
        
        protected override void Make()
        {
            _playerPool = _world.GetPool<PlayerComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _speedPool = _world.GetPool<TargetSpeedComponent>();
            _turnSpeedComponentPool = _world.GetPool<RotationSpeedComponent>();
            _animatorComponentPool = _world.GetPool<AnimatorComponent>();
            _accelerationComponentPool = _world.GetPool<AccelerationComponent>();

            _playerPool.Add(_entity);
            _transformPool.Add(_entity);
            _speedPool.Add(_entity);
            _turnSpeedComponentPool.Add(_entity);
            _animatorComponentPool.Add(_entity);
            _accelerationComponentPool.Add(_entity);

            ref TransformComponent transformComponent = ref _transformPool.Get(_entity);
            ref TargetSpeedComponent targetSpeedComponent = ref _speedPool.Get(_entity);
            ref RotationSpeedComponent rotationSpeedComponent = ref _turnSpeedComponentPool.Get(_entity);
            ref AnimatorComponent animatorComponent = ref _animatorComponentPool.Get(_entity);
            ref AccelerationComponent accelerationComponent = ref _accelerationComponentPool.Get(_entity);

            transformComponent.Transform = transform;
            targetSpeedComponent.TargetSpeed = _playerData.MoveSpeed;
            rotationSpeedComponent.Speed = _playerData.TurnSpeed;
            animatorComponent.Animator = _animator;
            accelerationComponent.Acceleration = _playerData.Acceleration;
        }
    }
}