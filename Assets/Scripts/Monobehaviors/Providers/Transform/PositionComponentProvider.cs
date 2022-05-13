using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject.Transform
{
    public class PositionComponentProvider : BaseMonoProvider, IConvertToEntity
    {
        private EcsPool<PositionComponent> _positionComponentPool;
        
        public void Convert(int entity, EcsWorld world)
        {
            _positionComponentPool = world.GetPool<PositionComponent>();
            _positionComponentPool.Add(entity).Position = transform.position;
        }
    }
}