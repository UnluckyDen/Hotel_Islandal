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
        [SerializeField] private ScreamerObject _screamerPrefab;

        private Coroutine _screamCoroutine;
        
        public void Scream()
        {
            _screamCoroutine = StartCoroutine(ScreamerCoroutine());
        }

        private IEnumerator ScreamerCoroutine()
        {
            var screamerObject = Instantiate(_screamerPrefab, _screamerRoot.position, _screamerRoot.rotation, _screamerRoot);
            PostprocessingController.Instance.ToScream(_toScream);
            yield return new WaitForSeconds(_toScream);

            screamerObject.Scream();
            yield return new WaitForSeconds(_wait);
            
            PostprocessingController.Instance.ToNormal(_toNormal);
            yield return new WaitForSeconds(_toNormal);
            Destroy(screamerObject.gameObject);
        }
    }
}