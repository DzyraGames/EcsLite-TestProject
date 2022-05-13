using UnityEngine;

namespace EcsLiteTestProject.Interfaces
{
    public interface IRotationListener
    {
        void OnRotationChanged(int entity, Quaternion rotation);
    }
}