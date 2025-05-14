using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Main.Scripts.Cooking.Devices.Cooking;
using _Main.Scripts.Cooking.Foods;
using Unity.Services.Analytics;

namespace _Main.Scripts.Analytics
{
    public readonly struct FoodCombinedAnalyticsEvent : ICustomAnalyticsEvent
    {
        private readonly List<Food> _foodIn;
        private readonly Food _foodOut;
        private readonly CookingDevice _cookingDevice;

        public FoodCombinedAnalyticsEvent(List<Food> foodIn, Food foodOut, CookingDevice cookingDevice)
        {
            _foodIn = foodIn;
            _foodOut = foodOut;
            _cookingDevice = cookingDevice;
        }

        public CustomEvent GetCustomEvent()
        {
            var customEvent = new CustomEvent("food_combined")
            {
                {"device_name", _cookingDevice.name},
                {"cooking_time", _foodIn.First().CurrentCookingTime},
                {"food_list", GetFoodString()},
                {"food_out", _foodOut.GetFoodName()},
            };
            
            return customEvent;
        }

        private string GetFoodString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var food in _foodIn)
            {
                stringBuilder.Append(food.GetFoodName());
                stringBuilder.Append($" ({food.CurrentCookingTime})");
                if (_foodIn.Last() != food)
                    stringBuilder.Append(", ");
            }
            
            return stringBuilder.ToString();
        }
    }
}