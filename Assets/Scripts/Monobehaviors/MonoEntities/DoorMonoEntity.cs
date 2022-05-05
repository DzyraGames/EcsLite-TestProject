using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class DoorMonoEntity : MonoEntity
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private EcsPool<DoorComponent> _doorComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<ConstantMoveSpeedComponent> _constantSpeedComponentPool; 

        protected override void Make()
        {
            _doorComponentPool = _world.GetPool<DoorComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _constantSpeedComponentPool = _world.GetPool<ConstantMoveSpeedComponent>();

            _doorComponentPool.Add(_entity);
            _transformComponentPool.Add(_entity);
            _pingPongMoveComponentPool.Add(_entity);
            _constantSpeedComponentPool.Add(_entity);

            ref TransformComponent transformComponent = ref _transformComponentPool.Get(_entity);
            ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(_entity);
            ref ConstantMoveSpeedComponent constantMoveSpeedComponent = ref _constantSpeedComponentPool.Get(_entity);
            
            transformComponent.Transform = transform;
            
            Vector3 transformPosition = transform.position;
            pingPongMoveComponent.StartPosition = transformPosition;

            float endMovePosition = transformPosition.y - _meshRenderer.bounds.size.y;
            pingPongMoveComponent.EndPosition = transformPosition.SetY(endMovePosition);
            constantMoveSpeedComponent.Speed = Constants.DefaultMovementSpeed;
        }
    }
}