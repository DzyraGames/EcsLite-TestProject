using UnityEngine;

namespace EcsLiteTestProject.Interfaces
{
    public interface IChangedValueListener<in TStruct> where TStruct : struct
    {
        void OnValueChanged(int entity, TStruct value);
    }
}