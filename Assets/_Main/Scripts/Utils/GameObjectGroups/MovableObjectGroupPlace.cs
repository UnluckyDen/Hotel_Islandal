using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Utils.GameObjectGroups
{
    public class MovableObjectGroupPlace : MonoBehaviour, IHoverable
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private Vector3 _offsetRotation;
        [SerializeField] private int _maxNumber = 1;

        private List<IMovableObject> _objectList = new();
        public IMovableObject LastMovableObject => _objectList.LastOrDefault();

        public void InGroup(IMovableObject movableObject)
        {
            movableObject.transform.SetParent(_root);
            movableObject.transform.localScale = new Vector3(1, 1, 1);
            movableObject.ToNonInteractive();

            _objectList.Add(movableObject);
            UpdateGroupPosition();
        }

        public IMovableObject OutGroup()
        {
            IMovableObject movableObject = _objectList.Last();
            movableObject.transform.SetParent(null);
            movableObject.transform.localScale = new Vector3(1, 1, 1);
            movableObject.transform.localEulerAngles = Vector3.zero;
            movableObject.ToInteractable();

            _objectList.Remove(movableObject);
            UpdateGroupPosition();

            return movableObject;
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

        public void OnHoverEnter()
        {
            foreach (var movableObject in _objectList)
                movableObject.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            foreach (var movableObject in _objectList)
                movableObject.OnHoverExit();
        }
    }
}