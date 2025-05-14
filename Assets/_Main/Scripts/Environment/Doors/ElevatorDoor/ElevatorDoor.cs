using _Main.Scripts.Environment.Doors.ElevatorDoor.ElevatorDoorStareMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ElevatorDoor
{
    public class ElevatorDoor : MonoBehaviour
    {
        [SerializeField] private Vector3 _closePositionLeft;
        [SerializeField] private Vector3 _closePositionRight;
        [SerializeField] private Vector3 _openPositionLeft;
        [SerializeField] private Vector3 _openPositionRight;
        [SerializeField] private Transform _leftRoot;
        [SerializeField] private Transform _rightRoot;
        [SerializeField] private float _moveSpeed = 4;

        private ElevatorDoorStateMachine _doorStateMachine;

        private void Awake()
        {
            _doorStateMachine = new ElevatorDoorStateMachine(_closePositionLeft, 
                _closePositionRight,
                _openPositionLeft,
                _openPositionRight,
                _leftRoot,
                _rightRoot,
                _moveSpeed);
            
            //_doorStateMachine.ToClose();
            _doorStateMachine.ToOpen();
        }

        private void OnDestroy() =>
            _doorStateMachine.Dispose();

        private void Update() =>
            _doorStateMachine.UpdateStates();

        [ContextMenu("Open")]
        public void OpenDoor()
        {
            _doorStateMachine.ToOpen();
        }

        [ContextMenu("Close")]
        public void CloseDoor()
        {
            _doorStateMachine.ToClose();
        }
    }
}