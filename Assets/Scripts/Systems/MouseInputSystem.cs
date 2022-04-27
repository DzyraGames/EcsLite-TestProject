using Leopotam.EcsLite;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MouseInputSystem : IInputSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<TargetMovePositionComponent> _targetPositionPool;

        private Camera _camera;
        private Vector3 _targetPosition;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<TargetMovePositionComponent>().Inc<PlayerTagComponent>().End();
            _targetPositionPool = _world.GetPool<TargetMovePositionComponent>();
            _camera = Camera.main;
        }

        public void Run(EcsSystems systems)
        {
            SetTargetPosition();

            foreach (int entity in _filter)
            {
                ref TargetMovePositionComponent targetMovePositionComponent = ref _targetPositionPool.Get(entity);
                targetMovePositionComponent.TargetPosition = _targetPosition;
            }
        }

        private void SetTargetPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Constants.MaxRaycastDistance,
                    LayerMask.GetMask(Constants.GroundLayerMask)))
                {
                    _targetPosition = hit.point;
                }
            }
        }
    }
}