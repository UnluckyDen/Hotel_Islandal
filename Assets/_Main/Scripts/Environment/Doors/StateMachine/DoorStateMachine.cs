using _Main.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.StateMachine
{
    public class DoorStateMachine : BaseStateMachine
    {
        private readonly Vector3 _openAngle;
        private readonly Vector3 _closeAngle;
        private readonly Transform _doorRoot;
        private readonly float _moveSpeed;
        
        private readonly CloseDoorState _closeDoorState;
        private readonly OpenDoorState _openDoorState;

        public DoorStateMachine(Vector3 closeAngle, Vector3 openAngle, Transform doorRoot, float moveSpeed)
        {
            _closeAngle = closeAngle;
            _openAngle = openAngle;
            _doorRoot = doorRoot;
            _moveSpeed = moveSpeed;

            _closeDoorState = new CloseDoorState(this, _closeAngle, _openAngle, _doorRoot, _moveSpeed);
            _openDoorState = new OpenDoorState(this, _closeAngle, _openAngle, _doorRoot, _moveSpeed);
        }

        public void OnClick()
        {
            var doorState = (IDoorState)CurrentState;
            doorState.OnClick();
        }
        
        public void ToClose() => ToState(_closeDoorState);
        public void ToOpen() => ToState(_openDoorState);
    }
}