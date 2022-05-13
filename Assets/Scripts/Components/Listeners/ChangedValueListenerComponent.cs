using EcsLiteTestProject.Interfaces;

namespace EcsLiteTestProject
{
    public struct ChangedValueListenerComponent<TComponent> where TComponent : struct
    {
        public IChangedValueListener<TComponent> ChangedValueListener;
    }
}