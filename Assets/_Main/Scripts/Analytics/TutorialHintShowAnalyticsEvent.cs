using _Main.Scripts.Tutorial;
using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public readonly struct TutorialHintShowAnalyticsEvent : ICustomAnalyticsEvent
    {
        private readonly BaseTutorialHint _baseTutorialHint;

        public TutorialHintShowAnalyticsEvent(BaseTutorialHint baseTutorialHint)
        {
            _baseTutorialHint = baseTutorialHint;
        }

        public CustomEvent GetCustomEvent()
        {
            var customEvent = new CustomEvent("tutorial_hint_show")
            {
                {"hint_name", _baseTutorialHint.gameObject.name},
            };
            
            return customEvent;
        }
    }
}