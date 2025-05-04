using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class BookPageCollection : MonoBehaviour
    {
        public event Action<BookSpread> PageUpdated; 
            
        [SerializeField] private List<BookSpread> _bookSpreads;
        
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;

        private int _currentSpread = 0;

        public void Init()
        {
            _buttonRight.onClick.AddListener(NextSpread);
            _buttonLeft.onClick.AddListener(PreviousSpread);
        }

        public void Destruct()
        {
            _buttonRight.onClick.RemoveListener(NextSpread);
            _buttonLeft.onClick.RemoveListener(PreviousSpread);
        }

        public BookSpread GetCurrentBookPage() => 
            _bookSpreads[_currentSpread];

        private void NextSpread()
        {
            if (_bookSpreads.Count > (_currentSpread + 1))
            {
                _currentSpread++;
                PageUpdated?.Invoke(GetCurrentBookPage());
            }
        }

        private void PreviousSpread()
        {
            if (_currentSpread > 0)
            {
                _currentSpread--;
                PageUpdated?.Invoke(GetCurrentBookPage());
            }
        }
    }
}