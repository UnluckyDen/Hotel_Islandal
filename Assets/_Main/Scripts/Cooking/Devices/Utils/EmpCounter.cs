using System.Collections;
using _Main.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts.Cooking.Devices.Utils
{
    public class EmpCounter : Device, IMovableObject
    {
        [SerializeField] private Collider _collider;
        [Space]
        [SerializeField] private Transform _arrow;
        [SerializeField] private Vector3 _angleStart;
        [SerializeField] private Vector3 _angleEnd;
        [Space] 
        [SerializeField] private float _updateTime;

        private Coroutine _arrowCoroutine;
        
        public bool IsTrashable => false;
        
        private IEnumerator UpdateCoroutine()
        {
            float factor = 0;

            Vector3 startAngle = _arrow.localEulerAngles;
            Vector3 endAngle = new Vector3(0, 0, Random.Range(_angleStart.z, _angleEnd.z));

            while (factor < 1)
            {
                _arrow.localEulerAngles = Vector3.Lerp(startAngle, endAngle, factor);
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