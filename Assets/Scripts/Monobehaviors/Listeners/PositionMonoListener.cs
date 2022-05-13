using EcsLiteTestProject.Interfaces;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class PositionMonoListener : BaseMonoProvider, IEventListener, IPositionListener
    {
        private EcsPool<PositionListenerComponent> _positionListenerComponentPool;

        public void AddListener(int entity, EcsWorld world)
        {
            _positionListenerComponentPool = world.GetPool<PositionListenerComponent>();
            _positionListenerComponentPool.Add(entity).PositionListener = this;
        }

        public void OnPositionChanged(int entity, Vector3 position)
        {
            transform.position = position;
        }
    }
}