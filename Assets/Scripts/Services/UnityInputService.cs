using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject.Services
{
    public class UnityInputService : IInputService
    {
        public bool GetLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }

        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
    }
}