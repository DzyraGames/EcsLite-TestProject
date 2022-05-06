using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class TransformComponentProvider : BaseMonoProvider, IConvertToEntity
    {
        
        private EcsPool<TransformComponent> _transformComponentPool; 
        
        public void Convert(int entity, EcsWorld world)
        {
            _transformComponentPool = world.GetPool<TransformComponent>();
            _transformComponentPool.AddIfNone(entity);
            
            ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
            transformComponent.Transform = transform;
        }
    }
}