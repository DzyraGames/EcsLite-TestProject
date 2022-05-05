using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class OpenDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;
        private EcsFilter _filter;

        private EcsPool<TargetPositionComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<OpenDoorButtonComponent> _openDoorButtonComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;
            _filter = _world.Filter<DoorComponent>().Inc<PingPongMoveComponent>().End();

            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _openDoorButtonComponentPool = _world.GetPool<OpenDoorButtonComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonPressedEvent>(out var buttonPressedEventPool))
            {
                ref ButtonPressedEvent buttonPressedEvent = ref buttonPressedEventPool.Get(eventBody);
                MonoEntity doorEntityLink = _openDoorButtonComponentPool.Get(buttonPressedEvent.Entity).DoorEntityLink;

                if (doorEntityLink.TryGetEntity(out int entity))
                {
                    _targetPositionMoveComponentPool.AddIfNone(entity);
                    ref TargetPositionComponent targetPositionComponent = ref _targetPositionMoveComponentPool.Get(entity);
                    ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(entity);

                    targetPositionComponent.TargetPosition = pingPongMoveComponent.EndPosition;
                }
            }

            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonUnpressedEvent>(out var buttonUnpressedEventPool))
            {
                ref ButtonUnpressedEvent buttonUnpressedEvent = ref buttonUnpressedEventPool.Get(eventBody);
                MonoEntity doorEntityLink = _openDoorButtonComponentPool.Get(buttonUnpressedEvent.Entity).DoorEntityLink;
                
                if (doorEntityLink.TryGetEntity(out int entity))
                {
                    _targetPositionMoveComponentPool.DeleteIfHas(entity);
                }
            }

            foreach (int eventBody in _eventsBus.GetEventBodies<TargetPositionReachedEvent>(out var targetPositionReachedEventsPool))
            {
                ref TargetPositionReachedEvent targetPositionReachedEvent = ref targetPositionReachedEventsPool.Get(eventBody);

                foreach (int doorEntity in _filter)
                {
                    if (doorEntity == targetPositionReachedEvent.Entity)
                    {
                        _eventsBus.NewEvent<DoorOpenedEvent>().Entity = doorEntity;
                    }
                }
                
            }
        }
    }
}