using EcsLiteTestProject.Data;
using EcsLiteTestProject.Fabrics;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.UnityEditor;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

namespace EcsLiteTestProject
{
    public sealed class EcsStartup : MonoBehaviour
    {
        #region SharedData

        [SerializeField] private PlayerData _playerData;

        #endregion

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;

        private SharedData _sharedData;
        private EcsSystemFactory _systemFactory;

        [Inject]
        public void Construct(EcsSystemFactory systemFactory)
        {
            _systemFactory = systemFactory;
        }

        private void Start()
        {
            _world = new EcsWorld();
            InitSharedData();

            _systems = new EcsSystems(_world, _sharedData);
            _fixedSystems = new EcsSystems(_world, _sharedData);
            EcsPhysicsEvents.ecsWorld = _world;

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

        private void InitSharedData()
        {
            _sharedData = new SharedData
            {
                EventsBus = new EventsBus(),
                PlayerData = _playerData
            };
        }

        private void AddFixedSystems()
        {
            AddFixedSystem<ButtonCollisionSystem>();
        }

        private void AddPhysicsEvents()
        {
            _fixedSystems.DelHere<OnTriggerEnterEvent>();
            _fixedSystems.DelHere<OnTriggerExitEvent>();
        }

        private void AddSystems()
        {
            AddDebugSystems();
            AddInputSystems();
            AddMovementSystems();
            AddDoorMechanicsSystems();
            AddAnimationSystems();
        }

        #region Systems

        private void AddDebugSystems()
        {
#if UNITY_EDITOR
            AddSystem<EcsWorldDebugSystem>();
#endif
        }

        private void AddInputSystems()
        {
            AddSystem<MouseInputSystem>();
        }

        private void AddMovementSystems()
        {
            AddSystem<MoveToTargetSystem>();
            AddSystem<AlignInDirectionSystem>();
            AddSystem<SpeedAccelerationSystem>();
        }

        private void AddAnimationSystems()
        {
            AddSystem<CharacterAnimationSpeedSystem>();
        }
        
        private void AddDoorMechanicsSystems()
        {
            AddSystem<ButtonPressSystem>();
            AddSystem<OpenDoorSystem>();
        }

        #endregion

        private void AddEvents()
        {
            EventsBus eventsBus = _sharedData.EventsBus;
            _systems.Add(eventsBus.GetDestroyEventsSystem().IncReplicant<ButtonPressedEvent>());
            _systems.Add(eventsBus.GetDestroyEventsSystem().IncReplicant<ButtonUnpressedEvent>());
            _systems.Add(eventsBus.GetDestroyEventsSystem().IncReplicant<TargetPositionReachedEvent>());
            _systems.Add(eventsBus.GetDestroyEventsSystem().IncReplicant<DoorOpenedEvent>());
        }

        private void AddSystem<TSystem>() where TSystem : IEcsSystem
        {
            _systems.Add(_systemFactory.Create<TSystem>());
        }

        private void AddFixedSystem<Tsystem>() where Tsystem : IEcsSystem
        {
            _systems.Add(_systemFactory.Create<Tsystem>());
        }
    }
}