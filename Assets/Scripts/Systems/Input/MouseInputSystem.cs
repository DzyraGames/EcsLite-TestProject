using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MouseInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<PositionComponent> _positionComponentPool;
        private EcsPool<TargetPositionMoveComponent> _targetPositionComponentPool;
        private EcsPool<DirectionComponent> _directionComponentPool;

        private IInputService _inputService;
        private ICameraService _cameraService;

        public MouseInputSystem(IInputService inputService, ICameraService cameraService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
        }

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<PlayerComponent>().Inc<PositionComponent>().End();
            _targetPositionComponentPool = _world.GetPool<TargetPositionMoveComponent>();
            _directionComponentPool = _world.GetPool<DirectionComponent>();
            _positionComponentPool = _world.GetPool<PositionComponent>();
        }

        public void Run(EcsSystems systems)
        {
            if (!_inputService.GetLeftMouseButtonDown())
                return;

            Vector3 targetPosition = GetTargetPosition();

            foreach (int entity in _filter)
            {
                _targetPositionComponentPool.AddIfNone(entity);
                _directionComponentPool.AddIfNone(entity);

                PositionComponent positionComponent = _positionComponentPool.Get(entity);
                ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionComponentPool.Get(entity);
                ref DirectionComponent directionComponent = ref _directionComponentPool.Get(entity);

                Vector3 currentPosition = positionComponent.Position;
                Vector3 ignoreYTargetPosition = targetPosition.SetY(currentPosition.y);
                targetPositionMoveComponent.TargetPosition = ignoreYTargetPosition;
                directionComponent.Direction = (ignoreYTargetPosition - currentPosition).normalized;
            }
        }

        private Vector3 GetTargetPosition()
        {
            Vector3 targetPosition = Vector3.zero;
            Ray ray = _cameraService.ScreenPointToRay(_inputService.GetMousePosition());

            if (Physics.Raycast(ray, out RaycastHit hit, Constants.MaxRaycastDistance,
                LayerMask.GetMask(Constants.LayerMasks.Ground)))
            {
                targetPosition = hit.point;
            }

            return targetPosition;
        }
    }
}