using EcsLiteTestProject.Events;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MouseInputSystem : IInputSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<TargetPositionMoveComponent> _targetPositionPool;
        private EcsPool<DirectionComponent> _directionComponentPool; 
        private EcsPool<TransformComponent> _transformComponentPool;

        private Camera _camera;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            _targetPositionPool = _world.GetPool<TargetPositionMoveComponent>();
            _directionComponentPool = _world.GetPool<DirectionComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _camera = Camera.main;
        }

        public void Run(EcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPosition = GetTargetPosition();

                foreach (int entity in _filter)
                {
                    _targetPositionPool.AddIfNone(entity);
                    _directionComponentPool.AddIfNone(entity);
                    
                    ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionPool.Get(entity);
                    TransformComponent transformComponent = _transformComponentPool.Get(entity);
                    var currentPosition = transformComponent.Transform.position;

                    Vector3 correctTargetPosition = targetPosition.SetY(currentPosition.y);
                    targetPositionMoveComponent.TargetPosition = correctTargetPosition;

                    ref DirectionComponent directionComponent = ref _directionComponentPool.Get(entity);
                    directionComponent.Direction = (correctTargetPosition - currentPosition).normalized;
                }
            }
        }

        private Vector3 GetTargetPosition()
        {
            Vector3 targetPosition = Vector3.zero;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Constants.MaxRaycastDistance,
                LayerMask.GetMask(Constants.LayerMasks.Ground)))
            {
                targetPosition = hit.point;
            }

            return targetPosition;
        }
    }
}