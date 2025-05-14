using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public readonly struct PlayerOpenJournalEvent : ICustomAnalyticsEvent
    {
        private readonly bool _open;

        public PlayerOpenJournalEvent(bool open)
        {
            _open = open;
        }

        public CustomEvent GetCustomEvent()
        {
            var customEvent = new CustomEvent("player_open_journal")
            {
                { "is_open", _open }
            };
            
            return customEvent;
        }
    }
}