
using _Main.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ElevatorDoor.ElevatorDoorStareMachine
{
    public class ElevatorDoorCloseState : IState
    {
        private readonly Vector3 _closePositionLeft;
        private readonly Vector3 _closePositionRight;
        private readonly Transform _leftRoot;
        private readonly Transform _rightRoot;
        private readonly float _moveSpeed;
        private readonly ElevatorDoorStateMachine _elevatorDoorStateMachine;

        private bool _done;
        private float _factor;

        public ElevatorDoorCloseState(Vector3 closePositionLeft,
            Vector3 closePositionRight,
            Transform leftRoot,
            Transform rightRoot,
            float moveSpeed,
            ElevatorDoorStateMachine elevatorDoorStateMachine)
        {
            _closePositionLeft = closePositionLeft;
            _closePositionRight = closePositionRight;
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
                _leftRoot.localPosition = Vector3.Lerp(leftStartPosition, _closePositionLeft, _factor);
                _rightRoot.localPosition = Vector3.Lerp(rightStartPosition, _closePositionRight, _factor);
                _factor += Time.deltaTime * _moveSpeed;
                return;
            }

            _done = true;
            _elevatorDoorStateMachine.StateChanged(false);
            
            _leftRoot.localPosition = _closePositionLeft;
            _rightRoot.localPosition = _closePositionRight;
        }

        public void Dispose()
        {
        }
    }
}