using _Main.Scripts.Cooking.Devices;
using _Main.Scripts.Environment.Systems.Radiation;
using _Main.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts.PortableDevices
{
    public class RadiationCounter : Device, IMovableObject, IActivatingObject
    {
        [SerializeField] private Collider _collider;
        [Space] 
        [SerializeField] private GameObject _activeLamp;
        [SerializeField] private GameObject _inactiveLamp;
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

        [Space] 
        [SerializeField] private RadiationCounterSound _radiationCounterSound;
        [SerializeField] private RadiationOverlapDetector _radiationOverlapDetector;

        private float _currentDisplayedTemperature;
        
        private float _smoothDampVelocity = 0.0f;
        private bool _countingActive;
        
        public bool IsTrashable => false;
        public bool CurrentActivity => _countingActive;

        private void Start()
        {
            _radiationOverlapDetector.Init(FindAnyObjectByType<RadiationController>());
            SetCurrentRadiation(_radiationOverlapDetector.GetCurrentRadiation());
            Deactivate();
        }

        private void Update()
        {
            if (!_countingActive && Mathf.Approximately(_currentDisplayedTemperature, 1f))
                return;
            
            float smoothedRadiation =
                Mathf.SmoothDamp(_currentDisplayedTemperature,
                1,
                ref _smoothDampVelocity,
                _smoothTime,
                _maxSmoothTimeSpeed);
            
            if (_countingActive)
            {
                float currentRadiation = _radiationOverlapDetector.GetCurrentRadiation() +
                                           Random.Range(-_accuracy, _accuracy); 
                smoothedRadiation = Mathf.SmoothDamp(_currentDisplayedTemperature,
                    currentRadiation,
                    ref _smoothDampVelocity,
                    _smoothTime,
                    _maxSmoothTimeSpeed);
            }

            SetCurrentRadiation(smoothedRadiation);
        }

        private void SetCurrentRadiation(float currentRadiation)
        {
            _radiationCounterSound.SetRadiationLevel(currentRadiation);
            _currentDisplayedTemperature = currentRadiation;
            
            float normalized = (currentRadiation - _minValue) / (_maxValue - _minValue);
            
            Vector3 result = Vector3.LerpUnclamped(_minAngle, _maxAngle, normalized);
            
            _arrow.localEulerAngles = result;
        }

        public void SwitchActive()
        {
            if (!_countingActive)
                Activate();
            else
                Deactivate();
        }

        public void Activate()
        {
            _countingActive = true;
            _activeLamp.SetActive(_countingActive);
            _inactiveLamp.SetActive(!_countingActive);
            _radiationCounterSound.Activate();
        }

        public void Deactivate()
        {
            _countingActive = false;
            _activeLamp.SetActive(_countingActive);
            _inactiveLamp.SetActive(!_countingActive);
            _radiationCounterSound.Deactivate();
        }

        public void ToNonInteractive()
        {
            _collider.enabled = false;
        }

        public void ToInteractable()
        {
            Deactivate();
            _collider.enabled = true;
        }
    }
}