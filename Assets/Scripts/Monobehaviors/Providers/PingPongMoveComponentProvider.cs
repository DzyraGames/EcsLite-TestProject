using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class PingPongMoveComponentProvider : BaseMonoProvider, IConvertToEntity
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        
        public void Convert(int entity, EcsWorld world)
        {
            _pingPongMoveComponentPool = world.GetPool<PingPongMoveComponent>();
            _pingPongMoveComponentPool.AddIfNone(entity);
            
            ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(entity);
            
            pingPongMoveComponent.StartPosition = transform.position;
            float endPosition = transform.position.y - _meshRenderer.bounds.size.y;
            pingPongMoveComponent.EndPosition = transform.position.SetY(endPosition);
        }
    }
}