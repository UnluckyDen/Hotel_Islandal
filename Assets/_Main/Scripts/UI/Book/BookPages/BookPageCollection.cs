using _Main.Scripts.ScriptableObjects.Book;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class BookPageCollection : MonoBehaviour
    {
        [SerializeField] private JournalCaptureSettings _welcomeJournalCapture;
        [SerializeField] private JournalCaptureSettings _languageJournalCapture;
        [SerializeField] private JournalCaptureSettings _recipeJournalCapture;
        [Space]
        [SerializeField] private Button _welcomeButton;
        [SerializeField] private Button _languageButton;
        [SerializeField] private Button _recipePageButton;
        [Space]
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;
        [SerializeField] private BookSpread _bookSpread;

        private int _currentSpread = 0;
        private int _maxSpreads;

        public void Init()
        {
            _maxSpreads = 0;
            _maxSpreads += _welcomeJournalCapture.GetBookSpreadCount();
            _maxSpreads += _languageJournalCapture.GetBookSpreadCount();
            _maxSpreads += _recipeJournalCapture.GetBookSpreadCount();
            
            _buttonRight.onClick.AddListener(NextSpread);
            _buttonLeft.onClick.AddListener(PreviousSpread);
            _bookSpread.PagesInit(_welcomeJournalCapture.GetFirstSpread().Item1, _welcomeJournalCapture.GetFirstSpread().Item2);
        }

        public void Destruct()
        {
            _buttonRight.onClick.RemoveListener(NextSpread);
            _buttonLeft.onClick.RemoveListener(PreviousSpread);
        }

        private void NextSpread()
        {
            if (_maxSpreads > (_currentSpread + 1))
            {
                _currentSpread++;
            }
        }

        private void PreviousSpread()
        {
            if (_currentSpread > 0)
            {
                _currentSpread--;
            }
        }
    }
}