namespace EcsLiteTestProject.Services
{
    public interface ITimeService
    {
        public float DeltaTime { get; }
        public float FixedDeltaTime { get; }
    }
}