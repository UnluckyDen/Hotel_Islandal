using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class EnablingCookingDevice : CookingDevice
    {
        public event Action<EnablingCookingDevice, bool> DeviceActivation;
        
        [SerializeField] private DeviceButton _deviceButton;
        [SerializeField] private Transform _foodPlace;
        
        private List<Food> _foodIn = new();
        private Food _foodOut;
        private bool _isCooking;

        public override IMovableObject CurrentMovableObject => _foodIn.FirstOrDefault();

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
            movableObject.transform.position = _foodPlace.position;
            movableObject.transform.SetParent(_foodPlace);
            
            _foodIn.Add(food);
        }

        public override IMovableObject TakeMovableObject()
        {
            if (_isCooking)
                return null;
            
            CombineFood();

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
            
            _foodOut = Instantiate(_recipeSettings.GetFoodByIngredients(_foodIn), transform);
            foreach (var food in _foodIn)
                Destroy(food.gameObject);
                    
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
            DeviceActivation?.Invoke(this, _isCooking);
        }
    }
}