using EcsLiteTestProject.Interfaces;

namespace EcsLiteTestProject
{
    public struct ChangedValueListenerComponent<TStruct> where TStruct : struct
    {
        public IChangedValueListener<TStruct> ChangedValueListener;
    }
}