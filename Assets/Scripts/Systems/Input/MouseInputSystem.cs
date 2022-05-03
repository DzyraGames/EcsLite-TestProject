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
        private EcsPool<TargetPositionReachedEvent> _targetPositionReachedEventPool;
        private EcsPool<TransformComponent> _transformComponentPool; 

        private Camera _camera;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<TargetPositionMoveComponent>().Inc<PlayerComponent>().Inc<TransformComponent>().End();
            _targetPositionPool = _world.GetPool<TargetPositionMoveComponent>();
            _targetPositionReachedEventPool = _world.GetPool<TargetPositionReachedEvent>();
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
                    ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionPool.Get(entity);
                    TransformComponent transformComponent = _transformComponentPool.Get(entity);
                    targetPositionMoveComponent.TargetPosition = targetPosition.SetY(transformComponent.Transform.position.y);
                    
                    _targetPositionReachedEventPool.DeleteIfHas(entity);
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