using System.Collections.Generic;
using System.Text;
using _Main.Scripts.Cooking.Foods;
using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public readonly struct FoodCombined : ICustomAnalyticsEvent
    {
        private readonly List<Food> _foodIn;
        private readonly Food _foodOut;

        public FoodCombined(List<Food> foodIn, Food foodOut)
        {
            _foodIn = foodIn;
            _foodOut = foodOut;
        }

        public CustomEvent GetCustomEvent()
        {
            var customEvent = new CustomEvent("food_combined")
            {
                {"food_list", GetFoodString(_foodIn, true)},
                {"food_list", GetFoodString(_foodOut, false)}
            };
            
            return customEvent;
        }

        private string GetFoodString(List<Food> foods, bool isFoodIn)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(isFoodIn ? "Food in : " : "Food out : ");

            foreach (var food in foods)
            {
                stringBuilder.Append("Name: ");
                stringBuilder.Append(food.GetFoodName());
                stringBuilder.Append(", CookingTime");
                stringBuilder.Append(food.CurrentCookingTime);
                stringBuilder.Append(", ");
            }
            
            return stringBuilder.ToString();
        }
        
        private string GetFoodString(Food food, bool isFoodIn)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(isFoodIn ? "Food in : " : "Food out : ");
            
            stringBuilder.Append(food.GetFoodName());
            stringBuilder.Append(food.CurrentCookingTime);
            stringBuilder.Append(", ");
            
            return stringBuilder.ToString();
        }
    }
}