using EcsLiteTestProject.Data;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;
using Zenject;

namespace EcsLiteTestProject
{
    public class PlayerInitProvider : BaseMonoProvider, IConvertToEntity
    {
        private CharacterData _characterData;

        private EcsPool<TargetMoveSpeedComponent> _targetMoveSpeedComponentPool;
        private EcsPool<RotationSpeedComponent> _rotationSpeedComponentPool;
        private EcsPool<AccelerationComponent> _accelerationComponentPool; 
        
        [Inject]
        public void Construct(CharacterData characterData)
        {
            _characterData = characterData;
        }
        
        public void Convert(int entity, EcsWorld world)
        {
            _targetMoveSpeedComponentPool = world.GetPool<TargetMoveSpeedComponent>();
            _rotationSpeedComponentPool = world.GetPool<RotationSpeedComponent>();
            _accelerationComponentPool = world.GetPool<AccelerationComponent>();

            _targetMoveSpeedComponentPool.AddIfNone(entity);
            _rotationSpeedComponentPool.AddIfNone(entity);
            _accelerationComponentPool.AddIfNone(entity);
            
            ref TargetMoveSpeedComponent targetMoveSpeedComponent = ref _targetMoveSpeedComponentPool.Get(entity);
            ref RotationSpeedComponent rotationSpeedComponent = ref _rotationSpeedComponentPool.Get(entity);
            ref AccelerationComponent accelerationComponent = ref _accelerationComponentPool.Get(entity);

            targetMoveSpeedComponent.TargetSpeed = _characterData.MoveSpeed;
            rotationSpeedComponent.RotationSpeed = _characterData.RotationSpeed;
            accelerationComponent.Acceleration = _characterData.Acceleration;
        }
    }
}