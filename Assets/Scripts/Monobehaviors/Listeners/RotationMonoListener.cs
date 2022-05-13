using EcsLiteTestProject.Interfaces;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class RotationMonoListener : BaseMonoProvider, IEventListener, IRotationListener
    {
        private EcsPool<RotationListenerComponent> _rotationListenerComponentPool; 
        
        public void AddListener(int entity, EcsWorld world)
        {
            _rotationListenerComponentPool = world.GetPool<RotationListenerComponent>();
            _rotationListenerComponentPool.Add(entity).RotationListener = this;
        }

        public void OnRotationChanged(int entity, Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}