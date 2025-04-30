using UnityEngine;

namespace _Main.Scripts.Utils
{
    public class HoverGroup : MonoBehaviour
    {
        [SerializeField] private bool _startEnabled = false;
        
        private void Start()
        {
            gameObject.SetActive(_startEnabled);
        }

        public void OnHoverEnter()
        {
            gameObject.SetActive(true);
        }

        public void OnHoverExit()
        {
            gameObject.SetActive(false);
        }
    }
}