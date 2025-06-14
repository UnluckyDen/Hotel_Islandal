using System;
using UnityEngine;

namespace _Main.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SimpleAudioZone : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _volume;

        private void Start()
        {
            _audioSource.volume = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player.Player>() != null)
                _audioSource.volume = _volume;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player.Player>() != null)
                _audioSource.volume = 0;
        }
    }
}