using _Main.Scripts.NPCs.Resident;
using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public readonly struct DoorInteractionAnalyticsEvent : ICustomAnalyticsEvent
    {
        private readonly ResidentConditionType _residentConditionType;

        public DoorInteractionAnalyticsEvent(ResidentConditionType residentConditionType)
        {
            _residentConditionType = residentConditionType;
        }

        public CustomEvent GetCustomEvent()
        {
            var customEvent = new CustomEvent("door_interaction")
            {
                {"resident_condition", _residentConditionType.ToString()},
            };
            
            return customEvent;
        }
    }
}