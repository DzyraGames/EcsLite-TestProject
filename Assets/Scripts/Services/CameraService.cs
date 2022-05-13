using UnityEngine;

namespace EcsLiteTestProject.Services
{
    public class CameraService : ICameraService
    {
        private Camera _camera;

        public Camera Camera
        {
            get { return _camera ? _camera : Camera.main; }
        }

        public Ray ScreenPointToRay(Vector3 position)
        {
            return Camera.ScreenPointToRay(position);
        }
    }
}