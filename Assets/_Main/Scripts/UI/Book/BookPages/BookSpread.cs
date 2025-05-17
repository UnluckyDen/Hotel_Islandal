using _Main.Scripts.ScriptableObjects.Book;
using UnityEngine;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class BookSpread : MonoBehaviour
    {
        [SerializeField] private DefaultBookPage _defaultBookPageLeft;
        [SerializeField] private DefaultBookPage _defaultBookPageRight;

        public DefaultBookPage BookPageLeft => _defaultBookPageLeft;
        public DefaultBookPage BookPageRight => _defaultBookPageRight;

        public void PagesInit(JournalPageSettings pageSettingsLeft, JournalPageSettings pageSettingsRight)
        {
            _defaultBookPageLeft.Init(pageSettingsLeft);
            _defaultBookPageRight.Init(pageSettingsRight);
        }

        public void PagesDestruct()
        {
            _defaultBookPageLeft.Destruct();
            _defaultBookPageRight.Destruct();
        }

        public void UpdatePages(JournalPageSettings pageSettingsLeft, JournalPageSettings pageSettingsRight)
        {
            PagesDestruct();
            PagesInit(pageSettingsLeft, pageSettingsRight);
        }
    }
}