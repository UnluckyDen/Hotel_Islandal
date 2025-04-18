using _Main.Scripts.Environment.Doors.Classic.DoorStateMachine;
using _Main.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ResidentDoor.ResidentsDoorStateMachine
{
    public class ResidentDoorStateMachine : BaseStateMachine
    {
        private readonly Vector3 _openAngle;
        private readonly Vector3 _closeAngle;
        private readonly Transform _doorRoot;
        private readonly float _moveSpeed;
        
        private ResidentDoorKnocker _residentDoorKnocker;
        private ResidentObjectPlace _residentObjectPlace;
        
        private readonly ResidentDoorCloseState _closeDoorState;
        private readonly ResidentDoorOpenState _openDoorState;

        public ResidentDoorStateMachine(Vector3 closeAngle,
            Vector3 openAngle,
            Transform doorRoot,
            float moveSpeed,
            ResidentDoorKnocker residentDoorKnocker,
            ResidentObjectPlace residentObjectPlace)
        {
            _closeAngle = closeAngle;
            _openAngle = openAngle;
            _doorRoot = doorRoot;
            _moveSpeed = moveSpeed;

            _residentDoorKnocker = residentDoorKnocker;
            _residentObjectPlace = residentObjectPlace;

            _closeDoorState = new ResidentDoorCloseState(this, _closeAngle, _openAngle, _doorRoot, _moveSpeed,
                _residentDoorKnocker, _residentObjectPlace);
            _openDoorState = new ResidentDoorOpenState(this, _closeAngle, _openAngle, _doorRoot, _moveSpeed,
                _residentDoorKnocker, _residentObjectPlace);
        }

        // public void OnClick()
        // {
        //     var doorState = (IDoorState)CurrentState;
        //     doorState.OnClick();
        // }

        public void ToClose()
        {
            if (CurrentState is not ResidentDoorCloseState)
                ToState(_closeDoorState);
        }

        public void ToOpen()
        {
            if (CurrentState is not ResidentDoorOpenState)
                ToState(_openDoorState);
        }
    }
}