using System.Collections.Generic;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class DeviceIngredientGroupPlace : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private Vector3 _offsetRotation;
        [SerializeField] private int _maxNumber = 1;

        private List<IMovableObject> _objectList = new List<IMovableObject>();
        
        public void InGroup(IMovableObject movableObject)
        {
            movableObject.transform.SetParent(_root);
            movableObject.transform.localScale = new Vector3(1, 1, 1);

            _objectList.Add(movableObject);
            UpdateGroupPosition();
        }

        public void OutGroup(IMovableObject movableObject)
        {
            movableObject.transform.SetParent(null);
            movableObject.transform.localScale = new Vector3(1, 1, 1);
            movableObject.transform.localEulerAngles = Vector3.zero;
            
            _objectList.Remove(movableObject);
            UpdateGroupPosition();
        }

        [ContextMenu("UpdateGroupPosition")]
        private void UpdateGroupPosition()
        {
            for (int i = 0; i < _objectList.Count; i++)
            {
                int current = 0;
                if (_maxNumber != 0) 
                    current = i % _maxNumber;
                
                _objectList[i].transform.localPosition = Vector3.zero;
                _objectList[i].transform.localPosition += _offsetPosition * current;
                _objectList[i].transform.localEulerAngles = Vector3.zero;
                _objectList[i].transform.localEulerAngles += _offsetRotation * current;

            }
        }
    }
}