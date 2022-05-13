using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class TransformComponentProvider : BaseMonoProvider, IConvertToEntity
    {
        private EcsPool<PositionComponent> _positionComponentPool;
        private EcsPool<RotationComponent> _rotationComponentPool; 
        
        public void Convert(int entity, EcsWorld world)
        {
            _positionComponentPool = world.GetPool<PositionComponent>();
            _rotationComponentPool = world.GetPool<RotationComponent>();
            
            _positionComponentPool.AddIfNone(entity);
            _rotationComponentPool.AddIfNone(entity);

            ref PositionComponent positionComponent = ref _positionComponentPool.Get(entity);
            ref RotationComponent rotationComponent = ref _rotationComponentPool.Get(entity);

            positionComponent.Position = transform.position;
            rotationComponent.Rotation = transform.localRotation;
        }
    }
}