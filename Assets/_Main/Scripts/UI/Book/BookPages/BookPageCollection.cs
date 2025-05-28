using System.Collections.Generic;
using _Main.Scripts.ScriptableObjects.Book;
using UnityEngine;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class BookPageCollection : MonoBehaviour
    {
        [SerializeField] private JournalCaptureSettings _welcomeJournalCapture;
        [SerializeField] private JournalCaptureSettings _languageJournalCapture;
        [SerializeField] private JournalCaptureSettings _recipeJournalCapture;


        private List<JournalPageSettings> _bookPages = new();

        private int _maxSpreads;
        private int _currentSpread = 0;

        public bool HasRightPages => _maxSpreads > _currentSpread + 1;
        public bool HasLeftPages => _currentSpread > 0;

        public void Init()
        {
            _bookPages.AddRange(_welcomeJournalCapture.JournalPageSettings);
            _bookPages.AddRange(_languageJournalCapture.JournalPageSettings);
            _bookPages.AddRange(_recipeJournalCapture.JournalPageSettings);

            _maxSpreads = GetBookSpreadCount();
        }

        public void Destruct()
        {

        }

        public (JournalPageSettings, JournalPageSettings) GetCurrentPages()
        {
            var pageNumbers = GetPageNumbersBySpread(_currentSpread);
            var pageLeft = _currentSpread >= 0 ? _bookPages[pageNumbers.Item1] : null;
            var pageRight = _bookPages.Count > pageNumbers.Item2 ? _bookPages[pageNumbers.Item2] : null;
            return (pageLeft, pageRight);
        }

        public void NextSpread()
        {
            if (_maxSpreads > _currentSpread)
                _currentSpread++;
        }

        public void PreviousSpread()
        {
            if (_currentSpread > 0)
                _currentSpread--;
        }

        public void ForceJumpToPagePage(int pageNumber) =>
            _currentSpread = GetSpreadByPageNumbers(pageNumber);

        private (int, int) GetPageNumbersBySpread(int spread) =>
            (spread * 2, spread * 2 + 1);
        
        private int GetSpreadByPageNumbers(int pageNumber) =>
            pageNumber / 2;

        private int GetBookSpreadCount() =>
            (int)((float)_bookPages.Count / 2 + 0.5f);
    }
}