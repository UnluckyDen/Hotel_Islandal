using System;
using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Receipts
{
    [CreateAssetMenu(fileName = "NewFoodRecipeSettings", menuName = "ScriptableObjects/FoodRecipes/FoodRecipeSettings", order = 1)]
    public class FoodRecipeSettings : ScriptableObject
    {
        [field: SerializeField] public List<FoodTimePair> FoodIn { get; private set; }
        [field: SerializeField] public Food FoodOut { get; private set; }
        [field: SerializeField] public float Accuracy { get; private set; } = 2f;

        public bool IngredientsIsMatch(List<Food> foods)
        {
            if (foods == null && FoodIn == null) 
                return true;
            
            if (foods == null || FoodIn == null) 
                return false;
            
            if (foods.Count != FoodIn.Count)
                return false;
    
            List<FoodTimePair> recipeFoods = new List<FoodTimePair>(FoodIn);
    
            foreach (Food food in foods)
            {
                bool found = false;
                for (int i = 0; i < recipeFoods.Count; i++)
                {
                    if (food.GetType() == recipeFoods[i].Food.GetType() 
                        && Mathf.Abs(food.CurrentCookingTime - recipeFoods[i].CookingTime) < Accuracy)
                    {
                        recipeFoods.RemoveAt(i);
                        found = true;
                        break;
                    }
                }
        
                if (!found) return false;
            }
    
            return recipeFoods.Count == 0;   
        }
    }

    [Serializable]
    public struct FoodTimePair
    {
        public Food Food;
        public float CookingTime;
    }
}