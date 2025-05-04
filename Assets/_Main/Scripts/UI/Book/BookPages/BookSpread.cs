using UnityEngine;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class BookSpread : MonoBehaviour
    {
        [SerializeField] private BookPage _bookPageLeft;
        [SerializeField] private BookPage _bookPageRight;

        public BookPage BookPageLeft => _bookPageLeft;
        public BookPage BookPageRight => _bookPageRight;

        public void Init()
        {
            _bookPageLeft.Init();
            _bookPageRight.Init();
        }

        public void Destruct()
        {
            _bookPageLeft.Destruct();
            _bookPageRight.Destruct();
        }
    }
}