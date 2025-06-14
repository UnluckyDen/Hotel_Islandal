using System.Collections;
using _Main.Scripts.Utils.GlobalEvents.Events;
using UnityEngine;
using UnityEngine.Rendering;
using EventProvider = _Main.Scripts.Utils.GlobalEvents.EventProvider;

namespace _Main.Scripts.Infrastructure.Postprocessing
{
    public class PostprocessingController : MonoBehaviour
    {
        [SerializeField] private Volume _globalVolume;
        [SerializeField] private Volume _screamerVolume;
        
        private Coroutine _coroutine;
        
        public static PostprocessingController Instance { get; private set; }
        
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

        private void Start() =>
            EventProvider.Instance.Subscribe<PlayerLostMindEvent>(ForceSetNormal);

        private void OnDestroy() =>
            EventProvider.Instance.UnSubscribe<PlayerLostMindEvent>(ForceSetNormal);

        [ContextMenu("Scream")]
        public void ToScream(float time)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(Scream(time));
        }

        [ContextMenu("Normal")]
        public void ToNormal(float time)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(Normal(time));
        }

        private void ForceSetNormal(PlayerLostMindEvent mindEvent)
        {
            _screamerVolume.weight = 0;
        }

        private IEnumerator Scream(float time)
        {
            float factor = 0f;

            while (factor < 1f)
            {
                _screamerVolume.weight = factor;
                factor += Time.deltaTime / time;
                yield return null;
            }

            _screamerVolume.weight = 1;
            _coroutine = null;
        }
        
        private IEnumerator Normal(float time)
        {
            float factor = 0f;

            while (factor < 1f)
            {
                _screamerVolume.weight = 1 - factor;
                factor += Time.deltaTime / time;
                yield return null;
            }

            _screamerVolume.weight = 0;
            _coroutine = null;
        }
    }
}