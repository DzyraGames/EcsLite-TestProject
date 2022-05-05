using EcsLiteTestProject.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class PlayerMonoEntity : MonoEntity
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerData _playerData;
        
        private EcsPool<PlayerComponent> _playerComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<TargetMoveSpeedComponent> _targetSpeedComponentPool;
        private EcsPool<RotationSpeedComponent> _rotationSpeedComponentPool;
        private EcsPool<AnimatorComponent> _animatorComponentPool;
        private EcsPool<AccelerationComponent> _accelerationComponentPool; 
        
        protected override void Make()
        {
            _playerComponentPool = _world.GetPool<PlayerComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _targetSpeedComponentPool = _world.GetPool<TargetMoveSpeedComponent>();
            _rotationSpeedComponentPool = _world.GetPool<RotationSpeedComponent>();
            _animatorComponentPool = _world.GetPool<AnimatorComponent>();
            _accelerationComponentPool = _world.GetPool<AccelerationComponent>();

            _playerComponentPool.Add(_entity);
            _transformComponentPool.Add(_entity);
            _targetSpeedComponentPool.Add(_entity);
            _rotationSpeedComponentPool.Add(_entity);
            _animatorComponentPool.Add(_entity);
            _accelerationComponentPool.Add(_entity);

            ref TransformComponent transformComponent = ref _transformComponentPool.Get(_entity);
            ref TargetMoveSpeedComponent targetMoveSpeedComponent = ref _targetSpeedComponentPool.Get(_entity);
            ref RotationSpeedComponent rotationSpeedComponent = ref _rotationSpeedComponentPool.Get(_entity);
            ref AnimatorComponent animatorComponent = ref _animatorComponentPool.Get(_entity);
            ref AccelerationComponent accelerationComponent = ref _accelerationComponentPool.Get(_entity);

            transformComponent.Transform = transform;
            targetMoveSpeedComponent.TargetSpeed = _playerData.MoveSpeed;
            rotationSpeedComponent.RotationSpeed = _playerData.TurnSpeed;
            animatorComponent.Animator = _animator;
            accelerationComponent.Acceleration = _playerData.Acceleration;
        }
    }
}