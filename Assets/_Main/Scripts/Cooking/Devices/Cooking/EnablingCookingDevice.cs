using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class EnablingCookingDevice : CookingDevice
    {
        [SerializeField] private DeviceButton _deviceButton;
        [SerializeField] private Transform _visual;

        private Coroutine _cookingCoroutine;

        private List<Food> _foodIn = new();
        private Food _foodOut;

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
            if (_foodOut == null)
                return null;
            
            var food = _foodOut;
            _foodOut = null;
            return food;

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

        private IEnumerator Cooking()
        {
            CombineFood();
            _visual.localEulerAngles = new Vector3(30,0,0);
            yield return new WaitForSeconds(0.3f);
            _visual.localEulerAngles = new Vector3(0f,0f,0f);
            yield return new WaitForSeconds(0.3f);
            _visual.localEulerAngles = new Vector3(-30f,0f,0f);
            yield return new WaitForSeconds(0.3f);
        }

        private void DeviceButtonOnClick(bool click)
        {
            if (click)
            {
                _cookingCoroutine = StartCoroutine(Cooking());
                return;
            }

            if (_cookingCoroutine != null)
                StopCoroutine(_cookingCoroutine);
        }
    }
}