using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.Tutorial
{
    public class BaseTutorialHint : MonoBehaviour
    {
        [SerializeField] private float _hintTime;
        [SerializeField] private Slider _slider;
        [SerializeField] private GameObject _hintGameObject;

        private bool _hintShowed;
        private Coroutine _showCoroutine;

        private void Awake()
        { 
            HideHint();
        }

        [ContextMenu("Show hint")]
        public void ShowHint()
        {
            if (_hintShowed || _showCoroutine != null)
                return;

            _showCoroutine = StartCoroutine(ShowCoroutine());
            _hintShowed = true;
        }

        [ContextMenu("Hide hint")]
        public void HideHint()
        {
            if (_showCoroutine != null)
                StopCoroutine(_showCoroutine);
            
            _hintGameObject.SetActive(false);
            _showCoroutine = null;
        }

        private IEnumerator ShowCoroutine()
        {
            _hintGameObject.SetActive(true);

            float factor = 0;

            while (factor < 1)
            {
                factor += Time.deltaTime / _hintTime;
                _slider.value = factor;
                yield return null;
            }
            
            _hintGameObject.SetActive(false);
        }

    }
}