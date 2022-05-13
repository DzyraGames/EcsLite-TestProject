using UnityEngine;

namespace EcsLiteTestProject.Interfaces
{
    public interface IPositionListener
    {
        void OnPositionChanged(int entity, Vector3 position);
    }
}