using EcsLiteTestProject.Events;
using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _sharedData;
        private EcsPool<PlayerComponent> _playerPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<SpeedComponent> _speedPool;
        private EcsPool<TargetPositionMoveComponent> _targetPositionMovePool;
        private EcsPool<TargetPositionReachedEvent> _targetPositionReachedPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>();
            
            int entity = _world.NewEntity();

            _playerPool = _world.GetPool<PlayerComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _speedPool = _world.GetPool<SpeedComponent>();
            _targetPositionMovePool = _world.GetPool<TargetPositionMoveComponent>();
            _targetPositionReachedPool = _world.GetPool<TargetPositionReachedEvent>();

            _playerPool.Add(entity);
            _transformPool.Add(entity);
            _speedPool.Add(entity);
            _targetPositionMovePool.Add(entity);
            _targetPositionReachedPool.Add(entity);

            ref TargetPositionMoveComponent targetPositionMoveComponent = ref _targetPositionMovePool.Get(entity);
            ref TransformComponent transformComponent = ref _transformPool.Get(entity);
            ref SpeedComponent speedComponent = ref _speedPool.Get(entity);

            transformComponent.Transform = _sharedData.PlayerTransform;
            targetPositionMoveComponent.TargetPosition = transformComponent.Transform.position;
            speedComponent.Speed = _sharedData.PlayerData.MoveSpeed;
        }
    }
}