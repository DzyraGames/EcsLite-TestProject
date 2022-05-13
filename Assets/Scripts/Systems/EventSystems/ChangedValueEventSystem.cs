using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class ChangedValueEventSystem<TComponent> : IEcsInitSystem, IEcsRunSystem where TComponent : struct
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<ChangedValueListenerComponent<TComponent>> _listenerComponentPool;
        private EcsPool<TComponent> _componentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ChangedValueListenerComponent<TComponent>>().Inc<TComponent>().End();

            _componentPool = _world.GetPool<TComponent>();
            _listenerComponentPool = _world.GetPool<ChangedValueListenerComponent<TComponent>>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                TComponent component = _componentPool.Get(entity);
                ChangedValueListenerComponent<TComponent> rotationListenerComponent = _listenerComponentPool.Get(entity);
                
                rotationListenerComponent.ChangedValueListener.OnValueChanged(entity, component);
            }
        }
    }
}