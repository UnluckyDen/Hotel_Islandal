using _Main.Scripts.UI.Book.BookPages;
using UnityEngine;

namespace _Main.Scripts.UI.Book
{
    public class PlayerBookWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform _bookSpreadContent;
        [Space] 
        [SerializeField] private BookPageCollection _bookPageCollection;
        [SerializeField] private BookSpread _currentBookSpread;

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Confined;

        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Init()
        {
            _bookPageCollection.Init();
            _bookPageCollection.PageUpdated += BookPageCollectionOnPageUpdated;
            
            if (_currentBookSpread != null)
                _currentBookSpread.Init();

            _currentBookSpread = InstantiateBookSpread(_bookPageCollection.GetCurrentBookPage());
        }

        public void Destruct()
        {
            _bookPageCollection.Destruct();
            _bookPageCollection.PageUpdated -= BookPageCollectionOnPageUpdated;
            
            if (_currentBookSpread != null)
                _currentBookSpread.Destruct();
        }
        
        private void BookPageCollectionOnPageUpdated(BookSpread bookSpread)
        {
            if (_currentBookSpread != null)
            {
                _currentBookSpread.Destruct();
                Destroy(_currentBookSpread.gameObject);
            }
            _currentBookSpread = InstantiateBookSpread(bookSpread);
        }

        private BookSpread InstantiateBookSpread(BookSpread bookSpread)
        {
            BookSpread spread = Instantiate(bookSpread, _bookSpreadContent);
            spread.Init();
            return spread;
        }
    }
}