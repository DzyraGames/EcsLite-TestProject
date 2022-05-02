using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EventsBus _eventsBus;
        
        private EcsFilter _buttonFilter;
        private EcsFilter _doorFilter;

        private EcsPool<OpenDoorButtonComponent> _openDoorButtonComponentPool;
        private EcsPool<DoorComponent> _doorComponentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventsBus = systems.GetShared<SharedData>().EventsBus;
            
            _buttonFilter = _world.Filter<OpenDoorButtonComponent>().End();
            _doorFilter = _world.Filter<DoorComponent>().End();

            _openDoorButtonComponentPool = _world.GetPool<OpenDoorButtonComponent>();
            _doorComponentPool = _world.GetPool<DoorComponent>();
        }

        public void Run(EcsSystems systems)
        {
            if (!_eventsBus.HasEvents<ButtonPressedEvent>())
                return;
            
            foreach (int buttonEntity in _buttonFilter)
            {
                    
            }
        }
    }
}