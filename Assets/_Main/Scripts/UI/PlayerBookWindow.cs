using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI
{
    public class PlayerBookWindow : MonoBehaviour
    {
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;
        
        [SerializeField] private BookPage _bookPageL;
        [SerializeField] private BookPage _bookPageR;
    }
}