using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject
{
    public struct ButtonUnpressedEvent : IEventReplicant
    {
        public int Entity;
    }
}