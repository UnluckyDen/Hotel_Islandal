using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Receipts
{
    [CreateAssetMenu(fileName = "NewCookingDeviceRecipeSettings", menuName = "ScriptableObjects/FoodRecipes/CookingDeviceRecipeSettings", order = 1)]
    public class CookingDeviceRecipeSettings : ScriptableObject
    {
        [field:SerializeField] public Sprite DeviceIcon { get; private set; }
        [field:SerializeField] public List<Food> FoodIn { get; private set; }
        [field:SerializeField] public Food FoodOut { get; private set; }
        [field:SerializeField] public float CookingTime { get; private set; }
    }
}