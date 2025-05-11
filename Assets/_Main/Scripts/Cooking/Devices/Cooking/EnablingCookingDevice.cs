using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Analytics;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class EnablingCookingDevice : CookingDevice
    {
        public event Action<EnablingCookingDevice, bool> DeviceActivation;
        
        [SerializeField] private DeviceButton _deviceButton;
        [SerializeField] private DeviceIngredientGroupPlace _foodGroup;
        
        private List<Food> _foodIn = new();
        private Food _foodOut;
        private bool _isCooking;

        public override IMovableObject CurrentMovableObject => _foodOut;

        private void Start()
        {
            _deviceButton.ButtonPressed += DeviceButtonOnClick;
        }

        private void OnDestroy()
        {
            _deviceButton.ButtonPressed -= DeviceButtonOnClick;
        }

        public override void PlaceMovableObject(IMovableObject movableObject)
        {
            if (movableObject is not Food food) 
                return;
            
            movableObject.ToNonInteractive();
            _foodGroup.InGroup(movableObject);
            
            _foodIn.Add(food);
        }

        public override IMovableObject TakeMovableObject()
        {
            if (_isCooking)
                return null;
            
            if (_foodOut == null)
                return null;
            
            var food = _foodOut;
            _foodOut = null;
            return food;
        }

        private void Update()
        {
            if (!_isCooking)
                return;
            
            HandleCooking();
        }

        private void CombineFood()
        {
            if (_foodIn == null || _foodIn.Count == 0)
                return;
            
            _foodOut = Instantiate(_recipeSettings.GetFoodByIngredients(_foodIn), _foodGroup.transform);
            
            if (_foodOut != null)
                GlobalAnalyticsService.Instance.SendCustomEvent(new FoodCombined(_foodIn, _foodOut));
            
            foreach (var food in _foodIn)
            {
                _foodGroup.OutGroup(food);
                Destroy(food.gameObject);
            }

            _foodIn.Clear();
        }

        private void HandleCooking()
        {
            foreach (var food in _foodIn)
                food.AddCookingTime(Time.deltaTime);
        }

        private void DeviceButtonOnClick(bool click)
        {
            if (click)
            {
                _isCooking = true;
                PlayCookingAnimation(true);
                PlayCookingSound(true);
                DeviceActivation?.Invoke(this, _isCooking);
                return;
            }

            PlayCookingAnimation(false);
            PlayCookingSound(false);
            _isCooking = false;
            CombineFood();
            DeviceActivation?.Invoke(this, _isCooking);
        }
    }
}