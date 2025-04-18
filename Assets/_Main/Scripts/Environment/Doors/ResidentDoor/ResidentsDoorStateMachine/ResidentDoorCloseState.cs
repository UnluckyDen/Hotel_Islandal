using UnityEngine;
using IState = _Main.Scripts.Utils.StateMachine.IState;

namespace _Main.Scripts.Environment.Doors.ResidentDoor.ResidentsDoorStateMachine
{
    public class ResidentDoorCloseState : IState
    {
        private Vector3 _closedAngle;
        private Vector3 _openAngle;
        private Transform _doorRoot;
        private float _rotationSpeed;
        private ResidentDoorStateMachine _doorStateMachine;
        private ResidentDoorKnocker _residentDoorKnocker;
        private ResidentObjectPlace _residentObjectPlace;

        private bool _done;
        private float _factor;
        
        public ResidentDoorCloseState(ResidentDoorStateMachine doorStateMachine,
            Vector3 closedAngle,
            Vector3 openAngle,
            Transform doorRoot,
            float rotationSpeed,
            ResidentDoorKnocker residentDoorKnocker,
            ResidentObjectPlace residentObjectPlace)
        {
            _doorStateMachine = doorStateMachine;
            _closedAngle = closedAngle;
            _openAngle = openAngle;
            _doorRoot = doorRoot;
            _rotationSpeed = rotationSpeed;

            _residentDoorKnocker = residentDoorKnocker;
            _residentObjectPlace = residentObjectPlace;
        }
        
        public void Enter()
        {
            _done = false;
            _factor = 0f;
            
            _residentDoorKnocker.gameObject.SetActive(true);
            _residentObjectPlace.gameObject.SetActive(false);
        }

        public void Update()
        {
            if (_done)
                return;
            
            if (!_done && _factor < 1f)
            {
                _doorRoot.localEulerAngles = Vector3.Lerp(_openAngle, _closedAngle, _factor);
                _factor += Time.deltaTime * _rotationSpeed;
                return;
            }

            _done = true;
            _doorRoot.localEulerAngles = _closedAngle;
        }

        public void Dispose()
        {
            _residentDoorKnocker.gameObject.SetActive(false);
            _residentObjectPlace.gameObject.SetActive(true);
        }
    }
}