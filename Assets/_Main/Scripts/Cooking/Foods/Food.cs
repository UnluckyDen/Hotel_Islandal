using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Foods
{
    public class Food : MonoBehaviour, IMovableObject
    {
        [SerializeField] private Collider _collider;
        
        public virtual void ToNonInteractive()
        {
            _collider.enabled = false;
        }

        public virtual void ToInteractable()
        {
            _collider.enabled = true;
        }

        public void OnHoverEnter()
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }

        public void OnHoverExit()
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}