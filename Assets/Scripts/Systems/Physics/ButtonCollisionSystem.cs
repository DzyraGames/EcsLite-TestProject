using LeoEcsPhysics;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class ButtonCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private SharedData _sharedData;
        
        private EcsFilter _onTriggerEnterFilter;
        private EcsFilter _onTriggerExitFilter;
        
        private EcsPool<OnTriggerEnterEvent> _onTriggerEnterEventPool;
        private EcsPool<OnTriggerExitEvent> _onTriggerExitEventPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>();
            
            _onTriggerEnterFilter = _world.Filter<OnTriggerEnterEvent>().End();
            _onTriggerExitFilter = _world.Filter<OnTriggerExitEvent>().End();

            _onTriggerEnterEventPool = _world.GetPool<OnTriggerEnterEvent>();
            _onTriggerExitEventPool = _world.GetPool<OnTriggerExitEvent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _onTriggerEnterFilter)
            {
                OnTriggerEnterEvent onTriggerEnterEvent = _onTriggerEnterEventPool.Get(entity);

                if (onTriggerEnterEvent.senderGameObject.TryGetComponent(out  ConvertToEntity openDoorButtonLink) &&
                    onTriggerEnterEvent.collider.CompareTag(Constants.Tags.Player))
                {
                    if (openDoorButtonLink.TryGetEntity(out int buttonEntity))
                    {
                        _sharedData.EventsBus.NewEvent<ButtonPressedEvent>().Entity = buttonEntity;
                    }
                }
            }
            
            foreach (int entity in _onTriggerExitFilter)
            {
                OnTriggerExitEvent onTriggerExitEvent = _onTriggerExitEventPool.Get(entity);

                if (onTriggerExitEvent.senderGameObject.TryGetComponent(out ConvertToEntity openDoorButtonLink) &&
                    onTriggerExitEvent.collider.CompareTag(Constants.Tags.Player))
                {
                    if (openDoorButtonLink.TryGetEntity(out int buttonEntity))
                    {
                        _sharedData.EventsBus.NewEvent<ButtonUnpressedEvent>().Entity = buttonEntity;
                    }
                }
            }
        }
    }
}