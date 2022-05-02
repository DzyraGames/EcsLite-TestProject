using EcsLiteTestProject.Events;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using UnityEngine.UI;

namespace EcsLiteTestProject
{
    public class OpenCloseDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;
        private EcsFilter _filter;
        private EcsFilter _buttonPressFilter;

        private EcsPool<TargetPositionMoveComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<ButtonPressedEvent> _buttonPressedEventPool;
        private EcsPool<TargetPositionReachedEvent> _targetPositionReachedEventPool;
        private EcsPool<OpenDoorButtonComponent> _openDoorButtonComponentPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;
            _filter = _world.Filter<DoorComponent>().Inc<TargetPositionMoveComponent>().Inc<PingPongMoveComponent>()
                .Exc<DoorOpenedEvent>().End();

            _buttonPressFilter = _world.Filter<OpenDoorButtonComponent>().Inc<ButtonPressedEvent>().End();

            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _buttonPressedEventPool = _world.GetPool<ButtonPressedEvent>();
            _targetPositionReachedEventPool = _world.GetPool<TargetPositionReachedEvent>();
            _openDoorButtonComponentPool = _world.GetPool<OpenDoorButtonComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _eventsBus.GetEventBodies<ButtonPressedEvent>(out var pool))
            {
                ref ButtonPressedEvent buttonPressedEvent = ref pool.Get(entity);
                _openDoorButtonComponentPool.Get(buttonPressedEvent.Entity).DoorEntityLink.TryGetEntity(out int doorLink);

                foreach (int doorEntity in _filter)
                {
                    if (doorEntity == doorLink)
                    {
                        ref TargetPositionMoveComponent targetPositionMoveComponent =
                            ref _targetPositionMoveComponentPool.Get(doorEntity);
                        ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(doorEntity);

                        targetPositionMoveComponent.TargetPosition = pingPongMoveComponent.EndPosition;
                        _targetPositionReachedEventPool.DeleteIfHas(doorEntity);
                    }
                }
            }
        }
    }
}