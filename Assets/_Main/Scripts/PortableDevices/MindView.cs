using System.Collections;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.PortableDevices
{
    public class MindView : MonoBehaviour
    {
        [SerializeField] private int _maxHp = 100;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Transform _textRoot;

        [Space] 
        [SerializeField] private AnimationCurve _scaleCurve;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private float _changeSpeed;

        [SerializeField] private int _currentMind;
        [SerializeField] private int _previousMind;

        private Coroutine _changeMindCoroutine;

        public void SetMindCount(int mind)
        {
            _previousMind = _currentMind;
            _currentMind = mind;
            
            UpdateContent();
        }
        
        [ContextMenu("UpdateContent")]
        private void UpdateContent()
        {
            if (_changeMindCoroutine != null) 
                StopCoroutine(_changeMindCoroutine);
            
            _changeMindCoroutine = StartCoroutine(ChangeMindValue());
        }

        private IEnumerator ChangeMindValue()
        {
            int delta = _currentMind - _previousMind;
            int tempMind = _previousMind;

            while (tempMind != _currentMind)
            {
                float factor = 0f;

                while (factor < 1)
                {
                    factor += Time.deltaTime * _changeSpeed;
                    _textRoot.transform.localScale = _scaleCurve.Evaluate(factor) * Vector3.one;
                    _count.color = _gradient.Evaluate(tempMind / (float)_maxHp);
                    yield return null;
                }

                tempMind += (int) Mathf.Sign(delta);
                _count.text = tempMind.ToString();
            }

            _textRoot.transform.localScale = Vector3.one;
            _count.text = _currentMind.ToString();
            _count.color = _gradient.Evaluate(_currentMind / (float)_maxHp);

            _changeMindCoroutine = null;
        }
    }
}