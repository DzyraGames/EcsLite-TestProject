using EcsLiteTestProject.Data;
using EcsLiteTestProject.Fabrics;
using EcsLiteTestProject.Services;
using UnityEngine;
using Zenject;

namespace EcsLiteTestProject
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private CharacterData _characterData;

        public override void InstallBindings()
        {
            BindData();
            BindServices();
            BindFactories();
        }

        private void BindData()
        {
            Container.Bind<CharacterData>().FromInstance(_characterData);
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<UnityTimeService>().AsSingle().NonLazy();
            Container.Bind<ICameraService>().To<CameraService>().AsSingle().NonLazy();
            Container.Bind<IInputService>().To<UnityInputService>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<EcsSystemFactory>().AsSingle().NonLazy();
        }
    }
}