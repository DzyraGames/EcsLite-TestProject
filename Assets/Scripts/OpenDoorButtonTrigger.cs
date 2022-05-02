using System;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class OpenDoorButtonTrigger : MonoBehaviour
    {
        [SerializeField] private OpenDoorButtonMonoEntity _openDoorButtonMonoEntity;

        private EcsPool<ButtonPressedEvent> _buttonPressedEventPool;
        private EcsWorld _world;

        private void Start()
        {
            _world = WorldHandler.GetMainWorld();

            _buttonPressedEventPool = _world.GetPool<ButtonPressedEvent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.Player))
            {
                if (_openDoorButtonMonoEntity.TryGetEntity(out int buttonEntity))
                {
                    _buttonPressedEventPool.AddIfNone(buttonEntity);
                }
            }
        }
    }
}