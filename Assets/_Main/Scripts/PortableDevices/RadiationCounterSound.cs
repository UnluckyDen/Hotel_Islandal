using UnityEngine;

namespace _Main.Scripts.PortableDevices
{
    public class RadiationCounterSound : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _geigerClicks; 
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private float _minDelay = 0.2f; 
        [SerializeField] private float _maxDelay = 5f; 
        [SerializeField] private float _minRadiation = 1f;
        [SerializeField] private float _maxRadiation = 5f;
        [SerializeField] private float _radiationLevel = 0.5f;
    
        private float _nextClickTime;
        private bool _active;

        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }

        public void SetRadiationLevel(float level)
        {
            _radiationLevel = Mathf.Clamp(level, _minDelay, _maxRadiation);
        }

        private void Update()
        {
            if (_active == false)
                return;
            
            if (Time.time >= _nextClickTime)
            {
                PlayGeigerClick();
                CalculateNextClick();
            }
        }

        private void PlayGeigerClick()
        {
            if (_geigerClicks.Length == 0) return;
        
            AudioClip clip = _geigerClicks[Random.Range(0, _geigerClicks.Length)];
            _audioSource.PlayOneShot(clip);
        }

        private void CalculateNextClick()
        {
            float delay = Mathf.Lerp(_maxDelay, _minDelay, _radiationLevel / _maxRadiation);
            _nextClickTime = Time.time + delay;
        }
    }
}