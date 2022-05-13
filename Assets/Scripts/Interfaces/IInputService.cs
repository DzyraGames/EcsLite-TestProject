using UnityEngine;

namespace EcsLiteTestProject
{
    public interface IInputService
    {
        bool GetLeftMouseButtonDown();
        Vector3 GetMousePosition();
    }
}