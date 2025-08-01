using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.Classic
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private DoorSign _doorSign;
        [SerializeField] private Vector3 _closeAngle;
        [SerializeField] private Vector3 _openAngele;
        [SerializeField] private Transform _doorRoot;
        [SerializeField] private float _rotationSpeed = 4;

        private DoorStateMachine.DoorStateMachine _doorStateMachine;

        private void Awake()
        {
            _doorStateMachine = new DoorStateMachine.DoorStateMachine(_closeAngle, _openAngele, _doorRoot, _rotationSpeed);
            _doorStateMachine.ToClose();
        }

        private void OnDestroy() =>
            _doorStateMachine.Dispose();

        private void Update() =>
            _doorStateMachine.UpdateStates();

        public void OnClick() =>
            _doorStateMachine.OnClick();

        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }
    }
}