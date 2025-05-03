using UnityEngine;

namespace _Main.Scripts.Player.Movement
{
    public class MovementSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _moveAudioClip;
        [SerializeField] private AudioClip _turnClip;

        [SerializeField] private float _fadeDuration;
        
        public void PlayMoveSound()
        {
            if (_audioSource.isPlaying)
                return;
            
            _audioSource.loop = true;
            _audioSource.clip = _moveAudioClip;
            _audioSource.Play();
        }

        public void StopPlayMoveSound()
        {
            if (!_audioSource.isPlaying)
                return;

            _audioSource.loop = false;
            _audioSource.Stop();
        }

        public void PlayTurnSound()
        {
            _audioSource.PlayOneShot(_turnClip);
        }
    }
}