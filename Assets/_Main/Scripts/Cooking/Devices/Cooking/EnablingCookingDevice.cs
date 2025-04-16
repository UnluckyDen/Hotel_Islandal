using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class EnablingCookingDevice : CookingDevice
    {
        [SerializeField] private DeviceButton _deviceButton;
        [SerializeField] private Transform _visual;
        
        private List<Food> _foodIn = new();
        private Food _foodOut;
        private bool _isCooking;

        public override bool IsEmpty => _foodOut == null;

        private void Start()
        {
            _deviceButton.Subscribe(DeviceButtonOnClick);
        }

        private void OnDestroy()
        {
            _deviceButton.Unsubscribe(DeviceButtonOnClick);
        }

        public override bool PlaceMovableObject(IMovableObject movableObject)
        {
            if (movableObject is not Food food) 
                return false;
            
            movableObject.ToNonInteractive();
            movableObject.transform.position = transform.position;
            
            _foodIn.Add(food);
            return true;
        }

        public override IMovableObject TakeMovableObject()
        {
            if (_foodOut == null || _isCooking)
                return null;
            
            var food = _foodOut;
            _foodOut = null;
            return food;

        }

        private void Update()
        {
            if (!_isCooking)
                return;
            
            if (_foodOut != null)
                HandleCooking();
        }

        private void CombineFood()
        {
            if (_foodIn == null || _foodIn.Count == 0)
                return;
            
            _foodOut = Instantiate(_recipeSettings.GetFoodByIngredients(_foodIn), transform);
            foreach (var food in _foodIn)
                Destroy(food);
                    
            _foodIn.Clear();
        }

        private void HandleCooking()
        {
            _foodOut.AddCookingTime(Time.deltaTime);
        }

        private void DeviceButtonOnClick(bool click)
        {
            if (click)
            {
                _isCooking = true;
                CombineFood();
                PlayCookingAnimation(true);
                return;
            }
            
            PlayCookingAnimation(false);
            _isCooking = false;
        }
    }
}