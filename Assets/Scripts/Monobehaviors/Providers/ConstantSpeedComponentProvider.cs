using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public class ConstantSpeedComponentProvider : BaseMonoProvider, IConvertToEntity
    {
        [SerializeField] private float _speed;
        
        private EcsPool<ConstantMoveSpeedComponent> _constantSpeedComponentPool; 
       
        public void Convert(int entity, EcsWorld world)
        {
            _constantSpeedComponentPool = world.GetPool<ConstantMoveSpeedComponent>();
            _constantSpeedComponentPool.AddIfNone(entity);
            
            ref ConstantMoveSpeedComponent constantMoveSpeedComponent = ref _constantSpeedComponentPool.Get(entity);

            if (Mathf.Approximately(_speed, 0f))
            {
                constantMoveSpeedComponent.Speed = Constants.DefaultMovementSpeed;
            }
        }
    }
}