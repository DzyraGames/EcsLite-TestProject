using EcsLiteTestProject.Components.Events;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;

        private EcsFilter _filter;

        private EcsPool<TargetPositionMoveComponent> _targetPositionMoveComponentPool;
        private EcsPool<PingPongMoveComponent> _pingPongMoveComponentPool;
        private EcsPool<TargetPositionReachedEvent> _targetPositionReachedEventPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;

            _filter = _world.Filter<OpenDoorButtonComponent>().Inc<TargetPositionMoveComponent>().Inc<PingPongMoveComponent>()
                .End();

            _targetPositionMoveComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _pingPongMoveComponentPool = _world.GetPool<PingPongMoveComponent>();
            _targetPositionReachedEventPool = _world.GetPool<TargetPositionReachedEvent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonPressedEvent>(out var buttonPressedEventPool))
            {
                ButtonPressedEvent buttonPressedEvent = buttonPressedEventPool.Get(eventBody);

                foreach (int entity in _filter)
                {
                    if (entity != buttonPressedEvent.Entity)
                        continue;

                    ref TargetPositionMoveComponent targetPositionMoveComponent =
                        ref _targetPositionMoveComponentPool.Get(entity);
                    ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(entity);

                    targetPositionMoveComponent.TargetPosition = pingPongMoveComponent.EndPosition;
                    _targetPositionReachedEventPool.DeleteIfHas(entity);
                }
            }
            
            foreach (int eventBody in _eventsBus.GetEventBodies<ButtonUnpressedEvent>(out var buttonUnpressedEventPool))
            {
                ButtonUnpressedEvent buttonPressedEvent = buttonUnpressedEventPool.Get(eventBody);

                foreach (int entity in _filter)
                {
                    if (entity != buttonPressedEvent.Entity)
                        continue;

                    ref TargetPositionMoveComponent targetPositionMoveComponent =
                        ref _targetPositionMoveComponentPool.Get(entity);
                    ref PingPongMoveComponent pingPongMoveComponent = ref _pingPongMoveComponentPool.Get(entity);

                    targetPositionMoveComponent.TargetPosition = pingPongMoveComponent.StartPosition;
                    _targetPositionReachedEventPool.DeleteIfHas(entity);
                }
            }
        }
        
        
    }
}