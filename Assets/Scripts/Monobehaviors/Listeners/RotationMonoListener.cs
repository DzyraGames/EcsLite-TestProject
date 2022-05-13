namespace EcsLiteTestProject
{
    public class RotationMonoListener : ChangedValueMonoListener<RotationComponent>
    {
        public override void OnValueChanged(int entity, RotationComponent value)
        {
            transform.localRotation = value.Rotation;
        }
        
    }
}