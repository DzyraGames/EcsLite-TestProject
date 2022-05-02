using System;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class DoorMonoEntity : MonoEntity
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private const float EndMovePositionMultiplier = 0.99f; // TODO change field name
        
        private EcsPool<DoorComponent> _doorComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<TargetPositionMoveComponent> _targetPositionMoveComponentPool;
        private EcsPool<SpeedComponent> _speedComponentPool;

        protected override void Make()
        {
            _doorComponentPool = _world.GetPool<DoorComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _speedComponentPool = _world.GetPool<SpeedComponent>();

            _doorComponentPool.Add(_entity);
            _transformComponentPool.Add(_entity);
            _pingPongMoveComponentPool.Add(_entity);
            _targetPositionMoveComponentPool.Add(_entity);
            _speedComponentPool.Add(_entity);

            ref TransformComponent transformComponent = ref _transformComponentPool.Get(_entity);
            ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(_entity);
            ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionMoveComponentPool.Get(_entity);
            ref SpeedComponent speedComponent = ref _speedComponentPool.Get(_entity);
            
            transformComponent.Transform = transform;
            
            Vector3 transformPosition = transform.position;
            pingPongMoveComponent.StartPosition = transformPosition;

            float endMovePosition = transformPosition.y - _meshRenderer.bounds.size.y * EndMovePositionMultiplier;
            pingPongMoveComponent.EndPosition = transformPosition.SetY(endMovePosition);
            
            targetPositionMoveComponent.TargetPosition = transformPosition;
            speedComponent.Speed = 5f;
        }
    }
}