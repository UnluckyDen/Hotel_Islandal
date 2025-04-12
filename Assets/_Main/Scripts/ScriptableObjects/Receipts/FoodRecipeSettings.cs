using System.Collections.Generic;
using _Main.Scripts.Cooking;
using _Main.Scripts.Cooking.Food;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Receipts
{
    [CreateAssetMenu(fileName = "NewFoodRecipeSettings", menuName = "ScriptableObjects/FoodRecipes/FoodRecipeSettings", order = 1)]
    public class FoodRecipeSettings : ScriptableObject
    {
        [field:SerializeField] public List<Food> FoodIn { get; private set; }
        [field:SerializeField] public Food FoodOut { get; private set; }
        [field:SerializeField] public float CookingTime { get; private set; }
    }
}