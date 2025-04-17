using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Player.PlayerObjectManipulator
{
    public class PlayerManipulatorPlace : MonoBehaviour, IObjectPlace
    {
        private IMovableObject _movableObject;

        public bool IsEmpty => _movableObject == null;
        public bool MayContainMultipleObjects => false;

        public void PlaceMovableObject(IMovableObject movableObject)
        {
            if (_movableObject != null || movableObject == null)
                return;

            _movableObject = movableObject;
            
            _movableObject.ToNonInteractive();
            _movableObject.transform.SetParent(transform);
            _movableObject.transform.localPosition = Vector3.zero;
            _movableObject.transform.localEulerAngles = Vector3.zero;
            
            _movableObject = movableObject;
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