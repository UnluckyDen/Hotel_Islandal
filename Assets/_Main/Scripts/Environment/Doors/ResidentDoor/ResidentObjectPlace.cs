using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using _Main.Scripts.NPCs.Resident;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ResidentDoor
{
    public class ResidentObjectPlace : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private Transform _objectPlace;
        [SerializeField] BaseResident _resident;

        private ResidentDoor _residentDoor;
        private Food _food;
        
        private readonly List<IMovableObject> _movableObjects = new();

        public bool IsEmpty => _movableObjects.Count == 0;
        public bool MayContainMultipleObjects => true;

        public void Init(ResidentDoor residentDoor)
        {
            _residentDoor = residentDoor;
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
            
            _movableObjects.Add(movableObject);
        }

        public IMovableObject TakeMovableObject()
        {
            if (_movableObjects.Count <= 0)
                return null;
            
            IMovableObject movableObject = _movableObjects.Last();
            _movableObjects.Remove(movableObject);
            
            movableObject.transform.SetParent(null);
            movableObject.ToInteractable();
            return movableObject;
        }

        public void OnHoverEnter()
        {
            _residentDoor.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _residentDoor.OnHoverExit();
        }
    }
}