using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public class OpenDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;
        private EcsFilter _filter;

        private EcsPool<TargetPositionMoveComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<IdComponent> _idComponentPool;
        private EcsPool<ConnectedIdComponent> _connectedIdComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;
            _filter = _world.Filter<DoorComponent>().Inc<PingPongMoveComponent>().Inc<IdComponent>().End();

            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _idComponentPool = _world.GetPool<IdComponent>();
            _connectedIdComponentPool = _world.GetPool<ConnectedIdComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonPressedEvent>(out var buttonPressedEventPool))
            {
                ButtonPressedEvent buttonPressedEvent = buttonPressedEventPool.Get(eventBody);
                int buttonEntity = buttonPressedEvent.Entity;

                if (!_connectedIdComponentPool.Has(buttonEntity))
                    continue;

                ConnectedIdComponent connectedIdComponent = _connectedIdComponentPool.Get(buttonEntity);

                foreach (int doorEntity in _filter)
                {
                    IdComponent idComponent = _idComponentPool.Get(doorEntity);
                    if (idComponent.Id == connectedIdComponent.Id)
                    {
                        _targetPositionMoveComponentPool.AddIfNone(doorEntity);
                        ref TargetPositionMoveComponent targetPositionMoveComponent =
                            ref _targetPositionMoveComponentPool.Get(doorEntity);
                        ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(doorEntity);
                        targetPositionMoveComponent.TargetPosition = pingPongMoveComponent.EndPosition;
                    }
                }
            }

            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonUnpressedEvent>(out var buttonUnpressedEventPool))
            {
                ButtonUnpressedEvent buttonUnpressedEvent = buttonUnpressedEventPool.Get(eventBody);
                int buttonEntity = buttonUnpressedEvent.Entity;

                if (!_connectedIdComponentPool.Has(buttonEntity))
                    continue;

                ConnectedIdComponent connectedIdComponent = _connectedIdComponentPool.Get(buttonEntity);

                foreach (int doorEntity in _filter)
                {
                    IdComponent idComponent = _idComponentPool.Get(doorEntity);
                    if (idComponent.Id == connectedIdComponent.Id)
                    {
                        _targetPositionMoveComponentPool.DeleteIfHas(doorEntity);
                    }
                }
            }

            foreach (int eventBody in _eventsBus.GetEventBodies<TargetPositionReachedEvent>(
                out var targetPositionReachedEventsPool))
            {
                TargetPositionReachedEvent targetPositionReachedEvent = targetPositionReachedEventsPool.Get(eventBody);

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