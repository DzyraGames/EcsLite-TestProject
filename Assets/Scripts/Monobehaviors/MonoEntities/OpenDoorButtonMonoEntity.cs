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
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<ConstantSpeedComponent> _constantSpeedComponentPool; 

        protected override void Make()
        {
            _openDoorButtonPool = _world.GetPool<OpenDoorButtonComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _constantSpeedComponentPool = _world.GetPool<ConstantSpeedComponent>();
            
            _openDoorButtonPool.Add(_entity);
            _transformComponentPool.Add(_entity);
            _pingPongMoveComponentPool.Add(_entity);
            _constantSpeedComponentPool.Add(_entity);

            ref OpenDoorButtonComponent openDoorButtonComponent = ref _openDoorButtonPool.Get(_entity);
            ref TransformComponent transformComponent = ref _transformComponentPool.Get(_entity);
            ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(_entity);
            ref ConstantSpeedComponent constantSpeedComponent = ref _constantSpeedComponentPool.Get(_entity);
            
            openDoorButtonComponent.DoorEntityLink = _doorLink;
            
            transformComponent.Transform = transform;
            
            Vector3 transformPosition = transform.position;
            pingPongMoveComponent.StartPosition = transformPosition;

            float endMovePosition = transformPosition.y - _meshRenderer.bounds.size.y;
            pingPongMoveComponent.EndPosition = transformPosition.SetY(endMovePosition);
            
            constantSpeedComponent.Speed = Constants.DefaultMovementSpeed;
        }
    }
}