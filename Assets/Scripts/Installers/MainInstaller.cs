using EcsLiteTestProject.Data;
using EcsLiteTestProject.Fabrics;
using EcsLiteTestProject.Services;
using UnityEngine;
using Zenject;

namespace EcsLiteTestProject
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CharacterData _characterData;

        public override void InstallBindings()
        {
            BindData();
            BindServices();
            BindFactories();
            BindCamera();
        }

        private void BindData()
        {
            Container.Bind<CharacterData>().FromInstance(_characterData);
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<UnityTimeService>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<EcsSystemFactory>().AsSingle().NonLazy();
        }

        private void BindCamera()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle().NonLazy();
        }
    }
}