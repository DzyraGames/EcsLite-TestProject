using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter; 
        private EcsPool<OnTriggerEnterEvent> _onTriggerEnterPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<OnTriggerEnterEvent>().End();
            _onTriggerEnterPool = _world.GetPool<OnTriggerEnterEvent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref OnTriggerEnterEvent onTriggerEnterEvent = ref _onTriggerEnterPool.Get(entity);

                if (onTriggerEnterEvent.senderGameObject.CompareTag(Constants.Tags.Button) 
                    && onTriggerEnterEvent.collider.CompareTag(Constants.Tags.Player))
                {
                    Debug.Log("Trigger");
                }
            }
        }
    }
}