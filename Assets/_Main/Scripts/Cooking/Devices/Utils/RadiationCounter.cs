using System.Collections;
using _Main.Scripts.Environment.Systems.Radiation;
using _Main.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts.Cooking.Devices.Utils
{
    public class RadiationCounter : Device, IMovableObject
    {
          [SerializeField] private Collider _collider;
        [Space]
        [SerializeField] private Transform _arrow;
        [SerializeField] private Vector3 _maxAngle;
        [SerializeField] private Vector3 _minAngle;
        [Space] 
        [SerializeField] private float _accuracy;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _minValue;
        [SerializeField] private float _smoothTime;
        [SerializeField] private float _maxSmoothTimeSpeed;
        
        [SerializeField] private RadiationOverlapDetector _radiationOverlapDetector;

        private float _currentDisplayedTemperature;
        
        float _smoothDampVelocity = 0.0f; 
        
        public bool IsTrashable => false;

        private void Start()
        {
            _radiationOverlapDetector.Init(FindAnyObjectByType<RadiationController>());
            SetCurrentTemperature(_radiationOverlapDetector.GetCurrentRadiation());
        }

        private void Update()
        {
            float currentTemperature = _radiationOverlapDetector.GetCurrentRadiation() + Random.Range(-_accuracy, _accuracy);
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
            
            Vector3 result = Vector3.LerpUnclamped(_minAngle, _maxAngle, normalized);
            
            _arrow.localEulerAngles = result;
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