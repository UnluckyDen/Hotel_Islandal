using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Foods
{
    public class Food : MonoBehaviour, IMovableObject, ICookable
    {
        [SerializeField] private Collider _collider;

        [SerializeField] private float _cookingTime;

        public float CurrentCookingTime => _cookingTime;

        public void AddCookingTime(float time) => _cookingTime += time;

        public void ToNonInteractive()
        {
            _collider.enabled = false;
        }

        public void ToInteractable()
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