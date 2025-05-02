using UnityEngine;

namespace _Main.Scripts.Player.Movement
{
    public class MovementSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _moveAudioClip;

        public void PlayMoveSound()
        {
            if (_audioSource.isPlaying)
                return;
            
            _audioSource.loop = true;
            _audioSource.PlayOneShot(_moveAudioClip);
        }

        public void StopPlayMoveSound()
        {
            _audioSource.loop = false;
            _audioSource.Stop();
        }
    }
}