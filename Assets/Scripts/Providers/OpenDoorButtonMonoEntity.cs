using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class OpenDoorButtonMonoEntity : MonoEntity
    {
        [SerializeField] private MonoEntity _doorLink;
        
        private EcsPool<OpenDoorButtonComponent> _openDoorButtonPool;
        
        protected override void Make()
        {
            _openDoorButtonPool = _world.GetPool<OpenDoorButtonComponent>();
            _openDoorButtonPool.Add(_entity);

            ref OpenDoorButtonComponent openDoorButtonComponent = ref _openDoorButtonPool.Get(_entity);
            openDoorButtonComponent.DoorEntityLink = _doorLink;
        }
    }
}