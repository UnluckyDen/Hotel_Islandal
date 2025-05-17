using _Main.Scripts.ScriptableObjects.Book;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class DefaultBookPage : MonoBehaviour
    {
        [SerializeField] private RectTransform _pageContent; 
            
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _body;

        private JournalPageSettings _journalPageSettings;
        
        public void Init(JournalPageSettings journalPageSettings)
        {
            _journalPageSettings = journalPageSettings;
            InstantiateContent();
        }

        public void Destruct()
        {
            
        }

        private void InstantiateContent()
        {
            if (_journalPageSettings.HaveHeader)
            {
                TMP_Text header = Instantiate(_header, _pageContent);
                header.text = _journalPageSettings.Header;
            }

            foreach (var bodyString in _journalPageSettings.BodyContent)
            {
                TMP_Text body = Instantiate(_body, _pageContent);
                body.text = bodyString;
            }

            _pageContent.ForceUpdateRectTransforms();
        }
    }
}