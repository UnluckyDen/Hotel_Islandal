using System.Collections.Generic;
using _Main.Scripts.ScriptableObjects.Book;
using _Main.Scripts.UI.Book.BookPages.PageElements;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book.BookPages
{
    public class DefaultBookPage : MonoBehaviour
    {
        [SerializeField] private RectTransform _pageContent;
        [SerializeField] private SerializedDictionary<PageElementType, BasePageElement> _pageElements;

        private JournalPageSettings _journalPageSettings;

        private List<BasePageElement> _elements = new();
        
        public void Init(JournalPageSettings journalPageSettings)
        {
            _journalPageSettings = journalPageSettings;
            InstantiateContent();
        }

        public void Destruct()
        {
            foreach (var pageElement in _elements)
            {
                pageElement.Destruct();
                Destroy(pageElement.gameObject);
            }

            _elements.Clear();
        }

        private void InstantiateContent()
        {
            if (_journalPageSettings == null)
                return;

            foreach (var contentElement in _journalPageSettings.PageContentElementSettingsList)
            {
                BasePageElement basePageElement = Instantiate(_pageElements[contentElement.PageElementType], _pageContent);
                basePageElement.Init(contentElement.TextContent);
                _elements.Add(basePageElement);
            }

            _pageContent.ForceUpdateRectTransforms();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_pageContent);
        }
    }
}