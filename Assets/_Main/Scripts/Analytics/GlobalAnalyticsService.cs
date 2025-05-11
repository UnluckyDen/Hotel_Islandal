using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace _Main.Scripts.Analytics
{
    public class GlobalAnalyticsService : MonoBehaviour
    {
        public static GlobalAnalyticsService Instance;

        private bool _isInited;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        private IEnumerator Start()
        {
            yield return UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();
            _isInited = true;
        }

        public void SendCustomEvent(ICustomAnalyticsEvent customAnalyticsEvent)
        {
            if (!_isInited)
                return;
            
            AnalyticsService.Instance.RecordEvent(customAnalyticsEvent.GetCustomEvent());
            AnalyticsService.Instance.Flush();
        }
    }
}
