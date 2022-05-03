using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public abstract class MonoEntity : MonoBehaviour
    {
        protected EcsWorld _world;
        protected int _entity;
        protected EcsPackedEntity _packedEntity;

        protected virtual void Start()
        {
            _world = WorldHandler.GetMainWorld();
            _entity = _world.NewEntity();
            _packedEntity = _world.PackEntity(_entity);

            Make();
        }

        protected abstract void Make();

        public bool TryGetEntity(out int entity)
        {
            if (_packedEntity.Unpack(_world, out int unpackedEntity))
            {
                entity = unpackedEntity;
                return true;
            }

            entity = -1;
            return false;
        }
    }
}