using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class FoodPlace : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private Transform _place;

        private IMovableObject _movableObject;
        
        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public bool TryPlaceMovableObject(IMovableObject movableObject)
        {
            if (_movableObject != null || movableObject == null)
                return false;

            _movableObject = movableObject;

            _movableObject.transform.SetParent(transform);
            _movableObject.transform.localPosition = Vector3.zero;
            _movableObject.transform.localEulerAngles = Vector3.zero;

            _movableObject.ToNonInteractive();
            
            return true;
        }

        public IMovableObject TryTakeMovableObject()
        {
            if (_movableObject == null)
                return null;

            var movableObject = _movableObject;
            _movableObject = null;

            movableObject.transform.SetParent(null);
            movableObject.ToInteractable();
            
            return movableObject;
        }
    }
}