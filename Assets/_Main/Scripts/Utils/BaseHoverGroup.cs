using UnityEngine;

namespace _Main.Scripts.Utils
{
    public class BaseHoverGroup : MonoBehaviour
    {
        [SerializeField] private bool _startEnabled = false;
        
        private void Start()
        {
            gameObject.SetActive(_startEnabled);
        }

        public virtual void OnHoverEnter()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnHoverExit()
        {
            gameObject.SetActive(false);
        }
    }
}