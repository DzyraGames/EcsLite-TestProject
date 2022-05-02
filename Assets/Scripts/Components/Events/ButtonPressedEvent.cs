using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public struct ButtonPressedEvent : IEventReplicant
    {
        public int Entity;
    }
}