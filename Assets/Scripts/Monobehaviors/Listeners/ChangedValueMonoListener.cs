using EcsLiteTestProject.Interfaces;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public abstract class ChangedValueMonoListener<TComponent> : BaseMonoProvider, IEventListener,
        IChangedValueListener<TComponent>
        where TComponent : struct
    {
        private EcsPool<ChangedValueListenerComponent<TComponent>> _listenerComponentPool;

        public void AddListener(int entity, EcsWorld world)
        {
            _listenerComponentPool = world.GetPool<ChangedValueListenerComponent<TComponent>>();
            _listenerComponentPool.Add(entity).ChangedValueListener = this;
        }

        public abstract void OnValueChanged(int entity, TComponent value);
    }
}