using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI.Book.BookPages.PageElements
{
    public class AudioSamplePageElement : BasePageElement
    {
        [SerializeField] private Button _playSoundButton;

        private void OnEnable()
        {
            _playSoundButton.onClick.AddListener(PlaySound);
        }

        private void OnDisable()
        {
            _playSoundButton.onClick.RemoveListener(PlaySound);
        }

        private void PlaySound()
        {
            _audioSource.Play();
        }
    }
}