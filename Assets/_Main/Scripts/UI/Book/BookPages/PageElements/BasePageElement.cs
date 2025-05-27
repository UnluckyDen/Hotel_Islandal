using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book.BookPages.PageElements
{
    public class BasePageElement : MonoBehaviour
    {
        [SerializeField] private PageElementType _pageElementType;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;
        [SerializeField] protected AudioSource _audioSource;

        public void Init(string elementText, Sprite sprite, AudioClip audioClip)
        {
            SetText(elementText);
            SetImage(sprite);
            SetAudio(audioClip);
        }

        public void Destruct()
        {
            
        }

        public virtual void SetText(string text)
        {
            if (_text != null)
                _text.text = text;
        }

        public virtual void SetImage(Sprite sprite)
        {
            if (_image != null)
                _image.sprite = sprite;
        }

        public virtual void SetAudio(AudioClip audioClip)
        {
            if (_audioSource != null)
                _audioSource.clip = audioClip;
        }
    }
}