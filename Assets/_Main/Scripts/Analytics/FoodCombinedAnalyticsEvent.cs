using System.Collections.Generic;
using System.Text;
using _Main.Scripts.Cooking.Foods;
using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public readonly struct FoodCombinedAnalyticsEvent : ICustomAnalyticsEvent
    {
        private readonly List<Food> _foodIn;
        private readonly Food _foodOut;

        public FoodCombinedAnalyticsEvent(List<Food> foodIn, Food foodOut)
        {
            _foodIn = foodIn;
            _foodOut = foodOut;
        }

        public CustomEvent GetCustomEvent()
        {
            var customEvent = new CustomEvent("food_combined")
            {
                {"food_list", GetFoodString()},
            };
            
            return customEvent;
        }

        private string GetFoodString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Food in : ");

            foreach (var food in _foodIn)
            {
                stringBuilder.Append("Name: ");
                stringBuilder.Append(food.GetFoodName());
                stringBuilder.Append(", CookingTime: ");
                stringBuilder.Append(food.CurrentCookingTime);
                stringBuilder.Append(", ");
            }

            stringBuilder.Append("Food out : ");
            
            stringBuilder.Append(_foodOut.GetFoodName());
            stringBuilder.Append(_foodOut.CurrentCookingTime);
            stringBuilder.Append(", ");
            
            return stringBuilder.ToString();
        }
    }
}