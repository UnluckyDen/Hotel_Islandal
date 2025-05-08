using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ResidentDoor
{
    public class ResidentObjectPlace : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private Transform _objectPlace;

        private ResidentDoor _residentDoor;
        private BaseResident _resident;
        private Food _food;
        
        private IMovableObject _movableObject;

        public IMovableObject CurrentMovableObject => _movableObject;

        public bool CanApply(IMovableObject movableObject) =>
            _movableObject == null && movableObject is Food;

        public void Init(ResidentDoor residentDoor, BaseResident resident)
        {
            _residentDoor = residentDoor;
            _resident = resident;
        }

        public void PlaceMovableObject(IMovableObject movableObject)
        {
            if (movableObject == null) 
                return;
            
            if (movableObject is Food food)
            {
                _food = food;
                if (_resident.TryAcceptOrder(food))
                {
                    _food.transform.SetParent(_objectPlace);
                    _food.transform.localPosition = Vector3.zero;
                    _food.transform.localEulerAngles = Vector3.zero;
                    _food.ToNonInteractive();
                    return;
                }

                Destroy(food.gameObject);
                return;
            }
            
            movableObject.ToNonInteractive();
            movableObject.transform.SetParent(_objectPlace);
            movableObject.transform.localPosition = Vector3.zero;
            movableObject.transform.localEulerAngles = Vector3.zero;
            
            _movableObject = movableObject;
        }

        public IMovableObject TakeMovableObject()
        {
            if (_movableObject == null)
                return null;
            
            IMovableObject movableObject = _movableObject;
            _movableObject = null;
            
            movableObject.transform.SetParent(null);
            movableObject.ToInteractable();
            return movableObject;
        }

        public void OnHoverEnter() => 
            _residentDoor.OnHoverEnter();

        public void OnHoverExit() => 
            _residentDoor.OnHoverExit();
    }
}