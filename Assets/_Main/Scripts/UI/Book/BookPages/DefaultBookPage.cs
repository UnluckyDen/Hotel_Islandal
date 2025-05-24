using System.Collections.Generic;
using _Main.Scripts.ScriptableObjects.Book;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class DefaultBookPage : MonoBehaviour
    {
        [SerializeField] private RectTransform _pageContent; 
            
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _body;

        private JournalPageSettings _journalPageSettings;

        private List<GameObject> _elements = new();
        
        public void Init(JournalPageSettings journalPageSettings)
        {
            _journalPageSettings = journalPageSettings;
            InstantiateContent();
        }

        public void Destruct()
        {
            foreach (var gameObjectElement in _elements)
                Destroy(gameObjectElement.gameObject);
            
            _elements.Clear();
        }

        private void InstantiateContent()
        {
            if (_journalPageSettings == null)
                return;
            
            if (_journalPageSettings.HaveHeader)
            {
                TMP_Text header = Instantiate(_header, _pageContent);
                _elements.Add(header.gameObject);
                header.text = _journalPageSettings.Header;
            }

            foreach (var bodyString in _journalPageSettings.BodyContent)
            {
                TMP_Text body = Instantiate(_body, _pageContent);
                _elements.Add(body.gameObject);
                body.text = bodyString;
            }

            _pageContent.ForceUpdateRectTransforms();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_pageContent);
        }
    }
}