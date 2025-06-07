namespace _Main.Scripts.Utils.GlobalEvents.Events
{
    public struct BookOpenedEvent : IEvent
    {
        public readonly bool IsOpen;

        public BookOpenedEvent(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}