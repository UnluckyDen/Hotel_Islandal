using _Main.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.StateMachine
{
    public class DoorStateMachine : BaseStateMachine
    {
        private Vector3 _openAngle;
        private Vector3 _closeAngle;
        private Transform _doorRoot;
        private float _moveSpeed;
        
        private CloseDoorState _closeDoorState;
        private OpenDoorState _openDoorState;

        public DoorStateMachine(Vector3 closeAngle, Vector3 openAngle, Transform doorRoot, float moveSpeed)
        {
            _closeAngle = closeAngle;
            _openAngle = openAngle;
            _doorRoot = doorRoot;
            _moveSpeed = moveSpeed;

            _closeDoorState = new CloseDoorState(_closeAngle, _openAngle, _doorRoot, _moveSpeed);
            _openDoorState = new OpenDoorState(_closeAngle, _openAngle, _doorRoot, _moveSpeed);
        }

        public void ChangeState()
        {
            var state = (IDoorState)CurrentState;
            
            if (state.InProgress)
                return;
            
            if (CurrentState == _closeDoorState)
            {
                ToOpen();
                return;
            }
            
            if (CurrentState == _openDoorState)
            {
                ToClose();
                return;
            }
        }
        
        public void ToClose() => ToState(_closeDoorState);
        public void ToOpen() => ToState(_openDoorState);
    }
}