using _Main.Scripts.Analytics;
using _Main.Scripts.Services;
using _Main.Scripts.UI.Book;
using _Main.Scripts.Utils.GlobalEvents.Events;
using UnityEngine;
using EventProvider = _Main.Scripts.Utils.GlobalEvents.EventProvider;

namespace _Main.Scripts.UI
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private PlayerBookWindow _playerBookWindow;

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
            if (_inputService != null)
                _inputService.OpenBook -= InputServiceOnOpenBook;
            
            _playerBookWindow.Destruct();
        }

        private void InputServiceOnOpenBook()
        {
            _playerBookWindow.gameObject.SetActive(!_playerBookWindow.gameObject.active);
            
            EventProvider.Instance.Invoke(new BookOpenedEvent(_playerBookWindow.gameObject.active));
            GlobalAnalyticsService.Instance.SendCustomEvent(new PlayerOpenJournalEvent(_playerBookWindow.gameObject.active));
        }
    }
}
