using UnityEngine;

namespace EcsLiteTestProject
{
    public interface ICameraService
    {
        Ray ScreenPointToRay(Vector3 position);
    }
}