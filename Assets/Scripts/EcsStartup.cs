using Leopotam.EcsLite;
using TMPro;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
            AddSystems();
            
            _systems.ConvertScene();
            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }
        
        private void AddSystems()
        {
#if UNITY_EDITOR
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _systems
                .Add(new MouseInputSystem())
                .Add(new MoveToTargetSystem());
        }
        
        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;

                _world.Destroy();
                _world = null;
            }
        }
    }
}