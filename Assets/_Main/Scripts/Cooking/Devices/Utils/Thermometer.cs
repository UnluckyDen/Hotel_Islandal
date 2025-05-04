using _Main.Scripts.Environment.Systems.Temperature;
using _Main.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts.Cooking.Devices.Utils
{
    public class Thermometer : Device, IMovableObject
    {
        [SerializeField] private Collider _collider;
        [Space]
        [SerializeField] private Transform _bar;
        [SerializeField] private Vector3 _maxScale;
        [SerializeField] private Vector3 _minScale;
        [Space] 
        [SerializeField] private float _accuracy;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _minValue;
        [SerializeField] private float _smoothTime;
        [SerializeField] private float _maxSmoothTimeSpeed;
        
        [SerializeField] private TemperatureOverlapDetector _temperatureOverlapDetector;

        private float _currentDisplayedTemperature;
        
        float _smoothDampVelocity = 0.0f; 
        
        public bool IsTrashable => false;

        private void Start()
        {
            _temperatureOverlapDetector.Init(FindAnyObjectByType<TemperatureController>());
            SetCurrentTemperature(_temperatureOverlapDetector.GetCurrentTemperature());
        }

        private void Update()
        {
            float currentTemperature = _temperatureOverlapDetector.GetCurrentTemperature() + Random.Range(-_accuracy, _accuracy);
            float smoothedTemperature = Mathf.SmoothDamp(_currentDisplayedTemperature, 
                currentTemperature,
                ref _smoothDampVelocity, 
                _smoothTime,
                _maxSmoothTimeSpeed);
            
            SetCurrentTemperature(smoothedTemperature);
        }

        private void SetCurrentTemperature(float currentTemperature)
        {
            _currentDisplayedTemperature = currentTemperature;
            
            float normalized = (currentTemperature - _minValue) / (_maxValue - _minValue);
            
            Vector3 result = Vector3.LerpUnclamped(_minScale, _maxScale, normalized);
            
            _bar.localScale = result;
        }
        
            
        public void ToNonInteractive()
        {
            _collider.enabled = false;
        }

        public void ToInteractable()
        {
            _collider.enabled = true;
        }
    }
}