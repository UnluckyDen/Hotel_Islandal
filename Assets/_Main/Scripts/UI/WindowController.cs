using System;
using _Main.Scripts.Services;
using _Main.Scripts.UI.Book;
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
            
            _playerBookWindow.Init();
        }

        private void OnDestroy()
        {
            _inputService.OpenBook -= InputServiceOnOpenBook;
            
            _playerBookWindow.Destruct();
        }

        private void InputServiceOnOpenBook()
        {
            _playerBookWindow.gameObject.SetActive(!_playerBookWindow.gameObject.active);
            _inputService.ToUiMode(_playerBookWindow.gameObject.active);
        }
    }
}
