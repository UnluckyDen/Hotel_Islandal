using _Main.Scripts.NPCs.Resident;
using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public readonly struct DoorInteractionAnalyticsEvent : ICustomAnalyticsEvent
    {
        private readonly ResidentConditionType _residentConditionType;
        
        public CustomEvent GetCustomEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}