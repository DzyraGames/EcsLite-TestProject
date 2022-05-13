using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject.Transform
{
    public class RotationComponentProvider : BaseMonoProvider, IConvertToEntity
    {
        private EcsPool<RotationComponent> _rotationComponentPool; 
        
        public void Convert(int entity, EcsWorld world)
        {
            _rotationComponentPool = world.GetPool<RotationComponent>();
            _rotationComponentPool.Add(entity).Rotation = transform.rotation;
        }
    }
}