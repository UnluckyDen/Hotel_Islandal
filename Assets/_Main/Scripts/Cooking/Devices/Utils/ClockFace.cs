using System;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Utils
{
    public class ClockFace : Device, IInteractable
    {
        [SerializeField] private Transform _mainArrow;
        [SerializeField] private Transform _notchArrow;
        [SerializeField] private int _segmentsCount;
        [SerializeField] private float _interval = 1f;
        [Space]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private float _anglePerSegment = 0f;
        private float _timer = 0f;
        
        private void Start()
        {
            _anglePerSegment = 360f / _segmentsCount;
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < _interval)
                return;
            
            UpdateArrow();
            _timer -= _interval;
        }

        private void UpdateArrow()
        {
            float angle = DateTime.Now.Second % _segmentsCount * _anglePerSegment;
            _mainArrow.localRotation = Quaternion.Euler(0f, angle, 0f);
            _audioSource.PlayOneShot(_audioClip);
        }
        
        public void OnClick() =>
            _notchArrow.localRotation = _mainArrow.localRotation;
    }
}