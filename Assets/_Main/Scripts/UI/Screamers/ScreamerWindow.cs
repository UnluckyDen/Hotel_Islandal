using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Screamers
{
    public class ScreamerWindow : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private AnimationCurve _scaleCurve;
        [SerializeField] private Image _screamerImage;
        
        public void Init()
        {
            StartCoroutine(ScreamerCoroutine());
        }

        private IEnumerator ScreamerCoroutine()
        {

            float factor = 0f;

            while (1f > factor)
            {
                factor += Time.deltaTime / _lifeTime;
                _screamerImage.transform.localScale = new Vector3(1,1,1) * _scaleCurve.Evaluate(factor);
                yield return null;
            }
            
            DestroySelf();
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}