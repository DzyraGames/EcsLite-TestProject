using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class OpenDoorButtonProvider : BaseMonoProvider, IConvertToEntity
    {
        private EcsPool<OpenDoorButtonComponent> _openDoorButtonPool;
        
        public void Convert(int entity, EcsWorld world)
        {
            _openDoorButtonPool = world.GetPool<OpenDoorButtonComponent>();
            _openDoorButtonPool.Add(entity);
        }
    }
}