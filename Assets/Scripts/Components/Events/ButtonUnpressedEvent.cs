using SevenBoldPencil.EasyEvents;

namespace EcsLiteTestProject.Components.Events
{
    public struct ButtonUnpressedEvent : IEventReplicant
    {
        public int Entity;
    }
}