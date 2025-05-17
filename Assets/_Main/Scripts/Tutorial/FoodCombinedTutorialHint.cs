using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Utils.GameObjectGroups;
using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class FoodCombinedTutorialHint : BaseTutorialHint
    {
        [SerializeField] private EnablingCookingDevice _enablingCookingDevice;
            
        private void Start()
        {
            _enablingCookingDevice.FoodCombined += EnablingCookingDeviceOnFoodCombined;
            _enablingCookingDevice.FoodGiven += EnablingCookingDeviceOnFoodGiven;
        }

        private void OnDestroy()
        {
            _enablingCookingDevice.FoodCombined -= EnablingCookingDeviceOnFoodCombined;
            _enablingCookingDevice.FoodGiven -= EnablingCookingDeviceOnFoodGiven;
        }

        private void EnablingCookingDeviceOnFoodCombined(Food food)
        {
            if (food is EspressoCoffee)
                ShowHint();
        }
        
        private void EnablingCookingDeviceOnFoodGiven(Food food)
        {
            if (food is EspressoCoffee)
                HideHint();
        }
    }
}