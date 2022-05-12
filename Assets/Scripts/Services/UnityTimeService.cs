using UnityEngine;
using Zenject;

namespace EcsLiteTestProject.Services
{
    public class UnityTimeService : ITimeService, ITickable, IFixedTickable
    {
        public float DeltaTime { get; private set; }
        public float FixedDeltaTime { get; private set; }


        public void Tick()
        {
            DeltaTime = Time.deltaTime;
        }

        public void FixedTick()
        {
            FixedDeltaTime = Time.fixedDeltaTime;
        }
    }
}