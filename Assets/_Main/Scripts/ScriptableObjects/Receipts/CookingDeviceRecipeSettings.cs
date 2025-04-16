using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Receipts
{
    [CreateAssetMenu(fileName = "NewCookingDeviceRecipeSettings", menuName = "ScriptableObjects/FoodRecipes/CookingDeviceRecipeSettings", order = 1)]
    public class CookingDeviceRecipeSettings : ScriptableObject
    {
        [field: SerializeField] public Sprite DeviceIcon { get; private set; }
        [field: SerializeField] public List<FoodRecipeSettings> FoodRecipeSettings { get; private set; }
        [field: SerializeField] public Food DubiousFood { get; private set; }

        public Food GetFoodByIngredients(List<Food> ingredients)
        {
            Food food = DubiousFood;
            foreach (var foodRecipeSetting in FoodRecipeSettings)
            {
                if (!foodRecipeSetting.IngredientsIsMatch(ingredients))
                    continue;
                
                food = foodRecipeSetting.FoodOut;
            }

            return food;
        }
    }
}