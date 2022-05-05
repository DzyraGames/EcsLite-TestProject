using Leopotam.EcsLite;
using Zenject;

namespace EcsLiteTestProject.Fabrics
{
    public class EcsSystemFactory
    {
        private DiContainer _diContainer;

        public EcsSystemFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Tsystem Create<Tsystem>() where Tsystem : IEcsSystem
        {
            return _diContainer.Instantiate<Tsystem>();
        }
    }
}