using System;
using _Main.Scripts.Analytics;
using _Main.Scripts.Services;
using _Main.Scripts.UI.Book;
using _Main.Scripts.UI.Screamers;
using UnityEngine;

namespace _Main.Scripts.UI
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private PlayerBookWindow _playerBookWindow;
        [SerializeField] private ScreamerWindow _screamerWindow;

        [SerializeField] private Transform _content;

        public static WindowController Instance { get; private set; }

        private InputService _inputService;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

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

        public void ShowScreamerWindow()
        {
            var a = Instantiate(_screamerWindow, _content);
            a.Init();
        }

        private void InputServiceOnOpenBook()
        {
            _playerBookWindow.gameObject.SetActive(!_playerBookWindow.gameObject.active);
            _inputService.ToUiMode(_playerBookWindow.gameObject.active);
            
            GlobalAnalyticsService.Instance.SendCustomEvent(new PlayerOpenJournalEvent(_playerBookWindow.gameObject.active));
        }
    }
}
