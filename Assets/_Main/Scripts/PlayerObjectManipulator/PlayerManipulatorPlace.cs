using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.PlayerObjectManipulator
{
    public class PlayerManipulatorPlace : MonoBehaviour, IObjectPlace
    {
        private IMovableObject _movableObject;

        public bool IsEmpty => _movableObject == null;
        public bool MayContainMultipleObjects => false;

        public bool PlaceMovableObject(IMovableObject movableObject)
        {
            if (_movableObject != null || movableObject == null)
                return false;

            _movableObject = movableObject;
            
            _movableObject.ToNonInteractive();
            _movableObject.transform.SetParent(transform);
            _movableObject.transform.localPosition = Vector3.zero;
            _movableObject.transform.localEulerAngles = Vector3.zero;
            
            _movableObject = movableObject;

            return true;
        }

        public IMovableObject TakeMovableObject()
        {
            if (_movableObject == null)
                return null;

            var movableObject = _movableObject;
            _movableObject = null;
            
            movableObject.ToInteractable();
            movableObject.transform.SetParent(null);

            return movableObject;
        }
    }
}