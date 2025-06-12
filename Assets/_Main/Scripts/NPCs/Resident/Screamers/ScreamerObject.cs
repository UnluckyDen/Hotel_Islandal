using _Main.Scripts.Utils.GlobalEvents.Events;
using UnityEngine;
using EventProvider = _Main.Scripts.Utils.GlobalEvents.EventProvider;

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
            EventProvider.Instance.Invoke(new ScreamerShowedEvent(25));
        }
    }
}