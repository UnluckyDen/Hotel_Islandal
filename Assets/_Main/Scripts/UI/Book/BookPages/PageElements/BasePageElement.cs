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
            SetText(elementText);
        }

        public void Destruct()
        {
            
        }

        public virtual void SetText(string text)
        {
            _text.text = text;
        }
    }
}