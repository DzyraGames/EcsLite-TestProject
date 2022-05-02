using EcsLiteTestProject.Data;
using EcsLiteTestProject.Events;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;
using Debug = System.Diagnostics.Debug;

namespace EcsLiteTestProject
{
    public sealed class EcsStartup : MonoBehaviour
    {
        #region SharedData

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerData _playerData;

        #endregion

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;
        private SharedData _sharedData;

        private void Start()
        {
            _world = new EcsWorld();
            InitSharedData();

            _systems = new EcsSystems(_world, _sharedData);
            _fixedSystems = new EcsSystems(_world, _sharedData);
            EcsPhysicsEvents.ecsWorld = _world;

            AddInitSystems();
            AddFixedSystems();
            AddPhysicsEvents();

            AddSystems();
            AddEvents();

            _systems.ConvertScene();
            _systems.Init();

            _fixedSystems.Init();
        }

        private void FixedUpdate()
        {
            _fixedSystems?.Run();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void InitSharedData()
        {
            _sharedData = new SharedData
            {
                EventsBus = new EventsBus(),
                PlayerTransform = _playerTransform,
                PlayerData = _playerData
            };
        }

        private void AddInitSystems()
        {
            _systems
                .Add(new PlayerInitSystem());
        }

        private void AddSystems()
        {
#if UNITY_EDITOR
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _systems
                .Add(new MouseInputSystem())
                .Add(new MoveToTargetSystem())
                .Add(new OpenCloseDoorSystem());
        }

        private void AddFixedSystems()
        {
            _fixedSystems
                .Add(new ButtonCollisionSystem())
                .Add(new ButtonPressSystem());
        }

        private void AddEvents()
        {
            _systems
                .Add(_sharedData.EventsBus.GetDestroyEventsSystem().IncReplicant<ButtonPressedEvent>());
        }

        private void AddPhysicsEvents()
        {
            _fixedSystems
                .DelHere<OnTriggerEnterEvent>()
                .DelHere<OnTriggerExitEvent>();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;

                _world.Destroy();
                _world = null;

                _sharedData.EventsBus.Destroy();
                _sharedData.EventsBus = null;

                EcsPhysicsEvents.ecsWorld = null;
            }
        }
    }
}