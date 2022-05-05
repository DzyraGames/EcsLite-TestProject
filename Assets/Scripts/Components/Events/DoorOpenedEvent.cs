using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public struct DoorOpenedEvent : IEventReplicant
    {
        public int Entity;
    }
}