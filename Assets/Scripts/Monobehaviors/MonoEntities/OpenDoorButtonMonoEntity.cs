using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class OpenDoorButtonMonoEntity : MonoEntity
    {
        [SerializeField] private MonoEntity _doorLink;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private EcsPool<OpenDoorButtonComponent> _openDoorButtonPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<TargetPositionMoveComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<SpeedComponent> _speedComponentPool;

        protected override void Make()
        {
            _openDoorButtonPool = _world.GetPool<OpenDoorButtonComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _speedComponentPool = _world.GetPool<SpeedComponent>();
            
            _openDoorButtonPool.Add(_entity);
            _transformComponentPool.Add(_entity);
            _targetPositionMoveComponentPool.Add(_entity);
            _pingPongMoveComponentPool.Add(_entity);
            _speedComponentPool.Add(_entity);

            ref OpenDoorButtonComponent openDoorButtonComponent = ref _openDoorButtonPool.Get(_entity);
            ref TransformComponent transformComponent = ref _transformComponentPool.Get(_entity);
            ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(_entity);
            ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionMoveComponentPool.Get(_entity);
            ref SpeedComponent speedComponent = ref _speedComponentPool.Get(_entity);
            
            openDoorButtonComponent.DoorEntityLink = _doorLink;
            
            transformComponent.Transform = transform;
            
            Vector3 transformPosition = transform.position;
            pingPongMoveComponent.StartPosition = transformPosition;

            float endMovePosition = transformPosition.y - _meshRenderer.bounds.size.y;
            pingPongMoveComponent.EndPosition = transformPosition.SetY(endMovePosition);
            
            targetPositionMoveComponent.TargetPosition = transformPosition;
            speedComponent.Speed = Constants.DefaultMovementSpeed;
        }
    }
}