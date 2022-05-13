namespace EcsLiteTestProject
{
    public class PositionMonoListener : ChangedValueMonoListener<PositionComponent>
    {
        public override void OnValueChanged(int entity, PositionComponent value)
        {
            transform.position = value.Position;
        }
    }
}