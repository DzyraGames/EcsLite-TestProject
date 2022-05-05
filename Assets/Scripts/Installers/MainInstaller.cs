using EcsLiteTestProject.Fabrics;
using UnityEngine;
using Zenject;

namespace EcsLiteTestProject
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle().NonLazy();
            Container.Bind<EcsSystemFactory>().AsSingle().NonLazy();
        }
    }
}