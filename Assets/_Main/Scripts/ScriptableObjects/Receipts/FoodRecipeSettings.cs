using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Receipts
{
    [CreateAssetMenu(fileName = "NewFoodRecipeSettings", menuName = "ScriptableObjects/FoodRecipes/FoodRecipeSettings", order = 1)]
    public class FoodRecipeSettings : ScriptableObject
    {
        [field: SerializeField] public Sprite DeviceIcon { get; private set; }
        [field: SerializeField] public List<Food> FoodIn { get; private set; }
        [field: SerializeField] public Food FoodOut { get; private set; }
        [field: SerializeField] public float CookingTime { get; private set; }
        [field: SerializeField] public float Accuracy { get; private set; } = 2f;

        public bool IngredientsIsMatch(List<Food> foods)
        {
            if (foods == null && FoodIn == null) 
                return true;
            
            if (foods == null || FoodIn == null) 
                return false;
            
            if (foods.Count != FoodIn.Count)
                return false;
    
            List<Food> tempList = new List<Food>(FoodIn);
    
            foreach (Food item in foods)
            {
                bool found = false;
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (item.GetType() == tempList[i].GetType() 
                        && Mathf.Abs(item.CurrentCookingTime - tempList[i].CurrentCookingTime) < Accuracy)
                    {
                        tempList.RemoveAt(i);
                        found = true;
                        break;
                    }
                }
        
                if (!found) return false;
            }
    
            return tempList.Count == 0;   
        }
    }
}