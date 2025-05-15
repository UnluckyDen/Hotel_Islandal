using _Main.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ElevatorDoor.ElevatorDoorStareMachine
{
    public class ElevatorDoorOpenState : IState
    {
        private readonly Vector3 _openPositionLeft;
        private readonly Vector3 _openPositionRight;
        private readonly Transform _leftRoot;
        private readonly Transform _rightRoot;
        private readonly float _moveSpeed;
        private ElevatorDoorStateMachine _elevatorDoorStateMachine;

        private bool _done;
        private float _factor;

        public ElevatorDoorOpenState(Vector3 openPositionLeft, 
            Vector3 openPositionRight,
            Transform leftRoot,
            Transform rightRoot,
            float moveSpeed,
            ElevatorDoorStateMachine elevatorDoorStateMachine)
        {
            _openPositionLeft = openPositionLeft;
            _openPositionRight = openPositionRight;
            _leftRoot = leftRoot;
            _rightRoot = rightRoot;
            _moveSpeed = moveSpeed;
            _elevatorDoorStateMachine = elevatorDoorStateMachine;
        }
        
        public void Enter()
        {
            _done = false;
            _factor = 0f;
        }

        public void Update()
        {
            if (_done)
                return;

            Vector3 leftStartPosition = _leftRoot.localPosition;
            Vector3 rightStartPosition = _rightRoot.localPosition;
            
            if (!_done && _factor < 1f)
            {
                _leftRoot.localPosition = Vector3.Lerp(leftStartPosition, _openPositionLeft, _factor);
                _rightRoot.localPosition = Vector3.Lerp(rightStartPosition, _openPositionRight, _factor);
                _factor += Time.deltaTime * _moveSpeed;
                return;
            }

            _done = true;
            
            _leftRoot.localPosition = _openPositionLeft;
            _rightRoot.localPosition = _openPositionRight;
        }

        public void Dispose()
        {
        }
    }
}