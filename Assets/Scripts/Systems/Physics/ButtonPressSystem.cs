using System.Xml.Serialization;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _onTriggerEnterFilter;
        private EcsFilter _onTriggerExitFilter;
        private EcsFilter _buttonFilter;
        
        private EcsPool<OnTriggerEnterEvent> _onTriggerEnterEventPool;
        private EcsPool<OnTriggerExitEvent> _onTriggerExitEventPool; 
        private EcsPool<ButtonPressedEvent> _buttonPressedEventPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _onTriggerEnterFilter = _world.Filter<OnTriggerEnterEvent>().End();
            _onTriggerExitFilter = _world.Filter<OnTriggerExitEvent>().End();
            _buttonFilter = _world.Filter<OpenDoorButtonComponent>().End();

            _onTriggerEnterEventPool = _world.GetPool<OnTriggerEnterEvent>();
            _onTriggerExitEventPool = _world.GetPool<OnTriggerExitEvent>();
            _buttonPressedEventPool = _world.GetPool<ButtonPressedEvent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int buttonEntity in _buttonFilter)
            {
                foreach (int onTriggerEnterEntity in _onTriggerEnterFilter)
                {
                    OnTriggerEnterEvent onTriggerEnterEvent = _onTriggerEnterEventPool.Get(onTriggerEnterEntity);

                    if (onTriggerEnterEvent.senderGameObject.CompareTag(Constants.Tags.Button)
                        && onTriggerEnterEvent.collider.CompareTag(Constants.Tags.Player))
                    {
                        _buttonPressedEventPool.AddIfNone(buttonEntity);
                        Debug.Log("Enter");
                    }
                }
                
                foreach (int onTriggerExitEntity in _onTriggerExitFilter)
                {
                    OnTriggerExitEvent onTriggerExitEvent = _onTriggerExitEventPool.Get(onTriggerExitEntity);

                    if (onTriggerExitEvent.senderGameObject.CompareTag(Constants.Tags.Button)
                        && onTriggerExitEvent.collider.CompareTag(Constants.Tags.Player))
                    {
                        _buttonPressedEventPool.DeleteIfHas(buttonEntity);
                        Debug.Log("Exit");
                    }
                }
            }
        }
    }
}