using TMPro;
using UnityEngine;

namespace _Main.Scripts.UI.Book.BookPages.PageElements
{
    public class BasePageElement : MonoBehaviour
    {
        [SerializeField] private PageElementType _pageElementType;
        [SerializeField] private TMP_Text _text;

        public void Init(string elementText)
        {
            BuildPage(elementText);
        }

        public void Destruct()
        {
            
        }

        protected virtual void BuildPage(string text)
        {
            _text.text = text;
        }
    }
}