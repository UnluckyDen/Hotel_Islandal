using _Main.Scripts.UI.Book.BookPages;
using UnityEngine;

namespace _Main.Scripts.UI.Book
{
    public class PlayerBookWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform _bookSpreadContent;
        [Space] 
        [SerializeField] private BookPageCollection _bookPageCollection;

        private void OnEnable() =>
            Cursor.lockState = CursorLockMode.Confined;

        private void OnDisable() =>
            Cursor.lockState = CursorLockMode.Locked;

        public void Init()
        {
            _bookPageCollection.Init();
        }

        public void Destruct()
        {
            _bookPageCollection.Destruct();
        }
        
        // private void BookPageCollectionOnPageUpdated(BookSpread bookSpread)
        // {
        //     if (_currentBookSpread != null)
        //     {
        //         _currentBookSpread.PagesDestruct();
        //         Destroy(_currentBookSpread.gameObject);
        //     }
        //     _currentBookSpread = InstantiateBookSpread(bookSpread);
        // }

        // private BookSpread InstantiateBookSpread(BookSpread bookSpread)
        // {
        //     BookSpread spread = Instantiate(bookSpread, _bookSpreadContent);
        //     spread.PagesInit(_welcomeJournalCapture.GetFirstSpread().Item1,_welcomeJournalCapture.GetFirstSpread().Item2);
        //     return spread;
        // }
    }
}