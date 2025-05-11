using _Main.Scripts.Interfaces;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Cooking.Foods
{
    public class Food : MonoBehaviour, IMovableObject, ICookable
    {
        [SerializeField] private Collider _collider;

        [SerializeField] private float _cookingTime;
        [SerializeField] private BaseHoverGroup _hoverGroup;


        public bool IsTrashable => true;
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
            _hoverGroup.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _hoverGroup.OnHoverExit();
        }

        public string GetFoodName() => gameObject.name;
    }
}