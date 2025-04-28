using System;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.UI
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private PlayerBookWindow _playerBookWindow;

        private InputService _inputService;

        private void Start()
        {
            _inputService = InputService.Instance;
            _inputService.OpenBook += InputServiceOnOpenBook;
            _playerBookWindow.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _inputService.OpenBook -= InputServiceOnOpenBook;
        }

        private void InputServiceOnOpenBook()
        {
            _playerBookWindow.gameObject.SetActive(!_playerBookWindow.gameObject.active);
        }
    }
}
