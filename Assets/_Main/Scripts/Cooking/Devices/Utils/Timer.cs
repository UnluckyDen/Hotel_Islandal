using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Devices.Cooking;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Utils
{
    public class Timer : Device
    {
        [SerializeField] private Transform _mainArrow;
        [SerializeField] private List<Transform> _deviceArrows;
        [SerializeField] private List<EnablingCookingDevice> _trackingDevices;
        [Space]
        [SerializeField] private int _segmentsCount;
        [SerializeField] private float _interval = 1f;
        [Space]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private float _anglePerSegment = 0f;
        private float _timer = 0f;

        private HashSet<Transform> _freeArrows = new();
        private Dictionary<EnablingCookingDevice, Transform> _trackingArrows = new();
        
        private void Start()
        {
            _anglePerSegment = 360f / _segmentsCount;

            foreach (var deviceArrow in _deviceArrows)
                _freeArrows.Add(deviceArrow);
            
            foreach (var deviceArrow in _deviceArrows)
                deviceArrow.gameObject.SetActive(false);

            foreach (var trackingDevice in _trackingDevices)
                trackingDevice.DeviceActivation += CookingDeviceOnDeviceActivation;
        }

        private void OnDestroy()
        {
            foreach (var trackingDevice in _trackingDevices)
                trackingDevice.DeviceActivation -= CookingDeviceOnDeviceActivation;
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

        private void CookingDeviceOnDeviceActivation(EnablingCookingDevice device, bool active)
        {
            Transform arrowForTrack = null;

            if (active)
            {
                if (_freeArrows.Count > 0)
                {
                    arrowForTrack = _freeArrows.First();
                    _freeArrows.Remove(arrowForTrack);
                }
                else
                {
                    KeyValuePair<EnablingCookingDevice, Transform> trackingPair = _trackingArrows.First();
                    _trackingArrows.Remove(trackingPair.Key);
                    arrowForTrack = trackingPair.Value;
                }

                arrowForTrack.gameObject.SetActive(active);
                arrowForTrack.localRotation = _mainArrow.localRotation;
                _trackingArrows.Add(device, arrowForTrack);
                return;
            }

            if (_trackingArrows.ContainsKey(device))
            {
                arrowForTrack = _trackingArrows[device];
                _trackingArrows.Remove(device);
                _freeArrows.Add(arrowForTrack);
                arrowForTrack.gameObject.SetActive(false);
            }
        }
    }
}