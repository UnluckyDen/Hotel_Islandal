using TMPro;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors
{
    public class DoorSign : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberText;
        private int _number;

        public void SetNumber(int number)
        {
            _number = number;
            _numberText.text = _number.ToString();
        }
    }
}