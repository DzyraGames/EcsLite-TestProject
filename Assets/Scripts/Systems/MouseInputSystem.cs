using Leopotam.EcsLite;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MouseInputSystem : IInputSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<TargetPositionMoveComponent> _targetPositionPool;

        private Camera _camera;
        private Vector3 _targetPosition;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<TargetPositionMoveComponent>().Inc<PlayerComponent>().End();
            _targetPositionPool = _world.GetPool<TargetPositionMoveComponent>();
            _camera = Camera.main;
        }

        public void Run(EcsSystems systems)
        {
            SetTargetPosition();

            foreach (int entity in _filter)
            {
                ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionPool.Get(entity);
                targetPositionMoveComponent.TargetPosition = _targetPosition;
            }
        }

        private void SetTargetPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Constants.MaxRaycastDistance,
                    LayerMask.GetMask(Constants.LayerMasks.Ground)))
                {
                    _targetPosition = hit.point;
                }
            }
        }
    }
}