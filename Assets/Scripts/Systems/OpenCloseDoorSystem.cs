using EcsLiteTestProject.Components.Events;
using EcsLiteTestProject.Events;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public class OpenCloseDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;
        private EcsFilter _filter;

        private EcsPool<TargetPositionMoveComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<TargetPositionReachedEvent> _targetPositionReachedEventPool;
        private EcsPool<OpenDoorButtonComponent> _openDoorButtonComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;
            _filter = _world.Filter<DoorComponent>().Inc<PingPongMoveComponent>()
                .Exc<DoorOpenedEvent>().End();

            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _targetPositionReachedEventPool = _world.GetPool<TargetPositionReachedEvent>();
            _openDoorButtonComponentPool = _world.GetPool<OpenDoorButtonComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonPressedEvent>(out var buttonPressedEventPool))
            {
                ref ButtonPressedEvent buttonPressedEvent = ref buttonPressedEventPool.Get(eventBody);
                _openDoorButtonComponentPool.Get(buttonPressedEvent.Entity).DoorEntityLink.TryGetEntity(out int doorLink);

                foreach (int doorEntity in _filter)
                {
                    if (doorEntity != doorLink)
                        continue;

                    ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(doorEntity);

                    _targetPositionMoveComponentPool.AddIfNone(doorEntity);

                    ref TargetPositionMoveComponent targetPositionMoveComponent =
                        ref _targetPositionMoveComponentPool.Get(doorEntity);
                    
                    targetPositionMoveComponent.TargetPosition = pingPongMoveComponent.EndPosition;
                    _targetPositionReachedEventPool.DeleteIfHas(doorEntity);
                }
            }

            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonUnpressedEvent>(out var buttonUnpressedEventPool))
            {
                ref ButtonUnpressedEvent buttonPressedEvent = ref buttonUnpressedEventPool.Get(eventBody);
                _openDoorButtonComponentPool.Get(buttonPressedEvent.Entity).DoorEntityLink.TryGetEntity(out int doorLink);

                foreach (int doorEntity in _filter)
                {
                    if (doorEntity != doorLink)
                        continue;

                    _targetPositionMoveComponentPool.DeleteIfHas(doorEntity);
                }
            }
        }
    }
}