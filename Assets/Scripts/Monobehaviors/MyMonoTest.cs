using Leopotam.EcsLite;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class MyMonoTest : MonoBehaviour, IEcsWorldEventListener
    {
        public void OnEntityCreated(int entity)
        {
            transform.position += Vector3.one * 5f;
        }

        public void OnEntityChanged(int entity)
        {
        }

        public void OnEntityDestroyed(int entity)
        {
        }

        public void OnFilterCreated(EcsFilter filter)
        {
        }

        public void OnWorldResized(int newSize)
        {
        }

        public void OnWorldDestroyed(EcsWorld world)
        {
        }
    }
}