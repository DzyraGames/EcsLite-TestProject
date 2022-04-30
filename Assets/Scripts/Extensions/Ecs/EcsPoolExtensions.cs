using Leopotam.EcsLite;

namespace EcsLiteTestProject
{
    public static class EcsPoolExtensions
    {
        public static void DeleteIfHas<TStruct>(this EcsPool<TStruct> pool, int entity) where TStruct : struct
        {
            if (pool.Has(entity))
            {
                pool.Del(entity);
            }
        }

        public static void AddIfNone<TStruct>(this EcsPool<TStruct> pool, int entity) where TStruct : struct
        {
            if (!pool.Has(entity))
            {
                pool.Add(entity);
            }
        }
    }
}