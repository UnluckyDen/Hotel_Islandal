using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book.BookPages
{
    [RequireComponent(typeof(Button))]
    public class CaptureButton : MonoBehaviour
    {
        public event Action<int> CaptureButtonClicked;
        [SerializeField] private Button _button;
        [SerializeField] private int _captureStartPage;

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonPressed);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonPressed);

        private void OnButtonPressed() =>
            CaptureButtonClicked?.Invoke(_captureStartPage);

        private void OnValidate()
        {
            if (_button == null)
                _button = GetComponent<Button>();
        }
    }
}