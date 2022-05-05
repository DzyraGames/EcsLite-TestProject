using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class CharacterAnimationSpeedSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<AnimatorComponent> _animatorComponentPool;
        private EcsPool<SpeedComponent> _speedComponentPool;
        private EcsPool<TargetSpeedComponent> _targetSpeedComponentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>().Inc<SpeedComponent>().Inc<TargetSpeedComponent>().End();

            _animatorComponentPool = _world.GetPool<AnimatorComponent>();
            _speedComponentPool = _world.GetPool<SpeedComponent>();
            _targetSpeedComponentPool = _world.GetPool<TargetSpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref AnimatorComponent animatorComponent = ref _animatorComponentPool.Get(entity);
                SpeedComponent speedComponent = _speedComponentPool.Get(entity);
                TargetSpeedComponent targetSpeedComponent = _targetSpeedComponentPool.Get(entity);

                float normalizedSpeed = speedComponent.Speed / targetSpeedComponent.TargetSpeed;
                animatorComponent.Animator.SetFloat(Constants.AnimatorHashes.SpeedHash, normalizedSpeed);
            }
        }
    }
}