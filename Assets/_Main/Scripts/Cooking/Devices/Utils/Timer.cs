using System;
using _Main.Scripts.Utils.GameObjectGroups;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Utils
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Transform _mainArrow;
        [SerializeField] private SerializedDictionary<EnablingCookingDevice, Transform> _arrowsTrackingDevices;
        [Space]
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
            
            
            foreach (var deviceArrow in _arrowsTrackingDevices)
                deviceArrow.Value.gameObject.SetActive(false);

            foreach (var trackingDevice in _arrowsTrackingDevices)
                trackingDevice.Key.DeviceActivation += CookingDeviceOnDeviceActivation;
        }

        private void OnDestroy()
        {
            foreach (var trackingDevice in _arrowsTrackingDevices)
                trackingDevice.Key.DeviceActivation -= CookingDeviceOnDeviceActivation;
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
                arrowForTrack = _arrowsTrackingDevices[device];

                arrowForTrack.gameObject.SetActive(active);
                arrowForTrack.localRotation = _mainArrow.localRotation;
                return;
            }
            
            arrowForTrack = _arrowsTrackingDevices[device]; 
            arrowForTrack.gameObject.SetActive(false);
        }
    }
}