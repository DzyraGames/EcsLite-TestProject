using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class PingPongMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<PingPongMoveComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                
            }
        }

        private void ChangeDirection()
        {
            
        }
    }
}