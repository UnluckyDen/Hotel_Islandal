using _Main.Scripts.Interfaces;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class MovableObjectPlace : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private Transform _place;
        [SerializeField] private BaseHoverGroup _hoverGroup;

        private IMovableObject _movableObject;

        public IMovableObject CurrentMovableObject => _movableObject;
        public bool CanApply(IMovableObject movableObject) => _movableObject == null;

        public void OnHoverEnter()
        {
            _hoverGroup.OnHoverEnter();
            _movableObject?.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _hoverGroup.OnHoverExit();
            _movableObject?.OnHoverExit();
        }

        public void PlaceMovableObject(IMovableObject movableObject)
        {
            if (_movableObject != null || movableObject == null)
                return;

            _movableObject = movableObject;
            
            _movableObject.transform.SetParent(transform);
            _movableObject.transform.localPosition = Vector3.zero;
            _movableObject.transform.localEulerAngles = Vector3.zero;
            
            OnHoverEnter();

            _movableObject.ToNonInteractive();
        }

        public IMovableObject TakeMovableObject()
        {
            if (_movableObject == null)
                return null;
            
            OnHoverExit();
            
            var movableObject = _movableObject;
            _movableObject = null;

            movableObject.transform.SetParent(null);
            movableObject.ToInteractable();
            
            return movableObject;
        }
    }
}