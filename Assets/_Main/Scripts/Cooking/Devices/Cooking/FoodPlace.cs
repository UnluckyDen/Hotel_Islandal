using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class FoodPlace : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private Transform _place;

        private IMovableObject _movableObject;
        
        public bool IsEmpty => _movableObject == null;
        public bool MayContainMultipleObjects => false;
        
        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public void PlaceMovableObject(IMovableObject movableObject)
        {
            if (_movableObject != null || movableObject == null)
                return;

            _movableObject = movableObject;

            _movableObject.transform.SetParent(transform);
            _movableObject.transform.localPosition = Vector3.zero;
            _movableObject.transform.localEulerAngles = Vector3.zero;

            _movableObject.ToNonInteractive();
        }

        public IMovableObject TakeMovableObject()
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