using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public struct TargetPositionReachedEvent : IEventReplicant
    {
        public int Entity;
    }
}