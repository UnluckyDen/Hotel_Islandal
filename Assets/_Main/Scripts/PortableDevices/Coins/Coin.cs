using System.Collections;
using _Main.Scripts.Cooking.Devices;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.PortableDevices.Coins
{
    public class Coin : Device, IMovableObject
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private AnimationCurve _flyCurve;
        [SerializeField] private float _flyDuration = 1f;
        [SerializeField] private float _arcHeight = 2f;
        [SerializeField] Vector3 _rotationAxis = Vector3.forward;
        [SerializeField] float _rotationSpeed = 720f;
        [SerializeField] private AudioSource _audioSource;
        
        private Coroutine _flyCoroutine;
        private CoinStash _coinStash;
        
        public bool IsTrashable => false;

        public void ToNonInteractive()
        {
            _collider.enabled = false;
        }

        public void ToInteractable()
        {
            _collider.enabled = true;
        }

        public void FlyToStash(CoinStash coinStash)
        {
            if (_flyCoroutine != null)
                StopCoroutine(_flyCoroutine);

            _coinStash = coinStash;
            _flyCoroutine = StartCoroutine(FlyToStashCoroutine());
        }

        private IEnumerator FlyToStashCoroutine()
        {
            ToNonInteractive();
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;
            float elapsedTime = 0f;
            _audioSource.Play();

            while (elapsedTime < _flyDuration)
            {
                float normalizedTime = elapsedTime / _flyDuration;
    
                Vector3 currentTarget = _coinStash.transform.position;
    
                Vector3 horizontalPosition = Vector3.Lerp(startPosition, currentTarget, normalizedTime);
        
                float curveValue = _flyCurve.Evaluate(normalizedTime);
                float verticalOffset = curveValue * _arcHeight;
    
                transform.position = horizontalPosition + Vector3.up * verticalOffset;
        
                float rotationAngle = _rotationSpeed * elapsedTime;
                transform.rotation = startRotation * Quaternion.AngleAxis(rotationAngle, _rotationAxis);
    
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _coinStash.PlaceMovableObject(this);
        }
    }
}