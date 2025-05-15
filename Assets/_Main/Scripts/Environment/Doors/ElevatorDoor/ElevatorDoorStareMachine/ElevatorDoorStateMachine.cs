using System;
using _Main.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ElevatorDoor.ElevatorDoorStareMachine
{
    public class ElevatorDoorStateMachine : BaseStateMachine
    {
        public event Action<bool> DoorStateChangeComplete;

        private readonly Vector3 _closePositionLeft;
        private readonly Vector3 _closePositionRight;
        private readonly Vector3 _openPositionLeft;
        private readonly Vector3 _openPositionRight;
        private readonly Transform _leftRoot;
        private readonly Transform _rightRoot;
        private readonly float _moveSpeed;

        private ElevatorDoorCloseState _elevatorDoorCloseState;
        private ElevatorDoorOpenState _elevatorDoorOpenState;

        public ElevatorDoorStateMachine(Vector3 closePositionLeft,
            Vector3 closePositionRight,
            Vector3 openPositionLeft,
            Vector3 openPositionRight,
            Transform leftRoot,
            Transform rightRoot,
            float moveSpeed)
        {
            _closePositionLeft = closePositionLeft;
            _closePositionRight = closePositionRight;
            _openPositionLeft = openPositionLeft;
            _openPositionRight = openPositionRight;
            _leftRoot = leftRoot;
            _rightRoot = rightRoot;
            _moveSpeed = moveSpeed;
            
            _elevatorDoorCloseState = new ElevatorDoorCloseState(_closePositionLeft, _closePositionRight, _leftRoot, _rightRoot, _moveSpeed, this);
            _elevatorDoorOpenState = new ElevatorDoorOpenState(_openPositionLeft, _openPositionRight, _leftRoot, _rightRoot, _moveSpeed, this);
        }
        
        public void ToClose() => ToState(_elevatorDoorCloseState);
        public void ToOpen() => ToState(_elevatorDoorOpenState);

        public void StateChanged(bool opened)
        {
            DoorStateChangeComplete?.Invoke(opened);
        }
    }
}