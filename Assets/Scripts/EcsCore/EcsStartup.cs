using System;
using EcsLiteTestProject.Events;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using SevenBoldPencil.EasyEvents;
using TMPro;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedSystems;
        private SharedData _sharedData;

        private void Start()
        {
            _world = new EcsWorld();
            _sharedData = new SharedData
            {
                EventsBus = new EventsBus()
            };
            
            _updateSystems = new EcsSystems(_world, _sharedData);
            _fixedSystems = new EcsSystems(_world, _sharedData);
            EcsPhysicsEvents.ecsWorld = _world;

            AddFixedSystems();
            AddUpdateSystems();
            AddEvents();
            
            _updateSystems.ConvertScene();
            _updateSystems.Init();

            _fixedSystems.ConvertScene();
            _fixedSystems.Init();
        }

        private void FixedUpdate()
        {
            _fixedSystems?.Run();
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void AddUpdateSystems()
        {
#if UNITY_EDITOR
            _updateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _updateSystems
                .Add(new MouseInputSystem())
                .Add(new MoveToTargetSystem());
        }

        private void AddFixedSystems()
        {
            _fixedSystems
                .Add(new ButtonPressSystem());
        }
        
        private void AddEvents()
        {
            _updateSystems
                .Add(_sharedData.EventsBus.GetDestroyEventsSystem().IncReplicant<ButtonPressedEvent>())
                .DelHere<OnTriggerEnterEvent>();
        }

        private void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;

                _world.Destroy();
                _world = null;
                
                _sharedData.EventsBus.Destroy();
                _sharedData.EventsBus = null;

                EcsPhysicsEvents.ecsWorld = null;
            }
        }
    }
}