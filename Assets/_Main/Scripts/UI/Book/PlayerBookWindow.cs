using System.Collections.Generic;
using _Main.Scripts.Analytics;
using _Main.Scripts.ScriptableObjects.Book;
using _Main.Scripts.Services;
using _Main.Scripts.UI.Book.BookPages;
using _Main.Scripts.Utils.GlobalEvents.Events;
using EventProvider = _Main.Scripts.Utils.GlobalEvents.EventProvider;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book
{
    public class PlayerBookWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform _bookSpreadContent;

        [Space] 
        [SerializeField] private BookPageCollection _bookPageCollection;
        [SerializeField] private BookSpread _bookSpread;

        [Space] 
        [SerializeField] private List<CaptureButton> _captureButtons;

        [Space]
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;
        [SerializeField] private Button _closeButton;

        private InputService _inputService;
        private bool _opened;

        public bool Opened => _opened;

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Confined;
            
            if (_inputService != null)
                _inputService.MovementInput += InstanceOnMovementInput;
            
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            if (_inputService != null)
                _inputService.MovementInput -= InstanceOnMovementInput;
            
            _closeButton.onClick.RemoveListener(Close);
        }

        public void Init()
        {
            _inputService = InputService.Instance;
            
            _bookPageCollection.Init();
            
            _buttonRight.onClick.AddListener(NextPageClicked);
            _buttonLeft.onClick.AddListener(PreviousPageClicked);

            foreach (var captureButton in _captureButtons)
                captureButton.CaptureButtonClicked += CaptureButtonOnCaptureButtonClicked;
            
            UpdatePages();
        }

        public void Destruct()
        {
            _buttonRight.onClick.RemoveListener(NextPageClicked);
            _buttonLeft.onClick.RemoveListener(PreviousPageClicked);
            
            foreach (var captureButton in _captureButtons)
                captureButton.CaptureButtonClicked += CaptureButtonOnCaptureButtonClicked;
            
            _bookPageCollection.Destruct();
        }

        private void InstanceOnMovementInput(Vector2 direction, bool pressed)
        {
            if (pressed && direction.x > 0)
            {
                NextPageClicked();
            }

            if (pressed && direction.x < 0)
            {
                PreviousPageClicked();
            }
        }

        private void CaptureButtonOnCaptureButtonClicked(int pageNumber)
        {
            _bookPageCollection.ForceJumpToPagePage(pageNumber);
            UpdatePages();
        }

        private void NextPageClicked()
        {
            _bookPageCollection.NextSpread();
            UpdatePages();
        }

        private void PreviousPageClicked()
        {
            _bookPageCollection.PreviousSpread();
            UpdatePages();
        }

        private void UpdatePages()
        {
            _buttonRight.gameObject.SetActive(_bookPageCollection.HasRightPages);
            _buttonLeft.gameObject.SetActive(_bookPageCollection.HasLeftPages);

            (JournalPageSettings, JournalPageSettings) pagePair = _bookPageCollection.GetCurrentPages();
            _bookSpread.UpdatePages(pagePair.Item1, pagePair.Item2);
        }

        public void Close()
        {
            _opened = false;
            gameObject.SetActive(_opened);
            EventProvider.Instance.Invoke(new BookOpenedEvent(_opened));
            GlobalAnalyticsService.Instance.SendCustomEvent(new PlayerOpenJournalEvent(_opened));
        }

        public void Open()
        {
            _opened = true;
            gameObject.SetActive(_opened);
            EventProvider.Instance.Invoke(new BookOpenedEvent(_opened));
            GlobalAnalyticsService.Instance.SendCustomEvent(new PlayerOpenJournalEvent(_opened));
        }

        public void ChangeState()
        {
            if (_opened)
                Close();
            else
                Open();
        }
    }
}