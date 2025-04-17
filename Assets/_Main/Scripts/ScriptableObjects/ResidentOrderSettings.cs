using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Foods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewResidentOrderSettings", menuName = "ScriptableObjects/ResidentOrders/ResidentOrderSettings", order = 1)]
    public class ResidentOrderSettings : ScriptableObject
    {
        [field: SerializeField] public List<OrderMethodPair> OrderMethodPairs { get; private set; }

        public GameObject GetMethod(Food food) =>
            OrderMethodPairs.FirstOrDefault(p => p.Food.GetType() == food.GetType()).Method;

        public OrderMethodPair GetRandomPair() =>
            OrderMethodPairs[Random.Range(0, OrderMethodPairs.Count)];
    }

    [Serializable]
    public struct OrderMethodPair
    {
        public Food Food;
        public GameObject Method;
    }
}