using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MouseInputSystem : IInputSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<TargetPositionComponent> _targetPositionComponentPool;
        private EcsPool<DirectionComponent> _directionComponentPool; 
        private EcsPool<TransformComponent> _transformComponentPool;

        private Camera _camera;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            _targetPositionComponentPool = _world.GetPool<TargetPositionComponent>();
            _directionComponentPool = _world.GetPool<DirectionComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _camera = Camera.main;
        }

        public void Run(EcsSystems systems)
        {
            if (!Input.GetMouseButtonDown(0)) 
                return;
            
            Vector3 targetPosition = GetTargetPosition();
            
            foreach (int entity in _filter)
            {
                _targetPositionComponentPool.AddIfNone(entity);
                _directionComponentPool.AddIfNone(entity);

                TransformComponent transformComponent = _transformComponentPool.Get(entity);
                ref TargetPositionComponent targetPositionComponent = ref _targetPositionComponentPool.Get(entity);
                ref DirectionComponent directionComponent = ref _directionComponentPool.Get(entity);

                Vector3 currentPosition = transformComponent.Transform.position;
                Vector3 ignoreYTargetPosition = targetPosition.SetY(currentPosition.y);
                targetPositionComponent.TargetPosition = ignoreYTargetPosition;
                directionComponent.Direction = (ignoreYTargetPosition - currentPosition).normalized;
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