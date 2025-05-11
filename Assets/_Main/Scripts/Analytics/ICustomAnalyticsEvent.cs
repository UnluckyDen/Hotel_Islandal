using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public interface ICustomAnalyticsEvent
    {
        public abstract CustomEvent GetCustomEvent();
    }
}