using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.Screamers
{
    public class ScreamerObject : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;

        public void Scream()
        {
            _audioSource.Play();
            _animator.SetTrigger("Scream");
        }
    }
}