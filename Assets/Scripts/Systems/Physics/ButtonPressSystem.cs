using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;

        private EcsPool<TargetPositionComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;

            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonPressedEvent>(out var buttonPressedEventPool))
            {
                ButtonPressedEvent buttonPressedEvent = buttonPressedEventPool.Get(eventBody);
                int buttonEntity = buttonPressedEvent.Entity;
                
                ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(buttonEntity);
                AddTargetPositionComponent(buttonEntity, pingPongMoveComponent.EndPosition);
            }

            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonUnpressedEvent>(out var buttonUnpressedEventPool))
            {
                ButtonUnpressedEvent buttonUnpressedEvent = buttonUnpressedEventPool.Get(eventBody);
                int buttonEntity = buttonUnpressedEvent.Entity;
                
                ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(buttonEntity);
                AddTargetPositionComponent(buttonEntity, pingPongMoveComponent.StartPosition);
            }
        }

        private void AddTargetPositionComponent(int entity, Vector3 targetPosition)
        {
            _targetPositionMoveComponentPool.AddIfNone(entity);
            ref TargetPositionComponent targetPositionComponent = ref _targetPositionMoveComponentPool.Get(entity);
            targetPositionComponent.TargetPosition = targetPosition;
        }
    }
}