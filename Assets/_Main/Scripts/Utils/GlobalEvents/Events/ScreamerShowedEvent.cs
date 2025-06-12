namespace _Main.Scripts.Utils.GlobalEvents.Events
{
    public struct ScreamerShowedEvent : IEvent
    {
        public readonly int MindDamage;

        public ScreamerShowedEvent(int mindDamage)
        {
            MindDamage = mindDamage;
        }
    }
}