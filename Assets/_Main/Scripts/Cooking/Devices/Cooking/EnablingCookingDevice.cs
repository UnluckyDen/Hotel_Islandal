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
        
        public override bool IsEmpty => _foodIn.Count == 0;
        
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
            movableObject.transform.position = transform.position;
            movableObject.transform.SetParent(transform);
            
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
                return;
            }
            
            PlayCookingAnimation(false);
            _isCooking = false;
        }
    }
}