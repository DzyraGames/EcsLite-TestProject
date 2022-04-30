using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class PingPongMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PingPongMoveComponent> _pingPongMovePool;
        private EcsPool<TransformComponent> _transformPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _pingPongMovePool = _world.GetPool<PingPongMoveComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _filter = _world.Filter<PingPongMoveComponent>().Inc<TransformComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMovePool.Get(entity);
                ref TransformComponent transformComponent = ref _transformPool.Get(entity);

                float pingPongValue = Mathf.PingPong(Time.time, 2f);
                transformComponent.Transform.position =  transformComponent.Transform.position.SetY(pingPongValue);
            }
        }

        private void ChangeDirection()
        {
            
        }
    }
}