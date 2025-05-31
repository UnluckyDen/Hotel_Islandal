using System.Collections;
using _Main.Scripts.Infrastructure.Postprocessing;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.Screamers
{
    public class ResidentScreamer : MonoBehaviour
    {
        [SerializeField] private float _toScream = 0.5f;
        [SerializeField] private float _wait = 1f;
        [SerializeField] private float _toNormal = 0.5f;
        [Space] 
        [SerializeField] private Transform _screamerRoot;
        [SerializeField] private Transform _lookRoot;
        [SerializeField] private ScreamerObject _screamerPrefab;

        private Coroutine _screamCoroutine;
        private Player.Player _player;
        
        public void Scream(Player.Player player)
        {
            _player = player;
            _screamCoroutine = StartCoroutine(ScreamerCoroutine());
        }

        private IEnumerator ScreamerCoroutine()
        {
            _player.Pause();
            var screamerObject = Instantiate(_screamerPrefab, _screamerRoot.position, _screamerRoot.rotation, _screamerRoot);
            
            _player.LockCameraAtObject(true, _lookRoot);
            
            PostprocessingController.Instance.ToScream(_toScream);
            yield return new WaitForSeconds(_toScream);

            screamerObject.Scream();
            yield return new WaitForSeconds(_wait);
            
            PostprocessingController.Instance.ToNormal(_toNormal);
            yield return new WaitForSeconds(_toNormal);
            
            _player.LockCameraAtObject(false, _lookRoot);
            
            Destroy(screamerObject.gameObject);
            _player.UnPause();
        }
    }
}