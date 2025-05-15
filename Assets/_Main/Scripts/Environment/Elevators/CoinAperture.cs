using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main.Scripts.Environment.Elevators
{
    public class CoinAperture : MonoBehaviour
    {
        public event Action<bool> StateChanged;
        [SerializeField] private List<Transform> _segmentsList;
        [SerializeField] private Vector3 _openRotation;
        [SerializeField] private Vector3 _closeRotation;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Collider _collider;

        private Coroutine _coroutine;

        private void Start() =>
            Open();

        [ContextMenu("Open")]
        public void Open()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(OpenCoroutine());
        }


        [ContextMenu("Close")]
        public void Close()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(CloseCoroutine());
        }

        private IEnumerator OpenCoroutine()
        {
            float factor = 0f;
            _collider.enabled = false;

            Vector3 startRotation = _segmentsList.First().localEulerAngles;

            while (factor < 1f)
            {
                foreach (var segment in _segmentsList)
                    segment.localEulerAngles = Vector3.Lerp(startRotation, _openRotation, factor);

                factor += Time.deltaTime * _moveSpeed;
                yield return null;
            }

            foreach (var segment in _segmentsList)
                segment.localEulerAngles = _openRotation;

            _coroutine = null;
            StateChanged?.Invoke(true);
        }

        private IEnumerator CloseCoroutine()
        {
            float factor = 0f;
            _collider.enabled = true;

            Vector3 startRotation = _segmentsList.First().localEulerAngles;

            while (factor < 1f)
            {
                foreach (var segment in _segmentsList)
                    segment.localEulerAngles = Vector3.Lerp(startRotation, _closeRotation, factor);

                factor += Time.deltaTime * _moveSpeed;
                yield return null;
            }

            foreach (var segment in _segmentsList)
                segment.localEulerAngles = _closeRotation;

            _coroutine = null;
            StateChanged?.Invoke(false);
        }
    }
}