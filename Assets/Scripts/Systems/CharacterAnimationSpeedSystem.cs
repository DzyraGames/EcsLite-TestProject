using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public class CharacterAnimationSpeedSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<AnimatorComponent> _animatorComponentPool;
        private EcsPool<SpeedComponent> _speedComponentPool; 

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>().Inc<SpeedComponent>().End();

            _animatorComponentPool = _world.GetPool<AnimatorComponent>();
            _speedComponentPool = _world.GetPool<SpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref AnimatorComponent animatorComponent = ref _animatorComponentPool.Get(entity);
                SpeedComponent speedComponent = _speedComponentPool.Get(entity);
                animatorComponent.Animator.SetFloat(Constants.AnimatorHashes.SpeedHash, speedComponent.Speed);
            }
        }
    }
}