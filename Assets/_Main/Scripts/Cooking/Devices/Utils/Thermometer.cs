using System.Collections;
using _Main.Scripts.Environment.Systems.Temperature;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Utils
{
    public class Thermometer : Device, IMovableObject
    {
        [SerializeField] private Collider _collider;
        [Space]
        [SerializeField] private Transform _bar;
        [SerializeField] private Vector3 _zeroScale;
        [SerializeField] private Vector3 _maxScale;
        [SerializeField] private Vector3 _minScale;
        [Space] 
        [SerializeField] private float _updateTime;
        [SerializeField] private TemperatureOverlapDetector _temperatureOverlapDetector;

        private Coroutine _arrowCoroutine;
        
        public bool IsTrashable => false;
        
        private IEnumerator UpdateCoroutine()
        {
            float factor = 0;

            Vector3 startScale = _bar.localScale;
            Vector3 endScale = new Vector3(1, Random.Range(_minScale.y, _maxScale.y), 1);

            while (factor < 1)
            {
                _bar.localScale = Vector3.Lerp(startScale, endScale, factor);
                factor += Time.deltaTime / _updateTime;
                yield return null;
            }

            _arrowCoroutine = null;
            StartShowing();
        }

        private void StartShowing()
        {
            _arrowCoroutine = StartCoroutine(UpdateCoroutine());
        }

        private void StopShowing()
        {
            if (_arrowCoroutine != null)
            {
                StopCoroutine(_arrowCoroutine);
                _arrowCoroutine = null;
            }
        }
            
        public void ToNonInteractive()
        {
            _collider.enabled = false;
            StartShowing();
        }

        public void ToInteractable()
        {
            _collider.enabled = true;
            StopShowing();
        }
    }
}