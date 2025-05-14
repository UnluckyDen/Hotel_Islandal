using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace _Main.Scripts.Analytics
{
    public class GlobalAnalyticsService : MonoBehaviour
    {
        [SerializeField] private bool _sendInEditor = false; 
            
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
            #if UNITY_EDITOR
            if (!_sendInEditor)
            {
                Debug.LogWarning("Analytics in editor disabled now");
                yield break;
            }
            #endif
         
            bool initialized = false;
            UnityServices.Initialized += () =>
            {
                initialized = true;
                Debug.Log("Analytics initialized");
            };
            
            yield return UnityServices.InitializeAsync();
            while (!initialized)
                yield return null;
            
            AnalyticsService.Instance.StartDataCollection();
            _isInited = true;
        }

        public void SendCustomEvent(ICustomAnalyticsEvent customAnalyticsEvent)
        {
            if (!_isInited)
                return;
            
            AnalyticsService.Instance.RecordEvent(customAnalyticsEvent.GetCustomEvent());
        }
    }
}
