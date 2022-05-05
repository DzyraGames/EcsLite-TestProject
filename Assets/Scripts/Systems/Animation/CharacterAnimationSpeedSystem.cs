using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class CharacterAnimationSpeedSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<AnimatorComponent> _animatorComponentPool;
        private EcsPool<MoveSpeedComponent> _speedComponentPool;
        private EcsPool<TargetMoveSpeedComponent> _targetSpeedComponentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>().Inc<MoveSpeedComponent>().Inc<TargetMoveSpeedComponent>().End();

            _animatorComponentPool = _world.GetPool<AnimatorComponent>();
            _speedComponentPool = _world.GetPool<MoveSpeedComponent>();
            _targetSpeedComponentPool = _world.GetPool<TargetMoveSpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref AnimatorComponent animatorComponent = ref _animatorComponentPool.Get(entity);
                MoveSpeedComponent moveSpeedComponent = _speedComponentPool.Get(entity);
                TargetMoveSpeedComponent targetMoveSpeedComponent = _targetSpeedComponentPool.Get(entity);

                float normalizedSpeed = moveSpeedComponent.Speed / targetMoveSpeedComponent.TargetSpeed;
                animatorComponent.Animator.SetFloat(Constants.AnimatorHashes.SpeedHash, normalizedSpeed);
            }
        }
    }
}