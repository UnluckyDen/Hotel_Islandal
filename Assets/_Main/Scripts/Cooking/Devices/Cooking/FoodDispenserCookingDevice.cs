using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class FoodDispenserCookingDevice : CookingDevice
    {
        [SerializeField] private Food _foodPrefab;
        [SerializeField] private Transform _foodTransform;

        public override bool IsEmpty => false;

        public override IMovableObject TakeMovableObject()
        {
            var food = Instantiate(_foodPrefab, _foodTransform);
            food.transform.SetParent(null);
            food.ToInteractable();

            return food;
        }
    }
}